using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IHeroSysData
{

    int mergeEquipID { set; get; }                                          // 合成装备ID
    int targetLv { set; get; }                                              // 强化目标等级
    int tempEquipID { set; get; }                                           // 缓存选中的装备ID
    int wingID { set; get; }                                                // 翅膀ID
    int equipID { set; get; }                                               // 装备ID
    int mergeCountCoins { set; get; }                                       // 合成需要金币
    long skillPointTime { set; get; }                                       // 技能点回复起始时间戳

    bool isUpSKforUpQuality { set; get; }                                   // 是否升阶品质提升的技能升级
    bool isAllEquip { set; get; }                                           // 是否一键装备(区分普通合成逻辑)
    bool isUpStar { set; get; }                                             // 是否升星
    bool isWearEquip { set; get; }                                          // true:正在穿戴装备切换界面
    bool isCanSelectItem { set; get; }                                      // 是否可选择材料
    bool isMedalMax { set; get; }                                           // 是否勋章满级

    LineUpSequence heroChooseType { set; get; }                             // 英雄选择界面的按钮点击
    IHeroData TempHero { set; get; }                                        // 缓存选中的英雄
    EquipButtonState tempState { set; get; }                                // 缓存装备按钮状态
    WearPosition equipGrid { set; get; }                                    // 装备位置
    SkillType skillType { set; get; }                                       // 技能类型

    Vector3 pos { set; get; }                                               // 位置坐标
    GameObject CurrentClickEquipGrid { set; get; }                          // 当前点击装备格子
    GameObject CurrentHeroModel { set; get; }                               // 当前显示的英雄模型
    Configs_ActionAndEffectData CurrentHeroEffect { set; get; }             // 当前上线的英雄模型特效

    //ArrayList GetLockedMerc(IPlayer player);                              // 获取未解锁的佣兵列表
    ArrayList UpMedalObjList { get; }                                       // 勋章强化的物品选取队列 里面存的是 MedalUpObj 

    List<IHeroData> AllHeroList { get; }                                    // 所有英雄列表
    List<IHeroData> FontHeroList { get; }                                   // 前排英雄列表
    List<IHeroData> MidHeroList { get; }                                    // 中排英雄列表
    List<IHeroData> BackHeroList { get; }                                   // 后排英雄列表
    List<IHeroData> NoneHeroList { get; }                                   // 未召唤英雄列表

    List<IHeroData> SoldiersList { set; get; }                              // 战士职业英雄列表
    List<IHeroData> AssassinsList { set; get; }                             // 刺客职业英雄列表
    List<IHeroData> MagicansList { set; get; }                              // 法师职业英雄列表
    List<IHeroData> SnipersList { set; get; }                               // 射手职业英雄列表
    List<IHeroData> SuppliesList { set; get; }                              // 辅助职业英雄列表

    List<EquipMergeObj> EquipMergeList { get; }                             // 存储合并的装备ID
    List<int> OneKeyEquipList { set; get; }                                 // 一键装备列表 (用于累加显示属性)
    List<int> OneKeyEquipUIEffectPosList { set; get; }                      // 一键装备特效位置列表 0:左上 1:右上 2:左中 3:右中 4:左下 5:右下 

    List<Configs_HeroData> GetNotHaveHeroList(IPlayer player);              // 获取未拥有的英雄列表,不包含佣兵


    Dictionary<WearPosition, int> GetOneKeyAllEquipDic { get; }             // 获取一键装备字典
    Dictionary<WearPosition, int> MergeOneKeyAllEquipDic { get; }           // 一键装备中的需要先合成的装备字典
    Dictionary<int,int> MergeItemDic { get; }                               // 合成物品所需要的消耗品

    Dictionary<int,int> MedalPropDic { set; get; }                          // 勋章升级 道具字典
    Dictionary<int,int> MedalEquipDic { set; get; }                         // 勋章升级 装备字典
    Dictionary<int,int> MedalEquipFragDic { set; get; }                     // 勋章升级 装备碎片字典
    Dictionary<int,int> MedalScrollFragDic { set; get; }                    // 勋章升级 卷轴碎片字典
    Dictionary<int,MedalSelectItem> SelectItemDic  { set; get; }            // 当前选择的勋章物品字典 


}


public class MedalSelectItem                                                // 勋章升级选择对象                     
{
    public MedalItemType itemTpey;
    public int itemNum;
    public int itemExp;
    public int maxNum;
}
public class MedalUpObj                                                     // 升级勋章对象                         
{
    public MedalItemType type;
    public int count;
    public int ID;
}
public class FightForce                                                     // 战斗力
{
    public int heroID = 0;
    public FightForceType item;
}

public enum MedalItemType                                                   // 勋章升级物品类型                     
{
    MedalExp  = 1,
    Equip     = 2,
    EquipFrag = 3,
    Reel      = 4,
    ReelFrag  = 5
}
public enum FightForceType                                                  // 战斗力类型                              
{
    HeroUpLv        = 1,                                // 英雄升级
    HeroUpStar      = 2,                                // 英雄升星
    HeroUpQuality   = 3,                                // 英雄品质升阶
    HeroUpEquip     = 4,                                // 英雄提升装备
    HeroUpSkill     = 5,                                // 英雄技能升级
    HeroUpWing      = 6,                                // 翅膀升级
    AddHero_Talent  = 7,                                // 上阵英雄+天赋
    AddLineUp       = 8,                                // 上阵英雄
    AddHero         = 9,                                // 获得英雄
    MercUpLv        = 10,                               // 佣兵升级
    MercUpQuality   = 11                                // 佣兵升阶
}