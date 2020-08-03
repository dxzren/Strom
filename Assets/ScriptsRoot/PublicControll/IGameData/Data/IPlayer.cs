using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

///-------------------------------------------------------------------------------------------------------------------------/// <summary> 玩家数据接口   </summary>
public interface                IPlayer                         
{
    int                         PlayerID                { set; get; }                           // 01.玩家ID
    int                         GuildID                 { set; get; }                           // 02.公会ID
    int                         WeakGuideID             { set; get; }                           // 03.弱引导ID
    int                         ClickHeroID             { set; get; }                           // 04.点击选择的英雄ID(存储)
    int                         GameServerID            { set; get; }                           // 05.当前游戏服务器分线ID

    int                         ServerCenterID          { set; get; }                           // 06.中心服务器ID
    int                         RegTime                 { set; get; }                           // 07.注册时间戳
    int                         DiamondOfRecharge       { set; get; }                           // 08.钻石充值金额
    int                         MonthCardRemainingDays  { set; get; }                           // 09.月卡剩余天数
    int                         LogInGameDays           { set; get; }                           // 10.登录游戏天数

    int                         SkillPoint              { set; get; }                           // 11.技能点
    int                         PlayerCurrentStamina    { set; get; }                           // 12.玩家当前体力
    int                         GuideProces             { set; get; }                           // 13.引导进度
    int                         FriendStaminaCount      { set; get; }                           // 14.已领取好友系统体力数量
    int                         BuyedStaminaTimes       { set; get; }                           // 15.已购体力次数

    int                         BuyedCoinsTimes         { set; get; }                           // 16.已购金币次数
    int                         BuyedSkillTimes         { set; get; }                           // 17.已购技能次数
    int                         LvRewardGot             { set; get; }                           // 18.已领取等级奖励
    int[]                       WeakGuideStates         { get; }                                // 19.弱引导状态(0:未完成 , 1:完成)
                                                                                                // 下标:10310 10610 17010 10810 1110 10510 10240 11610 11710 弱引导号
    
    long                        MonthCardTime           { set; get; }                           // 月卡时间戳
    float                       PlayerCurrentExp        { set; get; }                           // 玩家当前经验 

    string                      PlayerName              { set; get; }                           // 玩家名称
    string                      GuildName               { set; get; }                           // 公会名称
    string                      ServerName              { set; get; }                           // 服务器名称
    string                      PlayerHeadIconName      { set; get; }                           // 玩家头像图标名称
    string                      PlayerCircleIconName    { set; get; }                           // 玩家圆形图标名称
    string                      PublicInfo              { set; get; }                           // 公告信息

    //--------------------------------------------------------------------------------------------------------------
    IJJCPlayer                  JJCInfo                     { set; get; }                       // 竞技场信息
    IHeroData                   PlayerRoleHero              { set; get; }                       // 玩家主角英雄
    IHeroData                   PlayerMerc                  { set; get; }                       // 玩家的佣兵

    ObscuredInt                 PlayerLevel                 { set; get; }                       // 玩家等级
    ObscuredInt                 PlayerVIPExp                { set; get; }                       // 玩家VIP经验
    ObscuredInt                 PlayerVIPLevel              { set; get; }                       // 玩家VIP等级

    ObscuredLong                PlayerCoins                 { set; get; }                       // 玩家金币
    ObscuredLong                PlayerDiamonds              { set; get; }                       // 玩家钻石

    IHeroData                   GetHeroByID                 (int inHeroID);                     // 获得对应ID的英雄
    Dictionary<int, int>        GetHeroFragmentList         ();                                 // 获得英雄碎片列表
    Dictionary<int, int>        GetNormalCheckPointStars    { get; }                            // 获得普通关卡的星数
    Dictionary<int, int>        GetEliteCheckPointStars     { get; }                            // 获得精英关卡的星数
    Dictionary<int, int>        WingList                    { get; }                            // 获得翅膀列表
    Dictionary<PosNumType, IHeroData> GetLineUpFile         (BattleType lineUpName);            // 从本地读取保存的阵容
    Dictionary<PosNumType, IHeroData> BattleTypeToLineUp    (BattleType map);                   // 根据战斗地图获得相应的阵容

    CheckPointObject            NormalMaxCheckPointHistory  { set; get; }                       // 普通模式最大关卡
    CheckPointObject            EliteMaxCheckPointHistory   { set; get; }                       // 精英模式最大关卡
    CheckPointObject            CurrentCheckPoint           { set; get; }                       // 玩家当前进行的关卡ID

    List<IHeroData>             HeroList                    { get; }                            // 获取已召唤英雄列表
    List<Soul>                  GetHeroSoulList             { get; }                            // 获得已获得英雄魂石列表
    List<Prop>                  PropList                    { get; }                            // 玩家的道具列表
    List<Equip>                 EquipList                   { get; }                            // 玩家的装备列表
    List<Fragment>              FragmentList                { get; }                            // 玩家的碎片列表
    List<ItemData>              PropList_Gold               { set; get; }                       // 从背包中读取<金币道具>信息(商店兑换专用)
    List<PMDMessage>            HornMsgs                    { set; get; }                       // 主界面的喇叭信息

    //--------------------------------------------------------------------------------------------------------------

    bool IsGuide { set; get; }                                              // 是否强制引导
    bool IsWeakGuide { set; get; }                                          // 是否弱引导
    bool IsRecharged { set; get; }                                          // 是否已充值
    bool IsGetFirstRechargeReward { set; get; }                             // 是否领取首充奖励
    bool IsFirstLogIn { set; get; }                                         // 是否首次充登录
    bool IsFirstBattle { set; get; }                                        // 是否第一次进入战斗
    bool IsClickActivity { set; get; }                                      // 是否点击或活动面板
    bool IsGiftActivity { set; get; }                                       // 是否直接跳过限时礼包
    bool AddHero(IHeroData hero, bool NeedChkpro = true);                   // 往已召唤英雄里添加一个英雄

    bool IsShowed0 { set; get; }                                            // 引导序列0
    bool IsShowed1 { set; get; }                                            // 引导序列1
    bool IsShowed2 { set; get; }                                            // 引导序列2
    bool IsShowed3 { set; get; }                                            // 引导序列3
    bool IsShowed4 { set; get; }                                            // 引导序列4
    bool IsShowed5 { set; get; }                                            // 引导序列5
    bool IsShowed6 { set; get; }                                            // 引导序列6
    bool IsShowed7 { set; get; }                                            // 引导序列7
    bool IsShowed8 { set; get; }                                            // 引导序列8

    void UpBattle                   (PosNumType inPosNum, IHeroData hero, BattleType map);                                      /// 上战斗阵容
    void DownBattle                 (PosNumType inPosNum, BattleType map);                                                      /// 下战斗阵容
    void ExChangeBattlePosition     (PosNumType inPosNum1, PosNumType inPosNum2, BattleType map);                               /// 战斗位置互换
    void SaveLineUpFile             (Dictionary<PosNumType,IHeroData> LineUpDic, BattleType lineUpName);                        /// 根据阵容类型保存阵容到本地

}
///-------------------------------------------------------------------------------------------------------------------------/// <summary> 角色(玩家)数据 </summary>
public class                        PlayerData                      : IPlayer      
{

    #region================================================||   StartRoot   == < 启动系统>      ||<FourNode>================================================
    public bool                     IsFirstLogIn                { set { this._IsFirstLogIn  = value; }  get { return this._IsFirstLogIn; } }            /// 是否首次登录                       
    public bool                     IsFirstBattle               { set { this._IsFirstBattle = value; }  get { return this._IsFirstBattle; } }           /// 是否进入开场战斗场景               

    public int                      PlayerID                    { set { this._PlayerID      = value; }  get { return this._PlayerID; } }                /// 玩家ID             
    public int                      GameServerID                { set { this._GameServerID  = value; }  get { return this._GameServerID; } }            /// 游戏服务器ID      
    public int                      ServerCenterID              { set { this._ServerCenterID = value; } get { return this._ServerCenterID; } }          /// 中心服务器ID   
    public int                      RegTime                     { set { this._RegTime = value; }        get { return this._RegTime; } }                 /// 玩家注册时间戳    
                     
    public int                      LogInGameDays               { set { this._LogInGameDays = value; }  get { return this._LogInGameDays; } }           /// 累计登录游戏天数                  

    public string                   PlayerName                  { set { this._PlayerName = value; }     get { return this._PlayerName; } }              /// 玩家名称                   
    public string                   ServerName                  { set { this._ServerName = value; }     get { return this._ServerName; } }              /// 服务器名称                 
    public string                   PublicInfo                  { set { this._PublicInfo = value; }     get { return this._PublicInfo; } }              /// 公告信息      

    #region------------------------------------------------||   StartRootPrivate -- 私有模块< 函数_声明 >      ||<FourNode>----------------------------------
    private bool                _IsFirstLogIn           = true;                                                                                 /// 是否首次登录 
    private bool                _IsFirstBattle          = true;                                                                                 /// 是否进入开场战斗场景  

    private int                 _PlayerID               = 0;                                                                                    /// 玩家ID
    private int                 _ServerCenterID         = 0;                                                                                    /// 登录服ID
    private int                 _GameServerID           = 0;                                                                                    /// 游戏服ID
    private int                 _RegTime                = 0;                                                                                    /// 注册时间

    private int                 _LogInGameDays          = 0;                                                                                    /// 登录游戏天数  
              
    private string              _PublicInfo             = "";                                                                                   /// 公共信息
    private string              _PlayerName             = "";                                                                                   /// 玩家名称
    private string              _ServerName             = "";                                                                                   /// 游戏服务器名称

    #endregion
    #endregion

    #region================================================||   MainUI      == < 主界面系统>    ||<FourNode>================================================
                                                                                                                                                                ///<| ExtendModular           --  扩展模块>                                                                                                                                               
    public int                      FriendStaminaCount          { set { this._FriendStaminaCount = value; }     get { return this._FriendStaminaCount; }}       /// 好友系统领取的累计体力               
    public int                      GuildID                     { set { this._GuildID = value; }                get { return this._GuildID; } }                 /// 公会ID     
                             
    public string                   GuildName                   { set { this._GuildName = value; }              get { return this._GuildName; } }               /// 公会名称  
    public List<PMDMessage>         HornMsgs                    { set { this.HornMsgs = value; }                get { return this.HornMsgs; } }                 /// 喇叭消息 
    public Dictionary<int, int>     WingList                    { get { return this._WingList; } }                                                              /// 翅膀列表        
        
                      
                                                                                                                                                                ///<| KernelModular           -- 核心模块-物品>
    public  List<Soul>              GetHeroSoulList             { get { return this._GetHeroSoulList; } }                                                       /// 已经获取英雄魂石列表                   
    public  List<Equip>             EquipList                   { get { return this._EquipList; } }                                                             /// 玩家的装备列表                         
    public  List<Fragment>          FragmentList                { get { return this._FragmentList; } }                                                          /// 玩家的碎片列表                         
    public  List<Prop>              PropList                    { get { return this._PropList; } }                                                              /// 玩家的道具列表             
                    
    public  List<ItemData>          PropList_Gold               { set { _PropList_Gold = value; }               get { return _PropList_Gold; } }                /// 从背包中读取<金币道具>-商店兑换专用      


                                                                                                                                                                ///<| BattleMapTypeModular    --  关卡与战斗地图>
    public CheckPointObject         NormalMaxCheckPointHistory  { set { _NormalMaxCheckPointHistory = value; }  get { return _NormalMaxCheckPointHistory; } }   /// 普通最大关卡记录
    public CheckPointObject         EliteMaxCheckPointHistory   { set { _EliteMaxCheckPointHistory  = value; }  get { return _EliteMaxCheckPointHistory; } }    /// 精英最大关卡记录
    public CheckPointObject         CurrentCheckPoint           { set { _CurrentCheckPoint = value; }           get { return _CurrentCheckPoint; } }            /// 当前关卡ID

    public Dictionary<int, int>     GetNormalCheckPointStars    { get { return this._GetNormalCheckPointStars; } }                                              /// 普通模式下获取的关卡星数      
    public Dictionary<int, int>     GetEliteCheckPointStars     { get { return this._GetNormalCheckPointStars; } }                                              /// 精英模式下获取的关卡星数      
    public IJJCPlayer               JJCInfo                     { set { this._JJCInfo = value; } get { return this._JJCInfo; } }                                /// 竞技场信息                 


    public void                     UpBattle                    ( PosNumType inPosNum, IHeroData Hero,BattleType map)                                           // 战斗阵容添加 英雄               
    {
        foreach(var Item in BattleTypeToLineUp(map))                                                                        // 检测阵容相同英雄_有则删除  
        {
            if(Item.Value.ID == Hero.ID)
            {
                BattleTypeToLineUp(map).Remove(Item.Key);
                break;
            }
        }
        BattleTypeToLineUp(map).Add(inPosNum,Hero);                                                                         // 阵容添加英雄
        return;
    }
    public void                     DownBattle                  ( PosNumType inPosNum, BattleType map)                                                          // 战斗阵容删除 英雄               
    {
        foreach (var Item in BattleTypeToLineUp(map))
        {
            if(Item.Key == inPosNum)
            {
                BattleTypeToLineUp(map).Remove(Item.Key);
                break;
            }  
        }
    }
    public void                     ExChangeBattlePosition      ( PosNumType inPosNum1, PosNumType inPosNum2, BattleType map)                                   // 战斗阵容英雄互换 <英雄数据,互换> 
    {
        Dictionary<PosNumType, IHeroData> LineUpHeroDic         = BattleTypeToLineUp(map);                                  // 获取战斗类型 阵容

        if( !LineUpHeroDic.ContainsKey(inPosNum1) )                                                                         // PosNum null 报错
        { 
            Debug.LogError("this Position No Exist !");
            return;
        }
                                                                                                                            ///<| 交互字典value[英雄数据] |>
        IHeroData                   TheHero_D                   = null;
        TheHero_D                                               = LineUpHeroDic[inPosNum1];
        LineUpHeroDic[inPosNum1]                                = LineUpHeroDic[inPosNum2];
        LineUpHeroDic[inPosNum2]                                = TheHero_D;
        return;
    }
    public void                     SaveLineUpFile              ( Dictionary<PosNumType,IHeroData>LineUpDic,BattleType LineUpTpye)                              // 根据阵容类型保存到本地文件       
    {
        Debug.Log                   ("======== SaveFileToLocal: 保存阵容到本地文件! ");

        string                      HeroIDFilePath              = PlayerID + PlayerName + LineUpTpye.ToString() + "_HeroList";      /// 阵容英雄列表 文件名
        string                      PosFilePath                 = PlayerID + PlayerName + LineUpTpye.ToString() + "_PosList";       /// 阵容英雄列表 文件名
        List<int>                   HeroIDList                  = new List<int>();                                                  /// 英雄ID列表
        List<int>                   PosList                     = new List<int>();                                                  /// 位置Pos列表

        foreach(var Item in LineUpDic)
        {
            HeroIDList.Add          (Item.Value.ID);
            PosList.Add             ((int)Item.Key);
        } 
        Util.SaveFile               (HeroIDList, HeroIDFilePath);                                                                   /// 英雄ID列表  保存到本地文件
        Util.SaveFile               (PosList, PosFilePath);                                                                         /// 位置Pos列表 保存到本地文件 
    }
    public Dictionary<PosNumType,IHeroData>  GetLineUpFile      ( BattleType lineUpTpye)                                                                        // 从本地读取保存的阵容            
    {
        List<int>                   ThePosNumList               = new List<int>();                                                  /// 英雄坐标 位置类型
        List<int>                   TheHeroIDList               = new List<int>();                                                  /// 英雄ID   列表
        Dictionary<PosNumType, IHeroData> TheHeroDataDic        = new Dictionary<PosNumType, IHeroData>();                          /// 英雄数据 列表

        string                      HeroFilePath                = PlayerID + PlayerName + lineUpTpye.ToString() + "_HeroList";      /// 玩家ID+玩家名+战斗类型_HeroList
        string                      PosFilePath                 = PlayerID + PlayerName + lineUpTpye.ToString() + "_PosList";       /// 玩家ID+玩家名+战斗类型_PosList

        TheHeroIDList                                           = Util.GetLineUpIDFile(HeroFilePath);                               /// 加载 <|英雄ID|>   列表
        ThePosNumList                                           = Util.GetLineUpPosFile(PosFilePath);                               /// 加载 <|阵容位置|> 列表

        for (int i = 0; i < TheHeroIDList.Count; i++)
        {
            foreach (var Item in HeroList)
            {
                if (Item.ID == TheHeroIDList[i])                TheHeroDataDic.Add((PosNumType)ThePosNumList[i],Item);
            }
        }
        return                      TheHeroDataDic;
    }
    public Dictionary<PosNumType, IHeroData> BattleTypeToLineUp ( BattleType Map)                                                                               // 根据地图类型返回阵容            
    {
        switch (Map)
        {
            case BattleType.CheckPoint:              //主线战役地图
                return CheckPointLineUp;
            case BattleType.JJC:                     //竞技场地图
            case BattleType.JJCLevel:                //竞技场晋级挑战地图
            case BattleType.FriendWar:               //好友挑战地图
                return JJCLineUp;
            case BattleType.JJCDefence:              //竞技场防御地图
                return JJCDefanceLineUp;
            case BattleType.ParadiseRoad:            //天堂之路地图
                return ParadiseRoadLineUp;
            case BattleType.SecretTower:             //秘境塔地图
                return SecretTowerLineUp;
            case BattleType.DragonTrialIce:          //巨龙试炼冰龙地图
                return DragonTrialIceLineUp;
            case BattleType.DragonTrialFire:         //巨龙试炼火龙地图
                return DragonTrialFireLineUp;
            case BattleType.DragonTrialThunder:      //巨龙试炼雷龙地图
                return DragonTrialThunderLineUp;
            case BattleType.MonsterWarPhy:           //巨兽囚笼物理地图
                return MonsterWarPhyLineUp;
            case BattleType.MonsterWarMagic:         //巨兽囚笼魔法地图
                return MonsterWarMagicLineUp;
            case BattleType.Guild:                   //公会地图
                return GuildWarLineUp;
            default:
                return null;
        }

    }


                                                                                                                                                                ///<| BusinessModular     --  运营模块-经济系统>
    public bool                     IsClickActivity             { set { _IsClickActivity    = value; }          get { return _IsClickActivity; } }              /// 是否点击过活动面板  
    public bool                     IsGiftActivity              { set { _IsGiftActivity     = value; }          get { return _IsGiftActivity; } }               /// 是否跳过限时礼包                   

    public bool                     IsRecharged                 { set { _IsRecharged        = value; }          get { return _IsRecharged; } }                  /// 是否曾经充值                       
    public bool                     IsGetFirstRechargeReward    { set { _IsGetFirstRechargeReward = value; }    get { return _IsGetFirstRechargeReward; } }     /// 是否领取首充奖励                   

    public int                      DiamondOfRecharge           { set { _DiamondOfRecharge = value; }           get { return _DiamondOfRecharge; } }            /// 钻石充值金额（VIP经验）            
    public int                      MonthCardRemainingDays      { set { _MonthCardRemainingDays = value; }      get { return _MonthCardRemainingDays; } }       /// 月卡剩余天数                      


    #region------------------------------------------------||   MainUIPrivate -- 私有模块< 函数_声明 >      ||<FourNode>------------------------------------------------
                                                                                                                                                    ///<| ExtendModular           --  扩展模块> 
    private int                     _GuildID                        = 0;                                                                            /// 工会ID
    private string                  _PlayerHeadIconName             = "";                                                                           /// 玩家头像名称
    private string                  _PlayerCircleIconName           = "";                                                                           /// 玩家圆形图标名
    private string                  _GuildName                      = "";                                                                           /// 工会名称

                                                                                                                                                    ///<| KernelModular      -- 核心模块- 物品+英雄UI>
    private List<Soul>              _GetHeroSoulList                = new List<Soul>();                                                             /// 已经获取英雄魂石列表
    private List<Equip>             _EquipList                      = new List<Equip>();                                                            /// 玩家的装备列表
    private List<Fragment>          _FragmentList                   = new List<Fragment>();                                                         /// 玩家的碎片列表
    private List<Prop>              _PropList                       = new List<Prop>();                                                             /// 玩家的道具列表 
    private List<ItemData>          _PropList_Gold                  = new List<ItemData>();                                                         /// 从背包中读取<金币道具>-商店兑换专用 

                                                                                                                                                    ///<| BattleMapTypeModular     --  关卡与战斗地图>
    private CheckPointObject        _NormalMaxCheckPointHistory     = new CheckPointObject();
    private CheckPointObject        _EliteMaxCheckPointHistory      = new CheckPointObject();
    private CheckPointObject        _CurrentCheckPoint              = null;
                                                                                                                                                
    private IJJCPlayer              _JJCInfo                        = new JJCPlayerData();                                                                  
    private IHeroData               _PlayerHero                     = new HeroData();                                                                     
    private IHeroData               _PlayerMerc                     = new HeroData();

    private Dictionary<PosNumType, IHeroData> CheckPointLineUp                = new Dictionary<PosNumType, IHeroData>();                            /// 主线战役阵容
    private Dictionary<PosNumType, IHeroData> JJCLineUp                       = new Dictionary<PosNumType, IHeroData>();                            /// 竞技场阵容
    private Dictionary<PosNumType, IHeroData> JJCDefanceLineUp                = new Dictionary<PosNumType, IHeroData>();                            /// 竞技场防御阵容
    private Dictionary<PosNumType, IHeroData> ParadiseRoadLineUp              = new Dictionary<PosNumType, IHeroData>();                            /// 天堂之路阵容

    private Dictionary<PosNumType, IHeroData> SecretTowerLineUp               = new Dictionary<PosNumType, IHeroData>();                            /// 秘境塔阵容
    private Dictionary<PosNumType, IHeroData> DragonTrialIceLineUp            = new Dictionary<PosNumType, IHeroData>();                            /// 巨龙试炼冰龙阵容
    private Dictionary<PosNumType, IHeroData> DragonTrialFireLineUp           = new Dictionary<PosNumType, IHeroData>();                            /// 巨龙试炼火龙阵容
    private Dictionary<PosNumType, IHeroData> DragonTrialThunderLineUp        = new Dictionary<PosNumType, IHeroData>();                            /// 巨龙试炼雷龙阵容

    private Dictionary<PosNumType, IHeroData> MonsterWarPhyLineUp             = new Dictionary<PosNumType, IHeroData>();                            /// 巨兽囚笼物理阵容
    private Dictionary<PosNumType, IHeroData> MonsterWarMagicLineUp           = new Dictionary<PosNumType, IHeroData>();                            /// 巨兽囚笼魔法阵容
    private Dictionary<PosNumType, IHeroData> GuildWarLineUp                  = new Dictionary<PosNumType, IHeroData>();                            /// 公会战争阵容


                                                                                                                                                    ///<| ExtendModular   --  扩展模块> 
    private int                     _FriendStaminaCount             = 0;                                                                            /// 已经领取好友体力累计
    private List<PMDMessage>        _HornMsgs                       = new List<PMDMessage>();                                                       /// 喇叭消息列表(跑马灯)


                                                                                                                                                    ///<| BusinessModular --  运营模块-经济系统>
    private bool                    _IsGetFirstRechargeReward       = false;                                                                        /// 是否领取首充奖励
    private bool                    _IsRecharged                    = false;                                                                        /// 是否已充值
    private bool                    _IsClickActivity                = false;                                                                        /// 是否点击活动
    private bool                    _IsGiftActivity                 = false;                                                                        /// 是否点礼包活动

    private int                     _DiamondOfRecharge              = 0;                                                                            /// 充值钻石数量
    private int                     _MonthCardRemainingDays         = 0;                                                                            /// 月卡天数
    #endregion
    #endregion

    #region================================================||   PublicSys   == < 通用系统>      ||<FourNode>================================================
                                                                                                                                                    /// <|HeroSys     --  英雄系统|>

    public  int                     ClickHeroID             { set { _ClickHeroID = value; }             get { return _ClickHeroID; } }              /// 选择的英雄ID                               
    public IHeroData                PlayerRoleHero                                                                                                  //  玩家主角英雄       
    {
        set { this._PlayerHero = value; }
        get
        {
            foreach (IHeroData item in this._Heros)
            {
                if (item.ID == 100001 || item.ID == 100002 || item.ID == 100003)
                {
                    return item;
                }
            }
            Debug.Log("没有找到主角英雄");
            return null;
        }
    }
    public IHeroData                PlayerMerc              { set { _PlayerMerc = value; }              get { return _PlayerMerc; } }               /// 玩家佣兵                   
    public List<IHeroData>          HeroList                { get { return _Heros; } }                                                              /// 已召唤英雄列表   

    public bool                     AddHero                 (IHeroData hero, bool NeedChkPro = true)                                                /// 往已召唤英雄列表里添加一个英雄      
    {
        if (!HeroList.Contains(hero))
        {   HeroList.Add(hero);         }
        if(NeedChkPro)
        {
            List<IHeroData>     Slodiers    = new List<IHeroData>();      //战士
            List<IHeroData>     Assassins   = new List<IHeroData>();      //刺客
            List<IHeroData>     Magicians   = new List<IHeroData>();      //魔法师
            List<IHeroData>     Snipers     = new List<IHeroData>();      //射手
            List<IHeroData>     Supplies    = new List<IHeroData>();      //辅助
            foreach (IHeroData  item in this._Heros)
            {
                Configs_HeroData Data = Configs_Hero.sInstance.GetHeroDataByHeroID(item.ID);
                if(Configs_Hero.sInstance.GetHeroDataByHeroID(item.ID).HeroType == 4)
                {
                    continue;
                }
                if      ( Data.Profession2 ==1)
                {
                    Slodiers.Add(item);
                }
                else if ( Data.Profession2 ==2)
                {
                    Assassins.Add(item);
                }
                else if ( Data.Profession2==3)
                {
                    Magicians.Add(item);
                }
                else if(Data.Profession2==4)
                {
                    Snipers.Add(item);
                }
                else if(Data.Profession2==5)
                {
                    Supplies.Add(item);
                }
            }
            ProLevel(Slodiers);
            ProLevel(Assassins);
            ProLevel(Magicians);
            ProLevel(Snipers);
            ProLevel(Supplies);
        }

        return true;
    }
    public IHeroData                GetHeroByID             (int inHeroID)                                                                          /// 获得对应ID的英雄     
    {
        foreach(var Item in HeroList)
        {    if(Item.ID == inHeroID )   return Item;}
        return null;
    }  
    public Dictionary<int, int>     GetHeroFragmentList     ()                                                                                      //  获取英雄碎片列表     
    { return _GetHeroFragmentList; }


                                                                                                                                                    /// <|PlayerInfo    -- 玩家信息|>
                                                                                                                                                    
    public int                      SkillPoint              { set { _SkillPoint         = value; }      get { return this._SkillPoint; } }          /// 技能点数                          
    public int                      PlayerCurrentStamina    { set { _PlayerCurrentStamina = value; }    get { return _PlayerCurrentStamina; } }     /// 玩家当前体力值               
    public int                      BuyedStaminaTimes       { set { _BuyedStaminaTimes  = value; }      get { return this._BuyedStaminaTimes; } }   /// 已购买体力次数   
    public int                      BuyedCoinsTimes         { set { _BuyedCoinsTimes    = value; }      get { return this._BuyedCoinsTimes; } }     /// 已购买金币次数       
                     
    public int                      BuyedSkillTimes         { set { _BuyedSkillTimes    = value; }      get { return this._BuyedSkillTimes; } }     /// 已购买技能次数                    
    public int                      LvRewardGot             { set { _LvRewardGot        = value; }      get { return this._LvRewardGot; } }         /// 已领取等级奖励                    
    public ObscuredInt              PlayerLevel             { set { _PlayerLevel        = value; }      get { return this._PlayerLevel; } }         /// 玩家等级                      
    public ObscuredInt              PlayerVIPExp            { set { _PlayerVIPExp       = value; }      get { return this._PlayerVIPExp; } }        /// 玩家VIP经验      
                     
    public ObscuredInt              PlayerVIPLevel                                                                                                  //  玩家VIP等级         
    {
        set { }
        get
        {
            if (PlayerVIPExp < Configs_VIP.sInstance.mVIPDatas[1].Experience)
                return 0;
            if (PlayerVIPExp > Configs_VIP.sInstance.mVIPDatas[15].Experience)
                return 15;
            for (int i = 1; i < 20; i++)
            {
                if (!Configs_VIP.sInstance.mVIPDatas.ContainsKey(i))
                    return 0;
                if (!Configs_VIP.sInstance.mVIPDatas.ContainsKey(i) && PlayerVIPExp >= Configs_VIP.sInstance.mVIPDatas[i - 1].Experience)
                    return i - 1;
                if (PlayerVIPExp < Configs_VIP.sInstance.mVIPDatas[i].Experience && PlayerVIPExp >= Configs_VIP.sInstance.mVIPDatas[i - 1].Experience)
                    return i - 1;
            }
            return 0;
        }
    }
    public ObscuredLong             PlayerCoins             { set { _PlayerCoins        = value; }      get { return this._PlayerCoins; } }         /// 玩家金币                      
    public ObscuredLong             PlayerDiamonds          { set { _PlayerDiamonds     = value; }      get { return this._PlayerDiamonds; } }      /// 玩家钻石       
    public long                     MonthCardTime           { set { _MonthCardTime      = value; }      get { return this._MonthCardTime; } }       /// 月卡时间戳                        

    public float                    PlayerCurrentExp        { set { _PlayerCurrentExp   = value; }      get { return this._PlayerCurrentExp; } }    /// 玩家当前经验                     

    public string                   PlayerHeadIconName                                                                                              //  玩家头像图标资源名称  
    {
        set { this._PlayerHeadIconName = value; }
        get
        {
            if (_PlayerHeadIconName == "")
                return Configs_Hero.sInstance.GetHeroDataByHeroID(this.PlayerRoleHero.ID).head84;
            return Configs_TroopsHeadS.sInstance.GetTroopsHeadSDataByTroopsID(Util.ParseToInt(this._PlayerHeadIconName)).head70;
        }
    }
    public string                   PlayerCircleIconName                                                                                            //  玩家圆形头像资源名称  
    { set { this._PlayerCircleIconName = value; } get { return this._PlayerCircleIconName; } }


                                                                                                                                                    /// <|GuideControl    -- 新手引导|> 
                                                                                                                                                    
    public  bool                    IsGuide                 { set { this._IsGuide = value; }            get { return this._IsGuide; } }             /// 是否强制引导                       

    public  bool                    IsWeakGuide             { set { this._IsWeakGuide = value; }        get { return this._IsWeakGuide; } }         /// 是否弱引导                         

    public int[]                    WeakGuideStates         { get { return this._WeakGuideStates; } }                                               /// 弱引导状态（0：未完成 , 1:完成）   

    public int                      GuideProces             { set { this._GuideProces = value; }        get { return this._GuideProces; } }         /// 引导进度                          
    public int                      WeakGuideID             { set { this._GuildID = value; }            get { return this._GuildID; } }             /// 弱引导ID   
           

    public bool                     IsShowed0               { set { this._IsShowed0 = value; } get { return this._IsShowed0; } }                    /// 弱引导状态
    public bool                     IsShowed1               { set { this._IsShowed1 = value; } get { return this._IsShowed1; } }
    public bool                     IsShowed2               { set { this._IsShowed2 = value; } get { return this._IsShowed2; } }
    public bool                     IsShowed3               { set { this._IsShowed3 = value; } get { return this._IsShowed3; } }
    public bool                     IsShowed4               { set { this._IsShowed4 = value; } get { return this._IsShowed4; } }
    public bool                     IsShowed5               { set { this._IsShowed5 = value; } get { return this._IsShowed5; } }
    public bool                     IsShowed6               { set { this._IsShowed6 = value; } get { return this._IsShowed6; } }
    public bool                     IsShowed7               { set { this._IsShowed7 = value; } get { return this._IsShowed7; } }
    public bool                     IsShowed8               { set { this._IsShowed8 = value; } get { return this._IsShowed8; } }
             

    #region================================================||   PublicSysPrivate -- 私有模块< 函数_声明 >      ||<FourNode>================================================
                                                                                                                                                    /// <|HeroSys     --  英雄系统>
                                                                                                                                                     
    private int                     _ClickHeroID                    = 0;                                                                            /// 当前点击英雄ID

    private IHeroData               _GetHeroByID;                                                                                                   /// ID 获取英雄数据

    private List<IHeroData>         _Heros                          = new List<IHeroData>();                                                        /// 英雄列表

    private Dictionary<int, int>    _GetHeroFragmentList            = new Dictionary<int, int>();                                                   /// 英雄碎片列表
    private Dictionary<int, int>    _GetNormalCheckPointStars       = new Dictionary<int, int>();                                                   /// 普通关卡星级
    private Dictionary<int, int>    _GetEliteCheckPointStars        = new Dictionary<int, int>();                                                   /// 精英关卡星级
    private Dictionary<int, int>    _WingList                       = new Dictionary<int, int>();                                                   /// 翅膀列表

    private void                    ProLevel                        (List<IHeroData> Temp)                                                          /// 职业等级      
    {
        foreach (IHeroData item in Temp)
            item.ProLevel = Temp.Count > 9 ? 9 : Temp.Count;
    }


                                                                                                                                                    /// <|PlayerControl   -- 玩家信息>  
    private int                     _PlayerCurrentStamina           = 0;                                                                            /// 玩家当前体力 
    private int                     _PlayerLevel                    = 1;                                                                            /// 玩家等级
    private int                     _PlayerVIPExp                   = 0;                                                                            /// 玩家VIP经验
    private int                     _SkillPoint                     = 0;                                                                            /// 技能点数

    private int                     _LvRewardGot                    = 0;                                                                            /// 已经领取等级奖励
    private int                     _BuyedStaminaTimes              = 0;                                                                            /// 已经购买体力次数(日常)
    private int                     _BuyedCoinsTimes                = 0;                                                                            /// 已购买金币次数(日常)
    private int                     _BuyedSkillTimes                = 0;                                                                            /// 已购买技能次数(日常)

    private long                    _MonthCardTime                  = 0;                                                                            /// 月卡时间
    private long                    _PlayerCoins                    = 0;                                                                            /// 玩家金币
    private long                    _PlayerDiamonds                 = 0;                                                                            /// 玩家钻石

    private float                   _PlayerCurrentExp               = 0;                                                                            /// 玩家当前经验


                                                                                                                                                    /// <|GuideControl    -- 新手引导> 
    private bool                    _IsGuide                        = false;                                                                        /// 是否强制引导
    private bool                    _IsWeakGuide                    = false;                                                                        /// 是否弱引导

    private int                     _GuideProces                    = 0;                                                                            /// 引导进度 
    private int                     _WeakGuideID                    = 0;                                                                            /// 弱引导ID
    private int[]                   _WeakGuideStates                = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };                                 /// 弱引导数组

    private bool                    _IsShowed0                      = false;                                                                        /// 弱引导状态
    private bool                    _IsShowed1                      = false;
    private bool                    _IsShowed2                      = false;
    private bool                    _IsShowed3                      = false;
    private bool                    _IsShowed4                      = false;
    private bool                    _IsShowed5                      = false;
    private bool                    _IsShowed6                      = false;
    private bool                    _IsShowed7                      = false;
    private bool                    _IsShowed8                      = false;
    #endregion
    #endregion

}
public class                        CheckPointObject                                                                                                // 关卡对象配置    
{
    public int                      ID                              = 0;                                                                            /// 配置表ID
    public ChapterType              type                            = ChapterType.Normal;                                                           /// 关卡类型
}