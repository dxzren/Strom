using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using StormBattle;
public class MainContext : MVCSContext
{
    public MainContext(MonoBehaviour view) : base(view)
    {
        //dispatcher.Dispatch(GuideEvent.StartSpcificGuide_Event);                                                        /// 打开引导界面 <新手引导进度命令-启动端>
    }

    protected override void mapBindings()
    {
        #region================================================||  <ReLogin> or <PalyerSys>-- 游戏重连or玩家系统 ||=====================================================

        //-----------------------<< 断线重连 >>----------------------------------------------------------------
        commandBinder.Bind(StartEvent.LogIn_Event).To                   < LogIn_Command >();                            /// 账号登录
        commandBinder.Bind(StartEvent.GameEnter_Event).To               < GameEnterView >();                            /// 登录游戏服
        commandBinder.Bind(StartEvent.RoleEnter_Event).To               < RoleEnter_Command >();                        /// 角色进入游戏
        commandBinder.Bind(StartEvent.ClientReady_Event).To             < ClientReady_Command >();                      /// 客户端数据准备完毕
        commandBinder.Bind(RechargeEvent.GetPlayerID_Event).To          < REQPlayerIDCommand >();                       /// 请求玩家ID

        mediationBinder.Bind<StaminaTipsPanelView>().To                 < StaminaTipsPanelMediator>();                  /// 体力提示面板
//        mediationBinder.Bind<GuidePanelView>().To                         < GuidePanelMediator >();                       /// 新手引导面板
        #endregion

        #region================================================||  MainSys       -- 主界面系统UI           ||=====================================================




        mediationBinder.Bind<MainUIPanelView>().To                      < MainUIPanelMediator >();                      /// 主界面UI
        //mediationBinder.Bind<PlayerIconPanelView>().To                  < PlayerIconPanelMediator >();                  /// 玩家头像面板
        #endregion

        #region================================================||  CheckPointSys -- 关卡系统UI             ||=====================================================

        commandBinder.Bind(CheckPointEvent.REQ_CheckPointInfo_Event).To     < REQ_CheckPointInfo_Command >();               /// 关卡信息请求
        commandBinder.Bind(CheckPointEvent.REQ_CheckPointChallenge_Event).To< REQ_CheckPointChallenge_Command>();           /// 关卡挑战请求 
        //commandBinder.Bind(CheckPointEvent.REQ_ChallengeSuccess_Event).To<REQ_ChallengeSuccess_Command>();                /// 挑战成功

        mediationBinder.Bind<WorldMapPanelView>().To                        < WorldMapPanelMediator >();                    /// 世界地图面板
        mediationBinder.Bind<CheckPointItemView>().To                       < CheckPointItemMediator >();                   /// 关卡图标项
        mediationBinder.Bind<CheckPointSelectPanelView>().To                < CheckPointSelectPanelMediator >();            /// 关卡选择面板
        mediationBinder.Bind<CheckPointConfirmPanelView>().To               < CheckPointConfirmPanelMediator>();            /// 关卡确认面板

        mediationBinder.Bind<BattlePositionPanelView>().To                  < BattlePositionPanelMediator>();               /// 战斗预置阵容面板
        mediationBinder.Bind<BP_HeroIconItemView>().To                      < BP_HeroIconItemMediator>();                   /// 战斗预置阵容 英雄头像Item
        mediationBinder.Bind<BP_ModelItemView>().To                         < BP_ModelItemMediator >();                     /// 战斗预置阵容 模型Item
        #endregion

        #region================================================||  BattleSys       -- 战斗系统             ||=====================================================
        commandBinder.Bind(BattleEvent.LoadDataForCheckPoint_Event).To<LoadDataForCheckPointCommand>();

        mediationBinder.Bind<BP_DragView>().To<BP_DragMediator>();                          /// Drag Test
        #endregion


        //#region================================================||  HeroSys       -- 英雄系统UI(暂未开启)             ||=====================================================
        //commandBinder.Bind(HeroSysEvent.REQMergeEquip_Evnet ).To        < REQMergeEquip_Command >();                    /// 合成装备
        //commandBinder.Bind(HeroSysEvent.REQUpStar_Evnet).To             < REQUpStar_Command >();                        /// 升级星级
        //commandBinder.Bind(HeroSysEvent.REQUpQuality_Evnet).To          < REQUpQuality_Command >();                     /// 升级品质
        //commandBinder.Bind(HeroSysEvent.REQUpSkillLv_Evnet).To          < REQUpSkillLv_Command >();                     /// 升级技能
        //commandBinder.Bind(HeroSysEvent.WearEquip_Evnet).To             < WearEquip_Command >();                        /// 穿戴装备
        //mediationBinder.Bind<HeroShowPanelView>().To                    < HeroShowPanelMediator >();                    /// 英雄展示面板
        //mediationBinder.Bind<HeroItemView>().To                         < HeroItemMediator >();                         /// 英雄项
        //mediationBinder.Bind<HeroInfoPanelView>().To                    < HeroInfoPanelMediator >();                    /// 英雄信息面板
        //mediationBinder.Bind<HeroEquipItemView>().To                    < HeroEquipItemMediator >();                    /// 英雄装备面板
        //mediationBinder.Bind<HeroSkillItemView>().To                    < HeroSkillItemMediator >();                    /// 英雄技能面板

        //#region================================================||  MallSys       -- 商城系统UI(暂未开启)             ||=====================================================
        //commandBinder.Bind(MallEvent.REQ_OnceDraw_Event).To             < OnceDraw_Command>();                          /// 单次抽奖请求
        //commandBinder.Bind(MallEvent.REQ_TenTimesDraw_Event).To         < REQ_TenTimesDraw_Command>();                  /// 十连抽奖请求
        //mediationBinder.Bind<MallPanelView>().To                        < MallPanelMediator >();                        /// 商城面板
        //mediationBinder.Bind<OnceDrawPanelView>().To                    < OnceDrawPanelMediator >();                    /// 单次抽奖面板
        //mediationBinder.Bind<TenDrawPanelView>().To                     < TenDrawPanelMediator >();                     /// 十连抽奖面板
        //mediationBinder.Bind<HeroCardShowView>().To                     < HeroCardShowMediator >();                     /// 英雄展示
        //mediationBinder.Bind<CardItemPanelView>().To                    < CardItemPanelMediator >();                    /// 卡牌项


    }
}