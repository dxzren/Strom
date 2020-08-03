using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using strange.extensions.dispatcher.eventdispatcher.api;

public interface IBagNetWorkCallback
{
    void OnSellItemResponse(EventBase obj);                         // 卖出物品回调
    void OnUseHeroExpPropResponse(EventBase obj);                   // 使用英雄经验道具回调
}