using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_HERO_Info                                                 // 英雄信息回调                          
{
    public Head head;
    public int nHerosNum;                               // 英雄总数 
    public int npackgeIndex;                            // 数据包索引
    public int npackgeNum;                              // 本条实际数量
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
    public HERO_DATA[] heroDataList;                    // 英雄数据列表
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_HERO_DATA                                                 // 请求英雄数据回调(添加新英雄)           
{
    public Head head;
    public int nErrId;                                  // 错误码 (0: 成功 ; 1 2 3...失败原因)
    public HERO_DATA HeroData;                          // 英雄数据
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_HeroWearEquip                                                    // 英雄穿戴装备请求                      
{
    public Head head;

    public int nHeroID;                                 // 英雄ID
    public int nEquipPos;                               // 装备糟位置
    public int nEquipID;                                // 装备ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct RET_HeroWearEquip                                                    // 英雄穿戴装备回调                      
{
    public Head head;
    public int nHeroID;                                 // 英雄ID
    [MarshalAs(UnmanagedType.AsAny, SizeConst = 6)]
    public int[] HeroEquipList;                         // 英雄装备列表
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_HeroUpQuality                                                    // 英雄升级品质(升阶)请求                 
{
    public Head head;
    public int nHeroID;                                 // 英雄ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct RET_HeroUpQuality                                                    // 英雄升级品质(升阶)回调                 
{
    public Head head;
    public int nHeroID;
    public int nQuality;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public int[] nEquips;
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_HeroUpStar                                                       // 英雄升级星级请求                      
{
    public Head head;
    public int nHeroID;                                  // 英雄ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct RET_HeroUpStar                                                       // 英雄升级星级回调                      
{
    public Head herd;
    public int nHeroID;                                  // 英雄ID
    public int nHeroStarLv;                             // 英雄星级
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_HeroUpSkill                                                      // 英雄升级技能请求                      
{
    public Head head;
    public int nHeroID;                                 // 英雄ID
    public int nSkillID;                                // 升级技能ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct RET_HeroUpSkill                                                      // 英雄升级技能回调                      
{
    public Head head;
    public int nHeroID;                                 // 英雄ID
    public int nSkillID;                                // 升级的技能ID
    public int nSkillLv;                                // 升级的技能等级
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_HeroUpMedal                                                      // 英雄勋章升级请求                      
{
    public Head head;
    public int nHeroID;                                 // 英雄ID
    public int nNum;                                    // 物品ID列表元素个数
    [MarshalAs(UnmanagedType.ByValArray,SizeConst = 8)] 
    public ItemMedalData[] HeroItem;                    // 物品ID列表
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct RET_HeroUpMedal                                                      // 英雄勋章升级回调                      
{
    public Head head;                                   
    public int nHeroID;                                 // 英雄ID
    public int nMedalLv;                                // 勋章等级
    public int nMedalExp;                               // 勋章经验
}


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_HeroUPWing                                                       // 英雄升级翅膀请求                      
{
    public Head head;
    public int nHerodID;                                // 英雄ID
    public int nHeroPropID;                             // 升级翅膀道具ID
    public bool IsUseProtect;                           // 是否开启保护
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct RET_HeroUPWing                                                       // 英雄升级翅膀返回                      
{
    public Head head;
    public int nHeroID;                                 // 英雄ID
    public int nWingID;                                 // 翅膀ID
    public int nWingRate;                               // 翅膀祝福值
}


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct SYNC_HeroLvExp                                                       // 同步英雄经验等级                      
{
    public Head head;       
    public int nHeroID;                                 // 英雄ID
    public int nHeroLv;                                 // 英雄等级
    public int nHeroExp;                                // 英雄经验
}


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct HERO_DATA                                                     // 英雄信息                             
{
    public int nHeroID;                                 // 1.英雄ID
    public int nHeroLv;                                 // 2.英雄等级
    public int nHeroExp;                                // 3.英雄经验
    public int nHeroStarLv;                             // 4.英雄星级

    public int nHeroQuality;                            // 5.英雄品质
    public int nMedalLv;                                // 6.英雄勋章等级
    public int nMedalExp;                               // 7.英雄勋章经验
    public int nWingID;                                 // 8.英雄翅膀ID

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public int[] nHeroEquips;                           // 9.英雄装备列表
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] nHeroSkillLv;                          // 10.英雄技能等级
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct ItemMedalData                                                 // 勋章强化材料数据                      
{
    public int itemID;
    public short num;
}
