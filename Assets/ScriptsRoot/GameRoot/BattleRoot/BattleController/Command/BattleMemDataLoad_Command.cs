using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------///<summary>  战斗成员数据加载  </summary>
    public class BattleMemDataLoad_Command : EventCommand
    {
        [Inject]
        public IBattleStartData     InBattleStart_D                 { set; get; }
            
        public override void        Execute()                                                                               // 基础执行                    
        {
            Debug.Log               ("[ Main: Command_BattleMemDataLoad: 战斗成员数据加载 ( No Net Send )");

            dispatcher.AddListener  ( BattleEvent.LoadBattleOurMemberComplete_Event,   OurMemDataComplete);                 /// 我方成员数据加载完成   监听
            dispatcher.AddListener  ( BattleEvent.LoadBattleEnemyMemberComplete_Event, EnemyMemDataComplete);               /// 敌方方成员数据加载完成 监听


            Debug.Log("LoadMemType:" + LoadMemType);
            BattleControll.sInstance.BattleState                    = BattleState.Ready;                                    /// 设置战斗状态
            dispatcher.Dispatch     ( BattleEvent.BattleProcessChange_Event);                                               /// 战斗进度更新 
            LoadMemType                                             = (LoadMemDataType)evt.data;                            /// 加载成员类型

            if (LoadMemType == LoadMemDataType.All || LoadMemType == LoadMemDataType.Our)                                   //  我方成员准备就绪  
            {
                List<IBattleMemberData> OurMemDataList = InBattleStart_D.OurMemListAtWaveDic[BattleControll.sInstance.OurProgress];

                Debug.Log("OurMemDataListCount:" + OurMemDataList.Count);
                MemDataReady(OurMemDataList, Battle_Camp.Our);
            }
            if (LoadMemType == LoadMemDataType.All || LoadMemType == LoadMemDataType.Enemy)                                 //  敌方成员准备就绪  
            {
                Debug.Log("EnemyProgress: " + BattleControll.sInstance.EnemyProgress);
                List<IBattleMemberData>     EnemyMemDataList        = new List<IBattleMemberData>();
                EnemyMemDataList                                    = InBattleStart_D.EnemyMemListAtWaveDic[BattleControll.sInstance.EnemyProgress];
                Debug.Log("EnemyMemDataListCount: " + EnemyMemDataList.Count);
                for (int j = 0; j < EnemyMemDataList.Count; j++ )
                {
                    Debug.Log("ID:("+ j + ")EnemyMemDataListID: " + EnemyMemDataList[j].memberName);
                }
                MemDataReady(EnemyMemDataList, Battle_Camp.Enemy);
            }

        }

        public void                 OurMemDataComplete      (IEvent inEvent)                                                // 我方成员数据加载完成处理     
        {
            int                     TheMemPosNum                    = (int)inEvent.data;
            OurPosNumList.Remove    (TheMemPosNum);
            CheckAllComplete        ();
        }
        public void                 EnemyMemDataComplete    (IEvent inEvent)                                                // 敌方成员数据加载完成处理     
        {
            int                     TheMemID                        = (int)inEvent.data;
            EnemyPosNumList.Remove  (TheMemID);
            Debug.Log("[ [ __Sub Thread ]_EnemyPosNumList: " + EnemyPosNumList.Count);
            CheckAllComplete        ();
        }


        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private LoadMemDataType     LoadMemType                     = LoadMemDataType.All;                                  /// 加载成员类型
        private List<int>           OurPosNumList                   = new List<int>();                                      /// 我方成员Pos列表
        private List<int>           EnemyPosNumList                 = new List<int>();                                      /// 敌方成员Pos列表

        private void                MemDataReady        (List<IBattleMemberData> inMemDataList, Battle_Camp inCamp)         // 成员数据准备就绪 
        {


            foreach ( var Item in inMemDataList)
            {
                if ( inCamp == Battle_Camp.Our)         OurPosNumList.Add   ((int)Item.MemberPos);
                else                                    EnemyPosNumList.Add ((int)Item.MemberPos);
            }
            foreach ( var Item in inMemDataList)                                                                            // 成员UI初始化   
            {
                Debug.Log("MemID:"+Item.MemberID);
                if ( Item.BattleCamp == Battle_Camp.Our && InBattleStart_D.BattleType == BattleType.GuideFirstBattle )
                {
                    BattleMemShow_Command.EnterHero                 = Item;
                    Debug.Log("GuideFirstBattle!");
                }
                else
                {
                    Vector3             TheBirthV3                  = Vector3.zero;
                    BattlePosData       ThePos_D                    = new BattlePosData(Item.MemberPos);
                    TheBirthV3          = ThePos_D.GetWavePosV3(InBattleStart_D.BattleType, 0);                             /// 出生点坐标
                    if ( inCamp == Battle_Camp.Enemy   && Item.MemberType != Battle_MemberType.Npc_WorldBoss                /// NPC 出生点每波 - 16
                         && InBattleStart_D.BattleType != BattleType.JJC        && InBattleStart_D.BattleType != BattleType.GuideFirstBattle
                         && InBattleStart_D.BattleType != BattleType.FriendWar  && InBattleStart_D.BattleType != BattleType.ParadiseRoad 
                         && InBattleStart_D.BattleType != BattleType.JJCLevel                                                               )
                    { TheBirthV3.x -= 16;                                        }
                    Debug.Log("Read MemDataInit" );
                    MemDataInit(TheBirthV3, inCamp, Item);                                                                    // 成员UI初始化
                }
            }
        }
        private void                CheckAllComplete    ()                                                                  // 验证所有成员加载完成 -> 成员展示
        {
            if ( OurPosNumList.Count < 1 && EnemyPosNumList.Count < 1 )
            {
                dispatcher.RemoveListener   (BattleEvent.LoadBattleOurMemberComplete_Event,     OurMemDataComplete);
                dispatcher.RemoveListener   (BattleEvent.LoadBattleEnemyMemberComplete_Event,   EnemyMemDataComplete);
                Debug.Log                   ("[ Main:Next ] AllMemDataComplete ==> BattleMemShow_Event ");
                dispatcher.Dispatch         (BattleEvent.BattleMemShow_Event,                (int)LoadMemType);             /// 成员展示
            }
        }
        private void                MemDataInit         (Vector3 inPosV3,Battle_Camp inCamp , IBattleMemberData inMemD)     // 成员数据初始化 [加载BattleMemView]  
        {
            Debug.Log("[ __Sub Thread ]_MemUIInit:" + inMemD.MemberResID);                                                  ///<| 成员视图加载设置 [ BattleMemView ] |>
            Object                  TempObj     = Util.Load(BattleResStrName.PanelName_MemberUI);
            GameObject              TheGObj     = MonoBehaviour.Instantiate(TempObj) as GameObject;
            TempObj                             = null;

            TheGObj.name                        = inCamp.ToString() + (int)inMemD.MemberPos;
            TheGObj.transform.parent            = BattleControll.sInstance.RootMemberListObj.transform;
            TheGObj.transform.localPosition     = inPosV3;
            TheGObj.transform.localScale        = BattleParmConfig.GetMemberModelScale(inMemD);
            TheGObj.transform.localRotation     = Quaternion.Euler(0, -90, 0);
            TheGObj.SetActive                   (false);

            IBattleMemUI            TheMemUI    = BattleControll.sInstance.BattleMianPanel.LoadMemUI(inCamp,inMemD);        
            BattleMemView           TheMemView  = TheMemUI.AttackerObj.GetComponent<BattleMemView>();                       

            TheMemUI.IMemData                   = inMemD;
            TheMemUI.OnClicked                  = TheMemView.MemberUI_Onclick;
            TheMemUI.SetMoveMemUIShow           (false);
            TheMemUI.MemModelObj                = TheGObj;
            TheMemUI.MyModelUpItem.bodyHeight  = BattleParmConfig.GetMemberBodyHeight(inMemD.MemberResID);

            TheMemView.IMemUI                   = TheMemUI;
            TheMemView.ModelObj                 = TheGObj;

            if (ResourceKit.sInstance.IsFindAnimEffect(inMemD.MemberResID))                                                 // 
            {
//                Debug.Log("[ __Sub Thread ]_Member.ResourceID is Find of ResourceKit.");
                TheMemView.Battle_AE            = ResourceKit.sInstance.GetAnimEffect(inMemD.MemberResID).UseModelData();
                TheMemView.Battle_AE.Member_D   = inMemD;            
            }

            if (inMemD.MemberType == Battle_MemberType.Npc_WorldBoss)                                                       // 世界Boss 模型亮度修正
            {   ColorMangerItem.SetMemModelColor(TheMemView.Battle_AE.Model, BattleParmConfig.GetMemberModelColor(inMemD)); }

            TheMemView.IMem_D                   = inMemD;
            TheMemView.Camp                     = inCamp;
//            Debug.Log("[ __Sub Thread ]_MemberID:"+ inMemD.MemberID + "\tMemberPos: " + inMemD.MemberPos);
            TheMemView.MemPos_D                 = new  BattlePosData(inMemD.MemberPos);
            TheMemView.BodySize                 = BattleParmConfig.GetMemberModelSize(inMemD.MemberType);
            TheMemView.AttackRange              = BattleParmConfig.GetAttackRange(TheMemView.MemPos_D);
//            Debug.Log("[ __Sub Thread ]_TheMemView.Battle_AE.AnimAndEffec_C.AttackIntervalTime: " + TheMemView.Battle_AE.AnimAndEffec_C.AttackIntervalTime);
            TheMemView.AttackInterval           = TheMemView.Battle_AE.AnimAndEffec_C.AttackIntervalTime;
                                                                                                                            /// <| 影子设置 [ ShadowObj ] |>
            TempObj                             = BattleParmConfig.ShadowObj;
            GameObject              ShadowObj   = MonoBehaviour.Instantiate(TempObj) as GameObject;
            TempObj                             = null;
            ShadowObj.transform.parent          = TheMemView.Battle_AE.Model.transform;
            ShadowObj.transform.localPosition   = Vector3.zero;
            ShadowObj.transform.localRotation   = Quaternion.Euler(90, 0, 0);
            ShadowObj.transform.localScale      = new Vector3(TheMemView.BodySize, TheMemView.BodySize, TheMemView.BodySize) / 38f;
        }
        #endregion
    }
}
public enum LoadMemDataType                                                                                                 // 加载成员数据类型
{
    Our                             = 1,                            /// 我方成员数据                                                                   
    Enemy                           = 2,                            /// 敌方成员数据
    All                             = 3,                            /// 双方成员数据
}
