using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RegisterViewMediator : EventMediator
{
    [Inject]
    public RegisterView     regView                         { set; get; }
    [Inject]
    public IStartData       startData                       { set; get; }

    private bool            AccInputSuccess                 = false;
    private bool            PwdInputSuccess                 = false;
    private bool            PwdConfirmInputSuccess          = false;

    public override void    OnRegister()
    {
        if(startData.isBind)
        {
            regView.Register.text = "绑 定 帐 号";
        }
        regView.Init();
        regView.dispatcher.AddListener(regView.EnterButtonEvent,            EnterButtonHandler);                    /// 监听 确认按钮 处理
        regView.dispatcher.AddListener(regView.AccountInputEvent,           AccountInputHandler);                   /// 监听 帐号输入 处理
        regView.dispatcher.AddListener(regView.PasswordInputEvnet,          PasswordInputHandler);                  /// 监听 密码输入 处理
        regView.dispatcher.AddListener(regView.PasswordConfirmEvent,        PasswordConfirmHandler);                /// 监听 密码确认 处理

        PanelManager.sInstance.LoadOverHandler_10Planel(this.gameObject.name);                                      /// 加载完数据,加载当前界面
    }

    public override void    OnRemove()
    {
        regView.dispatcher.RemoveListener(regView.EnterButtonEvent,         EnterButtonHandler);                    /// 移除 确认按钮 处理
        regView.dispatcher.RemoveListener(regView.AccountInputEvent,        AccountInputHandler);                   /// 移除 帐号输入 处理
        regView.dispatcher.RemoveListener(regView.PasswordInputEvnet,       PasswordInputHandler);                  /// 移除 密码输入 处理
        regView.dispatcher.RemoveListener(regView.PasswordConfirmEvent,     PasswordConfirmHandler);                /// 移除 密码确认 处理
    }

    public void             EnterButtonHandler()                                                                    // 确认按钮处理             
    {
        if (regView.PhoneNum.value  == "" || regView.PhoneNum.value == "请输入电话号码")        
        {
            regView.PhoneNum.value = "请输入电话号码";
            return;
        }
        if (regView.Name.value      == "" || regView.Name.value     == "请输入真实姓名")        
        {
            regView.Name.value = "请输入真实姓名";
            return;
        }
        if (regView.IDCod.value     == "" || regView.IDCod.value    == "请输入身份证号码")      
        {
            regView.IDCod.value = "请输入身份证号码";
            return;
        }

        AccountInputHandler();
        PasswordInputHandler();
        PasswordConfirmHandler();

        if (regView.Acc.value               == "请输入6~20位的字母和数字"&&
            regView.Pwd.value               == "请输入6~20位的字母和数字"&&
            regView.ConfirmPwd.value        == "请输入6~20位的字母和数字")
        {
            regView.AccError.text           = "请输入帐号";
            regView.AccError.color          = Color.red;
            regView.PwdError.text           = "请输入密码";
            regView.PwdError.color          = Color.red;
            regView.ConfirmPwdError.text    = "请输入密码";
            regView.ConfirmPwdError.color   = Color.red;
        }
        else if (AccInputSuccess && PwdInputSuccess && PwdConfirmInputSuccess)
        {
            startData.account   = regView.Acc.value;
            startData.password  = regView.Pwd.value;

            if (startData.isBind)
            {
                dispatcher.Dispatch(StartEvent.AccBind_Event);                                  // 派发 用户绑定   _AccBind_Command
                return;
            }
            dispatcher.Dispatch(StartEvent.Register_Event);                                     // 派发 注册       _Register_Command
        }

    }
    public void             AccountInputHandler()                                                                   // 账号输入验证处理         
    {
        if ( regView.Acc.value == "" )                      
        {
             regView.AccError.text                  = "请输入6~20位的字母和数字";
             return;
        }
        if ( regView.Acc.value.Length < 6 &&
             regView.Acc.value.Length > 20  )               
        {
             regView.AccError.text                  = "请输入6~20位的字母和数字";
             regView.AccError.color                 = Color.red;
             return;
        }
        if ( regView.Acc.value != "请输入6~20位的字母和数字")
        {
            if ( AcountInputRight(regView.Acc.value ))
            {
                regView.AccSuccess.alpha            = 1;
                regView.AccSuccess.spriteName       = "duihao";
                regView.AccError.text               = " ";
                AccInputSuccess                     = true;
            }
            else
            {
                regView.AccSuccess.spriteName       = "cha";
                regView.AccSuccess.alpha            = 1;
                AccInputSuccess                     = false;
            }
        }
    }
    public void             PasswordInputHandler()                                                                  // 密码输入验证处理         
    {
        if (    regView.Pwd.value == "")
        {
                regView.Pwd.inputType               = UIInput.InputType.Standard;
                regView.Pwd.value                   = "请输入6~20位的字母和数字";
        }  
        if (    regView.Pwd.value.Length < 6 &&
                regView.Pwd.value.Length > 20  )
        {
                regView.PwdError.text                = "请输入6~20位的字母和数字";
                regView.PwdError.color               = Color.red;
                return;
        }
        if (    regView.Pwd.value != "请输入6~20位的字母和数字")
        {
            if (AcountInputRight(regView.Pwd.value))
            {
                regView.PwdSuccess.alpha            = 1;
                regView.PwdSuccess.spriteName       = "duihao";
                regView.PwdError.text               = "";
                PwdInputSuccess                     = true;
                return;
            }
            else
            {
                regView.PwdSuccess.alpha            = 1;
                regView.PwdSuccess.spriteName       = "cha";
                PwdInputSuccess                     = false;
            }
        }
    }
    public void             PasswordConfirmHandler()                                                                // 密码确认验证处理         
    {
        if (    regView.ConfirmPwd.value == "")
        {       return; }

        if (    regView.ConfirmPwd.value != "请输入6~20位的字母和数字")
        {
            if (regView.Pwd.value == regView.ConfirmPwd.value)
            {
                regView.ConfirmSuccess.alpha        = 1;
                regView.ConfirmSuccess.spriteName   = "duihao";
                regView.ConfirmPwdError.text        = " ";
                PwdConfirmInputSuccess = true;
                return;
            }
            else
            {
                regView.ConfirmSuccess.alpha         = 1;
                regView.ConfirmSuccess.spriteName    = "cha";
                regView.ConfirmPwdError.text         = "输入的两次密码不正确!";
                regView.ConfirmPwdError.color        = Color.red;
                PwdConfirmInputSuccess               = false;
                return;
            }
        }

    }
    public bool             AcountInputRight(string inputAcc)                                                       // 帐号包含字母和数字       
    {
        return true;
        System.Text.RegularExpressions.Regex rex = 
            new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]+$");                        // 检测包含字母和数字
        if (rex.IsMatch(inputAcc))
        {
            rex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]+$");                     // 检测包含纯字母
            if (rex.IsMatch(inputAcc))
            {
                regView.AccError.text = "帐号必须为字母和数字组成!";
                regView.AccError.color = Color.red;
                return false;
            }
            rex = new System.Text.RegularExpressions.Regex(@"^[0-9]+$");                        // 检测包含纯数字
            if (rex.IsMatch(inputAcc))
            {
                regView.AccError.text = "帐号必须为字母和数字组成!";
                regView.AccError.color = Color.red;
                return false;
            }
            return true;
        }
        regView.AccError.text = "帐号必须为字母和数字组成!";
        regView.AccError.color = Color.red;
        return false;
    }


}