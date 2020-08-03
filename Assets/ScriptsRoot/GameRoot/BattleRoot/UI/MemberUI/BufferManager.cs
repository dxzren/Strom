using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

namespace StormBattle
{
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> Buffer管理控制 </summary>
    public class BufferManagerItem : MonoBehaviour
    {
        public IBattleMemMediator        Owner                          { get; set; }                                       // 本体

        private List<BufferState>       _ShieldBuffList                 = new List<BufferState>()                           // 护盾Buff 列表 
        {
            BufferState.DamageShield,
            BufferState.HideDodge,
            BufferState.ReboundShield,
            BufferState.SimpleBuryShield,
            BufferState.SneerShield,
            BufferState.TenacityShield,
        };
        private List<BufferState>       _DizzBuffList                   = new List<BufferState>()                           // 昏眩Buff 列表 
        {
            BufferState.Dizziness,
            BufferState.Chains,
            BufferState.Daze,
            BufferState.ForbidMagic,
            BufferState.Freeze,
            BufferState.Landification,
            BufferState.Twine,
        };                               
        private Dictionary<BufferState, List<Buffer>> _BuffCollectDic   = new Dictionary<BufferState, List<Buffer>>();      // Buff 集合 字典


        private IEnumerator             RunBuffTickedTask   ( Buffer inBuff )                                               // 每秒buff运行完成任务  
        {
            while ( inBuff.CDTime >= 1 && inBuff.OnTicked != null )                                                         // 
            {
                inBuff.Ticked();
                inBuff.CDTime --;
                yield return new        WaitForSeconds(1);
            }
            if ( inBuff.CDTime > -10 )  ClearBuffer(inBuff,true);                                                           // buffer 中断不做完成结算
        }
        private IEnumerator             RunBufferTimer      ( Buffer inBuff)                                                // 运行buff CD    
        {
            if ( inBuff.CDTime > 0)     yield return new WaitForSeconds (inBuff.CDTime);
            if ( inBuff.CDTime > 0)     ClearBuffer (inBuff, true);

        }

        public float                    GetPlusBuffValue    ( BufferState inBuffState)                                      // 多个相同buff 累加 
        {
            float                       TheBuff = 0;
            GetBufferList(inBuffState).ForEach (P => TheBuff += P.Value);
            return                      TheBuff;
        }
        public float                    GetMultiBuffValue   ( BufferState inBuffState)                                      // 多个相同buff 累乘 
        {
            float                       TheBuff = 1;
            GetBufferList (inBuffState).ForEach (P => TheBuff *= P.Value);
            return                      TheBuff;
        }

        public bool                     IsContains          ( BufferState inBuffState )                                     // 查找buff         
        {
            return _BuffCollectDic.ContainsKey(inBuffState) && _BuffCollectDic[inBuffState].Count > 0;
        } 
        public bool                     IsAllowNormalAttack()                                                               // 允许普通攻击      
        {
            bool                        IsForbid        =   IsContains(BufferState.Dizziness) || IsContains(BufferState.Landification) ||
                                                            IsContains(BufferState.Freeze)    || IsContains(BufferState.Twine)         ||
                                                            IsContains(BufferState.Chains)    || IsContains(BufferState.Daze);
            return                      !IsForbid;
        }
        public bool                     IsAllowSkillAttack()                                                                // 允许技能攻击      
        {
            bool                        IsForbid        =   IsContains(BufferState.Dizziness) || IsContains(BufferState.Landification) ||
                                                            IsContains(BufferState.Freeze)    || IsContains(BufferState.Twine)         ||
                                                            IsContains(BufferState.Chains)    || IsContains(BufferState.Daze)          ||
                                                            IsContains(BufferState.ForbidMagic);
            return                      !IsForbid;
        }
        public bool                     IsAllowUltAttack()                                                                  // 允许大招攻击      
        {
            bool                        IsForbid        =   IsContains(BufferState.Dizziness) || IsContains(BufferState.Landification) ||
                                                            IsContains(BufferState.Freeze)    || IsContains(BufferState.Twine)         ||
                                                            IsContains(BufferState.Chains)    || IsContains(BufferState.Daze)          ||
                                                            IsContains(BufferState.ForbidMagic);
            return                      !IsForbid;
        }

        public Buffer                   GetBuffer           ( BufferState inBuffState)                                      // 获取buff         
        {
            if (!_BuffCollectDic.ContainsKey(inBuffState))
            {
                _BuffCollectDic.Add     (inBuffState, new List<Buffer>());
                return                  null;
            }              
            return                      _BuffCollectDic[inBuffState][0];
        }
        public List<Buffer>             GetBufferList       ( BufferState inBuffState )                                     // 获取buffList     
        {
            if (!_BuffCollectDic.ContainsKey(inBuffState))              _BuffCollectDic.Add(inBuffState, new List<Buffer>());
            return                                                      _BuffCollectDic[inBuffState];
        }

        public void                     AddBuff         ( Buffer inBuff, bool inIsReplace = false, bool inIsTicked = false) // 添加 Buff        
        {
            if (_ShieldBuffList.Contains(inBuff.BuffState))                                                                 /// 护盾类buff: 替换全部护盾类    
            {
                foreach (var Item in _ShieldBuffList)
                {
                    if (_BuffCollectDic.ContainsKey(Item))
                    {
                        List<Buffer>    TheBufferList                      = _BuffCollectDic[Item];                         /// BufferList
                        if (inIsReplace)                                                                                    /// 是否替换
                        {
                            TheBufferList.ForEach(P => P.Break());
                            TheBufferList.Clear();
                        }
                        _BuffCollectDic.Remove(Item);                                                                       /// DIC中 移除护盾 Buff
                    }
                }
            }
            else if (_DizzBuffList.Contains(inBuff.BuffState))                                                              /// 昏眩类buff: 替换特效,其他不变 
            {
                foreach (var Item in _DizzBuffList)
                {
                    if (_BuffCollectDic.ContainsKey(Item))
                    {
                        List<Buffer>    TheBuffList                         = _BuffCollectDic[Item];                        /// BufferList
                        if (inIsReplace)                                    TheBuffList.ForEach(P => P.CloseEffect());      /// 只关闭特效                        
                    }
                }
                if (_BuffCollectDic.ContainsKey(inBuff.BuffState)) _BuffCollectDic[inBuff.BuffState].Add(inBuff);
                else _BuffCollectDic.Add(inBuff.BuffState, new List<Buffer>() { inBuff });
            }
            else if (_BuffCollectDic.ContainsKey(inBuff.BuffState))                                                         ///     
            {
                List<Buffer> TheBuffList = _BuffCollectDic[inBuff.BuffState];
                if (inIsReplace)
                {
                    TheBuffList.ForEach(P => P.Break());
                    TheBuffList.Clear();
                }
                TheBuffList.Add(inBuff);
            }
            else     _BuffCollectDic.Add(inBuff.BuffState, new List<Buffer>() { inBuff });                                  /// 其他类buff:

            if (inBuff.SkillEffe.EffectName.Length > 0 && inBuff.SkillEffe.EffectName != "0")
                inBuff.MemEffe = Owner.MemAnimEffect.PlayBuff_E(inBuff.SkillEffe.EffectName, inBuff.SkillEffe.BonePos);
            if (inBuff.SkillEffe.SkillBuffType != SkillBuffType.DontBuff)
            {
                if (inIsTicked) inBuff.TaskRunning = StartCoroutine(RunBuffTickedTask(inBuff));
                else inBuff.TaskRunning = StartCoroutine(RunBufferTimer(inBuff));
            }
        }
        public void                     ClearBuffer     ( Buffer inBuff, bool inIsComplete = false)                         // 清理所有buff,停止任务协程,不执行任何回调
        {
            if (_BuffCollectDic.ContainsKey(inBuff.BuffState))
            {
                if ( inBuff.TaskRunning != null )                       StopCoroutine (inBuff.TaskRunning);
                _BuffCollectDic[inBuff.BuffState].Remove(inBuff);

                if ( inIsComplete)                                      inBuff.Complete();
            }
        }
        public void                     ClearBuffer     ( bool inIsComplete = false )                                       // 清理所有buff(重载)
        {
            foreach ( var Item in _BuffCollectDic.Values )
            {
                Item.ForEach ( P =>
                {
                    if (P.TaskRunning != null)                          StopCoroutine(P.TaskRunning);
                    if (inIsComplete)                                   P.Complete();
                });
                Item.Clear();
            }
            _BuffCollectDic.Clear();
        }
        public void                     StopBuff        ( BufferState inBuffState, bool inIsComplete)                       // 停止buff   
        {
            List<Buffer>                TheBuffList                     = GetBufferList(inBuffState);
            TheBuffList.ForEach (P =>
            {
                if (inIsComplete)                                       P.Complete();
                else                                                    P.Break();
            });
            TheBuffList.Clear();
        }
        public void                     StopBuff        ( SkillBuffType inSkBuffType, bool inIsComplete )                   // 停止buff (重载) 
        {
            foreach ( var Item in _BuffCollectDic)
            {
                if (Item.Value.Count > 0)
                {
                    Buffer              TheBuff                             = Item.Value[0];
                    if (TheBuff.SkillEffe.SkillBuffType == inSkBuffType)    StopBuff(Item.Key, inIsComplete);
                }
            }
        }
    }
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> Buffer数据 </summary>
    public class Buffer
    {

        public BufferState              BuffState                       = BufferState.None;                         /// Buff状态
        public IBattleMemMediator       Owner                           = null;                                     /// 本体
        public IBattleMemMediator       Attacker                        = null;                                     /// 攻击方

        public ObscuredFloat            Value                           = 0;                                        /// 数值
        public ObscuredFloat            CDTime                          = 0;                                        /// CD时间

        public SkillEffectData          SkillEffe                       = new SkillEffectData();                    /// 技能特效
        public MemberEffect             MemEffe                         = null;                                     /// 成员特效
         
        public UnityEngine.Coroutine    TaskRunning                     = null;                                     /// 定时协程
        public Action                   OnComplete                      = null;                                     /// buff执行完成
        public Action                   OnBreak                         = null;                                     /// buff中断_护盾打破,buff清除
        public Action<Buffer>           OnTicked                        = null;                                     /// 执行事件

        public void                     Complete()                                                                  // 完成           
        {
            CloseEffect();
            if (OnComplete != null)     OnComplete();
        }
        public void                     Ticked()                                                                    // 每秒执行的事件  
        {
            if (OnTicked != null)       OnTicked(this);
        }
        public void                     Break()                                                                     // 断开Buff       
        {
            BuffClose();
            if (OnBreak != null)        OnBreak();
        }
        public void                     BuffClose()                                                                 // 关闭buff       
        {
            CDTime                      = 0;
            CloseEffect();
        }
        public void                     CloseEffect()                                                               // 关闭特效        
        {
            if (MemEffe != null)
            {
                MemEffe.DestEffect();
                MemEffe                 = null;
            }
        }
    }
    public enum BufferState
    {
        Chains                          = 0,                // 禁锢 - 停止 技能和普攻CD\ 播放 禁锢特效  \-- Hit
        Dizziness                       = 1,                // 眩晕 - 停止 技能和普攻CD\ 播放 眩晕特效  \-- Hit
        Landification                   = 2,                // 石化 - 停止 技能和普攻CD\ 播放 石化特效  \-- Hit
        Freeze                          = 3,                // 冻结 - 停止 技能和普攻CD\ 播放 冻结特效  \-- Hit
        Twine                           = 4,                // 缠绕 - 停止 技能和普攻CD\ 播放 缠绕特效  \-- Hit
        Daze                            = 5,                // 迷乱 - 停止 技能和普攻CD\ 播放 迷乱特效  \-- Hit

        ForbidMagic                     = 6,                // 禁魔 --停止 所有技能CD  \ 大招技能 失效  \-- Hit

        SneerShield                     = 7,                // 嘲讽护盾              \\-- Hit
        ReboundShield                   = 8,                // 反弹护盾              \\-- Hit
        HideDodge                       = 9,                // 隐匿闪避              \\-- Hit
        TenacityShield                  = 10,               // 韧性护盾              \\-- Hit
        SimpleBuryShield                = 11,               // 薄葬护盾              \\-- Hit
        DamageShield                    = 12,               // 伤害护盾              \\-- Hit

        SkillEmphasis                   = 13,               // 技能强化              ||-- Attack
        LastDamage                      = 14,               // 持续伤害              \\-- Hit
        LastCure                        = 15,               // 持续治疗              \\-- Hit
        Venom                           = 16,               // 毒强化 - 50% 魔法伤害  ||-- Attack  
        Thunder                         = 17,               // 雷强化 - 50% 魔法伤害  ||-- Attack
        FireDust                        = 18,               // 火强化 - 50% 魔法伤害  ||-- Attack

        SpeedDown                       = 19,               // 攻速降低              ||-- Attack
        SpeedUp                         = 20,               // 攻速提升              ||-- Attack
        DamageUp                        = 21,               // 伤害提升              \\-- Hit
        DamageDown                      = 22,               // 伤害减少              \\-- Hit
        PhyDamageUP                     = 23,               // 物理伤害提升          ||-- Attack
        PhyDamageDown                   = 24,               // 物理伤害减少          \\-- Hit
        MagicDamageUp                   = 25,               // 魔法伤害提升          ||-- Attack
        MagicDamgaeDown                 = 26,               // 魔法伤害降低          ||-- Attack
        NormalAttackDamageUp            = 27,               // 普通攻击伤害提升      ||-- Attack

        NorDamageSilie                  = 28,               // 普通伤害永久强化      ||-- Attack
        ImmunePhyDamage                 = 29,               // CD内 免疫物理伤害     \\-- Hit
        ImmuneMagicDamage               = 30,               // CD内 免疫魔法伤害     \\-- Hit
        ImmuneDamage                    = 31,               // CD内 免疫所有伤害     \\-- Hit
        PhyAttackUp                     = 32,               // 物理攻击提升          ||-- Attack
        PhyAttackDown                   = 33,               // 物理攻击降低          ||-- Attack
        MagicAttackDown                 = 34,               // 魔法强度降低          ||-- Attack
        MagicAttackUp                   = 35,               // 魔法强度提升          ||-- Attack

        PhyArmorUp                      = 36,               // 物理护甲提升          \\-- Hit
        PhyArmorDown                    = 37,               // 物理物价降低          \\-- Hit
        MagicArmorDown                  = 38,               // 魔法护甲降低          \\-- Hit
        MagicArmorUp                    = 39,               // 魔法护甲提升          \\-- Hit
        CritUp                          = 40,               // 暴击提升              ||-- Attack
        Cleave                          = 41,               // 分裂攻击              ||-- Attack
        None                            = 99,               // 空
    }
}

