using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using StormBattle;

///<FourNode>---------------------------------------------------------------------------------------------------------------/// <summary> 战斗资源数据初始化 </summary>
public class BattleResInit_Command : EventCommand
{
    public const string             PrefabPath_Action               = "Prefabs/RoleAnimation/{0}";                          /// 角色动画
    public const string             PrefabPath_Model                = "Prefabs/RoleModel/{0}";                              /// 角色模型
    public const string             PrefabPath_Effect               = "Prefabs/RoleEffect/{0}";                             /// 角色特效
    public const string             PrefabPath_SenceSet             = "Prefabs/BattleScene/{0}_setting";                    /// 战斗场景设置
    public const string             PrefabPath_Sence                = "Prefabs/BattleScene/{0}";                            /// 战斗场景

    [Inject]
    public IPlayer                  InPlayer                        { set; get; }
    [Inject]
    public IBattleStartData         InBattleStart_D                 { set; get; }

    public override void            Execute                         ()                                                      //  基础执行(BaseExecute)           
    {
        Debug.Log                   ("[ Main: Command_BattleResInit: 战斗资源初始化 ( No Net Send )");

        BattleControll.sInstance.TheMono.StartCoroutine(LoadScene());                                                       /// 1)____加载场景
        LoadMemRes();                                                                                                       /// 2)____加载成员资源(模型/动画/特效)
        dispatcher.Dispatch         (BattleEvent.BattleResLoad_Event, _ResLoadTaskList);                                    /// 3)____加载资源 任务处理

        dispatcher.AddListener      (BattleEvent.ResLoadComplete_Event, ResLoadCompleteHandler);                            /// 监听 资源下载完成
    }
    public IEnumerator              LoadScene                       ()                                                      //  加载场景                        
    {
        SceneController.LoadBattleSceneAsync(InBattleStart_D.battleScene,()=>
        {
            BattleControll.sInstance.MainCamera                     = SceneController.SceneCamera;
            BattleControll.sInstance.SceneRootObj                   = SceneController.Scene;
            BattleControll.sInstance.SceneSetObj                    = SceneController.SceneSetting;
        });
        yield return null;
    }
    public void                     LoadMemRes                      ()                                                      //  加载我方成员资源 (模型/动画/特效) 
    {
        if(BattleResStrName.CameraZoomInEffect != null)                                                                     //  加载战斗通用特效                 
        {
            AddATKEffectTask(BattleResStrName.BornEffectName);                                                              /// 出生特效
            AddATKEffectTask(BattleResStrName.DeathEffect);                                                                 /// 死亡特效
            AddATKEffectTask(BattleResStrName.CameraZoomInEffect);                                                          /// 摄像机 拉近特效
            AddATKEffectTask(BattleResStrName.UltFired2Deffect);                                                            /// 大招释放 2D特效
            AddATKEffectTask(BattleResStrName.RunningEffect);                                                               /// 奔跑特效
        }

        foreach (var Item in InBattleStart_D.OurMemberList)                                                                 //  加载我方成员 模型/动作/技能特效 数据  
        {
            Item.Damage             = 0;                                                                                    //  初始化伤害
            MemLoadAnimEffect       (Item);                                                                                 //  加载成员 模型/动作/技能特效 数据
        }
        foreach(var Item in InBattleStart_D.EnemyMemListAtWaveDic.Values)                                                   //  加载敌方方成员 模型/动作/技能特效 数据  
        {
            for(int i = 0; i < Item.Count; i++)
            {
                Item[i].Damage      = 0;
                MemLoadAnimEffect   (Item[i]);
            }
        }
    }
    public void                     ResLoadCompleteHandler          ()                                                      //  资源加载完成 处理程序            
    {
        dispatcher.RemoveListener   (BattleEvent.ResLoadComplete_Event, ResLoadCompleteHandler);                            /// 移除监听 资源下载完成
        _ResLoadTaskList.Clear();
        _TaskNameList.Clear();
        dispatcher.Dispatch         (BattleEvent.BattleDataInit_Event);                                                     /// 1)____战斗数据初始化
    }


    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

    private List<ResLoadTaskData>   _ResLoadTaskList                = new List<ResLoadTaskData>();                          /// 加载任务列表
    private List<string>            _TaskNameList                   = new List<string>();                                   /// 任务名称列表

    private MemAnimEffectData       MemLoadAnimEffect               (IBattleMemberData inMem_D)                             //  成员加载 模型/动作/技能特效 数据 
    {
        int                             TheResID                    = inMem_D.MemberResID;                                  /// 资源ID 
        MemAnimEffectData               TheMemAnimEffec_D           = new MemAnimEffectData();                              /// 成员动画特效数据

        Configs_ActionAndEffectData     TheAnimEffect_C             = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID(TheResID);               /// 动画特效    配置数据 
        Configs_AttackTrailBonePosData  TheAttckTralBone_C          = Configs_AttackTrailBonePos.sInstance.GetAttackTrailBonePosDataByHeroResourceID(TheResID);     /// 攻击轨迹    配置数据
        Configs_SkillBonesMatchData     TheSkillBone_C              = Configs_SkillBonesMatch.sInstance.GetSkillBonesMatchDataByResourceID(TheResID);               /// 技能骨骼    配置数据
        if (TheAnimEffect_C     == null)                            Debug.LogError("AnimEffect配置文件加载错误:" + TheResID);    
        if (TheAttckTralBone_C  == null)                            Debug.LogError("AttackTrail配置文件加载错误:"+ TheResID);
        if (TheSkillBone_C      == null)                            Debug.LogError("SkillBone配置文件加载错误:"  + TheResID);

        if (TheMemAnimEffec_D   != null )                                                                                   /// 1)____设置数据:[TheMemAnimEffec_D] 
        {
            TheMemAnimEffec_D.ResourceID                            = TheResID;                                             /// 资源ID
            TheMemAnimEffec_D.Name                                  = TheAnimEffect_C.HeroModel;                            /// 模型Obj名称
            TheMemAnimEffec_D.AttackTrailBonePos_C                  = TheAttckTralBone_C;                                   /// 攻击轨迹骨骼配置数据
            TheMemAnimEffec_D.SkillBoneMatch_C                      = TheSkillBone_C;                                       /// 技能骨骼配置数据

            TheMemAnimEffec_D.AnimAndEffec_C                        = TheAnimEffect_C;                                      /// 成员动画特效配置数据
        }                                         
        if (inMem_D.TalentSkillID > 0   )                                                                                   /// 2)____成员天赋配置                 
        {
            Configs_PassiveSkillData ThePassSKill_D                 = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID(inMem_D.TalentSkillID);
            if (ThePassSKill_D == null)                             Debug.LogError("PassiveSkill配置文件加载错误:" + inMem_D.TalentSkillID);
            TheMemAnimEffec_D.TalentBuffEffectName                  = ThePassSKill_D.SpecialEffects;
            AddATKEffectTask                                        (ThePassSKill_D.SpecialEffects);
        }
        if (!_TaskNameList.Contains(TheMemAnimEffec_D.Name))                                                                /// 3)____加载成员: 模型,动作,特效;     
        {
            if ( TheAnimEffect_C.HeroModel              != null)                                                            /// 1>____加载模型      
            {
                AssetBundleName         ABType;                                                                             /// 需要加载的资源包
                switch(inMem_D.MemberType)
                {
                    case Battle_MemberType.Hero:                    ABType = AssetBundleName.commonhero;    break;          /// 英雄
                    case Battle_MemberType.Npc_Normal:              ABType = AssetBundleName.npc;           break;          /// NPC
                    case Battle_MemberType.Mercenary:               ABType = AssetBundleName.merc;          break;          /// 佣兵
                    case Battle_MemberType.Npc_CheckPointBoss:      ABType = AssetBundleName.npc;           break;          /// 关卡Boss
                    case Battle_MemberType.Npc_WorldBoss:           ABType = AssetBundleName.npc;           break;          /// 世界Boss
                    default:                                        ABType = AssetBundleName.commonhero;    break;          
                }
                AddModelTask            (TheMemAnimEffec_D, ABType);                                                        /// 加载模型
            }
            if ( TheAnimEffect_C.NormalAttackAciton     != null)                                                            /// 2>____加载动作 [15] 
            {
                                                                                                                                    /// <| 主动攻击动作: 6 >
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.SkillAttack_1,   TheAnimEffect_C.ActiveSkillAction1);                /// 主动技能_1
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.SkillAttack_2,   TheAnimEffect_C.ActiveSkillAction2);                /// 主动技能_2
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.UltAttack,       TheAnimEffect_C.AggressUltimateAttackAction1);      /// 大招攻击
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.UltStorage,      TheAnimEffect_C.StorageUltimateAttackAction);       /// 大招蓄力

                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Sprint,          TheAnimEffect_C.SprintAction);                      /// 冲刺
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.NormalAttack,    TheAnimEffect_C.NormalAttackAciton);                /// 普通攻击 


                                                                                                                                    ///<| 被动受击动作: 4 >
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Aerial,          TheAnimEffect_C.FloatingAction);                    /// 浮空      
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Repel,           TheAnimEffect_C.RepalAction);                       /// 击退
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Death,           TheAnimEffect_C.DeathAciton);                       /// 死亡
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.HardHit,         TheAnimEffect_C.ImpactAction);                      /// 受重击


                                                                                                                                    /// <| 其他功能动作: 5 >
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Entrance,        TheAnimEffect_C.EntranceAction);                    /// 入场      
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Talent,          TheAnimEffect_C.BuffReleaseAction);                 /// 天赋buff
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Standby,         TheAnimEffect_C.StandbyAction);                     /// 站立待机
                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Running,         TheAnimEffect_C.RunningAction);                     /// 奔跑

                TheMemAnimEffec_D.AddAction(BattleMemAnimState.Win,             TheAnimEffect_C.VictorAction);                      /// 胜利动作
            }
            if ( TheAnimEffect_C.NormalAttackEffect_10  != null)                                                            /// 3>____加载特效      
            {
                                                                                                                            /// <| 攻击特效 |>
                AddATKEffectTask        (TheAnimEffect_C.NormalAttackEffect_10);                                            /// 普通攻击特效_1
                AddATKEffectTask        (TheAnimEffect_C.NormalAttackEffect_20);                                            /// 普通攻击特效_2
                AddATKEffectTask        (TheAnimEffect_C.NormalAttackEffect_30);                                            /// 普通攻击特效_3

                AddATKEffectTask        (TheAnimEffect_C.UltimateAttackEffect_00);                                          /// 大招蓄力特效
                AddATKEffectTask        (TheAnimEffect_C.UltimateAttackEffect1_10);                                         /// 大招攻击特效_1
                AddATKEffectTask        (TheAnimEffect_C.UltimateAttackEffect1_20);                                         /// 大招攻击特效_2
                AddATKEffectTask        (TheAnimEffect_C.UltimateAttackEffect1_30);                                         /// 大招攻击特效_3
                
                if (inMem_D.ActiveSkillLv_1 >= 1)                                                                           /// 主动技能_1 特效       
                {
                    AddATKEffectTask(TheAnimEffect_C.ActiveAttackEffect1_10);
                    AddATKEffectTask(TheAnimEffect_C.ActiveAttackEffect1_20);
                    AddATKEffectTask(TheAnimEffect_C.ActiveAttackEffect1_30);
                }
                if (inMem_D.ActiveSkillLv_2 >= 1)                                                                           /// 主动技能_2 特效       
                {
                    AddATKEffectTask(TheAnimEffect_C.ActiveAttackEffect2_10);
                    AddATKEffectTask(TheAnimEffect_C.ActiveAttackEffect2_20);
                    AddATKEffectTask(TheAnimEffect_C.ActiveAttackEffect2_30);
                }
                if (TheSkillBone_C != null)                                                                                 //  技能攻击 骨骼附加特效  
                {
                    if (inMem_D.ActiveSkillLv_1 >= 1)                                                                       //  主动技能_1 骨骼附加特效
                    {
                        AddATKEffectTask (TheSkillBone_C.ActiveAttack1AddEffect1);
                        AddATKEffectTask (TheSkillBone_C.ActiveAttack1AddEffect2);
                        AddATKEffectTask (TheSkillBone_C.ActiveAttack1AddEffect3);
                    }
                    if (inMem_D.ActiveSkillLv_2 >= 2)                                                                       //  主动技能_2 骨骼附加特效
                    {
                        AddATKEffectTask (TheSkillBone_C.ActiveAttack2AddEffect1);
                        AddATKEffectTask (TheSkillBone_C.ActiveAttack2AddEffect2);
                        AddATKEffectTask (TheSkillBone_C.ActiveAttack2AddEffect3);
                    }
                    AddATKEffectTask (TheSkillBone_C.UltimateHoldAddEffects1, 8);                                           /// 大招蓄力 骨骼附加特效
                    AddATKEffectTask(TheSkillBone_C.UltimateHoldAddEffects2, 8);
                    AddATKEffectTask (TheSkillBone_C.UltimateHoldAddEffects3, 8);

                    AddATKEffectTask (TheSkillBone_C.UltimateAttackAddEffects1, 8);                                         /// 大招攻击 骨骼附加特效
                    AddATKEffectTask(TheSkillBone_C.UltimateAttackAddEffects2, 8);
                    AddATKEffectTask (TheSkillBone_C.UltimateAttackAddEffects3, 8);


                }

                Configs_ActiveSkillData TheActieSkill_1_C           = Configs_ActiveSkill.sInstance.GetActiveSkillDataByActiveSkillID(inMem_D.ActiveSkillID_1);
                Configs_ActiveSkillData TheActieSkill_2_C           = Configs_ActiveSkill.sInstance.GetActiveSkillDataByActiveSkillID(inMem_D.ActiveSkillID_2);
                Configs_UltSkillData    TheUltSkill_C               = Configs_UltSkill.sInstance.GetUltSkillDataByUltSkillID(inMem_D.UltSkillID);

                                                                                                                            ///<| Buff特效 |>
                if (TheActieSkill_1_C   != null)                                                                            /// 主动技能_1(BUFF特效)
                {
                    AddATKEffectTask        (TheActieSkill_1_C.Expression1);
                    AddATKEffectTask        (TheActieSkill_1_C.Expression2);
                }
                if (TheActieSkill_2_C   != null)                                                                            /// 主动技能_2(BUFF特效)
                {
                    AddATKEffectTask        (TheActieSkill_2_C.Expression1);
                    AddATKEffectTask        (TheActieSkill_2_C.Expression1);
                }
                if (TheUltSkill_C       != null )                                                                           /// 大招技能  (BUFF特效)  
                {
                    AddATKEffectTask        (TheUltSkill_C.Expression1);
                    AddATKEffectTask        (TheUltSkill_C.Expression2);
                }
                if (inMem_D.BossSkillID != 0)                                                                               /// Boss技能  (BUFF特效)
                {
                    Configs_ActiveSkillData TheBossSkill_C      = Configs_ActiveSkill.sInstance.GetActiveSkillDataByActiveSkillID(inMem_D.BossSkillID);
                    if (TheBossSkill_C != null)
                    {
                        AddATKEffectTask    (TheBossSkill_C.Expression1);
                        AddATKEffectTask    (TheBossSkill_C.Expression1);
                    }

                }


                                                                                                                            ///<| 其他功能特效 |>

                AddATKEffectTask        (TheAnimEffect_C.EntranceActionEffect);                                             /// 入场特效
                AddATKEffectTask        (TheAnimEffect_C.ShowFreeActionEffect);                                             /// 休闲待机
            }
        }

        return                          TheMemAnimEffec_D;                                                                  /// (返回)数据结构
    }
    private void                    AddATKEffectTask                (string inEffcName, float inDurat = 3)                  //  添加特效任务       
    {
        if(inEffcName.Trim() != "0" && inEffcName.Trim().Length > 0 && !_TaskNameList.Contains(inEffcName))
        {
            ResLoadTaskData            TheTask_D                   = new ResLoadTaskData();

            TheTask_D.Name                                          = inEffcName;                                           /// 特效名称
            TheTask_D.DownPath                                      = string.Format(PrefabPath_Effect, inEffcName);         /// 下载路径
            TheTask_D.CallbackObjArr                                = new object[] { inDurat };                             /// 回调参数
            TheTask_D.AB                                            = AssetBundleName.effect;                               /// 资源包类型

            TheTask_D.DownloadComplete                              = EffectDownComplete;                                   /// 下载完成委托

            _TaskNameList.Add(TheTask_D.Name);                                                                              /// 添加 下载任务名称列表
            _ResLoadTaskList.Add(TheTask_D);                                                                                /// 添加 下载任务列数据列表
        }
    }
    private void                    AddModelTask                    (MemAnimEffectData inMem_AE, AssetBundleName inAB)      //  添加模型任务       
    {
        Debug.Log("Model: " + inMem_AE.ResourceID);
        if (inMem_AE.Name.Trim().Length > 0 && inMem_AE.Name.Trim() != "0")
        {
            ResLoadTaskData        TheTaks_D                       = new ResLoadTaskData();

            TheTaks_D.ResID                                         = inMem_AE.ResourceID;                                  /// (Int) 资源ID
            TheTaks_D.Name                                          = inMem_AE.Name;                                        /// (Str) 模型对象名称
            TheTaks_D.DownPath                                      = string.Format(PrefabPath_Model, inMem_AE.Name);       /// (Str) 下载路径
            TheTaks_D.AB                                            = inAB;                                                 /// (enum) 资源包类型 

            TheTaks_D.CallbackObjArr                                = new object[] { inMem_AE };                            /// 回调对象
            TheTaks_D.DownloadComplete                              = ModelDownComplete;                                    /// 下载完成处理

            _ResLoadTaskList.Add    (TheTaks_D);                                                                            /// 下载任务数据 列表
            _TaskNameList.Add       (TheTaks_D.Name);                                                                       /// 任务名称列表

        }   
    }
    private void                    ModelDownComplete               (object[] inObjArr)                                     //  模型下载完成 处理  
    {
        ResLoadTaskData            TheTask_D                       = inObjArr[0] as ResLoadTaskData;

        if (TheTask_D != null && TheTask_D.LoadResult != null)
        {

            GameObject              TheGameObj                      = MonoBehaviour.Instantiate(TheTask_D.LoadResult) as GameObject; 
            TheTask_D.LoadResult                                    = null;

            TheGameObj.transform.localPosition                      = new Vector3(1000,1000,1000);
            TheGameObj.name                                         = TheTask_D.Name;

            Util.AddModelPartEffect (TheGameObj, TheTask_D.ResID, InPlayer.PlayerRoleHero.ID, InPlayer.PlayerRoleHero.WingID);

            TheGameObj.transform.parent                             = BattleControll.sInstance.RootModelListObj.transform;
            TheGameObj.transform.localPosition                      = Vector3.zero;

            MemAnimEffectData       TheMem_AE                       = TheTask_D.CallbackObjArr[0] as MemAnimEffectData;
            TheMem_AE.Model                                         = TheGameObj;

            ResourceKit.sInstance.AddAnimEffect                     (TheMem_AE);
        }
    }
    private void                    EffectDownComplete              (object[] inObjArr)                                     //  加载特效完成 处理   
    {
        ResLoadTaskData            TheTask                         = inObjArr[0] as ResLoadTaskData;

        if(TheTask != null      && TheTask.LoadResult != null       && !string.IsNullOrEmpty(TheTask.LoadResult.name))
        {

            GameObject              TheGameObj                      = MonoBehaviour.Instantiate(TheTask.LoadResult) as GameObject;
            BattleEffect            TheBattle_E                     = new BattleEffect(TheTask.Name, TheGameObj);
            TheTask.LoadResult                                      = null;

            TheGameObj.transform.parent                             = BattleControll.sInstance.RootEffectListObj.transform;

            if (BattleParmConfig.PublicHitStrList.Contains(TheTask.Name))
            {
                foreach (var Item in TheGameObj.GetComponentsInChildren<Transform>(true))
                {
                    Item.gameObject.layer                           = BattleParmConfig.LayerPublicHitEffect;
                }
            }

            float                   TheDurat                        = (float)TheTask.CallbackObjArr[0];
            TheBattle_E.duration                                    = TheDurat;
            ResourceKit.sInstance.AddEffect                         (TheBattle_E);                                          // 添加 战斗特效到 资源库
        }
    }

    #endregion
}

///<FourNode>---------------------------------------------------------------------------------------------------------------/// <summary>  战斗资源加载  </summary>
public class BattleResLoad_Command : EventCommand
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject               contextView                     { set; get; }

    public override void            Execute()                                                                               // 执行初始化    
    {
        Debug.Log                   ("[ Main: BattleResLoad_Command: 战斗资源初始化 ( Not NetSent )");

        if (evt.data != null)                                                                                               // 设置 _ResLoadTaskDataList/_ResLoadTaskResult_D 
        {
            _ResLoadTaskDataList                                    = (List<ResLoadTaskData>)evt.data;                      // 加载任务数据列表
            _ResLoadTaskResult_D.TheTaskDList                       = _ResLoadTaskDataList;                                 // 加载任务结果数据
            if (_ResLoadTaskDataList != null && _ResLoadTaskDataList.Count > 0)                                             // 加载任务结果数据 初始化
            {
                _ResLoadTaskResult_D.SuccessCount                   = 0;
                _ResLoadTaskResult_D.FailureCount                   = 0;
                TaskExec(0);
                return;
            }

        }
        _ResLoadTaskResult_D.SuccessCount                           = 0;
        _ResLoadTaskResult_D.FailureCount                           = 0;
        dispatcher.Dispatch         (BattleEvent.ResLoadComplete_Event, _ResLoadTaskResult_D);                              // 加载完成处理
    }


    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
    ResLoadTaskResultData           _ResLoadTaskResult_D            = new ResLoadTaskResultData();
    List<string>                    _TaskNameList                   = new List<string>();
    List<ResLoadTaskData>           _ResLoadTaskDataList            = new List<ResLoadTaskData>();
    Dictionary<string, Object>      _TaskCompleteDic                = new Dictionary<string, Object>();


    private void                    TaskExec                    ( short inDex)                                              // 执行加载任务      
    {
        if( _ResLoadTaskDataList.Count > inDex)
        { BattleControll.sInstance.TheMono.StartCoroutine(TaskExec(_ResLoadTaskDataList[inDex], inDex)); }     
        else
        {
            _TaskCompleteDic.Clear();
            System.GC.Collect();
            dispatcher.Dispatch(BattleEvent.ResLoadComplete_Event, _ResLoadTaskResult_D);
        }   

    }
    private void                    TaskCompleteHandler         ( ResLoadTaskData inTask_D, Object inResult, short inDex)   // 加载任务完成处理  
    {

        if (inDex  != _ResLoadTaskDataList.Count)
        {
            SceneController.SetLoadProgress((short)( 100 * (inDex+1) / _ResLoadTaskDataList.Count));
            Debuger.Log("________TestData:TaskComplete_Index: " + inDex + "_ResLoadTaskDataList.Count:" + _ResLoadTaskDataList.Count);
        }
        inTask_D.LoadResult         = inResult;
        if (inTask_D.DownloadComplete != null)                      inTask_D.DownloadComplete(inTask_D);
        if (!_TaskCompleteDic.ContainsKey(inTask_D.Name))           _TaskCompleteDic.Add(inTask_D.Name,inResult);
    }
    private IEnumerator             TaskExec                    ( ResLoadTaskData inLoadTask_D, short inDex)                // 执行加载任务      
    {
        if (_TaskCompleteDic.ContainsKey(inLoadTask_D.Name))
        {
            TaskCompleteHandler     (inLoadTask_D, _TaskCompleteDic[inLoadTask_D.Name], inDex);
            _ResLoadTaskResult_D.SuccessCount++;
        }
        else
        {
#if UNITY_ANDROID && !UNITY_EDITOR && !Force_Local                                                                          /// Android模型/非unity编辑模式/!非强制本地
            Object ResultObj = ResourceManager.Instance.LoadAsset<Object>(inLoadTask_D.AB, inLoadTask_D.Name);
#else
            Object                  ResultObj                       = Util.Load(inLoadTask_D.DownPath);
#endif
            if ( ResultObj != null)
            {
//                Debuger.Log("________TestData:ResultObj != null ");
                TaskCompleteHandler (inLoadTask_D, ResultObj, inDex);
                ResultObj                                           = null;
                _ResLoadTaskResult_D.SuccessCount++;
            }
            else
            {
                Debuger.Log("________TestData:ResultObj == null ");
                if (inLoadTask_D.DownPath.ToLower().Contains("rolemodel"))
                {
#if UNITY_EDITOR
                    Debuger.LogError("________TestData:not found Model (未找到模型):" + inLoadTask_D.Name);
#endif
                }
                else
                {
#if UNITY_EDITOR
                    Debuger.Log("________TestData:An error has occurred on downloading \"" + inLoadTask_D.Name + "\", the request has been canceled. ");
#endif
                }
                _ResLoadTaskResult_D.FailureCount++;
            }
        }
        yield return null;
        inDex++;
        TaskExec(inDex);
    }

#endregion
}

public class                        ResLoadTaskData                                                                         //  加载任务数据        
{
    public int                      ResID                       { set; get; }                                               /// 资源ID
    public string                   Name                        { set; get; }                                               /// 任务名称
    public string                   DownPath                    { set; get; }                                               /// 下载路径
    public AssetBundleName          AB                          { set; get; }                                               /// 资源包类型

    public Object                   LoadResult                  { set; get; }                                               /// 下载对象
    public object[]                 CallbackObjArr              { set; get; }                                               /// 回调参数
    public Delegate_ParamObj        DownloadComplete            { set; get; }                                               /// 下载完成委托

    public delegate void            Delegate_ParamObj           ( params object[] inObjArr );                               /// object 参数委托 (void)
}
public class                        ResLoadTaskResultData                                                                   //  加载任务结果 数据    
{
    public int                      SuccessCount                = 0;                                                        /// 成功累计次数
    public int                      FailureCount                = 0;                                                        /// 失败累计次数
    public List<ResLoadTaskData>    TheTaskDList;                                                                           /// 任务数据 列表
}
                                                                                                                                            