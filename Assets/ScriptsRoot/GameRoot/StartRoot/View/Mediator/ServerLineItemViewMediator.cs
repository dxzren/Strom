using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
/// <summary>   游戏服分线   </summary>

public class ServerLineItemViewMediator : EventMediator
{
    [Inject]
    public ServerLineItemView       View        { set; get; }
    [Inject]
    public IStartData               startData   { set; get; }

    public override void OnRegister()
    {
        View.Init();
        View.dispatcher.AddListener(View.ItemEvent, ItemHandler);                                           /// 游戏分线Line点击
    }
    public override void OnRemove()
    {
        View.dispatcher.RemoveListener(View.ItemEvent, ItemHandler);
    }

    public void ItemHandler ()
    {
        startData.gameServerSelected            = View.ServerItem;                                          /// 已经选择服务器信息 <ServerInfo>
        startData.gameServerID                  = View.ServerItem.nGameServerLineID;                        /// 游戏服 ID
        PanelManager.sInstance.HidePanel        (SceneType.Start, UIPanelConfig.ServerSelectPanel);         /// 隐藏服务器选择面板
        PanelManager.sInstance.HideLoadingPanel();                                                          /// 隐藏加载界面

        ServerInfo HasServerName                = Util.GetIPFile("hasSelected");                            /// 读取本地服务器信息文件
        HasServerName.serverName                = View.ServerName.text;
        HasServerName.gameServerID              = View.ServerItem.nGameServerLineID;
        HasServerName.centerServerID            = View.CenterServerID;
        HasServerName.IP                        = View.ServerItem.szGameSrvIP;
        HasServerName.port                      = View.ServerItem.nProt;

        Util.SaveFile                           (HasServerName, "hasSelected");                             /// 保存已选服务器信息文件<,IP,port,gameName>
        dispatcher.Dispatch                     (StartEvent.RefreshServerName_Event);                       /// 更新 游戏服分线选项
        //YiJieManager.Instance.SetData("levelup", player, game);

    }
}