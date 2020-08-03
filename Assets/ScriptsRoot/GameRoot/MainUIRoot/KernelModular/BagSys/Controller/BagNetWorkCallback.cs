using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;
public class BagNetWorkCallback : IBagNetWorkCallback
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { set; get; }

    public void OnSellItemResponse(EventBase obj)                                               // 售出物品回调       
    {
        RET_BAG_SellItem ReSellItem = (RET_BAG_SellItem)obj.eventValue;
        Debug.Log("(售出物品回调) 结果:" + ReSellItem.isSuccess);

        if(ReSellItem.isSuccess == 0)
        {
            dispatcher.Dispatch(BagEvent.EventSellItemCallback);            /// 售出物品回调 事件
            dispatcher.Dispatch(EventSignal.UpdateInfo_Event);              /// 更新界面(金币,钻石,体力)
        }
        else
        {
            PanelManager.sInstance.ShowNoticePanel("服务器返回为 售出失败!");
            Debug.Log(ReSellItem.isSuccess);
        }
    }

    public void OnUseHeroExpPropResponse(EventBase obj)                                         // 使用英雄经验道具回调   
    {
        Debug.Log("使用英雄经验物品回调:");

    }
}