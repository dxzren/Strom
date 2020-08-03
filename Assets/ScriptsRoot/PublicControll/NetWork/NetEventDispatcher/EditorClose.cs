using UnityEngine;
using System;
using System.Text;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Detectors;
using strange.extensions.mediation.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

public class EditorClose : EventView
{
    [Inject]
    public      ISocket             mySocket                    { set; get; }
    [Inject]
    public      IGameData           gameData                    { set; get; }
    [Inject]
    public      IPlayer             player                      { set; get; }
    [Inject]
    public      IRechargeData       rechargeData                { set; get; }
    [Inject]
    public      IStartData          startData                   { set; get; }

    [Inject     (ContextKeys.CONTEXT_DISPATCHER)]
    public      IEventDispatcher    dispEvent                   { get; set;}

    //--------------------------------------------------------------------------------------------------------------
    private NetworkReachability     state;                                                      /// 网络检测状态 (0:无法访问 1:4G网络 2:WiFi网络)

    public static int               staminaRaply;                                               /// 体力回复
    public static bool              isReceHeartBeat             = true;                         /// 接收心跳

    public UIInput                  UserID, SessonID;                                           /// 测试,取得服务器验证所需数据
    public static System.DateTime   date;                                                       /// 上一次心跳时间

    void                            OnApplicationQuit()                                         // 应用程序退出       
    {
        Debug.Log                   ("Game Quit");
        mySocket.                   SocketThreadQuit();
        //UMeng.Instance.UMengLogout();
    }
    void                            OnEnable()                                                  // 启用               
    {
        Debug.Log                   ("EditorClose.OnEnable()");
        Time.                       timeScale                   = 1;
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        state = Application.internetReachability;
        if(state == NetworkReachability.NotReachable)
        {
            mySocket.SocketThreadQuit();
            PanelManager.sInstance.ShowDialogPanel(" 无网络,请检测网络", "确定");
        }
        Debuger.EnableLog = false;
#endif
                                    stamina                     = player.PlayerCurrentStamina;  /// 玩家体力
                                    InvokeRepeating             ("HeroSkillTimer", 2f, 1f);     /// 英雄技能点计时

        ObscuredCheatingDetector.   StartDetection(ResetLogIn);                                 /// 防作弊检测-启用指定回调
    }

    private int                     escapeNum                   = 0;                            /// 返回键次数 
    private int                     stamina                     = -1;                           /// 体力
    private int                     HeartBeat                   = 0;                            /// 心跳
    void                            ResetLogIn()                                                // 重新启动游戏            
    {
        Debug.Log("EditorClose.ResetLogIn()");
        //if(Define.SDKSwitch)
        //{   SDKManager.Instance.logOut();   }
        StartNetWorkCallback.isFirst = true;
        Util.ReLogIn();
    }
    void                            Update()                                                    // 更新                   
    {
        NetEventDispatcher.Instance().OnTick();                                                 // 倒计时
        if(Input.GetKeyDown(KeyCode.Escape) && !gameData.IsExitPanel)                       // 检测Android手机是否按下返回键
        {
            //if(Define.SDKSwitch)
            //{
            //    SDKManager.Instance.logOut();
            //}

            PanelManager.sInstance.ShowDialogPanel("你确定要退出游戏?", "确定", () =>
              {
                  Application.Quit();
              });
            escapeNum++;
            if(escapeNum > 3 && !Debuger.EnableLog)
            {
                Debuger.EnableLog = true;
            }        
        }
    }
    public void                     HeroSkillTimer()                                            // 体力恢复倒计时    
    {
        staminaRaply--;
        if  (   staminaRaply <= 0 )                                                                                     // 体力到计时
        {
            if( stamina != player.PlayerCurrentStamina)                                                                 // 
            {
                                    staminaRaply                = CustomJsonUtil.GetValueToInt("PhysicalRecoverTime");
                                    stamina                     = player.PlayerCurrentStamina;
            }
            if( stamina <= 0 )                                                                                          //
            {                       staminaRaply                = 0;   }                                                
            if( HeartBeat >= 14)                                                                                        //  
            {
                                    Timer();
                                    HeartBeat                   = 0;
            }
            else
            {                       HeartBeat++;    }
        }
    }
    public void Timer()                                                     // 心跳包计时               
    {
        if(mySocket.StartHeartBeat)
        {
            LOGIN_Heart HeartMsg = new LOGIN_Heart();
            HeartMsg.Head.size  = (short)Marshal.SizeOf(HeartMsg);
            HeartMsg.Head.type1 = (short)eMsgType._MSG_PLAYER_MODULE;
            HeartMsg.Head.type2 = (short)PLAYER_CMD.PLAYER_KEEP_LIVE;
            byte[] SendHeartMsg = Util.StructToBytes(HeartMsg);
            mySocket.SendRequest(SendHeartMsg);
            if(!isReceHeartBeat)        
            {
                //网络不稳定
                //NetEventDispatcher.Instance().DispatchEvent(31542013, 31542013, null);
            }
            isReceHeartBeat = false;
            date = System.DateTime.Now;

            Debuger.Log("Send HeartBeat = " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + ": " + System.DateTime.Now.Second);
        }
    }


}
