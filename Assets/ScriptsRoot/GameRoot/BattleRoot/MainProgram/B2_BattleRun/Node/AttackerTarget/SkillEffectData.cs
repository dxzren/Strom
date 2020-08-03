using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 技能特效   </summary>
    public class SkillEffectData
    {
        public float            BaseDamage                      = 0;                                                    /// 基础伤害值
        public float            DamageRate                      = 0;                                                    /// 伤害系数比率
        public string           EffectName                      = "";                                                   /// 特效名称
        public string           BuffName                        { set; get; }                                           /// 技能名称
        public SkillBuffType    SkillBuffType                   { set; get; }                                           /// 技能作用类型

        public ObscuredInt      SkillCD                         = 0;                                                    /// 技能CD
        public ObscuredInt      BonePos                         = 0;                                                    /// 骨骼位置
        public ObscuredInt      SkillLevel                      = 1;                                                    /// 技能等级
        public virtual void     Exec(IBattleMemMediator         inAttacker, IBattleMemMediator defencer,                /// 基类虚方法 Exec执行了
                                     List<IBattleMemMediator>   inDefendList, int currtimes, int maxtimes)
        { }
    }

    public enum                 SkillBuffType
    {
        BufferUp                = 0,                                                                                    /// 正收益Buff
        BufferDown              = 1,                                                                                    /// 负受益Buff
        BufferNeutral           = 2,                                                                                    /// 中性Buff <定位不可清楚>
        DontBuff                = 3,                                                                                    /// 不是Buff
    }
}