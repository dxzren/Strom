using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPanelConfig
{
    public static string        Test                        = "UIs/TestRoot/GameObject";                            // 测试

    #region 全局通用（Global）
    public static string        BlackPanel                  = "UIs/Global/BlackPanel";                              // 01.返回面板
    public static string        DialogPanel                 = "UIs/Global/DialogPanel";                             // 02.对话面板
    public static string        HidePanel30Floor            = "UIs/Global/HidePanel30Floor";                        // 03.隐藏30Floor面板
    public static string        MaskPanel                   = "UIs/Global/MaskPanel";                               // 04.遮罩面板

    public static string        PassSkillTipsPanel          = "UIs/Global/PassSkillTipspanel";                      // 05.点击技能Tips提示面板
    public static string        PropUpInfoPanel             = "UIs/Global/PropUpInfopanel";                         // 07.维持信息面板
    public static string        PropUpSpritePanel           = "UIs/Global/PropUpSpritePanel";                       // 08.维持图标面板
    public static string        PropUpSpritePanel2          = "UIs/Global/PropUpSpritePanel2";                      // 09.维持图标面板2

    public static string        PropUpTextPanel             = "UIs/Global/PropUpTextPanel";                         // 10.维持文本面板
    public static string        RewardAllPanel              = "UIs/Global/RewardAllPanel";                          // 11.所有奖励面板
    public static string        RewardGrid                  = "UIs/Global/RewardGrid";                              // 12.奖励格子
    public static string        RewardCoinsItem             = "UIs/Global/RewardCoinsItem";                         // 13.奖励货币物品

    public static string        RewardPropItem              = "UIs/Global/RewardPropItem";                          // 14.奖励道具物品
    public static string        SkillTipsPanel              = "UIs/Global/SkillTipsPanel";                          // 15.技能Tips提示面板
    public static string        ShortLoadingPanel           = "UIs/Global/ShortLoadingPanel";                       // 16.短加载面板
    public static string        StaminaTipsPanel            = "UIs/Global/tiliTipsPanel";                           // 17.体力Tips提示面板

    public static string        ShowAccountInfo             = "UIs/Global/ShowAccountInfo";                         // 18.展示账户信息
    public static string        TipsPanel                   = "UIs/Global/TipsPanel";                               // 19.Tips提示面板
    public static string        ViewEmptyInfoPanel          = "UIs/Global/ViewEmptyInfoPanel";                      // 20.界面为空显示
    public static string        WarningPanel                = "UIs/Global/WarningPanel";                            // 21.警告面板

    public static string        FightForcePartAnimCureve    = "UIs/Global/FightForcePartAnimCureve";                // 22.战斗力粒子动画曲线
    #endregion

    #region 新手引导（Guide）
    public static string        GuidePanel                  = "UIs/Guide/GuidePanel";                               // 1.新手引导面板
    public static string        GuideRoot                   = "UIs/Guide/GuideRoot";                                // 2.新手引导入口

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region 登录界面（LogIn）
    public static string        GameEnterPanel              = "UIs/LogIn/GameEnterPanel";                           // 01.进入游戏面板
    public static string        LoadingPanel                = "UIs/LogIn/LoadingPanel";                             // 02.加载面板
    public static string        LogInOrRegisterPanel        = "UIs/LogIn/LogInRegisterPanel";                       // 03.（登录or注册）面板
    public static string        PublicPanel                 = "UIs/LogIn/PublicPanel";                              // 04.公告面板
    public static string        PublicContant               = "UIs/LogIn/PublicContant";                            // 05.公告内容
    public static string        RegisterPanel               = "UIs/LogIn/RegisterPanel";                            // 06.注册面板
    public static string        ReLogInPanel                = "UIs/LogIn/ReLogInPanel";                             // 07.消息返回面板

    public static string        RoleSelectPanel             = "UIs/LogIn/RoleSelectPanel";                          // 08.主角选择面板
    public static string        ServerList                  = "UIs/LogIn/ServerList";                               // 09.服务器列表
    public static string        ServerLineItem              = "UIs/LogIn/ServerLineItem";                           // 10.服务器项
    public static string        ServerZoneItem              = "UIs/LogIn/ServerZoneItem";                           // 11.服务器列表项
    public static string        ServerSelectPanel           = "UIs/LogIn/ServerSelectPanel";                        // 12.服务器选择面板
    public static string        StartMoviePanel             = "UIs/LogIn/StartMoviePanel";                          // 13.开场动画
    #endregion

    #region 主界面  （Main）
    public static string        BuyCoinsPanel               = "UIs/Main/BuyCoinsPanel";                             // 1.购买金币面板
    public static string        BuyedCoinsPanel             = "UIs/Main/BuyedCoinsPanel";                           // 2.已购买金币面板
    public static string        ChangePalyerNamePanel       = "UIs/Main/ChangePalyerNamePanel";                     // 3.更换玩家名称面板
    public static string        ChangePalyerIconPanel       = "UIs/Main/ChangePalyerIconPanel";                     // 4.更换玩家图标面板
    public static string        EnterPalyerNamePanel        = "UIs/Main/EnterPalyerNamePanel";                      // 5.输入玩家名称面板
    public static string        EnterGiftCodePanel          = "UIs/Main/EnterGiftCodePanel";                        // 6.输入礼包码面板

    public static string        MainUIPanel                 = "UIs/Main/MainUiPanel";                               // 主界面UI面板
    public static string        ReChargePanel               = "UIs/Main/ReChargePanel";                             // 充值面板
    public static string        PayCoinsPanel               = "UIs/Main/PayCoinsPanel";                             // 花费金币面板
    public static string        RoleAttributePanel          = "UIs/Main/RoleAttributePanel";                        // 角色属性面板
    public static string        SystemSetPanel              = "UIs/Main/SystemSetPanel";                            // 系统设置
    public static string        HeroIconItem                = "UIs/Main/HeroIconItem";                              // 英雄头像

    public static string        TalkerInfoPanel             = "UIs/Chat/TalkerInfoPanel";                           // 聊天信息面板
    public static string        TeamSysPanel                = "UIs/TeamBenefitSys/TeamSysPanel";                    // 战队系统面板


    #endregion

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////（4）

    #region 关卡系统 （CheckPoint）
    public static string        CheckPointConfirmPanel      = "UIs/CheckPoint/CheckPointConfirmPanel";              // 01.关卡确认面板
    public static string        CheckPointIconItem          = "UIs/CheckPoint/CheckPointIconItem";                  // 02.关卡图标项
    public static string        CheckPointItem              = "UIs/CheckPoint/CheckPointItem";                      // 03.关卡项
    public static string        CheckPointSelectPanel       = "UIs/CheckPoint/CheckPointSelectPanel";               // 04.关卡选择面板
    public static string        SweepPanel                  = "UIs/CheckPoint/SweepPanel";                          // 05.扫荡面板

    public static string        SweepResultItem             = "UIs/CheckPoint/SweepResultItem";                     // 06.扫荡结果项
    public static string        WorldMapPanel               = "UIs/CheckPoint/WorldMapPanel";                       // 07.世界地图面板
    public static string        CheckPointMapItem           = "UIs/CheckPoint/CheckPointMapItem";                   // 08.关卡地图项
    public static string        GetBoxPanel                 = "UIs/CheckPoint/GetBoxPanel";                         // 09.获取关卡宝箱面板
    public static string        BoxRewardItem               = "UIs/CheckPoint/BocRewardItem";                       // 10.宝箱奖励项
    public static string        LeaderShowpanel             = "UIs/CheckPoint/LeaderShowPanel";                     // 11.引导显示面板
    #endregion

    #region 战斗阵容 （BattleLineUp）
    public static string        BattleLineUpPanel           = "UIs/BattleLineUp/BattleLineUpPanel";                 // 1.战斗阵容面板
    public static string        BattleComparePanel          = "UIs/BattleLineUp/BattleComparepanel";                // 2.战斗比较面板
    public static string        HeroNumTipsPanle            = "UIs/BattleLineUp/HeroNumTipsPanel";                  // 3.英雄人数Tips
    public static string        BattleLineUpHeroIconItem    = "UIs/BattleLineUp/BattleLineUpHeroIconItem";          // 4.阵容头像图标

    public static string        BattlePositionPanel         = "UIs/BattlePosition/BattlePositionPanel";             // 1.战斗预置阵容面板
    public static string        BP_HeroIconItem             = "UIs/BattlePosition/BP_HeroIconItem";                 // 2.战斗预置阵容 英雄头像Item
    #endregion

    #region 战斗系统 （BattleSys）
    public static string        PVE_Victory                 = "UIs/BattleSys/PVE_Victory";                          // 1.战斗胜率
    public static string        PVE_Failure                 = "UIs/BattleSys/PVE_Failure";                          // 2.战斗失败

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    //-----------------------------------------------------------------------------------------------------------------=（3）
    #region 英雄系统 （HeroSys）
    public static string        EquipAllPanel               = "UIs/HeroSys/EquipAllPanel";                          // 01.装备所有面板
    public static string        EquipAllItem                = "UIs/HeroSys/EquipAllItem";                           // 02.装备所有项
    public static string        EquipGetItem                = "UIs/HeroSys/EquipGetItem";                           // 03.装备获得项
    public static string        EquipPanel                  = "UIs/HeroSys/EquipPanel";                             // 04.装备面板
    public static string        GetHeroShowPanel            = "UIs/HeroSys/GetHeroShowPanel";                       // 05.获得英雄展示面板

    public static string        GetSoulPanel                = "UIs/HeroSys/GetSoulPanel";                           // 06.获得魂石面板
    public static string        HeroEquipItem               = "UIs/HeroSys/HeroEquipItem";                          // 07.英雄装备项
    public static string        HeroInfoPanel               = "UIs/HeroSys/HeroInfoPanle";                          // 08.英雄信息面板
    public static string        HeroItem                    = "UIs/HeroSys/HeroItem";                               // 09.英雄项
    public static string        HeroListShowPanel           = "UIs/HeroSys/HeroListShowPanel";                      // 10.英雄列表展示面板

    public static string        HeroTipsPanel               = "UIs/HeroSys/HeroTipsPanel";                          // 11.英雄Tips提示面板
    public static string        MercInfoPanel               = "UIs/HeroSys/MercInfoPanel";                          // 12.佣兵信息面板
    public static string        ProfessionPanel             = "UIs/HeroSys/ProfessionPanel";                        // 13.职业面板
    public static string        UpSartOrLevelSuccessPanel   = "UIs/HeroSys/UpSartOrLevelSuccessPanel";              // 14.升星或升阶成功面板
    public static string        SkillItem                   = "UIs/HeroSys/SkillItem";                              // 15.技能项
    public static string        SoulGetItem                 = "UIs/HeroSys/SoulGetItem";                            // 16.魂石获取项
    #endregion

    #region 签到系统 （CheckIn） 
    public static string        CheckInPanel                = "UIs/CheckIn/CheckInPanel";                           // 1.签到面板
    public static string        CheckInAwardPanel           = "UIs/CheckIn/CheckInAwardPanel";                      // 2.签到奖励面板
    public static string        CheckInItem                 = "UIs/CheckIn/CheckInItem";                            // 3.签到项
    public static string        Effect                      = "UIs/CheckIn/Effect";                                 // 4.特效
    #endregion

    #region 充值系统 （Recharge）
    public static string        RechargePanel               = "UIs/Recharge/RechargePanel";                         // 1.充值面板
    public static string        VIPShowPanel                = "UIs/Recharge/VIPShowPanel";                          // 2.VIP展示面板
    public static string        VIPShow                     = "UIs/Recharge/VIPShow";                               // 3.VIP展示
    public static string        FirstRechargePanel          = "UIs/Recharge/FirstRechargePanel";                    // 4.首充面板
    #endregion

    #region 商城系统 （Mall）
    public static string        CardItemPanel               = "UIs/Mall/CardItemPanel";                             // 1.抽卡项面板
    public static string        MainMallPanel               = "UIs/Mall/MainMallPanel";                             // 2.商城主面板
    public static string        OnceCallPanel               = "UIs/Mall/OnceCallPanel";                             // 3.一抽面板
    public static string        TenTimesPanel               = "UIs/Mall/TenTimesPanel";                             // 4.十抽面板
    public static string        HeroCardShowPanel           = "UIs/Mall/HeroCardShowPanel";                         // 5.英雄卡展示面板
    #endregion

    #region 商人系统 （Merchant）
    public static string        MerchantViewPanel           = "UIs/Merchant/MerchantViewPanel";                     // 1.商人视图面板
    public static string        MerchantItemView            = "UIs/Merchant/MerchantItemView";                      // 2.商人项
    public static string        MerchantBuyViewPanel        = "UIs/Merchant/MerchantBuyViewPanel";                  // 3.商人购买视图面板
    public static string        MerchantSellPanel           = "UIs/Merchant/MerchantSellPanel";                     // 4.商人售出面板
    public static string        MerchantSellItem            = "UIs/Merchant/NerchantSellPanel";                     // 5.商人售出项
    #endregion

    #region 活动系统 （Activity）
    public static string        SevenDaysPanel              = "UIs/SevenDays/SevenDaysPanel";                       // 00.new_七日活动面板

    public static string        ActivityViewPanel           = "UIs/Activity/ActivityViewPanel";                     // 01.活动视图面板
    public static string        ActivityBlankPanel          = "UIs/Activity/ActivityBlankPanel";                    // 02.空白活动面板
    public static string        ActivityItemSprite          = "UIs/Activity/ActivityItemSprite";                    // 03.活动项图标
    public static string        BackGround                  = "UIs/Activity/BackGround";                            // 04.返回组
    public static string        FirstActivityItem           = "UIs/Activity/FirstActivityItem";                     // 05.首次活动项

    public static string        ForeverGiftItem             = "UIs/Activity/ForeverGiftItem";                       // 06.永久礼包
    public static string        ForeverGiftPanel            = "UIs/Activity/ForeverGiftPanel";                      // 07.永久礼包面板
    public static string        ForeverSelledPanel          = "UIs/Activity/ForeverSelledPanel";                    // 08.永久已售出面板
    public static string        FundUpItem                  = "UIs/Activity/FundUpItem";                            // 09.成长基金项
    public static string        FundUpPanel                 = "UIs/Activity/FundUpPanel";                           // 10.成长基金面板

    public static string        GiftItem                    = "UIs/Activity/GiftItem";                              // 11.礼包项
    public static string        LuckyRollerPanel            = "UIs/Activity/LuckyRollerPanel";                      // 12.幸运转盘面板
    public static string        MonthCardPanel              = "UIs/Activity/MonthCardPanel";                        // 13.月卡面板
    public static string        SevenLogInPanel             = "UIs/Activity/SevenLogInPanel";                       // 14.七日登录面板
    public static string        VIPGiftPanel                = "UIs/Activity/VIPGiftPanel";                          // 15.VIP礼包面板
    public static string        VIPGiftPanel2               = "UIs/Activity/VIPGiftPanel2";                         // 16.VIP礼包面板2
    #endregion


    #region 背包系统 （Bag）
    public static string        BagItem                     = "UIs/Bag/BagItem";                                    // 01.背包项
    public static string        HeroUpLevelPanel            = "UIs/Bag/HeroUpLevelPanel";                           // 02.英雄升级面板
    public static string        Hero                        = "UIs/Bag/Hero";                                       // 03.英雄
    public static string        ItemBagPanel                = "UIs/Bag/ItemBagPanel";                               // 04.物品背包面板
    public static string        PropItem                    = "UIs/Bag/PropItem";                                   // 05.道具项

    public static string        UseConfirmPanel             = "UIs/Bag/UseConfirmPanel";                            // 06.使用确认面板
    public static string        UseItemPanel                = "UIs/Bag/UseItemPanel";                               // 07.使用物品面板
    public static string        UsetiliPropPanel            = "UIs/Bag/UsetiliPropPanel";                           // 08.使用体力道具面板
    public static string        SellItemPanel               = "UIs/Bag/SellItemPanel";                              // 09.售出物品面板
    public static string        SellPanel                   = "UIs/Bag/SellPanel";                                  // 10.售出面板
    #endregion

    #region 任务系统 （Task）
    public static string        TaskPanel                   = "UIs/Task/TaskPanel";                                 // 1.任务面板
    public static string        GridViewItem                = "UIs/Task/GridViewItem";                              // 2.格子视图项目
    #endregion

    //-----------------------------------------------------------------------------------------------------------------=（8）
    #region 排行榜 （Ranking）
    public static string        RankingListPanel            = "UIs/Ranking/RankingListPanel";
    public static string        Ranking1                    = "UIs/Ranking/Ranking1";
    public static string        Ranking2                    = "UIs/Ranking/Ranking2";
    public static string        Ranking3                    = "UIs/Ranking/Ranking3";
    public static string        RankingListInfoMem          = "UIs/Ranking/RankingListInfoMem";
    #endregion                                                
    #region 好友   （Friend）
    public static string        Friend                      = "UIs/Friend/Friend";
    public static string        FriendPanel                 = "UIs/Friend/FriendPanel";
    public static string        ContestMemInfo              = "UIs/Friend/ContestMemInfo";
    public static string        TextTipsPanel               = "UIs/Friend/TextTipsPanel";
    #endregion
    #region 邮箱   （Email）
    public static string        EmailTotalPanel             = "UIs/Email/EmailToatlPanel";
    public static string        EmailItem                   = "UIs/Email/EmailItem";
    public static string        EmailBGView                 = "UIs/Email/EmailBGView";
    public static string        EmailRewardBGView           = "UIs/Email/EmailRewardBGView";
    #endregion
    #region 翅膀   （Wing）
    public static string        WingSysPanel                = "UIs/WingSys/WingSysPanel";                             // 1.翅膀系统面板
    public static string        WingPreviewPanel            = "UIs/WingSys/WingPreviewPanel";                     // 2.翅膀预览面板
    public static string        GetWingPanel                = "UIs/WingSys/GetWingPanel";                             // 3.获得翅膀面板
    public static string        GetWingUpdateObjPanel       = "UIs/WingSys/GetWingUpdeteObjPanel";           // 4.获得翅膀更新对象面板
    public static string        WingPreview                 = "UIS/WingSys/WingPreview";                               // 5.翅膀预览
    public static string        WingUpSuccessPanel          = "UIs/WingSys/WingUpSuccessPanel";                 // 6.翅膀升级成功面板
    #endregion
    #region 佣兵   （Merc）
    public static string        ModelShow                   = "UIs/TeamBenefitSys/ModelShow";                                    //1.模型显示
    public static string        MecenaryPanel               = "UIs/TeamBenefitSys/MecenaryPanel";                            //2.佣兵面板
    public static string        MecenaryMedalPanel          = "UIs/TeamBenefitSys/MecenaryMedalPanel";                  //3.佣兵勋章面板
    public static string        MecenaryMedalItem           = "UIs/TeamBenefitSys/MecenaryMedalItem";                    //4.佣兵勋章项
    public static string        MecenaryJinHua              = "UIs/TeamBenefitSys/MecenaryJinHua";                          //5.佣兵进化

    public static string        ShuXingItem                 = "UIs/TeamBenefitSys/ShuXingItem";                                //6.属性项
    public static string        MecenaryInfo                = "UIs/TeamBenefitSys/MecenaryInfo";                              //7.佣兵信息
    public static string        GetMecenaryUpdateObjPanel   = "UIs/TeamBenefitSys/GetMecenaryUpdateObjPanel";    //8.获得佣兵更新对象面板
    public static string        MecenaryPanel_2             = "UIs/TeamBenefitSys/MecenaryPanel_2";                        //9.佣兵面板2
    public static string        UpJieSuccesssPanel          = "UIs/TeamBenefitSys/UpJieSuccesssPanel";                  //10.升阶成功面板
    #endregion
    #region 宝石   （Gemstone）
    public static string        GemstonePanel = "UIs/Gemstone/GemstonePanel";
    public static string        GemstoneHeroItem = "UIs/Gemstone/GemstoneHeroItem";
    #endregion
    #region 公会   （Guild）
    public static string SociatyPanel = "UIs/Sociaty/Sociaty";
    public static string GetSociatyPanel = "UIs/Sociaty/GetSociatyPanel";
    public static string SociatyInfoMem = "UIs/Sociaty/SociatyListInfoMem";
    public static string SetSociatyIconPanel = "UIs/Sociaty/SetSociatyIconPanel";
    public static string SociatyIcon = "UIs/Sociaty/SociatyIcon";
    public static string SociatyMember = "UIs/Sociaty/SociatyMember";
    public static string MangerSociatyPanel = "UIs/Sociaty/ManagerSociatyPanel";
    public static string SociatyContributionPanel = "UIs/Sociaty/ContrilbutePanel";
    public static string SociatyCarbonPanel = "UIs/Sociaty/SociatyPassInfoPanel";
    public static string SociatyWorShipPanel = "UIs/Sociaty/WorShipPanel";
    public static string SociatyWareHousePanel = "UIs/Sociaty/DepotPanel";
    public static string SociatyChangeNamePanel = "UIs/Sociaty/SociatyRenamePanel";
    public static string SociatySetNoticePanel = "UIs/Sociaty/SociatyNoticePanel";
    public static string SociatyMerchantItem = "UIs/Sociaty/SociatyMerchantItemView";
    public static string JoinInSetPanel = "UIs/Sociaty/JoinInSetPanel";
    public static string ActivityPanel = "UIs/Sociaty/ActivityPanel";
    public static string ActivityMember = "UIs/Sociaty/ActicityMember";
    public static string MangerMemPanel = "UIs/Sociaty/ManageMemberPanel";
    public static string ApplyDealPanel = "UIs/Sociaty/ApplyDealPanel";
    public static string ApplyMembers = "UIs/Sociaty/ApplyMembers";
    public static string SociatyPassInPanel = "UIs/Sociaty/SociatyPassInPanel";
    public static string Texture = "UIs/Sociaty/Texture";
    public static string DepotItem = "UIs/Sociaty/DepotItem";
    public static string RecordPanel = "UIs/Sociaty/SociatyRecordPanel";
    public static string Record = "UIs/Sociaty/Record";
    public static string DistrubutePanel = "UIs/Sociaty/DealWithRewardsPanel";
    public static string DistributeMem = "UIs/Sociaty/ContributionMembers";
    public static string DistributeRecordPanel = "UIs/Sociaty/DistributeRecordPanel";
    public static string DistributeRecord = "UIs/Sociaty/DistributeRecord";
    public static string PassInfoShowPanel = "UIs/Sociaty/PassInfoShowPanel";
    public static string Passitem = "UIs/Sociaty/PassItem";
    public static string SociatyFriendsPanel = "UIs/Sociaty/SociatyFrendPanel";
    public static string SociatyCheapterInfo = "UIs/Sociaty/SociayPassInfo";

    //  公会商店
    public static string SociatyMerchantViewPanel = "UIs/Sociaty/SociatyMerchantViewPanel";
    public static string SociatyMerchantBuyViewPanel = "UIs/Sociaty/SociatyMerchantBuyViewPanel";
    public static string WorShipInfoPanel = "UIs/Sociaty/WorShipInfoPanel";
    public static string WorShipPanel = "UIs/Sociaty/WorShipPanel";
    // public static string ContrilbutePanel = "UIs/Sociaty/ContrilbutePanel";
    #endregion

    //-----------------------------------------------------------------------------------------------------------------=（7）
    #region 竞技场   （JJC）
    public static string JJCHeroIconItem = "UIs/JJC/JJCHeroIconItem";                                       // 01.竞技场英雄图标项
    public static string JJCPanel = "UIs/JJC/JJCPanel";                                                     // 02.竞技场面板
    public static string JJCAwardItem = "UIs/JJC/JJCAwardItem";                                             // 03.竞技场奖励项
    public static string BattleResultPanel = "UIs/JJC/BattleResultPanel";                                   // 04.战斗结果面板
    public static string DeffanceLineUpPanel = "UIs/JJC/DeffanceLineUpPanel";                               // 05.防御阵容面板
        
    public static string FightItem = "UIs/JJC/FightItem";                                                   // 06.攻击项
    public static string FightRecordPanel = "UIs/JJC/FightRecordPanel";                                     // 07.攻击记录面板
    public static string FightResultItem = "UIs/JJC/FIghtResultItem";                                       // 08.攻击结果项
    public static string JJCBuyConfirmPanel = "UIs/JJC/JJCBuyConfirmPanel";                                 // 09.竞技场购买确认面板
    public static string JJCStorePanel = "UIs/JJC/JJCStorePanel";                                           // 10.竞技场商店面板

    public static string RulesPanel = "UIs/JJC/RulesPanel";                                                 // 11.规则面板
    public static string RankingPanel = "UIs/JJC/RankingPanel";                                             // 12.排行榜
    public static string EnemyLineUpPanel = "UIs/JJC/EnemyLineUpPanel";                                     // 13.敌方阵容面板
    #endregion

    #region 巨兽囚笼 （MonsterWar）
    public static string MonsterWarpanel = "UIs/MonsterWar/MonsterWarPanel";                                // 1.巨兽囚笼面板
    public static string MonsterWarInPanel = "UIs/MonsterWar/MonsterWarInpanel";                            // 2.巨兽进入面板
    public static string MonsterWarPassExpainPanel = "UIs/MonsterWar/MonsterWarExpainPanel";                // 3.巨兽点击说明面板
    public static string MonsterWarPassShowPanel = "UIs/MonsterWar/";                                       // 4.巨兽点击显示面板

    public static string MonsterBear = "boss_Brownbear_skin";                                               // 5.Boss贴图
    #endregion

    #region 巨龙试炼 （DragonTrail）
    public static string DragonPanel = "UIs/DragonWar/DragonPanel";                                         // 01.巨龙面板
    public static string DragonWarInPanel = "UIs/DragonWar/DragonWarInpanel";                               // 02.巨龙进入面板
    public static string DragonWarExplainPanel = "UIs/DragonWar/DragonWarExplainPanel";                     // 03.巨龙说明面板
    public static string DragonWarPassShowPanel = "UIs/DragonWar/DragonWarPassShowPanel";                   // 04.巨龙点击显示面板
    public static string DragonHerosPanel = "UIs/DragonWar/DragonHerosPanel";                               // 05.巨龙英雄面板

    public static string IceDragon = "UIs/DragonWar/IceDragon";                                             // 06.冰龙
    public static string FireDragon = "UIs/DragonWar/FireDragon";                                           // 07.火龙
    public static string ThunderDragon = "UIs/DragonWar/ThunderDragon";                                     // 08.雷龙
    public static string TalentHero = "UIs/DragonWar/TalentHero";                                           // 09.英雄天赋
    public static string DragonInfoPanel = "UIs/DragonWar/DragonInfoPanel";                                 // 10.巨龙信息面板
    public static string DragonTipsPanel = "UIs/DragonWar/DragonTipsPanel";                                 // 11.巨龙提示面板
    #endregion

    #region 天堂之路 （ParadiseRoad）
    public static string Pass = "UIs/FreeWay/Pass";                                                         // 1.点击
    public static string FreeWayPanel = "UIs/FreeWay/FreeWayPanel";                                         // 2.天堂之路面板
    public static string Cloud = "UIs/FreeWay/Cloud";                                                       // 3.云
    public static string Heros = "UIs/FreeWay/HerosPanel";                                                  // 4.英雄面板
    public static string Herolist = "UIs/FreeWay/Hero";                                                     // 5.英雄
    public static string InfoPanel = "UIs/FreeWay/InfoPanel";                                               // 6.信息面板
    public static string FreeWayShopPanel = "UIs/FreeWay/ShopPanel";                                        // 7.天堂之路商店面板
    public static string BuyItemPanel = "UIs/FreeWay/BuyItemPanel";                                         // 8.购买物品面板
    #endregion

    #region 秘境之塔 （SecretTower）
    public static string RulePanel = "UIs/SecretTower/RulePanel";                                           // 1.规则面板
    public static string SecretTowerBuyViewPanel = "UIs/SecretTower/SecretTowerBuyViewPanel";               // 2.秘境塔购买面板
    public static string SecretTowerLevelItem = "UIs/SecretTower/SecretTowerLevelItem";                     // 3.等级项
    public static string SecretTowerPanel = "UIs/SecretTower/SecretTowerPanel";                             // 4.秘境塔面板
    public static string SecretTowerPropItem = "UIs/SecretTower/SecretTowerPropItem";                       // 5.秘境塔道具项
    public static string SecretTowerShopItemView = "UIs/SecretTower/SecretTowerShopItemView";               // 6.秘境塔商店项
    public static string SecretTowerShopViewPanel = "UIs/SecretTower/SecretTowerShopViewPanel";             // 7.秘境塔商店展示面板
    public static string SweepOverPanel = "UIs/SecretTower/SweepOverPanel";                                 // 8.扫荡完成面板
    #endregion

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////=（5）

}


public class UIPanelName
{
//    public static string Test = "GameObject";                                                    // 测试

//    #region 全局通用（Global）
//    public static string BlackPanel = "BlackPanel";                                              // 01.返回面板
//    public static string DialogPanel = "DialogPanel";                                            // 02.对话面板
//    public static string HidePanel30Floor = "HidePane30Floor";                                   // 03.隐藏30Floor面板
//    public static string MaskPanel = "MaskPanel";                                                // 04.遮罩面板
//    public static string PassSkillTipsPanel = "PassSkillTipspanel";                              // 05.点击技能Tips提示面板

//    public static string PublicPanel = "PublicPanel";                                            // 06.公告面板
//    public static string PropUpInfoPanel = "PropUpInfopanel";                                    // 07.维持信息面板
//    public static string PropUpSpritePanel = "PropUpSpritePanel";                                // 08.维持图标面板
//    public static string PropUpSpritePanel2 = "PropUpSpritePanel2";                              // 09.维持图标面板2
//    public static string PropUpTextPanel = "PropUpTextPanel";                                    // 10.维持文本面板

    public static string RewardAllPanelName = "RewardAllPanel";                                      // 11.所有奖励面板
    public static string RewardGridName = "RewardGrid";                                              // 12.奖励格子
    public static string RewardCoinsItemName = "RewardCoinsItem";                                    // 13.奖励货币
    public static string RewardPropItemName = "RewardPropItem";                                      // 14.奖励道具
    public static string SkillTipsPanel = "SkillTipsPanel";                                      // 15.技能Tips提示面板

//    public static string ShortLoadingPanel = "ShortLoadingPanel";                                // 16.短加载面板
//    public static string StaminaTipsPanel = "tiliTipsPanel";                                     // 17.体力Tips提示面板
//    public static string ShowAccountInfo = "ShowAccountInfo";                                    // 18.展示账户信息
//    public static string TipsPanel = "TipsPanel";                                                // 19.Tips提示面板
//    public static string ViewEmptyInfoPanel = "ViewEmptyInfoPanel";                              // 20.界面为空显示
//    public static string WarningPanel = "WarningPanel";                                          // 21.警告面板
//    public static string FightForcePartAnimCureve = "FightForcePartAnimCureve";                  // 22.战斗力粒子动画曲线
//    #endregion

//    #region 新手引导（Guide）
//    public static string GuidePanel = "GuidePanel";                                               // 1.新手引导面板
//    public static string GuideRoot = "GuideRoot";                                                 // 2.新手引导入口

//    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//    #endregion

//    #region 登录界面（LogIn）
//    public static string GameEnterPanel = "GameEnterPanel";                                       // 01.进入游戏面板
//    public static string LoadingPanel = "LoadingPanel";                                           // 02.加载面板
//    public static string LogInOrRegisterPanel = "LogInRegisterPanel";                             // 03.（登录or注册）面板
//    public static string PublicItem = "PublicItem";                                               // 04.公告项
//    public static string RegisterPanel = "RegisterPanel";                                         // 05.注册面板
//    public static string ReLogInPanel = "ReLogInPanel";                                           // 06.消息返回面板

//    public static string RoleSelectPanel    = "RoleSelectPanel";                                     // 07.主角选择面板
//    public static string ServerList         = "ServerList";                                               // 08.服务器列表
//    public static string ServerLineItem     = "ServerLineItem";                                               // 09.服务器项
//    public static string ServerZoneItem     = "ServerListerItem";                                      // 10.服务器列表项
//    public static string ServerSelectPanel  = "ServerSelectPanel";                                 // 11.服务器选择面板
//    public static string StartMoviePanel    = "StartMoviePanel";                                     // 12.开场动画
//    #endregion

//    #region 主界面  （Main）
//    public static string BuyCoinsPanel = "BuyCoinsPanel";                                          // 1.购买金币面板
//    public static string BuyedCoinsPanel = "BuyedCoinsPanel";                                      // 2.已购买金币面板
//    public static string ChangePalyerNamePanel = "ChangePalyerNamePanel";                          // 3.更换玩家名称面板
//    public static string ChangePalyerIconPanel = "ChangePalyerIconPanel";                          // 4.更换玩家图标面板
//    public static string EnterPalyerNamePanel = "EnterPalyerNamePanel";                            // 5.输入玩家名称面板
//    public static string EnterGiftCodePanel = "EnterGiftCodePanel";                                // 6.输入礼包码面板

//    public static string MainUIPanel = "MainUiPanel";                                              // 主界面UI面板
//    public static string ReChargePanel = "ReChargePanel";                                          // 充值面板
//    public static string PayCoinsPanel = "PayCoinsPanel";                                          // 花费金币面板
//    public static string RoleAttributePanel = "RoleAttributePanel";                                // 角色属性面板
//    public static string SystemSetPanel = "SystemSetPanel";                                        // 系统设置
//    public static string HeroIconItem = "HeroIconItem";                                            // 英雄头像

//    public static string TalkerInfoPanel = "TalkerInfoPanel";                                      // 聊天信息面板
//    public static string TeamSysPanel = "TeamSysPanel";                                  // 战队系统面板


//    #endregion

//    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////（4）

//    #region 关卡系统 （CheckPoint）
//    public static string CheckPointConfirmPanel = "CheckPointCofirmPanel";                   // 01.关卡确认面板
//    public static string CheckPointIconItem = "CheckPointIconItem";                          // 02.关卡图标项
//    public static string CheckPointItem = "CheckPointItem";                                  // 03.关卡项
//    public static string CheckPointSelectPanel = "CheckPointSelectPanel";                    // 04.关卡选择面板
//    public static string SweepPanel = "SweepPanel";                                          // 05.扫荡面板

//    public static string SweepResultItem = "SweepResultItem";                                // 06.扫荡结果项
//    public static string WorldMapPanel = "WordMapPanel";                                     // 07.世界地图面板
//    public static string CheckPointMapItem = "CheckPointMapItem";                            // 08.关卡地图项
//    public static string GetBoxPanel = "GetBoxPanel";                                        // 09.获取关卡宝箱面板
//    public static string BoxRewardItem = "BocRewardItem";                                    // 10.宝箱奖励项
//    public static string LeaderShowpanel = "LeaderShowPanel";                                // 11.引导显示面板
//    #endregion

//    #region 战斗阵容 （BattleLineUp）
//    public static string BattleLineUpPanel = "BattleLineUpPanel";                          // 1.战斗阵容面板
//    public static string BattleComparePanel = "BattleComparepanel";                        // 2.战斗比较面板
//    public static string HeroNumTipsPanle = "HeroNumTipsPanel";                            // 3.英雄人数Tips
//    public static string BattleLineUpHeroIconItem = "BattleLineUpHeroIconItem";            // 4.阵容头像图标
//    #endregion

//    #region 战斗系统 （BattleSys）
//    public static string PVE_Victory = "PVE_Victory";                                         // 1.战斗胜率
//    public static string PVE_Failure = "PVE_Failure";                                         // 2.战斗失败

//    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//    #endregion

//    //-----------------------------------------------------------------------------------------------------------------=（3）
//    #region 英雄系统 （HeroSys）
//    public static string EquipAllPanel = "EquipAllPanel";                                       // 01.装备所有面板
//    public static string EquipAllItem  = "EquipAllItem";                                        // 02.装备所有项
//    public static string EquipGetItem  = "EquipGetItem";                                        // 03.装备获得项
//    public static string EquipPanel    = "EquipPanel";                                          // 04.装备面板
//    public static string GetHeroShowPanel = "GetHeroShowPanel";                                 // 05.获得英雄展示面板

//    public static string GetSoulPanel   = "GetSoulPanel";                                       // 06.获得魂石面板
//    public static string HeroEquipItem  = "HeroEquipItem";                                      // 07.英雄装备项
//    public static string HeroInfoPanel  = "HeroInfoPanle";                                      // 08.英雄信息面板
//    public static string HeroItem       = "HeroItem";                                           // 09.英雄项
//    public static string HeroShowPanel  = "HeroShowPanel";                                      // 10.英雄展示面板

//    public static string HeroTipsPanel  = "HeroTipsPanel";                                      // 11.英雄Tips提示面板
//    public static string MercInfoPanel  = "MercInfoPanel";                                      // 12.佣兵信息面板
//    public static string ProfessionPanel = "ProfessionPanel";                                   // 13.职业面板
//    public static string UpSartOrLevelSuccessPanel = "UpSartOrLevelSuccessPanel";               // 14.升星或升阶成功面板
//    public static string SkillItem = "SkillItem";                                               // 15.技能项
//    public static string SoulGetItem = "SoulGetItem";                                           // 16.魂石获取项
//    #endregion

//    #region 签到系统 （CheckIn） 
//    public static string CheckInPanel = "CheckInPanel";                                         // 1.签到面板
//    public static string CheckInAwardPanel = "CheckInAwardPanel";                               // 2.签到奖励面板
//    public static string CheckInItem = "CheckInItem";                                           // 3.签到项
//    public static string Effect = "Effect";                                                     // 4.特效
//    #endregion

//    #region 充值系统 （Recharge）
//    public static string RechargePanel = "RechargePanel";                                      // 1.充值面板
//    public static string VIPShowPanel = "VIPShowPanel";                                        // 2.VIP展示面板
//    public static string VIPShow = "UVIPShow";                                                 // 3.VIP展示
//    public static string FirstRechargePanel = "FirstRechargePanel";                            // 4.首充面板
//    #endregion

//    #region 商城系统 （Mall）
//    public static string CardItemPanel = "CardItemPanel";                                          // 1.抽卡项面板
//    public static string MainMallPanel = "MainMallPanel";                                          // 2.商城主面板
//    public static string OnceCallPanel = "UOnceCallPanel";                                         // 3.一抽面板
//    public static string TenTimesPanel = "UTenTimesPanel";                                         // 4.十抽面板
//    public static string HeroCardShowPanel = "HeroCardShowPanel";                                  // 5.英雄卡展示面板
//    #endregion

//    #region 商人系统 （Merchant）
//    public static string MerchantViewPanel = "MerchantViewPanel";                              // 1.商人视图面板
//    public static string MerchantItemView = "MerchantItemView";                                // 2.商人项
//    public static string MerchantBuyViewPanel = "MerchantBuyViewPanel";                        // 3.商人购买视图面板
//    public static string MerchantSellPanel = "MerchantSellPanel";                              // 4.商人售出面板
//    public static string MerchantSellItem = "NerchantSellPanel";                               // 5.商人售出项
//    #endregion

//    #region 活动系统 （Activity）
//    public static string SevenDaysPanel = "SevenDaysPanel";                                    // 00.new_七日活动面板

//    public static string ActivityViewPanel = "ActivityViewPanel";                              // 01.活动视图面板
//    public static string ActivityBlankPanel = "ActivityBlankPanel";                            // 02.空白活动面板
//    public static string ActivityItemSprite = "ActivityItemSprite";                            // 03.活动项图标
//    public static string BackGround = "BackGround";                                            // 04.返回组
//    public static string FirstActivityItem = "FirstActivityItem";                              // 05.首次活动项

//    public static string ForeverGiftItem = "ForeverGiftItem";                                  // 06.永久礼包
//    public static string ForeverGiftPanel = "ForeverGiftPanel";                                // 07.永久礼包面板
//    public static string ForeverSelledPanel = "ForeverSelledPanel";                            // 08.永久已售出面板
//    public static string FundUpItem = "FundUpItem";                                            // 09.成长基金项
//    public static string FundUpPanel = "FundUpPanel";                                          // 10.成长基金面板

//    public static string GiftItem = "GiftItem";                                                // 11.礼包项
//    public static string LuckyRollerPanel = "LuckyRollerPanel";                                // 12.幸运转盘面板
//    public static string MonthCardPanel = "MonthCardPanel";                                    // 13.月卡面板
//    public static string SevenLogInPanel = "SevenLogInPanel";                                  // 14.七日登录面板
//    public static string VIPGiftPanel = "VIPGiftPanel";                                        // 15.VIP礼包面板
//    public static string VIPGiftPanel2 = "VIPGiftPanel2";                                      // 16.VIP礼包面板2
//    #endregion


//    #region 背包系统 （Bag）
//    public static string BagItem = "BagItem";                                                       // 01.背包项
//    public static string HeroUpLevelPanel = "HeroUpLevelPanel";                                     // 02.英雄升级面板
//    public static string Hero = "Hero";                                                             // 03.英雄
//    public static string ItemBagPanel = "ItemBagPanel";                                             // 04.物品背包面板
//    public static string PropItem = "PropItem";                                                     // 05.道具项

//    public static string UseConfirmPanel = "UseConfirmPanel";                                       // 06.使用确认面板
//    public static string UseItemPanel = "UseItemPanel";                                             // 07.使用物品面板
//    public static string UsetiliPropPanel = "UsetiliPropPanel";                                     // 08.使用体力道具面板
//    public static string SellItemPanel = "SellItemPanel";                                           // 09.售出物品面板
//    public static string SellPanel = "SellPanel";                                                   // 10.售出面板
//    #endregion

//    #region 任务系统 （Task）
//    public static string TaskPanel = "TaskPanel";                                                  // 1.任务面板
//    public static string GridViewItem = "GridViewItem";                                            // 2.格子视图项目
//    #endregion

//    //-----------------------------------------------------------------------------------------------------------------=（8）
//    #region 排行榜 （Ranking）
//    public static string RankingListPanel = "RankingListPanel";
//    public static string Ranking1 = "Ranking1";
//    public static string Ranking2 = "Ranking2";
//    public static string Ranking3 = "Ranking3";
//    public static string RankingListInfoMem = "RankingListInfoMem";
//    #endregion                                                
//    #region 好友   （Friend）
//    public static string Friend = "Friend";
//    public static string FriendPanel = "FriendPanel";
//    public static string ContestMemInfo = "ContestMemInfo";
//    public static string TextTipsPanel = "TextTipsPanel";
//    #endregion

//    #region 邮箱   （Email）
//    public static string EmailTotalPanel = "EmailToatlPanel";
//    public static string EmailItem = "EmailItem";
//    public static string EmailBGView = "EmailBGView";
//    public static string EmailRewardBGView = "EmailRewardBGView";
//    #endregion

//    #region 翅膀   （Wing）
//    public static string WingSysPanel = "WingSysPanel";                             // 1.翅膀系统面板
//    public static string WingPreviewPanel = "WingPreviewPanel";                     // 2.翅膀预览面板
//    public static string GetWingPanel = "GetWingPanel";                             // 3.获得翅膀面板
//    public static string GetWingUpdateObjPanel = "GetWingUpdeteObjPanel";           // 4.获得翅膀更新对象面板
//    public static string WingPreview = "WingPreview";                               // 5.翅膀预览
//    public static string WingUpSuccessPanel = "WingUpSuccessPanel";                 // 6.翅膀升级成功面板
//    #endregion

//    #region 佣兵   （Merc）
//    public static string ModelShow = "ModelShow";                                    //1.模型显示
//    public static string MecenaryPanel = "MecenaryPanel";                            //2.佣兵面板
//    public static string MecenaryMedalPanel = "MecenaryMedalPanel";                  //3.佣兵勋章面板
//    public static string MecenaryMedalItem = "MecenaryMedalItem";                    //4.佣兵勋章项
//    public static string MecenaryJinHua = "MecenaryJinHua";                          //5.佣兵进化

//    public static string ShuXingItem = "ShuXingItem";                                //6.属性项
//    public static string MecenaryInfo = "MecenaryInfo";                              //7.佣兵信息
//    public static string GetMecenaryUpdateObjPanel = "GetMecenaryUpdateObjPanel";    //8.获得佣兵更新对象面板
//    public static string MecenaryPanel_2 = "MecenaryPanel_2";                        //9.佣兵面板2
//    public static string UpJieSuccesssPanel = "UpJieSuccesssPanel";                  //10.升阶成功面板
//    #endregion

//    #region 宝石   （Gemstone）
//    public static string GemstonePanel = "GemstonePanel";
//    public static string GemstoneHeroItem = "GemstoneHeroItem";
//    #endregion

//    #region 公会   （Guild）
//    public static string SociatyPanel = "Sociaty";
//    public static string GetSociatyPanel = "GetSociatyPanel";
//    public static string SociatyInfoMem = "USociatyListInfoMem";
//    public static string SetSociatyIconPanel = "SetSociatyIconPanel";
//    public static string SociatyIcon = "SociatyIcon";
//    public static string SociatyMember = "SociatyMember";
//    public static string MangerSociatyPanel = "ManagerSociatyPanel";
//    public static string SociatyContributionPanel = "ContrilbutePanel";
//    public static string SociatyCarbonPanel = "SociatyPassInfoPanel";
//    public static string SociatyWorShipPanel = "WorShipPanel";
//    public static string SociatyWareHousePanel = "DepotPanel";
//    public static string SociatyChangeNamePanel = "SociatyRenamePanel";
//    public static string SociatySetNoticePanel = "SociatyNoticePanel";
//    public static string SociatyMerchantItem = "SociatyMerchantItemView";
//    public static string JoinInSetPanel = "JoinInSetPanel";
//    public static string ActivityPanel = "ActivityPanel";
//    public static string ActivityMember = "ActicityMember";
//    public static string MangerMemPanel = "ManageMemberPanel";
//    public static string ApplyDealPanel = "ApplyDealPanel";
//    public static string ApplyMembers = "ApplyMembers";
//    public static string SociatyPassInPanel = "SociatyPassInPanel";
//    public static string Texture = "Texture";
//    public static string DepotItem = "DepotItem";
//    public static string RecordPanel = "SociatyRecordPanel";
//    public static string Record = "Record";
//    public static string DistrubutePanel = "DealWithRewardsPanel";
//    public static string DistributeMem = "ContributionMembers";
//    public static string DistributeRecordPanel = "DistributeRecordPanel";
//    public static string DistributeRecord = "DistributeRecord";
//    public static string PassInfoShowPanel = "PassInfoShowPanel";
//    public static string Passitem = "PassItem";
//    public static string SociatyFriendsPanel = "SociatyFrendPanel";
//    public static string SociatyCheapterInfo = "SociayPassInfo";

//    //  公会商店
//    public static string SociatyMerchantViewPanel = "SociatyMerchantViewPanel";
//    public static string SociatyMerchantBuyViewPanel = "SociatyMerchantBuyViewPanel";
//    public static string WorShipInfoPanel = "WorShipInfoPanel";
//    public static string WorShipPanel = "WorShipPanel";
//    // public static string ContrilbutePanel = "UIs/Sociaty/ContrilbutePanel";
//    #endregion

//    //-----------------------------------------------------------------------------------------------------------------=（7）
//    #region 竞技场   （JJC）
//    public static string JJCHeroIconItem = "JJCHeroIconItem";                                       // 01.竞技场英雄图标项
//    public static string JJCPanel = "JJCPanel";                                                     // 02.竞技场面板
//    public static string JJCAwardItem = "JJCAwardItem";                                             // 03.竞技场奖励项
//    public static string BattleResultPanel = "BattleResultPanel";                                   // 04.战斗结果面板
//    public static string DeffanceLineUpPanel = "DeffanceLineUpPanel";                               // 05.防御阵容面板

//    public static string FightItem = "FightItem";                                                   // 06.攻击项
//    public static string FightRecordPanel = "FightRecordPanel";                                     // 07.攻击记录面板
//    public static string FightResultItem = "FIghtResultItem";                                       // 08.攻击结果项
//    public static string JJCBuyConfirmPanel = "UJJCBuyConfirmPanel";                                 // 09.竞技场购买确认面板
//    public static string JJCStorePanel = "JJCStorePanel";                                           // 10.竞技场商店面板

//    public static string RulesPanel = "RulesPanel";                                                 // 11.规则面板
//    public static string RankingPanel = "RankingPanel";                                             // 12.排行榜
//    public static string EnemyLineUpPanel = "EnemyLineUpPanel";                                     // 13.敌方阵容面板
//    #endregion

//    #region 巨兽囚笼 （MonsterWar）
//    public static string MonsterWarpanel = "MonsterWarPanel";                                // 1.巨兽囚笼面板
//    public static string MonsterWarInPanel = "MonsterWarInpanel";                            // 2.巨兽进入面板
//    public static string MonsterWarPassExpainPanel = "MonsterWarExpainPanel";                // 3.巨兽点击说明面板
//    public static string MonsterWarPassShowPanel = "MonsterWarPassShowPanel";                // 4.巨兽点击显示面板

//    public static string MonsterBear = "boss_Brownbear_skin";                                // 5.Boss贴图
//    #endregion

//    #region 巨龙试炼 （DragonTrail）
//    public static string DragonPanel = "DragonPanel";                                         // 01.巨龙面板
//    public static string DragonWarInPanel = "DragonWarInpanel";                               // 02.巨龙进入面板
//    public static string DragonWarExplainPanel = "DragonWarExplainPanel";                     // 03.巨龙说明面板
//    public static string DragonWarPassShowPanel = "DragonWarPassShowPanel";                   // 04.巨龙点击显示面板
//    public static string DragonHerosPanel = "DragonHerosPanel";                               // 05.巨龙英雄面板

//    public static string IceDragon = "IceDragon";                                             // 06.冰龙
//    public static string FireDragon = "FireDragon";                                           // 07.火龙
//    public static string ThunderDragon = "ThunderDragon";                                     // 08.雷龙
//    public static string TalentHero = "TalentHero";                                           // 09.英雄天赋
//    public static string DragonInfoPanel = "DragonInfoPanel";                                 // 10.巨龙信息面板
//    public static string DragonTipsPanel = "DragonTipsPanel";                                 // 11.巨龙提示面板
//    #endregion

//    #region 天堂之路 （ParadiseRoad）
//    public static string Pass = "Pass";                                                         // 1.点击
//    public static string FreeWayPanel = "FreeWayPanel";                                         // 2.天堂之路面板
//    public static string Cloud = "Cloud";                                                       // 3.云
//    public static string HerosPanel = "HerosPanel";                                             // 4.英雄面板
//    public static string Heros = "Hero";                                                        // 5.英雄
//    public static string InfoPanel = "InfoPanel";                                               // 6.信息面板
//    public static string FreeWayShopPanel = "ShopPanel";                                        // 7.天堂之路商店面板
//    public static string BuyItemPanel = "BuyItemPanel";                                         // 8.购买物品面板
//    #endregion

//    #region 秘境之塔 （SecretTower）
//    public static string RulePanel = "RulePanel";                                           // 1.规则面板
//    public static string SecretTowerBuyViewPanel = "SecretTowerBuyViewPanel";               // 2.秘境塔购买面板
//    public static string SecretTowerLevelItem = "SecretTowerLevelItem";                     // 3.等级项
//    public static string SecretTowerPanel = "SecretTowerPanel";                             // 4.秘境塔面板
//    public static string SecretTowerPropItem = "SecretTowerPropItem";                       // 5.秘境塔道具项
//    public static string SecretTowerShopItemView = "SecretTowerShopItemView";               // 6.秘境塔商店项
//    public static string SecretTowerShopViewPanel = "SecretTowerShopViewPanel";             // 7.秘境塔商店展示面板
//    public static string SweepOverPanel = "SweepOverPanel";                                 // 8.扫荡完成面板
//    #endregion

//    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////=（5）
}