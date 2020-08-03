using UnityEngine;
using System.Collections;

public class RechargeEvent
{
    public const string GetRechargeUrl_Event = "GetRechargeUrl_Event";                          // 获取支持地址
        
    public static string LoadVIPInfo_Event   = "LoadVIPInfo_Event";                             // 加载VIP信息
    public static string ShowVIP_Event       = "ShowVIP_Event";                                 // 显示VIP信息

    public static string MonthCardTime_Event = "MonthCardTime_Event";                           // 月卡时间戳
    public static string RechargeNum_Event   = "RechargeTimes_Event";                           // 充值卡充值次数
    public static string GetPlayerID_Event   = "GetPlayerID_Event";                             // 请求玩家帐号ID
    public static string RechargeNone_Event  = "RechargeNoneEvent";                             // 无需等待服务器返回
}
