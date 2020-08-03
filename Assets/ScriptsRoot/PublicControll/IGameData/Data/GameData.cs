using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameData : IGameData
{
                                                                                                                            ///<| 红点开关 >
    private bool                        _EMailSysRed                    = false;                                            // 1.邮件红点 
    private bool                        _MallSysRed                     = false;                                            // 2.商城红点 
    private bool                        _TaskSysRed                     = false;                                            // 3.任务红点
    private bool                        _ChatSysRed                     = false;                                            // 4.聊天红点 

    private bool                        _ActivitySysRed                 = false;                                            // 5.活动红点 
    private bool                        _MercSysRed                     = false;                                            // 6.佣兵红点 
    private bool                        _FriendSysRed                   = false;                                            // 7.好友红点
    private bool                        _PlayerSysRed                   = false;                                            // 8.玩家红点

    private bool                        _WingSysRed                     = false;                                            // 9.翅膀红点
    private bool                        _HeroSysRed                     = false;                                            // 10.英雄红点
                                                                                                                            /// <| 主界面系统

    private bool                        _IsExitPanel                    = false;
    private long                        _subServerT                     = 0;

    private List<PanelObj>              _StartPanelList                 = new List<PanelObj>();
    private List<PanelObj>              _MainPanelList                  = new List<PanelObj>();
    private List<PanelObj>              _BattlePanelList                = new List<PanelObj>();

    private Dictionary<string, UIAtlas> _AtlasList                      = new Dictionary<string, UIAtlas>();
    private Dictionary<int, int[][]>    _CheckPointDropDic              = new Dictionary<int, int[][]>();
    private Dictionary<MainSystem, SystemShowClick> _MainBtnStateDic    = new Dictionary<MainSystem, SystemShowClick>();

    public bool                         EMailSysRed                                                                         // 1.邮件红点               
    {
        set { this._EMailSysRed = value; }
        get { return this._EMailSysRed; }
    }
    public bool                         MallSysRed                                                                          // 2.商城红点               
    {
        set { this._MallSysRed = value; }
        get { return this._MallSysRed; }
    }
    public bool                         TaskSysRed                                                                          // 3.任务红点               
    {
        set { this._TaskSysRed = value; }
        get { return this._TaskSysRed; }
    }
    public bool                         ChatSysRed                                                                          // 4.聊天红点               
    {
        set { this._ChatSysRed = value; }
        get { return this._ChatSysRed; }
    }
    public bool                         ActivitySysRed                                                                      // 5.活动红点               
    {
        set { this._ActivitySysRed = value; }
        get { return this._ActivitySysRed; }
    }

    public bool                         MercSysRed                                                                          // 6.佣兵红点               
    {
        set { this._MercSysRed = value; }
        get { return this._MercSysRed; }
    }
    public bool                         FriendSysRed                                                                        // 7.好友红点               
    {
        set { this._FriendSysRed = value; }
        get { return this._FriendSysRed; }
    }
    public bool                         PlayerSysRed                                                                        // 8.玩家红点               
    {
        set { this.PlayerSysRed = value; }
        get { return this._PlayerSysRed; }
    }
    public bool                         WingSysRed                                                                          // 9.翅膀红点               
    {
        set { this._WingSysRed = value; }
        get { return this._WingSysRed; }
    }
    public bool                         HeroSysRed                                                                          // 10.英雄红点              
    {
        set { this._HeroSysRed = value; }
        get { return this._HeroSysRed; }
    }
    public bool                         IsExitPanel                                                                         // 是否弹出退出界面          
    {
        set { this._IsExitPanel = value; }
        get { return this._IsExitPanel; }
    }
    public long                         subServerT                                                                          // 同步服务器时间差          
    {
        set { this._subServerT = value; }
        get { return this._subServerT; }
    }

    public UIAtlas                      GetGameAtlas(string key)                                                            // 获取图集                 
    {
        if (!_AtlasList.ContainsKey(key))
        {
            return _AtlasList[key];
        }
        else
        {
            Util.ErrLog("Not found Atlas !" + key);
        }
        return null;
    }

    public List<PanelObj>               StartPanelList()                                                                    // 开始场景中的面板队列       
    {
        return _StartPanelList;
    }
    public List<PanelObj>               MainPanelList()                                                                     // 主场景中的面板队列         
    {
        return _MainPanelList;
    }
    public List<PanelObj>               BattlePanelList()                                                                   // 战斗场景中的面板队列       
    {
        return _BattlePanelList;
    }

    public void                         AtlasInit(string atlasPath)                                                         // 初始化图集                
    {
        if (!_AtlasList.ContainsKey(atlasPath))
        {
            _AtlasList.Add(atlasPath, Util.LoadAtlas(atlasPath));
        }
    }

    public Dictionary<int, int[][]>     CheckPointDropDic                                                                   // 战斗结果(本地保存)_key:ID value[0] 道具ID value[1] 未掉落数量
    {
        get { return this._CheckPointDropDic; }
    }
    public Dictionary<MainSystem, SystemShowClick> MainBtnStateDic                                                          // 主界面按钮状态            
    {
        set { this._MainBtnStateDic = value; }
        get { return this._MainBtnStateDic; }
    }
    public Dictionary<string, UIAtlas>  AtlasList                                                                           // 图集列表                 
    {
        set { this._AtlasList = value; }
        get { return this._AtlasList; }
    }
}
