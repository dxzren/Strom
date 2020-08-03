using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using LinqTools;

public class JJCPlayerData : IJJCPlayer
{
    private int                 _PlayerType                         = 1;                                                    /// 玩家类型（1：真人，2：预设NPC）
    private int                 _ID                                 = 0;                                                    /// 玩家竞技场服务器ID
    private int                 _IconID                             = 0;                                                    /// 头像ID
    private int                 _Index                              = 0;                                                    /// 玩家的位置标记

    private int                 _Score                              = 0;                                                    /// 分数
    private int                 _JJCCoins                           = 0;                                                    /// 竞技场金币
    private int                 _JJCRefreshTimes                    = 0;                                                    /// 竞技场刷新次数
    private int                 _Rank                               = 0;                                                    /// 排名

    private int                 _GradeLevel                         = 0;                                                    /// 段位等级
    private int                 _GradeChallanged                    = 0;                                                    /// 已经挑战段位
    private int                 _TimesLeft                          = 0;                                                    /// 剩余挑战次数
    private int                 _TimesBuyed                         = 0;                                                    /// 已购买挑战次数

    private float               _RefreshStaminaCDSec                = 0f;                                                   /// 距离下一次刷新时间
    private float               _RefreshJJCCDSec                    = 0f;                                                   /// 距离一下一次刷新CD时间

    private string              _PlayerName                         = "";                                                   /// 玩家名称
    private string              _GuildID                            = "";                                                   /// 公会ID

    List<IHeroData>             _GetLineUPForDefance                = new List<IHeroData> ();                               /// 竞技场防御阵容
    public  int                 PlayerType              { set { this._PlayerType = value; }             get { return this._PlayerType; } }          ///
    public  int                 ID                      { set { this._ID = value; }                     get { return this._ID; } }                  ///  
    public  int                 IconID                  { set { this._IconID = value; }                 get { return this._IconID; } }              ///   
    public  int                 Index                   { set { this._Index = value; }                  get { return this._Index; } }               /// 

    public  int                 Score                   { set { this._Score = value; }                  get { return this._Score; } }               /// 
    public  int                 JJCCoins                { set { this._JJCCoins = value; }               get { return this._JJCCoins; } }            ///
    public  int                 JJCRefreshTimes         { set { this._JJCRefreshTimes = value; }        get { return this._JJCRefreshTimes; } }     ///
    public  int                 Rank                    { set { this._Rank = value; }                   get { return this._Rank; } }                ///

    public  int                 GradeLevel              { set { this._GradeLevel = value; }             get { return this._GradeLevel; } }          ///
    public  int                 GradeChallanged         { set { this._GradeChallanged = value; }        get { return this._GradeChallanged; } }     ///
    public  int                 TimesLeft               { set { this._TimesLeft = value; }              get { return this._TimesLeft; } }           ///
    public  int                 TimesBuyed              { set { this._TimesBuyed = value; }             get { return this._TimesBuyed; } }          ///

    public float                RefreshStaminaCDSec     { set { this._RefreshStaminaCDSec = value; }    get { return this._RefreshStaminaCDSec; } } ///
    public float                RefreshJJCCDSec         { set { this._RefreshJJCCDSec = value; }        get { return this._RefreshJJCCDSec; } }     ///

    public string               PlayerName              { set { this._PlayerName = value; }             get { return this._PlayerName; } }          ///
    public string               GuildID                 { set { this._GuildID = value; }                get { return this._GuildID; } }             ///
    public List<IHeroData>      GetLineUpForDefance     { set { this._GetLineUPForDefance = value; }    get { return this._GetLineUPForDefance; } } ///

}
