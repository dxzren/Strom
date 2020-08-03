using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

/// <summary> 登录回调消息处理 分发 </summary>
public class LogInResponseHandler : IResponseHandler
{
    private static          LogInResponseHandler    _Instance = null;
    public static           LogInResponseHandler    Instance()           
    {
        if (_Instance == null)
        {
            _Instance = new LogInResponseHandler();
        }
        return _Instance;
    }


    ///<summary> 处理服务器返回消息 </summary>  
    public bool             ProtocolHandler( byte[] msg,short type1,short type2)                // msg: 消息 type1:通信大类 type2:通信的具体内容类别
    {
        if      ((eMsgType)type1 == eMsgType._MSG_LOGIN_CLIENT_LS)                              /// <20>.客户端与登陆服务器进行通讯    
        { 
            switch ((LOGIN_CLIENT_LS_CMD)type2)
            {
                case LOGIN_CLIENT_LS_CMD.           CLLS_RET_LOGIN:                                                 /// (3) 登录服登录请求回调         
                    {
                        Debug.Log                                           ("ProtocolHandler(20/ 3): LOGIN_CLIENT_LS_CMD.CLLS_RET_LOGIN");

                        RET_LOGIN_LogIn_LS          LogInLsMsg              = new RET_LOGIN_LogIn_LS();
                        LogInLsMsg                                          = ( RET_LOGIN_LogIn_LS )Util.BytesToStruct
                                                                                ( msg, msg.Length, LogInLsMsg.GetType());
                        Thread.Sleep                (2 * 1000);                                                                 /// 将线程挂起的指定时间
                        NetEventDispatcher.Instance().DispathcEvent         ( (int)eMsgType.              _MSG_LOGIN_CLIENT_LS, 
                                                                              (int)LOGIN_CLIENT_LS_CMD.   CLLS_RET_LOGIN, LogInLsMsg);
                        return true;
                    }
                case LOGIN_CLIENT_LS_CMD.           CLLS_RET_SRV_LIST:                                              /// (1) 服务器列表请求回调         
                    {
                        Debug.Log                                           ("ProtocolHandler(20/ 1): LOGIN_CLIENT_LS_CMD.CLLS_RET_SRV_LIST");
                     
                        RET_LOGIN_SRV_List          SRVListMsg              = (RET_LOGIN_SRV_List)LogIn_CustomDec_Enc.Dec_SRVList(msg);
                        NetEventDispatcher.Instance().DispathcEvent         (   (int)eMsgType.              _MSG_LOGIN_CLIENT_LS, 
                                                                                (int)LOGIN_CLIENT_LS_CMD.   CLLS_RET_SRV_LIST, SRVListMsg);
                        return true;
                    }
                case LOGIN_CLIENT_LS_CMD.           CLLS_RET_PUBLIC:                                                /// (5) 公告消息请求回调           
                    {
                        Debug.Log                                           ("ProtocolHandler(20/ 5): LOGIN_CLIENT_LS_CMD.CLLS_RET_PUBLIC");

                        RET_LOGIN_PUBLIC_INFO       PublicInfoMsg           = new RET_LOGIN_PUBLIC_INFO();
                        PublicInfoMsg                                       = LogIn_CustomDec_Enc.Dec_PublicInfo(msg);
                        NetEventDispatcher.Instance().DispathcEvent         (   (int)eMsgType.              _MSG_LOGIN_CLIENT_LS, 
                                                                                (int)LOGIN_CLIENT_LS_CMD.   CLLS_RET_PUBLIC, PublicInfoMsg);
                        return true;
                    }
            }
        }
        else if ((eMsgType)type1 == eMsgType._MSG_LOGIN_CLIENT_GS)                              /// <30>.客户端和游戏服务器进行通讯    
        {
            switch ((LOGIN_CLIENT_GS_CMD)type2)                                                                         
            {
                case LOGIN_CLIENT_GS_CMD.           CLGS_SYNC_PLAYER_RoleList:                                      /// (2)同步角色列表信息            
                    {
                        Debug.Log                                           ("ProtocolHandler(30/ 2): LOGIN_CLIENT_GS_CMD.CLGS_SYNC_PLAYER_RoleList");

                        SYNC_LOGIN_AllPlayerInfo    AllPlayerInfoMsg        = new SYNC_LOGIN_AllPlayerInfo();
                        AllPlayerInfoMsg                                    = LogIn_CustomDec_Enc.Dec_AllPlayerInfo(msg);
                        NetEventDispatcher.Instance().DispathcEvent         (   (int)eMsgType.              _MSG_LOGIN_CLIENT_GS, 
                                                                                (int)LOGIN_CLIENT_GS_CMD.   CLGS_SYNC_PLAYER_RoleList, AllPlayerInfoMsg);
                        return true;
                    }
                case LOGIN_CLIENT_GS_CMD.           CLGS_SYNC_SRV_TIME:                                             /// (3)同步服务器时间              
                    {
                        Debug.Log                                           ("ProtocolHandler(30/ 3): LOGIN_CLIENT_GS_CMD.CLGS_SYNC_SRV_TIME");

                        SYNC_LOGIN_LS_GS_Time       SyncTimeMsg             = new SYNC_LOGIN_LS_GS_Time();
                        SyncTimeMsg                                         = ( SYNC_LOGIN_LS_GS_Time )Util.BytesToStruct
                                                                                ( msg, msg.Length, SyncTimeMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         (   (int)eMsgType.              _MSG_LOGIN_CLIENT_GS, 
                                                                                (int)LOGIN_CLIENT_GS_CMD.   CLGS_SYNC_SRV_TIME, SyncTimeMsg);
                        return true;
                    }
            }
        }
        else if ((eMsgType)type1 == eMsgType._MSG_PLAYER_MODULE)                                /// <31>.同步玩家模块                 
        {
            switch ((PLAYER_CMD)type2)
            {
                case PLAYER_CMD.                    PLAYER_RET_ENTER_GAME:                                          /// (50)进入游戏                
                    {
                        Debug.Log                                           ("ProtocolHandler(31/50): PLAYER_CMD.PLAYER_RET_ENTER_GAME-- 进入游戏");

                        RET_ENTER_GAME              EnterGameMsg            = new RET_ENTER_GAME();
                        EnterGameMsg                                        = (RET_ENTER_GAME)Util.BytesToStruct(msg, msg.Length, EnterGameMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         (   (int)eMsgType.     _MSG_PLAYER_MODULE, 
                                                                                (int)PLAYER_CMD.   PLAYER_RET_ENTER_GAME, EnterGameMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_PLAYER_INFO:                                         /// (51)同步玩家数据            
                    {
                        Debug.Log                                           ("ProtocolHandler(31/51):PLAYER_CMD.PLAYER_RET_ENTER_GAME-- 同步玩家数据 返回");

                        DataForMainUI               MainSysMsg              = new DataForMainUI();
                        MainSysMsg                                          = (DataForMainUI) Util.BytesToStruct ( msg,msg.Length, MainSysMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ( (int)eMsgType.       _MSG_PLAYER_MODULE, 
                                                                              (int)PLAYER_CMD.     PLAYER_RET_PLAYER_INFO, MainSysMsg );
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_CREATE_ROLE:                                         /// (55)创建角色                
                    {
                        Debug.Log                                           ("ProtocolHandler(31/55): PLAYER_CMD.PLAYER_RET_CREATE_ROLE");

                        RET_CREATEROLE              CreateRoleMsg           = new RET_CREATEROLE();
                        CreateRoleMsg                                       = (RET_CREATEROLE)Util.BytesToStruct(msg, msg.Length, CreateRoleMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ( (int)eMsgType.       _MSG_PLAYER_MODULE, 
                                                                              (int)PLAYER_CMD.     PLAYER_RET_CREATE_ROLE, CreateRoleMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_BUY:                                                 /// (56)玩家购买回调            
                    {
                        Debug.Log                                           ("ProtocolHandler(31/56): LAYER_CMD.PLAYER_RET_BUY");
                        RET_PLAYER_BUY_SOMETHING    PlayerBuyMsg            = new RET_PLAYER_BUY_SOMETHING();
                        PlayerBuyMsg                                        = (RET_PLAYER_BUY_SOMETHING)Util.BytesToStruct(msg, msg.Length, PlayerBuyMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_RET_BUY, PlayerBuyMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_CHANGEICON:                                          /// (57)玩家更换头像图标回调     
                    {
                        Debug.Log                                           ("ProtocolHandler(31/57): PLAYER_CMD.PLAYER_RET_CHANGEICON");
                        RET_MAIN_CHANGE_ICON        ChangeIconMsg           = new RET_MAIN_CHANGE_ICON();
                        ChangeIconMsg                                       = (RET_MAIN_CHANGE_ICON)Util.BytesToStruct(msg, msg.Length, ChangeIconMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_RET_CHANGEICON, ChangeIconMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_CHECK_CREATE_ROLE:                                   /// (58)创建角色校验            
                    {
                        Debug.Log                                           ("ProtocolHandler(31/58): PLAYER_CMD.PLAYER_RET_CHECK_CREATE_ROLE");
                        RET_CREATEROLE              CreateRoleMsg           = new RET_CREATEROLE();
                        CreateRoleMsg                                       = (RET_CREATEROLE)Util.BytesToStruct(msg, msg.Length, CreateRoleMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ( (int)eMsgType.                _MSG_PLAYER_MODULE,
                                                                              (int)PLAYER_CMD.              PLAYER_RET_CHECK_CREATE_ROLE, CreateRoleMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_RENAME:                                              /// (59)玩家更换名称回调         
                    {
                        Debug.Log                                           ("ProtocolHandler(31/59): PLAYER_CMD.PLAYER_RET_RENAME");
                        RET_PLAYER_RENAME           PlayerRenameMsg         = new RET_PLAYER_RENAME();
                        PlayerRenameMsg                                     = (RET_PLAYER_RENAME)Util.BytesToStruct(msg, msg.Length, PlayerRenameMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_RET_RENAME, PlayerRenameMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_RET_UPLV_AWARD:                                          /// (60)玩家领取升级奖励回调     
                    {
                        Debug.Log                                           ("ProtocolHandler(31/60): PLAYER_CMD.PLAYER_RET_UPLV_AWARD");
                        RET_PLAYER_UpLvAward        UpLvAwardMsg            = new RET_PLAYER_UpLvAward();
                        UpLvAwardMsg                                        = (RET_PLAYER_UpLvAward)Util.BytesToStruct(msg, msg.Length, UpLvAwardMsg.GetType());
                        NetEventDispatcher.Instance().DispathcEvent         ((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_RET_UPLV_AWARD, UpLvAwardMsg);
                        return true;
                    }
                case PLAYER_CMD.                    PLAYER_KEEP_LIVE:                                               /// (100)是否接收到心跳包        
                    {
                        Debug.Log                                           ("ProtocolHandler(31/100): PLAYER_CMD.PLAYER_KEEP_LIVE");
                        long                        time                    = Util.DateTimeToStamp(System.DateTime.Now) - Util.DateTimeToStamp(EditorClose.date);
                        EditorClose.isReceHeartBeat                         = true;
                        if (time > 1)
                        {
                            NetEventDispatcher.Instance().DispathcEvent(31542013, 31542013, null);
                        }
                        return true;
                    }
            }
        }
        else if ((eMsgType)type1 == eMsgType._MSG_HERO_MODULE)                                  /// <32>.同步英雄模块                 
        {
            switch ((HERO_CMD)type2)
            {
                case HERO_CMD.RET_SYNC_HERO_AllHeroInfo:                                                            /// (50)同步所有英雄信息       
                    {
                        Debug.Log                                           ("HERO_CMD.RET_SYNC_HERO_AllHeroInfo (32/50)-- 同步所有英雄信息");
                        RET_HERO_Info               HeroInfoMsg             = new RET_HERO_Info();
                        HeroInfoMsg                                         = LogIn_CustomDec_Enc.Dec_HeroInfo(msg);
                        Debug.Log                                           ("手动解析包 所有英雄数据信息!");
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_HERO_MODULE, (int)HERO_CMD.RET_SYNC_HERO_AllHeroInfo, HeroInfoMsg);
                        return true;
                    }
            }
        }
        else if ((eMsgType)type1 == eMsgType._MSG_BAG_MODULE)                                   /// <40>.同步背包模块                 
        {
            switch ((BAG_CMD)type2)
            {
                case BAG_CMD.RET_BAG_List:                                                                          /// (51)同步背包列表信息        
                    {
                        Debug.Log                                           ("ProtocolHandler: BAG_CMD.RET_BAG_List (40/51)-- 同步背包模块");
                        RET_BAG_LIST                BagListMsg              = new RET_BAG_LIST();
                        BagListMsg                                          = LogIn_CustomDec_Enc.Dec_BagList(msg);
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_BAG_MODULE, (int)BAG_CMD.REQ_BAG_List, BagListMsg);
                        return true;
                    }
            }
        }

        else if ((eMsgType)type1 == eMsgType._MSG_RECHARGE_MODULE)                              // <51>.充值模块 (暂未使用)
        { }
        else if ((eMsgType)type1 == eMsgType._MSG_MALL_MODULE)                                  // <34>.商城模块 (暂未使用)
        { } 
        else if ((eMsgType)type1 == eMsgType._MSG_MERCHANT_MODULE)                              // <41>.商人模块 (暂未使用)
        { }
        else if ((eMsgType)type1 == eMsgType._MSG_ACTIVITY_MODULE)                              // <39>.活动模块 (暂未使用)
        { }     
        else if ((eMsgType)type1 == eMsgType._MSG_TASK_MODULE)                                  // <35>.任务模块 (暂未使用)
        { }
        else if ((eMsgType)type1 == eMsgType._MSG_EMAIL_MODULE)                                 // <36>.邮箱模块 (暂未使用)
        { }
        else if ((eMsgType)type1 == eMsgType._MSG_FRIENDS_MODULE)                               // <37>.好友模块 (暂未使用)
        { }
        else if ((eMsgType)type1 == eMsgType._MSG_MERC_MODULE)                                  // <33>.佣兵模块 (暂未使用)
        { }
        return false;
    } 
}
/// <summary> 手动解压网络数据，支持ios </summary>
public class                    LogIn_CustomDec_Enc                                                           
{
    public static RET_LOGIN_SRV_List                    Dec_SRVList         (byte[] msg)            // 解析服务器列表消息                 
    {
        RET_LOGIN_SRV_List      TempSRVList             = new RET_LOGIN_SRV_List();
        int startIndex = 0;

        TempSRVList.Head                                = (Head)Util.BytesToBase(TempSRVList.Head, msg, ref startIndex);
        TempSRVList.nCentralServerID                    = (int)Util.BytesToBase (TempSRVList.nCentralServerID, msg, ref startIndex);
        TempSRVList.nTotal                              = (int)Util.BytesToBase (TempSRVList.nTotal, msg, ref startIndex);
        TempSRVList.nloop                               = (int)Util.BytesToBase (TempSRVList.nloop, msg, ref startIndex);
        TempSRVList.nNum                                = (int)Util.BytesToBase (TempSRVList.nNum, msg, ref startIndex);
        TempSRVList.gameSrvInfoList                     = new GameSrvInfo[TempSRVList.nNum];
        for( int i = 0;i < TempSRVList.nNum;i++)
        {
            GameSrvInfo         TempGameSrvInfo         = new GameSrvInfo();
            TempGameSrvInfo.szGameSrvIP                 = (string)  Util.BytesToBase(TempGameSrvInfo.szGameSrvIP = "", msg, ref startIndex);
            TempGameSrvInfo.nProt                       = (int)     Util.BytesToBase(TempGameSrvInfo.nProt, msg, ref startIndex);
            TempGameSrvInfo.nGameServerLineID           = (int)     Util.BytesToBase(TempGameSrvInfo.nGameServerLineID, msg, ref startIndex);
            TempGameSrvInfo.nPosition                   = (int)     Util.BytesToBase(TempGameSrvInfo.nPosition, msg, ref startIndex);
            TempGameSrvInfo.nState                      = (int)     Util.BytesToBase(TempGameSrvInfo.nState, msg, ref startIndex);
            TempGameSrvInfo.szGameSrvName               = (string)  Util.BytesToBase(TempGameSrvInfo.szGameSrvName = "", msg, ref startIndex);
            TempSRVList.gameSrvInfoList[i]              = TempGameSrvInfo;
        }
        //Debug.Log(  " ====  测试 回调数据  ====" );
        //Debug.Log(  "Srv.CentID:"       + TempSRVList.nCentralServerID.ToString() );
        //Debug.Log(  "Srv.Total:"        + TempSRVList.nTotal.ToString() );
        //Debug.Log(  "Srv.loop:"         + TempSRVList.nloop.ToString() );
        //Debug.Log(  "Srv.Num:"          + TempSRVList.nNum.ToString() );
        //Debug.Log(  "Srv.SvrListCount:" + TempSRVList.gameSrvInfoList.Length.ToString());
        //Debug.Log(  "Srv.IP:"           + TempSRVList.gameSrvInfoList[0].szGameSrvIP.ToString());
        //Debug.Log(  "Srv.PORT:"         + TempSRVList.gameSrvInfoList[0].nProt.ToString());
        //Debug.Log(  "Srv.LineID:"       + TempSRVList.gameSrvInfoList[0].nGameServerLineID.ToString());
        //Debug.Log(  "Srv.Position:"     + TempSRVList.gameSrvInfoList[0].nPosition.ToString());
        //Debug.Log(  "Srv.State:"        + TempSRVList.gameSrvInfoList[0].nState.ToString());
        //Debug.Log(  "Srv.szGameSrvName:"+ TempSRVList.gameSrvInfoList[0].szGameSrvName.ToString());
        return                  TempSRVList;
    }
    public static RET_LOGIN_PUBLIC_INFO                 Dec_PublicInfo      (byte[] msg)            // 解析公告信息                       
    {
        RET_LOGIN_PUBLIC_INFO   TempPublicInfo          = new RET_LOGIN_PUBLIC_INFO();
        int                     startIndex              = 0;

        TempPublicInfo.Head                             = (Head)Util.BytesToBase(TempPublicInfo.Head, msg, ref startIndex);
        byte[]                  tempPublicText          = new byte[msg.Length - startIndex];
        Array.Copy              (msg, startIndex, tempPublicText, 0, msg.Length - startIndex);
        TempPublicInfo.publicText                       = System.Text.Encoding.UTF8.GetString(tempPublicText);
        return                  TempPublicInfo;
    }
    public static SYNC_LOGIN_AllPlayerInfo              Dec_AllPlayerInfo   (byte[] msg)            // 解析帐号下全部玩家信息              
    {
        SYNC_LOGIN_AllPlayerInfo TempAllPlayerInfo      = new SYNC_LOGIN_AllPlayerInfo();
        int                     startIndex              = 0;

        TempAllPlayerInfo.Head                          = (Head)Util.BytesToBase(TempAllPlayerInfo.Head, msg, ref startIndex);
        TempAllPlayerInfo.nNum                          = (int)Util.BytesToBase(TempAllPlayerInfo.nNum, msg, ref startIndex);
        TempAllPlayerInfo.PlayerData                    = new LOGIN_PlayerInfo[TempAllPlayerInfo.nNum];
        for(int i = 0;i < TempAllPlayerInfo.nNum; i++)
        {
            LOGIN_PlayerInfo    TempPlayerInfo          = new LOGIN_PlayerInfo();
            TempPlayerInfo.nPlayerLv                    = (int)Util.BytesToBase(TempPlayerInfo.nPlayerLv, msg, ref startIndex);
            TempPlayerInfo.nPLayerIcon                  = (int)Util.BytesToBase(TempPlayerInfo.nPLayerIcon, msg, ref startIndex);
            TempPlayerInfo.szPlayerName                 = (string)Util.BytesToBase(TempPlayerInfo.szPlayerName = "", msg, ref startIndex);
            TempAllPlayerInfo.PlayerData[i]             = TempPlayerInfo;
        }
        return TempAllPlayerInfo;
    }   
    public static RET_HERO_Info                         Dec_HeroInfo        (byte[] msg)            // 解析英雄信息                       
    {
        Debug.Log                                       ("手动解析 英雄信息!");
        Debug.Log                                       ("msg 消息长度: " + msg.Length.ToString());
        RET_HERO_Info           TempHeroInfo            = new RET_HERO_Info();
        int                     startIndex              = 0;

        TempHeroInfo.head                               = (Head)Util.BytesToBase( TempHeroInfo.head,            msg, ref startIndex);       // 头文件
        TempHeroInfo.nHerosNum                          = (int)Util.BytesToBase ( TempHeroInfo.nHerosNum,       msg, ref startIndex);       // 英雄总数
        TempHeroInfo.npackgeIndex                       = (int)Util.BytesToBase ( TempHeroInfo.npackgeIndex,    msg, ref startIndex);       // 包索引
        TempHeroInfo.npackgeNum                         = (int)Util.BytesToBase ( TempHeroInfo.npackgeNum,      msg, ref startIndex);       // 本条数量

        TempHeroInfo.heroDataList                       = new HERO_DATA[TempHeroInfo.npackgeNum];                                            // 英雄信息
        for(int i = 0;i < TempHeroInfo.npackgeNum; i++ )
        {
            HERO_DATA           TempHeroData            = new HERO_DATA();
            TempHeroData.nHeroID                        = (int)Util.BytesToBase(TempHeroData.nHeroID,           msg, ref startIndex);       // 1.英雄ID
            TempHeroData.nHeroLv                        = (int)Util.BytesToBase(TempHeroData.nHeroLv,           msg, ref startIndex);       // 2.英雄等级
            TempHeroData.nHeroExp                       = (int)Util.BytesToBase(TempHeroData.nHeroExp,          msg, ref startIndex);       // 3.英雄经验
            TempHeroData.nHeroStarLv                    = (int)Util.BytesToBase(TempHeroData.nHeroStarLv,       msg, ref startIndex);       // 4.英雄星级
            TempHeroData.nHeroQuality                   = (int)Util.BytesToBase(TempHeroData.nHeroQuality,      msg,ref startIndex);        // 5.英雄品质
            TempHeroData.nMedalLv                       = (int)Util.BytesToBase(TempHeroData.nMedalLv,          msg, ref startIndex);       // 6.英雄勋章等级
            TempHeroData.nMedalExp                      = (int)Util.BytesToBase(TempHeroData.nMedalExp,         msg, ref startIndex);       // 7.英雄勋章经验
            TempHeroData.nWingID                        = (int)Util.BytesToBase(TempHeroData.nWingID,           msg, ref startIndex);       // 8.英雄翅膀
            TempHeroData.nHeroEquips                    = new int[6];                                                                       // 9.装备列表
            for(int j = 0; j < 6;j++)
            {
                TempHeroData.nHeroEquips[j]             = (int)Util.BytesToBase((int)0, msg, ref startIndex);
            }
            TempHeroData.nHeroSkillLv                   = new int[4];                                                               // 10.技能列表
            for(int k = 0; k < 4; k++)
            {
                TempHeroData.nHeroSkillLv[k]            = (int)Util.BytesToBase((int)0, msg, ref startIndex);
            }
            TempHeroInfo.heroDataList[i]                = TempHeroData;
        }
        Debug.Log("手动解析 英雄信息完成!");
        return                  TempHeroInfo;
    }
    public static RET_BAG_LIST                          Dec_BagList         (byte[] msg)                                            // 背包数据列表                       
    {
        RET_BAG_LIST            TempBagList             = new RET_BAG_LIST();
        int                     startIndex              = 0;

        TempBagList.Head                                = (Head)Util.BytesToBase(TempBagList.Head, msg, ref startIndex);
        TempBagList.PackTotalCount                      = (short)Util.BytesToBase(TempBagList.PackTotalCount, msg, ref startIndex);
        TempBagList.Size                                = (short)Util.BytesToBase(TempBagList.PackTotalCount, msg, ref startIndex);
        TempBagList.bagList                             = new BagData[TempBagList.Size];
        for(int i = 0; i < TempBagList.Size; i++)
        {
            BagData             TempBagData             = new BagData();
            TempBagData.nItemID                         = (int)Util.BytesToBase (TempBagData.nItemID, msg, ref startIndex);
            TempBagData.nNum                            = (int)Util.BytesToBase(TempBagData.nNum, msg, ref startIndex);
            TempBagData.nGetTime                        = (int)Util.BytesToBase(TempBagData.nGetTime, msg, ref startIndex);
            TempBagData.Type                            = (byte)Util.BytesToBase(TempBagData.Type, msg, ref startIndex);
            TempBagList.bagList[i]                      = TempBagData;
        }
        return                  TempBagList;
    }
}