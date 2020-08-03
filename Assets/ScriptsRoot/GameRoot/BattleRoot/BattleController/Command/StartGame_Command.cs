using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using strange.extensions.context.api;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------///<summary>  启动战斗  </summary>
    public class StartGame_Command : EventCommand
    {
        [Inject]
        public IBattleStartData     InBattleStart_D                 { set; get; }
        [Inject]
        public IBattleEndData       InBattleEnd_D                   { set; get; }

        public override void        Execute()
        {
            Debug.Log               ("[ Main: Command_StartBattle ]: 启动战斗 ( NoSend )");

            BattleControll.sInstance.BattleState                    = BattleState.Ready;                                    /// 战斗状态: 准备完毕
            BattleControll.sInstance.BattleType                     = InBattleStart_D.BattleType;                           /// 战斗类型:
            BattleControll.sInstance.IsGradeChallenge               = InBattleStart_D.IsJJCLevel;                           /// 竞技场段位升级
#if UNITY_EDITOR
            Debuger.Log             ("BattleStartData --战斗初始化数据 To JsonObj:" + InBattleStart_D.BattleDataToJsonObj());
#endif
            dispatcher.Dispatch     (BattleEvent.BattleResInit_Event);                                                      /// 1)____初始化战斗资源
        }

    }

}

