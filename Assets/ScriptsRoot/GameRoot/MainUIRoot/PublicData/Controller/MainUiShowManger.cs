using UnityEngine;
using System.Collections;

public class MainUiShowManger
{
    [Inject]
    public IPlayer              InPlayer                    { set; get; }

    static MainUiShowManger     _sInstance;
    public static MainUiShowManger sInstance
    {
        get
        {
            if (_sInstance == null)                         _sInstance = new MainUiShowManger();
            return _sInstance;
        }
    }

    public SystemShowClick      ShowClickLvSet(IPlayer InPlayer,MainSystem sysName)                              // 主界面系统显示点击_设置  
    {
        int targetShowLv        = 0;                                                            // 显示等级
        int targetLimitLv       = 0;                                                            // 开发等级
        int sysID               = 0;                                                            // 系统ID
        int sortID              = 0;                                                            // 排序ID
        string showName         = "";                                                           // 显示名称
        SystemShowClick         TheSysShowClick             = new SystemShowClick();            // 系统点击显示设置
        switch (sysName)
        {
            case MainSystem.HeroSys:                                                            // 英雄系统     
                {
                    sysID = (int)SystemID.HeroPanel;
                    showName = "英雄";
                    TheSysShowClick.systemBtnType = SystemBtnType.viewBtn;
                    break;
                }
            case MainSystem.BagSys:                                                             // 背包系统     
                {
                    sysID = (int)SystemID.BagPanel;
                    showName = "背包";
                    TheSysShowClick.systemBtnType = SystemBtnType.UIBtn;
                    sortID = 1;
                    break;
                }
            case MainSystem.CheckPoint:                                                         // 关卡系统     
                {
                    sysID = (int)SystemID.CheckPointPanel;
                    showName = "关卡";
                    TheSysShowClick.systemBtnType = SystemBtnType.viewBtn;
                    break;
                }
            case MainSystem.MallSys:                                                            // 商城系统     
                {
                    sysID = (int)SystemID.MallPanel;
                    showName = "商城";
                    TheSysShowClick.systemBtnType = SystemBtnType.viewBtn;
                    break;
                }
        }
        Configs_SystemNavigationData TheSysNavigtData       = Configs_SystemNavigation.sInstance.
                                                              GetSystemNavigationDataBySystemID(sysID);
        TheSysShowClick.sortID              = sortID;
        TheSysShowClick.sysName             = TheSysNavigtData.SystemName;
        TheSysShowClick.showLevel           = TheSysNavigtData.ShowLevel;
        TheSysShowClick.viewName            = showName;

        if (TheSysNavigtData == null)       
        {
            TheSysShowClick.isShowLevel     = true;
            TheSysShowClick.isClickLevel    = true;
            return TheSysShowClick;
        }

        targetShowLv                        = TheSysNavigtData.ShowLevel;
        targetLimitLv                       = TheSysNavigtData.LimitLevel;

        if (InPlayer.PlayerLevel >= targetShowLv)
        { TheSysShowClick.isShowLevel = true; }
        else
        { TheSysShowClick.isShowLevel = false; }

        if (InPlayer.PlayerLevel >= targetLimitLv)
        { TheSysShowClick.isClickLevel = true; }
        else
        { TheSysShowClick.isClickLevel = false; }

        return TheSysShowClick;
    }
}
public enum SystemBtnType                                                                       // 系统按钮                
{
    viewBtn,                                            // 不需要隐藏Btn
    UIBtn,                                              // 需要隐藏Btn
}
public enum MainSystem                                                                          // 主界面系统              
{
    HeroSys         = 0,                                // 英雄系统
    MercSys         = 1,                                // 佣兵系统
    WingSys         = 2,                                // 翅膀系统
    BagSys          = 3,                                // 背包系统
    TaskSys         = 4,                                // 任务系统
    Friend          = 5,                                // 好友系统
    CheckPoint      = 6,                                // 关卡系统
    RankSys         = 7,                                // 排行榜系统
    GuildSys        = 8,                                // 公会系统
    MailSys         = 9,            

    SignInSys       = 10,                               // 签到系统
    ActivitySys     = 11,                               // 活动系统
    RechangeSys     = 12,                               // 充值系统
    FirstRechange   = 13,                               // 首充系统
    SevenActivity   = 14,                               // 七日活动
    MallSys         = 15,                               // 商城系统
    Merchant        = 16,                               // 商人系统

    JJC             = 17,                               // 竞技场
    MonsterWar      = 18,                               // 巨兽囚笼
    DragonTrial     = 19,                               // 巨龙试炼
    ParadiseRoad    = 20,                               // 天堂之路
    SecretTower     = 21,                               // 秘境之塔
}
public enum SystemID                                                                            // 系统ID                  
{
    RechargePanel   = 1001,                             // 重置面板
    CheckPointPanel = 1002,                             // 关卡系统
    MallPanel       = 1003,                             // 商城系统
    BuyCoinPanel    = 1004,                             // 购买金币
    HeroPanel       = 1005,                             // 英雄系统
    ArenaPanel      = 1006,                             // 竞技场面板
    MonsterWar      = 1007,                             // 巨兽面板
    BagPanel        = 1016,                             // 背包系统
}
public class SystemShowClick                                                                    // 主界面系统点击与显示     
{
    public int                  showLevel;                                                      /// 显示等级
    public int                  LimitLevel;                                                     /// 限制等级
    public int                  sortID = 0;                                                     /// 排序ID

    public string               sysName                 = "";                                   /// 系统名
    public string               viewName                = "";                                   /// 显示名称

    public bool                 isShowLevel             = false;                                /// 是否到达显示等级
    public bool                 isClickLevel            = false;                                /// 是否到达点击等级

    public SystemBtnType        systemBtnType           = SystemBtnType.viewBtn;                /// 是否需要隐藏 ( ViewBtn:不需要隐藏 UIBtn:需要隐藏Btn )
}