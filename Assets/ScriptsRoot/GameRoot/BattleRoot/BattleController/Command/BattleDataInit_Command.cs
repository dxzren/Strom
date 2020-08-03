using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;


namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  战斗数据初始化 </summary>
    public class BattleDataInit_Command : EventCommand
    {
        [Inject]
        public IBattleStartData     InBattleStart_D                 { set; get; }                                           /// 战斗启动数据

        public override void        Execute()                                                                               //  基础执行
        {
            Debug.Log               ("[ Main: Command_BattleDataInit ]: 战斗数据初始化  ( No Net Send )");
            if ( BattleReport.sInstance.IsShowHP )                                                                          /// 显示血条,显示设置面板 
            {
                PanelManager.sInstance.ShowPanel(SceneType.Battle, BattleResStrName.PanelName_BattleDataSet)
                                    .GetComponent<BattleDataSetUI>().ViewInit(InBattleStart_D, BattleControllDataInit);
            }
            else                    BattleControllDataInit();                                                               /// 战斗主控制数据初始化
        }

        public void                 BattleControllDataInit()                                                                //  战斗主控制数据初始化 
        {
            BattleControll.sInstance.OurProgress                            = 1;                                                                /// 我方当前进度
            BattleControll.sInstance.OurProgressMax                         = InBattleStart_D.OurMemListAtWaveDic.Count;                        /// 我方最大波次
            BattleControll.sInstance.EnemyProgress                          = InBattleStart_D.CurrBattleWave;                                   /// 敌方当前进度
            BattleControll.sInstance.EnemyProgressMax                       = InBattleStart_D.EnemyMemListAtWaveDic.Count;                      /// 敌方最大波次    
            BattleControll.sInstance.MainCamera.transform.localPosition     = BattleParmConfig.CameraPosSet(InBattleStart_D.BattleType, 0);     /// 设置主摄像机位置

            if (BattleControll.sInstance.OurProgressMax < 1 || BattleControll.sInstance.EnemyProgressMax < 1)                                   /// 一方成员进度少于1_战斗结束
            {                       dispatcher.Dispatch                     (BattleEvent.OverBattle_Event);                          }           
            else                    dispatcher.Dispatch                     (BattleEvent.BattleMemDataLoad_Event, (int)LoadMemDataType.All);    /// 加载双方战斗成员
        }
    }

}


