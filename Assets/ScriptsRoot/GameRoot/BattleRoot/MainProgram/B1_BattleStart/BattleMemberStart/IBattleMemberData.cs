using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using LinqTools;
using LitJson;
using MiniJSON;
using CodeStage.AntiCheat.ObscuredTypes;
///---------------------------------------------------------------------------------------------/// <summary>   战斗成员数据 接口</summary>
public interface                IBattleMemberData                                               
{
    bool                        isRoleHero                      { set; get; }                   /// 是否主角英雄
    int                         memberQuality                   { set; get; }                   /// 成员品质
    int                         memberQualityInit               { set; get; }                   /// 成员初始化品质
    int                         memberStarLv                    { set; get; }                   /// 成员星级
    int                         MemberProAtt                    { set; get; }                   /// 成员职业属性

    int                         MemberID                        { set; get; }                   /// 成员ID
    int                         MemberResID                     { set; get; }                   /// 资源ID

    string                      memberName                      { set; get; }                   /// 成员名称
    string                      HeadIconSpriteName              { set; get; }                   /// 成员头像 图集子名

    Battle_MemberType           MemberType                      { set; get; }                   /// 成员类型
    Battle_Camp                 BattleCamp                      { set; get; }                   /// 成员阵营(我:1 敌:2)

    PosNumType                  MemberPos                       { set; get; }                   /// 成员战斗站位

    ObscuredInt                 MemberProLv                     { set; get; }                   /// 成员职业等级
    ObscuredInt                 MemberPolarity                  { set; get; }                   /// 成员元素相性(1.冰 2.火 3.雷) 
    ObscuredInt                 MemberLv                        { set; get; }                   /// 成员等级

    ObscuredInt                 Power                           { set; get; }                   /// 力量
    ObscuredInt                 Agile                           { set; get; }                   /// 敏捷
    ObscuredInt                 Intellect                       { set; get; }                   /// 智力
    ObscuredInt                 CurrAnger                       { set; get; }                   /// 当前能量值
    ObscuredInt                 CurrHp                          { set; get; }                   /// 当前血量值
    ObscuredInt                 Hp                              { set; get; }                   /// 固定血量

    ObscuredInt                 SuckBlood                       { set; get; }                   /// 吸血
    ObscuredInt                 Hit                             { set; get; }                   /// 命中
    ObscuredInt                 Dodge                           { set; get; }                   /// 闪避
    ObscuredInt                 Damage                          { set; get; }                   /// 伤害
    ObscuredInt                 EnergyRegen                     { set; get; }                   /// 能量回复
    ObscuredInt                 BloodRegen                      { set; get; }                   /// 血量回复

    ObscuredInt                 PhyAttack                       { set; get; }                   /// 物理攻击
    ObscuredInt                 MagicAttack                     { set; get; }                   /// 魔法攻击
    ObscuredInt                 PhyArmor                        { set; get; }                   /// 物理护甲
    ObscuredInt                 MagicArmor                      { set; get; }                   /// 魔法护甲
    ObscuredInt                 PhyCrit                         { set; get; }                   /// 物理暴击
    ObscuredInt                 MagicCrit                       { set; get; }                   /// 魔法暴击
    ObscuredInt                 ThroughPhyArmor                 { set; get; }                   /// 物理护甲穿透

    ObscuredInt                 ActiveSkillID_1                 { set; get; }                   /// 主动技能_1 ID
    ObscuredInt                 ActiveSkillLv_1                 { set; get; }                   /// 主动技能_1 等级
    ObscuredInt                 ActiveSkillID_2                 { set; get; }                   /// 主动技能_2 ID
    ObscuredInt                 ActiveSkillLv_2                 { set; get; }                   /// 主动技能_2 等级
    ObscuredInt                 UltSkillID                      { set; get; }                   /// 大招技能ID
    ObscuredInt                 UltSkillLv                      { set; get; }                   /// 大招技能等级
    ObscuredInt                 PassiveSkillID                  { set; get; }                   /// 被动技能ID
    ObscuredInt                 PassiveSkillLv                  { set; get; }                   /// 被动技能等级
    ObscuredInt                 TalentSkillID                   { set; get; }                   /// 天赋技能ID (暂未开发)
    ObscuredInt                 BossSkillID                     { set; get; }                   /// Boss技能ID
    ObscuredInt                 BossSkillLv                     { set; get; }                   /// Boss技能等级
    ObscuredInt                 WingID                          { set; get; }                   /// 翅膀ID (暂未开发)
    JsonObject                  MemberDataToJsonObj();                                          /// 成员数据文件 ToJsonObj类型
}

public class BattleMemberData : IBattleMemberData
{
    [Inject]
    public IPlayer              InPlayer                        { set; get; }
    [Inject]
    public IHeroData            InHeroD                         { set; get; }

    private bool                _isRoleHero                     = false;                        /// 是否主角英雄

    private int                 _memberQuality                  = 0;                            /// 成员品质
    private int                 _memberQualityInit              = 0 ;                           /// 成员初始化品质
    private int                 _memberStarLv                   = 0;                            /// 成员星级
    private int                 _MemberID                       = 0;                            /// 成员ID

    private int                 _MemberResID                    = 0;                            /// 资源ID
    private int                 _MemberProAtt                   = 0;                            /// 成员职业属性

    private string              _memberName                     = "";                           /// 成员名称
    private string              _memberIcon                     = "";                           /// 成员图标

    private Battle_MemberType   _MemberType                     = Battle_MemberType.Hero;       /// 成员类型
    private Battle_Camp         _BattleCamp                     = Battle_Camp.Our;              /// 成员阵营(我:1 敌:2)

    private PosNumType          _MemberPos                      = PosNumType.Null;              /// 成员战斗站位

    private ObscuredInt         _MemberProLv                    = 0;                            /// 成员职业等级
    private ObscuredInt         _MemberPolarity                 = 0;                            /// 成员元素相性(1.冰 2.火 3.雷) 
    private ObscuredInt         _MemberLv                       = 0;                            /// 成员等级

    private ObscuredInt         _Power                          = 0;                            /// 力量
    private ObscuredInt         _Agile                          = 0;                            /// 敏捷
    private ObscuredInt         _Intellect                      = 0;                            /// 智力
    private ObscuredInt         _CurrAnger                      = 0;                            /// 当前能量值
    private ObscuredInt         _CurrBlood                      = 0;                            /// 当前血量
    private ObscuredInt         _Blood                          = 0;                            /// 血量

    private ObscuredInt         _SuckBlood                      = 0;                            /// 吸血
    private ObscuredInt         _Hit                            = 0;                            /// 命中
    private ObscuredInt         _Dodge                          = 0;                            /// 闪避
    private ObscuredInt         _Damage                         = 0;                            /// 伤害
    private ObscuredInt         _EnergyRegen                    = 0;                            /// 能量回复
    private ObscuredInt         _BloodRegen                     = 0;                            /// 血量回复

    private ObscuredInt         _PhyAttack                      = 0;                            /// 物理攻击
    private ObscuredInt         _MagicAttack                    = 0;                            /// 魔法攻击
    private ObscuredInt         _PhyArmor                       = 0;                            /// 物理护甲
    private ObscuredInt         _MagicArmor                     = 0;                            /// 魔法护甲
    private ObscuredInt         _PhyCrit                        = 0;                            /// 物理暴击
    private ObscuredInt         _MagicCrit                      = 0;                            /// 魔法暴击
    private ObscuredInt         _ThroughPhyArmor                = 0;                            /// 物理护甲穿透

    private ObscuredInt         _ActiveSkillID_1                = 0;                            /// 主动技能_1 ID
    private ObscuredInt         _ActiveSkillLv_1                = 0;                            /// 主动技能_1 等级
    private ObscuredInt         _ActiveSkillID_2                = 0;                            /// 主动技能_2 ID
    private ObscuredInt         _ActiveSkillLv_2                = 0;                            /// 主动技能_2 等级
    private ObscuredInt         _UltSkillID                     = 0;                            /// 大招技能ID
    private ObscuredInt         _UltSkillLv                     = 0;                            /// 大招技能等级
    private ObscuredInt         _PassiveSkillID                 = 0;                            /// 被动技能ID
    private ObscuredInt         _PassiveSkillLv                 = 0;                            /// 被动技能等级
    private ObscuredInt         _TalentSkillID                  = 0;                            /// 天赋技能ID (暂未开发)
    private ObscuredInt         _BossSkillID                    = 0;                            /// Boss技能ID
    private ObscuredInt         _BossSkillLv                    = 0;                            /// Boss技能等级
    private ObscuredInt         _WingID                         = 0;                            /// 翅膀ID     (暂未开发)

    public bool                 isRoleHero              { set { _isRoleHero = value; }          get { return _isRoleHero; } }           /// 是否主角英雄

    public int                  memberQuality           { set { _memberQuality = value; }       get { return _memberQuality; } }        /// 成员品质
    public int                  memberQualityInit       { set { _memberQualityInit = value; }   get { return _memberQuality; } }        /// 成员初始化品质
    public int                  memberStarLv            { set { _memberStarLv = value; }        get { return _memberStarLv; } }         /// 成员星级
    public int                  MemberProAtt            { set { _MemberProAtt = value; }        get { return _MemberProAtt; } }         /// 成员职业属性

    public int                  MemberID                { set { _MemberID = value; }            get { return _MemberID; } }             /// 成员ID
    public int                  MemberResID             { set { _MemberResID = value; }         get { return _MemberResID; } }          /// 资源ID

    public string               memberName              { set { _memberName = value; }          get { return _memberName; } }           /// 成员名称
    public string               HeadIconSpriteName      { set { _memberIcon = value; }          get { return _memberIcon; } }           /// 成员图标

    public Battle_MemberType    MemberType              { set { _MemberType = value; }          get { return _MemberType; } }           /// 成员类型
    public Battle_Camp          BattleCamp              { set { _BattleCamp = value; }          get { return _BattleCamp; } }           /// 成员阵营(我:1 敌:2)

    public PosNumType           MemberPos               { set { _MemberPos = value; }           get { return _MemberPos; } }            /// 成员战斗站位

    public ObscuredInt          MemberProLv             { set { _MemberProLv= value; }          get { return _MemberProLv; } }          /// 成员职业等级
    public ObscuredInt          MemberPolarity          { set { _MemberPolarity = value; }      get { return _MemberPolarity; } }       /// 成员元素相性(1.冰 2.火 3.雷) 
    public ObscuredInt          MemberLv                { set { _MemberLv = value; }            get { return _MemberLv; } }             /// 成员等级

    public ObscuredInt          Power                   { set { _Power = value; }               get { return _Power; } }                /// 力量
    public ObscuredInt          Agile                   { set { _Agile = value; }               get { return _Agile; } }                /// 敏捷
    public ObscuredInt          Intellect               { set { _Intellect= value; }            get { return _Intellect; } }            /// 智力
    public ObscuredInt          CurrAnger               { set { _CurrAnger = value; }           get { return _CurrAnger; } }            /// 当前能量值
    public ObscuredInt          CurrHp                  { set { _CurrBlood = value; }           get { return _CurrBlood; } }            /// 当前血量
    public ObscuredInt          Hp                      { set { _Blood = value; }               get { return _Blood; } }                /// 血量

    public ObscuredInt          SuckBlood               { set { _SuckBlood = value; }           get { return _SuckBlood; } }            /// 吸血
    public ObscuredInt          Hit                     { set { _Hit = value; }                 get { return _Hit; } }                  /// 命中
    public ObscuredInt          Dodge                   { set { _Dodge = value; }               get { return _Dodge; } }                /// 闪避
    public ObscuredInt          Damage                  { set { _Damage = value; }              get { return _Damage; } }               /// 伤害
    public ObscuredInt          EnergyRegen             { set { _EnergyRegen = value; }         get { return _EnergyRegen; } }          /// 能量回复
    public ObscuredInt          BloodRegen              { set { _BloodRegen = value; }          get { return _BloodRegen; } }           /// 血量回复

    public ObscuredInt          PhyAttack               { set { _PhyAttack = value; }           get { return _PhyAttack; } }            /// 物理攻击
    public ObscuredInt          MagicAttack             { set { _MagicAttack = value; }         get { return _MagicAttack; } }          /// 魔法攻击
    public ObscuredInt          PhyArmor                { set { _PhyArmor = value; }            get { return _PhyArmor; } }             /// 物理护甲
    public ObscuredInt          MagicArmor              { set { _MagicArmor = value; }          get { return _MagicArmor; } }           /// 魔法护甲
    public ObscuredInt          PhyCrit                 { set { _PhyCrit = value; }             get { return _PhyCrit; } }              /// 物理暴击
    public ObscuredInt          MagicCrit               { set { _MagicCrit = value; }           get { return _MagicCrit; } }            /// 魔法暴击
    public ObscuredInt          ThroughPhyArmor         { set { _ThroughPhyArmor= value; }      get { return _ThroughPhyArmor; } }      /// 物理护甲穿透

    public ObscuredInt          ActiveSkillID_1         { set { _ActiveSkillID_1 = value; }     get { return _ActiveSkillID_1; } }      /// 主动技能_1 ID
    public ObscuredInt          ActiveSkillLv_1         { set { _ActiveSkillLv_1 = value; }     get { return _ActiveSkillLv_1; } }      /// 主动技能_1 等级
    public ObscuredInt          ActiveSkillID_2         { set { _ActiveSkillID_2 = value; }     get { return _ActiveSkillID_2; } }      /// 主动技能_2 ID
    public ObscuredInt          ActiveSkillLv_2         { set { _ActiveSkillLv_2 = value; }     get { return _ActiveSkillLv_2; } }      /// 主动技能_2 等级
    public ObscuredInt          UltSkillID              { set { _UltSkillID = value; }          get { return _UltSkillID; } }           /// 大招技能ID
    public ObscuredInt          UltSkillLv              { set { _UltSkillLv = value; }          get { return _UltSkillLv; } }           /// 大招技能等级
    public ObscuredInt          PassiveSkillID          { set { _PassiveSkillID = value; }      get { return _PassiveSkillID; } }       /// 被动技能ID
    public ObscuredInt          PassiveSkillLv          { set { _PassiveSkillLv = value; }      get { return _PassiveSkillLv; } }       /// 被动技能等级
    public ObscuredInt          TalentSkillID           { set { _TalentSkillID = value; }        get { return _TalentSkillID; } }       /// 天赋技能ID (暂未开发)
    public ObscuredInt          BossSkillID             { set { _BossSkillID = value; }         get { return _BossSkillID; } }          /// Boss技能ID
    public ObscuredInt          BossSkillLv             { set { _BossSkillLv = value; }         get { return _BossSkillLv; } }          /// Boss技能等级
    public ObscuredInt          WingID                  { set { _WingID = value; }              get { return _WingID; } }               /// 翅膀ID (暂未开发)

    public JsonObject           MemberDataToJsonObj()                                                                                   // 成员数据文件 ToJsonObj类型   
    {
        JsonObject              TheJsonObj              = new JsonObject();                     /// JsonObj 类型

        TheJsonObj.Add          ("memberQuality",       memberQuality);                         /// 成员品质
        TheJsonObj.Add          ("memberQualityInit",   memberQualityInit);                     /// 成员初始化品质
        TheJsonObj.Add          ("memberStarLv",        memberStarLv);                          /// 成员星级
        TheJsonObj.Add          ("memberName",          memberName);                            /// 成员名称
        TheJsonObj.Add          ("HeadIconSpriteName",  HeadIconSpriteName);                    /// 成员图标
        TheJsonObj.Add          ("isRoleHero",          isRoleHero);                            /// 是否主角英雄
        TheJsonObj.Add          ("MemberType",          MemberType);                            /// 成员类型
        TheJsonObj.Add          ("BattleCamp",          BattleCamp);                            /// 成员阵营(我:1 敌:2)

        TheJsonObj.Add          ("MemberID",            MemberID);                              /// 成员ID
        TheJsonObj.Add          ("MemberResID",         MemberResID);                           /// 资源ID
        TheJsonObj.Add          ("MemberPos",           MemberPos);                             /// 成员战斗站位
        TheJsonObj.Add          ("MemberProAtt",        MemberProAtt);                          /// 成员职业属性
        TheJsonObj.Add          ("MemberProLv",         MemberProLv);                           /// 成员职业等级
        TheJsonObj.Add          ("MemberPolarity",      MemberPolarity);                        /// 成员元素相性(1.冰 2.火 3.雷)
        TheJsonObj.Add          ("MemberLv",            MemberLv);                              /// 成员等级

        TheJsonObj.Add          ("Power",               MemberLv);                              /// 力量
        TheJsonObj.Add          ("Agile",               MemberLv);                              /// 敏捷
        TheJsonObj.Add          ("Intellect",           Intellect);                             /// 智力
        TheJsonObj.Add          ("CurrAnger",           CurrAnger);                             /// 当前能量值
        TheJsonObj.Add          ("CurrHp",              CurrHp);                                /// 当前血量
        TheJsonObj.Add          ("Hp",                  Hp);                                    /// 血量

        TheJsonObj.Add          ("SuckBlood",           SuckBlood);                             /// 吸血
        TheJsonObj.Add          ("Hit",                 Hit);                                   /// 命中
        TheJsonObj.Add          ("Dodge",               Dodge);                                 /// 闪避
        TheJsonObj.Add          ("Damage",              Damage);                                /// 伤害
        TheJsonObj.Add          ("EnergyRegen",         EnergyRegen);                           /// 能量回复
        TheJsonObj.Add          ("BloodRegen",          BloodRegen);                            /// 血量回复

        TheJsonObj.Add          ("PhyAttack",           PhyAttack);                             /// 血量回复
        TheJsonObj.Add          ("MagicAttack",         MagicAttack);                           /// 血量回复
        TheJsonObj.Add          ("PhyArmor",            PhyArmor);                              /// 血量回复
        TheJsonObj.Add          ("MagicArmor",          MagicArmor);                            /// 血量回复
        TheJsonObj.Add          ("PhyCrit",             PhyCrit);                               /// 血量回复
        TheJsonObj.Add          ("MagicCrit",           MagicCrit);                             /// 血量回复
        TheJsonObj.Add          ("ThroughPhyArmor",     ThroughPhyArmor);                       /// 血量回复

        TheJsonObj.Add          ("ActiveSkillID_1",     ActiveSkillID_1);                       /// 主动技能_1 ID
        TheJsonObj.Add          ("ActiveSkillLv_1",     ActiveSkillLv_1);                       /// 主动技能_1 等级
        TheJsonObj.Add          ("ActiveSkillID_2",     ActiveSkillID_2);                       /// 主动技能_2 ID
        TheJsonObj.Add          ("ActiveSkillLv_2",     ActiveSkillLv_2);                       /// 主动技能_2 等级
        TheJsonObj.Add          ("UltSkillID ",         UltSkillID);                            /// 大招技能ID
        TheJsonObj.Add          ("UltSkillLv",          UltSkillLv);                            /// 大招技能等级
        TheJsonObj.Add          ("PassiveSkillID",      PassiveSkillID);                        /// 被动技能ID
        TheJsonObj.Add          ("PassiveSkillLv",      PassiveSkillLv);                        /// 被动技能等级
        TheJsonObj.Add          ("TalentSkillID",       TalentSkillID);                         /// 天赋技能ID
        TheJsonObj.Add          ("BossSkillID",         BossSkillID);                           /// Boss技能ID
        TheJsonObj.Add          ("BossSkillLv",         BossSkillLv);                           /// Boss技能等级
        TheJsonObj.Add          ("WingID",              WingID);                                /// 翅膀ID

        return                  TheJsonObj;
    }
    public                      BattleMemberData()                                                                                      // 构筑函数         
    {

    }
    public                      BattleMemberData        ( JsonObject inJsonObj)                                                         // 构筑函数         
    {
        JsonObjToMemberData(inJsonObj);
    }
    private void                JsonObjToMemberData     ( JsonObject inJsonObj)                                                         // JSonToMember    
    {
        JsonObject              TempObj                 = new JsonObject();                                 // 实例对象

        memberQuality           = Util.GetIntKeyValue(inJsonObj, "memberQuality");                          // 成员品质
        memberQualityInit       = Util.GetIntKeyValue(inJsonObj, "memberQualityInit");                      // 成员初始化品质
        memberStarLv            = Util.GetIntKeyValue(inJsonObj, "memberStarLv");                           // 成员星级
        memberName              = Util.GetStringKeyValue(inJsonObj, "memberName");                          // 成员名称
        HeadIconSpriteName      = Util.GetStringKeyValue(inJsonObj, "HeadIconSpriteName");                  // 成员图标
        isRoleHero              = Util.GetBoolKeyValue(inJsonObj, "isRoleHero");                            // 是否主角英雄 

        MemberID                = Util.GetIntKeyValue(inJsonObj, "memberID");                               // 成员ID
        MemberResID             = Util.GetIntKeyValue(inJsonObj, "MemberResID");                            // 资源ID        
        MemberProAtt            = Util.GetIntKeyValue(inJsonObj, "MemberProAtt");                           // 成员职业属性 (力,敏,智)
        MemberPolarity          = Util.GetIntKeyValue(inJsonObj, "MemberPolarity");                         // 成员元素相性 (1.冰 2.火 3.雷)
        MemberProLv             = Util.GetIntKeyValue(inJsonObj, "MemberProLv");                            // 成员职业等级
        MemberLv                = Util.GetIntKeyValue(inJsonObj, "MemberLv");                               // 成员等级

        Power                   = Util.GetIntKeyValue(inJsonObj, "Power");                                  // 力量
        Agile                   = Util.GetIntKeyValue(inJsonObj, "Agile");                                  // 敏捷
        Intellect               = Util.GetIntKeyValue(inJsonObj, "Intellect");                              // 智力
        CurrAnger               = Util.GetIntKeyValue(inJsonObj, "CurrAnger");                              // 当前能量值
        CurrHp                  = Util.GetIntKeyValue(inJsonObj, "CurrHp");                                 // 当前血量
        Hp                      = Util.GetIntKeyValue(inJsonObj, "Hp");                                     // 血量

        SuckBlood               = Util.GetIntKeyValue(inJsonObj, "SuckBlood");                              // 吸血
        Hit                     = Util.GetIntKeyValue(inJsonObj, "Hit");                                    // 命中
        Dodge                   = Util.GetIntKeyValue(inJsonObj, "Dodge");                                  // 闪避  
        Damage                  = Util.GetIntKeyValue(inJsonObj, "Damage");                                 // 伤害          
        EnergyRegen             = Util.GetIntKeyValue(inJsonObj, "EnergyRegen");                            // 能量回复
        BloodRegen              = Util.GetIntKeyValue(inJsonObj, "BloodRegen");                             // 血量回复

        PhyAttack               = Util.GetIntKeyValue(inJsonObj, "PhyAttack");                              // 物理攻击
        MagicAttack             = Util.GetIntKeyValue(inJsonObj, "MagicAttack");                            // 魔法攻击
        PhyArmor                = Util.GetIntKeyValue(inJsonObj, "PhyArmor");                               // 物理护甲
        MagicArmor              = Util.GetIntKeyValue(inJsonObj, "MagicArmor");                             // 魔法护甲
        PhyCrit                 = Util.GetIntKeyValue(inJsonObj, "PhyCrit");                                // 物理暴击
        MagicCrit               = Util.GetIntKeyValue(inJsonObj, "MagicCrit");                              // 魔法暴击
        ThroughPhyArmor         = Util.GetIntKeyValue(inJsonObj, "ThroughPhyArmor");                        // 物理护甲穿透

        ActiveSkillID_1         = Util.GetIntKeyValue(inJsonObj, "ActiveSkillID_1");                        // 主动技能_1 ID
        ActiveSkillLv_1         = Util.GetIntKeyValue(inJsonObj, "ActiveSkillLv_1");                        // 主动技能_1 等级
        ActiveSkillID_2         = Util.GetIntKeyValue(inJsonObj, "ActiveSkillID_2");                        // 主动技能_2 ID
        ActiveSkillLv_2         = Util.GetIntKeyValue(inJsonObj, "ActiveSkillLv_2");                        // 主动技能_2 等级
        UltSkillID              = Util.GetIntKeyValue(inJsonObj, "UltSkillID");                             // 大招技能ID
        UltSkillLv              = Util.GetIntKeyValue(inJsonObj, "UltSkillLv");                             // 大招技能等级
        PassiveSkillID          = Util.GetIntKeyValue(inJsonObj, "PassiveSkillID");                         // 被动技能ID
        PassiveSkillLv          = Util.GetIntKeyValue(inJsonObj, "PassiveSkillLv");                         // 被动技能等级
        TalentSkillID           = Util.GetIntKeyValue(inJsonObj, "TalentSkillID");                          // 天赋技能ID
        BossSkillID             = Util.GetIntKeyValue(inJsonObj, "BossSkillID");                            // Boss技能ID
        BossSkillLv             = Util.GetIntKeyValue(inJsonObj, "BossSkillLv");                            // Boss技能等级
        WingID                  = Util.GetIntKeyValue(inJsonObj, "WingID");                                 // 翅膀ID

        MemberType              = (Battle_MemberType)Util.GetIntKeyValue(inJsonObj, "MemberType");          // 成员类型
        BattleCamp              = (Battle_Camp)Util.GetIntKeyValue(inJsonObj, "BattleCamp");                // 成员阵营
        MemberPos               = (PosNumType)Util.GetIntKeyValue(TempObj, "MemberPos");                    // 成员位置
        TempObj                 = inJsonObj["MemberPos"] as JsonObject;                                     // 成员位置坐标

    }
    public static BattleMemberData BuildFromHeroData    ( PosNumType inPosNum, IHeroData inHeroD)                                       // 创建英雄成员数据 
    {
        Configs_HeroData        HeroConfig  = Configs_Hero.sInstance.GetHeroDataByHeroID(inHeroD.ID);
        if(HeroConfig == null) 
        {
            Debug.LogError("英雄的配置数据未找到:ID = " + HeroConfig.HeroID);
            return null;
        }
        BattleMemberData TempMemberData     = new BattleMemberData();

        TempMemberData.memberQuality        = (int)inHeroD.Quality;                             // 英雄品质
        TempMemberData.memberQualityInit    = 1;                                                // 成员初始品质
        TempMemberData.memberStarLv         = inHeroD.HeroStar;                                 // 英雄星级

        TempMemberData.memberName           = HeroConfig.HeroName;                              // 英雄成员名称
        TempMemberData.HeadIconSpriteName   = HeroConfig.head84;                                // 英雄成员图标名称
        TempMemberData.isRoleHero           = (inHeroD.ID == 100001 || inHeroD.ID == 100002 || inHeroD.ID == 100003);   // 是否主角英雄

        TempMemberData.MemberType           = Battle_MemberType.Hero;                           // 英雄成员类型(NPC,Hero,Boss)
        TempMemberData.BattleCamp           = Battle_Camp.Our;                                  // 我方

        TempMemberData.MemberID             = inHeroD.ID;                                       // 英雄成员ID
        TempMemberData.MemberResID          = HeroConfig.Resource;                              // 英雄成员资源ID
        TempMemberData.MemberPos            = inPosNum;                                         // 英雄战斗站位
        TempMemberData.MemberProAtt         = HeroConfig.Profession;                            // 英雄职业属性
        TempMemberData.MemberProLv          = inHeroD.ProLevel;                                 // 英雄职业等级
        TempMemberData.MemberPolarity       = HeroConfig.Polarity;                              // 英雄元素属性(冰,火,雷)
        TempMemberData.MemberLv             = inHeroD.HeroLevel;                                // 英雄成员等级



        TempMemberData.Power                = Mathf.RoundToInt(inHeroD.Power);                 // 力量
        TempMemberData.Intellect            = Mathf.RoundToInt(inHeroD.Agile);                 // 智力
        TempMemberData.Agile                = Mathf.RoundToInt(inHeroD.Agile);                 // 敏捷
        TempMemberData.CurrHp               = inHeroD.CurrentBlood;                            // 当前血量
        TempMemberData.CurrAnger            = inHeroD.CurrentAnger;                            // 当前能量
        TempMemberData.Hp                   = Mathf.RoundToInt(inHeroD.Blood);                 // 血量

        TempMemberData.SuckBlood            = Mathf.RoundToInt(inHeroD.SuckBlood);             // 吸血
        TempMemberData.Hit                  = Mathf.RoundToInt(inHeroD.Hit);                   // 命中
        TempMemberData.Dodge                = Mathf.RoundToInt(inHeroD.Dodge);                 // 闪避
        TempMemberData.EnergyRegen          = Mathf.RoundToInt(inHeroD.EnergyRegen);           // 能量回复
        TempMemberData.BloodRegen           = Mathf.RoundToInt(inHeroD.BloodRegen);            // 血量回复

        TempMemberData.PhyAttack            = Mathf.RoundToInt(inHeroD.PhyAttack);             // 物理攻击
        TempMemberData.MagicAttack          = Mathf.RoundToInt(inHeroD.MagicAttack);           // 魔法攻击
        TempMemberData.PhyArmor             = Mathf.RoundToInt(inHeroD.PhyArmor);              // 物理护甲
        TempMemberData.MagicArmor           = Mathf.RoundToInt(inHeroD.MagicArmor);            // 魔法护甲
        TempMemberData.PhyCrit              = Mathf.RoundToInt(inHeroD.PhyCrit);               // 物理暴击
        TempMemberData.MagicCrit            = Mathf.RoundToInt(inHeroD.MagicCrit);             // 魔法暴击
        TempMemberData.ThroughPhyArmor      = Mathf.RoundToInt(inHeroD.ThroughPhyArmor);       // 物理护甲穿透


        TempMemberData.ActiveSkillID_1      = HeroConfig.ActiveSkill1;                          // 主动技能_1   ID
        TempMemberData.ActiveSkillLv_1      = inHeroD.ActiveSkillLevel1;                        // 主动技能_1   等级
        TempMemberData.ActiveSkillID_2      = HeroConfig.ActiveSkill2;                          // 主动技能_2   ID
        TempMemberData.ActiveSkillLv_2      = inHeroD.ActiveSkillLevel2;                        // 主动技能_2   等级
        TempMemberData.UltSkillID           = HeroConfig.UltSkill;                              // 大招技能     ID
        TempMemberData.UltSkillLv           = inHeroD.UltSkillLevel;                            // 大招技能     等级
        TempMemberData.PassiveSkillID       = HeroConfig.PassiveSkill;                          // 被动技能     ID
        TempMemberData.PassiveSkillLv       = inHeroD.PassiveSkillLevel;                        // 被动技能     等级
        TempMemberData.WingID               = inHeroD.WingID;                                   // 英雄翅膀ID
        
        return                              TempMemberData;
    }
    //================================================||  创建 关卡NPC成员数据<CheckPoint>       ||===========================================================
    public static BattleMemberData BuildNpcMemData_CP   ( int inPos, ObscuredInt inBossPos,Configs_CheckPointData inCheckPointD,Configs_HeroData inHeroD)
    {
        BattleMemberData TempMemberData     = new BattleMemberData();

        TempMemberData.MemberID             = inHeroD.HeroID;                                   // NPC成员ID
        TempMemberData.memberName           = inHeroD.HeroName;                                 // NPC成员名称
        TempMemberData.MemberPos            = (PosNumType)(inPos + 10);                         // NPC成员位置
        TempMemberData.MemberLv             = inCheckPointD.NPCLevel;                           // NPC成员等级
        TempMemberData.MemberResID          = inHeroD.Resource;                                 // NPC成员资源ID
        TempMemberData.HeadIconSpriteName   = inHeroD.head84;                                   // NPC成员图标名称
        TempMemberData.BattleCamp           = Battle_Camp.Enemy;                                // 敌方
        TempMemberData.MemberType           = inPos == inBossPos ? Battle_MemberType.Npc_CheckPointBoss : Battle_MemberType.Npc_Normal;  // 成员类型(NPC,Hero,Boss)
        TempMemberData.WingID               = 0;                                                // 翅膀ID

        TempMemberData.memberQualityInit    = 1;                                                // NPC成员初始品质
        TempMemberData.memberQuality        = inCheckPointD.NPCQuality;                         // NPC品质
        TempMemberData.memberStarLv         = inCheckPointD.NPCStar;                            // NPC星级
        TempMemberData.MemberPolarity       = inHeroD.Polarity;                                 // NPC元素属性  (冰,火,雷)
        TempMemberData.MemberProAtt         = inHeroD.Profession;                               // NPC职业属性

        TempMemberData.ActiveSkillID_1      = inHeroD.ActiveSkill1;                             // 主动技能_1   ID
        TempMemberData.ActiveSkillLv_1      = inCheckPointD.ActiveSkill1Level;                  // 主动技能_1   等级
        TempMemberData.ActiveSkillID_2      = inHeroD.ActiveSkill2;                             // 主动技能_2   ID
        TempMemberData.ActiveSkillLv_2      = inCheckPointD.ActiveSkill2Level;                  // 主动技能_2   等级
        TempMemberData.UltSkillID           = inHeroD.UltSkill;                                 // 大招技能     ID
        TempMemberData.UltSkillLv           = inCheckPointD.UltSkill;                           // 大招技能     等级
        TempMemberData.PassiveSkillID       = inHeroD.PassiveSkill;                             // 被动技能     ID
        TempMemberData.PassiveSkillLv       = inCheckPointD.PassiveSkillLevel;                  // 被动技能     等级

        // NPC品质数据配置
        Configs_NPCQualityData NpcQConfig               = Configs_NPCQuality.sInstance.GetNPCQualityDataByQualityID(GetNpcQualityID(inCheckPointD.NPCQuality, inHeroD));
                                                                                                // NPC星级数据配置
        Configs_HeroStarData HeroStarConfig             = Configs_HeroStar.sInstance.GetHeroStarDataByStarID(GetNpcStarID(inCheckPointD.NPCStar, inHeroD));
                                                                                                // NPC属性调整配置
        Configs_NPCAbilityModifierData NpcAttModConfig  = Configs_NPCAbilityModifier.sInstance.GetNPCAbilityModifierDataByAdjustID(inCheckPointD.NPCAdjust);

        if (NpcQConfig          != null) SetNpcQualityAdjust(TempMemberData, NpcQConfig);       // NPC品质数据设置
        if (HeroStarConfig      != null) SetNpcStarAdjust(TempMemberData, HeroStarConfig);      // NPC星级数据设置
        if (NpcAttModConfig     != null) SetNpcModify(TempMemberData, NpcAttModConfig);         // NPC属性调整

        if (inPos == inBossPos)                                                                 // Boss属性调整 
        {
            Configs_NPCAbilityModifierData BossAttModDconfig = Configs_NPCAbilityModifier.sInstance.GetNPCAbilityModifierDataByAdjustID(inCheckPointD.BossAdjust);
            if (BossAttModDconfig != null) SetNpcModify(TempMemberData, BossAttModDconfig);
        }
        return TempMemberData;
    }
    public static void SecondAttToMember(IBattleMemberData inMember,ObscuredInt inPower,ObscuredInt inAgile,ObscuredInt inIntell)       // 英雄二级属性转换为英雄一级属性 (力,敏,智力)
    {
        Dictionary<int, Configs_AttributeRelationData> TheAttRelatDic = Configs_AttributeRelation.sInstance.mAttributeRelationDatas;    // 配置字典

        if (inMember.MemberProAtt == (int)Profession.Power)                                     // 力量属性转换       
        {
            Configs_AttributeRelationData AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(1);             // 力量属性转换
            inMember.CurrHp       += (int)(inPower * AttConfig.Blood);
            inMember.PhyAttack    += (int)(inPower * AttConfig.PhysicalAttack);
            inMember.MagicAttack  += (int)(inPower * AttConfig.MagicAttack);
            inMember.PhyArmor     += (int)(inPower * AttConfig.PhysicalArmor);
            inMember.MagicArmor   += (int)(inPower * AttConfig.MagicArmor);
            inMember.PhyCrit      += (int)(inPower * AttConfig.PhysicalCritical);

            AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(2);                                           // 敏捷属性转换
            inMember.CurrHp        += (int)(inAgile * AttConfig.Blood);
            inMember.PhyAttack    += (int)(inAgile * AttConfig.PhysicalAttack);
            inMember.MagicAttack  += (int)(inAgile * AttConfig.MagicAttack);
            inMember.PhyArmor     += (int)(inAgile * AttConfig.PhysicalArmor);
            inMember.MagicArmor   += (int)(inAgile * AttConfig.MagicArmor);
            inMember.PhyCrit      += (int)(inAgile * AttConfig.PhysicalCritical);

            AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(3);                                           // 智力属性转换
            inMember.CurrHp        += (int)(inIntell * AttConfig.Blood);
            inMember.PhyAttack    += (int)(inIntell * AttConfig.PhysicalAttack);
            inMember.MagicAttack  += (int)(inIntell * AttConfig.MagicAttack);
            inMember.PhyArmor     += (int)(inIntell * AttConfig.PhysicalArmor);
            inMember.MagicArmor   += (int)(inIntell * AttConfig.MagicArmor);
            inMember.PhyCrit      += (int)(inIntell * AttConfig.PhysicalCritical);
        }
        else if (inMember.MemberProAtt == (int)Profession.Agile)                                // 敏捷英雄 属性转换  
        {
            Configs_AttributeRelationData AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(4);             // 力量属性转换
            inMember.CurrHp         += (int)(inPower * AttConfig.Blood);
            inMember.PhyAttack      += (int)(inPower * AttConfig.PhysicalAttack);
            inMember.MagicAttack    += (int)(inPower * AttConfig.MagicAttack);
            inMember.PhyArmor       += (int)(inPower * AttConfig.PhysicalArmor);
            inMember.MagicArmor     += (int)(inPower * AttConfig.MagicArmor);
            inMember.PhyCrit        += (int)(inPower * AttConfig.PhysicalCritical);

            AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(5);                                           // 敏捷属性转换
            inMember.CurrHp          += (int)(inAgile * AttConfig.Blood);
            inMember.PhyAttack      += (int)(inAgile * AttConfig.PhysicalAttack);
            inMember.MagicAttack    += (int)(inAgile * AttConfig.MagicAttack);
            inMember.PhyArmor       += (int)(inAgile * AttConfig.PhysicalArmor);
            inMember.MagicArmor     += (int)(inAgile * AttConfig.MagicArmor);
            inMember.PhyCrit        += (int)(inAgile * AttConfig.PhysicalCritical);

            AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(6);                                           // 智力属性转换
            inMember.CurrHp         += (int)(inIntell * AttConfig.Blood);
            inMember.PhyAttack      += (int)(inIntell * AttConfig.PhysicalAttack);
            inMember.MagicAttack    += (int)(inIntell * AttConfig.MagicAttack);
            inMember.PhyArmor       += (int)(inIntell * AttConfig.PhysicalArmor);
            inMember.MagicArmor     += (int)(inIntell * AttConfig.MagicArmor);
            inMember.PhyCrit        += (int)(inIntell * AttConfig.PhysicalCritical);
        }
        else if (inMember.MemberProAtt == (int)Profession.Intelligence)                         // 智力英雄 属性转换  
        {
            Configs_AttributeRelationData AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(7);             // 力量属性转换
            inMember.CurrHp         += (int)(inPower * AttConfig.Blood);
            inMember.PhyAttack      += (int)(inPower * AttConfig.PhysicalAttack);
            inMember.MagicAttack    += (int)(inPower * AttConfig.MagicAttack);
            inMember.PhyArmor       += (int)(inPower * AttConfig.PhysicalArmor);
            inMember.MagicArmor     += (int)(inPower * AttConfig.MagicArmor);
            inMember.PhyCrit        += (int)(inPower * AttConfig.PhysicalCritical);

            AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(8);                                           // 敏捷属性转换
            inMember.CurrHp         += (int)(inAgile * AttConfig.Blood);
            inMember.PhyAttack      += (int)(inAgile * AttConfig.PhysicalAttack);
            inMember.MagicAttack    += (int)(inAgile * AttConfig.MagicAttack);
            inMember.PhyArmor       += (int)(inAgile * AttConfig.PhysicalArmor);
            inMember.MagicArmor     += (int)(inAgile * AttConfig.MagicArmor);
            inMember.PhyCrit        += (int)(inAgile * AttConfig.PhysicalCritical);

            AttConfig = Configs_AttributeRelation.sInstance.GetAttributeRelationDataByNum(9);                                           // 智力属性转换
            inMember.CurrHp         += (int)(inIntell * AttConfig.Blood);
            inMember.PhyAttack      += (int)(inIntell * AttConfig.PhysicalAttack);
            inMember.MagicAttack    += (int)(inIntell * AttConfig.MagicAttack);
            inMember.PhyArmor       += (int)(inIntell * AttConfig.PhysicalArmor);
            inMember.MagicArmor     += (int)(inIntell * AttConfig.MagicArmor);
            inMember.PhyCrit        += (int)(inIntell * AttConfig.PhysicalCritical);
        }
    }

    //          |````````````````````````````````````   Private Function   ```````````````````````````````````````|
    //          |_________________________________________________________________________________________________|

    private static ObscuredInt  GetNpcQualityID(int npcQuality, Configs_HeroData heroConfig)                                            // 获取NPC品质配置ID              
    {
        HeroQuality HeroQ = (HeroQuality)npcQuality;
        switch (HeroQ)                                   // 返回品质 ID
        {
            case HeroQuality.White: return heroConfig.White;
            case HeroQuality.Green: return heroConfig.Green;
            case HeroQuality.Green1: return heroConfig.Green1;
            case HeroQuality.Blue: return heroConfig.Blue;
            case HeroQuality.Blue1: return heroConfig.Blue1;
            case HeroQuality.Blue2: return heroConfig.Blue2;
            case HeroQuality.Purple: return heroConfig.Purple;
            case HeroQuality.Purple1: return heroConfig.Purple1;
            case HeroQuality.Purple2: return heroConfig.Purple2;
            case HeroQuality.Purple3: return heroConfig.Purple3;
            case HeroQuality.Gold: return heroConfig.Gold;
            default:
                return heroConfig.White;
        }
    }
    private static ObscuredInt  GetNpcStarID(int npcStar, Configs_HeroData heroConfig)                                                  // 获取NPC星级配置ID              
    {
        switch (npcStar)                                 // 返回星级 ID 
        {
            case (1): return heroConfig.Star1;
            case (2): return heroConfig.Star2;
            case (3): return heroConfig.Star3;
            case (4): return heroConfig.Star4;
            case (5): return heroConfig.Star5;
            default:
                return heroConfig.Star1;
        }
    }
    private static void         SetNpcQualityAdjust(BattleMemberData member, Configs_NPCQualityData npcQConfig)                         // 设置NPC品质属性调整            
    {
        member.Power            += npcQConfig.Strength;                         // 力量
        member.Agile            += npcQConfig.Agility;                          // 敏捷
        member.Intellect        += npcQConfig.Mentality;                        // 智力

        member.Hp               += npcQConfig.Blood;                            // 血量
        member.Hit              += npcQConfig.Hit;                              // 命中
        member.Dodge            += npcQConfig.Dodge;                            // 闪避
        member.SuckBlood        += npcQConfig.SuckBlood;                        // 吸血
        member.EnergyRegen      += npcQConfig.EnergyRegen;                      // 能量回复
        member.BloodRegen       += npcQConfig.BloodRegen;                       // 血量回复

        member.PhyAttack        += npcQConfig.PhysicalAttack;                   // 物理攻击
        member.MagicAttack      += npcQConfig.MagicAttack;                      // 魔法攻击
        member.PhyArmor         += npcQConfig.PhysicalArmor;                    // 物理护甲
        member.MagicArmor       += npcQConfig.MagicArmor;                       // 魔法护甲
        member.PhyCrit          += npcQConfig.PhysicalCrit;                     // 物理暴击
        member.MagicCrit        += npcQConfig.MagicCrit;                        // 魔法暴击
        member.ThroughPhyArmor  += npcQConfig.PenetratePhysicalArmor;           // 物理护甲穿透

    }
    private static void         SetNpcStarAdjust(BattleMemberData member, Configs_HeroStarData npcStarConfig)                           // 设置NPC星级属性调整            
    {
        member.Power            += (int)(npcStarConfig.StrengthGrowing * (member.MemberLv - 1));
        member.Agile            += (int)(npcStarConfig.AgilityGrowing * (member.MemberLv - 1));
        member.Intellect        += (int)(npcStarConfig.MentalityGrowing * (member.MemberLv - 1));
    }
    private static void         SetNpcModify(BattleMemberData member, Configs_NPCAbilityModifierData NpcAttModConfig)                   // 设置NPC属性修改                
    {
        member.Hp               += NpcAttModConfig.Blood;                       // 血量调整
        member.PhyAttack        += NpcAttModConfig.PhysicalAttack;              // 物理攻击调整
        member.MagicAttack      += NpcAttModConfig.MagicAttack;                 // 魔法攻击调整
        member.PhyArmor         += NpcAttModConfig.PhysicalArmor;               // 物理护甲调整
        member.MagicArmor       += NpcAttModConfig.MagicArmor;                  // 魔法护甲调整
    }
}
public enum                     Battle_MemberType                                               // 战斗成员类型                 
{
    Hero                        = 0,                            // 英雄
    Npc_Normal                  = 1,                            // 普通NPC
    Mercenary                   = 2,                            // 佣兵 ( 未启用 )
    Npc_CheckPointBoss          = 3,                            // 关卡boss
    Npc_WorldBoss               = 4,                            // 世界boss
}
public enum                     Battle_Camp                                                     // 战斗阵营(我:1 敌:2)          
{
    Our                         = 1,                            // 我方
    Enemy                       = 2,                            // 敌方
}