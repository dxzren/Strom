using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using LitJson;

public class LogInOrRegisterView : EventView
{
    public string                   CheckAccountInputsEvent                 = "CheckAccountInputsEvent";            // 校验账号输入事件
    public string                   CheckPasswordInputsEvent                = "CheckPasswordInputsEvent";           // 校验密码输入事件
    public string                   FastRegisterButtonEvent                 = "FastRegisterButtonEvent";            // 快速注册事件
    public string                   LogInButtonEvent                        = "LogInButtonEvent";                   // 登录事件
    public string                   RegisterButtonEvent                     = "RegisterButtonEvent";                // 注册按钮事件

    public UILabel                  AccountError, PasswordError;                                                    // 账号错误,密码错误
    public UIInput                  Account, Password;                                                              // 帐号,密码
    public UIInput                  TestIP, TestPort;                                                               // 测试IP,测试端口
    public GameObject               RegisterButton, LogInButton,                                                    // 注册按钮,登录按钮
                                    FastRegisterButton, Inputs;                                                     // 快速注册按钮,输入

    SavedAccountInfos               SAInfo                                  = new SavedAccountInfos();              // 保存账户信息

    public void Init()
    {
        if (Define.SDKSwitch)                                                                   // 关闭 帐号输入界面        
        {
            Inputs.SetActive(false);                                                            
        }
        TestIP.gameObject.SetActive(false);                                                     // 关闭 Test输入IP
        TestPort.gameObject.SetActive(false);                                                   // 关闭 Test输入端口

        GameObject GO = GameObject.FindWithTag("Respawn");                                      // 测试,取得服务器验证所需要数据
        if (GO != null)
        {
            EditorClose EClose = GO.GetComponent<EditorClose>();
            EClose.UserID = this.Account;
            EClose.SessonID = this.Password;
        }
        SAInfo = Util.GetAccFile("SavedAccountInfos");                                          // 获取帐号文件信息

        UIEventListener.Get(RegisterButton).onClick     = RegisterButtonClick;                  // 注册按钮  监听
        UIEventListener.Get(LogInButton).onClick        = LogInButtonClick;                     // 登录按钮  监听
        UIEventListener.Get(FastRegisterButton).onClick = FastRegisterButtonClick;              // 快速注册  监听

        if (SAInfo.Account != null && SAInfo.Password != null)                                  /// 自动填充本地保存帐号信息    
        {
            this.Account.value      = SAInfo.Account;
            this.Password.value     = SAInfo.Password;
            this.Password.inputType = UIInput.InputType.Password;
        }
        else
        {
            this.Password.value     = "请输入密码";
            this.Account.value      = "请输入帐号";
        }
 
    }

    public void CheckAccountInputs()                                        // 校验账号输入      
    {
        dispatcher.Dispatch(CheckAccountInputsEvent);
    }
    public void CheckPasswordInputs()                                       // 校验密码输入      
    {
        dispatcher.Dispatch(CheckAccountInputsEvent);
    }
    public void RegisterButtonClick( GameObject obj )                         // 注册按钮点击      
    {
        dispatcher.Dispatch(RegisterButtonEvent);
    }
    public void LogInButtonClick( GameObject obj )                            // 登录按钮点击      
    {

        dispatcher.Dispatch(LogInButtonEvent);
    }
    public void FastRegisterButtonClick( GameObject obj)                     // 快速注册点击      
    {

        dispatcher.Dispatch(FastRegisterButtonEvent);
    }

    public void ChangePasswordType()                                        // 更改密码类型      
    {
        if (this.Password.value == "请输入密码" || this.Password.value == SAInfo.Account)
        {
            this.Password.value = "";
            this.Password.inputType = UIInput.InputType.Password;
        }
    }

    public void ChangeAccountType()                                         // 更改账户类型      
    {
        if(this.Account.value == "请输入帐号" || this.Account.value == SAInfo.Password)
        {
            this.Account.value = "";
        }
    }
} 