using UnityEngine;
using System.Collections;

/// <summary> 客户端与登录服进行通信 </summary>
public enum LOGIN_CLIENT_LS_CMD                                             // 客户端与登录服进行通信              
{
    CLLS_REQ_SRV_LIST = 0,                              // 客户端请求登录(服务器列表)
    CLLS_RET_SRV_LIST = 1,                              // 登录服返回服务器列表
    CLLS_REQ_LOGIN    = 2,                              // 客户端请求登录
    CLLS_RET_LOGIN    = 3,                              // 登录服返回登录请求
    CLLS_REQ_PUBLIC   = 4,                              // 公告请求
    CLLS_RET_PUBLIC   = 5                               // 公告返回
}

/// <summary> 客户端与游戏服进行通信 </summary>
public enum LOGIN_CLIENT_GS_CMD                                             // 客户端与游戏服进行通信              
{
    CLGS_LOGIN                  = 0,                     // 客户端登录游戏服务器
    CLGS_RELOGIN                = 1,                     // 客户端断线重连
    CLGS_SYNC_PLAYER_RoleList   = 2,                     // 同步玩家角色列表信息
    CLGS_SYNC_SRV_TIME          = 3,                     // 同步服务器时间

    CUSTOM_RELOGIN              = 100                    // 自定义本地重新登录,与服务器无关
}

/// <summary> 玩家模块消息: (31) </summary>
public enum PLAYER_CMD                                                      // 玩家模块消息                       
{
    PLAYER_REQ_CHECK_CREATE_ROLE = 0,                   // 创建角色请求之前的验证请求
    PLAYER_REQ_CREATE_ROLE       = 1,                   // 请求创建角色
    PLAYER_REQ_ENTER_GAME        = 2,                   // 请求进入游戏
    PLAYER_REQ_CLIENT_READY      = 3,                   // 请求同步数据(客户端资源初始化完毕)
    PLAYER_REQ_UPDATA_GUIDE_STEP = 4,                   // 客户端上传引导步数
    PLAYER_REQ_BUY_STAMINA       = 5,                   // 请求购买体力
    PLAYER_REQ_BUY_COINS         = 6,                   // 请求购买金币
    PLAYER_REQ_BUY_SKILLPOINT    = 7,                   // 请求购买技能点数
    PLAYER_REQ_CHANGEICON        = 8,                   // 请求更改头像
    PLAYER_REQ_RENAME            = 9,                   // 请求更改玩家名称
    PLAYER_REQ_UPLV_AWARD        = 10,                  // 请求领取升级宝箱
    PLAYER_REQ_UPLV_TALENT       = 11,                  // 请求升级天赋

    PLAYER_RET_ENTER_GAME        = 50,                  // 返回进入游戏回调
    PLAYER_RET_PLAYER_INFO       = 51,                  // 同步玩家数据-返回
    PLAYER_SYNC_CURRENCY         = 52,                  // 同步货币
    PLAYER_SYNC_ATTRIBUTE        = 53,                  // 同步属性
    PLAYER_SYNC_ERROR_CODE       = 54,                  // 同步错误码
    PLAYER_RET_CREATE_ROLE       = 55,                  // 返回角色创建请求
    PLAYER_RET_BUY               = 56,                  // 返回购买请求
    PLAYER_RET_CHANGEICON        = 57,                  // 返回更改头像请求
    PLAYER_RET_CHECK_CREATE_ROLE = 58,                  // 返回角色创建前验证请求
    PLAYER_RET_RENAME            = 59,                  // 返回更改名称请求
    PLAYER_RET_UPLV_AWARD        = 60,                  // 返回升级宝箱请求
    PLAYER_RET_UPLV_TALENT       = 61,                  // 返回升级天赋请求
    PLAYER_SYNC_UPLV_TALENT      = 62,                  // 同步天赋点数

    PLAYER_KEEP_LIVE             = 100                  // 心跳包
}
/// <summary> 服务器整点推送 </summary>
public enum SERVER_SYNC_Push_CMD                                            // 服务器整点推送                      
{
    SYNC_CLOCK = 1,
}

public enum PLAYER_ATTR_TYPE                                                // 玩家属性同步                       
{
    PlayerLv            = 0,                            // 玩家等级
    PlayerExp           = 1,                            // 玩家经验
    PlayerStamina       = 2,                            // 玩家体力
    PlayerSkill         = 3,                            // 玩家技能点数
    PlayerVIPLv         = 4                             // 玩家VIP等级
}
public enum CURRENCY_TYPE                                                   // 货币类型
{
    Unknow  = 0,                // 未知
    Diamond = 1,                // 钻石
    Coins   = 2                 // 金币
}
