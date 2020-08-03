using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class ReLogInView : EventView
{
    public GameObject       RegisterButton, LogInButton, BackButton;                            // 注册按钮,登录按钮,返回按钮
    public UILabel          AccountError, PasswordError;                                        // 账号错误,密码错误
    public UIInput          Account, Password;                                                  // 账号,密码

    public string           RegisterClick_Event             = "RegisterClick_Event";            // 注册点击
    public string           LogInClick_Event                = "LogInClick_Event";               // 登录点击
    public string           CheckAccountInput_Event         = "CheckAccountInput_Event";        // 校验账号输入
    public string           CheckPasswordInput_Event        = "CheckPasswordInput_Event";       // 校验密码输入

    public void Init()
    {
        UIEventListener.Get(RegisterButton).onClick         = RegisterButtonHandler;            // 注册 注册点击 
        UIEventListener.Get(LogInButton).onClick            = LogInButtonHandler;               // 注册 登录点击 
        UIEventListener.Get(BackButton).onClick             = BackButtonHandler;                // 注册 返回点击 
    }

    public void RegisterButtonHandler( GameObject obj)      // 注册点击       
    {
        dispatcher.Dispatch ( RegisterClick_Event );
    }
    public void LogInButtonHandler( GameObject obj)         // 登录点击       
    {
        dispatcher.Dispatch ( LogInClick_Event );
    }
    public void BackButtonHandler(GameObject obj)           // 返回点击       
    {
        PanelManager.sInstance.HidePanel(SceneType.Start, UIPanelConfig.ReLogInPanel);
    }

    public void CheckAccountInputHandler()                  // 严重账号输入   
    {
        dispatcher.Dispatch(CheckAccountInput_Event);
    }
    public void CheckPasswordInputHandler()                 // 验证密码输入   
    {
        dispatcher.Dispatch(CheckPasswordInput_Event);
    }
}