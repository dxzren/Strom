using UnityEngine;
using System.Collections;

/// <summary> 关卡系统事件</summary>
public class CheckPointEvent
{
    public static string REQ_Sweep_Event                        = "REQ_Sweep_Event";                               // 扫荡一次请求
    public static string REQ_SweepTenTimes_Event                = "REQ_SweepTenTimes_Event";                       // 扫荡十次请求
    public static string RET_Sweep_Event                        = "RET_Sweep_Event";                               // 扫荡回调

    public static string REQ_GetStarBox_Event                   = "REQ_GetStarBox_Event";                          // 星级宝箱奖励请求
    public static string RET_GetStarBox_Event                   = "RET_GetStarBox_Event";                          // 星级宝箱奖励回调

    public static string REQ_CheckPointInfo_Event               = "REQ_CheckPointInfo_Event";                      // 关卡信息请求 
    public static string REQ_CheckPointChallenge_Event          = "REQ_CheckPointChallenge_Event";                 // 关卡挑战请求
    public static string REQ_ChallengeSuccess_Event             = "REQ_ChallengeSuccess_Event";                    // 关卡挑战成功请求
    public static string REQ_ResetChallengeTimes_Event          = "REQ_ResetChallengeTimes_Event";                 // 重置关卡次数请求
    public static string REQ_Move_Event                         = "REQ_Move_Event";                                // 
    public static string REQ_RefreshStarBox_Event               = "REQ_RefreshStarBox_Event";                      // 刷新星级宝箱请求
    public static string CheckPointItemClick_Event              = "CheckPointItemClick_Event";                     // 关卡点击确认
}