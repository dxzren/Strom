using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class MainUIPanelMediator : EventMediator
{
    [Inject]
    public MainUIPanelView      View                        { set; get; }
    [Inject]
    public IPlayer              InPlayer                    { set; get; }
    [Inject]
    public IGameData            InGameData                  { set; get; }

    public override void        OnRegister()
    {
        dispatcher.Dispatch(CheckPointEvent.REQ_CheckPointInfo_Event);                                              /// 关卡信息请求
        GetPlayerInfo();                                                                                            /// 获取玩家信息
        View.Init();                                                                                                /// 视图初始化

        View.dispatcher.AddListener(View.HeroSysClick_Event,            HeroSysClickHandler);                       /// 英雄系统点击 监听处理
        View.dispatcher.AddListener(View.BagSysClick_Event,             BagSysClickHandler);                        /// 背包系统点击 监听处理
        View.dispatcher.AddListener(View.CheckPointSysClick_Event,      CheckPointSysClickHandler);                 /// 关卡系统点击 监听处理
        View.dispatcher.AddListener(View.MallSysClick_Event,            MallSysClickHandler);                       /// 商城系统点击 监听处理

        dispatcher.AddListener(GlobalEvent.UIAnimationEvent,            MainUIAnimaStart);                          /// 主界面UI动画
        MainUIAnimaStart();                                                                                         /// 主界面UI动画播放
        PanelManager.sInstance.LoadOverHandler_10Planel(this.gameObject.name);                                      /// 加载完数据显示当前界面
        dispatcher.Dispatch(RechargeEvent.RechargeNone_Event);                                                      /// 无需等待返回，这是通知服务器即可
    }
    public override void        OnRemove()
    {
        View.dispatcher.RemoveListener(View.HeroSysClick_Event,         HeroSysClickHandler);                       /// 英雄系统点击 监听处理
        View.dispatcher.RemoveListener(View.BagSysClick_Event,          BagSysClickHandler);                        /// 背包系统点击 监听处理
        View.dispatcher.RemoveListener(View.CheckPointSysClick_Event,   CheckPointSysClickHandler);                 /// 关卡系统点击 监听处理
        View.dispatcher.RemoveListener(View.MallSysClick_Event,         MallSysClickHandler);                       /// 商城系统点击 监听处理

        dispatcher.RemoveListener(GlobalEvent.UIAnimationEvent,         MainUIAnimaStart);                          /// 主界面UI动画播放

    }

    public void                 GetPlayerInfo()                                                 // 获取玩家信息_显示主界面 
    {
        View.PlayerName.text            = InPlayer.PlayerName;                                                      /// 玩家名称
        View.PlayerLv.text              = InPlayer.PlayerLevel.ToString();                                          /// 玩家等级
        View.PlayerVipLv.text           = "VIP" + InPlayer.PlayerVIPLevel.ToString();                               /// 玩家VIP等级
        View.PlayerExpSlider.value      = InPlayer.PlayerCurrentExp / Configs_LeadingUpgrade.                       /// 玩家经验百分比 Slider
                                          sInstance.GetLeadingUpgradeDataByCurrentLevel(InPlayer.PlayerLevel).Consumption;
        View.PlayerExpNum.text          = ((int)(100 * InPlayer.PlayerCurrentExp / Configs_LeadingUpgrade.sInstance./// 玩家经验百分比 Label
                                           GetLeadingUpgradeDataByCurrentLevel(InPlayer.PlayerLevel).Consumption)).ToString() + "%";
        View.PlayerIcon.spriteName      = InPlayer.PlayerHeadIconName + "m";                                        /// 玩家头像名称
        View.PlayerIcon.MakePixelPerfect();                                                                         /// 像素优化
    }
    public void                 MainUIAnimaStart()                                              // 主界面UI动画播放        
    {
        View.Top.transform.localPosition        = new Vector3(  UIAnimationConfig.Top_fromX,
                                                                UIAnimationConfig.Top_fromY,
                                                                UIAnimationConfig.Top_fromZ     );
        View.Bottom.transform.localPosition     = new Vector3(  UIAnimationConfig.Bottom_fromX,
                                                                UIAnimationConfig.Bottom_fromY,
                                                                UIAnimationConfig.Bottom_fromZ  );
        View.Left.transform.localPosition       = new Vector3(  UIAnimationConfig.Left_fromX,
                                                                UIAnimationConfig.Left_fromY,
                                                                UIAnimationConfig.Left_fromZ    );
        View.Right.transform.localPosition      = new Vector3(  UIAnimationConfig.Right_fromX,
                                                                UIAnimationConfig.Right_fromY,
                                                                UIAnimationConfig.Right_fromZ   );
        Invoke ("PlayMainUiAnima", UIAnimationConfig.BlackToNomarl_duration);
    }
    public void                 PlayMainUiAnima()                                               // _主界面UI动画播放       
    {
        FlyInEffect.TopButtonFlyInEffect        ( View.Top );
        FlyInEffect.BottomButtonFlyInEffect     ( View.Bottom );
        FlyInEffect.LeftButtonFlyInEffect       ( View.Left );
        FlyInEffect.RightButtonFlyInEffect      ( View.Right );
    }
    public void                 PlayOutUIAnima()                                                // 屏幕飞出界面动画        
    {
        FlyInEffect.BottomButtonFlyOutEffect(View.Top);
        FlyInEffect.BottomButtonFlyOutEffect(View.Bottom);
        FlyInEffect.BottomButtonFlyOutEffect(View.Left);
        FlyInEffect.BottomButtonFlyOutEffect(View.Right);
    }

    public void                 HeroSysClickHandler()                                           // 英雄系统点击           
    {   PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.HeroListShowPanel);      }
    public void                 BagSysClickHandler()                                            // 背包系统点击           
    {   PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.ItemBagPanel);       }
    public void                 CheckPointSysClickHandler()                                     // 关卡系统点击           
    {
        Debuger.Log             ("CheckPointSysClickHandler()");
        dispatcher.Dispatch     ( CheckPointEvent.REQ_CheckPointInfo_Event, UIPanelConfig.WorldMapPanel );      /// 打开世界地图面板
        PanelManager.sInstance.HidePanel   (SceneType.Main, UIPanelConfig.MainUIPanel);         // 销毁主界面面板
    }
    public void                 MallSysClickHandler()                                           // 商城系统点击           
    {   PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.MainMallPanel);      }
}
