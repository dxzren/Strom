using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> 关卡系统数据 </summary>
public class CheckPointSys : ICheckPointSys
{
    private int                 _chapterTotal               = 14;
    private int                 _chapterGoToID              = 0;
    private int                 _chapterID                  = 0;
    private int                 _currentCheckPointID        = 0;
    private int                 _gotNormalStarBoxAward      = 0;
    private int                 _gotEliteStarBoxAward       = 0;
    private int                 _sweepTimes                 = 0;
    private int                 _sweepLimit                 = 0;

    private string              _checkPointPanelName        = "";

    private bool                _stopGuide                  = false;
    private bool                _isEliteGuide               = false;
    private bool                _isSweeping                 = false;

    private ChapterType         _checkPointType             = ChapterType.Normal;

    private List<int>           _GotEliteStartAwardList     = new List<int>();
    private List<ItemData[]>    _SweepResult                = new List<ItemData[]>();
    private List<CHECKPOINT_DATA_Elite>_EliteCheckPointList = new List<CHECKPOINT_DATA_Elite>();
    private Dictionary<int, int> _EliteChallangeTimesDic    = new Dictionary<int, int>();

    public int chapterTotal                                                 // 章节总数
    {
        set { this._chapterTotal = value; }
        get { return this._chapterTotal; }
    }
    public int chapterGoToID                                                // 通往章节关卡ID                     
    {
        set { this._chapterGoToID = value; }
        get { return this._chapterGoToID; }
    }
    public int chapterID                                                    // 章节ID                            
    {
        set { this._chapterID = value; }
        get { return this._chapterID; }
    }
    public int currentCheckPointID                                          // 当前关卡ID                         
    {
        set { this._currentCheckPointID = value; }
        get { return this._currentCheckPointID; }
    }
    public int gotNormalStarBoxAward                                        // 已经领取普通星级宝箱奖励最大等级     
    {
        set { this._gotNormalStarBoxAward = value; }
        get { return this._gotEliteStarBoxAward; }
    }
    public int gotEliteStarBoxAward                                         // 已经领取精英星级宝箱奖励最大等级     
    {
        set { this._gotEliteStarBoxAward = value; }
        get { return this._gotEliteStarBoxAward; }
    }
    public int sweepTimes                                                   // 扫荡次数                           
    {
        set { this._sweepTimes = value; }
        get { return this._sweepTimes; }
    }
    public int sweepLimit                                                   // 扫荡次数上限                       
    {
        set { this._sweepLimit = value; }
        get { return this._sweepLimit; }
    }

    public string checkPointPanelName                                       // 关卡面板名称                       
    {
        set { this._checkPointPanelName = value; }
        get { return this._checkPointPanelName; }
    }

    public bool stopGuide                                                   // 是否停止引导                       
    {
        set { this._stopGuide = value; }
        get { return this._stopGuide; }
    }

    public bool isEliteGuide                                                // 是否精英关卡引导                    
    {
        set { this._stopGuide = value; }
        get { return this._stopGuide; }
    }

    public bool isSweeping                                                  // 避免重复扫荡                        
    {
        set { this._isSweeping = value; }
        get { return this._isSweeping; }
    }

    public ChapterType CurrentCP_Type                                       // 章节类型                            
    {
        set { this._checkPointType = value; }
        get { return this._checkPointType; }
    }
    public List<int> GotEliteStartAwardList()                               // 已领取精英章节奖励
    { return this._GotEliteStartAwardList; }

    public List<ItemData[]> SweepResult                                     // 扫荡结果(物品列表)                   
    {
        set { this._SweepResult = value; }
        get { return this._SweepResult; }
    }

    public List<CHECKPOINT_DATA_Elite> EliteCheckPointList                  // 精英关卡挑战次数字典                
    {
        set { this._EliteCheckPointList = value; }
        get { return this._EliteCheckPointList; }
    }

    public Dictionary<int,int> EliteChallangeTimesDic                       // 精英关卡挑战次数字典                
    {
        set { this._EliteChallangeTimesDic = value; }
        get { return this._EliteChallangeTimesDic; }
    }
}