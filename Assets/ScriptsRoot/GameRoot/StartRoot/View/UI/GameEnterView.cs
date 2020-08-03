using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
public class GameEnterView : EventView
{
    public string           BindAccountClick_Event              = "BindAccountClick_Event";             // 绑定账号点击    事件
    public string           SwitchAccountClick_Event            = "SwitchAccountClick_Event";           // 切换账号点击    事件
    public string           ChangeServerClick_Event             = "ChangeServerClick_Event";            // 更换服务器点击  事件
    public string           EnterGameClick_Event                = "EnterGameClick_Event";               // 进入游戏点击    事件

    public GameObject       BindAccount, SwitchAccount, ChangeServer, EnterGame;                        // 绑定账号, 切换账号,  更换服务器, 进入游戏
    public UILabel          Welcome, ServerName, Version, BindAccountLable;                             // Welcom,  服务器名称, 版本号,    绑定账号标题

    public void Init()
    {
        UIEventListener.Get(BindAccount).onClick                = BindAccountClickHandler;              // 注册 绑定账号点击
        UIEventListener.Get(SwitchAccount).onClick              = SwitchAccountClickHandler;            // 注册 切换账号点击
        UIEventListener.Get(ChangeServer).onClick               = ChangeServerClickHandler;             // 注册 更换服务器点击
        UIEventListener.Get(EnterGame).onClick                  = EnterGameClickHandler;                // 注册 进入游戏点击
    }
    
    public void BindAccountClickHandler     (GameObject obj)                                            // 绑定账号点击   处理      
    {
        dispatcher.Dispatch(BindAccountClick_Event);
    }
    public void SwitchAccountClickHandler   (GameObject obj)                                            // 切换账号点击   处理      
    {
        dispatcher.Dispatch(SwitchAccountClick_Event);
    }
    public void ChangeServerClickHandler    (GameObject obj)                                            // 更换服务器点击 处理      
    {
        dispatcher.Dispatch(ChangeServerClick_Event);
    }
    public void EnterGameClickHandler       (GameObject obj)                                            // 进入游戏点击   处理      
    {
        dispatcher.Dispatch(EnterGameClick_Event);
    }
}