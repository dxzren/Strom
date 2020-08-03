using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroControll
{
    private static Dictionary<int, int>         CurrentMergeItemDic     = new Dictionary<int, int>();                       /// 单一合成的装备所消耗的资源
    private static Dictionary<string, UIAtlas>  Atlas                   = new Dictionary<string, UIAtlas>();                /// 图集字典

    public static  UIAtlas          GetGameAtlas(string key)                                                                // 获取图集                      
    {
        if (!Atlas.ContainsKey(key))
        {
            return Atlas[key];
        }
        else
        {
            Util.ErrLog("Not found Atlas !" + key);
        }
        return null;
    }
    private static bool             CheckBag                ( IPlayer  player,int equipID, IHeroData heroData)              // 检查背包装备                  
    {
        Configs_EquipData           CFEquipData                         = Configs_Equip.sInstance.GetEquipDataByEquipID(equipID);

        foreach(Equip equip in player.EquipList)
        {
            if(equip.ID == equipID)
            {
                if(CFEquipData.DressLev  <= heroData.HeroLevel)
                {   return true;    }

                else
                {    return false;  }
            }
        }
        return false;
    }                                                                                           
    private static bool             GetMergeItem            ( int ID,  ref int needCoins,Dictionary<int,int> CurrentMergeItemDic,IPlayer player) // 是否可合成
    {
        bool                        noHas                               = false;
        switch ((ItemType)Util.FindItemTypeFromID(ID, 0))
        {
            case ItemType.equip:
            case ItemType.scroll:               
                {
                    foreach(Equip equip in player.EquipList)        
                    {
                        if(equip.ID == ID)
                        {
                            if(CurrentMergeItemDic.ContainsKey(ID))
                            {
                                if((1 + CurrentMergeItemDic[ID]) <= equip.count)
                                {
                                    CurrentMergeItemDic[ID] += 1;
                                    noHas = true;
                                    return true;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if( 1 <= equip.count)
                                {
                                    CurrentMergeItemDic.Add(ID, 1);
                                    noHas = true;
                                    return true;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }

                    if(!noHas)                                              // 背包没有这件装备或碎片
                    {
                        List<int> MaterialList = new List<int>();

                        if(Configs_Equip.sInstance.GetEquipDataByEquipID(ID).CompoundType == 1)
                        {
                            return false;
                        }
                        if(Configs_Equip.sInstance.GetEquipDataByEquipID(ID).CompoundType == 2)                     // 装备合成
                        {
                            MaterialList = Configs_Equip.sInstance.GetEquipDataByEquipID(ID).Material;
                        }
                        else if(Configs_Equip .sInstance.GetEquipDataByEquipID(ID).CompoundType == 3)               // 碎片合成
                        {
                            MaterialList = new List<int>();
                            MaterialList.Add(Configs_Equip.sInstance.GetEquipDataByEquipID(ID).FragmentID);
                        }
                        if (MaterialList.Count == 0)
                        {
                            return false;
                        }

                        foreach (int myID in MaterialList)
                        {
                            if(myID == 0 )
                            {
                                return false;
                            }
                            if(!GetMergeItem(myID, ref needCoins,CurrentMergeItemDic,player))
                            {
                                return false;
                            }
                        }
                        needCoins += Configs_Equip.sInstance.GetEquipDataByEquipID(ID).Cost;
                    }
                }
                break;
            case ItemType.equipFragment:
            case ItemType.scrollFragment:       
                {
                    int needCount = Configs_Fragment.sInstance.GetFragmentDataByFragmentID(ID).Num;
                    foreach(Fragment frag in player.FragmentList)
                    {
                        if(frag.ID == ID)
                        {
                            if(CurrentMergeItemDic.ContainsKey(ID))
                            {
                                if(frag.count >= (needCount + CurrentMergeItemDic[ID]))
                                {
                                    CurrentMergeItemDic[ID] += needCount;
                                    needCoins += Configs_Fragment.sInstance.GetFragmentDataByFragmentID(ID).Cost;
                                    noHas = true;
                                    return true;
                                }
                                else
                                {   break;  }
                            }
                            else
                            {
                                if(frag.count >= needCount)
                                {
                                    CurrentMergeItemDic.Add(ID, needCount);
                                    noHas = true;
                                    return true;
                                }
                                else
                                {   break;  }
                            }
                        }
                    }
                    if(!noHas)
                    {
                        return false;
                    }
                }
                break;
        }
        return true;
    }
    public static int               OccuAdd                 ( int num, int proLevel, int type)                              // 计算职业等级增加的属性值       
    {
        Configs_OccupationalAdditionData data = Configs_OccupationalAddition.sInstance.GetOccupationalAdditionDataByID(num + proLevel);
        if (proLevel == 0)                                  return 0;
        if (data     == null)                               return 0;
        if (type     == data.Strength)                      return data.Agility;
        else                                                return 0;
    }
    public static int               FindItemTypeFromID      ( int ID,  int type)                                            // 根据物品ID返回物品类型         
    {
        if (ID >= 60001 && ID <= 69999)
        {
            return (int)ItemType.equip;
        }
        else if (ID >= 70001 && ID <= 79999)
        {
            return (int)ItemType.equipFragment;
        }
        else if (ID >= 90001 && ID <= 99999)
        {
            return (int)ItemType.soul;
        }
        else if (ID >= 10001 && ID <= 19999)
        {
            return (int)ItemType.coinsprop;
        }
        else if (ID >= 1 && ID <= 9999)
        {
            return (int)ItemType.wing;
        }
        else if (ID >= 100000 && ID <= 200000)
        {
            return (int)ItemType.hero;
        }
        return type;
    }

    public static string            GetPolarityName         ( Polarity inPolarity)                                          // 获取元素相性 图集子名          
    {
        switch(inPolarity)
        {
            case Polarity.Ice:      return MainUIStrConst.HeroPolarity_Ice;
            case Polarity.Fire:     return MainUIStrConst.HeroPolarity_Fire;
            case Polarity.Thunder:  return MainUIStrConst.HeroPolarity_Thunder;
            default:                return MainUIStrConst.HeroPolarity_Ice;
        }
    }
    public static string            GetHeroFrameName        ( HeroQuality inQuality)                                        // 获取角色框图集子名             
    {
       switch(inQuality)
        {
            case HeroQuality.White:                         return MainUIStrConst.HeroFrameQuality_White;
            case HeroQuality.Green:                         return MainUIStrConst.HeroFrameQuality_Green;
            case HeroQuality.Green1:                        return MainUIStrConst.HeroFrameQuality_Green1;
            case HeroQuality.Blue:                          return MainUIStrConst.HeroFrameQuality_Blue;
            case HeroQuality.Blue1:                         return MainUIStrConst.HeroFrameQuality_Blue1;
            case HeroQuality.Blue2:                         return MainUIStrConst.HeroFrameQuality_Blue2;
            case HeroQuality.Purple:                        return MainUIStrConst.HeroFrameQuality_Purple;
            case HeroQuality.Purple1:                       return MainUIStrConst.HeroFrameQuality_Purple1;
            case HeroQuality.Purple2:                       return MainUIStrConst.HeroFrameQuality_Purple2;
            case HeroQuality.Purple3:                       return MainUIStrConst.HeroFrameQuality_Purple3;
            case HeroQuality.Gold:                          return MainUIStrConst.HeroFrameQuality_Gold;
            default:
                Debug.LogWarning("边框数据错误!");           return MainUIStrConst.HeroFrameQuality_White;
        }      
    }
    public static string            GetHeroAttribTypeName   ( HeroAttribType_Main heroAttribType)                           // 获取英雄属性名称(主属性)        
    {
        switch(heroAttribType)
        {
            case HeroAttribType_Main.Blood:                 return MainUIStrConst.HeroAtt_Hp;
            case HeroAttribType_Main.PhyAttack:             return MainUIStrConst.HeroAtt_PhyAttack;
            case HeroAttribType_Main.MagicAttack:           return MainUIStrConst.HeroAtt_MagicAttack;
            case HeroAttribType_Main.PhyArmor:              return MainUIStrConst.HeroAtt_PhyArmor;
            case HeroAttribType_Main.MagicArmor:            return MainUIStrConst.HeroAtt_MagicArmor;
            case HeroAttribType_Main.PhyCrit:               return MainUIStrConst.HeroAtt_PhyCrit;
            case HeroAttribType_Main.MagicCrit:             return MainUIStrConst.HeroAtt_MagicCrit;
            case HeroAttribType_Main.ThroughPhyArmor:       return MainUIStrConst.HeroAtt_ThroughPhyArmor;

            case HeroAttribType_Main.EnergyRegen:           return MainUIStrConst.HeroAtt_EnergyRegen;
            case HeroAttribType_Main.BloodRegen:            return MainUIStrConst.HeroAtt_BloodRegen;
            case HeroAttribType_Main.SuckBlood:             return MainUIStrConst.HeroAtt_SuckBlood;
            case HeroAttribType_Main.Hit:                   return MainUIStrConst.HeroAtt_Hit;
            case HeroAttribType_Main.Dodge:                 return MainUIStrConst.HeroAtt_Dodge;
        }
        return "Error! 未知属性: (GetHeroAttribTypeName())";
    }
    public static string            GetHeroAttribTypeName   ( HeroAttribType_Lv1 heroAttribType)                            // 获取英雄属性名称(1级属性)       
    {
        switch(heroAttribType)
        {
            case HeroAttribType_Lv1.Power:      return MainUIStrConst.HeroAtt_Power;
            case HeroAttribType_Lv1.Agile:      return MainUIStrConst.HeroAtt_Agile;
            case HeroAttribType_Lv1.Intellect:  return MainUIStrConst.HeroAtt_Intellect;

        }
        return "Error! 未知属性: (GetHeroAttribTypeName())";
    }
  

    public static bool              IsCanWearEquip          ( IHeroData heroData,IPlayer player)                            // 是否有可穿戴装备                
    {
        int heroQualityID = 0;
        Configs_HeroQualityData CFHQualityData = Configs_HeroQuality.sInstance.GetHeroQualityDataByQualityID(heroQualityID);

        bool leftTopWear     = false;
        bool rightTopWear    = false;
        bool leftMidWear     = false;
        bool rightMidWear    = false;
        bool leftBottomWear  = false;
        bool rightBottomWear = false;

        switch(heroData.Quality)                                
        {
            case HeroQuality.White:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).White;
                break;
            case HeroQuality.Green:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Green;
                break;
            case HeroQuality.Green1:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Green1;
                break;
            case HeroQuality.Blue:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Blue;
                break;
            case HeroQuality.Blue1:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Blue1;
                break;
            case HeroQuality.Blue2:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Blue2;
                break;
            case HeroQuality.Purple:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple;
                break;
            case HeroQuality.Purple1:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple1;
                break;
            case HeroQuality.Purple2:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple2;
                break;
            case HeroQuality.Purple3:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple3;
                break;
            case HeroQuality.Gold:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Gold;
                break;

        }

        foreach(WearPosition position in heroData.EquipList.Keys)
        {
            if(position == WearPosition.LeftTop)
            {   leftTopWear = true;  }

            if(position == WearPosition.RightTop)
            {   rightTopWear = true; }

            if(position == WearPosition.LeftMid)
            {   leftMidWear = true;  }

            if(position == WearPosition.RightMid)
            {   rightMidWear = true; }

            if(position == WearPosition.LeftBottom)
            {   leftBottomWear = true; }

            if(position == WearPosition.RightBottom)
            {   rightBottomWear = true; }
        }

        if (!leftTopWear)
        {   return CheckBag(player, CFHQualityData.Equip1, heroData);   }

        if (!rightTopWear)
        {   return CheckBag(player, CFHQualityData.Equip2, heroData);   }

        if (!leftMidWear)
        {   return CheckBag(player, CFHQualityData.Equip3, heroData);   }

        if (!rightMidWear)
        {   return CheckBag(player, CFHQualityData.Equip4, heroData);   }

        if (!leftBottomWear)
        {   return CheckBag(player, CFHQualityData.Equip5, heroData);   }

        if (!rightBottomWear)
        {   return CheckBag(player, CFHQualityData.Equip6, heroData);   }

        return false;
    }
    public static bool              IsFullToMerge           ( int ID,IHeroData  hero,IPlayer player)                        // 合成材料是否足够(参考HeroInfoPanelView.IsFullToMerg方法改写)
    {
        int currentCoins = 0;
        CurrentMergeItemDic.Clear();
        bool isNotFull = GetMergeItem(ID, ref currentCoins, CurrentMergeItemDic, player);

        if(!(isNotFull && Configs_Equip.sInstance.GetEquipDataByEquipID(ID).DressLev  <= hero.HeroLevel))
        {
            return false;
        }
        if(player.PlayerCoins < currentCoins)
        {
            return false;
        }
        return true;
    }
    public static bool              IsCanStarUp             ( IHeroData heroData,IPlayer player)                            // 是否可以升星                    
    {
        int tagID = 0;
        int myStarCount = 0;
        int mySoulCount = 0;
        bool isUpStar = true;

        if (heroData.HeroStar == 1)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar2SoulNum");
        }
        else if (heroData.HeroStar == 2)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar3SoulNum");
        }
        else if (heroData.HeroStar == 3)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar4SoulNum");
        }
        else if (heroData.HeroStar == 4)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar5SoulNum");
        }
        else if(heroData.HeroStar == 5)
        {
            isUpStar = false;
            return isUpStar;
        }

        foreach (int soulID in Configs_Soul.sInstance.mSoulDatas.Keys)
        {
            Configs_SoulData CFSoulData = Configs_Soul.sInstance.mSoulDatas[soulID];
            if(CFSoulData.Target == heroData.ID)
            {
                tagID = soulID;
                break;
            }
        }
            
        foreach(Soul soul in player.GetHeroSoulList)
        {
            if(tagID == soul.ID)
            {
                mySoulCount = soul.count;
                break;
            }
        }
        if(mySoulCount >= myStarCount)
        {
            isUpStar = true;
        }
        else
        {
            isUpStar = false;
        }
        return isUpStar;
    }

    public static void              SetHeroNameDefine       ( UILabel lable,HeroQuality heroQ,Configs_HeroData heroData)    // 显示英雄名字 - 不改变字体颜色     
    {
        switch(heroQ)
        {
            case HeroQuality.White:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Green:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Green1:
                lable.text = Language.GetValue(heroData.HeroName) + "+1";
                break;
            case HeroQuality.Blue:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Blue1:
                lable.text = Language.GetValue(heroData.HeroName) + "+1";
                break;
            case HeroQuality.Blue2:
                lable.text = Language.GetValue(heroData.HeroName) + "+2";
                break;
            case HeroQuality.Purple:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Purple1:
                lable.text = Language.GetValue(heroData.HeroName) + "+1";
                break;
            case HeroQuality.Purple2:
                lable.text = Language.GetValue(heroData.HeroName) + "+2";
                break;
            case HeroQuality.Purple3:
                lable.text = Language.GetValue(heroData.HeroName) + "+3";
                break;
            case HeroQuality.Gold:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
        }
    }
    public static void              HeroListSort            ( List<IHeroData> inHeroD_List)                                 // 英雄数据列表 排序(星级>品质>等级) 
    {
        IHeroData                   TheHeroD;                                                   // 英雄数据变量

        for (int i = 0; i < inHeroD_List.Count; i++ )
        {

            for ( int j = i + 1; j < inHeroD_List.Count ; j++ )
            {
                if      ( inHeroD_List[i].HeroStar < inHeroD_List[j].HeroStar )                 // 比较 英雄星级.1
                {
                    TheHeroD            = inHeroD_List[i];
                    inHeroD_List[i]     = inHeroD_List[j];
                    inHeroD_List[j]     = TheHeroD;
                }
                else if ( inHeroD_List[i].HeroStar == inHeroD_List[j].HeroStar ) 
                {
                    if ( inHeroD_List[i].Quality < inHeroD_List[j].Quality)                     // 比较 英雄品质.2
                    {
                        TheHeroD            = inHeroD_List[i];
                        inHeroD_List[i]     = inHeroD_List[j];
                        inHeroD_List[j]     = TheHeroD;
                    }
                    else if ( inHeroD_List[i].Quality == inHeroD_List[j].Quality ) 
                    {
                        if (inHeroD_List[i].HeroLevel < inHeroD_List[j].HeroLevel)              // 比较 英雄等级.3
                        {
                            TheHeroD            = inHeroD_List[i];
                            inHeroD_List[i]     = inHeroD_List[j];
                            inHeroD_List[j]     = TheHeroD;
                        }
                    }
                }
            }
        }
        TheHeroD                    = null;
    }
}