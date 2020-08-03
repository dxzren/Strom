using UnityEngine;
using System.Collections;
/// <summary> 英雄系统事件 </summary>
public class HeroSysEvent
{
    public static string REQMergeEquip_Event                = "REQMergerEquip_Event";                               // 合成装备请求
    public static string RETMergeEquip_Event                = "RETMergeEquip_Event";                                // 合成装备回调

    public static string REQMedalStrengthen_Event           = "REQMedalStrengthen_Event";                           // 勋章强化请求
    public static string RETMedalStrengthen_Event           = "RETMedalStrengthen_Event";                           // 勋章强化回调

    public static string SoulToHero_Event                   = "SoulToHero_Event";                                   // 魂石转换英雄

    public static string REQUpStar_Event                    = "REQUpStar_Event";                                    // 升星请求
    public static string RETUpStar_Event                    = "RETUpStar_Event";                                    // 升星回调

    public static string REQUpQuality_Event                 = "REQUpQuality_Event";                                 // 升级品质请求
    public static string RETUpQuality_Event                 = "RETUpQuality_Event";                                 // 升阶品质回调

    public static string REQWingStrengthen_Event            = "REQWingStrengthen_Event";                            // 强化翅膀请求
    public static string RETWingStrengthen_Event            = "RETWingStrengthen_Event";                            // 强化翅膀回调

    public static string REQUpSkillLv_Event                 = "REQUpSkillLv_Event";                                 // 购买技能请求
    public static string RETUpSkillLv_Event                 = "RETUpSkillLv_Event";                                 // 购买技能回调   

    public static string REQBuySkill_Event                  = "ETBuySkill_Event";                                   // 购买技能请求
    public static string RETBuySkill_Event                  = "ETBuySkill_Event";                                   // 购买技能回调

    public static string WearEquip_Event                    = "WearEquip_Event";                                    // 穿戴装备
            
    public static string REQDownWing_Event                  = "REQDownWing_Event";                                  // 下翅膀
    public static string RETDownWing_Event                  = "RETDownWing_Event";                                  // 下翅膀
            
    public static string RefreshUI_Event                    = "RefreshUI_Event";                                    // 更新UI

    public static string ShowWing_Event                     = "ShowWing_Event";                                     // 展示翅膀

    public static string DestoryMedalItem_Event             = "DestoryMedalItem_Evnet";                             // 销毁勋章材料
    public static string MedalItemClick_Event               = "MedalItemClick_Event";                               // 勋章材料点击

    public static string CloseUpStar_Event                  = "CloseUpStar_Event";                                  // 关闭升星界面

    public static string HeroShowDontShowText_Event         = "HeroShowDontShowText_Event";                         // 英雄窗口卡牌不显示英雄名称
    public static string HeroShowQualityFresh_Event         = "HeroShowQualityFresh_Event";                         // 英雄窗口英雄品质刷新
    public static string HeroUpdate_Event                   = "HeroUpdate_Event";                                   // 英雄获取装备信息更新
    public static string HeroEquipUIEffect_Event            = "HeroEquipUIPart_Event";                              // 英雄穿装备UI特效

    public static string HeroCardRed_Event                  = "HeroCardRed_Event";                                  // 英雄红点
    public static string HeroCardMainRed_Event              = "HeroCardMainRed_Event";                              // 英雄主界面红点
    public static string HeroInfoOut_Event                  = "HeroInfoOut_Event";                                  // 英雄信息位置变动
    public static string HeroInfoIn_Event                   = "HeroInfoIn_Event";                                   // 英雄信息位置变动
}
