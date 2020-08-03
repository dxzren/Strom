using UnityEngine;
using System.Collections;

public class MainEvent
{
    public static string CheckIn_Event = "CheckIn_Event";                                       // 签到
    public static string REQCheckInfo_Event = "REQCheckInfo_Even";                              // 获取签到相关信息 
    public static string RETCheckInfo_Event = "RETCheckInfo_Event";                             // 签到刷新回调

    public static string ChangeIcon_Event = "ChangeIcon_Event";                                 // 更换头像
    public static string GetTotalCheckInAward_Event = "GetTotalCheckInAward_Event";             // 获取累积签到奖励
    public static string SendCDKey_Event = "SendCDKey_Event";                                   // 发送CDKey

    public static string BuyCoins_Event = "BuyCoins_Event";                                     // 购买金币
    public static string Buy_Event = "Buy_Event";                                               // 购买
    public static string SweepCallback_Event = "SweepCallback_Event";                           // 清除回调
    public static string IntoChat_Event = "IntoChat_Event";                                     // 弹出聊天框

    public static string BackToFriend_Event = "BackToFriend_Event";                             // 返回好友界面
    public static string BuyDiamond_Event = "BuyDiamond_Event";                                 // 充值(购买钻石)
    public static string GetCheckPointInfo_Event = "GetCheckPointInof_Event";                   // 获得关卡页面的相关信息
    public static string GM_Event = "GM_Event";                                                 // GM指令

    public static string BuyStamina_Event = "BuyStamina_Event";                                 // 购买体力
    public static string RefreshFriendList_Event = "RefreshFriendList_Event";                   // 更新好友列表
    public static string TaskToRoadOfHeaven_Event = "TaskToRoadOfHeaven_Event";                 // 天堂之路的任务
    public static string UpLevelComm_Event = "UpLevelComm_Event";                               // 升级
    public static string HornMessage_Event = "HornMessage_Event";                               // 喇叭消息


}
