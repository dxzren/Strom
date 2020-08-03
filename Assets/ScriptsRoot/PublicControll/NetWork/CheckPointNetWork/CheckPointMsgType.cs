using UnityEngine;
using System.Collections;

public enum CheckPointMsgType
{
    CHKP_REQ_Main                   = 1,                // 关卡主入口请求
    CHKP_REQ_Challenge              = 2,                // 挑战关卡请求
    CHKP_REQ_ResetChallengeTimes    = 3,                // 重置挑战次数请求
    CHKP_REQ_ChallengeSuccess       = 4,                // 关卡挑战成功请求
    CHKP_REQ_GetStarBox             = 5,                // 领取星级宝箱请求
    CHKP_REQ_Sweep                  = 6,                // 扫荡请求
    CHKP_REQ_EliteSweepTimes        = 7,                // 精英关卡挑战次数请求

    CHKP_RET_PassedMaxNormalID      = 51,               // 通过的最大普通关卡ID回调
    CHKP_RET_NormalInof             = 52,               // 普通大关卡信息回调
    CHKP_RET_EliteInfo              = 53,               // 精英关卡信息回调
    CHKP_RET_NormalStarBox          = 54,               // 已领取的星级宝箱回调
    CHKP_RET_EliteStarBox           = 55,               // 已领取的精英星级宝箱列表回调

    CHKP_RET_Challenge              = 56,               // 挑战关卡回调
    CHKP_RET_ResetChallengeTimes    = 57,               // 重置挑战次数回调
    CHKP_RET_ChallengeSuccess       = 58,               // 关卡挑战成功回调
    CHKP_RET_GetStarBox             = 59,               // 领取星级宝箱回调
    CHKP_RET_Sweep                  = 60,               // 扫荡回调
    CHKP_RET_EliteSweepTimes        = 61,               // 精英关卡挑战次数回调
}

