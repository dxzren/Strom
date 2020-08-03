using UnityEngine;
using System.Collections;

/// <summary>  充值消息类型  </summary>
enum eReCharge_CMD
{
    RECHARGE_REQ_MonthCard      = 1,                    // 月卡开始时间戳请求
    RECHARGE_REQ_Times          = 2,                    // 充值次数请求
    RECHARGE_REQ_PlayerID       = 3,                    // 玩家ID请求
    RECHARGE_REQ_Server_URL     = 4,                    // 服务器地址请求
    RECHARGE_REQ_Null_return    = 5,                    // 无需等待直接返回

    RECHARGE_RET_MonthCard      = 51,                   // 月卡开始时间戳回调
    RECHARGE_RET_Times          = 52,                   // 充值次数回调
    RECHARGE_RET_PlayerID       = 53,                   // 玩家ID回调
    RECHARGE_RET_Server_URL     = 54,                   // 服务器地址回调
    RECHARGE_RET_PMD_Main       = 55,                   // 主界面跑马灯回调
    RECHARGE_RET_Money          = 56,                   // 充值金额回调
}
