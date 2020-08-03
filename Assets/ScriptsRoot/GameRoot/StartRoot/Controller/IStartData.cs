using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IStartData
{
    string account { set; get; }                                            // 账户号
    string password { set; get; }                                           // 账户密码
    string channelID { set; get; }                                          // 渠道ID(易接)
    string randomName { set; get; }                                         // 随机的名字
    string gameServerIP { set; get; }                                       // 登录返回的游戏服IP
    string checkCode { set; get; }                                          // 校验码
    string gameServerName { set; get; }                                     // 游戏服名称
    string testIP { set; get; }                                             // 测试服登录IP
    string tempIP { set; get; }                                             // 登录服返回的游戏服IP

    int tempHeroID { set; get; }                                            // 选择的英雄ID
    int gameServerPort { set; get; }                                        // 游戏服端口
    int centerServerID { set; get; }                                        // 中心服登录ID
    int gameServerID { set; get; }                                          // 游戏服ID
    int createTimes { set; get; }                                           // 创建角色次数
    int testPort { set; get; }                                              // 测试服端口
    int tempPort { set; get; }                                              // 登录服返回的游戏服端口

    bool isBind { set; get; }                                               // 是否绑定帐号
    bool isLocalAcc { set; get; }                                           // 是否存在本地帐号
    bool isStartBattle { set; get; }                                        // 是否开始初始战斗

    Dictionary<int,List <GameSrvInfo>> centerServerList { set; get; }       // 中心服列表 
    GameSrvInfo gameServerSelected { set; get; }                            // 已选择游戏服信息 
    LOGIN_PlayerInfo playerSelected { set; get; }                           // 已选择玩家信息

} 
public class ServerInfo                                                     // 服务器登录信息(LS,GS) < 本地文件 >
{
    public string serverName        = "";                                   /// 服务器名称 
    public string gameServerName    = "";                                   /// 游戏服名称
    public string IP                = "";                                   /// 服务器IP

    public int centerServerID       = 0;                                    /// 中心服ID
    public int gameServerID         = 0;                                    /// 游戏服ID
    public int port                 = 0;                                    /// 端口
}
public struct SavedAccountInfos                                             // 本地保存账号信息
{
    public string Account;                                                  /// 玩家账号
    public string Password;                                                 /// 玩家密码 
    public string Tempporary;                                               /// 是否临时 null: 临时
}