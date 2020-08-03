using System;
using UnityEngine;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  创建技能类型数据 </summary>
    public class SkillDataBuilder   
    {
        public static ActiveSkillData   BuildActiveSkill    ( int inSkillID, int inSkillLv)                                 // 创建主动攻击技能数据 
        {
            Configs_ActiveSkillData     TheActSkill_C       = Configs_ActiveSkill.sInstance.GetActiveSkillDataByActiveSkillID(inSkillID);
            ActiveSkillData             TheActSkill         = new ActiveSkillData(TheActSkill_C);
            TheActSkill.SkillLv                             = inSkillLv;

            if ( TheActSkill_C.Effect1_1.Replace("0","").Trim().Length > 0)                                                 // 加载主动技能特效 1段
            {
                TheActSkill.SkillEffectList.Add(BuildEffect_1(inSkillLv, TheActSkill_C));
            }

            if ( TheActSkill_C.Effect1_2.Replace("0","").Trim().Length > 0)                                                 // 加载主动技能特效 2段
            {
                TheActSkill.SkillEffectList.Add(BuildEffect_2(inSkillLv, TheActSkill_C));
            }

            return                      TheActSkill;
        }
        public static UltSkillData      BuildUltSkill       ( int inSkillID, int inSkillLv)                                 // 大招技能数据        
        {
            Configs_UltSkillData        UltSkill_C          = Configs_UltSkill.sInstance.GetUltSkillDataByUltSkillID(inSkillID);
            UltSkillData                TheUltSkill         = new UltSkillData(UltSkill_C);
            TheUltSkill.SkillLevel                          = inSkillLv;

            if ( UltSkill_C.Effect1_1.Replace("0","").Trim().Length > 0 )
            {    TheUltSkill.SkillEffectList.Add (BuildUltEffect_1(inSkillLv, UltSkill_C));}
            if ( UltSkill_C.Effect1_2.Replace("0","").Trim().Length > 0 )
            {    TheUltSkill.SkillEffectList.Add (BuildUltEffect_2(inSkillLv, UltSkill_C));}

            return                      TheUltSkill;
        }
        public static BossSkillData     BuildBossSkill      ( int inSkillID, int inSkillLv)                                 // 创建boss技能数据    
        {
            if(inSkillID == 0 || inSkillLv == 0)            return null;

            Configs_ActiveSkillData     ActSkill_C          = Configs_ActiveSkill.sInstance.GetActiveSkillDataByActiveSkillID(inSkillID);
            BossSkillData               BossSkill           = new BossSkillData(ActSkill_C);
            BossSkill.SkillLv                               = inSkillLv;

            if ( ActSkill_C.Effect1_1.Replace("0","").Trim().Length > 0 )
            { BossSkill.SkillEffectList.Add( BuildEffect_1(inSkillLv,ActSkill_C));                    }
            if ( ActSkill_C.Effect1_2.Replace("0","").Trim().Length > 0 )
            { BossSkill.SkillEffectList.Add( BuildEffect_2(inSkillLv,ActSkill_C));                    }

            foreach ( var Item in BossSkill.SkillEffectList )                                                               // 不可清除类型buff
            {    Item.SkillBuffType                         = SkillBuffType.BufferNeutral;            }
            return                      BossSkill;
        }


        #region================================================||   PrivateModel--    < 私有模块 函数_声明 > ||<FourNode>================================================
        private static SkillEffectData  BuildEffect_1       (int inSkillLv, Configs_ActiveSkillData inActSkill_C )          // 创建技能特效_1       
        {
            SkillEffectData             TheSkillE           = GetSkillEffect(inActSkill_C.Effect1_1);                                               /// 技能特效

            TheSkillE.SkillLevel                            = inSkillLv;                                                                            /// 技能等级
            TheSkillE.SkillCD                               = inActSkill_C.CD1_1 <= 0 ? BattleParmConfig.MaxCDTime : inActSkill_C.CD1_1;            /// 技能CD
            TheSkillE.DamageRate                            = inActSkill_C.Coefficient1_1;                                                          /// 伤害系数
            TheSkillE.BaseDamage                            = inActSkill_C.BaseValue1_1 + inActSkill_C.UpValue1_1 * (Math.Max(inSkillLv,1) - 1);    /// 基础伤害
            TheSkillE.BuffName                              = inActSkill_C.Expression1;                                                             /// Buff名称
            TheSkillE.BonePos                               = inActSkill_C.Position1;                                                               /// 骨骼位置

            return                      TheSkillE;
        }
        private static SkillEffectData  BuildEffect_2       (int inSkillLv, Configs_ActiveSkillData inActSkill_C )          // 创建技能特效_2       
        {
            SkillEffectData             TheSkillE           = GetSkillEffect(inActSkill_C.Effect1_2);                                             /// 技能特效

            TheSkillE.SkillLevel                            = inSkillLv;                                                                            /// 技能等级
            TheSkillE.SkillCD                               = inActSkill_C.CD1_2 <= 0 ? BattleParmConfig.MaxCDTime : inActSkill_C.CD1_2;            /// 技能CD
            TheSkillE.DamageRate                            = inActSkill_C.Coefficient1_2;                                                          /// 伤害系数
            TheSkillE.BaseDamage                            = inActSkill_C.BaseValue1_2 + inActSkill_C.UpValue1_2 * (Math.Max(inSkillLv,1) - 1);    /// 基础伤害
            TheSkillE.BuffName                              = inActSkill_C.Expression2;                                                             /// Buff名称
            TheSkillE.BonePos                               = inActSkill_C.Position2;                                                               /// 骨骼位置

            return                      TheSkillE;
        }

        private static SkillEffectData  BuildUltEffect_1    (int inSkillLv, Configs_UltSkillData inUltSkill_C )             // 创建大招技能特效_1   
        {
            SkillEffectData             TheSkillE           = GetSkillEffect(inUltSkill_C.Effect1_1);                                             /// 技能特效

            TheSkillE.SkillLevel                            = inSkillLv;                                                                            /// 技能等级
            TheSkillE.SkillCD                               = inUltSkill_C.CD1_1 <= 0 ? BattleParmConfig.MaxCDTime : inUltSkill_C.CD1_1;            /// 技能CD
            TheSkillE.DamageRate                            = inUltSkill_C.Coefficient1_1;                                                          /// 伤害系数
            TheSkillE.BaseDamage                            = inUltSkill_C.BaseValue1_1 + inUltSkill_C.UpValue1_1 * (Math.Max(inSkillLv,1) - 1);    /// 基础伤害
            TheSkillE.BuffName                              = inUltSkill_C.Expression1;                                                             /// Buff名称
            TheSkillE.BonePos                               = inUltSkill_C.Position1;                                                               /// 骨骼位置

            return                      TheSkillE;
        }
        private static SkillEffectData  BuildUltEffect_2    (int inSkillLv, Configs_UltSkillData inUltSkill_C )             // 创建大招技能特效_2   
        {
            SkillEffectData             TheSkillE           = GetSkillEffect(inUltSkill_C.Effect1_2);                                             /// 技能特效

            TheSkillE.SkillLevel                            = inSkillLv;                                                                            /// 技能等级
            TheSkillE.SkillCD                               = inUltSkill_C.CD1_2 <= 0 ? BattleParmConfig.MaxCDTime : inUltSkill_C.CD1_2;            /// 技能CD
            TheSkillE.DamageRate                            = inUltSkill_C.Coefficient1_2;                                                          /// 伤害系数
            TheSkillE.BaseDamage                            = inUltSkill_C.BaseValue1_2 + inUltSkill_C.UpValue1_2 * (Math.Max(inSkillLv,1) - 1);    /// 基础伤害
            TheSkillE.BuffName                              = inUltSkill_C.Expression2;                                                             /// Buff名称
            TheSkillE.BonePos                               = inUltSkill_C.Position2;                                                               /// 骨骼位置

            return                      TheSkillE;
        }

        private static SkillEffectData  GetSkillEffect      (string inBuffType)                                             // 获取技能特效BUFF类型 
        {
            switch (inBuffType.ToLower())
            {
                case "magicdamage":                                                                                         /// 魔法伤害         
                case "physicaldamage":                                                                                      /// 物理伤害     
                case "smashmagicdamage":                                                                                    /// 魔法重伤     
                case "smashphysicaldamage":                                                                                 /// 物理重伤  
                case "cure":                                                                                                /// 治疗
                case "kill":                                                                                                /// 斩杀
                case "rangemagicdamage":                                                                                    /// 范围魔法伤害
                case "rangephysicaldamage":                                                                                 /// 范围物理伤害
                case "flight":                                                                                              /// 浮空
                case "launcher":                                                                                            /// 挑飞
                case "repulse":                                                                                             /// 击退
                case "venom":                                                                                               /// 毒灭
                case "leishen":                                                                                             /// 雷灭
                case "ranmie":                                                                                              /// 燃灭
                case "silie":                                                                                               /// 撕裂
                case "immunephysicaldamage":                                                                                /// 物理免疫
                case "immunemagicdamage":                                                                                   /// 魔法免疫
                case "immunedamage":                                                                                        /// 免疫伤害
                case "cleave":                                                                                              /// 分裂
                case "stealenergy":                                                                                         /// 偷取能量
                case "energydown":                                                                                          /// 能量降低
                case "wipebuff":                                                                                            /// 去除增益BUFF
                case "wipedebuff":              return new SkillEffectData() { SkillBuffType = SkillBuffType.DontBuff };    /// 去除减益BUFF              <| 不是Buff |>

                case "chains":                                                                                              /// 枷锁
                case "dizziness":                                                                                           /// 眩晕
                case "landification":                                                                                       /// 石化
                case "freeze":                                                                                              /// 冻结
                case "twine":                                                                                               /// 缠绕
                case "daze":                                                                                                /// 迷乱
                case "forbidmagic":                                                                                         /// 禁魔
                case "lastdamage":                                                                                          /// 持续伤害
                case "speeddown":                                                                                           /// 降低攻速
                case "physicalattackdown":                                                                                  /// 物理攻击力降低
                case "magicattackdown":                                                                                     /// 魔法强度降低
                case "physicalarmordown":                                                                                   /// 物理物价降低
                case "magicarmordown":          return new SkillEffectData() { SkillBuffType = SkillBuffType.BufferDown };      /// 魔法护甲降低              <| 减益Buff |>


                case "sneershield":                                                                                         /// 嘲讽护盾
                case "reboundshield":                                                                                       /// 反弹护盾
                case "hidedodge":                                                                                           /// 隐匿护盾
                case "tenacityshield":                                                                                      /// 韧性护盾
                case "simpleburialshield":                                                                                  /// 薄葬护盾
                case "damageshield":                                                                                        /// 伤害护盾
                case "skillemphasis":           return new SkillEffectData() { SkillBuffType = SkillBuffType.BufferNeutral };   /// 技能强化                  <| 无法去除-Buff |>


                case "lastcure":                                                                                            /// 持续加血       
                case "speedup":                                                                                             /// 攻速提升
                case "damageup":                                                                                            /// 伤害提升
                case "damagereduction":                                                                                     /// 伤害减免
                case "physicaldamageup":                                                                                    /// 物理伤害提升
                case "physicaldamagereduction":                                                                             /// 物理伤害减免
                case "magicdamageup":                                                                                       /// 魔法伤害提升
                case "normalattackdamageup":                                                                                /// 普通攻击伤害提升
                case "physicalattackup":                                                                                    /// 物理攻击提升
                case "MagicAttackUp":                                                                                       /// 魔法攻击力提升
                case "physicalarmorup":                                                                                     /// 物理护甲提升
                case "magicarmorup":                                                                                        /// 魔法护甲提升
                case "criticalup":              return new SkillEffectData() { SkillBuffType = SkillBuffType.BufferNeutral };   /// 暴击提升                  <| 增益Buff |>   

                default:
                    Debuger.LogError("BUFF_NUll !" + inBuffType + ": 不可识别buff特效类型");
                    return new SkillEffectData();

            }
        }
        #endregion
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  主动技能 </summary>
    public class ActiveSkillData    
    {
        public ObscuredInt              SkillLv                         = 0;                                                /// 技能等级
        public ObscuredInt              SkillID                         { get { return ActSkill_D.ActiveSkillID; } }        /// 技能ID
        public ObscuredInt              ReleaseTime                     { get { return ActSkill_D.ReleaseTime; } }          /// 释放时间
        public string                   SkillName                       { get { return ActSkill_D.SkillName; } }            /// 技能名称
        public string                   AttackRange                     { get { return ActSkill_D.Range; } }                /// 攻击范围

        public Configs_ActiveSkillData  ActSkill_D                      = null;                                             /// 主动技能配置
        public List<SkillEffectData>    SkillEffectList             = new List<SkillEffectData>();                          /// 技能列表
        public ActiveSkillData ( Configs_ActiveSkillData inActSkill_D)                                                      //  构造函数 
        {
            ActSkill_D                  = inActSkill_D; 
        }
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  大招技能数据 </summary>
    public class UltSkillData       
    {
        public ObscuredInt              SkillLevel                      = 0;                                                /// 技能等级
        public int                      SkillID                         { get { return UltSkill_C.UltSkillID; } }           /// 技能ID
        public int                      HitCount                        { get{ return UltSkill_C.DoubleHitNum; } }          /// 连击次数
        public string                   SkillName                       { get { return UltSkill_C.SkillName; } }            /// 技能名称
        public string                   AttackRange1                    { get { return UltSkill_C.Range1; } }               /// 攻击范围 1

        public Configs_UltSkillData     UltSkill_C                      = null;                                             /// 大招技能配置数据
        public List<SkillEffectData>    SkillEffectList             = new List<SkillEffectData>();                          /// 技能特效列表

        public                          UltSkillData( Configs_UltSkillData inUltSkill_C)                                        // 构造函数     
        {
            UltSkill_C                  = inUltSkill_C;
        }
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  Boss技能 </summary>
    public class BossSkillData      
    {
        public bool                     IsFired                         = false;                                            /// 释放

        public ObscuredInt              SkillLv                         = 0;                                                /// 技能等级
        public int                      SkillID                         { get { return ActiveSkill_C.ActiveSkillID; } }     /// 技能ID
        public string                   SkillName                       { get { return ActiveSkill_C.SkillName; } }         /// 技能名称
        public string                   AttackRange                     { get { return ActiveSkill_C.Range; } }             /// 攻击范围 

        public Configs_ActiveSkillData  ActiveSkill_C                   = null;                                             /// 主动技能配置数据
        public List<SkillEffectData>    SkillEffectList             = new List<SkillEffectData>();                          /// 技能特效列表
        public                          BossSkillData ( Configs_ActiveSkillData inActSkill_C)                               // 构造函数 
        {
            ActiveSkill_C               = inActSkill_C;
        }
    }

    
}
