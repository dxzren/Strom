using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
/// <summary>   服务器选择视图 </summary>

public class ServerSelectPanelViewMediator : EventMediator
{
    [Inject]
    public ServerSelectPanelView View            { set; get; }
    [Inject]
    public IStartData           startData       { set; get; }

    UIGrid                      serverGridLeft;
    UIGrid                      serverGridRight;

    public override void        OnRegister()
    {
        View.Init();

        RefreshHandler();
        View.dispatcher.AddListener( View.CloseSpace_Event, CloseSpace_Handler);

        dispatcher.AddListener  ( StartEvent.GridCallViewClick_Event, GridCallViewClickHandler);
        dispatcher.AddListener  ( StartEvent.RefreshServerName_Event, GridCallViewClickHandler);
    }
    public override void OnRemove()
    {
        dispatcher.RemoveListener  ( StartEvent.GridCallViewClick_Event, GridCallViewClickHandler);
        dispatcher.RemoveListener  ( StartEvent.RefreshServerName_Event, GridCallViewClickHandler);
    }
    public void                 CloseSpace_Handler()
    { }
    public void                 RefreshHandler()
    {
        if( Util.GetIPFile("hasSelected")!= null )
        {
            View.LastServer.text = Util.GetIPFile("hasSelected").serverName;
        }
        else
        {
            View.LastServer.text = "";
        }
        GameObject              obj = ( GameObject )Util.Load( UIPanelConfig.ServerZoneItem);
        Show_LeftItem(obj);
    }
    public void                 Show_LeftItem( GameObject obj)                                  // 显示左边的Item，第一个固定是推荐服务器，以后是：1--10，11-20,依次类推   
    {
        GameObject Item                             = Instantiate(obj) as GameObject;
        View.ListerGrid.AddChild                    (Item.transform);
        Item.transform.localEulerAngles             = Vector3.zero;
        Item.transform.localScale                   = Vector3.one;
        Item.transform.localPosition                = Vector3.zero;
        ServerZoneItemView ItemView                 = Item.GetComponent<ServerZoneItemView>();
        List<int> keys                              = new List<int>(startData.centerServerList.Keys);
        ItemView.serverID                           = keys[keys.Count -1];
        ItemView.name.text                          = "推荐服务器";

        List<int> SvrListKeys                       = new List<int>(startData.centerServerList.Keys);
        for ( int i = 0; i < SvrListKeys.Count; i++)
        {
            GameObject Item_2                       = Instantiate(obj) as GameObject;
            View.ListerGrid.AddChild                (Item_2.transform);
            Item_2.transform.localEulerAngles       = Vector3.zero;
            Item_2.transform.localScale             = Vector3.one;
            Item_2.transform.localPosition          = Vector3.zero;
            ServerZoneItemView ItemView_2           = Item_2.GetComponent<ServerZoneItemView>();
            ItemView_2.serverID                     = SvrListKeys[i];
            ItemView_2.name.text                    = "第" + (i + 1).ToString() + "区";
        }
        GridCallViewClickHandler(null);
    }
    public void                 GridCallViewClickHandler(IEvent evt)                            // 重新加载右边的列表
    {
        if ( View.serverListFather.childCount > 0)                                                                  ///
        {    DestroyImmediate(View.serverListFather.GetChild(0).gameObject);    }

        GameObject              obj                 = ( GameObject)Util.Load(UIPanelConfig.ServerList);             ///
        GameObject              Item                = Instantiate(obj) as GameObject;                               ///

        Item.transform.parent                       = View.serverListFather;                                        ///
        Item.transform.localEulerAngles             = Vector3.zero;                                                 ///
        Item.transform.localScale                   = Vector3.one;                                                  ///
        Item.transform.localPosition                = new Vector3(2,-24,0);                                         ///
        serverGridLeft                              = Item.transform.GetChild(0).GetComponent<UIGrid>();            ///
        serverGridRight                             = Item.transform.GetChild(1).GetComponent<UIGrid>();            ///
        List<GameSrvInfo>       SrvList;                                                                            ///
        bool                    haveOkServer        = false;                                                        ///

        GameObject              Obj_1               = (GameObject)Util.Load(UIPanelConfig.ServerLineItem);              ///
        if (evt == null || evt.data == null)                                                                        /// 推荐服务器
        {
            GameObject          Item_1              = Instantiate(Obj_1) as GameObject;
            serverGridLeft.AddChild                 (Item_1.transform);
            Item_1.transform.localEulerAngles       = Vector3.zero;
            Item_1.transform.localScale             = Vector3.one;
            Item_1.transform.localPosition          = Vector3.zero;

            ServerLineItemView  ItemView            = Item_1.GetComponent<ServerLineItemView>();
            foreach ( int key in startData.centerServerList.Keys)
            {
                SrvList = startData.centerServerList[key];
                for ( int j = 0; j < SrvList.Count; j++)
                {
                    if ( SrvList[j].nState == 1 )
                    {
                        ItemView.ServerItem         = SrvList[j];
                        ItemView.CenterServerID     = key;
                        haveOkServer                = true;
                        break;

                    }
                }
                if ( haveOkServer )
                {
                    break;
                }
                else
                {
                    Debug.Log("haveNotServer!");
                    ItemView.ServerItem             = SrvList[SrvList.Count - 1];
                }
            }
            return;
        }
        Debug.Log("return!");
        int                     serverID            = (int)evt.data;
        List<GameSrvInfo>       SrvInfoList         = startData.centerServerList[serverID];

        for ( int i = 0; i < SrvInfoList.Count; i ++ )
        {
            GameObject          Item_2              = Instantiate(Obj_1) as GameObject;
            if ( i % 2 == 0)
            {                   serverGridLeft.AddChild ( Item_2.transform );   }     
            else
            {                   serverGridRight.AddChild( Item_2.transform );   }

            Item_2.transform.localEulerAngles       = Vector3.zero;
            Item_2.transform.localScale             = Vector3.one;
            Item_2.transform.localPosition          = Vector3.zero;

            ServerLineItemView  ItemView_1          = Item_2.GetComponent<ServerLineItemView>();
            SrvList                                 = startData.centerServerList[startData.centerServerList.Count];
            ItemView_1.ServerItem                   = SrvInfoList[i];
            ItemView_1.CenterServerID               = serverID;
        }
        serverGridLeft.repositionNow                = true;
        serverGridLeft.Reposition();
        serverGridRight.repositionNow               = true;
        serverGridRight.Reposition();
    }

}

