using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroMerge
{
    public int equipID;
    public EquipButtonState type;
}

public class HeroRedPointManager
{
    static HeroRedPointManager _sInstance;
    public static HeroRedPointManager sInstance                                                 // 实例                         
    {
        get
        {
            if(_sInstance == null)
            {
                _sInstance = new HeroRedPointManager();

            }
            return _sInstance;
        }

    }

    public static Dictionary<int, Dictionary<WearPosition, HeroMerge>> heroMergeDic = new Dictionary<int, Dictionary<WearPosition, HeroMerge>>();
    public static Dictionary<int, bool> heroRedDic = new Dictionary<int, bool>();

    public bool IsCanHaveHero(IHeroSysData heroSys,IPlayer player)                                  // 是否有可召唤英雄             
    {
        foreach (Configs_HeroData heroConfig in heroSys.GetNotHaveHeroList(player))
        {


            foreach (Soul soul in player.GetHeroSoulList)
            {
                if (soul.ID == heroConfig.SoulID)
                {
                    if (soul.count >= Configs_Soul.sInstance.GetSoulDataBySoulID(soul.ID).Num)   // 可召唤
                    { return true; }
                }
            }
        }
        return false;
    }
    public bool CheckHero(IPlayer player,IHeroData heroData)                                    // 检测某个红点英雄             
    {
        int maxEquipID = 0;
        Configs_HeroData CFHeroData = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID);
        List<int> MaxEquipList = new List<int>();                           // 该英雄所能装备的装备ID
        HeroQuality HQuality = heroData.Quality;

        switch(HQuality)                                                    // 当前英雄品质 装备品质的最高等级       
        {
            case HeroQuality.White:
                maxEquipID = CFHeroData.White;
                break;
            case HeroQuality.Green:
                maxEquipID = CFHeroData.Green;
                break;
            case HeroQuality.Green1:
                maxEquipID = CFHeroData.Green1;
                break;
            case HeroQuality.Blue:
                maxEquipID = CFHeroData.Blue;
                break;
            case HeroQuality.Blue1:
                maxEquipID = CFHeroData.Blue1;
                break;
            case HeroQuality.Blue2:
                maxEquipID = CFHeroData.Blue2;
                break;
            case HeroQuality.Purple:
                maxEquipID = CFHeroData.Purple;
                break;
            case HeroQuality.Purple1:
                maxEquipID = CFHeroData.Purple1;
                break;
            case HeroQuality.Purple2:
                maxEquipID = CFHeroData.Purple2;
                break;
            case HeroQuality.Purple3:
                maxEquipID = CFHeroData.Purple3;
                break;
            case HeroQuality.Gold:
                maxEquipID = CFHeroData.Gold;
                break;

        }
        if (maxEquipID <= 0)                                                                                         
        {
            Debuger.Log("未找到指定英雄" + heroData.ID + "品质装备数据" + HQuality);
        }
        if (maxEquipID > 0)
        {
            Configs_HeroQualityData CFHeroQualityData = Configs_HeroQuality.sInstance.GetHeroQualityDataByQualityID(maxEquipID);
            if(CFHeroQualityData == null)
            {
                Debuger.Log("HeroQuality 数据未找到:" + maxEquipID);
                return false;
            }
            MaxEquipList.Add(CFHeroQualityData.Equip1);
            MaxEquipList.Add(CFHeroQualityData.Equip2);
            MaxEquipList.Add(CFHeroQualityData.Equip3);
            MaxEquipList.Add(CFHeroQualityData.Equip4);
            MaxEquipList.Add(CFHeroQualityData.Equip5);
            MaxEquipList.Add(CFHeroQualityData.Equip6);
        }
        foreach(WearPosition equipKey in heroData.EquipList.Keys)
        {
            foreach(int equipID in MaxEquipList)
            {
                if(equipID == heroData.EquipList[equipKey])
                {
                    MaxEquipList.Remove(equipID);
                    break;
                }
            }
        }
        if (MaxEquipList.Count <= 0)                                        // (此时MaxEquipList存着未穿上装备ID)_ 英雄装备全部穿上
        {   return false;    }
        for(int i = 0;i <MaxEquipList.Count;i++)
        {
            bool theRes = CheckBag(player, MaxEquipList[i], heroData);
            if(theRes)
            {   return true; }
        }
        return false;
    }
    bool CheckBag(IPlayer player,int equipID,IHeroData heroData)                                // 检查背包                     
    {
        Configs_EquipData CFEquipData = Configs_Equip.sInstance.GetEquipDataByEquipID(equipID);

        foreach(Equip equip in player.EquipList)
        {
            if(equip.ID == equipID)
            {
                if(CFEquipData.DressLev <= heroData.HeroLevel)
                {   return true;    }
                else
                {
                    if(Util.IsCanWearEquip(heroData,player))
                    {   return true;     }
                    else
                    {   return false;    }
                }
            }
        }

        switch(CFEquipData.CompoundType)
        {
            case 1:                                                         // 不可合成
                if(Util.IsCanWearEquip(heroData,player))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:                                                         // 装备合成
            case 3:                                                         
                {
                    bool res1 = Util.IsFullToMerge(equipID, heroData, player);
                    if (res1)
                    {
                        return res1;
                    }
                    else
                    {
                        if(Util.IsCanWearEquip(heroData,player))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
        }
        if(Util.IsCanStarUp(heroData,player))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
