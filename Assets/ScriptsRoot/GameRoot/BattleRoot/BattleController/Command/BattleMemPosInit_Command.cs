using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using LinqTools;
namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------///<summary>  战斗成员位置初始化  </summary>
    public class BattleMemPosInit_Command : EventCommand
    {
        [Inject]
        public IBattleStartData     InBattleStart_D                 { set; get; }                                           /// 战斗开始数据
        [Inject]
        public ICheckPointSys       InCHECKP_D                      { set; get; }                                           /// 关卡数据
        [Inject]
        public IPlayer              InPlayer                        { set; get; }                                           /// 玩家数据


        public override void        Execute()                                                                               //  基础执行
        {
            Debug.Log("[ Main: Command_BattleMemPosInit ]: 战斗成员位置初始化  ( No Net Send )");
            BattleControll.sInstance.TheMono.StartCoroutine( MemPosInit() );
        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        SceneNode                   Node                            = null;                                                 /// 坐标节点
        IEnumerator                 MemPosInit      ()                                                                      //  成员坐标初始化 
        {
            yield return new        WaitForSeconds(0.3f);

            IBattleMemMediator      OneMem      = BattleControll.sInstance.OurTeam.GetRandomMem();
            if (OneMem == null)                 dispatcher.Dispatch(BattleEvent.OverBattleWave_Event);                      /// 战斗结束
            else                                BattleControll.sInstance.TheMono.StartCoroutine(MoveToPos());               /// 移动到节点位置
        }
        IEnumerator                 MoveToNodePos   (SceneNode inNode)                                                      //  移动到节点坐标 
        {
            int                     IndexNum                        = 0;
            float                   FirstTimer                      = 0;
            float                   LastTimer                       = 0;
            float                   DelayTime                       = 0;

            foreach (var Item in BattleControll.sInstance.OurTeam.TeamList.OrderBy(P => BattleParmConfig.GetCompareNumByMem(P)))            /// <| 我方Team成员移动 |> 
            {
                IndexNum++;                                                                                                                 /// 索引号
                Vector3             TheFrom                         = Item.MemPos_D.NowPosV3;                                               /// v3坐标起点
                Vector3             TheTo                           = Node.OurPosDic[(int)Item.MemPos_D.FixedPosNum];                       /// v3坐标终点                               

                LastTimer                                           = BattleParmConfig.GetTimeByIndexNumPVE(IndexNum,Item.Camp);            /// 最后时间速度
                DelayTime                                           += BattleParmConfig.StartBattleDelay;                                   /// 延迟时间
                if (FirstTimer == 0)    FirstTimer                  = LastTimer;

                Item.ModelObj.transform.forward                     = (TheTo - Item.MemPos_D.NowPosV3).normalized;                          /// 世界坐标蓝轴
                Item.MoveToNextWavePos(TheTo, LastTimer, DelayTime);                                                                        /// 移动到下波位置
            }

            if (BattleControll.sInstance.OurProgress == 1 && BattleControll.sInstance.EnemyProgress == InBattleStart_D.CurrBattleWave)
            {   dispatcher.Dispatch(BattleEvent.BattleUIFlyOut_Event);          }

            IndexNum                                                = 0;
            DelayTime                                               = 0;
            foreach (var Item in BattleControll.sInstance.EnemyTeam.TeamList.OrderBy(P => BattleParmConfig.GetCompareNumByMem(P)))          ///  <| 敌方Team成员移动 |> 
            {
                IndexNum++;                                                                                                                 /// 索引号
                Vector3             TheFrom                         = Item.MemPos_D.NowPosV3;                                               /// v3坐标起点
                Vector3             TheTo    = BattleControll.sInstance.SceneNodesCollect.GetCurrBirthNode().EnemyPosDic[(int)Item.MemPos_D.FixedPosNum]; /// v3坐标终点

                LastTimer                                           = BattleParmConfig.GetTimeByIndexNumPVE(IndexNum,Item.Camp);            /// 最后时间速度
                DelayTime                                           += BattleParmConfig.StartBattleDelay;                                   /// 延迟时间
                if (FirstTimer == 0)    FirstTimer                  = LastTimer;

                Item.ModelObj.transform.forward                     = (TheTo - Item.MemPos_D.NowPosV3).normalized;                          /// 世界坐标蓝轴
                Item.MoveToNextWavePos                              (TheTo, LastTimer, DelayTime);                                          /// 移动到下波位置
            }

            if(LastTimer > 0)                                                                                                               ///  <| 摄像机移动 |>
            {
                if (!BattleParmConfig.IsChangeScene)
                {
                    Vector3         CameraTo                        = Node.CameraPos;
                    ITrialMove      TheTrialMove                    = CameraTrialMove.BuildMovable();                                       /// 轨迹位移
                    TheTrialMove.MoveTo(BattleControll.sInstance.MainCamera.gameObject, CameraTo, FirstTimer + 0.2f);                       /// 写定时长,速度不起作用
                }
                yield return new WaitForSeconds(FirstTimer / 3);
                ShowDialog(InBattleStart_D.DialogPanelIDList, () =>                                                                         /// 展示对话界面
                 {
                     BattleControll.sInstance.TheMono.StartCoroutine(StartBattle(FirstTimer * 2 / 3));
                 });
            }
            dispatcher.Dispatch     (BattleEvent.ResetBattleSpeed_Event);                                                                   /// 设置战斗速度
        }
        IEnumerator                 MoveToPos       ()                                                                      //  移动到坐标    
        {
            int                     IndexNum                        = 0;                                                    /// 索引号码
            float                   FirstTimer                      = 0;                                                    /// 开始时间
            float                   LastTimer                       = 0;                                                    /// 最后时间
            float                   DelayTime                       = 0;                                                    /// 延迟时间

            foreach (var Item in BattleControll.sInstance.OurTeam.TeamList.OrderBy(P => BattleParmConfig.GetCompareNumByMem(P)))            /// <| 我方Team成员移动 |> 
            {
                IndexNum++;                                                                                                                 /// 索引号
                Vector3             TheFrom                         = Item.MemPos_D.NowPosV3;                                               /// v3坐标起点
                Vector3             TheTo                           = Item.MemPos_D.GetWavePosV3(InBattleStart_D.BattleType, InBattleStart_D.CurrBattleWave);   /// v3坐标终点                          

                LastTimer                                           = BattleParmConfig.GetTimeByIndexNumPVE(IndexNum,Item.Camp);            /// 最后时间速度
                DelayTime                                           += BattleParmConfig.StartBattleDelay;                                   /// 延迟时间
                if (FirstTimer == 0)    FirstTimer                  = LastTimer;

                Item.ModelObj.transform.forward                     = (TheTo - Item.MemPos_D.NowPosV3).normalized;                          /// 世界坐标蓝轴
                Item.MemPos_D.NowPosV3                              = TheTo;
                Item.MoveToNextWavePos(TheTo, LastTimer, DelayTime);                                                                        /// 移动到下波位置

            }

            if (BattleControll.sInstance.OurProgress == 1 && BattleControll.sInstance.EnemyProgress == InBattleStart_D.CurrBattleWave)
            {   dispatcher.Dispatch(BattleEvent.BattleUIFlyOut_Event);          }

            IndexNum                                                = 0;
            DelayTime                                               = 0;
            foreach (var Item in BattleControll.sInstance.EnemyTeam.TeamList.OrderBy(P => BattleParmConfig.GetCompareNumByMem(P)))          ///  <| 敌方Team成员移动 |> 
            {
                IndexNum++;                                                                                                                 /// 索引号
                Vector3             TheFrom                         = Item.MemPos_D.NowPosV3;                                               /// v3坐标起点
                Vector3             TheTo                           = Item.MemPos_D.GetWavePosV3(InBattleStart_D.BattleType, InBattleStart_D.CurrBattleWave); /// v3坐标终点

                LastTimer                                           = BattleParmConfig.GetTimeByIndexNumPVE(IndexNum,Item.Camp);            /// 最后时间速度
                DelayTime                                           += BattleParmConfig.StartBattleDelay;                                   /// 延迟时间
                if (FirstTimer == 0)    FirstTimer                  = LastTimer;

                Item.ModelObj.transform.forward                     = (TheTo - Item.MemPos_D.NowPosV3).normalized;                          /// 世界坐标蓝轴
                Item.MoveToNextWavePos                              (TheTo, LastTimer, DelayTime);                                          /// 移动到下波位置
            }

            if(LastTimer > 0)                                                                                                               ///  <| 摄像机移动 |>
            {
                if (!BattleParmConfig.IsChangeScene)
                {
                    Vector3         CameraTo                        = Node.CameraPos;
                    ITrialMove      TheTrialMove                    = CameraTrialMove.BuildMovable();                                       /// 轨迹位移
                    TheTrialMove.MoveTo(BattleControll.sInstance.MainCamera.gameObject, CameraTo, FirstTimer + 0.2f);                       /// 写定时长,速度不起作用
                }
                yield return new WaitForSeconds(FirstTimer / 3);
                ShowDialog(InBattleStart_D.DialogPanelIDList, () =>                                                                         /// 展示对话界面
                 {
                     BattleControll.sInstance.TheMono.StartCoroutine(StartBattle(FirstTimer * 2 / 3));
                 });
            }

            dispatcher.Dispatch(BattleEvent.ResetBattleSpeed_Event);                                                                        /// 设置战斗速度
        }

        private void                ShowDialog (List<int> inDialogList, System.Action callback,int inDex = 0)               //  显示对话      
        {
            if ( InBattleStart_D.BattleType == BattleType.CheckPoint            ||                                          // PVE_Battle
                 InBattleStart_D.BattleType == BattleType.DragonTrialIce        || 
                 InBattleStart_D.BattleType == BattleType.DragonTrialFire       ||
                 InBattleStart_D.BattleType == BattleType.DragonTrialThunder    || 
                 InBattleStart_D.BattleType == BattleType.MonsterWarPhy         ||
                 InBattleStart_D.BattleType == BattleType.MonsterWarMagic           )
            {
                if ((InBattleStart_D.BattleType == BattleType.CheckPoint && BattleControll.sInstance.EnemyProgress == InBattleStart_D.CurrBattleWave ) ||
                    (InBattleStart_D.BattleType != BattleType.CheckPoint && BattleParmConfig.IsChangeScene))
                {
                    BattleControll.ShowBattleDialog(inDialogList, callback, InPlayer, inDex);
                    return;
                }
            }
            callback();
        }
        IEnumerator                 StartBattle(float inTimer)                                                              //  首次战斗      
        {
            yield return new WaitForSeconds(inTimer - 0.1f);
            Debug.Log("====  BattleStart: 战斗开始  ====");
            BattleControll.sInstance.BattleState                    = BattleState.Fighting;                                 // 设置战斗状态

            if ( InBattleStart_D.BattleType == BattleType.CheckPoint &&                                                     // 首次战斗(FirstBattle)
                 BattleControll.sInstance.EnemyProgress == BattleControll.sInstance.EnemyProgressMax -1 &&                  // 1:关卡类型, 2:敌方进度 = 最大进度-1, 3: 当前关卡<= 普通关卡最大通关, 4: 当前Boss关卡(true)
                 InPlayer.NormalMaxCheckPointHistory.ID <= InCHECKP_D.currentCheckPointID &&                                // 3: 当前关卡<= 普通关卡最大通关,      4: 当前Boss关卡(true)
                 Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(InCHECKP_D.currentCheckPointID).BOSSShow!= 0) 
            {
                Debug.Log("====  FristBattle: 首次战斗  ====");
                RoleShowUI          TheRoleShow     = PanelManager.sInstance.ShowPanel(SceneType.Battle, 
                                                      BattleResStrName.PanelName_RoleShowUI).GetComponent<RoleShowUI>();
                if (TheRoleShow != null)
                {
                    TheRoleShow.HeroInit(Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(InCHECKP_D.currentCheckPointID).BOSSShow);
                    TheRoleShow.Callback = () =>
                    {
                        BattleControll.sInstance.CurrentGameSpeed = BattleControll.sInstance.CurrentGameSpeed;
                    };
                }
            }

            dispatcher.Dispatch(BattleEvent.BattleUIFlyIn_Event);                                                           // UI界面飞入
            if (BattleControll.sInstance.EnemyProgress == InBattleStart_D.CurrBattleWave)                                   // 开始战斗计时
            {       dispatcher.Dispatch(BattleEvent.TimerClickBegin_Event);                        }

            else    dispatcher.Dispatch(BattleEvent.TimerClickContinue_Event);                                              // 战斗继续
 
        }
        #endregion

    }

}
