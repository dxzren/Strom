using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   进入游戏    </summary>
public class GameEnterViewMediator : EventMediator
{
    [Inject]
    public GameEnterView    View            { set; get; }
    [Inject]
    public IStartData       startData       { set; get; }
    [Inject]
    public IPlayer          playerData      { set; get; }

    public override void    OnRegister()       
    {
        View.Init();

        ShowWelcome();                                                                                          /// ShowWelcome信息
        ShowServerName();                                                                                       /// 显示服务器名称
        View.Version.text               = "版本号:" + Application.version;                                       /// 版本号  

        Debug.Log                       ("获取服务器列表!");
        dispatcher.Dispatch             (StartEvent. GetServerList_Event);                                      /// 获取服务器列表 
        Debug.Log                       ("获取公告!");
        dispatcher.Dispatch             (StartEvent. GetPublicInfo_Event);                                      /// 获取公告

        dispatcher.AddListener          (StartEvent.RefreshServerName_Event,    ShowServerName);                /// 监听 服务器名称更新
        dispatcher.AddListener          (StartEvent.RefreshPublic_Event,        RefreshPublic);                 /// 监听 公告更新

        View.dispatcher.AddListener     (View.BindAccountClick_Event,           BindAccountClickHandler);       /// 监听 绑定账号点击
        View.dispatcher.AddListener     (View.SwitchAccountClick_Event,         SwitchAccountClickHandler);     /// 监听 切换用户点击
        View.dispatcher.AddListener     (View.ChangeServerClick_Event,          ChangeServerClickHandler);      /// 监听 更换服务器点击
        View.dispatcher.AddListener     (View.EnterGameClick_Event,             EnterGameClickHandler);         /// 监听 进入游戏点击

        dispatcher.AddListener          (EventSignal.UpdateInfo_Event,          ShowServerName);                /// 监听 ShowWelcome信息
        PanelManager.sInstance.LoadOverHandler_10Planel(this.gameObject.name);
    }       
    public override void OnRemove()         
    {
        View.dispatcher.RemoveListener  (View.BindAccountClick_Event,           BindAccountClickHandler);       /// 移除 绑定账号点击
        View.dispatcher.RemoveListener  (View.SwitchAccountClick_Event,         SwitchAccountClickHandler);     /// 移除 切换用户点击
        View.dispatcher.RemoveListener  (View.ChangeServerClick_Event,          ChangeServerClickHandler);      /// 移除 更换服务器点击
        View.dispatcher.RemoveListener  (View.EnterGameClick_Event,             EnterGameClickHandler);         /// 移除 进入游戏点击

        dispatcher.RemoveListener       (StartEvent.RefreshServerName_Event,    ShowServerName);                /// 移除 服务器名称
        dispatcher.RemoveListener       (StartEvent.RefreshPublic_Event,        RefreshPublic);                 /// 移除 公告更新
        dispatcher.RemoveListener       (EventSignal.UpdateInfo_Event,          ShowWelcome);                   /// 移除 ShowWelcome信息
    }


    public void ShowWelcome()                                           // ShowWelcome信息                          
    {
        SavedAccountInfos SaveAcc = Util.GetAccFile("SavedAccountInfos");
        if (SaveAcc.Tempporary != null)
        {
            View.Welcome.text           = "亲爱的" + "[FFDB16]" + startData.account + "[-]" + ",欢迎您回来!";
            View.BindAccountLable.gameObject.SetActive(false);
        }
        else
        {
            View.Welcome.text           = "亲爱的" + "[FFDB16]" + startData.account + "[-]" + ",欢迎您回来!";
            View.BindAccountLable.text  = "绑定游戏账号";
            View.BindAccountLable.color = Color.green;
        }
    }
    public void ShowServerName()                                        // 显示服务器名称                            
    {
        Debug.Log("ShowServerName!");
        if (Util.GetIPFile("hasSelected") != null)
        {
            Debug.Log("GetIPFile");
            ServerInfo ServerData       = Util.GetIPFile("hasSelected");
            View.ServerName.text        = ServerData.serverName;
            startData.centerServerID    = ServerData.centerServerID;
            startData.gameServerID      = ServerData.gameServerID;
            startData.gameServerName    = ServerData.serverName;
        }
        else
        {
            View.ServerName.text        = startData.gameServerSelected.szGameSrvName;
        }
        if (PlayerPrefs.HasKey("VisitorName"))

        {
            Debug.Log("VisitorName");
            View.SwitchAccount.gameObject.SetActive(false);
        }
    }
    public void ShowPublic()                                            // 显示公告                                 
    {
        Debug.Log("ShowPublic!");
        PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.PublicPanel);
    }
    public void RefreshPublic()                                         // 更新公告                                 
    {
        Debug.Log("RefreshPublic!");
        if (playerData.PublicInfo.Length != 0)           
        {
            Debug.Log("Invoke Public!");
            Invoke("ShowPublic", UIAnimationConfig.BlackToNomarl_duration + 0.1f);
        }
    }
    public void BindAccountClickHandler()                               // 绑定账号点击:      展示注册界面            
    {
        startData.isBind = true;
        PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.RegisterPanel);
    }
    public void SwitchAccountClickHandler()                             // 切换账号点击:      展示重新登录界面        
    {
        PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.ReLogInPanel);
    }
    public void ChangeServerClickHandler()                              // 更换服务器点击:    展示服务器选择界面       
    {
        PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.ServerSelectPanel);
    }
    public void EnterGameClickHandler()                                 // 进入游戏点击:                             
    {
        dispatcher.Dispatch(StartEvent.LogIn_Event);
        View.EnterGame.GetComponent<BoxCollider>().enabled = false;
        Invoke("GetFocus", 2.0f);
    }

    private void GetFocus()         
    {
        View.EnterGame.GetComponent<BoxCollider>().enabled = false;
    }
}