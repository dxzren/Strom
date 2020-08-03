using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public interface                    IHeroData
{
    int                             ServerID                        { set; get; }                                           // 服务器ID
    int                             ProLevel                        { set; get; }                                           // 职业等级(职业属性加成)
    int                             BattlePos                       { set; get; }                                           // 战斗阵容 位置
    long                            MercState                       { set; get; }                                           // 佣兵状态 (0:已经解锁,大于0为起始雇佣时间)

    float                           Blood                           { set; get; }                                           // 生命值
    float                           PhyAttack                       { set; get; }                                           // 物理攻击
    float                           MagicAttack                     { set; get; }                                           // 魔法攻击
    float                           PhyArmor                        { set; get; }                                           // 物理防御
    float                           MagicArmor                      { set; get; }                                           // 魔法防御

    float                           PhyCrit                         { set; get; }                                           // 物理暴击
    float                           MagicCrit                       { set; get; }                                           // 魔法暴击
    float                           ThroughPhyArmor                 { set; get; }                                           // 物理穿透
    float                           EnergyRegen                     { set; get; }                                           // 能量回复
    float                           BloodRegen                      { set; get; }                                           // 生命回复

    float                           SuckBlood                       { set; get; }                                           // 吸血
    float                           Hit                             { set; get; }                                           // 命中
    float                           Dodge                           { set; get; }                                           // 闪避
    float                           Power                           { set; get; }                                           // 力量
    float                           Agile                           { set; get; }                                           // 敏捷
    float                           Interllect                      { set; get; }                                           // 智力

    ObscuredInt                     ID                              { set; get; }                                           // 配置ID
    ObscuredInt                     HeroLevel                       { set; get; }                                           // 英雄等级
    ObscuredInt                     HeroStar                        { set; get; }                                           // 英雄星级
    ObscuredInt                     CurrentBlood                    { set; get; }                                           // 当前生命值
    ObscuredInt                     CurrentAnger                    { set; get; }                                           // 当前愤怒值

    ObscuredInt                     ProDataID                       { set; get; }                                           // 职业ID
    ObscuredInt                     HeroExp                         { set; get; }                                           // 英雄经验值
    ObscuredInt                     ResourceID                      { set; get; }                                           // 资源ID
    ObscuredInt                     WingID                          { set; get; }                                           // 翅膀ID
    ObscuredInt                     ActiveSkillLevel1               { set; get; }                                           // 主动技能1

    ObscuredInt                     ActiveSkillLevel2               { set; get; }                                           // 主动技能2
    ObscuredInt                     UltSkillLevel                   { set; get; }                                           // 大招技能
    ObscuredInt                     PassiveSkillLevel               { set; get; }                                           // 被动技能
    ObscuredInt                     FightForce                      { set; get; }                                           // 战斗力

    Dictionary<WearPosition,int >   EquipList                       { set; get; }                                           // 装备列表< 位置,配置ID >
    HeroQuality                     Quality                         { set; get; }                                           // 英雄品质
    Polarity                        HeroPolarity                    { set; get; }                                           // 英雄属性 (冰火雷)
    bool                            IsInBattle                      { set; get; }                                           // 是否上阵
    bool                            IsInDefance                     { set; get; }                                           // 是否在防御阵容

    PosNumType                      PosNum                          { set; get; }                                           // 成员固定站位
    Medal                           MedalObject                     { set; get; }                                           // 勋章对象
}
public class                        HeroData : IHeroData
{
    [Inject]
    public IPlayer                  Inplayer                        { get; set; }
    [Inject]
    public IGameData                InGame_D                        { get; set; }



    public int                      ServerID                                                                                // 服务器ID               
    {
        set { this._ServeID = value; }
        get { return this._ServeID; }
    }
    public int                      ProLevel                                                                                // 职业等级(不大于配置参数"OccupationalAdditionLevel") 
    {
        set { this._ProLevel = value > CustomJsonUtil.GetValueToInt("OccupationalAdditionLevel") ? CustomJsonUtil.GetValueToInt("OccupationlAdditionLevel"):value; }
        get { return this._ProLevel; }
    }
    public int                      BattlePos                                                                               // 战斗阵容位置             
    {
        set { _BattlePos = value; }         get { return _BattlePos; }
    }
    public long                     MercState                                                                               // 佣兵状态 (0:已经解锁,大于0为起始雇佣时间) 
    {
        set { this._MercState = value; }
        get { return this._MercState; }
    }

    public float                    Blood                                                                                   // 生命值                   
    {
        set { this._Blood = value; }
        get                                                                 // 生命值 = 1.属性加成(力,敏,智) + 2.翅膀加成 + 3.英雄品质 + 4.装备加成 + 5.职业等级加成
        {
            if(_Blood == 0)
            { _Blood = Configs_Hero.sInstance.GetHeroDataByHeroID(_ID).Blood; }
            int proType = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).Profession;
            float tempblood = this._Blood + Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 1)).Blood * this.Power +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 2)).Blood * this.Agile +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 3)).Blood * this.Interllect;
            if (_HeroLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1014).LimitLevel)
            {
                tempblood += Configs_Wing.sInstance.GetWingDataByWingNum(WingID) == null ? 0 : _Blood + 
                    (int)(_Blood * Configs_Wing.sInstance.GetWingDataByWingNum(WingID).Blood / 100);
            }
            return tempblood += QualityCalculator(4) + EquipAttribute(4) + Util.OccuAdd(
                    (Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Profession2 - 1) * 9, this.ProLevel, 4);
        }
    }
    public float                    PhyAttack                                                                               // 物理攻击                 
    {
        set { this._PhyAttack = value; }
        get
        {
            if(_PhyAttack == 0)
            {
                _PhyAttack = Configs_Hero.sInstance.GetHeroDataByHeroID(_ID).PhysicalAttack;
            }
            int proType = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).Profession;
            float tempPhyAttack = this._PhyAttack + Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 1)).PhysicalAttack * this.Power +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 2)).PhysicalAttack * this.Agile +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 3)).PhysicalAttack * this.Interllect;
            if (HeroLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1014).LimitLevel)
            {
                tempPhyAttack += Configs_Wing.sInstance.GetWingDataByWingNum(WingID) == null ? 0 :_PhyAttack +
                    (int)(_PhyAttack * Configs_Wing.sInstance.GetWingDataByWingNum(WingID).PhysicalAttack / 100);
            }
            return tempPhyAttack += QualityCalculator(5) + EquipAttribute(5) + Util.OccuAdd(
                (Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Profession2 - 1) * 9, this.ProLevel, 5);
        }
    }
    public float                    MagicAttack                                                                             // 魔法攻击                 
    {
        set { this._MagicAttack = value; }
        get
        {
            if(_MagicAttack == 0)
            {
                _MagicAttack = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).MagicAttack;
            }
            int proType = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).Profession;
            float tempMA = this._MagicAttack + Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 1)).MagicAttack * this.Power +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 2)).MagicAttack * this.Agile +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 3)).MagicAttack * this.Interllect;
            if(HeroLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1014).LimitLevel)
            {
                tempMA += Configs_Wing.sInstance.GetWingDataByWingNum(WingID) == null ? 0: _MagicAttack +
                    (int)(_MagicAttack * Configs_Wing.sInstance.GetWingDataByWingNum(WingID).MagicAttack / 100);
            }
            return tempMA += QualityCalculator(6) + EquipAttribute(6) + Util.OccuAdd(
                (Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Profession2 - 1) * 9, this.ProLevel, 6);
        }
    }
    public float                    PhyArmor                                                                                // 物理防御                 
    {
        set { this._PhyArmor = value; }
        get
        {
            if(this._PhyArmor == 0)
            {
                _PhyArmor = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).PhysicalArmor;
            }
            int proType = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Profession;
            float tempPA = this._PhyArmor + Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 1)).PhysicalArmor * this.Power +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 2)).PhysicalArmor * this.Agile +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 3)).PhysicalArmor * this.Interllect;
            if (HeroLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1014).LimitLevel)
            {
                tempPA += Configs_Wing.sInstance.GetWingDataByWingNum(WingID) == null ? 0 :_PhyArmor +
                    (int)(_PhyArmor * Configs_Wing.sInstance.GetWingDataByWingNum(WingID).PhysicalArmor / 100);
            }
            return tempPA += QualityCalculator(7) + EquipAttribute(7);
        }
    }
    public float                    MagicArmor                                                                              // 魔法防御                 
    {
        set { this._MagicArmor = value; }
        get
        {
            if(this._MagicArmor == 0)
            {
                _MagicArmor = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).MagicArmor;
            }
            int proType = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).Profession;
            float tempMA = this._MagicArmor + Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 1)).MagicArmor * this.Power +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 2)).MagicArmor * this.Agile +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 3)).MagicArmor * this.Interllect;
            if(HeroLevel >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1014).LimitLevel)
            {
                tempMA += Configs_Wing.sInstance.GetWingDataByWingNum(WingID) == null ? 0 : _MagicArmor +
                    (int)(_MagicArmor * Configs_Wing.sInstance.GetWingDataByWingNum(WingID).MagicArmor / 100);
            }
            return (float)Mathf.RoundToInt(tempMA * 100) / 100 + QualityCalculator(19) + EquipAttribute(19);
        }
    }

    public float                    PhyCrit                                                                                 // 物理暴击                  
    {
        set { this._PhyCrit = value; }
        get
        {
            if(_PhyCrit == 0)
            {
                _PhyCrit    = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).PhysicalCritical;
            }
            int     proType = Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).Profession;
            float   tempPC  = this._PhyCrit + Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 1)).PhysicalCritical * this.Power +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 2)).PhysicalCritical * this.Agile +
                Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(((proType - 1) * 3 + 3)).PhysicalCritical * this.Interllect;
            if (HeroLevel   >= Configs_SystemNavigation.sInstance.GetSystemNavigationDataBySystemID(1014).LimitLevel)
            {
                tempPC += Configs_Wing.sInstance.GetWingDataByWingNum(WingID) == null ? 0 : _PhyCrit +
                    (int)(_PhyCrit *Configs_Wing.sInstance.GetWingDataByWingNum(WingID).PhysicalArmor / 100);
            }
            return tempPC + QualityCalculator(11) + EquipAttribute(11) + Util.OccuAdd(
                (Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Profession2 - 1) * 9, this.ProLevel, 11);
        }
    }
    public float                    MagicCrit                                                                               // 魔法暴击                  
    {
        set { this._MagicCrit = value; }
        get
        {
            return this._MagicCrit + QualityCalculator(12) + EquipAttribute(12) + Util.OccuAdd(
                (Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).Profession2 - 1) * 9, this.ProLevel, 12);
        }
    }
    public float                    ThroughPhyArmor                                                                         // 物理穿透                  
    {
        set { this._ThroughPhyArmor = value; }
        get { return this._ThroughPhyArmor + QualityCalculator (13) + EquipAttribute(13) ; }
    }
    public float                    EnergyRegen                                                                             // 能量回复                  
    {
        set { this._EnergyRegen = value; }
        get { return this._EnergyRegen + QualityCalculator (14) + EquipAttribute(14); }
    }
    public float                    BloodRegen                                                                              // 生命回复                  
    {
        set { this._BloodRegen = value; }
        get { return this._BloodRegen + QualityCalculator (15) + EquipAttribute(15); }
    }

    public float                    SuckBlood                                                                               // 吸血                      
    {
        set { this._SuckBlood = value; }
        get { return this._SuckBlood + QualityCalculator (16) + EquipAttribute(16); }
    }
    public float                    Hit                                                                                     // 命中                      
    {
        set { this._Hit = value; }
        get { return this._Hit + QualityCalculator(17) + EquipAttribute(17) ; }
    }
    public float                    Dodge                                                                                   // 闪避                      
    {
        set { this._Dodge = value; }
        get { return this._Dodge + QualityCalculator(18) + EquipAttribute(18); }
    }
    public float                    Power                                                                                   // 力量                      
    {
        set { this._Power = value; }
        get
        {
            float tempPower = 0;
            int star = 0;

            switch (this.HeroStar)
            {
                case 1:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star1;
                    break;
                case 2:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star2;
                    break;
                case 3:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star3;
                    break;
                case 4:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star4;
                    break;
                case 5:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star5;
                    break;
                default:
                    break;
            }
            tempPower += QualityCalculator(1) + EquipAttribute(1);
            return tempPower += (this.HeroLevel - 1) * Configs_HeroStar.sInstance.GetHeroStarDataByStarID(star).StrengthGrowing;
        }
    }
    public float                    Agile                                                                                   // 敏捷                      
    {
        set { this._Agile = value; }
        get {
            float tempAgile = 0;
            int star = 0;

            switch (this.HeroStar)
            {
                case 1:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star1;
                    break;
                case 2:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star2;
                    break;
                case 3:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star3;
                    break;
                case 4:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star4;
                    break;
                case 5:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star5;
                    break;
                default:
                    break;
            }
            tempAgile += QualityCalculator(2) + EquipAttribute(2);
            return tempAgile += (this.HeroLevel - 1) * Configs_HeroStar.sInstance.GetHeroStarDataByStarID(star).AgilityGrowing;

        }
    }
    public float                    Interllect                                                                              // 智力                      
    {
        set { this._Interllect = value; }
        get
        {
            float tempInter = 0;
            int star = 0;

            switch (this.HeroStar)
            {
                case 1:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star1;
                    break;
                case 2:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star2;
                    break;
                case 3:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star3;
                    break;
                case 4:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star4;
                    break;
                case 5:
                    star = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Star5;
                    break;
                default:
                    break;
            }
            tempInter += QualityCalculator(3) + EquipAttribute(3);
            return tempInter += (this.HeroLevel - 1) * Configs_HeroStar.sInstance.GetHeroStarDataByStarID(star).MentalityGrowing;
        }
    }

    public ObscuredInt              ID                                                                                      // 配置ID                    
    {
        set { this._ID = value; }
        get { return this._ID; }
    }
    public ObscuredInt              HeroLevel                                                                               // 英雄等级                  
    {
        set { this._HeroLevel = value; }
        get { return this._HeroLevel; }
    }
    public ObscuredInt              HeroStar                                                                                // 英雄星级                  
    {
        set { this._HeroStar = value; }
        get
        {
            if(Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).HeroType == 4)
            {
                return Configs_MercenaryData.sInstance.GetMercenaryDataDataByMercenaryID(this.ID).MercenaryStar;
            }
            if(_HeroStar == 0)
            {
                _HeroStar = Configs_Hero.sInstance.GetHeroDataByHeroID(_ID).InitialStar;
            }
            return this._HeroStar;
        }
    }
    public ObscuredInt              CurrentBlood                                                                            // 当前血量                  
    {
        set { this._CurrentBlood = value; }
        get { return this._CurrentBlood; }
    }
    public ObscuredInt              CurrentAnger                                                                            // 当前怒气值                
    {
        set { this._CurrentAnger = value; }
        get { return this._CurrentAnger; }
    }

    public ObscuredInt              ProDataID                                                                               // 职业加成配置ID            
    {
        set { this._ProDataID = value; }
        get { return (Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).Profession2 -1) * 9 + ProLevel; }
    }
    public ObscuredInt              HeroExp                                                                                 // 英雄经验                  
    {
        set { this._HeroExp = value; }
        get { return this._HeroExp; }
    }
    public ObscuredInt              ResourceID                                                                              // 资源ID                    
    {
        set { this._ResourceID = value; }
        get { return this._ResourceID; }
    }
    public ObscuredInt              WingID                                                                                  // 翅膀ID                    
    {
        set { this._WindID = value; }
        get { return this._WindID; }
    }
    public ObscuredInt              ActiveSkillLevel1                                                                       // 主动技能1                  
    {
        set { this._ActiveSkillLevel1 = value; }
        get
        {
            if (Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).HeroType == 4)
            {
                return this.HeroLevel;
            }
            return this._ActiveSkillLevel1;
        }

    }

    public ObscuredInt              ActiveSkillLevel2                                                                       // 主动技能2                  
    {
        set { this._ActiveSkillLevel2 = value; }
        get
        {
            if(Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).HeroType == 4)
            {
                return this.HeroLevel;
            }
            return this._ActiveSkillLevel2;
        }
    }
    public ObscuredInt              UltSkillLevel                                                                           // 大招技能                   
    {
        set { this._UltSkillLevel = value; }
        get
        {
            if (Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).HeroType == 4)
            {
                return this.HeroLevel;
            }
            return _UltSkillLevel;
        }
    }
    public ObscuredInt              PassiveSkillLevel                                                                       // 被动技能                   
    {
        set { this._PassiveSkillLevel = value; }
        get
        {
            if(Configs_Hero.sInstance.GetHeroDataByHeroID(this.ID).HeroType == 4)
            {
                return this.HeroLevel;
            }
            return this._PassiveSkillLevel;
        }
    }
    public ObscuredInt              FightForce                                                                              // 战斗力                     
    {
        set { this._FightForce = value; }
        get
        {
            float FF =
                this.Blood * Configs_Power.sInstance.GetPowerDataByAttribute(1).PowerNum +
                this.PhyAttack * Configs_Power.sInstance.GetPowerDataByAttribute(2).PowerNum +
                this.MagicAttack * Configs_Power.sInstance.GetPowerDataByAttribute(3).PowerNum +
                this.PhyArmor * Configs_Power.sInstance.GetPowerDataByAttribute(4).PowerNum +
                this.PhyCrit * Configs_Power.sInstance.GetPowerDataByAttribute(8).PowerNum +

                this.MagicCrit * Configs_Power.sInstance.GetPowerDataByAttribute(9).PowerNum +
                this.ThroughPhyArmor * Configs_Power.sInstance.GetPowerDataByAttribute(10).PowerNum +
                this.EnergyRegen * Configs_Power.sInstance.GetPowerDataByAttribute(11).PowerNum +
                this.BloodRegen * Configs_Power.sInstance.GetPowerDataByAttribute(12).PowerNum +
                this.SuckBlood * Configs_Power.sInstance.GetPowerDataByAttribute(13).PowerNum +

                this.Hit * Configs_Power.sInstance.GetPowerDataByAttribute(14).PowerNum +
                this.Dodge * Configs_Power.sInstance.GetPowerDataByAttribute(15).PowerNum +
                this.MagicArmor * Configs_Power.sInstance.GetPowerDataByAttribute(16).PowerNum;
            return Mathf.RoundToInt(FF + this.ActiveSkillLevel1 * 4 + this.ActiveSkillLevel2 * 4 +
                this.UltSkillLevel * 4 + this.UltSkillLevel * 4);
        }      
    }

    public PosNumType               PosNum                                                                        // 固定战斗位置数据            
    {   set { _FixedBattlePos_D = value; }                          get { return _FixedBattlePos_D; } }
    public Medal                            MedalObject                                                                     // 勋章对象                   
    {
        set { _MedalObject = value; }
        get { return _MedalObject; }
    }

    public Dictionary<WearPosition, int>    EquipList                                                                       // 装备列表                   
    {
        set { this._EquipList = value; }
        get { return this._EquipList; }
    }
    public Polarity                         HeroPolarity            { set { _HeroPolarity = value; }    get { return _HeroPolarity; } }         // 英雄属性类型

    public HeroQuality              Quality                                                                                 // 英雄品质                    
    {
        set { this._Quality = value; }
        get
        {
            if(Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID).HeroType == 4)
            {
                return (HeroQuality)Configs_MercenaryData.sInstance.GetMercenaryDataDataByMercenaryID(this.ID).MercenaryQuality;
            }
            return this._Quality;
        }
    }

    public bool                     IsInBattle                                                                              // 是否已上阵                  
    {
        set { this._IsInBattle = value; }
        get { return this._IsInBattle; }
    }
    public bool                     IsInDefance                                                                             // 是否在防御阵容              
    {
        set { this.IsInDefance = value; }
        get { return this.IsInDefance; }
    }

    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
    private int                     _ServeID                            = 0;
    private int                     _ProLevel                           = 0;
    private int                     _BattlePos                          = 0;
    private long                    _MercState                          = 0;

    private float                   _Blood                              = 0;
    private float                   _PhyAttack                          = 0;
    private float                   _MagicAttack                        = 0;
    private float                   _PhyArmor                           = 0;
    private float                   _MagicArmor                         = 0;

    private float                   _PhyCrit                            = 0;
    private float                   _MagicCrit                          = 0;
    private float                   _ThroughPhyArmor                    = 0;
    private float                   _EnergyRegen                        = 0;
    private float                   _BloodRegen                         = 0;

    private float                   _SuckBlood                          = 0;
    private float                   _Hit                                = 0;
    private float                   _Dodge                              = 0;
    private float                   _Power                              = 0;
    private float                   _Agile                              = 0;
    private float                   _Interllect                         = 0;

    private int                     _PlayerLv                           = 0;
    private int                     _ID                                 = 0;
    private int                     _HeroLevel                          = 0;
    private int                     _HeroStar                           = 0;
    private int                     _CurrentBlood                       = 100;
    private int                     _CurrentAnger                       = 100;

    private int                     _ProDataID                          = 0;
    private int                     _HeroExp                            = 0;
    private int                     _ResourceID                         = 0;
    private int                     _WindID                             = 0;

    private int                             _ActiveSkillLevel1          = 1;
    private int                             _ActiveSkillLevel2          = 1;
    private int                             _UltSkillLevel              = 1;
    private int                             _PassiveSkillLevel          = 1;
    private int                             _FightForce                 = 0;

    private PosNumType                      _FixedBattlePos_D           = PosNumType.Null;
    private Medal                           _MedalObject                = new Medal();
    private Dictionary<WearPosition, int>   _EquipList                  = new Dictionary<WearPosition, int>();
    private Polarity                        _HeroPolarity               = Polarity.Ice;
    private HeroQuality                     _Quality                    = HeroQuality.White;
    private bool                            _IsInBattle                 = false;
    private bool                            _IsInDefance                = false;

                                                                                                                            ///<| Private Func: 私有方法 | >
    private int                     QualityCalculator (int num)                                                             // 英雄品质对于属性的影响换算方法(属性基础值:未转换)   
    {
        Configs_NPCQualityData NPCData = new Configs_NPCQualityData();
        Configs_HeroData HeroData = Configs_Hero.sInstance.GetHeroDataByHeroID(this._ID);
        switch(this.Quality)                                                            
        {
            case HeroQuality.White:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.White);
                break;
            case HeroQuality.Green:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Green);
                break;
            case HeroQuality.Green1:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Green1);
                break;
            case HeroQuality.Blue:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Blue);
                break;
            case HeroQuality.Blue1:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Blue1);
                break;
            case HeroQuality.Blue2:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Blue2);
                break;
            case HeroQuality.Purple:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Purple);
                break;
            case HeroQuality.Purple1:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Purple1);
                break;
            case HeroQuality.Purple2:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Purple2);
                break;
            case HeroQuality.Purple3:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Purple3);
                break;
            case HeroQuality.Gold:
                NPCData = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(HeroData.Gold);
                break;
            default:
                break;
        }
        switch(num)                                                                     
        {
            case 1:
                return NPCData.Strength;
            case 2:
                return NPCData.Agility;
            case 3:
                return NPCData.Mentality;
            case 4:
                return NPCData.Blood;
            case 5:
                return NPCData.PhysicalAttack;
            case 6:
                return NPCData.MagicAttack;
            case 7:
                return NPCData.PhysicalArmor;
            //case 8:
            //    return Data.magicarmor;
            //case 9:
            //    return Data.IceMagicArmor;
            //case 10:
            //    return Data.ThunderMagiceArmor;
            case 11:
                return NPCData.PhysicalCrit;
            case 12:
                return NPCData.MagicCrit;
            case 13:
                return NPCData.PenetratePhysicalArmor;
            case 14:
                return NPCData.EnergyRegen;
            case 15:
                return NPCData.BloodRegen;
            case 16:
                return NPCData.SuckBlood;
            case 17:
                return NPCData.Hit;
            case 18:
                return NPCData.Dodge;
            case 19:
                return NPCData.MagicArmor;
            default:
                Debuger.Log("品质相关属性调整值没有找到");
                return 0;

        }
    }
    private int                     EquipAttribute (int num)                                                                // 英雄身上装备提供某项属性值计算    
    {
        int EA = 0;
        foreach(KeyValuePair<WearPosition, int> item in this.EquipList)
        {
            Configs_EquipData EData = Configs_Equip.sInstance.GetEquipDataByEquipID(item.Value);
            if(EData == null)
            {
                continue;
            }
            else if (num == EData.Attribute1)
            {
                EA += EData.AttributeValue1;
            }
            else if (num == EData.Attribute2)
            {
                EA += EData.AttributeValue2;
            }
            else if (num == EData.Attribute3)
            {
                EA += EData.AttributeValue3;
            }
            else if (num == EData.Attribute4)
            {
                EA += EData.AttributeValue4;
            }
            else if (num == EData.Attribute5)
            {
                EA += EData.AttributeValue5;
            }
            else if (num == EData.Attribute6)
            {
                EA += EData.AttributeValue6;
            }
        }
        return EA;
    }
    #endregion
}
