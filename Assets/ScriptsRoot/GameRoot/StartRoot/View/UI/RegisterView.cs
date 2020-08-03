using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RegisterView : EventView
{
    public UILabel          AccError, PwdError, ConfirmPwdError, Register;
    public UIInput          Acc, Pwd, ConfirmPwd, PhoneNum, Name, IDCod;
    public GameObject       EnterButton, BackButton;
    public UISprite         AccSuccess, AccFial, PwdSuccess, PwdFail, ConfirmSuccess, ConfirmFail;

    public string           EnterButtonEvent            = "EnterButtonEvent";
    public string           AccountInputEvent           = "AccountInputEvent";
    public string           PasswordInputEvnet          = "PasswordInputEvnet";
    public string           PasswordConfirmEvent        = "PasswordConfirmEvent";
    public string           BackButtonEvent             = "BackButtonEvent";

    public void Init()
    {
        UIEventListener.Get(EnterButton).onClick        = EnterOnClick;
        UIEventListener.Get(BackButton).onClick         = BackOnClick;
    }
    public void AccountInput()                      
    {   dispatcher.Dispatch(AccountInputEvent);  }

    public void PasswordInput()                     
    {   dispatcher.Dispatch(PasswordInputEvnet); }

    public void PasswordConfirm()                   
    {   dispatcher.Dispatch(PasswordConfirmEvent); }

    public void EnterOnClick(GameObject obj)      
    {   dispatcher.Dispatch(EnterButtonEvent);    }

    public void BackOnClick(GameObject obj)
    {
        Debug.Log("BackOnClick!");
        PanelManager.sInstance.HidePanel(SceneType.Start, UIPanelConfig.RegisterPanel);
    }
    public void changeAccountType()                 
    {
        if (this.Acc.value              == "请输入6~20位的字母和数字")
        {
            this.Acc.value              =   "";
        }
    }
    public void changePasswordType()                
    {
        if (this.Pwd.value              == "请输入6~20位的字母和数字")
        {
            this.Pwd.value              =   "";
            this.Pwd.inputType          =   UIInput.InputType.Password;
        }
    }
    public void ConfrimPasswordType()               
    {
        if (this.ConfirmPwd.value       == "请输入6~20位的字母和数字")
        {
            this.ConfirmPwd.value       =   "";
            this.ConfirmPwd.inputType   =   UIInput.InputType.Password;
        }
    }
}
