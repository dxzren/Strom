using UnityEngine;
using System.Collections;

public interface IMainNetWorkCallback
{
    void OnBuyResponse(EventBase obj);                  // 购买回调
    void OnChangeIconResponse(EventBase obj);           // 更换头像回调
    void OnChangeNameResponse(EventBase obj);           // 更换名称回调
    void OnUpLvAwardResponse(EventBase obj);            // 升级奖励回调
    void TimeSyncResponse(EventBase obj);               // 同步时间回调
}