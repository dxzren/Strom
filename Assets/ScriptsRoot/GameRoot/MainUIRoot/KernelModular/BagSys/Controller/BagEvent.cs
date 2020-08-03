using UnityEngine;
using System.Collections;

public class BagEvent
{
    public static string EventUpdateItem = "EventUpdateItem";                                   // 更新物品 事件 
    public static string EventUse = "EventUse";                                                 // 使用物品 事件
    public static string EventSell = "EventSell";                                               // 售出物品 事件
    public static string EventLoadItem = "EventLoadItem";                                       // 加载物品 事件
    public static string EventPropChangeCount = "EventPropChangeCount";                         // 道具数量更新 事件
    public static string EventHeroExpCallback = "EventHeroExpCallback";                         // 英雄经验回调 事件

    public static string EventUpLeve = "EventUpLeve";                                           // 升级 事件
    public static string EventUpLeveCallback = "EventUpLeveCallback";                           // 升级回调 事件 
    public static string EventSellItem = "EventSellItem";                                       // 售出物品 事件
    public static string EventSellItemCallback = "EventSellItemCallback";                       // 售出物品回调 事件
    public static string EventMergeItem = "EventMergeItem";                                     // 合成物品 事件
    public static string EventMergeItemCallback = "EventMergeItemCallback";                     // 合成物品回调 事件
    public static string EventInitBagItem = "EventInitBagItem";                                 // 初始化背包物品 事件
    public static string EventInitBagItemCallback = "EventInitBagItemCallback";                 // 初始化背包物品回调 事件
}