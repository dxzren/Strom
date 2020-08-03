using UnityEngine;
using System.Collections;
using System.Collections.Generic;
///-------------------------------------------------------------------------------------------------------------------------/// <summary> 战斗阵容预置数据 </summary>
public interface IPersetLineupData
{
    int                                     configID                        { set; get; }                               /// 配置ID
    int                                     waveIndex                       { set; get; }                               /// 战斗波数

    bool                                    isRoleUpLineup                  { set; get; }                               /// 主角英雄是否上阵
    bool                                    isUpLevel                       { set; get; }                               /// 是否升级

    JJCPlayerData                           JJCplayer                       { set; get; }                               /// 竞技场玩家

    IHeroData                               TempHero_D                      { set; get; }                               /// 当前英雄数据
    BattleType                              BattleType                      { set; get; }                               /// 战斗地图(类型)

    Dictionary<PosNumType, IHeroData>       EnemyLineupList                 { set; get; }                               /// 敌方阵容     字典
    Dictionary<int ,GameObject>             AllHeroObjDic                   { set; get; }                               /// 所有英雄Obj  字典
    Dictionary <PosNumType, BattlePosSet>   BattlePosSetDic                 { set; get; }                               /// 战斗阵容预置 设置
}
///-------------------------------------------------------------------------------------------------------------------------///<summary> 战斗阵容预设数据 </summary>

public class PersetLineupData : IPersetLineupData
{
    private int                 _configID                               = 0;                                                /// 配置ID
    private int                 _waveIndex                              = 0;                                                /// 战斗波数

    private bool                _isRoleUpLineup                         = false;                                            /// 主角英雄是否上阵
    private bool                _isUpLevel                              = false;                                            /// 是否升级

    private JJCPlayerData       _JJCplayer                              = new JJCPlayerData();                              /// 竞技场玩家

    private IHeroData           _TempHero_D                             = new HeroData();                                   /// 当前英雄数据

    private BattleType          _BattleMap                              = BattleType.CheckPoint;                            /// 战斗地图(类型)
    private Dictionary<PosNumType, IHeroData>       _EnemyLineupList    = new Dictionary<PosNumType, IHeroData>();          /// 敌方阵容     字典
    private Dictionary<int, GameObject>             _AllHeroObjDic      = new Dictionary<int, GameObject>();                /// 所有英雄Obj  字典
    private Dictionary<PosNumType, BattlePosSet>    _BattlePosSetDic    = new Dictionary<PosNumType, BattlePosSet>();       /// 战斗阵容预置(我方阵容) 设置
    public int                  configID                                { set { _configID = value; }        get { return _configID; } }             /// 配置ID
    public int                  waveIndex                               { set { _waveIndex = value; }       get { return _waveIndex; } }            /// 战斗波数
    public bool                 isRoleUpLineup                          { set { _isRoleUpLineup = value; }  get { return _isRoleUpLineup; } }       /// 是否已经登陆阵容
    public bool                 isUpLevel                               { set { _isUpLevel = value; }       get { return _isUpLevel; } }            /// 是否升级
    public JJCPlayerData        JJCplayer                               { set { _JJCplayer = value; }       get { return _JJCplayer; } }            /// 竞技场玩家
    public IHeroData            TempHero_D                              { set { _TempHero_D = value; }      get { return _TempHero_D; } }           /// 当前英雄数据
    public BattleType           BattleType                              { set{ _BattleMap = value; }        get { return _BattleMap; } }            /// 战斗地图(类型)

    public Dictionary<PosNumType, IHeroData>        EnemyLineupList     { set { _EnemyLineupList = value; } get { return _EnemyLineupList; } }      /// 敌方阵容     字典
    public Dictionary<int,GameObject>               AllHeroObjDic       { set { _AllHeroObjDic = value; }   get { return _AllHeroObjDic; } }        /// 所有英雄Obj  字典
    public Dictionary<PosNumType, BattlePosSet>     BattlePosSetDic     { set { _BattlePosSetDic = value; }  get { return _BattlePosSetDic; } }     /// (我方阵容) 战斗预置数据 设置


}