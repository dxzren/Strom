using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

/// <summary> 提示面板视图 </summary>
public class TipsPanelView : EventView
{
    public IPlayer player;

    public int                      ID                              = 0;
    public int                      type                            = 0;
    public float                    targWidth, targHeight;
    public bool                     boxTips                         = false;                                                /// 是否为等级宝箱 特色处理

    public UILabel                  TipsDes, CurrencyLabel;                                                                 /// 提示说明, 货币标签

    public UILabel                  HeroLevel,   HeroName;                                                                  /// 英雄:      名称, 等级
    public UILabel                  MonsterName, MonsterLevel, MonsterBossName, MonsterBossLevel;                           /// Npc/Boss:  名称, 等级

    public UILabel                  SoulName,  SoulPrice;                                                                   /// 魂石: 名称, 价值
    public UILabel                  FragName,  FragPrice,   FragCount;                                                      /// 碎片: 名称, 价值, 数量
    public UILabel                  EquipName, EquipPrice,  EquipCount;                                                     /// 装备: 名称, 价值, 数量
    public UILabel                  PropName,  PropPrice,   PropCount;                                                      /// 道具: 名称, 价值, 数量


    public UISprite                 TipsIcon,  TipsBackGround;                                                              /// 提示图标,提示背框
    public UISprite                 HeroType,  MonsterType, MonsterBossType;                                                /// 英雄类型,NPC类型,Boss类型
    public UISprite                 BackGround;                                                                             /// 背景

    public UIAtlas                  HeroAtlas, FragAtlas,   EquipAtlas, PropAtlas;                                          /// 英雄图集,碎片图集,装备图集,道具图集

    public GameObject               Monster,   MonsterBoss, Hero, Soul, Frag, Equip, Prop;                                  /// NPC,Boss,英雄,魂石,道具,碎片,装备
    public GameObject               ItemObj,   CurrencyObj;                                                                 /// 物品对象,货币对象

    public Configs_CheckPointData   CheckPointD;



    public void                     SetData (GameObject obj,float x,float y,int id,int type,Configs_CheckPointData checkPointData)  // 设置数据                   
    {
        this.ID = id;
        this.type = type;
        this.CheckPointD = checkPointData;
        this.targWidth = x;
        this.targHeight = y;

        GetParentList(obj.transform);
        if(!IsInvoking("CheckObj"))
        {
            InvokeRepeating("CheckObj", 0.1f, 1f);
        }
    }
    public void                     ShowTips()                                                                                      // 显示提示面板               
    {
        switch (type)
        {
            case (int)TipsHero.hero:                                                            // 1.已有英雄                        
                {
                    Configs_HeroData HeroD = Configs_Hero.sInstance.GetHeroDataByHeroID(ID);                                        // 英雄数据类型实例数据

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Hero.SetActive(true);                                                                                           // 英雄对象 活动
                    TipsIcon.atlas = HeroAtlas;                                                                                     // 提示头像图集
                    TipsBackGround.atlas = HeroAtlas;                                                                               // 提示背框图集                    
                    TipsIcon.spriteName = HeroD.head84;                                                                             // 提示头像图集标签名称
                    TipsDes.text = Language.GetValue(HeroD.HeroDes);                                                                // 文本显示英雄说明

                    switch (HeroD.Polarity)                                                                                         // 元素类型图集标签名称       
                    {
                        case (int)ElementType.Ice:                                                                          // 图集标签名称 冰属性
                            HeroType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Ice);
                            break;
                        case (int)ElementType.Fire:                                                                         // 图集标签名称 火属性
                            HeroType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Fire);
                            break;
                        case (int)ElementType.Thunder:                                                                      // 图集标签名称 雷属性
                            HeroType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Thunder);
                            break;
                    }
                    foreach (IHeroData hero in player.HeroList)                                                                   // 获取英雄背框,名称,等级,元素类型参数
                    {
                        if (hero.ID == ID)
                        {
                            TipsBackGround.spriteName = PanelManager.sInstance.HeroQualitySpriteName(hero.Quality);                 // 提示背框图集标签名称
                            Util.SetHeroNameDefine(MonsterName, hero.Quality, HeroD);                                               // 显示英雄名字
                            HeroLevel.text = "LV:" + hero.HeroLevel;                                                                // 文本显示英雄等级
                            break;
                        }
                    }

                }
                break;
            case (int)CurrencyType.hero:
            case (int)TipsHero.newHero:                                                         // 2.新英雄                          
                {
                    Configs_HeroData HeroD = Configs_Hero.sInstance.GetHeroDataByHeroID(ID);                                        // 英雄数据类型实例数据

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Hero.SetActive(true);                                                                                           // 英雄对象 活动
                    TipsIcon.atlas = HeroAtlas;                                                                                     // 提示头像图集
                    TipsBackGround.atlas = HeroAtlas;                                                                               // 提示背框图集
                    TipsIcon.spriteName = HeroD.head84;                                                                             // 提示头像图集标签名称
                    TipsBackGround.spriteName = PanelManager.sInstance.GetSpriteName(SpriteType.HF_White);                          // 提示背框图集标签名称
                    Util.SetHeroNameDefine(HeroName, (HeroQuality)TipsHero.hero, HeroD);                                            // 设置英雄名称
                    TipsDes.text = Language.GetValue(HeroD.HeroDes);                                                                // 文本显示英雄说明

                    switch (HeroD.Polarity)                                                                                         // 元素类型图集标签名称       
                    {
                        case (int)ElementType.Ice:                                                                          // 图集标签名称 冰属性
                            HeroType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Ice);
                            break;
                        case (int)ElementType.Fire:                                                                         // 图集标签名称 火属性
                            HeroType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Fire);
                            break;
                        case (int)ElementType.Thunder:                                                                      // 图集标签名称 雷属性
                            HeroType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Thunder);
                            break;
                    }
                }
                break;
            case (int)TipsHero.boss:                                                            // 3.BOSS_与关卡有关                 
                {
                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    MonsterBoss.SetActive(true);                                                                                    // BOSS对象 活动
                    TipsIcon.atlas = HeroAtlas;                                                                                     // 提示头像图集
                    TipsBackGround.atlas = HeroAtlas;                                                                               // 提示背框图集
                    Configs_HeroData HeroD = Configs_Hero.sInstance.GetHeroDataByHeroID(ID);                                        // 英雄数据类型实例数据
                    TipsIcon.spriteName = HeroD.head84;                                                                             // 提示头像图集标签名称

                    TipsBackGround.spriteName = PanelManager.sInstance.HeroQualitySpriteName((HeroQuality)CheckPointD.NPCQuality);  // 提示背框图集标签名称
                    Util.SetHeroNameDefine(MonsterBossName, (HeroQuality)CheckPointD.NPCQuality, HeroD);                             // 设置NPC名字
                    switch (HeroD.Polarity)                                                                                         // 元素类型图集标签名称       
                    {
                        case (int)ElementType.Ice:                                                                                  // 图集标签名称 冰属性
                            MonsterBossType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Ice);
                            break;
                        case (int)ElementType.Fire:                                                                                 // 图集标签名称 火属性
                            MonsterBossType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Fire);
                            break;
                        case (int)ElementType.Thunder:                                                                              // 图集标签名称 雷属性
                            MonsterBossType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Thunder);
                            break;
                    }
                    MonsterBossLevel.text = "LV:" + CheckPointD.NPCLevel;                                                           // 英雄等级
                    TipsDes.text = Language.GetValue(HeroD.HeroDes);                                                                // 英雄说明

                }
                break;
            case (int)TipsHero.npc:                                                             // 4.NPC_与关卡有关                  
                {
                    Configs_HeroData HeroD = Configs_Hero.sInstance.GetHeroDataByHeroID(ID);                                        // 英雄数据类型实例数据

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Monster.SetActive(true);                                                                                        // BOSS对象 活动
                    TipsIcon.atlas = HeroAtlas;                                                                                     // 提示头像图集
                    TipsBackGround.atlas = HeroAtlas;                                                                               // 提示背框图集
                    TipsIcon.spriteName = HeroD.head84;                                                                             // 提示头像图集标签名称
                    TipsBackGround.spriteName = PanelManager.sInstance.HeroQualitySpriteName((HeroQuality)CheckPointD.NPCQuality);  // 提示背框图集标签名称
                    Util.SetHeroNameDefine(MonsterName, (HeroQuality)CheckPointD.NPCQuality, HeroD);                                // 设置NPC名字
                    switch (HeroD.Polarity)                                                                                         // 元素类型图集标签名称       
                    {
                        case (int)ElementType.Ice:                                                                                  // 图集标签名称 冰属性
                            MonsterType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Ice);
                            break;
                        case (int)ElementType.Fire:                                                                                 // 图集标签名称 火属性
                            MonsterType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Fire);
                            break;
                        case (int)ElementType.Thunder:                                                                              // 图集标签名称 雷属性
                            MonsterType.spriteName = PanelManager.sInstance.GetElementTypeSpriteName(ElementType.Thunder);
                            break;
                    }
                    MonsterLevel.text = "LV:" + CheckPointD.NPCLevel;                                                               // NPC等级
                    TipsDes.text = Language.GetValue(HeroD.HeroDes);                                                                // NPC说明
                }
                break;
            case (int)ItemType.soul:                                                            // 5.英雄魂石                        
                {
                    Configs_SoulData SoulD = Configs_Soul.sInstance.GetSoulDataBySoulID(ID);

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Hero.SetActive(true);                                                                                           // 英雄对象 活动
                    TipsIcon.atlas = HeroAtlas;                                                                                     // 提示头像图集
                    TipsBackGround.atlas = HeroAtlas;                                                                               // 提示背框图集                    
                    TipsIcon.spriteName = SoulD.head84;                                                                             // 提示头像图集标签名称
                    TipsBackGround.spriteName = PanelManager.sInstance.GetSpriteName(SpriteType.SoulFrame);                         // 提示背框图集标签名称                    
                    int soulCount = 0;
                    foreach (Soul soul in player.GetHeroSoulList)                                                                   // 获取魂石数量       
                    {
                        if (soul.ID == this.ID)
                        {
                            soulCount = soul.count;
                            break;
                        }
                    }
                    string TempHeroName = Language.GetValue(Configs_Hero.sInstance.GetHeroDataByHeroID(SoulD.Target).HeroName);     // 英雄名称

                    SoulName.text = "魂石(" + TempHeroName + ") (" + soulCount + "/" + SoulD.Num + ")";                             // 魂石名称,魂石数量, 
                    SoulPrice.text = SoulD.SellPrice + "";                                                                          // 魂石售价
                    TipsDes.text = Language.GetValue(SoulD.SoulDes, SoulD.Num, TempHeroName);                                       // 魂石说明
                }
                break;
            case (int)ItemType.scroll:
            case (int)ItemType.equip:                                                           // 6.装备                            
                {
                    Configs_EquipData EquipD = Configs_Equip.sInstance.GetEquipDataByEquipID(ID);

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Equip.SetActive(true);                                                                                          // 装备对象 活动
                    TipsIcon.atlas = EquipAtlas;                                                                                    // 提示头像图集
                    TipsBackGround.atlas = PropAtlas;                                                                               // 提示背框图集                    
                    TipsIcon.spriteName = EquipD.EquipIcon_84;                                                                      // 提示头像图集标签名称
                    TipsBackGround.spriteName = PanelManager.sInstance.PropToSpriteName(EquipD.EquipQuality);                       // 提示背框图集标签名称  

                    int equipCount = 0;
                    foreach (Equip equip in player.EquipList)                                                                       // 获取装备数量       
                    {
                        if (equip.ID == this.ID)
                        {
                            equipCount = equip.count;
                            break;
                        }
                    }
                    EquipName.text = Language.GetValue(EquipD.EquipName);                                                           // 装备名称
                    EquipCount.text = "拥有" + equipCount + "件";                                                                    // 装备数量
                    EquipPrice.text = EquipD.SellPrice + "";                                                                        // 装备售价
                    TipsDes.text = GetEquipDes(ID);                                                                                 // 装备说明文本
                }
                break;
            case (int)ItemType.scrollFragment:
            case (int)ItemType.equipFragment:                                                   // 7.装备碎片                        
                {
                    Configs_FragmentData FragD = Configs_Fragment.sInstance.GetFragmentDataByFragmentID(ID);
                    Configs_EquipData EquipD = Configs_Equip.sInstance.GetEquipDataByEquipID(FragD.Target);

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Equip.SetActive(true);                                                                                          // 碎片对象 活动
                    TipsIcon.atlas = EquipAtlas;                                                                                    // 提示头像图集
                    TipsBackGround.atlas = FragAtlas;                                                                               // 提示背框图集                    
                    TipsIcon.spriteName = FragD.FragmentIcon_84;                                                                    // 提示头像图集标签名称
                    TipsBackGround.spriteName = PanelManager.sInstance.FragToSpriteName(FragD.FragmentQuality);                     // 提示背框图集标签名称  

                    int fragCount = 0;
                    foreach (Fragment frag in player.FragmentList)                                                                  // 获取碎片数量       
                    {
                        if (frag.ID == this.ID)
                        {
                            fragCount = frag.count;
                            break;
                        }
                    }

                    FragName.text = Language.GetValue(FragD.FragmentName) + "(" + fragCount + "/" + FragD.Num + ")";                // 碎片名称,碎片数量,合成数量
                    FragPrice.text = FragD.SellPrice + "";                                                                          // 碎片售价
                    TipsDes.text = Language.GetValue(FragD.FragmentDes, FragD.Num, Language.GetValue(EquipD.EquipName));            // 碎片说明文本
                }
                break;
            case (int)ItemType.coinsprop:
            case (int)ItemType.heroExpProp:
            case (int)ItemType.medalExpProp:
            case (int)ItemType.wingExpProp:
            case (int)ItemType.ticket:

            case (int)ItemType.staminaProp:
            case (int)ItemType.protectedstone:
            case (int)ItemType.jinjiestone:
            case (int)ItemType.mercExpProp:

            case (int)ItemType.SkillProp:
            case (int)ItemType.soulbag:
            case (int)ItemType.diamondsbag:                                                     // 8.道具物品                        
                {
                    Configs_PropData PropD = Configs_Prop.sInstance.GetPropDataByPropID(ID);
                    if(PropD == null)
                    {   Debug.Log(type + "/" + ID);     }

                    ItemObj.SetActive(true);                                                                                        // 物品对象 活动
                    CurrencyObj.SetActive(false);                                                                                   // 货币对象 关闭
                    Prop.SetActive(true);                                                                                           // 道具对象 活动
                    TipsIcon.atlas = PropAtlas;                                                                                     // 提示头像图集
                    TipsBackGround.atlas = PropAtlas;                                                                               // 提示背框图集                    
                    TipsIcon.spriteName = PropD.PropIcon84;                                                                         // 提示头像图集标签名称
                    TipsBackGround.spriteName = PanelManager.sInstance.PropToSpriteName(PropD.PropQuality);                         // 提示背框图集标签名称  

                    int propCount = 0;
                    foreach (Prop prop in player.PropList)                                                                          // 获取道具数量       
                    {
                        if (prop.ID == this.ID)
                        {
                            propCount = prop.count;
                            break;
                        }
                    }
                    PropName.text = Language.GetValue(PropD.PropName);                                                              // 道具名称
                    EquipCount.text = "拥有" + propCount + "件";                                                                    // 道具数量
                    EquipPrice.text = PropD.SellPrice.ToString();                                                                   // 道具售价
                    TipsDes.text = Language.GetValue(PropD.PropDes, PropD.Value);                                                   // 道具说明文本
                }
                break;
            case (int)ItemType.diamonds:                                                        // 9.钻石                            
                {
                    ItemObj.SetActive(false);                                   // 物品对战 关闭
                    CurrencyObj.SetActive(true);                                // 货币对象 活动
                    CurrencyLabel.text = Language.GetValue("Diamond_des1");     // 货币标签文本
                }
                break;
            case (int)ItemType.coins:                                                           // 10.金币                           
                {
                    ItemObj.SetActive(false);                                   // 物品对战 关闭
                    CurrencyObj.SetActive(true);                                // 货币对象 活动
                    CurrencyLabel.text = Language.GetValue("Gold_des1");        // 货币标签文本
                }
                break;
            case (int)ItemType.SecretTowerCoins:                                                // 11.竞技场货币                     
                {
                    ItemObj.SetActive(false);                                   // 物品对战 关闭
                    CurrencyObj.SetActive(true);                                // 货币对象 活动
                    CurrencyLabel.text = Language.GetValue("MijingGold_des1");  // 货币标签文本
                }
                break;
            case (int)ItemType.ParadiseRoadCoins:                                               // 12.天堂之路货币                   
                {
                    ItemObj.SetActive(false);                                   // 物品对战 关闭
                    CurrencyObj.SetActive(true);                                // 货币对象 活动
                    CurrencyLabel.text = Language.GetValue("TainluGold_des1");  // 货币标签文本
                }
                break;
            case (int)ItemType.JJCCoins:                                                        // 13.竞技场货币                     
                {
                    ItemObj.SetActive(false);                                   // 物品对战 关闭
                    CurrencyObj.SetActive(true);                                // 货币对象 活动
                    CurrencyLabel.text = Language.GetValue("JingjiGold_des1");  // 货币标签文本
                }
                break;
            case 200:                                                                           // 14.VIP                            
                {
                    ItemObj.SetActive(false);                                   // 物品对战 关闭
                    CurrencyObj.SetActive(true);                                // 货币对象 活动
                    CurrencyLabel.text = Language.GetValue("JingjiGold_des1");  // 货币标签文本
                }
                break;

        }        
    }   

    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
    private GameObject theObj = null;
    private List<GameObject> theParentList = new List<GameObject>();
 
    private string                  GetEquipDes     (int equipID)                                                           // 获取装备说明文本            
    {
        string equipText = "";
        Configs_EquipData EquipD = Configs_Equip.sInstance.GetEquipDataByEquipID(equipID);
        List<string> theDesList = new List<string>();

        string attrib_1 = GetAttribDes(EquipD.Attribute1, EquipD.AttributeValue1);
        string attrib_2 = GetAttribDes(EquipD.Attribute2, EquipD.AttributeValue2);
        string attrib_3 = GetAttribDes(EquipD.Attribute3, EquipD.AttributeValue3);
        string attrib_4 = GetAttribDes(EquipD.Attribute4, EquipD.AttributeValue4);
        string attrib_5 = GetAttribDes(EquipD.Attribute5, EquipD.AttributeValue5);
        string attrib_6 = GetAttribDes(EquipD.Attribute6, EquipD.AttributeValue6);

        if (attrib_1.CompareTo("") != 0)
        {
            theDesList.Add(attrib_1);
        }
        if (attrib_2.CompareTo("") != 0)
        {
            theDesList.Add(attrib_2);
        }
        if (attrib_3.CompareTo("") != 0)
        {
            theDesList.Add(attrib_4);
        }
        if (attrib_3.CompareTo("") != 0)
        {
            theDesList.Add(attrib_5);
        }
        if (attrib_3.CompareTo("") != 0)
        {
            theDesList.Add(attrib_6);
        }

        if(theDesList.Count > 0)
        {
            for(int i = 0;i < theDesList.Count;i++)
            {
                if(i != (theDesList.Count - 1))
                {
                    equipText = equipText + theDesList[i] + "\n";
                }
                else
                {
                    equipText = equipText + theDesList[i];
                }
            }
        }
        else
        {
            equipText = Language.GetValue(EquipD.EquipDes);
        }
        return equipText;
    }
    private string                  GetAttribDes    (int attributeType,int attributeValue)                                  // 装备单项属性说明文本        
    {
        string attribText = "";
        if(attributeType != 0 && attributeValue != 0)
        {
            switch(attributeType)
            {
                case 1:
                    attribText = Util.GetHeroAttribTypeName(HeroAttribType_Lv1.Power);
                    break;
                case 2:
                    attribText = Util.GetHeroAttribTypeName(HeroAttribType_Lv1.Agile);
                    break;
                case 3:
                    attribText = Util.GetHeroAttribTypeName(HeroAttribType_Lv1.Intellect);
                    break;
                case 4:
                    attribText = Util.GetHeroAttribTypeName(HeroAttribType_Main.Blood);
                    break;
                case 5:
                    attribText = Util.GetHeroAttribTypeName(HeroAttribType_Main.PhyAttack);
                    break;
                case 6:
                    attribText = Util.GetHeroAttribTypeName(HeroAttribType_Main.MagicAttack);
                    break;
            }
        }
        return attribText;
    }
    
    private void                    GetParentList   (Transform obj)                        
    {
        while(obj.parent != null)
        {
            obj = obj.parent;
            theParentList.Add(obj.gameObject);
        }
    }
    private void                    CheckObj()                                          
    {
        if(theObj == null || theObj.activeSelf == false)
        {
            Destroy(gameObject);
            CancelInvoke("CheckObj");
            return;
        }
        if(theParentList.Count > 0)
        {
            for(int i = 0;i < theParentList.Count; i++)
            {
                if(theParentList[i].activeSelf == false)
                {
                    theParentList.Clear();
                    Destroy(gameObject);
                    CancelInvoke("CheckObj");
                    return;
                }
            }
        }
    }
    #endregion
}