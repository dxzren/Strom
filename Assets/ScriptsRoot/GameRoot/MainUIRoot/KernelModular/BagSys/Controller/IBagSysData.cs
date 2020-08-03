using UnityEngine;
using System.Collections;
using System;


public interface IBagSysData
{
    int itemID { set; get; }                            // 物品ID
    int itemCount { set; get; }                         // 物品数量
    int itemPrice { set; get; }                         // 物品价格


    string itemName { set; get; }                       // 物品名称
    string itemIcon { set; get; }                       // 物品图标


    bool isPressed { set; get; }                        // Press or Click
    bool sellOrUse { set; get; }                        // 售出 or 使用

    ItemType itemType { set; get; }                     // 物品类型
    ItemQuality itemQuality { set; get; }               // 物品品质
    DateTime itemTime { set; get; }                     // 物品获取时间

    IHeroData Hero { set; get; }                        // 英雄
}

