using UnityEngine;
using System.Collections;

/// <summary> 通信大类 </summary>
public enum eMsgType
{
    _MSG_INVALID = 0,                                   // 无效消息
    _MSG_CS_COMMUNICATE_LS  = 1,                        // 中心服务器与登录服务器通信
    _MSG_CS_COMMUNICATE_GS  = 2,                        // 中心服务器与游戏服务器通信
    _MSG_GS_COMMUNICATE_DB  = 3,                        // 游戏服务器与数据服通信
    _MSG_GS_COMMUNIACTE_CMD = 5,                        // 游戏服务器与指令服通信

    _MSG_LOGIN_CLIENT_LS    = 20,                       // 客户端与登录服务器通信
    _MSG_LOGIN_CLIENT_GS    = 30,                       // 客户端登录游戏服

    _MSG_PLAYER_MODULE      = 31,                       // 玩家模块
    _MSG_HERO_MODULE        = 32,                       // 英雄模块
    _MSG_MERC_MODULE        = 33,                       // 佣兵模块
    _MSG_MALL_MODULE        = 34,                       // 商城模块
    _MSG_TASK_MODULE        = 35,                       // 任务模块
    _MSG_EMAIL_MODULE       = 36,                       // 邮箱模块
    _MSG_FRIENDS_MODULE     = 37,                       // 好友模块
    _MSG_CHAT_MODULE        = 38,                       // 聊天模块
    _MSG_ACTIVITY_MODULE    = 39,                       // 活动模块

    _MSG_BAG_MODULE          = 40,                      // 背包模块
    _MSG_MERCHANT_MODULE     = 41,                      // 商人模块
    _MSG_CHECKPOINT_MODULE   = 42,                      // 关卡模块
    _MSG_MONSTERWAR_MODULE   = 43,                      // 巨兽囚笼模块
    _MSG_JJC_MODULE          = 44,                      // 竞技场模块
    _MSG_PARADISEROAD_MODULE = 45,                      // 天堂之路模块
    _MSG_SECRETTOWER_MODULE  = 46,                      // 秘境塔模块
    _MSG_SIGMIN_MODULE       = 47,                      // 签到模块
    _MSG_GUILD_MODULE        = 48,                      // 公会模块
    _MSG_RANKING_MODULE      = 49,                      // 排行榜模块
    _MSG_DRGONTRAIL_MODULE   = 50,                      // 巨龙之穴模块
    _MSG_RECHARGE_MODULE     = 51,                      // 充值模块
    _MSG_CURRENCY_MODULE     = 52,                      // 同步货币

    _MSG_KEEPLIVE            = 98,                      // 网速测试 - 心跳包
    _MSG_GM_COMMAND          = 99,                      // GM后台指令
    _MSG_SYNC_Push_MODULE    = 101                      // 服务器整点时间同步
} 