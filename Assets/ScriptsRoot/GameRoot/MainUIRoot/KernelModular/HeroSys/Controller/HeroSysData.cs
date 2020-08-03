using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroSysData : IHeroSysData
{
    private int _mergeEquipID       = 0;
    private int _targetLv           = 1;
    private int _tempEquipID        = 0;
    private int _wingID             = 0;
    private int _equipID            = 0;
    private int _mergeCountCoins    = 0;

    private long _skillPointTime    = 0;

    private bool _isUpSKforUpQuality    = false;
    private bool _isAllEquip            = false;
    private bool _isUpStar              = false;
    private bool _isWearEquip           = false;
    private bool _isCanSelectItem       = false;
    private bool _isMedalMax            = false;

    private LineUpSequence _heroChooseType  = LineUpSequence.All;
    private EquipButtonState _tempState     = EquipButtonState.NoEquip;
    private WearPosition _equipGrid         = WearPosition.LeftTop;
    private SkillType _skillType            = SkillType.ActiveSkill1;

    private IHeroData _TempHero = null;

    private Vector3 _pos                                    = Vector3.zero;
    private Configs_ActionAndEffectData _CurrentHeroEffect  = new Configs_ActionAndEffectData();
    private GameObject _CurrentClickEquipGrid               = new GameObject();
    private GameObject _CurrentHeroModel                    = new GameObject();

    private ArrayList _UpMedalObjList       = new ArrayList();

    private List<IHeroData> _AllHeroList    = new List<IHeroData>();
    private List<IHeroData> _FontHeroList   = new List<IHeroData>();
    private List<IHeroData> _MidHeroList    = new List<IHeroData>();
    private List<IHeroData> _BackHeroList   = new List<IHeroData>();
    private List<IHeroData> _NoneHeroList   = new List<IHeroData>();

    private List<IHeroData> _SoldiersList   = new List<IHeroData>();
    private List<IHeroData> _AssassinsList  = new List<IHeroData>();
    private List<IHeroData> _MagicansList   = new List<IHeroData>();
    private List<IHeroData> _SnipersList    = new List<IHeroData>();
    private List<IHeroData> _SuppliesList   = new List<IHeroData>();

    private List<EquipMergeObj> _EquipMergeList = new List<EquipMergeObj>();
    private List<int> _OneKeyEquipList = new List<int>();
    private List<int> _OneKeyUIEffectList = new List<int>();

    private Dictionary<WearPosition, int> _GetOneKeyAllEquipDic = new Dictionary<WearPosition, int>();
    private Dictionary<WearPosition, int> _MergeAllEquipDic = new Dictionary<WearPosition, int>();

    private Dictionary<int, int> _MergeItemDic              = new Dictionary<int, int>();

    private Dictionary<int, int> _MedalPropDic              = new Dictionary<int, int>();
    private Dictionary<int, int> _MedalEquipDic             = new Dictionary<int, int>();
    private Dictionary<int, int> _MedalEquipFragDic         = new Dictionary<int, int>();
    private Dictionary<int, int> _MedalScrollFragDic        = new Dictionary<int, int>();
    private Dictionary<int, MedalSelectItem> _SelectItemDic = new Dictionary<int, MedalSelectItem>();

    public int mergeEquipID                                                 // 合成装备ID                           
    {
        set { this._mergeEquipID = value; }
        get { return this._mergeEquipID; }
    }
    public int targetLv                                                     // 强化目标ID                           
    {
        set { this._targetLv = value; }
        get { return this._targetLv; }
    }
    public int tempEquipID                                                  // 缓存选中装备ID                       
    {
        set { this._tempEquipID = value; }
        get { return this._tempEquipID; }
    }
    public int wingID                                                       // 翅膀ID                               
    {
        set { this._wingID = value; }
        get { return this._wingID; }
    }
    public int equipID                                                      // 装备ID                               
    {
        set { this._equipID = value; }
        get { return this._equipID; }
    }
    public int mergeCountCoins                                              // 合成需要的金币                       
    {
        set { this._mergeCountCoins = value; }
        get { return this._mergeCountCoins; }
    }

    public long skillPointTime                                              // 技能点时间戳                         
    {
        set { this._skillPointTime = value; }
        get { return this._skillPointTime; }
    }

    public bool isUpSKforUpQuality                                          // 此次技能提升_是否因为品质提升        
    {
        set { this._isUpSKforUpQuality = value; }
        get { return this._isUpSKforUpQuality; }
    }
    public bool isAllEquip                                                  // 是否一键All装备(区分普通合成)        
    {
        set { this._isAllEquip = value; }
        get { return this._isAllEquip; }
    }
    public bool isUpStar                                                    // 是否升星                             
    {
        set { this._isUpStar = value; }
        get { return this._isUpStar; }
    }
    public bool isWearEquip                                                 // 是否穿戴装备                         
    {
        set { this._isWearEquip = value; }
        get { return this._isWearEquip; }
    }
    public bool isCanSelectItem                                             // 是否是否可选材料                     
    {
        set { this._isCanSelectItem = value; }
        get { return this._isCanSelectItem; }
    }
    public bool isMedalMax                                                  // 是否勋章满级                         
    {
        set { this._isMedalMax = value; }
        get { return this._isMedalMax; }
    }

    public LineUpSequence heroChooseType                                    // 英雄界面类型选择                     
    {
        set { this._heroChooseType = value; }
        get { return this._heroChooseType; }
    }
    public EquipButtonState tempState                                       // 缓存装备状态                         
    {
        set { this._tempState = value; }
        get { return this._tempState; }
    }
    public WearPosition equipGrid                                          // 装备位置                             
    {
        set { this._equipGrid = value; }
        get { return this.equipGrid; }
    }
    public SkillType skillType                                              // 技能类型                             
    {
        set { this._skillType = value; }
        get { return this._skillType; }
    }

    public IHeroData TempHero                                               // 缓存英雄                             
    {
        set { this._TempHero = value; }
        get { return this._TempHero; }
    }
    public Vector3 pos                                                      // 坐标位置                             
    {
        set { this._pos = value; }
        get { return this._pos; }
    }
    public Configs_ActionAndEffectData CurrentHeroEffect                    // 当前英雄特效                         
    {
        set { this._CurrentHeroEffect = value; }
        get { return this._CurrentHeroEffect; }
    }
    public GameObject CurrentClickEquipGrid                                 // 当前点击装备格子                     
    {
        set { this._CurrentClickEquipGrid = value; }
        get { return this._CurrentClickEquipGrid; }
    }
    public GameObject CurrentHeroModel                                      // 当前英雄模型                         
    {
        set { this._CurrentClickEquipGrid = value; }
        get { return this._CurrentClickEquipGrid; }
    }

    public ArrayList UpMedalObjList                                         // 升级勋章 物品队列                    
    {
        set { this._UpMedalObjList = value; }
        get { return _UpMedalObjList; }
    }
    public List<IHeroData> AllHeroList                                      // 所有英雄列表                         
    {
        set { this._AllHeroList = value; }
        get { return this._AllHeroList; }
    }
    public List<IHeroData> FontHeroList                                     // 前排英雄列表                         
    {
        set { this._FontHeroList = value; }
        get { return this._FontHeroList; }
    }
    public List<IHeroData> MidHeroList                                      // 中排英雄列表                         
    {
        set { this._MidHeroList = value; }
        get { return this._MidHeroList; }
    }
    public List<IHeroData> BackHeroList                                     // 后排英雄列表                         
    {
        set { this._BackHeroList = value; }
        get { return this._NoneHeroList; }
    }
    public List<IHeroData> NoneHeroList                                     // 未召唤英雄列表                       
    {
        set { this._NoneHeroList = value; }
        get { return this._NoneHeroList; }
    }

    public List<IHeroData> SoldiersList                                     // 战士职业列表                         
    {
        set { this._SoldiersList = value; }
        get { return this._SoldiersList; }
    }
    public List<IHeroData> AssassinsList                                    // 刺客职业列表                         
    {
        set { this._AssassinsList = value; }
        get { return this._AssassinsList; }
    }
    public List<IHeroData> MagicansList                                     // 法师职业列表                         
    {
        set { this._MagicansList = value; }
        get { return this._MagicansList; }
    }
    public List<IHeroData> SnipersList                                      // 射手职业列表                         
    {
        set { this._SnipersList = value; }
        get { return this._SnipersList; }
    }
    public List<IHeroData> SuppliesList                                     // 辅助职业列表                         
    {
        set { this._SuppliesList = value; }
        get { return this._SuppliesList; }
    }

    public List<EquipMergeObj> EquipMergeList                               // 存储合并的装备ID                     
    {
        set { this._EquipMergeList = value; }
        get { return this._EquipMergeList; }
    }
    public List<int> OneKeyEquipList                                        // 一键装备列表 (用于累加显示属性)      
    {
        set { this._OneKeyEquipList = value; }
        get { return this._OneKeyEquipList; }
    }
    public List<int> OneKeyEquipUIEffectPosList                             // 一键装备特效位置列表 (0:左上 1:右上 2:左中 3:右中 4:左下 5:右下)
    {
        set { this._OneKeyUIEffectList = value; }
        get { return this._OneKeyUIEffectList; } 
    }
    public List<Configs_HeroData> GetNotHaveHeroList(IPlayer player)        // 获取未召唤英雄列表                   
    {
        List<Configs_HeroData> NotHaveHeroList = new List<Configs_HeroData>();
        List<Configs_HeroData> HeroList = new List<Configs_HeroData>(Configs_Hero.sInstance.mHeroDatas.Values);

        foreach(Configs_HeroData hero in HeroList)
        {
            bool isExits = false;

            for (int i = 0;i < player.HeroList.Count;i++)
            {
                if(hero.HeroID == player.HeroList[i].ID)
                {
                    isExits = true;
                    break;
                }
            }
            if(!isExits && (HeroType)hero.HeroType != HeroType.NPC && (HeroType)hero.HeroType != HeroType.Merc && (hero.HeroRace == Configs_Hero.sInstance.GetHeroDataByHeroID(player.PlayerRoleHero.ID).HeroRace || hero.HeroRace == 0 ))
            {
                NotHaveHeroList.Add(hero);                  
            }
        }
        return NotHaveHeroList;
    }

    public Dictionary<WearPosition,int> GetOneKeyAllEquipDic                // 获取一键装备字典                     
    {
        set { this._GetOneKeyAllEquipDic = value; }
        get { return this._GetOneKeyAllEquipDic; }
    }
    public Dictionary<WearPosition,int> MergeOneKeyAllEquipDic              // 一键装备中 需要先合成的装备字典      
    {
        set { this._MergeAllEquipDic = value; }
        get { return this._MergeAllEquipDic; }
    }
    public Dictionary<int,int> MergeItemDic                                 // 合成物品所需要的消耗品               
    {
        set { this._MergeItemDic = value; }
        get { return this._MergeItemDic; }
    }

    public Dictionary<int,int> MedalPropDic                                 // 勋章升级 道具字典                    
    {
        set { this._MedalPropDic = value; }
        get { return this._MedalPropDic; }
    }
    public Dictionary<int,int> MedalEquipDic                                // 勋章升级 装备字典                    
    {
        set { this._MedalEquipDic = value; }
        get { return this._MedalEquipDic; }
    }
    public Dictionary<int,int> MedalEquipFragDic                            // 勋章升级 装备碎片字典                
    {
        set { this._MedalEquipFragDic = value; }
        get { return this._MedalEquipFragDic; }
    }
    public Dictionary<int,int> MedalScrollFragDic                           // 勋章升级 卷轴碎片字典                
    {
        set { this._MedalScrollFragDic = value; }
        get { return this._MedalScrollFragDic; }
    }
    public Dictionary<int,MedalSelectItem> SelectItemDic                    // 当前选择的勋章物品字典               
    {
        set { this._SelectItemDic = value; }
        get { return this._SelectItemDic; }
    }
}
