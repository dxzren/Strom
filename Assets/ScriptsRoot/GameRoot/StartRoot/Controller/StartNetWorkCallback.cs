using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;
using LitJson;
/// <summary>   开始游戏(Start)回调   </summary>

public class StartNetWorkCallback : IStartNetWorkCallback
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher             dispatcher                  { set; get; }
    [Inject]
    public ISocket                      socket                      { set; get; }
    [Inject]
    public IStartData                   startData                   { set; get; }
    [Inject]
    public IPlayer                      player                      { set; get; }
    [Inject]
    public IGameData                    gameData                    { set; get; }
    [Inject]
    public IHeroSysData                 heroSys                     { set; get; }

    [Inject]
    public IRechargeData                rechargeData                { set; get; }

    private int                         receive_count               = 0;                                            /// 接收累计
    private int                         bagItem_Count               = 0;                                            /// 背包物品累计
    private int                         eMailCount                  = 0;                                            /// 邮件累计

    public static bool                  isFirst                     = true;                                         /// 标记是否加载主界面逻辑(游戏中断重连,不走加载主界面逻辑)
    public static bool                  isStartBattle               = false;                                        /// 是否进入第一次战斗

    public void                         OnServerListResponse        (EventBase obj)                                 // (20/ 1):登录中心服列表数据回调    
    {
        Debuger.Log                     ("***** ResponseHnadler ***** < 20/1 > OnServerListResponse--获取服务器列表回调");
        RET_LOGIN_SRV_List              data                        = (RET_LOGIN_SRV_List)obj.eventValue;           /// 登录中心服数据
        List<GameSrvInfo>               GSrvInfoList;                                                               /// 游戏服列表

        if( startData.centerServerList.ContainsKey                  ( data.nCentralServerID ))                         
        {
            GSrvInfoList                                            = startData.centerServerList[data.nCentralServerID];
            for(int i = 0; i < data.nNum;i++)
            {
                GSrvInfoList.Add        ( data.gameSrvInfoList[i] );
            }           
        }
        else
        {
            GSrvInfoList                                            = new List<GameSrvInfo>();                      /// 处理服务器消息
            for(int i = 0;i < data.nNum;i++)
            {
                GSrvInfoList.Add        ( data.gameSrvInfoList[i] );
            }
            startData.centerServerList.Add(data.nCentralServerID, GSrvInfoList);
        }

        /// 首次登陆,本地不存在已有账户,如果本地已存在,就使用本地帐号.(不能保证本地账户有效性,可能本地帐号已经失效)
        if(!startData.isLocalAcc)
        {
            GameSrvInfo                 GSInfo;
            List<int>                   CSrvListkeyList             = new List<int>(startData.centerServerList.Keys);
            if (Util.GetIPFile          ("hasSelected")             == null)                      // 读取最后<登录IP信息>的本地保存文件                                 
            {
                List<GameSrvInfo>       gameSrvList                 = startData.centerServerList[CSrvListkeyList[CSrvListkeyList.Count - 1]];
                GSInfo                                              = gameSrvList[gameSrvList.Count - 1];
                ServerInfo              serverName                  = new ServerInfo();
                serverName.             serverName                  = GSInfo.szGameSrvName;
                serverName.             gameServerID                = GSInfo.nGameServerLineID;
                serverName.             centerServerID              = CSrvListkeyList[CSrvListkeyList.Count - 1];
                serverName.             IP                          = GSInfo.szGameSrvIP;
                serverName.             port                        = GSInfo.nProt;
                Util.SaveFile           (serverName, "hasSelected");
            }
            else
            {
                List<GameSrvInfo>       gameSrvList                 = startData.centerServerList[CSrvListkeyList[CSrvListkeyList.Count - 1]];
                GSInfo                                              = gameSrvList[gameSrvList.Count - 1];
                ServerInfo              hasServerName               = Util.GetIPFile("hasSelected");
                hasServerName.          serverName                  = GSInfo.szGameSrvName;
                hasServerName.          gameServerID                = GSInfo.nGameServerLineID;
                hasServerName.          centerServerID              = CSrvListkeyList[CSrvListkeyList.Count - 1];
                hasServerName.          IP                          = GSInfo.szGameSrvIP;
                hasServerName.          port                        = GSInfo.nProt;
                Util.SaveFile           (hasServerName, "hasSelected");
            }
        }
        PanelManager.sInstance.         HideLoadingPanel();
        Debuger.Log                     ( "StartEvent.RefreshServerName_Event");
        dispatcher.Dispatch             ( StartEvent.RefreshServerName_Event);
    }
    public void                         OnPublicResponse            (EventBase obj)                                 // (20/ 5):公告回调                 
    {
        Debuger.Log                     ("**** ResponseHnadler **** < 20/ 5 > OnPublicResponse--获取公告回调");
        RET_LOGIN_PUBLIC_INFO           data                        = ( RET_LOGIN_PUBLIC_INFO)obj.eventValue;

        player.PublicInfo               = data.publicText;
        dispatcher.Dispatch             (StartEvent.RefreshPublic_Event);
    }
    public void                         OnLogInCheckCodeResponse    (EventBase obj)                                 // (20/ 3):登录验证码回调            
    {
        Debuger.Log                     ("**** ResponseHnadler **** < 20/ 3 > OnLogInCheckCodeResponse-- 获取校验码回调");
        RET_LOGIN_LogIn_LS              data                        = ( RET_LOGIN_LogIn_LS )obj.eventValue;

        PanelManager.sInstance.         HideLoadingPanel();

        if                              ( data.nErrorID == 0)
        {
            startData.                  checkCode               = data.szCheckCode;
            startData.                  tempIP                  = data.szIP;
            startData.                  tempPort                = data.nport;
            SavedAccountInfos           save                    = new SavedAccountInfos();                          /// 存储账户信息到本地

            save.                       Account                 = startData.account;
            save.                       Password                = startData.password;
            Util.SaveFile               ( save, "SavedAccountInfos" );
            dispatcher.Dispatch         ( StartEvent.GameEnter_Event );
        }
        else if                         ( data.nErrorID == 7)
        {
            PanelManager.sInstance.     ShowNoticePanel         ("服务器无回调!");
            socket.SocketThreadQuit();
            SceneManager.               LoadScene               ("LogIn");
        }
        else
        {
            switch((LOGIN_ERR_RET)data.nErrorID)
            {
                case LOGIN_ERR_RET.     LOGIN_ERR_NO_ERR:
                    PanelManager.sInstance.ShowNoticePanel      ("");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_VERSION:
                    PanelManager.sInstance.ShowNoticePanel      ("版本不符 !");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_ACCOUNT:
                    PanelManager.sInstance.ShowNoticePanel      ("帐号不存在 !");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_PASSWORD:
                    if(Define.SDKVersion == GameVersion.Lan || Define.SDKVersion == GameVersion.Censor)
                    {
                        PanelManager.sInstance.ShowNoticePanel  ("密码不正确 !");
                    }
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_FORBIDACC:
                    PanelManager.sInstance.ShowNoticePanel      ("帐号被禁止登录 !");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_FORBIDIP:
                    PanelManager.sInstance.ShowNoticePanel      ("IP禁止登录 !");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_SESSION:
                    PanelManager.sInstance.ShowNoticePanel      ("会话错误 !");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_SVR_OFFLINE:
                    PanelManager.sInstance.ShowNoticePanel      ("错误的服务器线路或者线路已关闭");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_SRV_FULL:
                    PanelManager.sInstance.ShowNoticePanel      ("服务器已满 !");
                    break;
                case LOGIN_ERR_RET.     LOGIN_ERR_DATA_INVALID:
                    PanelManager.sInstance.ShowNoticePanel      ("平台验证失败 !");
                    break;
                default:
                    break;

            }
            Debuger.Log                 ("错误码: " + data.nErrorID);
            socket.SocketThreadQuit();
        }
    }

    public void                         OnGetRoleResponse           (EventBase obj)                                 // (30/ 2):获取帐号角色信息回调       

    {
        Debuger.Log                     ("**** ResponseHnadler **** < 30/ 2 > OnGetRoleResponse -- 获取角色信息回调");
        PanelManager.sInstance.HideLoadingPanel();
        SYNC_LOGIN_AllPlayerInfo        data                        = (SYNC_LOGIN_AllPlayerInfo)obj.eventValue;
        if(data.nNum != 0)
        {
            Debuger.Log                                             ("以已存在的角色进入游戏主界面");
            startData.playerSelected                                = data.PlayerData[0];
            player.PlayerName                                       = startData.playerSelected.szPlayerName;
            dispatcher.Dispatch                                     (StartEvent.RoleEnter_Event);
        }
        else
        {
            dispatcher.Dispatch                                     (StartEvent.Hide_Event);
            GameObject                  Gobj                        = GameObject.Find("LoadingPanel");
            if(Gobj != null)
            {
                Gobj.transform.localPosition                        = new Vector3(Gobj.transform.localPosition.x, 
                                                                                  Gobj.transform.localPosition.x+1000f,
                                                                                  Gobj.transform.localPosition.z);
            }
            PanelManager.sInstance.HidePanel(SceneType.Start, UIPanelConfig.GameEnterPanel);
            PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.RoleSelectPanel);
            Debuger.Log                                             ("新创建的帐号,进入角色选择界面");
        }
    }
    public void                         OnServerTimeResponse        (EventBase obj)                                 // (30/ 3):同步服务器时间差         
    {
        Debuger.Log                         ("***** ResponseHnadler ***** < 30/ 3 > OnServerTimeResponse-- 同步服务器时间");
        SYNC_LOGIN_LS_GS_Time           syncTime                    = (SYNC_LOGIN_LS_GS_Time)obj.eventValue;

        gameData.subServerT             = syncTime.curentTime - Util.DateTimeToStamp(System.DateTime.Now);          /// 服务器同步时间差 = STime-Ctime.
        heroSys.skillPointTime          = Util.DateTimeToStamp(Util.StampToDateTime(Util.GetNowTime(gameData)));    /// 技能点时间戳
    }
    public void                         OnLogInGameServerResponse   (EventBase obj)                                 // (31/50):登录游戏服回调           
    {
        Debuger.Log("***** ResponseHnadler ***** < 30/3 > 登录游戏服务器回调");

        PanelManager.sInstance.HideLoadingPanel();

        RET_ENTER_GAME data = (RET_ENTER_GAME)obj.eventValue;

        if(data.nErrorID == 0)
        {
            dispatcher.Dispatch(StartEvent.ClientReady_Event);
        }
    }
    public void                         OnPlayerInfoResponse        (EventBase obj)                                 // (31/51):同步玩家信息              
    {
        Debuger.Log                                         ("**** ResponseHnadler **** < 31/51 > OnPlayerInfoResponse-- 同步玩家信息回调");
        DataForMainUI                   data                = (DataForMainUI)obj.eventValue;
        player.PlayerID                                     = data.nplayerID;                                       // 玩家ID
        player.PlayerHeadIconName                           = data.nPlayerIcon.ToString();                          // 玩家头像图标
        player.PlayerLevel                                  = data.nPlayerLv;                                       // 玩家等级
        player.PlayerCurrentExp                             = data.nPlayerExp;                                      // 玩家经验
        player.PlayerCurrentStamina                         = data.nStamina;                                        // 玩家体力

        player.SkillPoint                                   = data.nSkillPoint;                                     // 玩家技能点
        player.PlayerVIPLevel                               = data.nVIPLv;                                          // VIP等级
        player.RegTime                                      = data.nRegTime;                                        // 注册时间戳
        player.GuideProces                                  = data.NewGuideIndex;                                   // 新手引导标记(步数)
        player.BuyedCoinsTimes                              = data.BuyedCoinsTimes;                                 // 已购买金币次数

        player.BuyedStaminaTimes                            = data.BuyedStaminaTimes;                               // 已购买体力次数
        player.BuyedSkillTimes                              = data.BuyedSkillTimes;                                 // 已购买技能点次数
        player.FriendStaminaCount                           = data.FriendStaminaCount;                              // 已领取好友体力数
        player.LogInGameDays                                = data.nLogInGameDays;                                  // 登录游戏天数
        player.LvRewardGot                                  = data.nLvRewardGot;                                    // 已领取等级奖励


//      GuideExc(data);                                                     // 处理新手引导
                                                                        
        if(isFirst && !isStartBattle)                                       // 断线重连，手动登录时，走正常的流程，加载主界面，如果是断线重连，就跳过加载主界面逻辑。
        {
            Debug.Log                                       ("玩家 ID请求 -- 加载 Main");
            dispatcher.Dispatch                             (RechargeEvent.GetPlayerID_Event);                      /// 玩家ID请求
            SceneController.LoadScene                       ("Main");                                               /// 加载主界面场景
            isFirst                                         = false;                                                /// 标记是否加载主界面逻辑(游戏中断重连, 不走加载主界面逻辑)
        }
        Debuger.Log                                         ("-- 同步玩家信息回调 完成");
        //LogInCheck();                                                       /// 登录验证(易接平台) 
    }
    public void                         OnCreateRoleResponse        (EventBase obj)                                 // (31/55):创建角色回调              
    {
        Debuger.Log                     ("**** ResponseHnadler **** < 31/55 > OnCreateRoleResponse-- 创建角色回调");
        PanelManager.sInstance.HideLoadingPanel();
        RET_CREATEROLE                  data                         = (RET_CREATEROLE)obj.eventValue;

        if(data.nErrorID == 0)
        {
            Debuger.Log             ("创建角色成功");
            isStartBattle           = false;                                                    /// 开启初始战斗
            dispatcher.Dispatch     (StartEvent.RoleEnter_Event);                               /// 游戏角色进入游戏
            //YiJieManager.Instance.SetData("createrole", player, game);
        }
        else
        {
            startData.createTimes   = 0;
            Debuger.Log             ("错误码:" + data.nErrorID);
            if      (data.nErrorID == 1)
            {
                PanelManager.sInstance.ShowNoticePanel("角色名长度不对");
            }
            else if (data.nErrorID == 2)
            {
                PanelManager.sInstance.ShowNoticePanel("角色名含敏感词");
            }
            else if (data.nErrorID == 3)
            {
                PanelManager.sInstance.ShowNoticePanel("角色名已存在");
            }
            else if (data.nErrorID == 4)
            {
                PanelManager.sInstance.ShowNoticePanel("帐号下已有一个角色");
            }
        }
    }
    public void                         OnCheckCreateRoleResponse   (EventBase obj)                                 // (31/58):检验角色创建回调          
    {
        Debuger.Log                     ("**** ResponseHnadler **** < 31/58 > OnCheckCreateRoleResponse-- 预先检验创建角色 回调");
        PanelManager.sInstance.HideLoadingPanel();
        RET_CREATEROLE data = (RET_CREATEROLE)obj.eventValue;

        if(data.nErrorID == 1)
        {
            Debuger.Log("通过预检验");
            dispatcher.Dispatch(StartEvent.CreateRole_Event);                                   /// 通过验证_创建角色
        }
        else
        {
            startData.createTimes = 0;
            Debuger.Log("错误码:" + data.nErrorID);
            if(data.nErrorID == 0)
            {
                PanelManager.sInstance.ShowNoticePanel("角色名已存在");                          /// 创建角色失败
            }
        }
    }
    public void                         OnHeroInfoResponse          (EventBase obj)                                 // (32/50):英雄信息回调              

    {
        Debuger.Log                     ("***** ResponseHnadler ***** < 32/50 > OnHeroInfoResponse-- 同步英雄列表数据回调 回调");
        Debuger.Log                     ("同步英雄列表数据回调");
        RET_HERO_Info                   heroInfoData                = (RET_HERO_Info)obj.eventValue;

        int                             heroCount                   = heroInfoData.nHerosNum;
        bool                            isPossessed                 = false;
        if(heroInfoData.npackgeNum != 0)
        {
            for(int i = 0;i < heroInfoData.npackgeNum;i++)
            {
                HERO_DATA       hero                        = heroInfoData.heroDataList[i];
                IHeroData       heroData                    = new HeroData();
                heroData.ID                                 = hero.nHeroID;
                heroData.HeroExp                            = hero.nHeroExp;
                heroData.HeroLevel                          = hero.nHeroLv;
                heroData.HeroStar                           = hero.nHeroStarLv;
                heroData.Quality                            = (HeroQuality)hero.nHeroQuality;

                heroData.MedalObject.medalExp               = hero.nMedalExp;
                heroData.MedalObject.medalLv                = hero.nMedalLv;
                heroData.WingID                             = hero.nWingID;

                int count = 0;
                foreach(int ID in hero.nHeroEquips)                                             // 装备列表             
                {
                    if(ID != 0)
                    {
                        heroData.EquipList.Add((WearPosition)count,ID);
                    }
                    count += 1;
                }
                int skillCount = 1;
                foreach(int lv in hero.nHeroSkillLv)                                            // 技能列表             
                {
                    switch(skillCount)
                    {
                        case 1:
                            heroData.UltSkillLevel = lv;
                            break;
                        case 2:
                            heroData.ActiveSkillLevel1 = lv;
                            break;
                        case 3:
                            heroData.ActiveSkillLevel2 = lv;
                            break;
                        case 4:
                            heroData.PassiveSkillLevel = lv;
                            break;
                    }
                    skillCount++;
                }

                foreach(var Item in player.HeroList)
                {
                    if (Item.ID == hero.nHeroID)            isPossessed = true;
                }
                if (isPossessed == true)                    continue;
                if( i == heroInfoData.npackgeNum - 1)
                {
                    player.AddHero(heroData);
                }
                else
                {
                    player.AddHero(heroData, false);
                }
            }
            receive_count += heroInfoData.npackgeNum;
        }
        Debuger.Log                     ("同步英雄列表数据完成");
    }

    public void                         OnBagInfoResponse           (EventBase obj)                                 // (40/51):同步背包数据回调          
    {
        Debuger.Log                     ("**** ResponseHnadler **** < 40/51 > OnBagInfoResponse-- 同步背包信息回调(背包数据)");
        RET_BAG_LIST                    baglist                     = (RET_BAG_LIST)obj.eventValue;

        if (baglist.PackTotalCount > 0)
        {
            foreach (BagData item in baglist.bagList)  
            {
                switch ((ItemType)item.Type)
                {
                    case ItemType.equip:                                    // 装备
                    case ItemType.scroll:                                   // 卷轴               
                        {
                            Equip equip = new Equip();
                            equip.ID = item.nItemID;
                            equip.count = item.nNum;
                            equip.time = Util.StampToDateTime(item.nGetTime);
                            if (!player.EquipList.Contains(equip) && item.nNum > 0)
                            {
                                player.EquipList.Add(equip);
                            }
                        }
                        break;
                    case ItemType.equipFragment:                            // 装备碎片
                    case ItemType.scrollFragment:                           // 卷轴碎片           
                        {
                            Fragment frag = new Fragment();
                            frag.ID = item.nItemID;
                            frag.count = item.nNum;
                            frag.time = Util.StampToDateTime(item.nGetTime);
                            if (!player.FragmentList.Contains(frag) && item.nNum > 0)
                            {
                                player.FragmentList.Add(frag);
                            }
                        }
                        break;
                    case ItemType.soul:                                     // 魂石               
                        {
                            Soul soul = new Soul();
                            soul.ID = item.nItemID;
                            soul.count = item.nNum;
                            soul.time = Util.StampToDateTime(item.nGetTime);
                            if (!player.GetHeroSoulList.Contains(soul) && item.nNum > 0)
                            {
                                player.GetHeroSoulList.Add(soul);
                            }
                        }
                        break;
                    case ItemType.ticket:                                   // 扫荡券
                    case ItemType.jinjiestone:                              // 进阶石
                    case ItemType.protectedstone:                           // 保护石(翅膀)
                    case ItemType.coinsprop:                                // 金币道具
                    case ItemType.heroExpProp:                              // 英雄经验道具
                    case ItemType.medalExpProp:                             // 勋章经验道具
                    case ItemType.mercExpProp:                              // 佣兵经验道具
                    case ItemType.SkillProp:                                // 技能点道具
                    case ItemType.staminaProp:                              // 体力道具
                    case ItemType.wingExpProp:                              // 翅膀经验道具
                    case ItemType.soulbag:                                  // 魂石包道具
                    case ItemType.diamondsbag:                              // 钻石包道具           
                        {
                            Prop prop = new Prop();
                            prop.ID = item.nItemID;
                            prop.count = item.nNum;
                            prop.time = Util.StampToDateTime(item.nGetTime);
                            if (!player.PropList.Contains(prop) && item.nNum > 0)
                            {
                                player.PropList.Add(prop);
                            }
                        }
                        break;
                    case ItemType.wing:                                     // 翅膀                 
                        {
                            player.WingList.Add(item.nItemID, item.nNum);
                        }
                        break;
                }

                bagItem_Count += 1;
                if (baglist.PackTotalCount <= bagItem_Count)
                {
                    dispatcher.Dispatch(EventSignal.UpdateInfo_Event);
                    bagItem_Count = 0;
                    break;
                }
            }
        }
    }

    public void                         OnLoadResResponse           (EventBase obj)                                 // (3154201,3154201)下载资源回调 本地   
    {
        Debuger.Log                     ("**** ResponseHnadler **** < 3154201/3154201 > OnLoadResResponse-- 下载资源回调");
        float                           value                       = (float)obj.eventValue;
        dispatcher.Dispatch                                         (StartEvent.Process_Event, value);
    }
    public void                         OnLoadResSucceedResponse    (EventBase obj)                                 // (31542012/31542012)加载资源完毕回调  
    {
#if UPDATRES || !UNITY_EDITOR || HOTFIX_ENABLE
        // 下载资源结束后应用热修复
        Updater.Instance.CheckandUseHotFix();
#endif
        Debuger.Log                     ("**** ResponseHnadler **** < 31542012/31542012 > OnLoadResSucceedResponse-- 加载资源完成回调");
        Debug.Log                       ("09>    StartNetWorkCallback.OnLoadResSucceedResponse().REQLoadConfig_Event  加载配置文件");
        dispatcher.Dispatch             (StartEvent.REQLoadConfig_Event);                                    /// 加载配置文件(初始化)
    }
    public void                         OnMonthCardTimeResponse     (EventBase obj)                                 // 月卡时间戳回调                      
    {
        Debuger.Log                     ("**** ResponseHnadler **** < **/** > OnMonthCardTimeResponse-- 月卡时间戳回调.");
        RET_RECHARGE_MONTHCARD_TIME     MothCardTime                = (RET_RECHARGE_MONTHCARD_TIME)obj.eventValue;
        player.MonthCardTime                                        = MothCardTime.nMonthCreateTime;

        if(player.MonthCardTime == 0)
        { return; }

        long                            currentTime                 = Util.GetNowTime(gameData);
        long                            expendTime                  = (currentTime - player.MonthCardTime);

        if(expendTime < (30*24*60*60))
        {
            player.MonthCardRemainingDays                           = 30 - (int)((float)expendTime / (24f * 60f * 60f));
        }
    }

    public void                         OnRechargeTimesResponse     (EventBase obj)                                 // 充值卡充值次数回调                 
    {
        RET_RECHARGE_TIMES              RechargeTimes               = (RET_RECHARGE_TIMES)obj.eventValue;
        Debug.Log                       ("**** ResponseHnadler **** < **/** > OnRechargeTimesResponse--充值次数回调:" + RechargeTimes.nPackNum+"/"+ RechargeTimes.nTotal);

        if(RechargeTimes.nTotal == 0)
        {
            Debug.Log                   ("首充");
            player.IsRecharged          = false;
        }                                                                                       // 是否首充
        else
        {
            Debug.Log                   ("非首充");
            player.IsRecharged          = true;
        }
                                                                                                // 检索数据包所有充值ID.1.充值数据列表没有的添加 2.添加充值次数
        for(int i = 0; i < RechargeTimes.RechargeDataList.Length;i++)
        {
            Debug.Log("xxxxxx=" + RechargeTimes.RechargeDataList[i].nCardID + "/" + RechargeTimes.RechargeDataList[i].nTimes);
            if(RechargeTimes.RechargeDataList[i].nCardID > 0)
            {
                if(!rechargeData.RechargeCardTimes.ContainsKey(RechargeTimes.RechargeDataList[i].nCardID))
                {
                    rechargeData.RechargeCardTimes.Add(RechargeTimes.RechargeDataList[i].nCardID, 0);
                }
                rechargeData.RechargeCardTimes[RechargeTimes.RechargeDataList[i].nCardID] = RechargeTimes.RechargeDataList[i].nTimes;
            }
        }


    }
    public void                         OnPlayerIDRechargeResponse  (EventBase obj)                                 // 请求玩家ID回调 玩家是否首充(易接)   
    {
        RET_RECHARGE_PLAYERID           RechargePlayerID            = (RET_RECHARGE_PLAYERID)obj.eventValue;
        Debug.Log                       ("**** ResponseHnadler **** < **/** > OnPlayerIDRechargeResponse--请求玩家ID,返回是否首充Recharged:"
                                        + RechargePlayerID.havearged + "/" + player.IsRecharged);

        LogInCheck();                                                       // 登录验证(平台)
    }
    public void                         OnHornMsgResponse           (EventBase obj)                                 // 喇叭消息回调                      
    {
        MAIN_PMD_SINGLE                 PMDDataSingle               = (MAIN_PMD_SINGLE)obj.eventValue;
        if(PMDDataSingle.PMDData.nTimes != 0)
        {
            return;
        }
        player.HornMsgs.Add             (new PMDMessage(PMDDataSingle));                                            /// 跑马灯内容
    }
    public void                         OnAllCurrencyResponse       (EventBase obj)                                 // (52/50):同步玩家所有货币           
    {
        Debug.Log                       ("**** ResponseHnadler **** < 52/50 > OnAllCurrencyResponse--同步玩家所有货币回调:");
        SYNC_CURRENCY_ALLDATA           CurrencyAll                 = (SYNC_CURRENCY_ALLDATA)obj.eventValue;
        player.PlayerCoins                                          = CurrencyAll.nCoins;
        player.PlayerDiamonds                                       = CurrencyAll.nDiamond;
    }
    public void                         OnCurrencyResponse          (EventBase obj)                                 // (52/51):同步单项货币              
    {
        Debug.Log                       ("**** ResponseHnadler **** < 52/51 > OnCurrencyResponse_once--同步玩家单项货币回调:");
        SYNC_CURRENCY_DATA              CurrencyData                = (SYNC_CURRENCY_DATA)obj.eventValue;
        switch (CurrencyData.nCurrencyType)
        {
            case 1:player.PlayerDiamonds                            = CurrencyData.nValue;
                dispatcher.Dispatch(EventSignal.UpdateInfo_Event);                                                  /// 事件信号,更新信息
                break;
            case 2:player.PlayerCoins                               = CurrencyData.nValue;
                dispatcher.Dispatch(GlobalEvent.CurrencyEvent);                                                     /// 全局事件,货币变化
                break;
            default:
                break;
        }
    }

    public void                         OnRechargeMoneyResponse     (EventBase obj)                                 // 同步充值金额              
    {
        SYNC_CURRENCY_MONEY_DATA        MoneyData                   = (SYNC_CURRENCY_MONEY_DATA)obj.eventValue;
        player.DiamondOfRecharge                                    = MoneyData.nMoney;
        Debuger.Log                                                 ("充值金额同步:" + player.DiamondOfRecharge);
        Debuger.LogWarning                                          ("充值金额同步-----:" + player.DiamondOfRecharge);
    }
    public void                         OnURLResponse               (EventBase obj)                                 // 充值地址回调              
    {
        RET_RECHARGE_SERVER_URL         URL                         = (RET_RECHARGE_SERVER_URL)obj.eventValue;
        rechargeData.rechargeURL                                    = URL.sURL;
        Debug.LogError                                              ("URL backed URL = " + URL.sURL);
        Debuger.Log                                                 ("URL Back Url = " + URL.sURL);
        if(rechargeData.rechargeURL != "")
        {
            if (rechargeData.Recharge != null)                      rechargeData.Recharge();
            rechargeData.Recharge                                   = null;
        }
    }
    public void                         LogInCheck()                                                                // 登录验证(易接平台)        
    {

    }

    private DataForMainUI               GuideExc( DataForMainUI data )                                              // 新手引导处理              
    {
        if(data.NewGuideIndex == 0)
        {
            player.GuideProces = 0;
            return new DataForMainUI();
        }

        char[] a = new char[6];
        char[] n = new char[4];
        char[] m = new char[10];

        for(int i = 0;i < 10;i++)
        {
            if(i < 10 - data.NewGuideIndex.ToString().ToCharArray().Length)
            {
                m[i] = '0';
            }
            else
            {
                m[i] = data.NewGuideIndex.ToString().ToCharArray()[10 - data.NewGuideIndex.ToString().ToCharArray().Length];
            }
        }
        for(int i = 0; i < 6;i++)
        {
            a[i] = m[i];
        }
        for(int i = 0; i < 4;i++)
        {
            n[i] = m[6 + i];
        }
        string weakGuides = System.Convert.ToString(Util.ParseToInt(new string(a)), 2);
        player.GuideProces = Util.ParseToInt(new string(n));
        int index = 10 - weakGuides.ToCharArray().Length;

        for(int i = 0; i < 10; i++)
        {
            if(i < index)
            {
                player.WeakGuideStates.SetValue(0, i);
            }
            else
            {
                player.WeakGuideStates.SetValue(Util.ParseToInt(weakGuides.ToCharArray()[i - index].ToString()),i);
            }
        }

        player.IsShowed0 = player.WeakGuideStates[0] == 1;
        player.IsShowed1 = player.WeakGuideStates[1] == 1;
        player.IsShowed2 = player.WeakGuideStates[2] == 1;
        player.IsShowed3 = player.WeakGuideStates[3] == 1;
        player.IsShowed4 = player.WeakGuideStates[4] == 1;
        player.IsShowed5 = player.WeakGuideStates[5] == 1;
        player.IsShowed6 = player.WeakGuideStates[6] == 1;
        player.IsShowed7 = player.WeakGuideStates[7] == 1;
        player.IsShowed8 = player.WeakGuideStates[8] == 1;
        return data;
    }
}