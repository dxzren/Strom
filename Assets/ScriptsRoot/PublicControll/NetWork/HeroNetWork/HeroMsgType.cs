using UnityEngine;
using System.Collections;
/// <summary>  英雄系统消息命令  </summary>

enum HERO_CMD
{
    REQ_HERO_WearEquip          = 0,                    // 穿戴装备请求
    REQ_HERO_UpQuality          = 1,                    // 升级品质请求
    REQ_HERO_UpStarLv           = 2,                    // 升级星级请求
    REQ_HERO_UPSkill            = 3,                    // 升级技能请求
    REQ_HERO_UpMedal            = 4,                    // 升级勋章请求
    REQ_HERO_UpWing             = 5,                    // 升级翅膀请求

    RET_SYNC_HERO_AllHeroInfo   = 50,                   // 同步所有英雄信息回调
    RET_HERO_AddHero            = 51,                   // 添加英雄回调
    RET_SYNC_HERO_MedalLvExp    = 52,                   // 同步勋章等级和经验回调
    RET_SYNC_HERO_HeroLvExp     = 53,                   // 同步英雄等级和经验回调
    RET_HERO_WearEquip          = 54,                   // 穿戴装备回调
    RET_HERO_UpQuality          = 55,                   // 升级品质回调
    RET_HERO_UpStarLv           = 56,                   // 升级星级回调
    RET_HERO_UpSkill            = 57,                   // 升级技能回调
    RET_HERO_UpWing             = 58,                   // 升级翅膀回调
    
}