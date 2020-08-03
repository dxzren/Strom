using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartData : IStartData
{
    private string _account        = "";
    private string _password       = "";
    private string _channelID      = "";
    private string _randomName     = "";
    private string _gameServerIP   = "";
    private string _checkCode      = "";
    private string _gameServerName = "";
    private string _testIP         = "";
    private string _tempIP         = "";

    private int _heroID         = 0;
    private int _gameServerPort = 0;
    private int _centerServerID = 0;
    private int _gameServerID   = 0;
    private int _createTimes    = 0;
    private int _testPort       = 0;
    private int _tempPort       = 0;

    private bool _isBind        = false;
    private bool _isLocalAcc    = false;
    private bool _isStartBattle = false;

    private Dictionary<int, List<GameSrvInfo>> _gameServerList = new Dictionary<int, List<GameSrvInfo>>();
    private GameSrvInfo _gameServerSelected = new GameSrvInfo();
    private LOGIN_PlayerInfo _playerSelected = new LOGIN_PlayerInfo();

        
    public string account                                                                       // 账户号                            
    {
        set { this._account = value; }
        get { return this._account; }
    }
    public string password                                                                      // 账户密码                          
    {
        set { this._password = value; }
        get { return this._password; }
    }
    public string channelID                                                                     // 渠道ID(易接)                      
    {
        set { this._channelID = value; }
        get { return this._channelID; }
    }
    public string randomName                                                                    // 随机的名字                        
    {
        set { this._randomName = value; }
        get { return this._randomName; }
    }
    public string gameServerIP                                                                  // 登录返回的游戏服IP                
    {
        set { this._gameServerIP = value; }
        get { return this._gameServerIP; }
    }
    public string checkCode                                                                     // 校验码                            
    {
        set { this._checkCode = value; }
        get { return this._checkCode; }
    }
    public string gameServerName                                                                // 游戏服名称                        
    {
        set { this._gameServerName = value; }
        get { return this._gameServerName; }
    }
    public string testIP                                                                        // 测试服登录IP                      
    {
        set { this._testIP = value; }
        get { return this._testIP; }
    }
    public string tempIP                                                                        // 登录服返回的游戏服IP              
    {
        set { this._tempIP = value; }
        get { return this._tempIP; }
    }

    public int tempHeroID                                                                       // 选择的英雄ID                      
    {
        set { this._heroID = value; }
        get { return this._heroID; }
    }
    public int gameServerPort                                                                   // 游戏服端口                        
    {
        set { this._gameServerPort = value; }
        get { return this._gameServerPort; }
    }
    public int centerServerID                                                                   // 中心服登录ID                      
    {
        set { this._centerServerID = value; }
        get { return this._centerServerID; }
    }
    public int gameServerID                                                                     // 游戏服ID                          
    {
        set { this._gameServerID = value; }
        get { return this._gameServerID; }
    }
    public int createTimes                                                                       // 创建时间                          
    {
        set { this._createTimes = value; }
        get { return this._createTimes; }
    }
    public int testPort                                                                         // 测试服端口                        
    {
        set { this._testPort = value; }
        get { return this._testPort; }
    }
    public int tempPort                                                                         // 登录服返回的游戏服端口             
    {
        set { this._tempPort = value; }
        get { return this._tempPort; }
    }

    public bool isBind                                                                          // 是否绑定帐号                      
    {
        set { this._isBind = value; }
        get { return this._isBind; }
    }
    public bool isLocalAcc                                                                      // 是否存在本地帐号                  
    {
        set { this._isLocalAcc = value; }
        get { return this._isLocalAcc; }
    }

    public bool isStartBattle                                                                   // 是否开始初始战斗                  
    {
        set { this._isStartBattle = value; }
        get { return this._isStartBattle; }
    }

    public Dictionary<int, List<GameSrvInfo>> centerServerList                                  // 中心服列表                       
    {
        set { this._gameServerList = value; }
        get { return this._gameServerList; }
    }
    public GameSrvInfo gameServerSelected                                                       // 已选择游戏服信息                  
    {
        set { this._gameServerSelected = value; }
        get { return this._gameServerSelected; }
    }
    public LOGIN_PlayerInfo  playerSelected                                                     // 已选择玩家信息                    
    {
        set { this._playerSelected = value; }
        get { return this._playerSelected; }
    }
}
