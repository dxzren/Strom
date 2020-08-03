using UnityEngine;
using System;
using System.Collections;
using strange.extensions.command.impl;
namespace StormBattle
{
    ///--------------------------------------------------------------------------------------------------------------------/// 返回游戏主界面
    public class BackToMainUI_Command:EventCommand
    {
        [Inject]
        public IGameData        InGame_D                            { set; get; }

        public override void    Execute()
        {
            //BattleReport.sInstance.Clear();                                                                               /// 战斗回放      清理
            InGame_D.BattlePanelList().Clear();                                                                             /// 战斗面板list  清理
            BattleControll.sInstance.CurrentGameSpeed               = 1;                                                    /// 游戏速度恢复: 1

            ResourceKit.sInstance.CleanData();                                                                              /// 资源数据       清理
            GC.Collect();                                                                                                   /// 垃圾回收
            SceneController.LoadScene("Main");                                                                              /// 加载主场景
        }
    }
}

