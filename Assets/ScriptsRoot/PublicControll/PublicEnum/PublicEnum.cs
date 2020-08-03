using UnityEngine;
using System.Collections;
//--------------------- << 资源管理 >> --------------------------------------------------------------------------------------
public enum AssetBundleName                                                 // 所属AB(资源集合,资源包)的名字         
{
    config,                         // 1.配置表
    commonhero,                     // 2.英雄
    merc,                           // 3.佣兵
    npc,                            // 4.NPC(包括所有的小怪和Boss)
    wing,                           // 5.翅膀
    effect,                         // 6.特效
    scene                           // 7.场景

}
//--------------------- << 战斗系统关联 >> ----------------------------------------------------------------------------------
public enum BattleType                                                       // 战斗地图类型                          
{
    CheckPoint,                     // 01.主线关卡
    JJC,                            // 02.竞技场
    JJCDefence,                     // 03.竞技场防御阵容
    JJCLevel,                       // 04.竞技场等级挑战
    ParadiseRoad,                   // 05.天堂之路

    SecretTower,                    // 06.秘境塔
    DragonTrialIce,                 // 07.巨龙试炼冰龙
    DragonTrialFire,                // 08.巨龙试炼火龙
    DragonTrialThunder,             // 09.巨龙试炼雷龙
    MonsterWarPhy,                  // 10.巨兽囚笼物理

    MonsterWarMagic,                // 11.巨兽囚笼魔法
    Guild,                          // 12.公会
    GuideFirstBattle,               // 13.新手引导第一次战斗
    FriendWar                       // 14.好友挑战
}



public enum LineUpSequence                                                  // 阵容排列                              
{
    All,                            // 全部
    Font,                           // 前排
    Mid,                            // 中排
    Back,                           // 后排
    Mercen                          // 佣兵
}

//--------------------- << 玩家，英雄职业与属性 >> ---------------------------------------------------------------------------
public enum Race                                                            // 玩家种族                              
{
    Humy    = 1,
    Delue   = 2,
    Sprite  = 3,
    Swarf   = 4
}
public enum Gender                                                          // 性别                                  
{
    Men   = 1,
    Women = 2
}
public enum HeroType                                                        // 英雄类型                              
{
    Lead      = 1,          // 1.主角      
    Exclusive = 2,          // 2.专属
    Neutrl    = 3,          // 3.中立
    Merc      = 4,          // 4.佣兵
    NPC       = 5           // 5.NPC
}
public enum HeroQuality                                                     // 英雄品质                              
{
    White   = 1,                    // 01.白色
    Green   = 2,                    // 02.绿色
    Green1  = 3,                    // 03.绿色+1
    Blue    = 4,                    // 04.蓝色
    Blue1   = 5,                    // 05.蓝色+1
    Blue2   = 6,                    // 06.蓝色+2
    Purple  = 7,                    // 07.紫色
    Purple1 = 8,                    // 08.紫色+1
    Purple2 = 9,                    // 09.紫色+2
    Purple3 = 10,                   // 10.紫色+3
    Gold    = 11                    // 11.金色
}
public enum TipsHero                                                        // 英雄 （Tips显示）                     
{
    newHero = 501,          // 新英雄   
    boss    = 502,          // Boss
    hero    = 503,          // 英雄
    npc     = 504           // NPC
}
public enum Profession                                                      // 英雄类型                              
{
    Power           = 1,            // 1.力量
    Agile           = 2,            // 2.敏捷
    Intelligence    = 3             // 3.智力
}
public enum Polarity                                                        // 元素属性                              
{
    Ice       = 1,                  // 1.冰
    Fire      = 2,                  // 2.火
    Thunder   = 3,                  // 3.雷
}
public enum HeroAttribType_Main                                             // 英雄主属性                            
{
    Blood           = 1,                // 生命值
    PhyAttack       = 2,                // 物理攻击
    MagicAttack     = 3,                // 魔法攻击
    PhyArmor        = 4,                // 物理护甲
    MagicArmor      = 5,                // 魔法护甲

    PhyCrit         = 6,                // 物理暴击
    MagicCrit       = 7,                // 魔法暴击
    ThroughPhyArmor = 8,                // 物理护甲穿透
    EnergyRegen     = 9,                // 能量恢复
    BloodRegen      = 10,               // 生命回复

    SuckBlood       = 11,               // 吸血
    Hit             = 12,               // 命中
    Dodge           = 13,               // 闪避
}
public enum HeroAttribType_Lv1                                              // 英雄1级属性                           
{
    Power       = 101,                                  // 力量
    Agile       = 102,                                  // 敏捷
    Intellect   = 103                                   // 智力
}
public enum HeroPorAtt                                                      // 英雄职业属性
{
    Power       = 1,                                    // 力量
    Agile       = 2,                                    // 敏捷
    Intellect   = 3                                     // 智力
}

//--------------------- << 装备物品类 >> -----------------------------------------------------------------------------------
public enum CurrencyType                                                    // 货币类型                              
{
    diamonds            = 101,            // 101.钻石
    coins               = 102,            // 102.金币
    stamina             = 103,            // 103.体力
    playerExp           = 104,            // 104.玩家经验
    hero                = 105,            // 105.英雄
    heroExp             = 106,            // 106.英雄经验
    secretTowerCoins    = 107,            // 107.秘境塔金币
    JJCCoins            = 108,            // 108.竞技场金币
    ParadiseRoadCoins   = 109             // 109.天堂之路金币
    
}
public enum ItemType                                                        // 物品类型                              
{
    None                = 0,            // 00.未指定初始值
    equip               = 1,            // 01.装备
    equipFragment       = 2,            // 02.装备碎片
    scroll              = 3,            // 03.卷轴
    scrollFragment      = 4,            // 04.卷轴碎片
    soul                = 5,            // 05.魂石
            
    coinsprop           = 6,            // 06.金币道具
    heroExpProp         = 7,            // 07.英雄经验道具
    medalExpProp        = 8,            // 08.勋章经验道具
    wingExpProp         = 9,            // 09.翅膀经验道具
    ticket              = 10,           // 10.扫荡券
        
    wing                = 11,           // 11.翅膀
    staminaProp         = 12,           // 12.体力道具
    protectedstone      = 13,           // 13.保护石
    jinjiestone         = 14,           // 14.进阶石
    mercExpProp         = 15,           // 15.佣兵经验道具
            
    SkillProp           = 16,           // 16.技能道具
    soulbag             = 17,           // 17.魂石包
    diamondsbag         = 18,           // 18.钻石包

    diamonds            = 101,          // 101.钻石
    coins               = 102,          // 102.金币
    stamina             = 103,          // 103.体力
    playerExp           = 104,          // 104.玩家经验
    hero                = 105,          // 105.英雄
    heroExp             = 106,          // 106.英雄经验
    SecretTowerCoins    = 107,          // 107.秘境塔货币
    JJCCoins            = 108,          // 108.竞技场货币
    ParadiseRoadCoins   = 109,          // 109.天堂之路货币
    WingExp             = 110           // 110.天使遗羽（翅膀升级）

}
public enum ItemQuality                                                     // 物品品质                              
{
    White = 1,                      // 白
    Green = 2,                      // 绿
    Blue  = 3,                      // 蓝
    Puru  = 4,                      // 紫
    Gold  = 5                       // 金

}
public enum WearPosition                                                    // 装备穿戴位置                          
{
    LeftTop     = 0,                    // 1.左上
    RightTop    = 1,                    // 2.右上
    LeftMid     = 2,                    // 3.左中
    RightMid    = 3,                    // 4.右中
    LeftBottom  = 4,                    // 5.左下
    RightBottom = 5                     // 6.右下
}
public enum SkillType                                                       // 技能类型                              
{
    UltSkill     = 0,       //大招技能
    ActiveSkill1 = 1,       //主动技能1
    ActiveSkill2 = 2,       //主动技能2
    PassiveSkill = 3        //被动技能
}
public enum EquipButtonState                                                // 装备按钮状态缓存                      
{
    Weard     = 0,          // 已穿戴
    NoEquip   = 1,          // 没有装备
    HaveEquip = 2,          // 有装备
    CanWear   = 3,          // 背包中有装备且英雄等级充足
    CanMerge  = 4,          // 可合成
}

//--------------------- << 系统,关卡,副本 >> --------------------------------------------------------------------------------
public enum ChapterType                                                     // 章节类型
{
    Normal = 1,                     // 普通关卡
    Elite  = 2                      // 精英关卡
}


//--------------------- << 服务器错误码 >> ----------------------------------------------------------------------------------
public enum LOGIN_ERR_RET                                                   // 客户端登录请求，验证码返回错误        
{
    LOGIN_ERR_NO_ERR       = 0,         // 0.验证无误
    LOGIN_ERR_VERSION      = 1,         // 1.版本不符
    LOGIN_ERR_ACCOUNT      = 2,         // 2.帐号错误
    LOGIN_ERR_PASSWORD     = 3,         // 3.密码错误
    LOGIN_ERR_FORBIDACC    = 4,         // 4.帐号禁止登录
    LOGIN_ERR_FORBIDIP     = 5,         // 5.IP禁止登录
    LOGIN_ERR_SESSION      = 6,         // 6.会话错误
    LOGIN_ERR_SVR_OFFLINE  = 7,         // 7.错误的或者服务器线路关闭
    LOGIN_ERR_SRV_FULL     = 8,         // 8.服务器人数已满
    LOGIN_ERR_DATA_INVALID = 9          // 9.平台验证失败
}
public enum eError                                                          // 服务器错误码                          
{
    // 系统预留 0-99
    NO_MSG_ERR = 0,                                                         /**< 无错误 */
    MSG_ERR_GLOBAL_CFG,                                                     /**< 常量配置表数据错误 */


    // 角色模块段 100-199
    MSG_ERR_ROLE_CREATE_MSG_FAIL = 100,                                     /**< 创建消息实例失败 */
    MSG_ERR_ROLE_NO_MSG_MATCH = 101,                                        /**< 消息路由时无匹配项 */
    MSG_ERR_ROLE_NO_ROLE_INSTANCE = 102,                                    /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_ROLE_MISMATCH_RACE = 103,                                       /**< 创建角色时角色职业非法 */
    MSG_ERR_ROLE_MISMATCH_NAME_LEN = 104,                                   /**< 创建角色时角色名长度非法 */
    MSG_ERR_ROLE_ILLEGAL_NAME = 105,                                        /**< 创建角色时角色名字非法 */
    MSG_ERR_ROLE_EXIST_ROLE = 106,                                          /**< 创建角色时已拥有多于1个角色存在 */
    MSG_ERR_ROLE_ACC_OR_STATUS = 107,                                       /**< 创建角色时，帐号已丢失或状态非法 */
    MSG_ERR_ROLE_STATUS = 108,                                              /**< 请求进入游戏时，角色当前状态非法 */
    MSG_ERR_ROLE_INVALID_NAME = 109,                                        /**< 请求角色数据时，角色名无效 */
    MSG_ERR_ROLE_INVALID_DB = 110,                                          /**< 请求角色数据时，数据库句柄无效 */
    MSG_ERR_ROLE_BE_DANGER = 111,                                           /**< 下发角色数据时，危险值超过预警 */
    MSG_ERR_ROLE_BUY_STRENGTH_MAX = 112,                                    /**< 购买体力——今日次数已达上限 */
    MSG_ERR_ROLE_BUY_STRENGTH_MONEY = 113,                                  /**< 购买体力——钻石不足 */
    MSG_ERR_ROLE_BUY_GOLD_MAX = 114,                                        /**< 购买金币——今日次数已达上限 */
    MSG_ERR_ROLE_BUY_GOLD_MONEY = 115,                                      /**< 购买金币——钻石不足 */
    MSG_ERR_ROLE_BUY_SP_NO_ZERO = 116,                                      /**< 购买技能点——点数非0 */
    MSG_ERR_ROLE_BUY_SP_MAX = 117,                                          /**< 购买技能点——今日次数已达上限 */
    MSG_ERR_ROLE_BUY_SP_MONEY = 118,                                        /**< 购买技能点——钻石不足 */


    // 宠物模块段 200-299
    MSG_ERR_PET_CREATE_MSG_FAIL = 200,                                      /**< 创建消息实例失败 */
    MSG_ERR_PET_NO_MSG_MATCH = 201,                                         /**< 消息路由时无匹配项 */
    MSG_ERR_PET_NO_ROLE_INSTANCE = 202,                                     /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_PET_DE_NO_PLAYER = 203,                                         /**< 穿装备-无角色实例 */
    MSG_ERR_PET_DE_EQUIP_POS = 204,                                         /**< 穿装备-装备槽索引越界 */
    MSG_ERR_PET_DE_NO_PET = 205,                                            /**< 穿装备-无宠物实例 */
    MSG_ERR_PET_DE_EQUIP_ID = 206,                                          /**< 穿装备-请求穿戴装备ID无效 */
    MSG_ERR_PET_DE_DRESS_LV = 207,                                          /**< 穿装备-宠物未达到装备的穿戴等级 */
    MSG_ERR_PET_DE_ALREADY_DRESS = 208,                                     /**< 穿装备-指定槽位已有装备 */
    MSG_ERR_PET_DE_DEL_BAG_DRESS = 209,                                     /**< 穿装备-从背包删除装备失败 */
    MSG_ERR_PET_UQ_NO_PLAYER = 210,                                         /**< 升级品质-无角色实例 */
    MSG_ERR_PET_UQ_NO_PET = 211,                                            /**< 升级品质-无宠物实例 */
    MSG_ERR_PET_UQ_REACH_MAX_LV = 212,                                      /**< 升级品质-已达品质上限 */
    MSG_ERR_PET_UQ_NO_DRESS_ALL = 213,                                      /**< 升级品质-未穿齐全套装备 */
    MSG_ERR_PET_USL_NO_PLAYER = 214,                                        /**< 升级星级-无角色实例 */
    MSG_ERR_PET_USL_NO_PET = 215,                                           /**< 升级星级-无宠物实例 */
    MSG_ERR_PET_USL_REACH_MAX_LV = 216,                                     /**< 升级星级-已达星级上限 */
    MSG_ERR_PET_USL_FIND_CFG = 217,                                         /**< 升级星级-查找升级所需道具失败 */
    MSG_ERR_PET_USL_DEL_BAG_ITEM = 218,                                     /**< 升级星级-从背包删除道具失败 */
    MSG_ERR_PET_USK_NO_PLAYER = 219,                                        /**< 升级技能-无角色实例 */
    MSG_ERR_PET_USK_NO_PET = 220,                                           /**< 升级技能-无宠物实例 */
    MSG_ERR_PET_USK_INVALID_LV = 221,                                       /**< 升级技能-角色等级不足无法升级技能 */
    MSG_ERR_PET_USK_UNLOCK = 222,                                           /**< 升级技能-技能未解锁，无法进行升级 */
    MSG_ERR_PET_USK_FIND_CFG = 223,                                         /**< 升级技能-查找升级所需道具失败 */
    MSG_ERR_PET_USK_REACH_MAX_LV = 224,                                     /**< 升级技能-技能等级已达上限 */
    MSG_ERR_PET_USK_NO_ENOUGH_MONEY = 225,                                  /**< 升级技能-金币不足 */
    MSG_ERR_PET_USK_NO_ENOUGH_POINT = 226,                                  /**< 升级技能-技能点不足 */
    MSG_ERR_PET_UM_NO_PLAYER = 227,                                         /**< 升级勋章-无角色实例 */
    MSG_ERR_PET_UM_NO_PET = 228,                                            /**< 升级勋章-无宠物实例 */
    MSG_ERR_PET_UM_NO_ITEM = 229,                                           /**< 升级勋章-未选择消耗道具 */
    MSG_ERR_PET_UM_LOCK = 230,                                              /**< 升级勋章-勋章还未解锁无法升级 */
    MSG_ERR_PET_UM_REACH_MAX_LV = 231,                                      /**< 升级勋章-勋章已达强化上限 */
    MSG_ERR_PET_UM_DEL_BAG_ITEM = 232,                                      /**< 升级勋章-从背包删除道具失败 */
    MSG_ERR_PET_UW_NO_PLAYER = 233,                                         /**< 升级翅膀-无角色实例 */
    MSG_ERR_PET_UW_NO_PET = 234,                                            /**< 升级翅膀-无宠物实例 */
    MSG_ERR_PET_UW_NO_ITEM = 235,                                           /**< 升级翅膀-升级道具读取不到 */
    MSG_ERR_PET_UW_ITEM_TYPE = 236,                                         /**< 升级翅膀-升级道具类型错误 */
    MSG_ERR_PET_UW_WING_LOCK = 237,                                         /**< 升级翅膀-该翅膀尚未解锁 */
    MSG_ERR_PET_UW_ERASE_ITEM = 238,                                        /**< 升级翅膀-从背包删除升级道具失败 */
    MSG_ERR_PET_UW_REACH_MAX_LV = 239,                                      /**< 升级翅膀-翅膀已达等级上限 */
    MSG_ERR_PET_UW_WING_ID = 240,                                           /**< 升级翅膀-翅膀ID不在服务器数据表中 */


    // 佣兵模块段 300-399
    MSG_ERR_MER_CREATE_MSG_FAIL = 300,                                      /**< 创建佣兵消息实例失败 */
    MSG_ERR_MER_NO_MSG_MATCH = 301,                                         /**< 消息路由时无匹配项 */
    MSG_ERR_MER_NO_ROLE_INSTANCE = 302,                                     /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_MER_SE_NO_PLAYER = 303,                                         /**< 开始雇佣-无角色实例 */
    MSG_ERR_MER_SE_LOCK = 304,                                              /**< 开始雇佣-佣兵未解锁 */
    MSG_ERR_MER_SE_NO_MER = 305,                                            /**< 开始雇佣-无指定ID的佣兵实例 */
    MSG_ERR_MER_SE_ALREADY_EMPLOY = 306,                                    /**< 开始雇佣-该佣兵已被雇佣 */
    MSG_ERR_MER_SE_NO_ENOUGH_MONEY = 307,                                   /**< 开始雇佣-钻石不足 */


    // 商城抽卡模块段 400-499
    MSG_ERR_MALL_CREATE_MSG_FAIL = 400,                                     /**< 创建商城抽卡消息实例失败 */
    MSG_ERR_MALL_NO_MSG_MATCH = 401,                                        /**< 消息路由时无匹配项 */
    MSG_ERR_MALL_NO_ROLE_INSTANCE = 402,                                    /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_MALL_RBP_TYPE_MISMATCH = 403,                                   /**< 请求抽卡-抽卡类型及方式无效 */
    MSG_ERR_MALL_RBP_NO_PLAYER = 404,                                       /**< 请求抽卡-无角色实例 */
    MSG_ERR_MALL_RBP_NO_FREE_CNT = 405,                                     /**< 请求抽卡-今日已无免费金币抽卡次数 */
    MSG_ERR_MALL_RBP_CALC_ONEGOLD_FAIL = 406,                               /**< 请求抽卡-金币免费或单抽计算失败 */
    MSG_ERR_MALL_RBP_CALC_TENGOLD_FAIL = 407,                               /**< 请求抽卡-金币十连抽计算失败 */
    MSG_ERR_MALL_RBP_IN_GODL_CD_TIME = 408,                                 /**< 请求抽卡-免费金币单抽在CD时间内 */
    MSG_ERR_MALL_RBP_NO_ENOUGH_GOLD = 409,                                  /**< 请求抽卡-无足够金币 */
    MSG_ERR_MALL_RBP_CALC_ONEDIAMOND_FAIL = 410,                            /**< 请求抽卡-钻石免费或单抽计算失败 */
    MSG_ERR_MALL_RBP_CALC_TENDIAMOND_FAIL = 411,                            /**< 请求抽卡-钻石十连抽计算失败 */
    MSG_ERR_MALL_RBP_IN_DIAMOND_CD_TIME = 412,                              /**< 请求抽卡-免费钻石单抽在CD时间内 */
    MSG_ERR_MALL_RBP_NO_ENOUGH_DIAMOND = 413,                               /**< 请求抽卡-无足够钻石 */


    // 任务模块段 500-599
    MSG_ERR_QUEST_CREATE_MSG_FAIL = 500,                                    /**< 创建任务消息实例失败 */
    MSG_ERR_QUEST_NO_MSG_MATCH = 501,                                       /**< 消息路由时无匹配项 */
    MSG_ERR_QUEST_NO_ROLE_INSTANCE = 502,                                   /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_QUEST_PRIZE_NO_ACCEPT = 503,                                    /**< 请求领取奖励-该任务还未领取 */
    MSG_ERR_QUEST_PRIZE_NO_ROLE = 504,                                      /**< 请求领取奖励-无角色实例 */
    MSG_ERR_QUEST_PRIZE_NO_COMPLETE = 505,                                  /**< 请求领取奖励-任务非完成状态无法领取奖励 */
    MSG_ERR_QUEST_PRIZE_NOT_IN_VALIDATE_TIME = 506,                         /**< 请求领取奖励-不在有效领奖时间段内 */
    MSG_ERR_QUEST_PRIZE_ERR_STATE = 507,                                    /**< 请求领取奖励-限时任务错误状态(已领取体力) */


    // 邮箱模块段 600-699
    MSG_ERR_MAIL_CREATE_MSG_FAIL = 600,                                     /**< 创建邮箱消息实例失败 */
    MSG_ERR_MAIL_NO_MSG_MATCH = 601,                                        /**< 消息路由时无匹配项 */
    MSG_ERR_MAIL_NO_ROLE_INSTANCE = 602,                                    /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_MAIL_INVALID_ID = 603,                                          /**< 无效ID */
    MSG_ERR_MAIL_NO_MAIL = 604,                                             /**< 无邮件实例 */
    MSG_ERR_MAIL_ERR_STATE = 605,                                           /**< 邮件状态错误 */
    MSG_ERR_MAIL_ERR_TYPE = 606,                                            /**< 邮件类型错误 */


    // 好友模块段 700-799
    MSG_ERR_FRIEND_CREATE_MSG_FAIL = 700,                                   /**< 创建好友消息实例失败 */
    MSG_ERR_FRIEND_NO_MSG_MATCH = 701,                                      /**< 消息路由时无匹配项 */
    MSG_ERR_FRIEND_NO_ROLE_INSTANCE = 702,                                  /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_FRIEND_NO_ROLE = 703,                                           /**< 无角色实例 */
    MSG_ERR_FRIEND_REFRESH_IN_CDTIME = 704,                                 /**< 请求刷新推荐列表-在冷却时间内 */
    MSG_ERR_FRIEND_ADD_BY_NAME_NO_ROLE = 705,                               /**< 请求根据名字添加好友-数据库内无该名字角色 */
    MSG_ERR_FRIEND_NO_ID = 706,                                             /**< 参数指定ID对应实例不在维护列表中 */
    MSG_ERR_FRIEND_ERR_STATE = 707,                                         /**< 好友关系状态不对 */
    MSG_ERR_FRIEND_MAX_FRIENDS = 708,                                       /**< 自身正式好友超出上限 */
    MSG_ERR_FRIEND_TAR_MAX_FRIENDS = 709,                                   /**< 对方正式好友超出上限 */
    MSG_ERR_FRIEND_ALREADY_FRIEND = 710,                                    /**< 已经是好友/申请者关系 */
    MSG_ERR_FRIEND_ALREADY_SEND_STRENGTH = 711,                             /**< 已赠送过体力 */
    MSG_ERR_FRIEND_NAME_NO_REG = 712,                                       /**< 通过名字查询好友——查找名字非注册 */
    MSG_ERR_FRIEND_ID_NO_REG = 713,                                         /**< 通过ID查询好友——查找名字非注册 */
    MSG_ERR_FRIEND_RECV_STRENGTH_ERR_STATE = 714,                           /**< 领取赠送体力——领取状态错误(未收到or已领取) */
    MSG_ERR_FRIEND_GLOBAL_CFG_MIS = 715,                                    /**< 常量配置读取失败*/
    MSG_ERR_FREIEND_GET_STRENGTH_REACH_MAX = 716,                           /**< 今日获取体力超过上限*/


    // 活动模块 800-899
    MSG_ERR_ACTIVITY_CREATE_MSG_FAIL = 800,                                 /**< 创建活动消息实例失败 */
    MSG_ERR_ACTIVITY_NO_MSG_MATCH = 801,                                    /**< 消息路由时无匹配项 */
    MSG_ERR_ACTIVITY_NO_ROLE_INSTANCE = 802,                                /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_ACTIVITY_NO_ROLE = 803,                                         /**< 无角色实例 */
    MSG_ERR_ACTIVITY_EXPIRE = 804,                                          /**< 活动过期 */
    MSG_ERR_ACTIVITY_UNREACH_CONDITION = 805,                               /**< 活动奖励条件未达到 */
    MSG_ERR_ACTIVITY_ALREADY_GET_AWARD = 806,                               /**< 已经领取过该奖励 */
    MSG_ERR_ACTIVITY_AWARD_NO_ID = 807,                                     /**< 请求领奖——请求ID不存在 */
    MSG_ERR_ACTIVITY_AWARD_NO_TYPE = 808,                                   /**< 请求领奖——请求类型错误 */
    MSG_ERR_ACTIVITY_NO_JOIN_FUND = 809,                                    /**< 请求领奖——未参与成长基金活动(未报名) */
    MSG_ERR_ACTIVITY_FUND_ALREADY_JOIN = 810,                               /**< 请求参与基金活动——已经参与了某个基金活动 */
    MSG_ERR_ACTIVITY_FUND_UNREACH_CONDITION = 811,                          /**< 请求参与基金活动——VIP或钱不够 */
    MSG_ERR_ACTIVITY_FUND_ERR_TYPE = 812,                                   /**< 错误的基金类型 */


    // 聊天模块 900-999
    MSG_ERR_CHAT_CREATE_MSG_FAIL = 900,                                     /**< 创建活动消息实例失败 */
    MSG_ERR_CHAT_NO_MSG_MATCH = 901,                                        /**< 消息路由时无匹配项 */
    MSG_ERR_CHAT_NO_ROLE_INSTANCE = 902,                                    /**< 消息路由时无角色实例(player == NULL) */
    MSG_ERR_CHAT_NO_ROLE = 903,                                             /**< 无角色实例 */
    MSG_ERR_CHAT_NO_CHANNEL = 904,                                          /**< 错误的频道 */
    MSG_ERR_CHAT_INTERVAL_TIME = 905,                                       /**< 发言间隔时间不足 */
    MSG_ERR_CHAT_NO_ENOUGH_DIAMOND = 906,                                   /**< 世界发言钻石不足 */
    MSG_ERR_CHAT_NO_ROLE_NAME = 907,                                        /**< 私聊对象名字不存在 */
    MSG_ERR_CHAT_ROLE_OFFLINE = 908,                                        /**< 私聊对象不在线 */


    //背包
    MSG_ERR_BAG_NO_ITEM_CONF = 2000,                                        /**< 物品不存在配置*/
    MSG_ERR_ITEM_USE_NO_ITEM = 2001,                                        /**< 背包物品不足*/
    MSG_ERR_ITEM_USE_EXP_PILL_EXP_UP = 2002,                                /**< 使用经验丹，玩家经验已经达到上限*/
    MSG_ERR_ITEM_USE_SOUL_PET_EXIST = 2003,                                 /**< 合成英雄，英雄已经存在*/
    MSG_ERR_ITEM_USE_NO_MSG_MATCH = 2004,                                   /**< 物品使用，未匹配的消息请求*/
    MSG_ERR_ITEM_USE_PLAYER_NULL = 2005,                                    /**< 角色player=NULL*/
    MSG_ERR_ITEM_USE_NO_FUNC_MATCH = 2006,                                  /**< 物品使用，物品的类型没有对应的使用方式*/
    MSG_ERR_ITEM_CREATE_NO_FUNC_MATCH = 2007,                               /**< 物品合成，物品的类型没有对应的使用方式*/

    // 个人商店 2000-2099
    MSG_ERR_PERSONAL_STORE_BUSINESS_ERROR_INDEX = 2050,                     /**< 购买个人商店物品时发送下标过大*/
    MSG_ERR_PERSONAL_STORE_BUSINESS_DATA_ERROR = 2051,                      /**< 商店内的物品不足应该有的数量，属于逻辑错误*/
    MSG_ERR_PERSONAL_STORE_BUSINESS_NO_ITEM = 2052,                         /**< 要购买的物品数量为0*/
    MSG_ERR_PERSONAL_STORE_BUSINESS_NO_CONFIG = 2053,                       /**< 要购买的物品配置为空*/
    MSG_ERR_PERSONAL_STORE_BUY_BUSINESS_NO_ENOUGH_GOLD = 2054,              /**< 购买商人金币不足*/
    MSG_ERR_PERSONAL_STORE_BUY_BUSINESS_NO_ENOUGH_DIAMOND = 2055,           /**< 购买商人钻石不足*/
    MSG_ERR_PERSONAL_STORE_REFRESH_BUSINESS_NO_ENOUGH_DIAMOND = 2056,       /**< 刷新商人钻石不足*/
    MSG_ERR_PERSONAL_STORE_NO_FUNC_MATCH = 2057,                            /**< 个人商店模块物品配消息*/

    MSG_ERR_PERSONAL_STORE_DUEL_ERROR_INDEX = 2060,                         /**< 购买竞技场商店物品时发送下标过大*/
    MSG_ERR_PERSONAL_STORE_DUEL_DATA_ERROR = 2061,                          /**< 竞技场商店内的物品不足应该有的数量，属于逻辑错误*/
    MSG_ERR_PERSONAL_STORE_DUEL_NO_ITEM = 2062,                             /**< 竞技场商店要购买的物品数量为0*/
    MSG_ERR_PERSONAL_STORE_DUEL_NO_CONFIG = 2063,                           /**< 竞技场商店要购买的物品配置为空*/
    MSG_ERR_PERSONAL_STORE_BUY_DUEL_NO_ENOUGH_GOLD = 2064,                  /**< 竞技场商店刷新金币不足*/
    MSG_ERR_PERSONAL_STORE_REFRESH_DUEL_NO_ENOUGH_GOLD = 2065,              /**< 刷新竞技场商店消耗不足*/

    MSG_ERR_PERSONAL_STORE_HEAVEN_ERROR_INDEX = 2070,                       /**< 购买天堂商店物品时发送下标过大*/
    MSG_ERR_PERSONAL_STORE_HEAVEN_DATA_ERROR = 2071,                        /**< 竞技场商店内的物品不足应该有的数量，属于逻辑错误*/
    MSG_ERR_PERSONAL_STORE_HEAVEN_NO_ITEM = 2072,                           /**< 竞技场商店要购买的物品数量为0*/
    MSG_ERR_PERSONAL_STORE_HEAVEN_NO_CONFIG = 2073,                         /**< 竞技场商店要购买的物品配置为空*/
    MSG_ERR_PERSONAL_STORE_BUY_HEAVEN_NO_ENOUGH_GOLD = 2074,                /**< 竞技场商店购买商人金币不足*/
    MSG_ERR_PERSONAL_STORE_REFRESH_HEAVEN_NO_ENOUGH_GOLD = 2075,            /**< 刷新竞技场金币不足*/

    MSG_ERR_PERSONAL_STORE_TOWER_ERROR_INDEX = 2070,                        /**< 购买秘境塔商店物品时发送下标过大*/
    MSG_ERR_PERSONAL_STORE_TOWER_DATA_ERROR = 2071,                         /**< 秘境塔商店内的物品不足应该有的数量，属于逻辑错误*/
    MSG_ERR_PERSONAL_STORE_TOWER_NO_ITEM = 2072,                            /**< 秘境塔商店要购买的物品数量为0*/
    MSG_ERR_PERSONAL_STORE_TOWER_NO_CONFIG = 2073,                          /**< 秘境塔商店要购买的物品配置为空*/
    MSG_ERR_PERSONAL_STORE_BUY_TOWER_NO_ENOUGH_GOLD = 2074,                 /**< 秘境塔商店购买金币不足*/
    MSG_ERR_PERSONAL_STORE_REFRESH_TOWER_NO_ENOUGH_GOLD = 2075,             /**< 刷新秘境塔商店消耗不足*/


    // 竞技场模块段
    MSG_ERR_DUEL_ERROR = 2100,                                              /**< 竞技场逻辑错误*/
    MSG_ERR_DUEL_NO_DATA = 2101,                                            /**<找不到该角色竞技场的信息*/
    MSG_ERR_DUEL_CHALLENGE_RESULT_INDEX_ERROR = 2102,                       /**< 挑战结果返回的index错误*/
    MSG_ERR_DUEL_NO_RECORD_TIME = 2103,                                     /**< 保存记录时发送的time找不到*/
    MSG_ERR_DUEL_CHALLENGE_HONOR_HAVE_CHALLENGED = 2104,                    /**< 段位挑战，该段位已经挑战过*/
    MSG_ERR_DUEL_CHALLENGE_HONOR_SCORE_CAN_NOT_CHALLENGE = 2105,            /**< 积分达不到挑战要求*/
    MSG_ERR_DUEL_CONF_NULL = 2106,                                          /**< 配置为空*/
    MSG_ERR_DUEL_VIP_NO_BUY_TIMES = 2107,                                   /**< 购买挑战次数，vip等级对应的购买次数已经超过上限*/
    MSG_ERR_DUEL_VIP_BUY_CHALL_TIMES_NO_DIAMOND = 2108,                     /**< 购买挑战次数，没有钻石*/
    MSG_ERR_DUEL_CHALLENGE_NO_TIMES = 2109,                                 /**< 无剩余挑战次数*/
    MSG_ERR_DUEL_CHALLENGE_CD = 2110,                                       /**< 竞技场挑战CD*/

    //天堂之路
    MSG_ERR_HEAVEN_CAN_NOT_PASS = 2201,                                     /**< 天堂之路无法通关*/
    MSG_ERR_HEAVEN_CONF = 2202,                                             /**< 天堂之路配置读取不到*/
    MSG_ERR_HEAVEN_NO_MONEY = 2203,                                         /**< 金币不足*/
    MSG_ERR_HEAVEN_NO_TIMES = 2204,                                         /**< 无挑战次数*/
    MSG_ERR_HEAVEN_ERROR_INDEX = 2205,                                      /**< 错误的下标*/
    MSG_ERR_HEAVEN_GETBOX_TYPE_ERROR = 2206,                                /**< 领取宝箱状态为不可领取*/

    //秘境塔
    MSG_ERR_TOWER_CAN_NOT_PASS = 2301,                                      /**< 不能通关*/
    MSG_ERR_TOWER_CAN_NOT_CLEAN_UP = 2302,                                  /**< 不能扫荡*/
    MSG_ERR_TOWER_CONF = 2303,                                              /**< 配置错误*/
    MSG_ERR_TOWER_NO_DATA = 2304,                                           /**< 找不到秘境塔数据*/
    MSG_ERR_TOWER_NO_MONEY = 2305,                                          /**< 金币不足*/
    MSG_ERR_TOWER_NO_TIMES = 2306,                                          /**< 无挑战次数*/

    //签到
    MSG_ERR_SINGIN_NO_CONF = 2401,                                          /**< 签到配置错误*/
    MSG_ERR_SINGIN_HAVE_GOT_DAY_AWD = 2402,                                 /**< 今日已经领取过*/
    MSG_ERR_SINGIN_HAVE_GOT_ADD_UP_AWD = 2403,                              /**< 已经领取过累计奖励，或者当前未达到领取累计次数*/
    MSG_ERR_SINGIN_NO_MATCH_ERROR_TYPE2 = 2404,                             /**< 错误的type2*/

};
