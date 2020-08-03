using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using StormBattle;

public class DataContext : MVCSContext
{
    public DataContext              ( MonoBehaviour view ) : base( view )
    { }

    protected override void         mapBindings()
    {
        #region=========================================||  SystemData      -- 系统数据绑定     ||=======================================
                                                                                                                                        ///<@ LogIn_System: 登录系统_主系统
        injectionBinder.Bind        < IStartData >().To                 < StartData >           ().ToSingleton().CrossContext();        /// 游戏启动数据
        injectionBinder.Bind        < IGameData >().To                  < GameData >            ().ToSingleton().CrossContext();        /// 游戏主数据
        injectionBinder.Bind        < IPlayer >().To                    < PlayerData >          ().ToSingleton().CrossContext();        /// 玩家主数据
        injectionBinder.Bind        < IHeroData >().To                  < HeroData >            ().CrossContext();                      /// 英雄数据


                                                                                                                                        ///<@ MainSystem:   主界面功能系统 
        injectionBinder.Bind        < IMainData >().To                  < MainData >            ().ToSingleton().CrossContext();        /// 主界面数据
        injectionBinder.Bind        < IHeroSysData >().To               < HeroSysData >         ().ToSingleton().CrossContext();        /// 英雄系统数据
        injectionBinder.Bind        < IRechargeData >().To              < RechargeData >        ().ToSingleton().CrossContext();        /// 充值系统
        injectionBinder.Bind        < IBagSysData >().To                < BagSysData >          ().ToSingleton().CrossContext();        /// 背包系统


                                                                                                                                        ///<@ CheckPoint:   关卡副本系统  
        injectionBinder.Bind        < ICheckPointSys >().To             < CheckPointSys >       ().ToSingleton().CrossContext();        /// 主关卡系统


                                                                                                                                        ///<@ Battle:       战斗系统 
        injectionBinder.Bind        < IBattleMemberData>().To           < BattleMemberData>     ().ToSingleton().CrossContext();        /// 战斗成员数据
        injectionBinder.Bind        < IBattleEndData>().To              < BattleEndData>        ().ToSingleton().CrossContext();        /// 战斗结束数据
        injectionBinder.Bind        < IBattleStartData>().To            < BattleStartData>      ().ToSingleton().CrossContext();        /// 战斗数据准备加载
        injectionBinder.Bind        < IPersetLineupData>().To           < PersetLineupData>     ().ToSingleton().CrossContext();        /// 战斗预置阵容


                                                                                                                                        ///<@ Server:       服务器 
        injectionBinder.Bind        < IRequest >().To                   < HttpRequest  >        ().CrossContext();                      /// WEB请求绑定      
        injectionBinder.Bind        < IServers >().To                   < Servers >             ().ToSingleton().CrossContext();        /// 服务器信息
        injectionBinder.Bind        < ISocket >().To                    < SocketClient >        ().ToSingleton().CrossContext();        /// 客户端请求
        #endregion
        #region=========================================||  NetWorkCallback -- 服务器回调绑定   ||=======================================
                                                                                                                                        ///<@ LogIn_System: 登录系统_主系统 
        injectionBinder.Bind        < IGlobalNetWorkCallback>().To      < GlobalNetWorkCallback>().ToSingleton().CrossContext();        /// 全局回调
        injectionBinder.Bind        < IStartNetWorkCallback>().To       < StartNetWorkCallback> ().ToSingleton().CrossContext();        /// Start回调

                                                                                                                                        ///<@ MainSystem:   主界面功能系统 
        injectionBinder.Bind        < IMainNetWorkCallback >().To       < MainNetWorkCallback > ().ToSingleton().CrossContext();        /// 主界面回调
        injectionBinder.Bind        < IHeroSysNetWorkCallback>().To     < HeroSysNetWorkCallback>().ToSingleton().CrossContext();       /// 英雄系统回调
        injectionBinder.Bind        < IBagNetWorkCallback >().To        < BagNetWorkCallback >  ().ToSingleton().CrossContext();        /// 背包系统回调

                                                                                                                                        ///<@ CheckPoint:   关卡副本系统 
        injectionBinder.Bind        < ICheckPointNetWorkCallback>().To  < CheckPointNetWorkCallback >().ToSingleton().CrossContext();   /// 关卡系统回调
        #endregion
        #region=========================================||  Event_To_Command-- 事件与命令绑定   ||======================================

        commandBinder.Bind(         GlobalEvent.StartGlobalListenerEvent).To<GlobalListenerCommander>();                                /// 全局监听


        BindGlobalMsg();
        #endregion
    }
    
    public void BindGlobalMsg()
    {
        #region=========================================||  登录系统_全局核心系统   ||============================================

        //-----------------------<< Start_Event--  登录开始事件 >>----------------------------------------------------------------
        crossContextBridge.Bind(StartEvent.         GetNickNameEventCallBack_Event );                    // 获取随机昵称回调
        crossContextBridge.Bind(StartEvent.         GameEnter_Event );                                   // 进入游戏
        crossContextBridge.Bind(StartEvent.         RoleEnter_Event );                                   // 以选定的角色进如游戏
        crossContextBridge.Bind(StartEvent.         ClientReady_Event );                                 // 客户端准备完毕可以接收数据
        crossContextBridge.Bind(StartEvent.         StartBattleBegin_Event );                            // 启动初始战斗

        crossContextBridge.Bind(StartEvent.         RefreshServerName_Event );                           // 刷新服务器名称
        crossContextBridge.Bind(StartEvent.         RefreshPublic_Event );                               // 更新公告事件
        crossContextBridge.Bind(StartEvent.         LogIn_Event );                                       // 登录事件    <断线重连>
        crossContextBridge.Bind(StartEvent.         REQCheckCreateRole_Event );                          // 请求校验创建角色事件
        crossContextBridge.Bind(StartEvent.         CreateRole_Event );                                  // 创建角色事件
        crossContextBridge.Bind(StartEvent.         REQLoadConfig_Event );                               // 加载初始化事件

        //-----------------------<< EventSignal-- 事件信号 >>----------------------------------------------------------------
        crossContextBridge.Bind(EventSignal.        Reward_sure_Event );                                 // 每天服务器更新发送的推送通知
        crossContextBridge.Bind(EventSignal.        UpdateInfo_Event );                                  // 更新界面(金币,钻石,体力)
        crossContextBridge.Bind(EventSignal.        BattleFinished_Event );                              // 战斗结束事件
        crossContextBridge.Bind(EventSignal.        GuideDestroy_Event );                                // 新手引导清除
        crossContextBridge.Bind(EventSignal.        NewDayRefresh_Event );                               // 每天更新服务器发送的推送通知

        //-----------------------<< Global_Event--  全局事件 >>-------------------------------------------------------------------
        crossContextBridge.Bind(GlobalEvent.        StartGlobalListenerEvent );                          // 开始全局监听

        crossContextBridge.Bind(GlobalEvent.        UIAnimationEvent );                                  // UI动画
        crossContextBridge.Bind(GlobalEvent.        RechargeEvent );                                     // 充值事件
        crossContextBridge.Bind(GlobalEvent.        ChangeEvent );                                       // 服务器推送数据发生变化(包含:装备,装备碎片,卷轴,卷轴碎片,英雄魂石,金币道具,英雄经验道具,勋章经验道具,翅膀经验道具,扫荡券,体力道具)
        crossContextBridge.Bind(GlobalEvent.        AddHeroEvent );                                      // 添加整卡英雄
        crossContextBridge.Bind(GlobalEvent.        AddHeroClickEvent );                                 // 新整卡英雄信息点击

        crossContextBridge.Bind(GlobalEvent.        CurrencyEvent );                                     // 玩家货币发生变化
        crossContextBridge.Bind(GlobalEvent.        TaskList_NewEvent );                                 // 新接任务
        crossContextBridge.Bind(GlobalEvent.        TaskList_StateEvent );                               // 单个任务状态发生变化
        crossContextBridge.Bind(GlobalEvent.        Eamil_New );                                         // 刷新邮件数据
        crossContextBridge.Bind(GlobalEvent.        RET_Red_Point_Event );                               // 刷新主界面红点

        crossContextBridge.Bind(GlobalEvent.        RET_FriendStateUpdate_Event );                       // 好友状态更新
        crossContextBridge.Bind(GlobalEvent.        RET_FriendListUpdate_Event );                        // 好友列表更新
        crossContextBridge.Bind(GlobalEvent.        PlayerExpEvent );                                    // 玩家经验发生变化


        #endregion

        #region=========================================||  主界面功能系统          ||============================================

        //-----------------------<< HeroSys_Event--  英雄事件 >>-------------------------------------------------------------------
        crossContextBridge.Bind(HeroSysEvent.RETMergeEquip_Event);                              // 合成装备
        crossContextBridge.Bind(HeroSysEvent.RETWingStrengthen_Event);                          // 翅膀提升
        crossContextBridge.Bind(HeroSysEvent.RETUpQuality_Event);                               // 升级品质
        crossContextBridge.Bind(HeroSysEvent.RETUpSkillLv_Event);                               // 升级技能
        crossContextBridge.Bind(HeroSysEvent.RETUpStar_Event);                                  // 升级星级

        crossContextBridge.Bind(HeroSysEvent.RefreshUI_Event);                                  // 更新UI界面
        crossContextBridge.Bind(HeroSysEvent.RETBuySkill_Event);                                // 技能购买
        crossContextBridge.Bind(HeroSysEvent.WearEquip_Event);                                  // 穿戴装备
        crossContextBridge.Bind(HeroSysEvent.RETMedalStrengthen_Event);                         // 寻找提示
        crossContextBridge.Bind(HeroSysEvent.DestoryMedalItem_Event);                           // 销毁勋章材料
        crossContextBridge.Bind(HeroSysEvent.MedalItemClick_Event);                             // 勋章材料点击

        //-----------------------<< CheckPoint_Event--  关卡事件 >>-------------------------------------------------------------------
        crossContextBridge.Bind(CheckPointEvent.RET_GetStarBox_Event);                          // 获取星级宝箱
        crossContextBridge.Bind(CheckPointEvent.RET_Sweep_Event);                               // 扫荡
        crossContextBridge.Bind(CheckPointEvent.REQ_SweepTenTimes_Event);                       // 扫荡十次
        crossContextBridge.Bind(CheckPointEvent.CheckPointItemClick_Event);                     // 关卡点击


        //-----------------------<< ActivityViewEvent--  活动视图事件 >>----------------------------------------------------------------

        crossContextBridge.Bind(GuideEvent.GuideFinished_Event);                                // 新手引导完成
        //-----------------------<< RechargeEvent-- 充值事件 >>-------------------------------------------------------------------
        crossContextBridge.Bind(RechargeEvent.MonthCardTime_Event);                             // 月卡时间戳
        crossContextBridge.Bind(RechargeEvent.RechargeNum_Event);                               // 充值次数
        crossContextBridge.Bind(RechargeEvent.GetPlayerID_Event);                               // 获取玩家ID
        crossContextBridge.Bind(RechargeEvent.GetRechargeUrl_Event);                            // 获取充值地址

        //-----------------------<< ChatEvent-- 聊天消息事件 >>-------------------------------------------------------------------

        #endregion

        #region=========================================||  副本功能系统            ||===============================================

        #endregion

        #region=========================================||  战斗系统                ||===============================================
        crossContextBridge.Bind(BattlePosEvent.UpdateFightForce_Event);                         /// 战斗力更新
        crossContextBridge.Bind(BattlePosEvent.HeroIconClickUpHero_Evnet);                      /// 战斗阵容 英雄上阵 <预置展示>
        crossContextBridge.Bind(BattlePosEvent.HeroIconClickDownHero_Evnet);                    /// 战斗阵容 英雄下阵 <预置展示>

        crossContextBridge.Bind(BattleEvent.LoadDataForCheckPoint_Event);                       /// 关卡 战斗数据加载

        #endregion
    }
}
