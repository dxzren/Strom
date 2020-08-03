using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;

public class GlobalNetWorkCallback : IGlobalNetWorkCallback
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { set; get; }
    [Inject]
    public IPlayer player { set; get; }
    [Inject]
    public IGameData gameData { set; get; }
    [Inject]
    public IHeroSysData heroSys { set; get; }


    public void AddHero(EventBase obj)                                                          // 添加新英雄            
    {
        Debug.Log("添加新英雄");
        RET_HERO_DATA HeroData = (RET_HERO_DATA)obj.eventValue;
        IHeroData NewHero = new HeroData();
        NewHero.ID = HeroData.HeroData.nHeroID;                             // 1.新英雄ID
        NewHero.HeroExp = HeroData.HeroData.nHeroExp;                       // 2.新英雄经验
        NewHero.HeroLevel = HeroData.HeroData.nHeroLv;                      // 3.新英雄等级
        NewHero.HeroStar = HeroData.HeroData.nHeroStarLv;                   // 4.新英雄星级

        NewHero.Quality = (HeroQuality)HeroData.HeroData.nHeroQuality;      // 5.新英雄品质
        NewHero.MedalObject.medalLv = HeroData.HeroData.nMedalLv;           // 6.勋章等级
        NewHero.MedalObject.medalExp = HeroData.HeroData.nMedalExp;         // 7.勋章经验
        NewHero.WingID = HeroData.HeroData.nWingID;                         // 8.翅膀ID

        int wearCount = 1;                                                  // 穿戴装备列表
        foreach (int id in HeroData.HeroData.nHeroEquips)
        {
            if(id != 0)
            {
                NewHero.EquipList.Add((WearPosition)wearCount, id);
            }
            wearCount += 1;
        }
        int skillCount = 1;                                                 // 技能列表
        foreach (int lv in HeroData.HeroData.nHeroSkillLv)
        {
            switch(skillCount)
            {
                case 1:
                    NewHero.UltSkillLevel = lv;
                    break;
                case 2:
                    NewHero.ActiveSkillLevel1 = lv;
                    break;
                case 3:
                    NewHero.ActiveSkillLevel2 = lv;
                    break;
                case 4:
                    NewHero.PassiveSkillLevel = lv;
                    break;
            }
            skillCount += 1;
        }

        player.AddHero(NewHero);
        heroSys.TempHero = NewHero;

        gameData.HeroSysRed = false;

        dispatcher.Dispatch(GlobalEvent.AddHeroEvent);                                          // 添加新英雄
        if(!PanelManager.sInstance.IsShowPanel(PanelManager.sInstance.GetScene(SceneType.Main),UIPanelConfig.TenTimesPanel))
        {
            dispatcher.Dispatch(HeroSysEvent.HeroInfoOut_Event);                               // 英雄信息位置变动
        }
        PanelManager.sInstance.HideLoadingPanel();                                              // BUG修改 ( 点击卡死 )

        FightForce newfightForce = new FightForce();
        newfightForce.heroID = HeroData.HeroData.nHeroID;                                       // 获取战力英雄ID

   
    }
    public void ChangeCurrency (EventBase obj)                                                  // 玩家货币更新           
    {
        Debug.Log("金币变动");
        SYNC_CURRENCY_DATA CurrencyData = (SYNC_CURRENCY_DATA)obj.eventValue;
        if(CurrencyData.nCurrencyType == (int)CURRENCY_TYPE.Diamond)
        {
            Debug.Log("玩家当前钻石数量 : " + (long)CurrencyData.nValue);
            player.PlayerDiamonds = (long)CurrencyData.nValue;
        }
        else if (CurrencyData.nCurrencyType == (int)CURRENCY_TYPE.Coins)
        {
            Debug.Log("玩家当前金币数量 : " + (long)CurrencyData.nValue);
            player.PlayerCoins = (long)CurrencyData.nValue;
        }
        dispatcher.Dispatch(GlobalEvent.CurrencyEvent);                     // 玩家货币更新
        HeroRedPointManager.heroMergeDic.Clear();                           // 英雄穿戴装备合成清空
    }
    public void ChangeItemData (EventBase obj)                                                  // 玩家物品数据更新       
    {
        RET_BAG_ChangeItem CItemData = (RET_BAG_ChangeItem)obj.eventValue;
        HeroRedPointManager.heroMergeDic.Clear();

        if(CItemData.total > 0)
        {
            int itemCount = 0;
            foreach(ITEM_DATA T in CItemData.itemDataList)
            {
                switch((ItemType)T.itemType)
                {

                    case ItemType.equip:
                    case ItemType.scroll:                                   // 1.装备             
                        {
                            Debug.Log("装备,卷轴发送变化" + T.nItemID + "/" + T.nItemNum);
                            bool isFind = false;

                            for (int i = 0; i < player.EquipList.Count; i++)                    // 检索装备列表
                            {
                                if(T.nItemID == player.EquipList[i].ID)
                                {
                                    if(T.nItemNum == 0)
                                    {
                                        player.EquipList.Remove(player.EquipList[i]);           // 数量为零则移除该装备
                                    }
                                    else
                                    {
                                        player.EquipList[i].count = T.nItemNum;                 // 更新装备数量
                                    }
                                    isFind = true;
                                    break;
                                }
                            }
                            if(!isFind)                                                          // 没在装备列表,添加新装备
                            {
                                Equip newEquip = new Equip();
                                newEquip.ID = T.nItemID;
                                newEquip.count = T.nItemNum;
                                newEquip.time = Util.StampToDateTime(CItemData.time);
                                player.EquipList.Add(newEquip);
                            }
                            HeroRedPointManager.heroMergeDic.Clear();                           // 装备合成缓存清理
                        }                        
                        break;
                    case ItemType.equipFragment:
                    case ItemType.scrollFragment:                           // 2.碎片             
                        {
                            Debug.Log("装备碎片,卷轴碎片发生变化" + T.nItemID + "/" + T.nItemNum);
                            bool isFind = false;

                            for(int i = 0; i < player.FragmentList.Count; i++)                  // 检索碎片列表
                            {
                                if(T.nItemID == player.FragmentList[i].ID)
                                {
                                    if(T.nItemNum == 0)
                                    {
                                        player.FragmentList.Remove(player.FragmentList[i]);     // 数量为零则移除该碎片
                                    }
                                    else
                                    {
                                        player.FragmentList[i].count = T.nItemNum;              // 更新碎片数量
                                    }
                                    isFind = true;
                                    break;
                                }
                            }
                            if(!isFind)
                            {
                                Fragment newFrag = new Fragment();
                                newFrag.ID = T.nItemID;
                                newFrag.count = T.nItemNum;
                                newFrag.time = Util.StampToDateTime(CItemData.time);
                                player.FragmentList.Add(newFrag);

                            }
                            HeroRedPointManager.heroMergeDic.Clear();                           // 装备合成缓存清理
                        }
                        break;
                    case ItemType.soul:                                     // 3.魂石             
                        {
                            Debug.Log("魂石发生变化" + T.nItemID + "/" + T.nItemNum);
                            bool isFind = false;

                            for (int i = 0; i < player.GetHeroSoulList.Count; i++)              // 检索魂石列表
                            {
                                if(T.nItemID == player.GetHeroSoulList[i].ID)
                                {
                                    if(T.nItemNum == 0)
                                    {
                                        player.GetHeroSoulList.Remove(player.GetHeroSoulList[i]);
                                    }
                                    else
                                    {
                                        player.GetHeroSoulList[i].count = T.nItemNum;           // 更新魂石数量
                                    }
                                    isFind = true;
                                    break;
                                }
                            }
                            if (!isFind)                                                        // 添加新魂石
                            {
                                Soul newSoul = new Soul();
                                newSoul.ID = T.nItemID;
                                newSoul.count = T.nItemNum;
                                newSoul.time = Util.StampToDateTime(CItemData.time);
                                player.GetHeroSoulList.Add(newSoul);
                            }
                        }
                        break;
                    case ItemType.coinsprop:
                    case ItemType.heroExpProp:
                    case ItemType.medalExpProp:
                    case ItemType.wingExpProp:
                    case ItemType.ticket:

                    case ItemType.staminaProp:
                    case ItemType.protectedstone:
                    case ItemType.jinjiestone:
                    case ItemType.mercExpProp:

                    case ItemType.SkillProp:
                    case ItemType.soulbag:
                    case ItemType.diamondsbag:                              // 4.道具 (6-18)      
                        {
                            Debug.Log("道具发生变化:" + T.nItemID + "/" + T.nItemNum);
                            bool isFind = false;

                            for (int i = 0; i < player.PropList.Count; i++)                     // 检索道具列表
                            {
                                if(T.nItemID == player.PropList[i].ID)
                                {
                                    if(T.nItemNum == 0)
                                    {
                                        player.PropList.Remove(player.PropList[i]);             // 数量为0,移除道具
                                    }
                                    else
                                    {
                                        player.PropList[i].count = T.nItemNum;                  // 更新道具数量
                                    }
                                    isFind = true;
                                    break;
                                }
                            }

                            if(!isFind)                                                         // 添加新道具
                            {
                                Prop newProp = new Prop();
                                newProp.ID = T.nItemID;
                                newProp.count = T.nItemNum;
                                newProp.time = Util.StampToDateTime(CItemData.time);
                                player.PropList.Add(newProp);
                            }

                            dispatcher.Dispatch(EventSignal.UpdateInfo_Event);               // 更新信息

                            dispatcher.Dispatch(BagEvent.EventUpdateItem);                      // 更新道具事件
                            dispatcher.Dispatch(BagEvent.EventUse);                             // 更新使用事件
                        }
                        break;
                    case ItemType.wing:                                     // 5.翅膀             
                        {
                            Debug.Log("翅膀变化:" + T.nItemID +"/" + T.nItemNum);
                            bool isFind = false;

                            for (int i = 0; i < player.WingList.Keys.Count; i++)
                            {
                                if( player.WingList.ContainsKey(T.nItemID))
                                {
                                    if (T.nItemNum == 0)
                                    {
                                        player.WingList.Remove(T.nItemID);
                                    }
                                    else
                                    {
                                        player.WingList[T.nItemID] = T.nItemNum;
                                    }
                                    isFind = false;
                                    break;
                                }
                            }
                            if(!isFind)
                            {
                                player.WingList.Add(T.nItemID, T.nItemNum);
                            }
                        }
                        break;
                }
                itemCount += 1;
                if(itemCount == CItemData.total)
                {
                    Debug.Log("物品加载超出总数");
                    itemCount = 0;
                    break;
                }
            }
        }
    }
    public void ChangePlayerAttr(EventBase obj)                                                 // 玩家属性更新           
    {
        SYNC_PLAYER_ATTR PlayerAttr = (SYNC_PLAYER_ATTR)obj.eventValue;
        Debug.Log("玩家属性更新:");

        switch (PlayerAttr.playerAttrType)
        {
            case PLAYER_ATTR_TYPE.PlayerLv:                                                     // 1.玩家等级更新           
                {
                    Debug.Log("玩家等级更新:" + PlayerAttr.nValue);
                    player.PlayerLevel = PlayerAttr.nValue;
                    dispatcher.Dispatch(GlobalEvent.PlayerLvEvent);         // 玩家等级更新

                    HeroRedPointManager.heroMergeDic.Clear();               // 英雄装备合成缓存清理

                    // 玩家等级是否到配置等级 解锁对应翅膀
                    if (PlayerAttr.nValue >= CustomJsonUtil.GetValueToInt("WingUnlock") && player.PlayerRoleHero.WingID == 0)
                    {
                        switch (player.PlayerRoleHero.ID)
                        {
                            case 100001:
                                player.PlayerRoleHero.WingID = 1;
                                break;
                            case 10002:
                                player.PlayerRoleHero.WingID = 9;
                                break;
                            case 100003:
                                player.PlayerRoleHero.WingID = 17;
                                break;
                            default:
                                break;
                        }
                    }
                    //                  YiJieManager.Instance.SetRoleData(player.PlayerId.ToString(), player.PlayerName, player.PlayerLevel.ToString(), player.ServerID.ToString(), player.ServerName);
                    //                  YiJieManager.Instance.SetData("loginGameRole", player, game);////这个接口只有接uc的时候才会用到和setRoleData一样的功能，但是两个放在一起不会出现冲突,必接接口
                }
                break;
            case PLAYER_ATTR_TYPE.PlayerExp:                                                    // 2.玩家经验更新           
                {
                    Debug.Log("玩家经验更新:");
                    player.PlayerCurrentExp = PlayerAttr.nValue;
                    dispatcher.Dispatch(GlobalEvent.PlayerExpEvent);        // 玩家经验更新


                }
                break;
            case PLAYER_ATTR_TYPE.PlayerStamina:                                                // 3.玩家体力更新            
                {
                    Debug.Log("玩家体力更新:");
                    player.PlayerCurrentStamina = PlayerAttr.nValue;
                    dispatcher.Dispatch(EventSignal.UpdateInfo_Event);
                }
                break;
            case PLAYER_ATTR_TYPE.PlayerSkill:                                                  // 4.玩家技能点更新          
                {
                    Debug.Log("玩家技能点更新:");
                    player.SkillPoint = PlayerAttr.nValue;
                    dispatcher.Dispatch(GlobalEvent.PlayerSkillPointChange);                    // 玩家技能点变化_事件
                }
                break;
            case PLAYER_ATTR_TYPE.PlayerVIPLv:                                                  // 5.玩家VI经验等级更新       
                {
                    Debug.Log("玩家VIP等级更新");
                    int tempVIPLV = player.PlayerVIPLevel;
                    player.PlayerVIPExp = PlayerAttr.nValue;

                    if (tempVIPLV != player.PlayerVIPLevel)
                    {
                        PanelManager.sInstance.ShowNoticePanel(string.Format(Language.GetValue("VIPupgrade"), player.PlayerVIPLevel.ToString()), TipsType.Gold);
                    }
                    dispatcher.Dispatch(EventSignal.UpdateInfo_Event);
                }
                break;
        }
    }

 

    public void ChangeHeroLvExp (EventBase obj)                                                 // 英雄等级经验更新        
    {
        SYNC_HeroLvExp HeroLvExp = (SYNC_HeroLvExp)obj.eventValue;
        Debug.Log("英雄等级,经验发生变化:" + HeroLvExp.nHeroID);

        foreach (IHeroData T in player.HeroList)
        {
            if (T.ID == HeroLvExp.nHeroID)
            {
                T.HeroExp = HeroLvExp.nHeroExp;                                                 // 更新英雄经验
                bool isLeveUp = false;                                                          // 英雄是否升级
                if(T.HeroLevel != HeroLvExp.nHeroLv)    
                {
                    isLeveUp = true;
                }
                T.HeroLevel = HeroLvExp.nHeroLv;

                dispatcher.Dispatch(BagEvent.EventHeroExpCallback);                             // 英雄经验回调 事件
                if(isLeveUp)                                                                    // 英雄升级_战斗力变化
                {
                    FightForce newFightForce = new FightForce();
                    newFightForce.heroID = T.ID;
                    newFightForce.item = FightForceType.HeroUpLv;
                }

            }
        }
        HeroRedPointManager.heroMergeDic.Clear();                                               // 英雄穿戴装备合成清理
    }








    public void ReLogIn(EventBase obj)                                                          // 重新登录(本地触发了重新登录机制,先自动重连，连不上，再启动手动重新登录机制)
    {
        PanelManager.sInstance.ShowNoticePanel("服务器重新连接中......");
        dispatcher.Dispatch(StartEvent.LogIn_Event);                                            // 登录游戏
    }
    public void NetNotGood(EventBase obj)                                                       // 网络信号不稳定          
    {
        PanelManager.sInstance.ShowNoticePanel("网络信号不稳定!");
    }
    public void ErrorCode(EventBase obj)                                                        // 错误码                
    {
        ErrorCode ErrorCode = (ErrorCode)obj.eventValue;
        int newErrorCode = ErrorCode.code;

        PanelManager.sInstance.ShowNoticePanel("错误码:" + (eError)newErrorCode);
        Debuger.LogWarning("服务器返回错误码:" + newErrorCode);
        PanelManager.sInstance.HideLoadingPanel();

    }




}
