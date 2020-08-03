using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
namespace StormBattle
{
    ////------------------------------------------------------------------------------------------------------------------------/// <summary>   关卡战斗数据加载   </summary>
    public class LoadDataForCheckPointCommand : EventCommand
    {
        [Inject]
        public IBattleStartData         InBattleStart                   { set; get; }                                           /// 战斗初始化数据
        [Inject]
        public IPlayer                  InPlayer                        { set; get; }                                           /// 玩家数据
           
        public override void            Execute()
        {
            Debug.Log                   ("[ Main: Command_LoadDataCHECKP ]: 关卡战斗资源初始化 ( NoSend )");  
            BattleDataInit_CP           ();                                                                                     /// 1)____关卡数据初始化           
            InBattleStart.ExecPassSkOrTalentSk();                                                                               /// 2)____执行:被动技能_天赋技能 
            SceneController.LoadScene   ("Battle");                                                                             /// 3)____加载场景:[Battle]       
         }


        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private const BattleType        _BttleType                      = BattleType.CheckPoint;                                /// 战斗类型-关卡
        private void                    BattleDataInit_CP()                                                                     //  关卡数据初始化  
        {
            int                         TheCheckP_ID                    = 0;                                                    /// 关卡ID
            Dictionary<PosNumType,IHeroData> TheLineupDic               = new Dictionary<PosNumType, IHeroData>();              /// 阵容实例

            if (InPlayer.GetLineUpFile  (_BttleType).Count < 1)         Debug.LogError(Language.GetValue("FeedbackMsg063"));    /// 没有数据,提示并报错
            if (evt.data == null)                                       Debug.LogError("事件传入参数错误: [关卡ID]");             /// 事件传入参数错误(evt) 

            TheCheckP_ID                                                = (int)evt.data;                                        /// 关卡ID设置
            TheLineupDic                                                = InPlayer.BattleTypeToLineUp(_BttleType);              /// 关卡阵容设置
            InBattleStart.Clear();                                                                                              /// 1)____清理 BattleStart 数据   

            Configs_CheckPointData      CheckP_C                        = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(TheCheckP_ID);   /// 关卡配置数据
            if ( CheckP_C == null)                                      Debug.LogError("错误的配置文件:" + TheCheckP_ID);                                /// 数据载入错误 返回 
            Configs_ChapterData         Chaper_C                        = Configs_Chapter.sInstance.GetChapterDataByChapterID(CheckP_C.GenusChapter);   /// 章节配置数据
            if ( Chaper_C == null)                                      Debug.LogError("错误的配置文件:" + Chaper_C);                                    /// 数据载入错误 返回 

            if ( InBattleStart.BattleType == _BttleType  )                                                                      /// 2)____[设置外部数据]:BattleStartData 
            {
                InBattleStart.TempObjData                               = TheCheckP_ID;                                         /// 关卡ID
                InBattleStart.rewardCoin                                = CheckP_C.Gold;                                        /// 奖励金币数量
                InBattleStart.battleScene                               = CheckP_C.BattleScene;                                 /// 战斗地图Str
                InBattleStart.BattleType                                = _BttleType;                                           /// 战斗类型(地图)

                InBattleStart.DialogPanelIDList                         = CheckP_C.DialogueGroupID;                             /// 对话面板ID列表
                InBattleStart.EnemyMemListAtWaveDic                     = BattleStartData.BuildNpcMemberDic_CP(CheckP_C);       /// 敌方方阵容字典
                InBattleStart.OurMemberList                             = BattleStartData.BuildHeroMemberList(TheLineupDic,Battle_Camp.Our);        /// 我方基础阵容列表[Base]
                InBattleStart.OurMemListAtWaveDic.Add                   (1, InBattleStart.OurMemberList);                       /// 战斗波次_ 我方阵容 + 第一波
            }
            InPlayer.SaveLineUpFile(InPlayer.BattleTypeToLineUp(_BttleType), _BttleType);                                       /// 3)____保存关卡阵容到本地文件
            InPlayer.GetLineUpFile(_BttleType).Clear();                                                                         /// 4)____关卡阵容数据缓存清理
        }
        #endregion
    }
}
