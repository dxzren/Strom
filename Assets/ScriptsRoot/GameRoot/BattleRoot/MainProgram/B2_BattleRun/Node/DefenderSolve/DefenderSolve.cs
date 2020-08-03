using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 守方单个成员伤害结算 </summary>
    public class DefenderDamageSolve                                    
    {
        public IBattleMemMediator   Owner                           { get; set; }                                           /// 本体(守方)
        private int                 EnergyAttackBase                = 91;                                                   /// 攻击能量基础值

        public bool                 GetDodge            (IBattleMemMediator inAttacker)                                     //  获取闪避结果                    
        {
            float                   HitBase                         = 0.7f;                                                 /// 基础命中率
            if ( Owner.MemBuff.IsContains(BufferState.HideDodge))
            {    Owner.IMemUI.TipBuffHit("闪避");                  return true;                }

            float                   TheHitRate  = (Owner.IMemData.Dodge + 100) / ( inAttacker.IMemData.Hit + 100) * HitBase;/// 真实命中 = ( 命中能力+a）/（闪避能力+a）* 基本命中率 )
            int                     TheHit      = Mathf.RoundToInt(TheHitRate * 100);

            if ( BattleReport.sInstance.GetRandEvent(BattleRandomType.Dodge, TheHit,inAttacker,Owner))                      /// 获取随机结果,并记录随机事件信息(回放数据)
            {    Owner.IMemUI.TipBuffHit("闪避");                  return true;                }
            else                                                  return false;                                             /// 返回
        }

        public int                  GetPhyAttack()                                                                          //  物理攻击力_buff结算后           
        {
            int                     TheAttack                       = Owner.IMemData.PhyAttack;                     
            TheAttack               += (int)Owner.MemBuff.GetPlusBuffValue(BufferState.PhyArmorUp);
            TheAttack               -= (int)Owner.MemBuff.GetPlusBuffValue(BufferState.PhyAttackDown);
            return                  Mathf.Max(TheAttack,0);
        }
        public int                  GetMagicAttack()                                                                        //  魔法攻击力_buff结算后           
        {
            int                     TheMagAttack                    = Owner.IMemData.MagicAttack;
            TheMagAttack            += (int)Owner.MemBuff.GetPlusBuffValue(BufferState.MagicAttackUp);
            TheMagAttack            -= (int)Owner.MemBuff.GetPlusBuffValue(BufferState.MagicAttackDown);
            return                  Mathf.Max( TheMagAttack,0);
        }
        public int                  GetPhyCrit          ( IBattleMemMediator inDefender)                                    //  物理暴击 --返回暴击伤害比        
        {
            int                     TheCrit                         = Owner.IMemData.PhyCrit;                               /// 物理暴击概率
            int                     CritDamage                      = 100;                                                  /// 暴击伤害

            TheCrit                 += (int)Owner.MemBuff.GetPlusBuffValue(BufferState.CritUp);
            if ( BattleReport.sInstance.GetRandEvent(BattleRandomType.PhyCrit, TheCrit, Owner, inDefender))
            {
                inDefender.IMemUI.TipBuffHit ("暴击");
            }
            else                    CritDamage                      = 0;
            return                  CritDamage;
        }
        public int                  GetMagicCrit        ( IBattleMemMediator inDefender)                                    //  魔法暴击 --返回暴击伤害比        
        {
            int                     TheCrit                         = Owner.IMemData.MagicCrit;                             /// 暴击概率值
            int                     CritDamage                      = 100;                                                  /// 暴击伤害值 (+ 100%) 固定伤害

            TheCrit                 += (int)Owner.MemBuff.GetPlusBuffValue(BufferState.CritUp);                             /// 累加 暴击buff 值
            if ( BattleReport.sInstance.GetRandEvent(BattleRandomType.MagicCrit, TheCrit, Owner, inDefender))
            {
                inDefender.IMemUI.TipBuffHit("暴击");
            }
            else                    return  CritDamage              = 0;
            return                  CritDamage;
        }
        public float                GetPhyArmor         ( IBattleMemMediator inAttacker)                                    //  物理护甲 (护甲 最低值0,不为负数) 
        {
            float                   ThePhyArmor = Mathf.Max(Owner.IMemData.PhyArmor - inAttacker.IMemData.ThroughPhyArmor,0);   
            ThePhyArmor             += Owner.MemBuff.GetPlusBuffValue(BufferState.PhyArmorUp);                              /// 加护甲增益buff
            ThePhyArmor             -= Owner.MemBuff.GetPlusBuffValue(BufferState.PhyArmorDown);                            /// 键护甲减益buff
            return                  Mathf.Max (ThePhyArmor, 0);
        }
        public float                GetMagicArmor       ( IBattleMemMediator inAttacker)                                    //  魔法护甲 (护甲 最低值0,不为负数) 
        {
            float                   TheMagicArmor                   = Owner.IMemData.MagicArmor;
            TheMagicArmor           += Owner.MemBuff.GetPlusBuffValue(BufferState.MagicArmorUp);                            /// 加护甲增益buff
            TheMagicArmor           -= Owner.MemBuff.GetPlusBuffValue(BufferState.MagicArmorDown);                          /// 键护甲减益buff
            return                  Mathf.Max(TheMagicArmor,0);
        }
        public float                GetPhyProtect       ( IBattleMemMediator inAttacker)                                    //  物理伤害减免    
        {
            return                  ComputeProtect(inAttacker, GetPhyArmor(inAttacker));
        }
        public float                GetMagicProtect     ( IBattleMemMediator inAttacker)                                    //  魔法伤害减免    
        {
            return                  ComputeProtect(inAttacker, GetMagicArmor(inAttacker));
        }
        public float                ComputeProtect      ( IBattleMemMediator inAttacker,float inArmor)                      //  护甲减免计算    
        {
            return (float)Math.Round(inArmor * 0.75 / (inArmor + (inAttacker.IMemData.MemberLv + 10) * 1.5), 2);            // 两位精度浮点数
        }

                                                                                                                            ///<| 普通攻击_技能攻击 结算 >
        public AttackDamageData     GetSkillCureHp      ( SkillEffectData inSkill_E, IBattleMemMediator inDefender)         //  技能治疗血量     
        {
            int                     TheCureHp                       = GetMagicAttack();
            int                     TheCrit                         = GetMagicCrit(inDefender);
            AttackDamageData        TheAttackAction                 = new AttackDamageData(Owner,inDefender);
            TheAttackAction.SetCrit (TheCrit);

            TheCureHp               = (int)((TheCureHp * inSkill_E.DamageRate + inSkill_E.BaseDamage) * (100 + TheCrit) / 100);
            TheAttackAction.Damage  = TheCureHp;

            return                  TheAttackAction;
        }
        public AttackDamageData     GetNorAttackDamage  ( IBattleMemMediator inDefender )                                   //  普通攻击单次伤害 
        {
            int                     TheAttack                       = GetPhyAttack();                                       /// 结算后物理攻击力
            int                     TheCrit                         = GetPhyCrit(inDefender);                               /// 结算后物理暴击伤害
            float                   TheDamageBuffValue              = 0;                                                    /// 伤害类buff值 <百分比系数>
            bool                    IsElementBuff                   = true;                                                 /// leishen,venom,ranmie<| 雷灭,毒灭,燃灭- 50%几率伤害加倍>
            AttackDamageData        TheAttackAction                 = new AttackDamageData(Owner,inDefender);               /// 单次攻击行为数据

            if ( inDefender.MemBuff.IsContains(BufferState.ImmunePhyDamage) ||                                              /// 伤害免疫 (物理免疫Buff + 伤害免疫buff)
                 inDefender.MemBuff.IsContains(BufferState.ImmunePhyDamage))
            {
                inDefender.IMemUI.TipBuffHit("免疫");
                return new AttackDamageData(Owner, inDefender, 0 ,false);
            }

            int TheDamage           = (int)(TheAttack * ( 100 + TheCrit ) / 100 * ( 1 - inDefender.DefendSolve.GetPhyProtect(Owner)));  ///<|伤害公式:结算伤害 = 攻击力 + 暴击伤害% - 护甲类减免伤害%|>


            if (TheDamageBuffValue == 0 )                                                                                   // 普攻伤害buff 结算
            {
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.DamageUp);                                /// 伤害提升
                TheDamageBuffValue  -= inDefender.MemBuff.GetPlusBuffValue(BufferState.DamageDown);                         /// 伤害减少     <守方>
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.PhyDamageUP);                             /// 物理伤害提升
                TheDamageBuffValue  -= inDefender.MemBuff.GetPlusBuffValue(BufferState.PhyDamageDown);                      /// 物理伤害减少 <守方>

                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.NormalAttackDamageUp);                    /// 普通攻击伤害提升
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.NorDamageSilie);                          /// 普通攻击伤害永久提升
            }

            TheDamage               = (int)(TheDamage * Mathf.Max( 1 + TheDamageBuffValue,0.05f));                          /// <| 结算Buff伤害 * 当前伤害 ( 乘法公式 )

            float                   TheClValue = Owner.MemBuff.GetPlusBuffValue(BufferState.Cleave);                        /// <| 结算分裂攻击 百分比
            if (TheClValue == 0)    TheClValue = 1;
            TheDamage               = (int)(TheDamage * TheClValue);
            TheDamage               = Mathf.Min(TheDamage, inDefender.Hp);                                                  /// <| 结算伤害不超出敌方血量上限
            if ( IsElementBuff )                                                                                            /// <| 结算雷灭,毒灭,燃灭- 50%几率伤害加倍>
            {
                if (Owner.MemBuff.IsContains(BufferState.Thunder))                                                          //  雷灭  50%几率伤害加倍 
                {
                    if (BattleReport.sInstance.GetRandEvent( BattleRandomType.ThunderTalent, 50 ,Owner,inDefender))
                    {
                        Buffer      TheBuff_D                       = Owner.MemBuff.GetBuffer(BufferState.Thunder);
                        if (TheBuff_D != null)
                        {
                            AttackDamageData TheDamgae_D            = Owner.DefendSolve.GetMagicSkillDamage(TheBuff_D.SkillEffe, inDefender);
                            inDefender.DefendSolve.ComputeDefencerDamage(Owner, TheDamgae_D);
                        }
                    }
                }
                if (Owner.MemBuff.IsContains(BufferState.Venom))                                                            //  毒灭  50%几率伤害加倍 
                {
                    if (BattleReport.sInstance.GetRandEvent( BattleRandomType.VenomTalent, 50 ,Owner,inDefender))
                    {
                        Buffer      TheBuff_D                       = Owner.MemBuff.GetBuffer(BufferState.Venom);
                        if (TheBuff_D != null)
                        {
                            AttackDamageData TheDamgae_D            = Owner.DefendSolve.GetMagicSkillDamage(TheBuff_D.SkillEffe, inDefender);
                            inDefender.DefendSolve.ComputeDefencerDamage(Owner, TheDamgae_D);
                        }
                    }
                }
                if (Owner.MemBuff.IsContains(BufferState.FireDust))                                                         //  燃灭  50%几率伤害加倍 
                {
                    if (BattleReport.sInstance.GetRandEvent( BattleRandomType.FireTalent, 50 ,Owner,inDefender))
                    {
                        Buffer      TheBuff_D                       = Owner.MemBuff.GetBuffer(BufferState.FireDust);
                        if (TheBuff_D != null)
                        {
                            AttackDamageData TheDamgae_D            = Owner.DefendSolve.GetMagicSkillDamage(TheBuff_D.SkillEffe, inDefender);
                            inDefender.DefendSolve.ComputeDefencerDamage(Owner, TheDamgae_D);
                        }
                    }
                }
            }
            TheAttackAction.Damage  = TheDamage;                                                                            /// 单次攻击行为伤害
            return                  TheAttackAction;                                                                        /// 返回本次攻击数据
        }
        public AttackDamageData     GetPhySkillDamage   ( SkillEffectData inSkill_E, IBattleMemMediator inDefender)         //  物理技能伤害     
        {
            int                     TheAttack                       = GetPhyAttack();
            int                     TheCrit                         = GetPhyCrit(inDefender);
            float                   TheDamageBuffValue              = 0;                                                    /// 伤害类buff值 <百分比系数>
            AttackDamageData            TheAttackAction                 = new AttackDamageData(Owner,inDefender);

            if (inDefender.MemBuff.IsContains(BufferState.ImmuneDamage) ||                                                  /// 伤害免疫 (物理免疫Buff + 伤害免疫buff)
                inDefender.MemBuff.IsContains(BufferState.ImmunePhyDamage))
            {
                inDefender.IMemUI.TipBuffHit("免疫");
                return new          AttackDamageData(Owner, inDefender, 0, false);
            }  

            TheAttack               = (int)(TheAttack * inSkill_E.DamageRate + inSkill_E.BaseDamage);                       /// <|技能攻击力公式: 技能攻击力 = 物理攻击力*技能系数 + 技能基础伤害|>
            int TheDamage           = (int)(TheAttack * (100 + TheCrit) / 100 * (1 - inDefender.DefendSolve.GetPhyProtect(Owner)));     /// <|伤害公式:结算伤害 = 技能攻击力 + 暴击伤害% - 护甲类减免伤害%|>

            if (TheDamageBuffValue == 0)                                                                                    // 技能伤害buff 结算    
            {
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.DamageUp);                                /// 伤害提升
                TheDamageBuffValue  -= inDefender.MemBuff.GetPlusBuffValue(BufferState.DamageDown);                         /// 伤害减少     <守方>
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.PhyDamageUP);                             /// 物理伤害提升
                TheDamageBuffValue  -= inDefender.MemBuff.GetPlusBuffValue(BufferState.PhyDamageDown);                      /// 物理伤害减少 <守方>
            }
            TheDamage               = (int)(TheDamage * Mathf.Max((1 + TheDamageBuffValue), 0.05f));                        ///  <| 结算Buff伤害 * 当前伤害 ( 乘法公式 )

            TheAttackAction.SetCrit(TheCrit);                                                                               /// 暴击设置
            if (TheAttackAction.IsCrit)                             BattleControll.sInstance.CameraVibration();             /// 技能暴击--触发 震屏效果

            TheAttackAction.Damage  = TheDamage;                                                                            /// 单次攻击行为伤害
            return                  TheAttackAction;                                                                        /// 返回本次攻击数据
        }
        public AttackDamageData     GetMagicSkillDamage ( SkillEffectData inSkill_E, IBattleMemMediator inDefender )        //  魔法技能伤害     
        {
            int                     TheAttack                       = GetMagicAttack();                                     //
            int                     TheCrit                         = GetMagicCrit(inDefender);                             //
            float                   TheDamageBuffValue              = 0;                                                    //
            AttackDamageData        TheAttackAction                 = new AttackDamageData(Owner,inDefender);               //

            if ( inDefender.MemBuff.IsContains(BufferState.ImmuneMagicDamage) ||                                            /// 伤害免疫 (魔法免疫Buff + 伤害免疫buff)
                 inDefender.MemBuff.IsContains(BufferState.ImmuneDamage))
            {
                inDefender.IMemUI.TipBuffHit("免疫");
                return new           AttackDamageData(Owner, inDefender, 0, false);
            }
            TheAttack               = (int) ( TheAttack * inSkill_E.DamageRate + inSkill_E.BaseDamage );                    /// <|技能攻击力公式: 技能攻击力 = 魔法攻击力*技能系数 + 技能基础伤害|>
            int TheDamage           = (int) ( TheAttack * (TheCrit + 100) / 100 * ( 1 - inDefender.DefendSolve.GetPhyProtect(Owner)));    /// <|伤害公式:结算伤害 = 技能攻击力 + 暴击伤害% - 护甲类减免伤害%|>

            if ( TheDamageBuffValue == 0)                                                                                   //  技能伤害buff 结算 
            {
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.DamageUp);                                /// 伤害提升
                TheDamageBuffValue  -= inDefender.MemBuff.GetPlusBuffValue(BufferState.DamageDown);                         /// 伤害减少     <守方>
                TheDamageBuffValue  += Owner.MemBuff.GetPlusBuffValue(BufferState.MagicDamageUp);                           /// 魔法伤害提升
                TheDamageBuffValue  -= inDefender.MemBuff.GetPlusBuffValue(BufferState.MagicDamgaeDown);                    /// 魔法伤害减少 <守方>
            }
            TheDamage               = (int)(TheDamage * Mathf.Max(1 + TheDamageBuffValue, 0.05f));                          ///  <| 结算Buff伤害 * 当前伤害 ( 乘法公式 )

            TheAttackAction.SetCrit(TheCrit);                                                                               /// 暴击设置
            if (TheAttackAction.IsCrit)                             BattleControll.sInstance.CameraVibration();             /// 技能暴击--触发 震屏效果

            TheAttackAction.Damage  = TheDamage;                                                                            /// 单次攻击行为伤害
            return                  TheAttackAction;                                                                        /// 返回本次攻击数据
        }

                                                                                                                            /// <| 伤害 结算 >
        public AttackDamageData     ComputeDefencerDamage( IBattleMemMediator inAttacker, AttackDamageData inAttackAct)     //  伤害结算: 攻击时返回守方掉血_或者死亡 
        {
            int                     TheDamage                       = inAttackAct.Damage;                                   /// 单次攻击伤害

            bool                    IsPolarCompute                  = true;                                                 /// 1) 结算 属性相克
            bool                    IsNoDeathShield                 = true;                                                 /// 2) 结算 不死护盾
            bool                    IsReboundDamage                 = true;                                                 /// 3) 结算 反伤护盾
          
            if ( IsPolarCompute)                                                                                            //  1) 结算属性相克 (冰火雷) 
            {
                if ( BattleControll.sInstance.BattleType == BattleType.DragonTrialIce       ||
                     BattleControll.sInstance.BattleType == BattleType.DragonTrialFire      ||
                     BattleControll.sInstance.BattleType == BattleType.DragonTrialThunder       )
                {
                    float           DamageRate                      = 1;                                                    /// 属性相克系数
                    switch (inAttacker.IMemData.MemberPolarity)
                    {
                        case 1:     DamageRate = Owner.IMemData.MemberPolarity == 2 ? 1.3f : 1; break;                      /// 冰->火 系数+1.3
                        case 2:     DamageRate = Owner.IMemData.MemberPolarity == 3 ? 1.3f : 1; break;                      /// 火->冰 系数+1.3
                        case 3:     DamageRate = Owner.IMemData.MemberPolarity == 1 ? 1.3f : 1; break;                      /// 雷->冰 系数+1.3
                    }
                    TheDamage       = (int)(TheDamage * DamageRate);
                }
            }
            Owner.HpLost            ( TheDamage, inAttackAct.IsCrit);                                                       /// <| Hp_掉血结算 >
            if ( IsNoDeathShield && Owner.Hp < 1)                                                                           //  2) 结算 不死护盾        
            {
                if (Owner.MemBuff.IsContains(BufferState.SimpleBuryShield))     Owner.HpAdd(1,false);                       //  加1点血
            }    
            if ( Owner.Hp < 1)                                                                                              //  阵亡结算                
            {
                inAttacker.EnergyAdd(300);
                inAttacker.IMemUI.TipBuffHit("击杀奖励");
                Owner.MemState      = BattleMemState.Dead;                                                                  /// 成员状态
            }
            if ( Owner.Hp > 1 && IsReboundDamage )                                                                          //  3) 反伤结算             
            {
                if(Owner.MemBuff.IsContains(BufferState.ReboundShield))                                                     /// 检测反伤护盾                           
                {
                    int                 TheReboundDamage                = 0;                                                /// 反射伤害
                    TheReboundDamage                                    =  (int)(TheDamage * 0.2f);                         /// 反射伤害0.2常量
                    inAttacker.DefendSolve.ComputeDefencerDamage(Owner, new AttackDamageData(Owner, inAttacker, TheReboundDamage, false)); /// 结算反伤
                }
            }
           
            if ( Owner.Hp > 1)                                                                                              //  (守方)结算 能量         
            {
                float                   TheRate     = BattleParmConfig.GetEnergyRate(Owner.MemPos_D);                       /// 能量计算系数
                int                     TheEnergy   = Mathf.RoundToInt(TheDamage * 1.0f/Owner.MaxHp * BattleParmConfig.MemberEnergyMax); /// 能量结算
                Owner.EnergyAdd         (TheEnergy);                                                                        /// 增加能量
            }

            inAttackAct.Damage      = TheDamage;                                                                            /// 伤害
            return                  inAttackAct;                                                                            /// 返回
        }

        public int                  ComputeNorAttackSuckBlood   ( IBattleMemMediator inDefender, int inDamage)              //  普通攻击吸血结算 
        {
            int                     TheHpRate                       = Owner.IMemData.SuckBlood;                             /// 吸血百分比
            int                     ReHp                            = 0;                                                    /// 返回血量

            ReHp                                                    = (int)(inDamage * TheHpRate / 100);                                                     
            if (inDamage > 0)       Owner.HpAdd( inDamage, false);
            return                  ReHp;
        }
        public int                  ComputeSkAttackSuckBlood    ( IBattleMemMediator inDefender, int inDamage )             //  技能吸血结算     
        {
            int                     TheHpRate                       = Owner.IMemData.SuckBlood;                             /// 吸血百分比
            int                     ReHp                            = 0;                                                    /// 返回血量    

            ReHp                                                    = (int)(inDamage * TheHpRate / 100);
            if (inDamage > 0)       Owner.HpAdd( inDamage, false);
            return                  ReHp;
        }
        public int                  ComputeAttackerEnergy       ( List<IBattleMemMediator> inMemAct_DList)                  //  结算攻方能量     
        {
            int                     TheEnergy                       = EnergyAttackBase;                                     /// 单次攻击能量
            TheEnergy               = Owner.EnergyAdd(TheEnergy);
            return                  TheEnergy;
        }
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 单次攻击行为数据 </summary>
    public class AttackDamageData                                       
    {
        public int                  Damage                          = 0;                                                    /// 伤害值
        public bool                 IsCrit                          { get; private set; }                                   /// 是否暴击
        public IBattleMemMediator   Attacker                        { set; get; }                                           /// 攻击方成员数据
        public IBattleMemMediator   Defender                        { set; get; }                                           /// 防御方成员数据

        public AttackDamageData(IBattleMemMediator inAttacker,IBattleMemMediator inDefender)                                //  构造函数      
        {
            Attacker                = inAttacker;
            Defender                = inDefender;
        }
        public AttackDamageData(IBattleMemMediator inAttacker, IBattleMemMediator inDefender, int inDamage, bool inIsCrit)  //  构造函数 重载 
        {
            Damage = inDamage;
            IsCrit = inIsCrit;
            Attacker = inAttacker;
            Defender = inDefender;
        }

        public void                 SetCrit(int inCrit)                                                                     //  暴击设置(暴击值大于 0 - 暴击:true)
        {
            IsCrit                  = inCrit > 0;
        }
        public void                 ComputeDamage()                                                                         //  结算伤害 
        {
            Attacker.IMemData.Damage                                += Damage;
        }
    }

}