using UnityEngine;
using System.Collections;
                                                                                                                            /// 主界面系统 字符串常量
public class MainUIStrConst
{
    #region================================================||   SpriteName -- < 图集子名 > ||<FourNode>================================================
                                                                                                                            /// < @英雄边框 SpriteName >
    public const string             HeroFrameQuality_White              = "touxiang-bai-84";                                /// 白
    public const string             HeroFrameQuality_Green              = "touxiang-lv-84";                                 /// 绿
    public const string             HeroFrameQuality_Green1             = "touxiang-lv+1-84";                               /// 绿+1
    public const string             HeroFrameQuality_Blue               = "touxiang-lan-84";                                /// 蓝
    public const string             HeroFrameQuality_Blue1              = "touxiang-lan+1-84";                              /// 蓝+1
    public const string             HeroFrameQuality_Blue2              = "touxiang-lan+2-84";                              /// 蓝+2
    public const string             HeroFrameQuality_Purple             = "touxiang-zi-84";                                 /// 紫
    public const string             HeroFrameQuality_Purple1            = "touxiang-zi+1-84";                               /// 紫+1
    public const string             HeroFrameQuality_Purple2            = "touxiang-zi+2-84";                               /// 紫+2
    public const string             HeroFrameQuality_Purple3            = "touxiang-zi+3-84";                               /// 紫+3
    public const string             HeroFrameQuality_Gold               = "touxiang-jin-84";                                /// 金
                                                                                                                            /// < @英雄元素相性 SpriteName >
    public const string             HeroPolarity_Ice                    = "departicon_ice";                                 /// 冰
    public const string             HeroPolarity_Fire                   = "departicon_fire";                                /// 火
    public const string             HeroPolarity_Thunder                = "departicon_thunder";                             /// 雷
    #endregion

    #region================================================||   StrName -- < 字符串名称 > ||<FourNode>================================================

    public const string             HeroAtt_Power                       = "力量";                                           /// 力量                                            
    public const string             HeroAtt_Agile                       = "敏捷";                                           /// 敏捷     
    public const string             HeroAtt_Intellect                   = "智力";                                           /// 智力

    public const string             HeroAtt_Hp                          = "生命";                                           /// 生命
    public const string             HeroAtt_PhyAttack                   = "物理攻击";                                       /// 物理攻击
    public const string             HeroAtt_MagicAttack                 = "魔法攻击";                                       /// 魔法攻击
    public const string             HeroAtt_PhyArmor                    = "物理护甲";                                       /// 物理护甲
    public const string             HeroAtt_MagicArmor                  = "魔法抗性";                                       /// 魔法抗性
    public const string             HeroAtt_PhyCrit                     = "物理暴击";                                       /// 物理暴击
    public const string             HeroAtt_MagicCrit                   = "魔法暴击";                                       /// 魔法暴击
    public const string             HeroAtt_ThroughPhyArmor             = "物理穿透";                                       /// 物理穿透

    public const string             HeroAtt_EnergyRegen                 = "能力回复";                                       /// 能力回复
    public const string             HeroAtt_BloodRegen                  = "生命回复";                                       /// 生命回复
    public const string             HeroAtt_SuckBlood                   = "吸血";                                          /// 吸血
    public const string             HeroAtt_Hit                         = "命中";                                          /// 命中
    public const string             HeroAtt_Dodge                       = "闪避";                                          /// 闪避
    #endregion
}
