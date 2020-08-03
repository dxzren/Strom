using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   公告信息面板视图    </summary>

public class PublicInfoViewMediator : EventMediator
{
    [Inject]
    public PublicInfoView          View        { set; get; }
    [Inject]
    public IPlayer                 playeData   { set; get; }

    public override void    OnRegister()
    {
        View.Init();
        LoadPublicContent();                                                // 加载公告内容
    }
    public override void    OnRemove()  
    {

    }
    public void             LoadPublicContent()                             // 加载公告内容       
    {
        GameObject          PanelObj            = (GameObject)Util.Load(UIPanelConfig.PublicContant);                           /// 面板内容对象
        int                 index               = 0;
        for (   int i = Configs_Notice.sInstance.mNoticeDatas.Count;i > 0; i--)
        {
            GameObject Item                     = Instantiate(PanelObj) as GameObject;                                          /// 实例化对象
            Item.name                           = "PublicContent";                                                              /// 面板名称
            Item.transform.parent               = View.ScrollGrid.transform;                                                    /// 对象父级别
            if ( index > 0)
            {
                int refo                        = 0;
                for (int j = 0; j < View.ScrollGrid.transform.childCount - 1; j++)  
                {
                    refo -=     (   View.ScrollGrid.transform.GetChild(j).transform.GetChild(0).GetComponent<UISprite>().height + 
                                    View.ScrollGrid.transform.GetChild(1).GetComponent<UILabel>().height +
                                    View.ScrollGrid.transform.GetChild(j).transform.GetChild(2).GetComponent<UILabel>().height + 80 );
                }
                Item.transform.localPosition    = new Vector3 ( -126, refo, 0 );
            }
            else
            {   Item.transform.localPosition    = new Vector3 ( -126, 0, 0 );   }
            Item.transform.localEulerAngles     = Vector3.zero;
            Item.transform.localScale           = Vector3.one;

            PublicContentView ItemView          = Item.GetComponent<PublicContentView>();                                       /// 公告内容视图脚本
            ItemView.PublicTitle.text           = Language.GetValue( Configs_Notice.sInstance.mNoticeDatas[i].NoticeTitle);     /// 公告标题
            ItemView.PublicContent.text         = playeData.PublicInfo.Replace("//n","\n");                                     /// 公告内容
            ItemView.DownTitle.text             = Language.GetValue( Configs_Notice.sInstance.mNoticeDatas[i].NoticeSign);      /// 公告下标署名
            index += 1 ;
        }
        if (    View.ScrollGrid.transform.childCount < 0 )                                                                      /// 公告信息为空
        {
            View.ScrollGrid.transform.parent.parent.FindChild   ( "ViewEmptyInfoPanel").gameObject.SetActive(true);
            PanelManager.sInstance.ShowViewEmptyInfoPanel       ( new Vector3 (0f,50f,0f), "这个游戏好久没更新了" );
        }
        else
        {   View.ScrollGrid.transform.parent.FindChild          ( "ViewEmptyInfoPanel").gameObject.SetActive(false);    }
    }
}

