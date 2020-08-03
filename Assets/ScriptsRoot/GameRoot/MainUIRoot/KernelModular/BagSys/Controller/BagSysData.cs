using UnityEngine;
using System.Collections;
using System;

public class BagSysData : IBagSysData
{
    private int _itemID     = 0;
    private int _itemCount  = 0;
    private int _itemPrice  = 0;

    private string _itemName = "None";
    private string _itemIcon = "None";

    bool _isPerssed = false;
    bool _sellOrUse = false;

    private ItemType _itemType = ItemType.None;
    private ItemQuality _itemQuality = ItemQuality.White;

    private DateTime _itemTime;
    private IHeroData _Hero;

    public int itemID                                                       // 物品ID                              
    {
        set { this._itemID = value; }
        get { return this._itemID; }
    }
    public int itemCount                                                    // 物品数量                             
    {
        set { this.itemCount = value; }
        get { return this.itemCount; }
    }
    public int itemPrice                                                    // 物品价格                             
    {
        set { this._itemPrice = value; }
        get { return this._itemPrice; }
    }

    public string itemName                                                  // 物品名称                             
    {
        set { this._itemName = value; }
        get { return this._itemName; }
    }
    public string itemIcon                                                  // 物品图标                             
    {
        set { this._itemIcon = value; }
        get { return this._itemIcon; }
    }

    public bool isPressed                                                   // 检测Press or Click                  
    {
        set { this._isPerssed = value; }
        get { return this._isPerssed; }
    }
    public bool sellOrUse                                                   // 售出 or 使用                         
    {
        set { this._sellOrUse = value; }
        get { return this._sellOrUse; }
    }
    public ItemType itemType                                                // 物品类型                             
    {
        set { this._itemType = value; }
        get { return this._itemType; }
    }
    public ItemQuality itemQuality                                          // 物品品质                             
    {
        set { this.itemQuality = value; }
        get { return this.itemQuality; }
    }

    public DateTime itemTime                                                // 物品获取时间                         
    {
        set { this._itemTime = value; }
        get { return this._itemTime; }
    }
    public IHeroData Hero                                                   // 英雄                                
    {
        set { this._Hero = value; }
        get { return this._Hero; }
    }

}

public class BagObject          
{
    public int ID;                                      // 配置表ID
    public DateTime time = DateTime.Now;                // 获取时间
    public int count;                                   // 数量
}
public class Equip              : BagObject
{

}
public class Fragment           : BagObject
{

}
public class Prop               : BagObject
{

}
public class Soul               : BagObject
{

}