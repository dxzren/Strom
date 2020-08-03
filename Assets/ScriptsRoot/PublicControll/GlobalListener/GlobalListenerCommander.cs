using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using SimpleJson;
using System.Text;
using System.Globalization;
using System.IO;

public class GlobalListenerCommander : EventCommand
{
    [Inject]
    public ISocket                      socket              { set; get; }                       // 端口
    [Inject]
    public IGlobalNetWorkCallback       globalCallback      { set; get; }                       // 全局回调
    [Inject]
    public IStartNetWorkCallback        startCallback       { set; get; }                       // 启动登录回调
    [Inject]
    public IHeroSysNetWorkCallback      heroSysCallback     { set; get; }                       // 英雄系统回调
    [Inject]
    public IMainNetWorkCallback         mainCallback        { set; get; }                       // 主界面回调
    [Inject]
    public IBagNetWorkCallback          bagSysCallback      { set; get; }                       // 背包系统回调
    [Inject]
    public ICheckPointNetWorkCallback   checkPointCallback  { set; get; }                       // 关卡系统回调


    public override void Execute()
    {

        Debug.Log("启动网络全局监听!");
        #region================================================||  注册监听模块          ||=====================================================
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_LOGIN_CLIENT_LS, (int)LOGIN_CLIENT_GS_CMD.CUSTOM_RELOGIN, globalCallback.ReLogIn);                          // 本地重新登录      回调(客户端_游戏服)

        #region-------------------------------------------------<  监听 登录_核心系统  >------------------------------------------------------------------------------
        //-----------------------<< Start_LoginModeule-- 启动登录模块 >>-------------------------------------------------------------------------
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_LOGIN_CLIENT_LS, (int)LOGIN_CLIENT_LS_CMD.CLLS_RET_SRV_LIST,    startCallback.OnServerListResponse);        // 服务器列表        回调(客户端_登录服)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_LOGIN_CLIENT_LS, (int)LOGIN_CLIENT_LS_CMD.CLLS_RET_PUBLIC,      startCallback.OnPublicResponse);            // 公告             回调(客户端_登录服)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_LOGIN_CLIENT_LS, (int)LOGIN_CLIENT_LS_CMD.CLLS_RET_LOGIN,       startCallback.OnLogInCheckCodeResponse);    // 登录验证码        回调(客户端_登录服)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_LOGIN_CLIENT_GS, (int)LOGIN_CLIENT_GS_CMD.CLGS_SYNC_PLAYER_RoleList, startCallback.OnGetRoleResponse);      // 获取帐号角色信息  回调(客户端_游戏服)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_LOGIN_CLIENT_GS, (int)LOGIN_CLIENT_GS_CMD.CLGS_SYNC_SRV_TIME,   startCallback.OnServerTimeResponse);        // 同步服务器时间    回调(客户端_游戏服)
        NetEventDispatcher.Instance().RegEventListener(3154201,  3154201,  startCallback.OnLoadResResponse);                                                                            // 下载资源          回调(下载)
        NetEventDispatcher.Instance().RegEventListener(31542012, 31542012, startCallback.OnLoadResSucceedResponse);                                                                     // 下载资源完毕       回调(下载)
        NetEventDispatcher.Instance().RegEventListener(31542013, 31542013, globalCallback.NetNotGood);                                                                                  // 下载失败,网络差    回调(下载)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_ENTER_GAME,         startCallback.OnLogInGameServerResponse);   // 进入游戏          回调(登录)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_PLAYER_INFO,        startCallback.OnPlayerInfoResponse);        // 同步玩家信息      回调(登录)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_CREATE_ROLE,        startCallback.OnCreateRoleResponse);        // 创建角色          回调(登录)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_CHECK_CREATE_ROLE,  startCallback.OnCheckCreateRoleResponse);   // 检测创建角色      回调(登录)


        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE,   (short)HERO_CMD.RET_SYNC_HERO_AllHeroInfo,       startCallback.OnHeroInfoResponse);          // 同步所有英雄信息      (登录)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_BAG_MODULE,    (short)BAG_CMD.RET_BAG_List,                     startCallback.OnBagInfoResponse);           // 同步背包信息      回调(登录)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CURRENCY_MODULE, (short)CURRENCY_CMD.CURRENCY_SYNC_ALL_INFO,    startCallback.OnAllCurrencyResponse);       // 同步所有货币      回调(登录)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CURRENCY_MODULE, (short)CURRENCY_CMD.CURRENCY_SYNC_INFO,        startCallback.OnCurrencyResponse);          // 同步所有货币      回调(登录)


        #endregion

        #region-------------------------------------------------<  监听 登录_主界面功能系统  >------------------------------------------------------------------------
        //-----------------------<< PlayerModule--  玩家模块 >>-----------------------------------------------------------------------------
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_SYNC_CURRENCY,          globalCallback.ChangeCurrency);           // 玩家同步货币      回调(全局)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_SYNC_ATTRIBUTE,         globalCallback.ChangePlayerAttr);         // 玩家属性变化      回调(全局)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_SYNC_ERROR_CODE,        globalCallback.ErrorCode);                // 玩家错误码        回调(全局)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_CHANGEICON,         mainCallback.OnChangeIconResponse);       // 更换头像图标      回调(主界面)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_RENAME,             mainCallback.OnChangeNameResponse);       // 更换玩家名称      回调(主界面)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_UPLV_AWARD,         mainCallback.OnUpLvAwardResponse);        // 升级等级奖励      回调(主界面)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_PLAYER_MODULE, (short)PLAYER_CMD.PLAYER_RET_BUY,                mainCallback.OnBuyResponse);              // 购买             回调(主界面)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_SYNC_Push_MODULE, (int)SERVER_SYNC_Push_CMD.SYNC_CLOCK,         mainCallback.TimeSyncResponse);           // 服务器准点同步推送 
        //-----------------------<< PlayerModule--  英雄模块 >>-----------------------------------------------------------------------------
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE, (short)HERO_CMD.RET_HERO_AddHero,                  globalCallback.AddHero);                  // 添加新英雄       回调(全局)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE, (short)HERO_CMD.RET_SYNC_HERO_HeroLvExp,           globalCallback.ChangeHeroLvExp);          // 英雄经验等级同步  回调(全局)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE, (short)HERO_CMD.RET_HERO_UpQuality,                heroSysCallback.OnUpHeroQualityResponse); // 英雄升级品质     回调(英雄系统)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE, (short)HERO_CMD.RET_HERO_UpSkill,                  heroSysCallback.OnUpHeroSkillLvResponse); // 英雄升级技能     回调(英雄系统)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE, (short)HERO_CMD.RET_HERO_UpStarLv,                 heroSysCallback.OnUpHeroStarResponse);    // 英雄升级星级     回调(英雄系统)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_HERO_MODULE, (short)HERO_CMD.RET_HERO_WearEquip,                heroSysCallback.OnWearEquipResponse);     // 英雄穿戴装备     回调(英雄系统)

        //-----------------------<< BagModule--  背包模块 >>--------------------------------------------------------------------------------
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_BAG_MODULE, (short)BAG_CMD.RET_Push_BAG_ChangeItem,             globalCallback.ChangeItemData);           // 物品发生变化     回调(全局)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_BAG_MODULE, (short)BAG_CMD.RET_BAG_MergeItem,                   heroSysCallback.OnMergeEquipResponse);    // 合成物品         回调(英雄系统)

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_BAG_MODULE, (short)BAG_CMD.RET_BAG_SellItem,                    bagSysCallback.OnSellItemResponse);       // 售出物品         回调(背包系统)
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_BAG_MODULE, (short)BAG_CMD.RET_BAG_UseItem,                     bagSysCallback.OnUseHeroExpPropResponse); // 使用物品         回调(背包系统)


        //-----------------------<< CheckPointModule--  关卡模块 >>-----------------------------------------------------------------------------
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_GetStarBox,          checkPointCallback.OnGetStarBoxResponse);               // 获取星级宝箱
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_PassedMaxNormalID,   checkPointCallback.OnPassedMaxNorCHKP_IDResponse);      // 当前已经通关最大关卡ID
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_NormalInof,          checkPointCallback.OnNormalCheckPointInfoResponse);     // 普通大关卡数据
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_EliteInfo,           checkPointCallback.OnEliteCheckPointInfoResponse);      // 精英关卡数据
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_NormalStarBox,       checkPointCallback.OnNorChapterResponse);               // 普通章节数据回调
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_EliteStarBox,        checkPointCallback.OnEliteChatperResponse);             // 精英章节数据回调

        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_ResetChallengeTimes, checkPointCallback.OnResetChallengeTimesResponse);      // 重置关卡挑战次数  回调
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_EliteSweepTimes,     checkPointCallback.OnSweepTimesErrorResponse);          // 精英关卡扫次      回调
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_RET_Sweep,               checkPointCallback.OnSweepResponse);                    // 扫荡             回调
        NetEventDispatcher.Instance().RegEventListener((short)eMsgType._MSG_CHECKPOINT_MODULE, (int)CheckPointMsgType.CHKP_REQ_Challenge,           checkPointCallback.OnChallengeCheckPointResponse);      // 挑战关卡         回调
        #endregion

        #region-------------------------------------------------<  监听 登录_副本关卡系统    >------------------------------------------------------------------------
        #endregion

        #endregion

        #region================================================||  添加处理模块          ||=====================================================
        NetHandlerManager.Instance().AddHandler(LogInResponseHandler.Instance());               // 登录消息处理
        NetHandlerManager.Instance().AddHandler(GlobalResponseHandler.Instance());              // 全局消息处理
        NetHandlerManager.Instance().AddHandler(CheckPointResponseHandler.Instance());          // 关卡消息处理

        #endregion
    }
}