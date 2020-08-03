using UnityEngine;
using System.Collections;

public class GlobalEvent
{
    public const string     MsgHead                         = "Global";
    public const string     StartGlobalListenerEvent        = "StartGlobalListenerEvent";
    public static string    PlayerSkillPointChange          = "PlayerSkillPointChange_Evnt";    // 玩家技能变动
    public static string    PlayerStaminaChangeForMain      = "PlayerStaminaChangeForMain";     // 玩家体力变化触发主界面显示

    public const int        ChangeEvent                     = 1000001;                          // 服务器推送数据发生变化(包含:装备,装备碎片,卷轴,卷轴碎片,英雄魂石,金币道具,英雄经验道具,勋章经验道具,翅膀经验道具,扫荡券,体力道具)
    public const int        AddHeroEvent                    = 1000002;                          // 添加整卡英雄
    public const int        CurrencyEvent                   = 1000003;                          // 玩家货币发生变化
    public const int        PlayerExpEvent                  = 1000004;                          // 玩家经验发生变化
    public const int        PlayerLvEvent                   = 1000005;                          // 玩家等级发生变化
      
    public const int        MedalEvent                      = 1000006;                          // 勋章经验和等级发生变化
    public const int        ErrorCodeEvent                  = 1000007;                          // 错误码事件
    public const int        TaskList_StateEvent             = 1000010;                          // 单个任务状态发生变化
    public const int        TaskList_NewEvent               = 1000011;                          // 新接任务
    public const int        Eamil_New                       = 1000012;                          // 刷新邮件数据

    public const int        RET_Red_Point_Event             = 1000013;                          // 刷新主界面红点
    public const int        RET_FriendStateUpdate_Event     = 1000014;                          // 好友状态更新
    public const int        RET_FriendListUpdate_Event      = 1000015;                          // 好友列表更新
    public const int        AddHeroClickEvent               = 1000016;                          // 新整卡英雄信息点击
    public const int        UIAnimationEvent                = 1000017;                          // UI动画

    public const int        LoadResEvent                    = 1000018;                          // 加载资源
    public const int        RechargeEvent                   = 1000019;                          // 充值成功
}