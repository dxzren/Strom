using UnityEngine;
using System.Collections;
/// <summary>   战斗事件管理    </summary>

public class BattleEvent
{
    #region================================================||   BattleStart -- 战斗初始化       ||<FourNode>================================================

    public const string         LoadDataForCheckPoint_Event         = "BattleDataForCheckPoint_Event";              /// 关卡战斗数据加载
    public const string         BattleResInit_Event                 = "BattleResInit_Event";                        /// 初始化战斗资源
    public const string         BattleResLoad_Event                 = "BattleResLoad_Event";                        /// 加载战斗资源
    public const string         ResLoadComplete_Event               = "ResLoadComplete_Event";                      /// 资源下载(加载)完成处理

    public const string         BattleDataInit_Event                = "BattleDataInit_Event";                       /// 初始化战斗数据
    public const string         BattleMemDataLoad_Event             = "BattleMemDataLoad_Event";                    /// 加载战斗成员数据
    public const string         BattleMemShow_Event                 = "BattleMemShow_Event";                        /// 显示战斗成员
//    public const string         BattleMemPosInit_Event              = "BattleMemPosInit_Event";                     /// 战斗成员位置初始化
    public const string         BattleMemPvPInit_Event              = "BattleMemPvPInit_Event";                     /// 移动到战斗位置  竞技场
    public const string         LoadBattleOurMemberComplete_Event   = "LoadBattleOurMemberComplete_Event";          /// 加载成员数据完成
    public const string         LoadBattleEnemyMemberComplete_Event = "LoadBattleEnemyMemberComplete_Event";        /// 加载敌员数据完成
  
    #endregion
    #region================================================||   BattleRun -- 战斗运行中         ||<FourNode>================================================     
                                                                                                                    /// <@ 首次战斗 FirstBattle>
    public const string         FristBattleScene_Event              = "FristBattleScene_Event";                     /// 首次战斗场景
    public const string         FristBattleNext_Event               = "FristBattleNext_Even";                       /// 首次战斗跳下一阶段

                                                                                                                    /// <@ 战斗控制 >                                                       
    public const string         FristStartBattle_Event              = "FristStartBattle_Event";                     /// 首次战斗   

    public const string         ChangeBattleScene_Event             = "ChangeBattleScene_Event";                    /// 更换战斗场景
    public const string         OverBattleWave_Event                = "OverBattleWave_Event";                       /// 当前波次结束
    public const string         ResetBattleSpeed_Event              = "ResetBattleSpeed_Event";                     /// 重载战斗速度
    public const string         BattleProcessChange_Event           = "BattleProcessChange_Event";                  /// 战斗进度更改

                                                                                                                    /// <@ UI控件 >
    public const string         ClockerTimeOut_Event                = "ClockerTimeOut_Event";                       /// 计时退出
    public const string         BattleUIFlyIn_Event                 = "BattleUIFlyIn_Event";                        /// 战斗UI 飞入
    public const string         BattleUIFlyOut_Event                = "BattleUIFlyOut_Event";                       /// 战斗UI 飞出
    public const string         SceneGrey_Event                     = "SceneGrey_Event";                            /// 灰屏显示
    public const string         SceneGreyRelease_Event              = "SceneGreyRelease_Event";                     /// 释放黑屏
    public const string         ShowCoinEffect_Event                = "ShowCoinEffect_Event";                       /// 金币掉落特效
    public const string         ShowBoxEffect_Event                 = "ShowBoxEffect_Event";                        /// 宝箱掉落特效

                                                                                                                    /// <@ 按钮点击_战斗计时状态 >
    public const string         MemUltOnClick_Event                 = "MemUltOnClick_Event";                        ///  成员大招 点击
    public const string         TimerClickStop_Event                = "TimerClickStop_Event";                       ///  战斗计时 停止
    public const string         TimerClickBegin_Event               = "TimerClickBegin_Event";                      ///  战斗计时 开始
    public const string         TimerClickPause_Event               = "TimerClickPause_Event";                      ///  战斗计时 暂停
    public const string         TimerClickContinue_Event            = "TimerClickContinue_Event";                   ///  战斗计时 继续
    public const string         TimerClickReset_Event               = "TimerClickReset_Event";                      ///  战斗计时 重新开始
    #endregion
    #region================================================||   BattleOver -- 战斗结束          ||<FourNode>================================================

    public const string         OverBattle_Event                    = "OverBattle_Event";                           /// 战斗结束
    public const string         BattleCompute_Event                 = "BattleCompute_Event";                        /// 战斗结算

    public const string         CheckPointCompute_Event             = "CheckPointCompute_Event";                    /// 关卡结算
    public const string         MonsterCompute_Event                = "MonsterCompute_Event";                       /// 巨兽囚笼结算
    public const string         DragonCompute_Event                 = "DragonCompute_Event";                        /// 巨龙试炼结算
    public const string         AernaCompute_Event                  = "AernaCompute_Event";                         /// 竞技场结算
    public const string         FriendCompute_Event                 = "FriendCompute_Event";                        /// 好友跳战结算
    public const string         ParaRoadCompute_Event               = "ParaRoadCompute_Event";                      /// 天堂之路结算
    public const string         SecretTowerCompute_Event            = "SecretTowerCompute_Event";                   /// 秘境之塔结算

    public const string         ArenaReplayCompute_Event            = "ArenaReplayCompute_Event";                   /// 竞技场场回放结算
    public const string         FriendReplayCompute_Event           = "FriendReplayCompute_Event";                  /// 好友回放结算
    public const string         BackToMianUI_Event                  = "BackToMianUI_Event";                         /// 返回主界面UI

    #endregion
}