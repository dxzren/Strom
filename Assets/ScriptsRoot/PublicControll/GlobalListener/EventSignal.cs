using UnityEngine;
using System.Collections;

public class EventSignal                                                                        // 事件信号
{
    public static string Reward_sure_Event       = "Reward_sure_Event";      // 奖励确认 (共用对话框)
    public static string UpdateInfo_Event        = "UpdateInfo_Event";       // 更新界面(金币,钻石,体力)
    public static string BattleFinished_Event    = "BattleFinished_Event";   // 战斗结束事件
    public static string GuideDestroy_Event      = "GuideDestroy";           // 新手引导清除
    public static string NewDayRefresh_Event     = "NewDayRefresh_Event";    // 每天更新服务器发送的推送通知      
}