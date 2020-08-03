using UnityEngine;
using System.Collections;
using System;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

public class LogInOrRegisgerViewMediator : EventMediator
{
    [Inject]
    public LogInOrRegisterView      LogInOrRegView          { set; get; }
    [Inject]
    public IStartData               startData               { set; get; }
    [Inject]
    public IGameData                gameData                { set; get; }

    private bool                    AccCheckSuccess         = false;
    private bool                    PwdCheckSuccess         = false;

    public override void            OnRegister()   
    {
        AtlasInit();
        LogInOrRegView.Init();
        LogInOrRegView.dispatcher.AddListener(LogInOrRegView.CheckAccountInputsEvent,       CheckAccInputHandler);  // 校验帐号输入
        LogInOrRegView.dispatcher.AddListener(LogInOrRegView.CheckPasswordInputsEvent,      CheckPwdInputHandler);  // 校验密码输入
        LogInOrRegView.dispatcher.AddListener(LogInOrRegView.FastRegisterButtonEvent,       FastRegButtonHandler);  // 快速注册按钮
        LogInOrRegView.dispatcher.AddListener(LogInOrRegView.RegisterButtonEvent,           RegButtonHandler);      // 注册按钮
        LogInOrRegView.dispatcher.AddListener(LogInOrRegView.LogInButtonEvent,              LogInButtonHandler);    // 登录按钮   

        dispatcher.AddListener      (StartEvent.RefreshAcct_Event, LogInOrRegView.Init);                            // 帐号更新后初始化界面
        dispatcher.Dispatch         (GlobalEvent.StartGlobalListenerEvent);                                         // 全局监听

        PanelManager.sInstance.LoadOverHandler_10Planel(this.gameObject.name);                                      // 加载完数据 展示10级界面展示动画
        Util.BackToNomarl(this.gameObject);                                                                         // 渐隐动画
    }

    public override void            OnRemove()     
    {
        LogInOrRegView.dispatcher.RemoveListener(LogInOrRegView.CheckAccountInputsEvent,    CheckAccInputHandler);  // 校验帐号输入
        LogInOrRegView.dispatcher.RemoveListener(LogInOrRegView.CheckPasswordInputsEvent,   CheckPwdInputHandler);  // 校验密码输入
        LogInOrRegView.dispatcher.RemoveListener(LogInOrRegView.FastRegisterButtonEvent,    FastRegButtonHandler);  // 快速注册按钮
        LogInOrRegView.dispatcher.RemoveListener(LogInOrRegView.RegisterButtonEvent,        RegButtonHandler);      // 注册按钮
        LogInOrRegView.dispatcher.RemoveListener(LogInOrRegView.LogInButtonEvent,           LogInButtonHandler);    // 登录按钮   
    }
    public void                     CheckAccInputHandler ()                                                         // 验证帐号输入处理       
    {
        if ( LogInOrRegView.Account.value != "请输入帐号")
        {
            if ( LogInOrRegView.Account.value == "")                                            /// 输入为空             
            {
                LogInOrRegView.AccountError.text    = "请输入帐号!";
                LogInOrRegView.AccountError.color   = Color.red;
                return;
            }                                         

            if (    AcountInputLimit        (LogInOrRegView.Account.value) &&                   /// 输入合理长度 (6-20)
                    AcountInputNotNumAll    (LogInOrRegView.Account.value) &&                   /// 输入非全数字
                    AcountInputNotLetterAll (LogInOrRegView.Account.value) &&                   /// 输入非全字母
                    AccoutInputRight        (LogInOrRegView.Account.value) &&                   /// 输入只含字母和数字
                    AcountInputNotOccupied  (LogInOrRegView.Account.value)                      /// 输入未被占用
                )
            {
                AccCheckSuccess = true;
                LogInOrRegView.AccountError.text = "";
            }
            else
            {
                AccCheckSuccess = false;
            }
        }
    }
    public void                     CheckPwdInputHandler ()                                                         // 验证密码输入处理       
    {
        if ( LogInOrRegView.Password.value != "请输入密码")
        {
            if ( LogInOrRegView.Password.value == "")                                           // 输入为空              
            {
                LogInOrRegView.PasswordError.text = "请输入密码!";
                LogInOrRegView.PasswordError.color = Color.red;
                return;
            }
            else if ( AcountInputLimit(LogInOrRegView.Account.value))                           // 输入合理长度 (6-20)   
            {
                PwdCheckSuccess = true;
                LogInOrRegView.PasswordError.text = "";
            }
            else
            {
                AccCheckSuccess = false;
            }
        }
    }
    public void                     FastRegButtonHandler ()                                                         // 快速注册按钮处理       
    {
        if (!string.IsNullOrEmpty(LogInOrRegView.TestIP.value))                                 /// TestIP  : 不为空
        { startData.testIP = LogInOrRegView.TestIP.value; }
        if (!string.IsNullOrEmpty(LogInOrRegView.TestPort.value))                               /// TestPort: 不为空       
        {
            int result = 0;
            if (int.TryParse(LogInOrRegView.TestPort.value, out result) == true)                /// TestPort: 为数字 
            {
                startData.testPort = int.Parse(LogInOrRegView.TestPort.value);
            }
            else
            {
                startData.testIP = "";
                startData.testPort = 0;
            }
        }

        dispatcher.Dispatch(StartEvent.FastRegister_Event);                                     /// 分发快速注册事件
        LogInOrRegView.gameObject.SetActive(false);                                             /// 隐藏当前界面
    }
    public void                     RegButtonHandler ()                                                             // 注册按钮处理           
    {
        if (!string.IsNullOrEmpty(LogInOrRegView.TestIP.value))                                 /// TestIP  : 不为空
        { startData.testIP = LogInOrRegView.TestIP.value; }
        if (!string.IsNullOrEmpty(LogInOrRegView.TestPort.value))                               /// TestPort: 不为空       
        {
            int result              = 0;
            if (int.TryParse(LogInOrRegView.TestPort.value, out result) == true)                /// TestPort: 为数字 
            {
                startData.testPort  = int.Parse(LogInOrRegView.TestPort.value);
            }
            else
            {
                startData.testIP    = "";
                startData.testPort  = 0;
            }
        }

        PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.RegisterPanel);         /// 显示注册界面
    }
    public void                     LogInButtonHandler()                                                            // 登录按钮处理           
    {
        if (!string.IsNullOrEmpty(LogInOrRegView.TestIP.value))                                 /// TestIP  : 不为空
        {   startData.testIP = LogInOrRegView.TestIP.value;     }
        if (!string.IsNullOrEmpty(LogInOrRegView.TestPort.value))                               /// TestPort: 不为空       
        {
            int result = 0;
            if (int.TryParse(LogInOrRegView.TestPort.value, out result) == true)                /// TestPort: 为数字 
            {
                startData.testPort  = int.Parse(LogInOrRegView.TestPort.value);
            }
            else
            {
                startData.testIP    = "";
                startData.testPort  = 0;
            }
        }   

        if (!Define.SDKSwitch)                                                                  /// 非渠道SDK              
        {
            CheckAccInputHandler();                                                             /// 验证帐号输入处理
            CheckPwdInputHandler();                                                             /// 验证密码输入处理

            if (LogInOrRegView.Password.value == "请输入密码" && LogInOrRegView.Account.value == "请输入帐号")              
            {
                LogInOrRegView.AccountError.text    = "请输入密码";
                LogInOrRegView.AccountError.color   = Color.red;
                LogInOrRegView.PasswordError.text   = "请输入帐号";
                LogInOrRegView.PasswordError.color  = Color.red;
            }

            else if ( AccCheckSuccess && PwdCheckSuccess )                                      /// 验证帐号密码正确         
            {
                startData.account   = LogInOrRegView.Account.value;                             /// 保存帐号
                startData.password  = LogInOrRegView.Password.value;                            /// 保存登录密码

                PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.GameEnterPanel);/// 显示游戏进入界面
                LogInOrRegView.gameObject.SetActive(false);                                     /// 关闭登录OR注册界面
            }
            else    
            {
                PanelManager.sInstance.ShowLoadingPanel();
                //SDKManager.Instance.LogIn();
            }
        }
    }

    private bool                    AcountInputLimit (string inputAcc)                                              // 帐号输入合理长度(6-20) 
    {
        if (inputAcc.Length < 0 || inputAcc.Length > 20)
        {
            LogInOrRegView.AccountError.text    = "账号长度应在6~20位之间！";
            LogInOrRegView.AccountError.color   = Color.red;
            return false;
        }
        return true;
    }
    private bool                    AcountInputNotNumAll (string inputAcc)                                          // 帐号输入非全数字       
    {
        return true;
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");
        if (!rex.IsMatch(inputAcc))
        {
            return true;
        }
        else
        {
            LogInOrRegView.AccountError.text    = "帐号不能为全数字!";
            LogInOrRegView.AccountError.color   = Color.red;
            return false;
        }
    }
    private bool                    AcountInputNotLetterAll (string inputAcc)                                       // 帐号输入非全字母       
    {
        return true;
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]+$");
        if (!rex.IsMatch(inputAcc))
        {
            return true;
        }
        LogInOrRegView.AccountError.text    = "帐号不能为全字母!";
        LogInOrRegView.AccountError.color   = Color.red;
        return false;
    }
    private bool                    AccoutInputRight (string inputAcc)                                              // 帐号只含字母和数字     
    {
        return true;
        System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9]+$");
        if(rex.IsMatch(inputAcc))
        {
            return true;
        }
        LogInOrRegView.AccountError.text    = "帐号必须为字母和数字组成!";
        LogInOrRegView.AccountError.color   = Color.red;
        return false;
    }
    private bool                    AcountInputNotOccupied (string inputAcc)                                        // 输入未被占用           
    {
        return true;
    }
    private void                    AtlasInit()                                                                     // 再一次图集初始化，注销登陆使用  
    {
        Debug.Log("初始化图集....");
        gameData.AtlasInit(AtlasConfig.GlobalAtlas);                        /// 全局图集

        gameData.AtlasInit(AtlasConfig.HeroSysAtlas);                       /// 英雄系统
        gameData.AtlasInit(AtlasConfig.HeroSysAtlasGray);                   
        gameData.AtlasInit(AtlasConfig.HeroHeadIcon);                       /// 英雄头像
        gameData.AtlasInit(AtlasConfig.HeroHeadGray);
        gameData.AtlasInit(AtlasConfig.HeroBody);                           /// 英雄本身像
        gameData.AtlasInit(AtlasConfig.HeroBodyGray);          

        gameData.AtlasInit(AtlasConfig.EquipIcon84);                        /// 装备图标
        gameData.AtlasInit(AtlasConfig.EquipIcon84Gray);
        gameData.AtlasInit(AtlasConfig.PropIcon70);                         /// 道具图标
        gameData.AtlasInit(AtlasConfig.PropIcon70Gray);
        gameData.AtlasInit(AtlasConfig.FragmentIcon);                       /// 碎片图标

        gameData.AtlasInit(AtlasConfig.CheckPoint);                         /// 关卡图集
        gameData.AtlasInit(AtlasConfig.CheckPointGray);                     
    }
}
