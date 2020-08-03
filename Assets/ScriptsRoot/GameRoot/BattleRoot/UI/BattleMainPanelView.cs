using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.mediation.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  战斗主界面View </summary>
    public class BattleMainPanelView : EventView
    {
        public float                SpeedParaX2                     = BattleParmConfig.SpeedPara_X2;                        /// 2倍战斗速度
        public float                SpeedParaX3                     = BattleParmConfig.SpeedPara_X3;                        /// 3倍战斗速度

        public UIGrid               HeroList_Grid;                                                                          /// 英雄列表

        public UILabel              CurrProgress_Label;                                                                     /// 当前进度
        public UILabel              MaxProgress_Label;                                                                      /// 进度满
                                             
        public GameObject           PauseBtn_Obj;                                                                           /// 暂停
        public GameObject           TopUI,BottomUI;                                                                         /// 上,下 UI组 Group
        public GameObject           EnemyUIList_Obj;                                                                        /// 敌方成员列表 
        public GameObject           TalentInfo_Obj;                                                                         /// 天赋信息

        public GameObject           TalentPanel_Obj;                                                                        /// 天赋面板
        public GameObject           MaskOpen_Obj;                                                                           /// 遮罩
        public GameObject           SceneGreyObj;                                                                           /// 大招灰屏

        public UILabel              Timer_Label;                                                                            /// 计时
        public Clocker              BattleTime_Clock;                                                                       /// 计时

        #region================================================||   AutoBattle,Battlespeed -- < 自动战斗,战斗速度 > ||<FourNode>================================================
        public UILabel              BattleSpeed_Label;
        public GameObject           AutoBattleBtn_Obj;
        public GameObject           AutoBattleLock_Obj;
        public GameObject           AutoBattleOpen_Obj;
        public GameObject           AutoBattleClose_Obj;
        public GameObject           BattleSpeedBtn_Obj;
        public GameObject           BattleSpeedLockObj;
        #endregion
        #region================================================||   Drop_Coin,Drop_Box  --    < 掉落金币,掉落宝箱 > ||<FourNode>================================================
        public UILabel              DropBox_Label;                                                                          /// 掉落宝箱文本
        public GameObject           DropBox_Obj;                                                                            /// 掉落宝箱对象
        public GameObject           DropBoxEffect_Obj;                                                                      /// 掉落宝箱特效对象

        public UILabel              DropCoin_Label;                                                                         /// 掉落金币文本
        public NumTurn              Turn_CoinCount;                                                                         /// 金币数字增加减少
        public GameObject           DropCoin_Obj;                                                                           /// 掉落金币对象
        public GameObject           DropCoinEffect_Obj;                                                                     /// 掉落金币特效对象
        #endregion
        [Inject]
        public IPlayer              InPlayer                        { set; get; }
        [Inject]
        public IBattleStartData     InBattleStart_D                 { set; get; }

        protected override void Start()                                                                                     // 初始化执行(Base)       
        {
            UIEventListener.Get(PauseBtn_Obj).onClick               = PauseBtn_Click;                                       /// 暂停点击
            UIEventListener.Get(AutoBattleBtn_Obj).onClick          = AutoBattleBtn_Click;                                  /// 自动战斗点击
            UIEventListener.Get(BattleSpeedBtn_Obj).onClick         = BattleSpeedBtn_Click;                                 /// 战斗速度点击

            UIEventListener.Get(AutoBattleLock_Obj).onClick         = ShowNotAutoBattleTips;                                /// 自动战斗未开启 提升
            UIEventListener.Get(TalentPanel_Obj).onClick            = ShowTalent;                                           /// 显示天赋

            BaseInit();                                                                                                     /// 初始化设置
            base.Start();           
        }

        public void                 BaseInit                ()                                                              // 初始化设置             
        {
            BattleSpeedLockObj.SetActive (InPlayer.PlayerLevel < X2UnlockLevel);                                            /// 战斗速度显示设置       
            SetReward               ();                                                                                     /// 自动战斗设置       
                    
            TickedReset             ();                                                                                     /// 计时器重置
            BattleTime_Clock.TheTicked                              = Clock_OnTicked;                                       /// 计时器启动

            SetAutoBattle           ();                                                                                     /// 战斗掉落奖励UI 设置
            UIFlyOut                ();                                                                                     /// UI飞出动画

        }
        public void                 PauseBtn_Click          ( GameObject inObj )                                            // 暂停 点击              
        {
            BattleControll.sInstance.GreyCamera.depth               = BattleParmConfig.CameraNormalDept;                    // 摄像机正常深度
            BattleControll.sInstance.IsBattlePause                  = true;                                                 // 战斗暂停
            BattleControll.sInstance.CurrentGameSpeed               = BattleParmConfig.TimeScaleZero;                       // 游戏进度 0_停止
            PanelManager.sInstance.ShowPanel                        (SceneType.Battle, BattleResStrName.PanelName_Pause);   // 显示暂停面板
        }
        public void                 AutoBattleBtn_Click     ( GameObject inObj )                                            // 自动战斗 点击          
        {
            BattleParmConfig.IsAutoBattle                           = !BattleParmConfig.IsAutoBattle;                       
            AutoBattleOpen_Obj.SetActive    (!BattleParmConfig.IsAutoBattle);
            AutoBattleClose_Obj.SetActive   ( BattleParmConfig.IsAutoBattle);
        }
        public void                 BattleSpeedBtn_Click    ( GameObject inObj )                                            // 战斗速度 点击          
        {
            if (!X2Enabled)                                                                                                 // 2倍加速未开启,返回
            {
                PanelManager.sInstance.ShowNoticePanel(string.Format(Language.GetValue("Clickautofight"),
                             Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1027).Viplevel.ToString(),
                             Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1027).LimitLevel.ToString()));
                return;
            }
            ClickCount++;
            ClickCount = ClickCount % 3;

            if(!X3Enabled)                                                                                                  // 2倍加速点击切换
            {
                if      ( ClickCount == 0)
                {
                    BattleSpeed_Label.text                          = "战斗\n加速";
                    BattleParmConfig.TimeScaleNormal                /= SpeedParaX2;
                    BattleParmConfig.TimeScaleUltFired              /= SpeedParaX2;
                    BattleParmConfig.TimeScaleZoom                  /= SpeedParaX2;
                    BattleControll.sInstance.CurrentGameSpeed       /= SpeedParaX2; 
                }
                else if ( ClickCount == 1)
                {
                    BattleSpeed_Label.text                          = "2倍\n加速";
                    BattleParmConfig.TimeScaleNormal                *= SpeedParaX2;
                    BattleParmConfig.TimeScaleUltFired              *= SpeedParaX2;
                    BattleParmConfig.TimeScaleZoom                  *= SpeedParaX2;
                    BattleControll.sInstance.CurrentGameSpeed       *= SpeedParaX2; 
                }
                else if ( ClickCount == 2)
                {
                    PanelManager.sInstance.ShowNoticePanel(string.Format(Language.GetValue("Clickautofight"),
                                 Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1028).Viplevel.ToString(),
                                 Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1028).LimitLevel.ToString()));
                }
            }
            else                                                                                                            // 3倍加速点击切换
            {
                if      ( ClickCount == 0)
                {
                    BattleSpeed_Label.text                          = "战斗\n加速";
                    BattleParmConfig.TimeScaleNormal                /= SpeedParaX3;
                    BattleParmConfig.TimeScaleUltFired              /= SpeedParaX3;
                    BattleParmConfig.TimeScaleZoom                  /= SpeedParaX3;
                    BattleControll.sInstance.CurrentGameSpeed       /= SpeedParaX3; 
                }
                else if ( ClickCount == 1)
                {
                    BattleSpeed_Label.text                          = "2倍\n加速";
                    BattleParmConfig.TimeScaleNormal                *= SpeedParaX2;
                    BattleParmConfig.TimeScaleUltFired              *= SpeedParaX2;
                    BattleParmConfig.TimeScaleZoom                  *= SpeedParaX2;
                    BattleControll.sInstance.CurrentGameSpeed       *= SpeedParaX2; 
                }
                else if ( ClickCount == 1)
                {
                    BattleSpeed_Label.text                          = "3倍\n加速";
                    BattleParmConfig.TimeScaleNormal                *= SpeedParaX3 / SpeedParaX2;
                    BattleParmConfig.TimeScaleUltFired              *= SpeedParaX3 / SpeedParaX2;
                    BattleParmConfig.TimeScaleZoom                  *= SpeedParaX3 / SpeedParaX2;
                    BattleControll.sInstance.CurrentGameSpeed       *= SpeedParaX3 / SpeedParaX2; 
                }
            }
                                  
        }

        public void                 ShowNotAutoBattleTips   ( GameObject inObj )                                            // 自动战斗未开启提示      
        {
            PanelManager.sInstance.ShowNoticePanel(string.Format(Language.GetValue("Clickautofight"),
                        Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1026).Viplevel.ToString(),
                        Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1026).LimitLevel.ToString()));
        }
        public void                 ShowTalent              ( GameObject inObj )                                            // 天赋显示               
        {
            if (TalentInfo_Obj.activeSelf)                          TalentInfo_Obj.SetActive(false);
            else                                                    TalentInfo_Obj.SetActive(true);
        }
        public void                 SetAutoBattle()                                                                         // 自动战斗初始化设置      
        {
            BattleSpeedBtn_Obj.SetActive        (true);
            if (BattleReport.sInstance.IsReplay)                                                                            // 战斗回放
            {
                AutoBattleBtn_Obj.SetActive     (false);
                AutoBattleLock_Obj.SetActive    (false);
                MaskOpen_Obj.SetActive          (true);
                PauseBtn_Obj.SetActive          (true);

            }
            else if ( InPlayer.IsGuide)                                                                                     // 新手引导
            {
                AutoBattleBtn_Obj.SetActive     (false);
                AutoBattleLock_Obj.SetActive    (false);
                MaskOpen_Obj.SetActive          (false);
                PauseBtn_Obj.SetActive          (false);
                BattleSpeedBtn_Obj.SetActive    (false);
            }
            else
            {
                bool                IsShow                          = false;

                AutoBattleOpen_Obj.SetActive(!BattleParmConfig.IsAutoBattle);
                AutoBattleClose_Obj.SetActive(BattleParmConfig.IsAutoBattle);

                if ( InPlayer.PlayerVIPLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1026).Viplevel ||
                     InPlayer.PlayerVIPLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1026).LimitLevel )
                {    IsShow         = true;                         }

                switch ( InBattleStart_D.BattleType)
                {
                    case BattleType.CheckPoint:
                    case BattleType.MonsterWarPhy:
                    case BattleType.MonsterWarMagic:
                    case BattleType.DragonTrialIce:
                    case BattleType.DragonTrialFire:
                    case BattleType.DragonTrialThunder:
                        {
                            AutoBattleBtn_Obj.SetActive             (IsShow);
                            AutoBattleLock_Obj.SetActive            (!IsShow);
                            if (InPlayer.IsFirstBattle && IsShow)   AutoBattleBtn_Click(null);
                            MaskOpen_Obj.SetActive(false);
                            break;
                        }
                    case BattleType.JJCLevel:
                    case BattleType.JJC:
                        {
                            if (InBattleStart_D.IsJJCLevel )
                            {
                                AutoBattleBtn_Obj.SetActive         (true);
                                AutoBattleLock_Obj.SetActive        (false);
                                MaskOpen_Obj.SetActive              (false);
                            }
                            else
                            {
                                AutoBattleBtn_Obj.SetActive         (true);
                                AutoBattleLock_Obj.SetActive        (false);
                                MaskOpen_Obj.SetActive              (true);
                            }
                            break;
                        }
                    case BattleType.GuideFirstBattle:
                        {
                            AutoBattleBtn_Obj.SetActive             (false);
                            AutoBattleLock_Obj.SetActive            (false);
                            BattleSpeedBtn_Obj.SetActive            (false);
                            MaskOpen_Obj.SetActive                  (false);
                            PauseBtn_Obj.SetActive                  (false);
                            Timer_Label.enabled                     = false;
                            break;
                        }
                    default:
                        {
                            AutoBattleBtn_Obj.SetActive             (true);
                            AutoBattleLock_Obj.SetActive            (false);
                            MaskOpen_Obj.SetActive                  (false);
                            break;
                        }
                }
            }
        }
        public void                 SetReward()                                                                             // 战斗掉落奖励 初始化设置 
        {
            switch (InBattleStart_D.BattleType)
            {
                case BattleType.CheckPoint:
                    {
                        DropCoin_Obj.SetActive      (true);
                        DropBox_Obj.SetActive       (false);
                        break;
                    }
                case BattleType.MonsterWarPhy:
                case BattleType.MonsterWarMagic:
                case BattleType.DragonTrialIce:
                case BattleType.DragonTrialFire:
                case BattleType.DragonTrialThunder:
                    {
                        DropCoin_Obj.SetActive      (false);
                        DropBox_Obj.SetActive       (true);
                        break;
                    }
                default:
                    {
                        DropCoin_Obj.SetActive      (false);
                        DropBox_Obj.SetActive       (false);
                        break;
                    }
            }
        }

        public IBattleMemUI         LoadMemUI                ( Battle_Camp inCamp, IBattleMemberData inMemD)                // 加载成员UI <可迁移代码> 
        {
            if      ( inCamp == Battle_Camp.Our)                                                                            // OurMemUI     
            {
                UnityEngine.Object  TempObj                 = Resources.Load(BattleResStrName.PanelName_MemberUI);
                GameObject          TheGObj                 = Instantiate(TempObj) as GameObject;
                TempObj                                     = null;


                TheGObj.name                                = inMemD.MemberResID.ToString() + "-" + (int)inMemD.MemberPos;
                TheGObj.transform.parent                    = HeroList_Grid.transform;
                TheGObj.transform.localPosition             = Vector3.zero;
                TheGObj.transform.localRotation             = Quaternion.identity;
                TheGObj.transform.localScale                = Vector3.one;

                HeroList_Grid.sorting                       = UIGrid.Sorting.Custom;
                HeroList_Grid.onCustomSort                  = new Comparison<Transform>(delegate (Transform TF1, Transform TF2)
                                                            { return int.Parse(TF2.name.Split('-')[1]) - int.Parse(TF1.name.Split('-')[1]); } );
                HeroList_Grid.repositionNow                 = true;

                return                                      TheGObj.GetComponent<MemberUI>();
            }
            else if ( inCamp == Battle_Camp.Enemy)                                                                          // EnemyMemUI   
            {
                UnityEngine.Object  TempObj                 = Resources.Load(BattleResStrName.PanelName_MemberUI);
                GameObject          TheGObj                 = MonoBehaviour.Instantiate(TempObj) as GameObject;
                TempObj                                     = null;

                TheGObj.name                                = inMemD.MemberResID.ToString();
                TheGObj.transform.parent                    = EnemyUIList_Obj.transform;
                TheGObj.transform.localPosition             = Vector3.zero;
                TheGObj.transform.localRotation             = Quaternion.identity;
                TheGObj.transform.localScale                = Vector3.one;

                return              TheGObj.GetComponent<MemberUI>();
            }
            else                                                                                                            // BossMemUI    
            {
                UnityEngine.Object  TempObj                 = Resources.Load(BattleResStrName.PanelName_MemberUI);
                GameObject          TheGObj                 = MonoBehaviour.Instantiate(TempObj) as GameObject;
                TempObj                                     = null;

                TheGObj.name                                = inMemD.MemberResID.ToString();
                TheGObj.transform.parent                    = gameObject.transform;
                TheGObj.transform.localPosition             = Vector3.zero;
                TheGObj.transform.localRotation             = Quaternion.identity;
                TheGObj.transform.localScale                = Vector3.one;

                return              TheGObj.GetComponent<MemberUI>();
            }

        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private bool                X2Enabled                       = false;                                                /// 2倍速度开发
        private bool                X3Enabled                       = false;                                                /// 3倍速度开发

        private int                 ClickCount                      = 0;                                                    /// 点击累计
        private int                 X2UnlockLevel                   = 0;                                                    /// 2倍速度解锁等级
        private int                 X3UnlockLevel                   = 0;                                                    /// 3倍速度解锁等级

        private void                Clock_OnTicked          (TimeSpan inStartTime)                                          //  计时器开启      
        {
            BattleControll.sInstance.BattleDuration                 = inStartTime;                                          /// 

            inStartTime                                             = BattleParmConfig.BattleMaxTime - inStartTime;         ///
            Timer_Label.text        = inStartTime.Minutes.ToString() + ":" + inStartTime.Seconds.ToString().PadLeft(2, '0');/// 转换显示分钟,秒钟

            if ( inStartTime.TotalSeconds < 1 )
            {
                BattleTime_Clock.Stop();
                dispatcher.Dispatch (BattleEvent.ClockerTimeOut_Event);
            }

        }
        private void                TickedReset()                                                                           //  计时器重置      
        {
            BattleControll.sInstance.BattleDuration = TimeSpan.Zero;
            Timer_Label.text = BattleParmConfig.BattleMaxTime.Minutes.ToString() + ":" + BattleParmConfig.BattleMaxTime.Seconds.ToString().PadLeft(2, '0');
            BattleTime_Clock.ClockReset();
        }
        private void                InitBattleSpeed()                                                                       //  初始化战斗速度  
        {
            X2UnlockLevel           = Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1027).LimitLevel;
            X3UnlockLevel           = Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1028).LimitLevel;

            X2Enabled               = InPlayer.PlayerLevel >= X2UnlockLevel || InPlayer.PlayerVIPLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1027).Viplevel;
            X3Enabled               = InPlayer.PlayerLevel >= X3UnlockLevel || InPlayer.PlayerVIPLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1028).Viplevel;
        }

        public void                 UIFlyOut()                                                                              //  UI飞出 动画     
        {
            TopUI.AddComponent<TweenPosition>().duration            = 0.6f;
            TopUI.GetComponent<TweenPosition>().from                = new Vector3(0, 320, 0);
            TopUI.GetComponent<TweenPosition>().to                  = new Vector3(0, 420, 0);
            TopUI.GetComponent<TweenPosition>().PlayForward();

            BottomUI.AddComponent<TweenPosition>().duration         = 0.6f;
            BottomUI.GetComponent<TweenPosition>().from             = new Vector3(0, -342, 0);
            BottomUI.GetComponent<TweenPosition>().to               = new Vector3(0, -470, 0);
            BottomUI.GetComponent<TweenPosition>().PlayForward();
        }
        public void                 UIFlyIn()                                                                               //  UI飞入 动画     
        {
            TopUI.GetComponent<TweenPosition>().duration            = 10f;
            BottomUI.GetComponent<TweenPosition>().delay            = 2f;
            TopUI.GetComponent<TweenPosition>().from                = new Vector3(0, 421, 0);
            TopUI.GetComponent<TweenPosition>().to                  = new Vector3(0, 322, 0);
            TopUI.GetComponent<TweenPosition>().PlayForward();

            BottomUI.GetComponent<TweenPosition>().duration         = 10f;
            BottomUI.GetComponent<TweenPosition>().delay            = 2f;
            BottomUI.GetComponent<TweenPosition>().from             = new Vector3(0, -470, 0);
            BottomUI.GetComponent<TweenPosition>().to               = new Vector3(0, -320, 0);
            BottomUI.GetComponent<TweenPosition>().PlayForward();
        }
        #endregion
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  战斗主界面 Mediator </summary>
    public class BattleMainPanelMediator : EventMediator
    {
        [Inject]
        public BattleMainPanelView  InView                          { set; get; }                                           //  战斗主界面视图
        [Inject]
        public IBattleStartData     InBattleStart_D                 { set; get; }                                           //  战斗初始化数据
        [Inject]
        public IBattleEndData       InBattleEnd_D                   { set; get; }                                           //  战斗结束数据

        public override void        OnRegister                  ()                                                          //  注册监听 (Base)    
        {                                                                                   
                                                                                                                            /// <| 计时器监听 [Timer] |>
            dispatcher.AddListener ( BattleEvent.ClockerTimeOut_Event,          ClockerTimeOut_Handler);                    /// 计时器超时 处理
            dispatcher.AddListener ( BattleEvent.TimerClickStop_Event,          TimerClickStop_Handler);                    /// 计时器停止
            dispatcher.AddListener ( BattleEvent.TimerClickBegin_Event,         TimerClickBegin_Handler);                   /// 计时器开始
            dispatcher.AddListener ( BattleEvent.TimerClickPause_Event,         TimerClickPause_Handler);                   /// 计时器暂停

            dispatcher.AddListener ( BattleEvent.TimerClickContinue_Event,      TimerClickContinue_Handler);                /// 计时器继续
            dispatcher.AddListener ( BattleEvent.TimerClickReset_Event,         TimerClickReset_Handler);                   /// 计时器重置

                                                                                                                            /// <| 动画监听 [Anim] |>
            dispatcher.AddListener ( BattleEvent.SceneGrey_Event,               SceneGrey_Handler);                         /// 灰屏
            dispatcher.AddListener ( BattleEvent.SceneGreyRelease_Event,        SceneGreyRelease_Handler);                  /// 灰屏释放
            dispatcher.AddListener ( BattleEvent.ShowCoinEffect_Event,          ShowCoinEffect_Handler);                    /// 展示金币掉落
            dispatcher.AddListener ( BattleEvent.ShowBoxEffect_Event,           ShowBoxEffect_Handler);                     /// 展示宝箱掉落

            dispatcher.AddListener ( BattleEvent.BattleUIFlyIn_Event,           UIFlyIn_Handler);                           /// UI飞入动画
            dispatcher.AddListener ( BattleEvent.BattleUIFlyOut_Event,          UIFlayOut_Handler);                         /// UI飞出动画

                                                                                                                            /// <| 战斗进度 [BattleWave] |>
            dispatcher.AddListener ( BattleEvent.BattleProcessChange_Event,     ProgressChanged_Handler);                   /// 进度更新

        }
        public override void        OnRemove                    ()                                                          //  移除监听 (Base)    
        {
            dispatcher.RemoveListener( BattleEvent.ClockerTimeOut_Event,        ClockerTimeOut_Handler);                    /// 计时器超时 处理
            dispatcher.RemoveListener( BattleEvent.TimerClickStop_Event,        TimerClickStop_Handler);                    /// 计时器停止
            dispatcher.RemoveListener( BattleEvent.TimerClickBegin_Event,       TimerClickBegin_Handler);                   /// 计时器开始
            dispatcher.RemoveListener( BattleEvent.TimerClickPause_Event,       TimerClickPause_Handler);                   /// 计时器暂停
            dispatcher.RemoveListener( BattleEvent.TimerClickContinue_Event,    TimerClickContinue_Handler);                /// 计时器继续
            dispatcher.RemoveListener( BattleEvent.TimerClickReset_Event,       TimerClickReset_Handler);                   /// 计时器重置


            dispatcher.RemoveListener( BattleEvent.SceneGrey_Event,             SceneGrey_Handler);                         /// 灰屏
            dispatcher.RemoveListener( BattleEvent.SceneGreyRelease_Event,      SceneGreyRelease_Handler);                  /// 灰屏释放
            dispatcher.RemoveListener( BattleEvent.ShowCoinEffect_Event,        ShowCoinEffect_Handler);                    /// 展示金币掉落
            dispatcher.RemoveListener( BattleEvent.ShowBoxEffect_Event,         ShowBoxEffect_Handler);                     /// 展示宝箱掉落
            dispatcher.RemoveListener( BattleEvent.BattleUIFlyIn_Event,         UIFlyIn_Handler);                           /// UI飞入动画
            dispatcher.RemoveListener( BattleEvent.BattleUIFlyOut_Event,        UIFlayOut_Handler);                         /// UI飞出动画

            dispatcher.RemoveListener( BattleEvent.BattleProcessChange_Event,   ProgressChanged_Handler);                   /// 进度更新
        }


                                                                                                                            /// <| 计时器监听 [Timer] |>
        public void                 ClockerTimeOut_Handler      (IEvent inEvent)                                            //  战斗超时处理(到达战斗最大时长)
        {
            InBattleEnd_D.IsTimeOut                                 = true;
            foreach ( var Item in BattleControll.sInstance.OurTeam.TeamList)
            {    Item.OwnerDie();                               }
            if( InBattleStart_D.BattleType == BattleType.ParadiseRoad)
            {
                foreach (var Item in BattleControll.sInstance.EnemyTeam.TeamList)
                {   Item.OwnerDie();                            }
            }
        }
        public void                 TimerClickStop_Handler      (IEvent inEvent)                                            //  计时器停止   
        {
            InView.BattleTime_Clock.Stop();
        }
        public void                 TimerClickBegin_Handler     (IEvent inEvent)                                            //  计时器开始   
        {
            InView.BattleTime_Clock.ClockerStart();
        }
        public void                 TimerClickPause_Handler     (IEvent inEvent)                                            //  计时器暂停   
        {
            InView.BattleTime_Clock.Pause();
        }

        public void                 TimerClickContinue_Handler  (IEvent inEvent)                                            //  计时器继续   
        {
            InView.BattleTime_Clock.Continue();
        }
        public void                 TimerClickReset_Handler     (IEvent inEvent)                                            //  计时器重置   
        {
            InView.BattleTime_Clock.ClockReset();
        }


                                                                                                                            /// <| 动画监听 [Anim] |>
        public void                 SceneGrey_Handler           (IEvent inEvent)                                            //  灰屏        
        {
            IBattleMemMediator      TheMem_D                        = (IBattleMemMediator)inEvent.data;
            StartCoroutine          (UltGrey(TheMem_D));
        }
        public void                 SceneGreyRelease_Handler    (IEvent inEvent)                                            //  灰屏释放    
        {
            StartCoroutine(GreyReleas());
        }
        public void                 ShowCoinEffect_Handler      (IEvent inEvent)                                            //  展示金币掉落特效 
        {
            GameObject              TheObj                      = inEvent.data as GameObject;
            if (gameObject.activeSelf)                          StartCoroutine(PlayDropCoinEffect(TheObj));
        }
        public void                 ShowBoxEffect_Handler       (IEvent inEvent)                                            //  展示宝箱掉落特效 
        {
            GameObject              TheObj                      = inEvent.data as GameObject;
            if (gameObject.activeSelf)                          StartCoroutine(PlayDropBoxEffect(TheObj));
        }

        public void                 UIFlyIn_Handler             (IEvent inEvent)                                            //  UI飞入动画   
        {
            InView.UIFlyIn();
        }
        public void                 UIFlayOut_Handler           (IEvent inEvent)                                            //  UI飞出动画   
        {
            InView.UIFlyIn();
        }


                                                                                                                            /// <| 战斗进度 [BattleWave] |>
        public void                 ProgressChanged_Handler     (IEvent inEvent)                                            //  进度更新    
        {
            InView.CurrProgress_Label.text                      = (BattleControll.sInstance.EnemyProgress + 1).ToString();
            InView.MaxProgress_Label.text                       = BattleControll.sInstance.EnemyProgressMax.ToString();
        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private int                 _DropBosCount                = 0;
        private int                 _DropCoinCount               = 0;

        private IEnumerator         UltGrey                     ( IBattleMemMediator inMem )                                //  大招灰屏      
        {
            yield return null;
            TweenAlpha              TheTween                    = InView.SceneGreyObj.GetComponent<TweenAlpha>();
            if ( TheTween != null && TheTween.value == 0)
            {
                TheTween.duration                               = 0.3f;
                TheTween.PlayForward();
            }
            foreach (var Item in inMem.ModelObj.GetComponentsInChildren<Transform>(true))
            {
                if ( Item.gameObject.layer != BattleParmConfig.LayerPublicHitEffect )
                {    Item.gameObject.layer = BattleParmConfig.LayerGrey;            }
            }
        }
        private IEnumerator         GreyReleas                  ( Action inCallback = null )                                //  灰屏释放      
        {
            yield return null;

            TweenAlpha TheTween = InView.SceneGreyObj.GetComponent<TweenAlpha>();
            if (TheTween != null )
            {
                TheTween.duration = 0.3f;
                TheTween.PlayReverse();
            }
            
            foreach (var Item in BattleControll.sInstance.OurTeam.TeamList )
            {
                if (Item.ScaleState != NoScaleTimeState.Normal)
                {
                    Item.ScaleState = NoScaleTimeState.Normal;
                    foreach ( var TheItem in Item.ModelObj.GetComponentsInChildren<Transform>(true))
                    {
                        if (TheItem.gameObject.layer != BattleParmConfig.LayerPublicHitEffect)
                        {   TheItem.gameObject.layer = BattleParmConfig.LayerDefault; }
                    }
                }
            }
            foreach (var Item in BattleControll.sInstance.EnemyTeam.TeamList )
            {
                if (Item.ScaleState != NoScaleTimeState.Normal)
                {
                    Item.ScaleState = NoScaleTimeState.Normal;
                    foreach ( var TheItem in Item.ModelObj.GetComponentsInChildren<Transform>(true))
                    {
                        if (TheItem.gameObject.layer != BattleParmConfig.LayerPublicHitEffect)
                        {   TheItem.gameObject.layer = BattleParmConfig.LayerDefault; }
                    }
                }
            }

            yield return null;
            inCallback();
        }
        private IEnumerator         PlayDropBoxEffect           ( GameObject inObj)                                         //  掉落宝箱特效  
        {
            float                   RateX               = 0;
            float                   RateY               = 0;

            GameObject              TheObj              = InView.DropBox_Obj.transform.FindChild("action_baoxiang").gameObject;
            Vector3                 ThePosV3            = TheObj.transform.position;
            Vector3                 FromV3              = Vector3.zero;
            Vector3                 ToPosV3             = Vector3.zero;
            Vector3[]               TheV3Arr            = new Vector3[3];
            yield return null;

            ThePosV3                                    = BattleControll.sInstance.MainCamera.WorldToScreenPoint(ThePosV3);
            FromV3                                      = BattleControll.sInstance.UICamera.ScreenToWorldPoint(ThePosV3);
            FromV3.z                                    = 0;

            ToPosV3                                     = InView.DropBoxEffect_Obj.transform.position;
            RateX                                       = UnityEngine.Random.Range(0.2f, 0.8f);
            RateY                                       = 1 - RateX;

            TheV3Arr[0]                                 = FromV3;
            TheV3Arr[1]                                 = new Vector3(FromV3.x + RateX * (ToPosV3.x - FromV3.x), FromV3.y + RateY * (ToPosV3.y - FromV3.y), 0);
            TheV3Arr[2]                                 = ToPosV3;

            GameObject              TheEffectObj        = Instantiate(InView.DropBoxEffect_Obj) as GameObject;
            TheEffectObj.transform.parent               = InView.DropBoxEffect_Obj.transform.parent;
            TheEffectObj.transform.position             = FromV3;
            TheEffectObj.transform.localRotation        = Quaternion.identity;
            TheEffectObj.transform.localScale           = Vector3.zero;
            TheEffectObj.SetActive                      (true);

            iTween.MoveTo(TheEffectObj, iTween.Hash("path", TheV3Arr, "time", 1f, "easeType", iTween.EaseType.easeInCirc, "looktarget", ToPosV3));
            yield return new WaitForSeconds(1.05f);

            _DropBosCount++;
            InView.DropBox_Label.text                   = _DropBosCount.ToString();
            TheEffectObj.SetActive(false);
        }
        private IEnumerator         PlayDropCoinEffect          ( GameObject inObj)                                         //  掉落金币特效  
        {
            int                     CoinTotal           = int.Parse(inObj.name);
            float                   RateX               = 0;
            float                   RateY               = 0;
            GameObject              CoinObj             = inObj.transform.FindChild("coin").gameObject; 

            Vector3                 ThePosV3            = CoinObj.transform.position;
            Vector3                 FromPosV3           = Vector3.zero;
            Vector3                 ToPosV3             = Vector3.zero;
            Vector3[]               TheV3Arr            = new Vector3[3];
            yield return null;

            ThePosV3                = BattleControll.sInstance.MainCamera.WorldToScreenPoint(ThePosV3);
            FromPosV3               = BattleControll.sInstance.UICamera.ScreenToWorldPoint(ThePosV3);

            ToPosV3                 = InView.DropBoxEffect_Obj.transform.position;
            RateX                   = UnityEngine.Random.Range(0.2f, 0.8f);
            RateY                   = 1 - RateX;

            TheV3Arr[0]             = FromPosV3;
            TheV3Arr[1]             = new Vector3(FromPosV3.x + RateX * (ToPosV3.x - FromPosV3.x), FromPosV3.y + RateY * (ToPosV3.y - FromPosV3.y), 0);
            TheV3Arr[0]             = ToPosV3;

            GameObject              TheEffectObj        = Instantiate(InView.DropCoinEffect_Obj) as GameObject;
            TheEffectObj.transform.parent               = InView.DropCoinEffect_Obj.transform.parent;
            TheEffectObj.transform.localPosition        = FromPosV3;
            TheEffectObj.transform.localRotation        = Quaternion.identity;
            TheEffectObj.transform.localScale           = Vector3.one;
            TheEffectObj.SetActive(true);

            iTween.MoveTo           (TheEffectObj, iTween.Hash("path", TheV3Arr, "time", 1f, "easeType", iTween.EaseType.easeInCirc, "looktarget", ToPosV3));
            yield return new WaitForSeconds(1.05f);

            InView.Turn_CoinCount.Speed                 = 5;
            InView.Turn_CoinCount.Turn(_DropCoinCount, CoinTotal);
            _DropCoinCount++;

            TheEffectObj.SetActive(false);
        }

        #endregion
    }

}
