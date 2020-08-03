using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LinqTools;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  正常攻击(非位移) </summary>
    public class NormalAttackItem : MonoBehaviour 
    {
        public IBattleMemMediator       Owner { get; set; }                                                                 /// 本体(功方)

        public void                     AttackStart ()                                                                      //  攻击初始化      
        {
            _IsContinue                 = true;
            SetDefender();                                                                                                  /// 设置防御阵容
            StartCoroutine              (WaitForAttack());
        }
        public void                     SetDefender ()                                                                      //  守方成员设置    
        {
            DefenderList                                            = new List<IBattleMemMediator>();
            IBattleMemMediator          TheDefendMem                = Owner.EnemyTeam.GetAttackRangeSingle(Owner);
            if (TheDefendMem == null)                                Owner.MemAnimEffect.Standby();
            else
            {
                Owner.AttackMove.MoveTarget                         = new MoveToMember(TheDefendMem);
                DefenderList.Add        (TheDefendMem);
            }                                      
        }
        public void                     StopAction  ()                                                                      //  停止行动        
        {
            StopCoroutine(WaitForAttack());                                                                                 //  停止协程( 等待进攻 )
            StopCoroutine(NormalAttackStart());                                                                             //  停止协程( 普攻攻击开始 )
            StopAllCoroutines();                                                                                            //  停止所有协程
            _IsContinue                 = false;                                                                            //  停止
            DefenderList                = new List<IBattleMemMediator>();                                                   //  清空守方成员列表
            if ( Owner.AttackMove.MoveTarget != null )
            {    Owner.AttackMove.MoveTarget.RemoveTarget();            }
        }
        public void                     AttackOver  ()                                                                      //  攻击结束        
        {
            if (_IsContinue)
            {
                _IsContinue             = false;
                Owner.AttackOver();
            }
        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private bool                    _IsContinue                     = false;                                            /// 是否继续
        private List<IBattleMemMediator>DefenderList                    = new List<IBattleMemMediator>();                   /// 守方阵容 列表
        IEnumerator WaitForAttack       ( )                                                                                 //  等待攻击       
        {
            if (_IsContinue)
            {
                SetDefender();
                if (DefenderList.Count > 0)
                {
                    if (Owner.AttackMove.IsArrive)                                                                          // 到达目标
                    {
                        if ( _IsContinue )                              StartCoroutine(NormalAttackStart());                // 普通攻击开始
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.03f);
                        if (_IsContinue)                                StartCoroutine(WaitForAttack());                    // 等待攻击
                    }
                }
            }
        }
        IEnumerator                     NormalAttackStart   ( )                                                             //  普通攻击开始    
        {
            if (_IsContinue && Owner.MemState == BattleMemState.Normal )
            {
                if (Owner.MemBuff.IsContains(BufferState.Cleave))                                                           // (分裂)多重攻击;普通攻击多个目标 
                {
                    List<IBattleMemMediator> TheMemList = AttackTarget.GetAttackTargetList(Owner,BattleResStrName.DefendRandomThree );     /// 设置目标列表
                    TheMemList.Remove(DefenderList[0]);                                                                     /// 移除第一个初始成员
                    TheMemList.AddRange(TheMemList);                                                                        /// 添加成员列表
                }

                float                   WaitTime                        = 0;                                                //  等待时间
                float                   TheTime                         = Owner.MemAnimEffect.NormalAttactk();              //  普通攻击动作 返回时间
                Owner.MemAnimEffect.PlayAttackNormalFirst_E();                                                              //  普通攻击特效 第一段

                int                     TotalTimes      = Owner.MemAnimEffect.AnimAndEffec_C.NormalKeys.Count;              //  普攻关键帧数量
                int                     NowTimes        = 0;                                                                //  当前关键帧帧数量

                foreach ( var Item in Owner.MemAnimEffect.AnimAndEffec_C.NormalKeys )                                        
                {
                    float               KeyWaitTime     = TheTime;                                                          //  当前帧时间
                    if ( Owner.MemAnimEffect.AnimAndEffec_C.NormalFrame > 0 )                                               /// 普攻帧数
                    {    KeyWaitTime    = TheTime * Item * 1.0f / Owner.MemAnimEffect.AnimAndEffec_C.NormalFrame; }         //  动作时间 * 关键帧 / 普攻总帧数
                    KeyWaitTime         = Mathf.Max(0.001f, KeyWaitTime - WaitTime);
                    WaitTime            += KeyWaitTime;

                    yield return new WaitForSeconds(KeyWaitTime);
                    NowTimes++;                     
                    if (_IsContinue && Owner.MemState == BattleMemState.Normal )
                    {
                        if ( Owner.MemAnimEffect.AnimAndEffec_C.NormalAttackTrail == 3)                                     /// 闪电链
                        {    BattleControll.sInstance.TheMono.StartCoroutine(HitThunder(Owner, NowTimes, TotalTimes)); }
                        else
                        {
                            for( int i = 0; i < DefenderList.Count;  i++ )
                            {
                                if ( DefenderList[i].MemState == BattleMemState.Normal && DefenderList[i] != null)          /// 受击轨迹
                                {    BattleControll.sInstance.TheMono.StartCoroutine(HitTrail(DefenderList[i], NowTimes, TotalTimes)); }
                            }
                        }
                    }
                }
                if (_IsContinue)
                {
                    if (TheTime > WaitTime )            yield return new WaitForSeconds(TheTime - WaitTime);
                    if ( _IsContinue && Owner.MemState == BattleMemState.Normal )
                    {
                        Owner.DefendSolve.ComputeAttackerEnergy(DefenderList);                                              // 攻方 怒气结算
                        AttackOver();                                                                                       // 攻击结束
                    }
                }
            }
        }
        IEnumerator                     HitThunder  ( IBattleMemMediator inAttacker, int inNowTimes, int inTotalTimes)      //  连锁闪电击中    
        {
            IBattleMemMediator          TheAttacker         = inAttacker;
            string                      FromBone            = TheAttacker.MemAnimEffect.AttackTrailBonePos_C.NormalTrailBones2;
            Vector3                     FromOffSet          = BattleParmConfig.ListToVector3(TheAttacker.MemAnimEffect.AttackTrailBonePos_C.NormalTrailBonesPos2);

            foreach ( var Item in DefenderList )
            {
                float TrailTime         = TheAttacker.MemAnimEffect.PlayThunder_E(inAttacker.MemAnimEffect.AnimAndEffec_C.NormalAttackEffect_20, FromBone, FromOffSet ,Item.MemAnimEffect);

                TheAttacker             = Item;
                FromBone                = Item.MemAnimEffect.AnimAndEffec_C.StrikeBonePos;
                FromOffSet              = Vector3.zero;

                yield return new WaitForSeconds(TrailTime);
                Hit(Item, inNowTimes, inTotalTimes);
                yield return new WaitForSeconds(BattleParmConfig.ThunderHoldTime);
            }
        }
        IEnumerator                     HitTrail    ( IBattleMemMediator inDefender, int inNowTimes, int inTotalTimes)      //  击中轨迹        
        {
            if ( Owner.MemState == BattleMemState.Normal && inDefender.MemState == BattleMemState.Normal
                 && Owner.MemPos_D.XPos != X_Axis.X_03 && Owner.MemPos_D.XPos != X_Axis.X_04 )
            {
                float                   TheTime             = Owner.MemAnimEffect.PlayAttackNormalSeconde_E(inDefender.MemAnimEffect);
                if (TheTime > 0 )                           yield return new WaitForSeconds(TheTime);
            }
            Hit(inDefender, inNowTimes, inTotalTimes);
        }
        private void                    Hit         ( IBattleMemMediator inDefender, int inNowTimes, int inTotalTimes)      //  击中            
        {
            if (inDefender.MemState == BattleMemState.Normal)
            {
                if (!inDefender.DefendSolve.GetDodge(Owner))                                                                //  闪避结算(失败)
                {
                    Owner.MemAnimEffect.PlayAttackNormalHit_E(inDefender.MemAnimEffect);                                    //  播放击中特效
                    inDefender.ColorManger.ToBeHitColor(Owner);                                                             //  击中颜色
                    AttackDamageData    TheDamage_D         = Owner.DefendSolve.GetNorAttackDamage(inDefender);             //  守方普攻伤害结算数据
                    int                 DamageAdd           = BattleControll.sInstance.ComboPoint_UI.CombineNum * 5;        //  连击增加的伤害
                    TheDamage_D.Damage                      += DamageAdd;                                                   //  增加连击伤害
                    if (inNowTimes < inTotalTimes )         TheDamage_D.Damage = TheDamage_D.Damage / inTotalTimes;         //  多段伤害
                    else                TheDamage_D.Damage  = TheDamage_D.Damage / inTotalTimes + TheDamage_D.Damage % inTotalTimes;

                    TheDamage_D         = inDefender.DefendSolve.ComputeDefencerDamage(Owner, TheDamage_D);                 ///  <! 守方伤害结算 !>
                    Owner.DefendSolve.ComputeNorAttackSuckBlood(inDefender, TheDamage_D.Damage);                            //  吸血结算

                    if ( TheDamage_D.Damage > 0)                                                                            //  击中影响        
                    {
                        if ( inDefender.Camp == Battle_Camp.Enemy)    BattleControll.sInstance.CombineTiemsAdd();
                        if ( inDefender.Hp > 0) inDefender.Hit();                                                           //  击中影响
                    }
                }
            }
        }
        #endregion
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  主动技能         </summary>
    public class ActiveSkillItem  : MonoBehaviour 
    {
        public byte                     ActiveSkillType                 = 1;                                                // ( 1:主动技能_1, 2:主动技能_2 )   
        public ActiveSkillData          ActSkill_D                      { set; get; }                                       // 主动技能
        public IBattleMemMediator       Owner                           { set;get; }                                        // 成员本体(攻方)
            
        public void                     AttackStart()                                                                       // 攻击初始化    
        {
            _IsContinue                 = true;

            if (ActSkill_D == null || BattleControll.sInstance.BattleState != BattleState.Fighting)                           // IF非战斗状态_结束攻击
            {   AttackOver();           return;  }

            DefenderList                = AttackTarget.GetAttackTargetList(Owner, ActSkill_D.AttackRange );                 /// 守方成员列表
            if (DefenderList.Count < 1) AttackOver();                                                                       // 守方队列为 0
            else                                                                                                            // 前排战士<近战攻击技能> 
            {
                if ((Owner.MemPos_D.XPos == X_Axis.X_03 || Owner.MemPos_D.XPos == X_Axis.X_04)
                    && DefenderList[0].Camp                             != Owner.Camp 
                    && Owner.Hero_C.Profession2                         == 1)
                {   StartCoroutine(WaitForAttack());                                       }
                else                    StartCoroutine(SkillAttackStart());
            }
        }
        public void                     SetTarget (int inCount = 3)                                                         // 移动目标位置  
        {
            IBattleMemMediator          TheMem                          = null;
            for ( int i = DefenderList.Count - 1; i >= 0; i-- )
            {
                IBattleMemMediator      TempMem                         = DefenderList[i];
                if      ( TempMem == null || TempMem.Hp <= 0)           DefenderList.RemoveAt(i);
                else if ( TheMem == null || Vector3.Distance ( TheMem.MemPos_D.NowPosV3, Owner.MemPos_D.NowPosV3) < 
                          Vector3.Distance (TheMem.MemPos_D.NowPosV3, Owner.MemPos_D.NowPosV3))
                { TheMem                = TempMem;                                                   }
            }

            if ( TheMem == null )
            {
                DefenderList            = AttackTarget.GetAttackTargetList(Owner, ActSkill_D.AttackRange);
                if (inCount > 0 && DefenderList.Count > 0)
                {
                    inCount--;                                                                                              // 三次重新计算目标，三次均无目标，则攻击结束
                    SetTarget(inCount);
                    return;
                }
                else                    AttackOver();                                                                       // 攻击结束
            }
            Owner.AttackMove.MoveTarget = new MoveToMember(TheMem);                                                         // 移动到对象
        }
        public void                     StopAction()                                                                        // 停止行动      
        {
            StopCoroutine(WaitForAttack());                                                                                 /// 停止协程 方法
            StopCoroutine(SkillAttackStart());                                                                              /// 停止协程 方法
            StopAllCoroutines();                                                                                            /// 停止所有协程 方法
            _IsContinue                             = false;                                                                
            DefenderList                            = new List<IBattleMemMediator>();                                       /// 初始化列表
        }
        public void                     AttackOver()                                                                        // 结束攻击      
        {
            if (_IsContinue)
            {
                _IsContinue                                             = false;
                Owner.AttackOver();                 
            }
        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private bool                    _IsContinue                     = false;                                                        // 是否继续 (false:中断)
        private List<IBattleMemMediator> DefenderList                   = new List<IBattleMemMediator>();                               // 守方成员列表
        IEnumerator                     WaitForAttack()                                                                                 // 等待攻击      
        {
            if (_IsContinue)
            {
                SetTarget();
                if ( DefenderList.Count > 0)
                {
                    if (Owner.AttackMove.IsArrive)                      StartCoroutine(SkillAttackStart());
                    else
                    {
                        yield return new WaitForSeconds(0.1f);
                        StartCoroutine(WaitForAttack());
                    }
                }
            }
        }

        IEnumerator                     SkillAttackStart()                                                                              // 技能攻击初始化 
        {
            if (_IsContinue && Owner.MemState == BattleMemState.Normal )
            {
                float                   WaitTime                        = 0;
                float                   TheTime                         = 0;

                if      ( ActiveSkillType == 1)                                                                             // 主动技能 1   
                {
                    TheTime                                                 = Owner.MemAnimEffect.SkillAttack_1();                      /// 主动技能1 时间
                    Owner.MemAnimEffect.PlaySkill_1First_E();
                    if (Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillKey1.Count > 0 )                                                  // 
                    {
                        int             TotalTimes                      = Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillKey1.Count;     /// 关键帧总数
                        int             NowTimes                        = 0;                                                            /// 当前关键数量 
                        foreach ( var Item in Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillKey1 )
                        {
                            float       KeyTime                         = TheTime;                                                      /// 帧数时间
                            if ( Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillFrame1 > 0)               
                            {    KeyTime    = TheTime * KeyTime * 1.0f / Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillFrame1;  }   

                            KeyTime     = Mathf.Max(0.001f, KeyTime - WaitTime);                                                        /// 关键帧时间
                            WaitTime    += KeyTime;                                                                                     /// 等待时间 + 技能时间

                            yield return new WaitForSeconds(KeyTime);
                            NowTimes++;
                            if ( _IsContinue && Owner.MemState == BattleMemState.Normal)
                            {
                                if ( Owner.MemAnimEffect.AnimAndEffec_C.ActiveEffectTrail1 == 3 )
                                {    BattleControll.sInstance.TheMono.StartCoroutine( HitThunder(Owner, NowTimes, TotalTimes)); }
                                else
                                {
                                    int     Index                               = 0;
                                    for (int i = 0; i < DefenderList.Count; i++)
                                    {
                                        if ( DefenderList[i] != null && DefenderList[i].MemState == BattleMemState.Normal )
                                        {    BattleControll.sInstance.TheMono.StartCoroutine(HitTrail(Index, DefenderList[i],NowTimes, TotalTimes)); }
                                        Index++;
                                    }
                                }
                            }
                        }
                    }
                }
                else if ( ActiveSkillType == 2)                                                                             // 主动技能 2   
                {
                    TheTime                                             = Owner.MemAnimEffect.SkillAttack_2();                          /// 主动技能1 时间
                    Owner.MemAnimEffect.PlaySkill_2First_E();
                    if (Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillKey2.Count > 0)                                                   // 
                    {
                        int             TotalTimes                      = Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillKey2.Count;     /// 关键帧总数
                        int             NowTimes                        = 0;                                                            /// 当前关键数量 
                        foreach (var Item in Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillKey2)
                        {
                            float       KeyTime                         = TheTime;                                                      /// 帧数时间
                            if (Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillFrame2 > 0)
                            { KeyTime   = TheTime * KeyTime * 1.0f / Owner.MemAnimEffect.AnimAndEffec_C.ActiveSkillFrame2; }

                            KeyTime     = Mathf.Max(0.001f, KeyTime - WaitTime);                                                        /// 关键帧时间
                            WaitTime    += KeyTime;                                                                                     /// 等待时间 + 技能时间

                            yield return new WaitForSeconds(KeyTime);
                            NowTimes++;
                            if (_IsContinue && Owner.MemState == BattleMemState.Normal)
                            {
                                if (Owner.MemAnimEffect.AnimAndEffec_C.ActiveEffectTrail2 == 3)
                                { BattleControll.sInstance.TheMono.StartCoroutine(HitThunder(Owner, NowTimes, TotalTimes)); }
                                else
                                {
                                    int Index                           = 0;
                                    for (int i = 0; i < DefenderList.Count; i++)
                                    {
                                        if (DefenderList[i] != null && DefenderList[i].MemState == BattleMemState.Normal)
                                        { BattleControll.sInstance.TheMono.StartCoroutine(HitTrail(Index, DefenderList[i], NowTimes, TotalTimes)); }
                                        Index++;
                                    }
                                }
                            }
                        }
                    }
                }

                if ( _IsContinue )      
                {
                    if (TheTime > WaitTime)                             yield return  new WaitForSeconds(TheTime - WaitTime);
                    if (_IsContinue && Owner.MemState == BattleMemState.Normal )
                    {
                        Owner.DefendSolve.ComputeAttackerEnergy(DefenderList);
                        AttackOver();
                    }
                }
            }
        }

        IEnumerator                     HitThunder  ( IBattleMemMediator inAttacker, int inNowTimes,int inToatlTimes)                   // 闪电链击中    
        {
            int                         Index           = 0;
            string                      FromBone        = "";
            IBattleMemMediator          TheMem          = inAttacker;
            Vector3                     FromPosV3       = Vector3.zero;
            if      ( ActiveSkillType == 1)                                                                                 // 主动技能 1   
            {
                FromBone                                = inAttacker.MemAnimEffect.AttackTrailBonePos_C.ActiveTrailBones1;
                FromPosV3                               = BattleParmConfig.ListToVector3(TheMem.MemAnimEffect.AttackTrailBonePos_C.ActiveTrailBonesPos1);
                foreach (var Item in DefenderList )
                {
                    float               TrailTime       = TheMem.MemAnimEffect.PlayThunder_E                                ///
                                                          (inAttacker.MemAnimEffect.AnimAndEffec_C.ActiveAttackEffect1_20, FromBone, FromPosV3, Item.MemAnimEffect);
                    TheMem                              = Item;                                                             ///
                    FromBone                            = Item.MemAnimEffect.AnimAndEffec_C.StrikeBonePos;                  ///
                    FromPosV3                           = Vector3.zero;                                                     ///

                    yield return new WaitForSeconds(TrailTime);                                                             /// 等待时间( 飞行轨迹时间 )
                    Hit( Index, Item, inNowTimes, inToatlTimes);                                                            /// 击中
                    yield return new WaitForSeconds(BattleParmConfig.ThunderHoldTime);                                      /// 等待时间( 闪电保持时间 )
                    Index++;
                }
            }
            else if ( ActiveSkillType == 2)                                                                                 // 主动技能 2   
            {
                FromBone                                = inAttacker.MemAnimEffect.AttackTrailBonePos_C.ActiveTrailBones2;
                FromPosV3                               = BattleParmConfig.ListToVector3(TheMem.MemAnimEffect.AttackTrailBonePos_C.ActiveTrailBonesPos2);
                foreach (var Item in DefenderList )
                {
                    float               TrailTime       = TheMem.MemAnimEffect.PlayThunder_E
                                                          (inAttacker.MemAnimEffect.AnimAndEffec_C.ActiveAttackEffect2_20, FromBone, FromPosV3, Item.MemAnimEffect);
                    TheMem                              = Item;
                    FromBone                            = Item.MemAnimEffect.AnimAndEffec_C.StrikeBonePos;
                    FromPosV3                           = Vector3.zero;

                    yield return new WaitForSeconds(TrailTime);                                                                 /// 等待时间( 飞行轨迹时间 )
                    Hit( Index, Item, inNowTimes, inToatlTimes);                                                                /// 击中
                    yield return new WaitForSeconds(BattleParmConfig.ThunderHoldTime);                                          /// 等待时间( 闪电保持时间 )
                    Index++;
                }
            }

        }

        IEnumerator                     HitTrail    ( int inDefendex, IBattleMemMediator inDefender, int inNowTimes, int inTotalTimes)  // 普通轨迹击中   
        {
            if (   Owner.MemState                   == BattleMemState.Normal 
                && inDefender.MemState              == BattleMemState.Normal
                && Owner.MemPos_D.XPos  != X_Axis.X_03 && Owner.MemPos_D.XPos != X_Axis.X_04)
            {
                float                   TheTime     = 0;                                                                    /// 技能 二段时间
                TheTime = (ActiveSkillType == 1) ?  Owner.MemAnimEffect.PlaySkill_1Second_E(inDefender.MemAnimEffect):
                                                    Owner.MemAnimEffect.PlaySkill_2Second_E(inDefender.MemAnimEffect);
                if ( TheTime > 0)                   yield return new WaitForSeconds(TheTime);                               /// 等待时间 
            }
            Hit( inDefendex, inDefender, inNowTimes, inTotalTimes);
        }

        private void                    Hit         ( int inDefendx, IBattleMemMediator inDefender, int inNowTimes, int inTotalTimes)   // 击中          
        {
            if (inDefender.MemState == BattleMemState.Normal)
            {
                if ( ActiveSkillType == 1)                                                                                  // 主动技能 1   
                {
                    if (Owner.MemAnimEffect.AnimAndEffec_C.ActiveEffectRange1 == 1)                                          /// 单体
                    { Owner.MemAnimEffect.PlaySkill_1Hit_E(inDefender.MemAnimEffect); }
                    else
                    { if (inDefendx == 0) Owner.MemAnimEffect.PlaySkill_1Hit_E(DefenderList); }
                }
                if ( ActiveSkillType == 2)                                                                                  // 主动技能 2   
                {
                    if (Owner.MemAnimEffect.AnimAndEffec_C.ActiveEffectRange2 == 1)                                          /// 单体
                    { Owner.MemAnimEffect.PlaySkill_2Hit_E(inDefender.MemAnimEffect); }
                    else
                    { if (inDefendx == 0) Owner.MemAnimEffect.PlaySkill_2Hit_E(DefenderList); }
                }

            }
        }
        #endregion
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  大招技能         </summary>
    public class UltSkillItem     : MonoBehaviour 
    {
        public UltSkillData             UltSkill_D                      { set; get; }                                       ///  大招配置数据
        public IBattleMemMediator       Owner                           { set; get; }                                       ///  本体(攻方)

        public void                     AttackStart ()                                                                      //   攻击初始化运行   
        {
            _IsContinue                 = true;                                                                             /// 设置成员进行状态
            _DefenderList               = AttackTarget.GetAttackTargetList(Owner, UltSkill_D.AttackRange1);                 /// 设置目标成员列表

            if ( UltSkill_D == null || BattleControll.sInstance.BattleState != BattleState.Fighting)                        /// 验证战斗进行状态
            {    AttackOver();                                          }

            if ( _DefenderList.Count < 1 )                              AttackOver();                                       /// (敌方成员 < 1): 战斗结束 Or 攻击开始
            else
            {
                if      ( Owner.MemAnimEffect.AnimAndEffec_C.UltimateAttackType == 1)                                       /// 大招攻击类型_1: 跳到攻击目标位置前攻击 <!刺客>
                {         SprintToTarget();                                    }

                else if ((Owner.MemPos_D.XPos == X_Axis.X_03 || Owner.MemPos_D.XPos == X_Axis.X_04)                         /// 前排攻击技能,前走到目标,再攻击         <!战士>
                          && _DefenderList[0].Camp          != Owner.Camp 
                          && Owner.Hero_C.Profession2       == 1)
                {         StartCoroutine(WaitForAttack());              }
                else                                                                                                        /// 大招蓄力
                {         UltStorage();                                 }
            } 
        }
        public void                     MoveTarget  ( int inCount = 3)                                                      //   移动目标位置     
        {
            IBattleMemMediator          TheMem                          = null;

            for ( int i = 0; i < _DefenderList.Count; i++ )                                                                  //  List中 冒泡出最近目标成员  
            {
                IBattleMemMediator      TempMem                         = _DefenderList[i];
                if      ( TheMem.Hp <= 0)                               _DefenderList.Remove(TempMem);
                else if ( TheMem == null || Vector3.Distance( TempMem.MemPos_D.NowPosV3, Owner.MemPos_D.NowPosV3 ) < Vector3.Distance( TheMem.MemPos_D.NowPosV3, Owner.MemPos_D.NowPosV3) )
                {         TheMem        = TempMem;                      }                                                   // 对象为null || 冒泡距离最近目标 
            }

            if ( TheMem == null )                                                                                           //  重新嵌套计算三次,均无目标,攻击结束
            {
                _DefenderList           = AttackTarget.GetAttackTargetList(Owner, UltSkill_D.AttackRange1 );                /// 设置守方成员列表
                
                if ( inCount > 0 && _DefenderList.Count > 0)                                                                 /// 重新计算三次,均无目标,攻击结束
                {
                    inCount--;
                    MoveTarget(inCount);
                    return;
                }
                else
                {   AttackOver();       return;                         } 
            }

            Owner.AttackMove.MoveTarget = new MoveToMember(TheMem);                                                         /// 成员攻击前移动
        }
        public void                     StopAction  ()                                                                      //   停止行动         
        {
            _IsContinue                 = false;                                                                            ///  行动停止
            StopCoroutine               (WaitForAttack());                                                                  ///  停止主动线程(WaitForAttack())
            StopCoroutine               (UltBurst());                                                                       ///  停止主动线程(UltBurst())
            StopAllCoroutines();                                                                                            ///  停止所有迭代线程

            _DefenderList               = new List<IBattleMemMediator>();                                                   ///  清空守方成员列表
        }
        public void                     AttackOver  ()                                                                      //   攻击结束         
        {
            if (_IsContinue)
            {
                _IsContinue             = false;
                Owner.AttackOver();
            }
        }


        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private bool                    _IsContinue                     = false;                                            // 是否继续
        private Action                  _AttackComplete                 = null;                                             // 攻击完成
        private List<IBattleMemMediator>_DefenderList                   = new List<IBattleMemMediator>();                   // 守方成员列表

        IEnumerator                     WaitForAttack   ()                                                                  // 等待攻击 ( 到达目标,Next(大招蓄力) ->否则等待0.1秒:迭代) 
        {
            if (_IsContinue)
            {
                MoveTarget();                                                                                               // 移动目标位置

                if ( _DefenderList.Count > 0)                                                                               // 本体到达目标执行下一阶段(大招蓄力) __ 否则等待0.1秒 迭代线程
                {
                    if ( Owner.AttackMove.IsArrive)
                    {    UltStorage();                              }
                }
                else
                {
                    yield return new    WaitForSeconds(0.1f);
                    StartCoroutine      (WaitForAttack());
                }
            }
        }
        IEnumerator                     UltBurst        ()                                                                  // 大招爆发阶段     
        {
            if( _IsContinue && Owner.MemState == BattleMemState.Normal )
            {
                bool                    IsVibration             = false;                                                    // 震屏
                int                     TotalFame               = Owner.MemAnimEffect.AnimAndEffec_C.UltimateSkillFrame1;   // 关键帧总数
                float                   WaitTime                = 0;                                                        // 等待时间
                float                   AttAckTime              = Owner.MemAnimEffect.UltAttack();                          // 攻击时间
                List<int>               FameList                = Owner.MemAnimEffect.AnimAndEffec_C.UltimateSkillKeys1;    // 关键帧列表

                if ( _DefenderList.Count > 0 && _DefenderList[0].Camp != Owner.Camp )
                {    IsVibration        = true;                 }

                if ( FameList.Count > 0)
                {
                    int TempTotalFram   = FameList.Count;
                    int NowFramCount    = 0; 
                    foreach (var Item in FameList)
                    {
                        float           TempKeyWaitTime         = AttAckTime;
                        if ( TotalFame > 0 )                    TempKeyWaitTime = AttAckTime * Item * 1.0f/ TotalFame;      /// 关键帧等待 
                        TempKeyWaitTime                         = Mathf.Max(0.001f, TempKeyWaitTime - WaitTime);            /// 关键帧等待                    
                        WaitTime                                += TempKeyWaitTime;                                         /// 等待时间

                        yield return new WaitForSeconds         (TempKeyWaitTime);                                          /// 等待时间

                        NowFramCount++;                                                                                     /// 关键帧累加
                        if (_IsContinue && Owner.MemState == BattleMemState.Normal )                                        //  击中目标    
                        {
                            if ( Owner.MemAnimEffect.AnimAndEffec_C.UltimateEffectTrail1 == 3)                              /// 闪电链轨迹 击中
                            {    BattleControll.sInstance.TheMono.StartCoroutine(HitThunder(Owner, IsVibration, NowFramCount, TotalFame)); }
                            else                                                                                            //  普通轨迹 击中
                            {
                                int         Index               = 0;
                                for( int i = 0; i < _DefenderList.Count; i++ )
                                {
                                    if ( _DefenderList[i]!= null && _DefenderList[i].MemState == BattleMemState.Normal )
                                    {    BattleControll.sInstance.TheMono.StartCoroutine(HitTrail(Index, IsVibration, _DefenderList[i], NowFramCount, TotalFame));   }
                                }
                            }
                        }
                        if ( _IsContinue )
                        {
                            if ( AttAckTime > WaitTime)         yield return new WaitForSeconds( AttAckTime - WaitTime );   //  延迟攻击时间差
                            
                            if ( _IsContinue && Owner.MemState == BattleMemState.Normal )
                            {
                                if ( _AttackComplete != null )
                                {
                                    Owner.MemAnimEffect.Standby();                                                          /// 战力待机
                                    _AttackComplete();                                                                      /// 攻击完成
                                }
                                else
                                {
                                    Owner.MemAnimEffect.Standby();                                                          /// 战力待机
                                    AttackOver();                                                                           /// 攻击结束
                                }
                            } 
                        }
                    }
                }
            }
        }
        IEnumerator                     HitThunder  ( IBattleMemMediator inAttacker, bool inIsVibrat, int inNowFameCount, int inToatlFame )                 // 普通轨迹击中   
        {
            int                         Index           = 0;                                                                                                    /// 索引数
            string                      FromBone        = inAttacker.MemAnimEffect.AttackTrailBonePos_C.UltimateTrailBones;                                     /// 大招轨迹起点(攻方骨骼Str)
            IBattleMemMediator          TheDefender     = null;                                                                                                 /// 守方数据
            Vector3                     FromOffSet      = BattleParmConfig.ListToVector3(inAttacker.MemAnimEffect.AttackTrailBonePos_C.UltimateTrailBonesPos);  /// 起点V3坐标

            foreach ( var Item in _DefenderList )
            {
                float                   TheTrailTime    = inAttacker.MemAnimEffect.PlayThunder_E                                                                /// 轨迹特时间
                                                        ( inAttacker.MemAnimEffect.AnimAndEffec_C.UltimateAttackEffect1_20, FromBone, FromOffSet, inAttacker.MemAnimEffect);
                TheDefender             = Item;                                                                                                                 /// 守方
                FromBone                = TheDefender.MemAnimEffect.AnimAndEffec_C.StrikeBonePos;                                                               /// 受击骨骼
                FromOffSet              = Vector3.zero;                                                                                                         /// 起点V3坐标设置

                yield return new WaitForSeconds (TheTrailTime);                                                                                                 /// 延迟(轨迹时间)
                Hit ( Index, inIsVibrat, TheDefender, inNowFameCount, inToatlFame);                                                                             /// 击中
                yield return new WaitForSeconds (BattleParmConfig.ThunderHoldTime);                                                                             /// 延迟(闪电链持续时间)
                Index++;                                                                                                                                        /// 索引数++
            }
        }
        IEnumerator                     HitTrail    ( int inIndex, bool inIsVibrat, IBattleMemMediator inDefender,  int inNowFramCount, int inTotalFame )   // 闪电链轨迹击中 
        {
            if ( Owner.MemState == BattleMemState.Normal && inDefender.MemState == BattleMemState.Normal && Owner.MemPos_D.XPos != X_Axis.X_03 && Owner.MemPos_D.XPos != X_Axis.X_03)
            {
                float                   TheTrailTime                    = Owner.MemAnimEffect.PlayUltSecond_E(inDefender.MemAnimEffect);                        /// 大招
                if ( TheTrailTime > 0 )                                 yield return  new WaitForSeconds(TheTrailTime);                                         /// 延迟(轨迹特效时间)
            }
            Hit                         ( inIndex, inIsVibrat, inDefender, inNowFramCount, inTotalFame);                                                        /// 击中
        }
        private void                    Hit         ( int inIndex, bool inIsVibrat, IBattleMemMediator inDefender, int inNowFramCount, int inTotalTimes)    // 击中          
        {
            if ( inDefender.MemState == BattleMemState.Normal )
            {
                if ( inIsVibrat )       BattleControll.sInstance.CameraVibration();                                                                             // 摄像震屏

                if ( Owner.MemAnimEffect.AnimAndEffec_C.UltimateEffectRange1 == 1 )                                                                             // 单体击中特效
                {    Owner.MemAnimEffect.PlayUltHitSingle_E(inDefender.MemAnimEffect);          }
                else
                {
                    if ( Owner.MemAnimEffect.AnimAndEffec_C.UltimateWhetherCastOnce == 1 )                                                                      // 多段,是否播发一个受击特
                    {
                        if ( inNowFramCount == 1 )                      Owner.MemAnimEffect.PlayUltHitList_E(_DefenderList);                                    // 群体击中特效 
                    }
                    else
                    {    Owner.MemAnimEffect.PlayUltHitList_E(_DefenderList);                   }                                                               // 群体击中特效
                }
            }
        }
        private void                    SprintToTarget  ()                                                                                                  // 冲刺到目标     
        {
            float                       TheDistance                     = 0;
            float                       TheRealRange                    = 0;
            IBattleMemMediator          TheDefender                     = null;
            Vector3                     FromPosV3                       = Vector3.zero;
            Vector3                     TopPosV3                        = Vector3.zero;

            _DefenderList               = AttackTarget.GetAttackTargetList(Owner, BattleResStrName.DefendBackRowOne);                                           /// 敌方后排随机单体
            if ( _DefenderList.Count < 1)       _DefenderList           = AttackTarget.GetAttackTargetList(Owner, UltSkill_D.AttackRange1);                     /// 获取守方列表
            if ( _DefenderList.Count < 1)       AttackOver();                                                                                                   /// 攻击结束

            TheDefender                 = _DefenderList[0];                                                                                                     /// 守方成员
            FromPosV3                   = Owner.MemPos_D.NowPosV3;                                                                                             /// 起始点世界坐标
            TopPosV3                    = TheDefender.MemPos_D.NowPosV3;                                                                                       /// 目标点世界坐标
            TheDistance                 = Vector3.Distance (FromPosV3, TopPosV3 );                                                                              /// 距离( 世界视图 )

            if (TheDistance > 0)        Owner.ModelObj.transform.forward = (TopPosV3 - FromPosV3).normalized;                                                   /// Z轴位置 (竖排位移)
            TheRealRange                = Owner.BodySize / 2 + TheDefender.BodySize / 2 + 1;                                                                    /// 真实估计范围
            if (TheDistance <= TheRealRange)                            UltStorage();                                                                           /// 不需冲刺 直接大招释放
            else
            {
                float                   TempTime                        = 0;
                TopPosV3                = Vector3.Lerp(TopPosV3, FromPosV3, TheRealRange / TheDistance);                                                        /// 跳到目的点
                TempTime                = Vector3.Distance(TopPosV3, FromPosV3) / BattleParmConfig.MemberSprintSpeed;                                           /// 跳跃时间

                Owner.MemAnimEffect.Sprint();                                                                                                                   /// 冲刺动作
                Owner.TrialMove.MoveTo  (Owner.ModelObj, TopPosV3, TempTime, () =>     { UltStorage(); });                                                                     /// 移动到 ->完成回调线程 (大招释放)
                _AttackComplete         = () =>                         { SprintBack(FromPosV3); };                                                             /// 封装完成回调线程      (冲刺返回)
            }
        }
        private void                    SprintBack      ( Vector3 inFormPos )                                                                               // 冲刺返回位置   
        {
            float                       TheTime                         = Vector3.Distance(Owner.MemPos_D.NowPosV3, inFormPos) / BattleParmConfig.MemberSprintSpeed; /// 返回时间
            Owner.TrialMove.MoveTo      (Owner.ModelObj, inFormPos, TheTime, () =>      { AttackOver(); });                                                     /// 移动到目的->回调线程 (攻击技术)
        }
        private void                    UltStorage      ()                                                                                                  // 大招蓄力阶段   
        {
            bool                        IsNoScale       = Owner.Camp == Battle_Camp.Our || BattleControll.sInstance.BattleType == BattleType.JJC 
                                                                                        || BattleControll.sInstance.BattleType == BattleType.JJCLevel;
            float                       TheTime         = 0;                                                                /// 蓄力时间
            Configs_HeroData            TheHero_C       = Configs_Hero.sInstance.GetHeroDataByHeroID(Owner.Hero_C.HeroID);  /// 英雄配置数据

            if ( Owner.IMemData.isRoleHero )                                                                                /// 主角大招技能面板               
            {
                 PanelManager.sInstance.ShowPanel(SceneType.Battle, BattleResStrName.PanelName_RoleUltraFired).             /// 大招技能面板
                 GetComponent<RoleUltFiredItem>().ShowEffect(Owner.IMemData.MemberID);                                      /// 主角大招特效
            }
            Owner.EnergyLost(BattleParmConfig.MemberUltExEnergy);                                                           /// 大招能量消耗

            if ( IsNoScale )                                                                                                //  (Our)  大招慢放                   
            {
                Owner.Graying();
                TheTime                                 = Owner.MemAnimEffect.UltStorage(IsNoScale, BattleParmConfig.TimeScaleUltFired);    /// 蓄力动作时间
                Owner.MemAnimEffect.PlayUltStorage_E    (IsNoScale, BattleParmConfig.TimeScaleUltFired);                                    /// 蓄力动作_特效
                BattleEffect            TheEffect       = ResourceKit.sInstance.GetEffect(BattleResStrName.UltFired2Deffect);               /// 战斗特效
                MemberEffect            MemEffect       = TheEffect.GetMemEffect();                                                         /// 成员特效
                MemEffect.PlayEffect(BattleControll.sInstance.UICamera.transform, Vector3.zero, 2, BattleParmConfig.TimeScaleUltFired, true);   /// 2D特效
                BattleControll.sInstance.RealTimerRunning.Run(TheTime, () =>                                                                /// 蓄力结束
                {
                    Owner.GrayReleasing();
                    StartCoroutine(UltBurst());                                                                                             /// 大招爆发
                });
            }
            else                                                                                                            //  (Enmey)大招全局慢放(TimeScale控制) 
            {
                TheTime                                 = Owner.MemAnimEffect.UltStorage(IsNoScale);                        /// 蓄力动作
                Owner.MemAnimEffect.PlayUltStorage_E    (IsNoScale);                                                        /// 蓄力特效

                BattleEffect            TheEffect       = ResourceKit.sInstance.GetEffect(BattleResStrName.UltFired2Deffect);
                MemberEffect            MemEffect       = TheEffect.GetMemEffect();
                MemEffect.PlayEffect                    (BattleControll.sInstance.UICamera.transform, Vector3.zero);        /// 2D蓄力特效
                MemEffect.EffectObj.transform.position  = BattleParmConfig.WorldToUiPoint(Owner.MemAnimEffect.GetHitBone_TF(1).transform.position);

                Owner.Delay             (TheTime, () => { StartCoroutine(UltBurst()); });                                   /// 延迟执行 大招爆发

            }
        }
        #endregion
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  位移攻击         </summary>
    public class MoveToAttackItem : MonoBehaviour 
    {
        public IBattleMemMediator       Owner                           { set; get; }                                       /// 成员本体
        public IMoveTarget              MoveTarget                      = null;                                             /// 移动目标
        void Update()
        {
            if (!Owner.isAttacking)                                                                                         //  非攻击状态    
            {
                if ( BattleControll.sInstance.BattleState == BattleState.PreprogressOver &&                                 /// 1.战斗状态:本波结束, 2.本体存活, 3.动作:奔跑
                     Owner.Hp > 0  &&  Owner.MemAnimEffect.MemAnimState == BattleMemAnimState.Running)
                {    Owner.MemAnimEffect.Standby();                     }                                                   /// 站立待机
                return;
            }
            if ( Owner.MemState == BattleMemState.Normal && Owner.MemAnimEffect.MemAnimState == BattleMemAnimState.Running  /// 1.本体存活, 2.动作:奔跑 3.目标为空
                 && Owner != null && ( MoveTarget == null || MoveTarget.IsCancel == true ))
            {    Owner.MemAnimEffect.Standby();                         }                                                   /// 站立待机

            if (Owner.MemState == BattleMemState.Normal  && MoveTarget != null && MoveTarget.IsCancel == false && Owner.MemAnimEffect != null &&  // 1.本体存活, 2. 目标不为空, 3.本体特效不为空, 3.动作: 奔跑||站立
               (Owner.MemAnimEffect.MemAnimState == BattleMemAnimState.Standby || Owner.MemAnimEffect.MemAnimState == BattleMemAnimState.Running))  
            {
                Vector3                 TargetPos                       = MoveTarget.GetTargetPos();                                /// 目标坐标
                Vector3                 FromPos                         = Owner.MemPos_D.NowPosV3;                                  /// 本体当前坐标 = 始发坐标

                if ( Vector3.Distance (TargetPos, FromPos) > 0)                                                                     /// 距离大于 0
                {    Owner.ModelObj.transform.forward                   = (TargetPos - FromPos).normalized;               }         /// 模型_Z轴(上下)位移 1格
                if (Owner.MemPos_D.XPos != X_Axis.X_03 && Owner.MemPos_D.XPos != X_Axis.X_03)                                       /// 本体成员不为前排,本体坐标 = 目标坐标    
                {
                    TargetPos.y         = FromPos.y;
                    TargetPos.z         = FromPos.z;
                }

                float                   Distance                        = Vector3.Distance(FromPos, TargetPos);                     /// 目标坐标 始发坐标 距离
                float                   RealDistance                    = GetRealDistance (FromPos, TargetPos);                     /// 目标坐标 始发坐标 真实距离(除去成员占位)
                float                   Direction                       = TargetPos.x <= FromPos.x ? 1 : -1;                        /// 方向

                if ( RealDistance <= Owner.AttackRange)                                                                             // 目标在攻击范围内     
                {
                    if (Owner.MemAnimEffect.MemAnimState == BattleMemAnimState.Running)         Owner.MemAnimEffect.Standby();      // 本体动作为本跑,播放战力待机
                }
                else                                                                                                                // 目标在攻击范围之外   
                {
                    if (Owner.MemAnimEffect.MemAnimState == BattleMemAnimState.Standby)         Owner.MemAnimEffect.Runing();       // 当前动作为战力 _播放本跑动作
                    Vector3             ToPos                           = Owner.MoveSpeed * Time.deltaTime / Distance * (TargetPos - FromPos) + FromPos; 

                    Owner.ModelObj.transform.localPosition              = ToPos;                                                    // 本体移动到目标坐标(一帧)

                    IBattleMemMediator  TheMem                          = null;
                    foreach (var Item in BattleControll.sInstance.OurTeam.TeamList.OrderBy(P => P.MemPos_D.FixedPosNum))            // 我方固定为号码 排序
                    {
                        if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.NowPosV3.x * Direction < Owner.MemPos_D.NowPosV3.x * Direction
                            && GetRealDistance(Item.MemPos_D.NowPosV3, Owner.MemPos_D.NowPosV3, Item) < 0)
                        {               TheMem                           = Item;                    }                               // 返回目标位置 我方成员数据

                    }
                    foreach ( var Item in BattleControll.sInstance.EnemyTeam.TeamList)                                              // 返回目标位置 敌方成员数据
                    {
                        if (Item.MemState == BattleMemState.Normal && Item.MemPos_D.NowPosV3.x * Direction < Owner.MemPos_D.NowPosV3.x * Direction
                            && GetRealDistance(Item.MemPos_D.NowPosV3, Owner.MemPos_D.NowPosV3) < 0)
                        {
                            TheMem                                      = Item;
                            break;
                        }
                    }
                    if (TheMem != null )                                                                                            // 目标成员不为空
                    {
                        Vector3         ThePos                          = TheMem.MemPos_D.NowPosV3 - Owner.MemPos_D.NowPosV3;         
                        if ( Vector3.Dot(TargetPos - Owner.MemPos_D.NowPosV3, ThePos) < 0 )
                        {
                            ThePos                                      = -ThePos;
                        }
                        Owner.ModelObj.transform.localPosition          = Owner.MoveSpeed * 1.2f * Time.deltaTime *2 / Distance * ThePos + FromPos; // 本体位移到坐标(一帧)
                    }
                }
            }
        }
        public bool     IsArrive                                                                                            //  是否到达       
        {
            get
            {
                Vector3                 TargetV3                        = MoveTarget.GetTargetPos();
                Vector3                 FromV3                          = Owner.MemPos_D.NowPosV3;

                if( Owner.MemPos_D.XPos != X_Axis.X_03 && Owner.MemPos_D.XPos != X_Axis.X_04)
                {
                    TargetV3.y = FromV3.y;
                    TargetV3.z = FromV3.z;
                }
                return GetRealDistance(FromV3, TargetV3) <= Owner.AttackRange;
            }
        }
        private float   GetRealDistance( Vector3 inFrom, Vector3 inTarget, IBattleMemMediator inTargetMem = null )          //  获取真实距离   
        {
            if (inTargetMem == null)            return Vector3.Distance(inFrom, inTarget) - Owner.BodySize / 2 - MoveTarget.GetTargetBodySize() / 2;
            else                                return Vector3.Distance(inFrom, inTarget) - Owner.BodySize / 2 - inTargetMem.BodySize / 2;
        }

    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  主角英雄大招释放  </summary>
    public class RoleUltFiredItem : MonoBehaviour 
    {
        public  List<UISprite>          SpriteList;                                                                         /// 图集资源列表
        public float                    ShowEffect(int inID, Action inCallback = null)                                      //  展示特效    
        {
            string                      StrID                           = "";                                               /// SpriteName

            switch(inID)
            {
                case 105002:            inID = 100001;                  break;
                case 105003:            inID = 100002;                  break;
                case 105004:            inID = 100003;                  break;
                default:                                                break;
            }
            StrID                       = inID.ToString();

            foreach ( var Item in SpriteList)
            { Item.spriteName           = StrID; }                                                                          /// 指定资源
            Callback                    = inCallback;                                                                       /// 回调线程
            SpriteList[1].GetComponent<TweenAlpha>().onFinished.Add     (new EventDelegate(() => { FinishDelay(); }));      /// 变换完成并回调

            return                      1.8f;                                                                               /// 特效时间                       
        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private Action                  Callback                        = null;                                             /// 回调线程

        private void                    FinishDelay()                                                                       //  延迟执行线程 
        {   Invoke("Finish", 0.4f);     }
        private void                    Finish()                                                                            //  特效完成后,隐藏大招释放面板
        {
            PanelManager.sInstance.HidePanel(SceneType.Battle, BattleResStrName.PanelName_RoleUltraFired);                  /// 隐藏大招释放面板
            Callback();
        }
        #endregion
    }
}
