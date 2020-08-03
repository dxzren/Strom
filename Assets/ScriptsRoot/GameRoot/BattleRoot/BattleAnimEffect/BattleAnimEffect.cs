using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 成员动画特效数据 </summary>
    public class MemAnimEffectData   
    {
        public int                  ResourceID                      { set; get; }                                           /// 资源ID
        public string               Name                            { set; get; }                                           /// 模型对象名称
        public string               TalentBuffEffectName            = "";                                                   /// 天赋特效名称
        public BattleModelState     ModelState                      = BattleModelState.Ready;                               /// 模型状态
        public BattleMemAnimState   MemAnimState                    = BattleMemAnimState.Standby;                           /// 成员动作播放状态

        public IBattleMemberData    Member_D                        { set; get; }                                           /// 战斗成员数据  实例
        public MemberAction         MemAnim                         { set; get; }                                           /// 成员动作 实例
        public GameObject           Model                                                                                   //  模型实例 
        {
            get { return _Model; }
            set
            {
                _Model              = null;
                if (value != null )
                {
                    _Model          = value;
                    MemAnim         = new MemberAction(_Model); 
                }
            }
        }
        public Configs_ActionAndEffectData                          AnimAndEffec_C       { set; get; }                      /// 动作特效数据        配置数据  

        public Configs_AttackTrailBonePosData                       AttackTrailBonePos_C { set; get; }                      /// 攻击轨迹骨骼发出点  配置数据
        public Configs_SkillBonesMatchData                          SkillBoneMatch_C     { set; get; }                      /// 技能特效骨骼匹配    配置数据


        private GameObject          _Model                          = null;                                                 /// 模型对象     实例
        private List<MemAnimEffectData> BattleAnimEffeList          = new List<MemAnimEffectData>();                        /// 动画数据列表
        private Dictionary<BattleMemAnimState, string> MemAnimDic   = new Dictionary<BattleMemAnimState, string>();         /// 动作字典

        public                      MemAnimEffectData()                                                                     // 构造函数 
        {
            ModelState                                              = BattleModelState.Ready;
            MemAnimState                                            = BattleMemAnimState.Standby;
        }

        public Transform            GetHitBone_TF   (int inBoneType)                                                        // 获取击中骨骼   
        {
            if( inBoneType == 1 )
            {
                Transform           Hit_TF                          = GetBone_TF(AnimAndEffec_C.StrikeBonePos);
                if ( Hit_TF != null )                               return Hit_TF;
            }
            return                                                  Model.transform.parent;
        }
        public Transform            GetBone_TF      (string inBonePath)                                                     // 获取骨骼坐标   
        {
            if ( inBonePath.Length > 0 )
            {
                Transform           Hit_TF                          = Model.transform.FindChild(inBonePath);
                if (Hit_TF != null)                                 return Hit_TF;
                else
                    Debug.LogError  ("骨骼未找到 Model: " + Model.name + "    路径: " + inBonePath);
            }
            return                                                  null;
        }
        public MemAnimEffectData    UseModelData    ()                                                                      // 应用和添加动画数据   
        {
            MemAnimEffectData        TheBattleAnimEffec              = BattleAnimEffeList.Find(p => p.ModelState == BattleModelState.Ready);
            if ( TheBattleAnimEffec == null )
            {
                TheBattleAnimEffec                                  = this.Clone();
                if (TheBattleAnimEffec != null )                    BattleAnimEffeList.Add(TheBattleAnimEffec);
            }
            TheBattleAnimEffec.ModelState                           = BattleModelState.Busy;
            return                                                  TheBattleAnimEffec;
        }
        public MemAnimEffectData    Clone           ()                                                                      // 动画数据副本   
        {
            if (Model != null )
            {
                MemAnimEffectData   BattleAnimEffeClone             = new MemAnimEffectData();                              /// 动画数据实例
                BattleAnimEffeClone.ResourceID                      = ResourceID;                                           /// 资源ID
                BattleAnimEffeClone.Name                            = Name;                                                 /// 名称
                BattleAnimEffeClone.MemAnimDic                      = MemAnimDic;                                           /// 动作字典
                BattleAnimEffeClone.Model                           = MonoBehaviour.Instantiate(Model) as GameObject;       /// 模型对象
                BattleAnimEffeClone.MemAnim                         = new MemberAction(BattleAnimEffeClone.Model);          /// 动作成员数据

                BattleAnimEffeClone.AnimAndEffec_C                  = AnimAndEffec_C;                                       /// 动画配置数据
                BattleAnimEffeClone.AttackTrailBonePos_C            = AttackTrailBonePos_C;                                 /// 攻击轨迹骨骼位置
                BattleAnimEffeClone.SkillBoneMatch_C                = SkillBoneMatch_C;                                     /// 技能特效骨骼匹配

                return                                              BattleAnimEffeClone;
            }
            return null;
        }
        public void                 CleanData       ()                                                                      // 清理动画数据   
        {
            GameObject.Destroy(Model);
            Model                                                   = null;                                                 /// 模型对象为NULL
            BattleAnimEffeList.ForEach                              (p => p.CleanData());                                   /// 清理动画数据列表
            BattleAnimEffeList.Clear();                                                                                     /// 清空数据列表
        }
        public void                 CleanClone      ()                                                                      // 清理副本数据   
        {
            for( int i = BattleAnimEffeList.Count - 1; i >= 0; i-- )
            {
                var TheData = BattleAnimEffeList[i];
                if (TheData.ModelState != BattleModelState.Busy)
                {
                    TheData.CleanData();
                    BattleAnimEffeList.RemoveAt(i);
                }
            }
        }

        #region================================================||   Animation --动作播放                  ||=====================================================
        public void                 AddAction       (BattleMemAnimState inActState,string ActName)                                      // 添加到动作列表    
        {
            if (ActName.Length > 0 && ActName != "0" )
            {
                if ( MemAnimDic.ContainsKey(inActState) == false )
                                    MemAnimDic.Add(inActState, ActName);
                else
                                    MemAnimDic[inActState] = ActName;
            }
        }
        public float                PlayAction      (BattleMemAnimState inActState, bool inIsLoop = false, float inSpeedRate = 1f)      // 播放动作         
        {
            MemAnimState            = inActState;
            if ( MemAnimDic.ContainsKey(inActState))
            {
                return              PlayAction(MemAnimDic[inActState], inIsLoop, inSpeedRate);
            }
            return                  0;
        }
        public float                PlayAnimNoScale (BattleMemAnimState inActState, bool inIsLoop = false, float inSpeedRate = 1f)      // 播放未拉伸动作    
        {
            MemAnimState            = inActState;
            if ( MemAnimDic.ContainsKey(inActState))
            {
                return              PlayAnimNoScale(MemAnimDic[inActState], inIsLoop, inSpeedRate);
            }
            return                  0;
        }
        protected float             PlayAction      (string inActName, bool inIsLoop = false, float inSpeedRate = 1f )                  // 播放动作         
        {
            if ( Model != null && MemAnim != null )
            {
                return              MemAnim.PlayAnim(inActName, inIsLoop, inSpeedRate);
            }
            return                  0;
        }
        protected float             PlayAnimNoScale (string inActName, bool inIsLoop = false, float inSpeedRate = 1f )                  // 播发未拉伸动作    
        {
            if ( Model != null && MemAnim != null )
            {
                return              MemAnim.PlayAnimNoScale(inActName, inIsLoop, inSpeedRate);
            }
            return                  0;
        }


        public float                Talent()                                                                                            // 天赋动作     
        {
            return 0;
            //return PlayAction(BattleMemAnimState.Talent, false);
        }
        public float                Standby(bool inIgnoreTime = false )                                                                 // 站立待机动作 
        {
            if (inIgnoreTime)       return PlayAnimNoScale(BattleMemAnimState.Standby,true);
            else                    return PlayAction(BattleMemAnimState.Standby,true);
        }
        public float                Runing()                                                                                            // 奔跑动作     
        {
            return PlayAction(BattleMemAnimState.Running, true);
        }
        public float                NormalAttactk()                                                                                     // 普通攻击     
        {
            return PlayAction(BattleMemAnimState.NormalAttack);
        }
        public float                Entrance()                                                                                          // 入场动作     
        {
            return PlayAnimNoScale(BattleMemAnimState.Entrance);
        }
        public float                SkillAttack_1()                                                                                     // 技能攻击_1   
        {
            return PlayAction(BattleMemAnimState.SkillAttack_1);
        }
        public float                SkillAttack_2()                                                                                     // 技能攻击_2   
        {
            return PlayAction(BattleMemAnimState.SkillAttack_2);    
        }
        public float                UltStorage(bool inIsNoScale = true,float inSpeed = 1f)                                              // 大招蓄力     
        {
            if (inIsNoScale)        return PlayAnimNoScale(BattleMemAnimState.UltStorage,false,inSpeed);                    /// 播放未拉伸动作
            else                    return PlayAction(BattleMemAnimState.UltStorage,false,inSpeed);                         /// 播放动作
        }
        public float                UltAttack()                                                                                         // 大招攻击     
        {
            return                  PlayAction(BattleMemAnimState.UltAttack);
        }
        public float                HardHit()                                                                                           // 重击击中     
        {
            return PlayAction(BattleMemAnimState.HardHit);
        }
        public float                NormalHit()                                                                                         // 普通击中     
        {
            return PlayAction(BattleMemAnimState.NormalHit);
        }
        public float                Aerial()                                                                                            // 浮空动作     
        {
            return PlayAction(BattleMemAnimState.Aerial);
        }
        public float                Sprint ( bool inIsNoScale = true,float inSpeed = 1)                                                 // 冲刺动作     
        {
            if (inIsNoScale)        return PlayAnimNoScale(BattleMemAnimState.Sprint, false,inSpeed);
            else                    return PlayAction(BattleMemAnimState.Sprint,false,inSpeed);
        }
        public float                Death()                                                                                             // 死亡动作     
        {
            return PlayAction(BattleMemAnimState.Death);
        }
        public float                Sink()                                                                                              // 播放角色死亡后的沉默效果     
        {
            TweenPosition           Sink                            = Model.GetComponent<TweenPosition>();
            if(Sink != null )
            {
                MonoBehaviour.Destroy                               (Sink);
                Sink                                                = null;
            }
                                                            
            Sink                                                    = Model.AddComponent<TweenPosition>();
            Sink.duration                                           = 0.8f;
            Vector3                 Former                          = Model.transform.localPosition;                                            /// 位移坐标

            Sink.from               = Former;
            Sink.to                 = new Vector3(Former.x, Former.y - BattleParmConfig.GetMemberModelSize(Member_D.MemberType), Former.z);
            Sink.ignoreTimeScale    = false;                                                                                                    /// 忽略时间刻度
            Sink.onFinished.Add     (new EventDelegate(() => { Model.transform.localPosition = Former; }));                                     /// 委托
            return                  Sink.duration - 0.2f;
        }
        public float                Repel()                                                                                             // 击退动作     
        {
            return PlayAction(BattleMemAnimState.Repel);
        }
        public float                Victory()                                                                                           // 胜利动作     
        {
            return PlayAction(BattleMemAnimState.Win);
        }

        #endregion
        #region================================================||   Effect    --特效播放                  ||=====================================================
        private BattleEffect        RunEffect                       = null;                                                 /// 跑步特效
        
        public bool                 PlayAttackNormalFirst_E()                                                               // 普通攻击前段 特效 (攻击方)     
        {   
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.NormalAttackEffect_10);
            if (TheBattle_E != null)                                return TheBattle_E.PlayEffect(Model.transform.parent,Vector3.zero,false);       /// 播放前段特效
            else                                                    return false;
        }
        public float                PlayAttackNormalSeconde_E       ( MemAnimEffectData inTarget_AE )                       // 普通攻击中段 特效 (击中目标方) 
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.NormalAttackEffect_20);         /// 播放中段特效           
            if (TheBattle_E == null)                                return 0;
            Transform               Attack_TF                       = GetBone_TF(AttackTrailBonePos_C.NormalTrailBones2);                            /// 攻击骨骼 对象
            Transform               Target_TF                       = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.NormalAttackBoneType);                /// 击中目标Bone 对象
            Vector3                 FromPos                         = Model.transform.parent.position;                                               /// 攻击轨迹 发起坐标

            if (Attack_TF != null)  FromPos         = Attack_TF.TransformPoint( BattleParmConfig.ListToVector3(AttackTrailBonePos_C.NormalTrailBonesPos2));     /// 攻击轨迹 发起坐标
            float                   TheSpeed        = AnimAndEffec_C.NormalAttackTrail == 2 ? BattleParmConfig.ParabolaSpeed : BattleParmConfig.ShootSpeed;     /// 抛线轨迹 or 直线轨迹 
            float                   TheTime         = Vector3.Distance(Target_TF.position, FromPos) / TheSpeed;                                                 /// 距离/速度 = 飞行时间

            MemberEffect            TheMem_E        = TheBattle_E.GetMemEffect();                                                                               /// 成员特效
            TheMem_E.PlayEffect     (FromPos, Model.transform.rotation, Model.transform.lossyScale, TheTime);                                                   /// 播放特效
            TheMem_E.EffectObj.transform.parent     = inTarget_AE.Model.transform.parent;                                                                       /// 成员特效父级

            Vector3                 ToPos           = inTarget_AE.Model.transform.parent.InverseTransformPoint(Target_TF.transform.position);                   /// 攻击轨迹 到达坐标
            TheMem_E.EffectObj.transform.forward    = (Target_TF.transform.position - TheMem_E.EffectObj.transform.position ).normalized;                       /// 世界坐标 蓝轴(z)

            if ( AnimAndEffec_C.NormalAttackTrail == 2 )                                                                                                         /// 普通轨迹类型_2 ( 抛物线 )
            {
                ITrialMove            TheMovable                      = ParabolaTrialMove.BuildMovable();                                                              /// 抛物线移动 数据类型
                TheMovable.MoveTo   (TheMem_E.EffectObj, ToPos, TheTime);                                                                                       /// 移动对象
            }
            else                                                                                                                                                /// 普通轨迹类型_2 ( 抛物线 )                                                                                       /// 直线移动   
            {
                ITrialMove            TheMovable                      = StraightTrialMove.BuildMovable();                                                              /// 直线移动 数据类型     
                TheMovable.MoveTo   (TheMem_E.EffectObj, ToPos, TheTime);                                                                                       /// 移动对象                                                                                          
            }
            return                  TheTime;
        }
        public bool                 PlayAttackNormalHit_E           ( MemAnimEffectData inTarget_AE)                        // 普通攻击击中 特效 (击中目标方) 
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.NormalAttackEffect_30);
            if (TheBattle_E != null)return TheBattle_E.PlayEffect(inTarget_AE.GetHitBone_TF(AnimAndEffec_C.NormalAttackBoneType), Vector3.zero);
            else                    return false;
        }

        public bool                 PlayUltStorage_E                (bool inIsNoScale = true, float inSpeed = 1)            // 大招技能蓄力 特效       
        {
            bool                    TheResult                       = PlayBone_E(AnimAndEffec_C.UltimateAttackEffect_00, "", true, inSpeed);
            if ( SkillBoneMatch_C != null)
            {
                TheResult           = PlayBone_E(SkillBoneMatch_C.UltimateHoldAddEffects1,SkillBoneMatch_C.UltimateHoldBones1,true,inSpeed) || TheResult;
                TheResult           = PlayBone_E(SkillBoneMatch_C.UltimateHoldAddEffects2,SkillBoneMatch_C.UltimateHoldBones2,true,inSpeed) || TheResult;
                TheResult           = PlayBone_E(SkillBoneMatch_C.UltimateHoldAddEffects3,SkillBoneMatch_C.UltimateHoldBones3,true,inSpeed) || TheResult;
            }
            return                  TheResult;
        }
        public bool                 PlayUltFirst_E()                                                                        // 大招技能 攻击一段 特效  
        {
            bool                    TheResult                       = PlayBone_E(AnimAndEffec_C.UltimateAttackEffect1_10);

            if ( SkillBoneMatch_C != null)
            {
                TheResult           = PlayBone_E(SkillBoneMatch_C.UltimateAttackAddEffects1, SkillBoneMatch_C.UltimateAttackBones1) || TheResult;
                TheResult           = PlayBone_E(SkillBoneMatch_C.UltimateAttackAddEffects2, SkillBoneMatch_C.UltimateAttackBones2) || TheResult;
                TheResult           = PlayBone_E(SkillBoneMatch_C.UltimateAttackAddEffects3, SkillBoneMatch_C.UltimateAttackBones3) || TheResult;
            }
            return                  TheResult;
        }
        public float                PlayUltSecond_E                 ( MemAnimEffectData inTarget_AE)                        // 大招技能 攻击二段 特效  
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.UltimateAttackEffect1_20);     /// 战斗特效
            if ( TheBattle_E == null )                              return 0;

            Transform               TheTargetTF                     = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.UltimateAttackBoneType);             /// 击中目标 TF
            Transform               AttackTF                        = GetBone_TF (AttackTrailBonePos_C.UltimateTrailBones);                         /// 攻击方 TF
            Vector3                 FromPos                         = Model.transform.parent.position;                                              /// 攻击轨迹 开始位置

            if ( AttackTF != null)  FromPos     = AttackTF.TransformPoint (BattleParmConfig.ListToVector3(AttackTrailBonePos_C.UltimateTrailBonesPos)); /// 攻方TF转换 轨迹起点
            float                   TheSpeed    = AnimAndEffec_C.NormalAttackTrail == 2 ? BattleParmConfig.ParabolaSpeed : BattleParmConfig.ShootSpeed;  /// 攻击速度 <直线,抛物线>
            float                   TheTime     = Vector3.Distance (AttackTF.position, FromPos) / TheSpeed;                                             /// 持续时间

            MemberEffect            TheMem_E                        = TheBattle_E.GetMemEffect();                                                   /// 获取成员特效
            TheMem_E.PlayEffect                                     (FromPos, Model.transform.rotation, Model.transform.lossyScale, TheTime);       /// 播放特效
            TheMem_E.EffectObj.transform.parent                     = inTarget_AE.Model.transform.parent;                                           /// 设置成员对象父级

            Vector3                 ToPos        = inTarget_AE.Model.transform.parent.InverseTransformPoint (TheTargetTF.transform.position);       /// 攻击轨迹 终点
            TheMem_E.EffectObj.transform.forward = (TheTargetTF.transform.position - TheMem_E.EffectObj.transform.position).normalized;

            if (AnimAndEffec_C.UltimateEffectTrail1 == 2)                                                                                            /// 抛物线轨迹
            {
                ITrialMove           TheMovable                      = ParabolaTrialMove.BuildMovable();
                TheMovable.MoveTo   (TheMem_E.EffectObj, ToPos, TheTime);
            }
            else                                                                                                                                    /// 直线轨迹
            {
                ITrialMove           TheMovable                      = StraightTrialMove.BuildMovable();
                TheMovable.MoveTo   (TheMem_E.EffectObj, ToPos, TheTime);
            }
            return                  TheTime;
        }
        public bool                 PlayUltHitSingle_E              ( MemAnimEffectData inTarget_AE)                        // 大招技能 击中特效 (单体)      
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.UltimateAttackEffect1_30);
            Transform               TheTargetTF                     = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.UltimateAttackBoneType);

            if ( TheBattle_E != null)                               return TheBattle_E.PlayEffect(TheTargetTF,Vector3.zero);
            else                                                    return false;
        }
        public bool                 PlayUltHitList_E                ( List<IBattleMemMediator> inTargetList)                // 大招技能 击中特效 (成员列表)  
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.UltimateAttackEffect1_30);
            if ( TheBattle_E != null)                               return TheBattle_E.PlayEffect(inTargetList);
            else                                                    return false;
        }

        public bool                 PlaySkill_1First_E ()                                                                   // 主动技能_1 攻击一段    
        {
            bool                    TheResult                       = PlayBone_E(AnimAndEffec_C.ActiveAttackEffect1_10);
            if (SkillBoneMatch_C != null)
            {
                TheResult = PlayBone_E(SkillBoneMatch_C.ActiveAttack1AddEffect1, SkillBoneMatch_C.ActiveAttack1Bones1) || TheResult;
                TheResult = PlayBone_E(SkillBoneMatch_C.ActiveAttack1AddEffect2, SkillBoneMatch_C.ActiveAttack1Bones2) || TheResult;
                TheResult = PlayBone_E(SkillBoneMatch_C.ActiveAttack1AddEffect3, SkillBoneMatch_C.ActiveAttack1Bones3) || TheResult;
            }
            return                  TheResult;
        }       
        public float                PlaySkill_1Second_E             ( MemAnimEffectData inTarget_AE)                        // 主动技能_1 攻击二段    
        {
            BattleEffect            TheBattle_E     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.ActiveAttackEffect1_20);                                    /// 获取配置特效
            if (TheBattle_E == null)                return 0;
            if (inTarget_AE == null)                return 0;

            Transform               Attack_TF       = GetBone_TF(AttackTrailBonePos_C.ActiveTrailBones1);                                                       /// 攻击开始起点_TF
            Transform               Target_TF       = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.ActiveAttackBoneType1);                                          /// 击中目标终点_TF
            Vector3                 TheFromPos      = Model.transform.parent.position;                                                                          /// 攻击轨迹起点世界坐标
            if (Attack_TF != null)  TheFromPos      = Attack_TF.TransformPoint(BattleParmConfig.ListToVector3(AttackTrailBonePos_C.ActiveTrailBonesPos1));      /// 局部骨骼坐标 转换世界坐标

            float                   TheSpeed        = AnimAndEffec_C.NormalAttackTrail == 2 ? BattleParmConfig.ParabolaSpeed : BattleParmConfig.ShootSpeed;     /// 技能特效 飞行速度
            float                   TheTime         = Vector3.Distance (Target_TF.position, TheFromPos) / TheSpeed;                                             /// 技能特效 飞行时间

            MemberEffect            TheMem_E        = TheBattle_E.GetMemEffect();                                                                               /// 成员特效
            TheMem_E.PlayEffect     (TheFromPos, Model.transform.rotation, Model.transform.localScale, TheTime);                                                /// 成员播发技能特效
            TheMem_E.EffectObj.transform.parent     = inTarget_AE.Model.transform.parent;                                                                       /// 设置特效对象父级

            Vector3                 ToPos           = inTarget_AE.Model.transform.parent.InverseTransformPoint(Target_TF.transform.position);                   /// 攻击轨迹终点 TF (世界坐标转成局部坐标)
            TheMem_E.EffectObj.transform.forward    = (Target_TF.transform.position - TheMem_E.EffectObj.transform.position).normalized;                        /// 蓝轴 (z)

            if( AnimAndEffec_C.ActiveEffectTrail1 == 2)                                                                                                         /// 特效移动到 目标位置<抛物线> 
            {
                ITrialMove            TheIMovable     = ParabolaTrialMove.BuildMovable();
                TheIMovable.MoveTo  ( TheMem_E.EffectObj, ToPos, TheTime);
            }
            else                                                                                                                                                /// 特效移动到 目标位置<直线>  
            {
                ITrialMove            TheIMovable     = StraightTrialMove.BuildMovable();
                TheIMovable.MoveTo  ( TheMem_E.EffectObj, ToPos, TheTime);
            }
            return                  TheTime;                                                                                                                    /// 返回持续时间
        }
        public bool                 PlaySkill_1Hit_E                ( MemAnimEffectData inTarget_AE )                       // 主动技能_1 击中特效    
        {
            BattleEffect            TheBattle_E     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.ActiveAttackEffect1_30);                                    /// 获取配置特效
            Transform               Target_TF       = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.ActiveAttackBoneType1);                                           /// 击中目标 TF

            if ( TheBattle_E != null)               return TheBattle_E.PlayEffect(Target_TF, Vector3.zero);                                                     /// 播发击中特效
            else                                    return false;

        }
        public bool                 PlaySkill_1Hit_E                ( List<IBattleMemMediator> inTargetList)                // 主动技能_1 击中特效(重载)
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.ActiveAttackEffect1_30);
            if ( TheBattle_E != null)                               return TheBattle_E.PlayEffect(inTargetList);
            else                                                    return false;
        }

        public bool                 PlaySkill_2First_E ()                                                                   // 主动技能_2 攻击一段    
        {
            bool                    TheResult                       = PlayBone_E(AnimAndEffec_C.ActiveAttackEffect2_10);
            if (SkillBoneMatch_C != null)
            {
                TheResult = PlayBone_E(SkillBoneMatch_C.ActiveAttack2AddEffect1, SkillBoneMatch_C.ActiveAttack2Bones1) || TheResult;
                TheResult = PlayBone_E(SkillBoneMatch_C.ActiveAttack2AddEffect2, SkillBoneMatch_C.ActiveAttack2Bones2) || TheResult;
                TheResult = PlayBone_E(SkillBoneMatch_C.ActiveAttack2AddEffect3, SkillBoneMatch_C.ActiveAttack2Bones3) || TheResult;
            }
            return                  TheResult;
        }       
        public float                PlaySkill_2Second_E             ( MemAnimEffectData inTarget_AE)                        // 主动技能_2 攻击二段    
        {
            BattleEffect            TheBattle_E     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.ActiveAttackEffect2_20);                                    /// 获取配置特效
            if (TheBattle_E == null)                return 0;
            if (inTarget_AE == null)                return 0;

            Transform               Attack_TF       = GetBone_TF(AttackTrailBonePos_C.ActiveTrailBones2);                                                       /// 攻击开始起点_TF
            Transform               Target_TF       = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.ActiveAttackBoneType2);                                          /// 击中目标终点_TF
            Vector3                 TheFromPos      = Model.transform.parent.position;                                                                          /// 攻击轨迹起点世界坐标
            if (Attack_TF != null)  TheFromPos      = Attack_TF.TransformPoint(BattleParmConfig.ListToVector3(AttackTrailBonePos_C.ActiveTrailBonesPos2));      /// 局部骨骼坐标 转换世界坐标

            float                   TheSpeed        = AnimAndEffec_C.NormalAttackTrail == 2 ? BattleParmConfig.ParabolaSpeed : BattleParmConfig.ShootSpeed;      /// 技能特效 飞行速度
            float                   TheTime         = Vector3.Distance (Target_TF.position, TheFromPos) / TheSpeed;                                             /// 技能特效 飞行时间

            MemberEffect            TheMem_E        = TheBattle_E.GetMemEffect();                                                                               /// 成员特效
            TheMem_E.PlayEffect     (TheFromPos, Model.transform.rotation, Model.transform.localScale, TheTime);                                                /// 成员播发技能特效
            TheMem_E.EffectObj.transform.parent     = inTarget_AE.Model.transform.parent;                                                                       /// 设置特效对象父级

            Vector3                 ToPos           = inTarget_AE.Model.transform.parent.InverseTransformPoint(Target_TF.transform.position);                   /// 攻击轨迹终点 TF (世界坐标转成局部坐标)
            TheMem_E.EffectObj.transform.forward    = (Target_TF.transform.position - TheMem_E.EffectObj.transform.position).normalized;                        /// 蓝轴 (z)

            if( AnimAndEffec_C.ActiveEffectTrail2 == 2)                                                                                                         /// 特效移动到 目标位置<抛物线> 
            {
                ITrialMove            TheIMovable     = ParabolaTrialMove.BuildMovable();
                TheIMovable.MoveTo  ( TheMem_E.EffectObj, ToPos, TheTime);
            }
            else                                                                                                                                                /// 特效移动到 目标位置<直线>  
            {
                ITrialMove            TheIMovable     = StraightTrialMove.BuildMovable();
                TheIMovable.MoveTo  ( TheMem_E.EffectObj, ToPos, TheTime);
            }
            return                  TheTime;                                                                                                                    /// 返回持续时间
        }
        public bool                 PlaySkill_2Hit_E                ( MemAnimEffectData inTarget_AE )                       // 主动技能_2 击中特效    
        {
            BattleEffect            TheBattle_E     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.ActiveAttackEffect2_30);                                   /// 获取配置特效
            Transform               Target_TF       = inTarget_AE.GetHitBone_TF(AnimAndEffec_C.ActiveAttackBoneType1);                                          /// 击中目标 TF

            if ( TheBattle_E != null)               return TheBattle_E.PlayEffect(Target_TF, Vector3.zero);                                                     /// 播发击中特效
            else                                    return false;

        }
        public bool                 PlaySkill_2Hit_E                ( List<IBattleMemMediator> inTargetList)                // 主动技能_2 击中特效(重载)
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(AnimAndEffec_C.ActiveAttackEffect2_30);
            if ( TheBattle_E != null)                               return TheBattle_E.PlayEffect(inTargetList);
            else                                                    return false;
        }


        public float                PlayThunder_E                   ( string  inEffeName, string inBonePath,                // 闪电链特效
                                                                      Vector3 inAmendPos, MemAnimEffectData inTarget_AE)     
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(inEffeName);          /// 获取特效
            
            Transform               Attack_TF                       = GetBone_TF(inBonePath);                               /// 攻击方_Bone_TF
            Vector3                 FromPos                         = Model.transform.position;                             /// 起点位置
            FromPos                 = Attack_TF.position + inAmendPos;                                                      /// 修正后 攻击起点

            Transform               Target_TF                       = inTarget_AE.GetHitBone_TF(1);                         /// 击中目标 Bone_TF
            float                   TheTime                         = Vector3.Distance( Target_TF.position, FromPos) / BattleParmConfig.ThunderSpeed;
            MemberEffect            TheMem_E                        = TheBattle_E.GetMemEffect();
            TheMem_E.PlayEffect     (Attack_TF, inAmendPos, TheTime + BattleParmConfig.ThunderHoldTime);                    /// 攻击方播放闪电链特效

            ThunderTrialMove        TheMoveThunder                  = new ThunderTrialMove();
            TheMoveThunder.Move     (TheMem_E.EffectObj, Attack_TF, Target_TF, TheTime);                                    /// 特效移动

            return                  TheTime;                                                                                /// 返回持续时间
        }
        public MemberEffect         PlayRunEffect(float inDurat)                                                            // 播放跑步特效            
        {
            if ( AnimAndEffec_C.IsRunningEffect == 0)                return null;

            Vector3                 OffSet                          = Vector3.zero;
            Vector3                 Rotate                          = Vector3.zero;

            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(BattleResStrName.RunningEffect);
            if ( TheBattle_E == null )                              return null;

            MemberEffect            TheMem_E                        = TheBattle_E.GetMemEffect();
            if (TheMem_E == null )                                  return null;

            TheMem_E.PlayEffect     (Model.transform.parent, OffSet,Rotate,inDurat);
            return                  TheMem_E;

        }
        public MemberEffect         PlayBuff_E                      ( string  inEffeName,   int inBonePos = 0)              // Buff 特效              
        {
            string                  TheBonePath                     = "";                                                   /// 骨骼
            Vector3                 AmendPos                        = Vector3.zero;                                         /// 修正 偏移位置
            Vector3                 AmendRotate                     = Vector3.zero;                                         /// 修正 旋转
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(inEffeName);          /// 获取特效

            if      (TheBattle_E == null)                           return null;
            if      ( inBonePos == 1 )
            {
                Configs_BuffBindBoneData TheBuffBind_C      = Configs_BuffBindBone.sInstance.GetBuffBindBoneDataByResourceID(ResourceID);
                if  ( TheBuffBind_C != null)
                {
                    TheBonePath     = TheBuffBind_C.BuffBondBone1;                                                          /// Buff绑定骨骼点
                    AmendPos        = BattleParmConfig.ListToVector3 ( TheBuffBind_C.BuffRelatedPos1);                      /// 相对偏移位置
                    AmendRotate     = BattleParmConfig.ListToVector3 ( TheBuffBind_C.BuffRelatedRotate1);                   /// 相对旋转修正
                }
            }
            else if ( inBonePos == 2 )
            {
                Configs_BuffBindBoneData TheBuffBind_C      = Configs_BuffBindBone.sInstance.GetBuffBindBoneDataByResourceID(ResourceID);
                if ( TheBuffBind_C != null)
                {
                    TheBonePath     = TheBuffBind_C.BuffBondBone2;                                                          /// Buff绑定骨骼点
                    AmendPos        = BattleParmConfig.ListToVector3 ( TheBuffBind_C.BuffRelatedPos2);                      /// 相对偏移位置
                    AmendPos        = BattleParmConfig.ListToVector3 ( TheBuffBind_C.BuffRelatedRotate2);                   /// 相对旋转修正
                }
            }

            float                   TheDurat                        = (float)BattleParmConfig.BattleMaxTime.TotalSeconds * 2;
            MemberEffect            TheMem_E                        = TheBattle_E.GetMemEffect();

            if ( TheBonePath.Length > 0 && TheBonePath != "0" )
            {
                Transform           TheTF                           = GetBone_TF(TheBonePath);
                if ( TheTF != null )
                {
                    TheMem_E.PlayEffect ( TheTF, AmendPos, AmendRotate,TheDurat );
                    return          TheMem_E;
                }
            }

            TheMem_E.PlayEffect(Model.transform.parent, AmendPos, AmendRotate, TheDurat);
            return                  TheMem_E;
        }

        public bool                 PlayTimer_E                     ( string inEffeName,    float inTimer,                  // 计时 特效
                                                                      bool   inIgnoreTime = false, float inSpeed = 1 )       
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(inEffeName);          /// 获取特效
            TheBattle_E.duration                                    = inTimer;                                              /// 持续时间
            return                  TheBattle_E.PlayEffect( Model.transform.parent, Vector3.zero, inIgnoreTime, inSpeed );  /// 播放成员特效 
        }
        public bool                 PlayBoneTimer_E                 ( string inEffeName,     float inTimer,                 // 骨骼计时 特效
                                                                      string inBonePath = "", bool inIgnoretime = false, float inSpeed = 1) 
        {
            BattleEffect            TheBattle_E                     = ResourceKit.sInstance.GetEffect(inEffeName);
            Transform               TargetTF                        = GetBone_TF(inBonePath);
            TargetTF                                                = Model.transform.parent;
            TheBattle_E.duration                                    = inTimer;
            return                  TheBattle_E.PlayEffect          ( TargetTF, Vector3.zero, inIgnoretime,inSpeed );
        }
        public bool                 PlayBone_E                      ( string inEffeName,  string inBonePath = "",           // 骨骼节点上播放特效
                                                                      bool   inIsNoScale = false, float inSpeed = 1)        
        {
            if (inEffeName.Length > 0)
            {
                BattleEffect        TheBattle_E                     = ResourceKit.sInstance.GetEffect(inEffeName);
                Transform           TheTF                           = GetBone_TF(inBonePath);
                if (TheTF == null )                                 TheTF = Model.transform.parent;
                if (TheBattle_E != null )                           return TheBattle_E.PlayEffect(TheTF, Vector3.zero, inIsNoScale, inSpeed);
            }
            return                  false;
        }

        #endregion

    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  战斗特效         </summary>
    public class BattleEffect       
    {
        public float                duration                        { set; get; }                                           // 持续时间
        public string               Name                            { set; get; }                                           // 对象名称
        public MemberEffect         MemEffect                       { set; get; }                                           // 成员特效实例

        private List<MemberEffect>  MemEffeList                     = new List<MemberEffect>();                             // 成员特效列表

        public BattleEffect(string inName,GameObject inEffeObj,float inDurat)                                               // 构造函数     
        {
            Name                    = inName;
            MemEffect               = new MemberEffect(inName, inEffeObj);
            duration                = inDurat;
        }
        public BattleEffect(string inName,GameObject inEffeObj)                                                             // 构造函数     
        {
            Name = inName;
            MemEffect = new MemberEffect(inName, inEffeObj);
        }

        public bool                 PlayEffect(List<IBattleMemMediator> inMemAct_D, bool inIgnoreTime = false, float inSpeedRate = 1)                   // 播放特效      
        {
            int                     count                           = 0;
            Vector3                 ThePos                          = Vector3.zero;

            if (inMemAct_D.Count < 1)                               return false;            
            Battle_Camp             TheCamp                         = inMemAct_D[0].Camp;

            foreach (var Item in inMemAct_D)
            {
                if (Item.Hp > 0)
                {
                    ThePos          += Item.MemPos_D.NowPosV3;
                    count++;
                }
            }

            if (count < 1)                                          return false;
            ThePos                                                  = ThePos / inMemAct_D.Count;
            MemberEffect            TheMemEffc                      = GetMemEffect();

            if (TheMemEffc != null )
            {
                Quaternion          TheRotate                       = (TheCamp == Battle_Camp.Enemy ? Quaternion.identity : Quaternion.Euler(0, 180, 0));
                bool                TheIsPlayEffct                  = TheMemEffc.PlayEffect(ThePos, TheRotate, Vector3.zero, duration, inSpeedRate, inIgnoreTime);
                TheMemEffc.EffectObj.transform.parent               = BattleControll.sInstance.Root3DMainObj.transform;
                return                                              TheIsPlayEffct;
            }
            return                                                  false;
        }
        public bool                 PlayEffect(Transform inParent, Vector3 inOffSet, bool inIgnoreTime = false, float inSpeedRate = 1)                  // 播放特效_重载 
        {
            MemberEffect            TheMemEffc                      = GetMemEffect();
            if ( TheMemEffc != null )                               return TheMemEffc.PlayEffect(inParent,inOffSet,duration,inSpeedRate,inIgnoreTime);
            else                                                    return false;
        }
        public bool                 PlayEffect(Vector3 inPos, Quaternion inRotate, Vector3 inScale, bool inIgnoreTime = false, float inSpeedRate = 1)   // 播放特效_重载 
        {
            MemberEffect            TheMemEffc                      = GetMemEffect();
            if ( TheMemEffc != null )                               return TheMemEffc.PlayEffect(inPos,inRotate,inScale,duration,inSpeedRate,inIgnoreTime);
            else                                                    return false;
        }
        public MemberEffect         GetMemEffect()                                                                          // 获取成员特效数据  
        {
            MemberEffect            TheMemEffc                      = MemEffeList.Find(P => P.EffectState == BattleModelState.Ready);
            if (TheMemEffc == null )
            {
                TheMemEffc          = MemEffect.Clone();             
                if (TheMemEffc != null)                             MemEffeList.Add(TheMemEffc);   
            }               
            return                  TheMemEffc;
        }
        public void                 CleanData()                                                                             // 清理特效数据      
        {
            if (MemEffect != null )                                 MemEffect.DestEffect();
            MemEffeList.ForEach(P => P.DestEffect());
            MemEffeList.Clear();
        }
        public void                 CleanCloneData()                                                                        // 清理特效数据副本  
        {
            for (int i = MemEffeList.Count -1; i >= 0; i--)
            {
                var                 Item                            = MemEffeList[i];
                if (Item.EffectState != BattleModelState.Busy)
                {
                    Item.DestEffect();
                    MemEffeList.RemoveAt(i);
                }
            }
        }
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  成员动作         </summary>
    public class MemberAction       
    {
        public Animation            _MemAnim;                                                                               // 成员动作实例
        private GameObject          Model;                                                                                  // 成员模型对象实例

        public                      MemberAction (GameObject inModel)                                                       // 构造函数 <Object> 
        {
            Model                   = inModel;
            _MemAnim                = Model.GetComponent<Animation>();
            {
                if (_MemAnim == null )
                {
                    _MemAnim        = Model.AddComponent<Animation>();
                }
            }
        }

        public float                PlayAnim (string inAnimName,bool inIsLoop = false, float inSpeedRate = 1f)              // 播放动作          
        {
            AnimationState          AnimState                       = _MemAnim[inAnimName];                                 /// 混合动画实例
            if (AnimState == null )
            {
                Debug.LogError("动作" + inAnimName + "未找到");
                return 0;
            }
            AnimState.speed         *= inSpeedRate;                                                                         /// 动画速度
            AnimState.wrapMode      = inIsLoop ? WrapMode.Loop : WrapMode.Default;                                          /// 播放模式
            _MemAnim.CrossFade      (inAnimName, 0.1f);                                                                     /// 混合过渡
            return                  AnimState.length / inSpeedRate;                                                         /// 播放时间 (动画长度/播放速率)
        }
        public float                PlayAnimNoScale(string inAnimName, bool inIsLoop = false, float inSpeedRate = 1)        // 播放未拉伸动作    
        {
            AnimationState          AnimState                       = _MemAnim[inAnimName];                                 /// 混合动画实例
            if (AnimState == null )
            {
                Debug.LogError("动作" + inAnimName + "未找到");
                return 0;
            }
            AnimState.speed         *= inSpeedRate;                                                                         /// 动画速度
            AnimState.wrapMode      = inIsLoop ? WrapMode.Loop : WrapMode.Default;                                          /// 播放模式

            NoScaleAnim             NoScaleAnim = Model.GetComponent<NoScaleAnim>();
            if (NoScaleAnim != null) MonoBehaviour.Destroy(NoScaleAnim);

            NoScaleAnim             = Model.AddComponent<NoScaleAnim>();
            NoScaleAnim.actionName  = inAnimName;
            NoScaleAnim.Model       = Model;
            NoScaleAnim.speedRate   = inSpeedRate;

            _MemAnim.CrossFade      (inAnimName, 0.1f);                                                                     /// 混合过渡
            return                  AnimState.length / inSpeedRate;                                                         /// 播放时间 (动画长度/播放速率)
        }

        public float                GetDuringTime  (string inAnimName)                                                      // 获取间隔时间      
        {
            AnimationState          TempAnimState                   = _MemAnim[inAnimName];
            if ( TempAnimState == null )
            {
                Debug.LogError      ("动作" + inAnimName + "未找到");
                return 0;
            }
            return                  TempAnimState.length;
        }
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  成员特效         </summary>
    public class MemberEffect       
    {
        string Name;                                                                                                        // 对象名称
        public GameObject           EffectObj                       { set; get; }                                           // 特效GameObject
        public BattleModelState     EffectState                     = BattleModelState.Ready;                               // 特效状态

        public MemberEffect         (string inName,GameObject inEffeObj)                                                    // 参数构造函数   
        {
            if ( inEffeObj != null)                                 inEffeObj.name = inName;
            Name                    = inName;
            EffectObj               = inEffeObj;
        }

        public void                 DestEffect()                                                                            // 销毁特效       
        {
            EffectState                                             = BattleModelState.Clean;
            if (EffectObj != null)                                  MonoBehaviour.Destroy(EffectObj);
            EffectObj               = null;
        }
        public MemberEffect         Clone()                                                                                 // 成员特效 副本  
        {
            if(EffectObj != null )
            {
                return new MemberEffect(Name, MonoBehaviour.Instantiate(EffectObj) as GameObject);
            }
            return null;
        }

        bool                        PlayEffectNoScale(float inDuration,float inSpeedRate = 1f)
        {
            EffectState             = BattleModelState.Busy;
            if (EffectObj != null )
            {
                EffectObj.SetActive(false);
                EffectObj.SetActive(true);

                NoScaleEffect                                       TheNoScalEffe = EffectObj.GetComponent<NoScaleEffect>();
                if (TheNoScalEffe == null)                          TheNoScalEffe = EffectObj.AddComponent<NoScaleEffect>();
                TheNoScalEffe.TimeOutCallBack = () =>
                {
                    EffectObj.transform.parent                      = BattleControll.sInstance.RootEffectListObj.transform;
                    EffectState                                     = BattleModelState.Ready;
                    EffectObj.SetActive(false);
                };

                TheNoScalEffe.EffectObj                             = EffectObj;
                TheNoScalEffe.duration                              = inDuration;
                TheNoScalEffe.speedRate                             = inSpeedRate;
                TheNoScalEffe.Begin();
                return true;
            }
            return false;
        }
        bool                        PlayEffect(float duration)                                                                          // 播放特效 
        {
            EffectState = BattleModelState.Busy;

            if (EffectObj != null)
            {
                EffectObj.SetActive(false);
                EffectObj.SetActive(true);

                TimeEndHandler                                      TheTimeEndHandler = EffectObj.GetComponent<TimeEndHandler>();
                if (TheTimeEndHandler == null)                      TheTimeEndHandler = EffectObj.AddComponent<TimeEndHandler>();
                TheTimeEndHandler.TimeEndToDo                       = TimeEndOperation.Close;
                TheTimeEndHandler.TimeEndCallBack = () =>
                {
                    EffectObj.transform.parent                         = BattleControll.sInstance.RootEffectListObj.transform;
                    EffectState                                     = BattleModelState.Ready;
                };
                TheTimeEndHandler.duration                          = duration;
                TheTimeEndHandler.Run();

                return true;
            }
            return false;
        }
        public bool                 PlayEffect( Transform inParent,       Vector3    inOffSet,       float    inDurat = 3,              // 父级对象下播放
                                                float     inSpeedRate= 1, bool       inIgnoreTime = false)
        {
            if (EffectObj != null)
            {
                EffectObj.transform.parent = inParent.transform;
                return PlayEffect(inOffSet, Quaternion.identity, Vector3.one, inDurat, inSpeedRate, inIgnoreTime);
            }
            return false;
        }
        public bool                 PlayEffect( Transform inParent,       Vector3    inOffset,        Vector3 inRotate,                 // 父级对象下播放
                                               float      inDurat = 3,    float      inSpeedRate = 1, bool    inIgnoreTime = false)
        {
            if (EffectObj != null)
            {
                EffectObj.transform.parent = inParent.transform;
                return PlayEffect(inOffset, inRotate,Vector3.one, inDurat, inSpeedRate, inIgnoreTime);
            }
            return false;
        }

        public bool                 PlayEffect( Vector3   inPos,          Vector3    inRotate,        Vector3 inScale,                  // 播放特效,默认完毕后销毁
                                                float     inDurat = 3,    float      inSpeedRate =1,  bool    inIgnoreTime = false)
        {
            if (EffectObj != null )
            {
                EffectObj.transform.localPosition                      = inPos;
                EffectObj.transform.localEulerAngles                   = inRotate;
                EffectObj.transform.localScale                         = inScale;

                if (inIgnoreTime)
                {
                    PlayEffectNoScale(inDurat, inSpeedRate);
                }
                else
                {
                    PlayEffect(inDurat);
                }
                return true;
            }
            return false;
        }

        public bool                 PlayEffect( Vector3   inPos,          Quaternion inRotate,        Vector3 inScale,                  // 播放特效,默认完毕后销毁
                                                float     inDurat = 3,    float      inSpeedRate = 1, bool    inIgnoreTime = false)
        {
            if (EffectObj != null )
            {
                EffectObj.transform.localPosition                      = inPos;
                EffectObj.transform.localRotation                      = inRotate;
                EffectObj.transform.localScale                         = inScale;

                if (inIgnoreTime)                                   PlayEffectNoScale(inDurat,inSpeedRate);
                else                                                PlayEffect(inDurat);
                return true;
            }
            return false;
        }

    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  未拉伸动作       </summary>
    public class NoScaleAnim        : MonoBehaviour  
    {
        public GameObject           Model;
        public string               actionName                      = "";                                                   /// 动画名称
        public float                speedRate                       = 1f;                                                   /// 动画速率

        private bool                isComplete                      = false;                                                /// 是否完成
        private float               lastFrameTime                   = 0;                                                    /// 最后一帧时间
        private float               progressTime                    = 0;                                                    /// 进度时间
        private Animation           TheAnim;                                                                                /// 动画实例
        private AnimationState      TheAnimState;                                                                           /// 混合动画实例

        private void Start()
        {
            if (Model != null)
            {
                TheAnim             = Model.GetComponent<Animation>();                                                      /// 动画实例

                if (TheAnim != null )   TheAnimState                = TheAnim[actionName];                                  /// 当前动画状态
                TheAnim.Play        (actionName);                                                                           /// 播放动作

                lastFrameTime       = Time.realtimeSinceStartup;                                                            /// 当前时间
                progressTime        = 0;                                                                                    /// 进度时间
                isComplete          = false;                                                                                /// 播放未完成
            }
        }
        private void Update()
        {
            if (!isComplete && Model != null && TheAnim != null && TheAnimState != null )
            {
                float curTime       = Time.realtimeSinceStartup;                                                            /// 当前时间
                float deltaTime     = curTime - lastFrameTime;                                                              /// 当前帧与上一帧的间隔
                lastFrameTime       = curTime;                                                                              /// 记录当前帧
                progressTime        += deltaTime * speedRate;                                                               /// 东湖已经播放时间

                TheAnimState.normalizedTime = Mathf.Min(progressTime / TheAnimState.length, 1);                             /// 动画规范化时间[0-1]
                
                if( progressTime >= TheAnimState.length )
                {
                    isComplete      = true;                                                                                 /// 完成
                    Complete();                                                                                             /// 摧毁当前动作
                }                
            }
            else
            {
                if (Time.timeScale != BattleParmConfig.TimeScaleZero)
                    Debug.LogWarning("Time.timeScale= " + Time.timeScale);                                                  /// 当前动画不停止(时间尺度)
                else
                    Debug.LogWarning("模型或动作不存在,ActionName=" + actionName);
                isComplete          = true;                                                                                 /// 动画完成
                Complete();                                                                                                 /// 摧毁当前动作
            }
        }
        private void Complete()                                                                                             // 动作完成摧毁当前动作
        {
            Destroy(this);
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  未拉伸特效       </summary>
    public class NoScaleEffect      : MonoBehaviour 
    {
        public float                duration                        = 3;                                                    // 持续时间
        public float                speedRate                       = 1f;                                                   // 速率

        public Action               TimeOutCallBack;                                                                        // 时间到回调 
        public GameObject           EffectObj;                                                                              // 特效对象


        private float               beginTime                       = 0;                                                    // 开始时间
        private float               lastTime                        = 0;                                                    // 结束的时间
        private ParticleSystem[]    PartSysArr;                                                                             // 粒子系统数组

        private void Start()
        {
            if (EffectObj != null)  PartSysArr  = EffectObj.GetComponentsInChildren<ParticleSystem>(true);
        }

        public void Begin()
        {
            beginTime               = Time.unscaledTime;                                                                    // 游戏开始后 开始帧 戳
            lastTime                = beginTime;                                                                            // 结束时间
        }

        private void Update()
        {
            if ( beginTime > 0 )
            {
                float               dura                            = (Time.unscaledTime - beginTime) * speedRate;          // 持续时间
                float               timeAlfa                        = (Time.unscaledTime - lastTime) * speedRate;           // 阿尔法时间
                lastTime                                            = Time.unscaledTime;                                    // 结束时间

                if (PartSysArr != null)
                {
                    foreach(var Item in PartSysArr)
                    {
                        Item.Simulate(timeAlfa, false, false);                                                              // 模拟
                    }
                }
                if (dura >= duration)
                {
                    if (TimeOutCallBack != null)                    TimeOutCallBack();
                }
            }
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  特效播放完处理   </summary>
    public class TimeEndHandler     : MonoBehaviour 
    {
        public float                duration                        = 3;                                                    // 持续时间
        public TimeEndOperation     TimeEndToDo                     = TimeEndOperation.Close;                               // 播放完毕操作
        public Action               TimeEndCallBack;                                                                        // 播放完毕回调

        private void Start()        
        {
            if ( duration <= 0)
            {
                duration            = 3;
            }
        }

        public void Run()                                                                                                   // 激活特效 
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            Invoke("CloseObject", duration);
        }
        void CloseObject()                                                                                                  // 关闭特效 
        {
            if ( TimeEndToDo == TimeEndOperation.Close)
            {
                Destroy             (this.gameObject);
            }
            else if (TimeEndToDo == TimeEndOperation.Close )
            {
                this.gameObject.SetActive(false);
            }
            if (TimeEndCallBack != null)                            TimeEndCallBack();
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  资源库           </summary>
    public class ResourceKit        
    {
        public                          ResourceKit() { }                                                                   // 构造函数
        public static ResourceKit       sInstance                                                                           // 公用实例 
        {
            get
            {
                if (_sInstance == null)                             _sInstance = new ResourceKit();
                return              _sInstance;
            }
        }


        public BattleEffect             GetEffect           (string inEffeName)                                             // Get 特效数据         
        {
            if (_EffectKitDic.ContainsKey(inEffeName))                  return _EffectKitDic[inEffeName];
            return                                                      null;
        }

        public bool                     IsFindEffect        (string inEffecName)                                            // Find 特效数据        
        {
            return                      _EffectKitDic.ContainsKey(inEffecName);
        }
        public bool                     IsFindAnimEffect    (int inResID)                                                   // Find 动画特效数据    
        {
            return                      _AnimEffectKitDic.ContainsKey(inResID);
        }
        public MemAnimEffectData        GetAnimEffect       (int inResID)                                                   // Get 动画特效数据      
        {
            if (_AnimEffectKitDic.ContainsKey(inResID))                 return _AnimEffectKitDic[inResID];
            return null;
        }
        public void                     AddAnimEffect       (MemAnimEffectData inAnimE)                                     // ADD 动画特效数据     
        {
            if (_AnimEffectKitDic.ContainsKey(inAnimE.ResourceID))      _AnimEffectKitDic[inAnimE.ResourceID] = inAnimE;    // 有则 替换
            else                                                        _AnimEffectKitDic.Add(inAnimE.ResourceID,inAnimE);  // 无贼 添加
        }

        public void                     AddEffect           (BattleEffect inBattleE)                                        // ADD 特效数据         
        {
            if (_EffectKitDic.ContainsKey(inBattleE.Name))              _EffectKitDic[inBattleE.Name]       = inBattleE;
            else                                                        _EffectKitDic.Add(inBattleE.Name,inBattleE);
        }

        public void                     CleanData()                                                                         // 清理数据             
        {
            foreach ( var Item in _AnimEffectKitDic.Values)
            {
                Item.CleanData();
            }
            _AnimEffectKitDic.Clear();
            foreach (var Item in _EffectKitDic.Values )
            {
                Item.CleanData();
            }
            _EffectKitDic.Clear();
            GC.Collect();
        }
        public void                     CleanCloneData()                                                                    // 清理副本数据          
        {
            foreach (var Item in _AnimEffectKitDic.Values)
            {
                Item.CleanClone();
            }
            foreach (var Item in _EffectKitDic.Values)
            {
                Item.CleanCloneData();
            }
            GC.Collect();
        }
        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private static ResourceKit                      _sInstance;                                                         // 私有实例
        private Dictionary<int, MemAnimEffectData>      _AnimEffectKitDic   = new Dictionary<int, MemAnimEffectData>();     // 动画特效字典
        private Dictionary<string, BattleEffect>        _EffectKitDic       = new Dictionary<string, BattleEffect>();       // 特效字典




        #endregion
    }

    public enum BattleModelState                                                                                            // 模型状态         
    {
        Ready                       = 0,                            /// 可以使用
        Busy                        = 1,                            /// 不能使用
        Clean                       = 2,                            /// 清理
    }
    public enum TimeEndOperation                                                                                            // 特效播放完毕操作  
    {
        NoOperation                 = 0,                                                                                    // 无操作
        Close                       = 1,                                                                                    // 关闭
        Destroy                     = 2,                                                                                    // 销毁
    }

}

