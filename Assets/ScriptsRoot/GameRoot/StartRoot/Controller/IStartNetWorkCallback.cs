using UnityEngine;
using System.Collections;

public interface IStartNetWorkCallback
{
    void OnServerListResponse(EventBase obj);                               // 服务器列表回调
    void OnPublicResponse(EventBase obj);                                   // 公告回调
    void OnLogInGameServerResponse(EventBase obj);                          // 登录游戏服回调
    void OnLogInCheckCodeResponse(EventBase obj);                           // 校验码回调
    void OnGetRoleResponse(EventBase obj);                                  // 获取帐号角色信息回调

    void OnCheckCreateRoleResponse(EventBase obj);                          // 创建角色前的验证回调
    void OnCreateRoleResponse(EventBase obj);                               // 创建角色回调
    void OnPlayerInfoResponse(EventBase obj);                               // 同步玩家信息回调
    void OnHeroInfoResponse(EventBase obj);                                 // 同步英雄信息回调



    void OnBagInfoResponse(EventBase obj);                                  // 同步包裹信息回调

    void OnServerTimeResponse(EventBase obj);                               // 同步服务器时间回调

    void OnLoadResResponse(EventBase obj);                                  // 加载资源回调

    void OnLoadResSucceedResponse(EventBase obj);                           // 下载成功回调
    void OnMonthCardTimeResponse(EventBase obj);                            // 月卡时间戳回调
    void OnRechargeTimesResponse(EventBase obj);                            // 充值次数回调
    void OnPlayerIDRechargeResponse(EventBase obj);                         // 玩家ID回调
    void OnHornMsgResponse(EventBase obj);                                  // 喇叭消息回调

    void OnURLResponse(EventBase obj);                                      // 充值地址回调


    void OnAllCurrencyResponse(EventBase obj);                              // 同步所有货币回调
    void OnCurrencyResponse(EventBase obj);                                 // 同步单项货币回调
    void OnRechargeMoneyResponse(EventBase obj);                            // 同步充值金额回调


}