using System.Collections;
using System.Runtime.InteropServices;

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           Head                                                                    // 头文件                                    
{
    public short        size;
    public short        type1;
    public short        type2;
}

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]       
struct                  LOGIN_Heart                                                             // 心跳协议                                  
{
    public Head         Head;       
    public int          none;                                               /// 填充数据,无意义
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct                  ErrorCode                                                               // 错误码                                    
{
    public Head         Head;
    public int          code;                                               /// 错误码
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_LOGIN_PUBLIC_INFO                                                   // 返回公告信息                                  
{
    public Head         Head;
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 512)]
    public string       publicText;                                         /// 公告信息
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           GameSrvInfo                                                             // 服务器状态信息                             
{
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szGameSrvIP;                                        /// 游戏服IP
    public int          nProt;                                              /// 游戏服端口
    public int          nGameServerLineID;                                  /// 游戏服分线ID
    public int          nPosition;                                          /// 占用位
    public int          nState;                                             /// 游戏服状态
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szGameSrvName;                                      /// 游戏服名称


}

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           ACC_LOGIN_GS                                                            // 帐号登录游戏服                             
{
    public Head Head;
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 64)]
    public string       szAccount;                                          /// 游戏帐号
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szCheckCode;                                        /// 校验码
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           ACC_LOGIN_GS_CENSOR                                                     // 帐号登录游戏服_版署审核版本                 
{
    public Head         Head;
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szAccount;                                          /// 游戏帐号
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szCheckCode;                                        /// 校验码
}

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct                  REQ_LOGIN_LogIn_LS                                                      // 登录服登录请求             
{
    public Head         Head;

    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 64)]
    public string       sAccount;                                           /// 请求登录的帐号名称
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 256)]
    public string       sPassword;                                          /// 请求登录的帐号密码
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       sMac;                                               /// 请求登录的机器Mac地址
    public int          nGameVersion;                                       /// 请求登录的客户端携带的游戏版本号
    public int          nCentralServerID;                                   /// 请求登入的中心服ID
    public int          nGameServerID;                                      /// 请求登录的游戏服分线ID
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 64)]
    public string       sYiJieChannelID;                                    /// 当前使用的渠道ID(易接使用)
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct REQ_LOGIN_Censor                                                                         // 版署,本地测试用的登录结构体                 
{
    public Head Head;
    public int          nGameVersion;                                       /// 请求登录的客户端携带的游戏版本号
    public int          nCentralServerID;                                   /// 请求登入的中心服ID
    public int          nGameServerID;                                      /// 请求登录的游戏服分线ID
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 64)]
    public string       szAccount;                                          /// 请求登录的帐号名称
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 256)]
    public string       szPassword;                                         /// 请求登录的帐号密码
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szMac;                                              /// 请求登录的机器Mac地址

}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           REQ_LOGIN_SRV_List                                                      // 请求服务器列表                             
{
    public Head         Head;
    public int          Random;
}

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_LOGIN_LogIn_LS                                                      // 登录服登录请求返回                               
{
    public Head         Head;
    public int          nErrorID;                                           /// 错误代码 0 成功 1.....相应错误代码
    public int          nport;                                              /// 端口号
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szIP;                                               /// 校验码
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 25)]
    public string       szCheckCode;                                        /// IP地址

}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_LOGIN_SRV_List                                                      // 登录服返回的服务器列表                      
{
    public Head         Head;
    public int          nCentralServerID;                                   /// 中心服务器ID
    public int          nTotal;                                             /// 中心服挂接的游服数量
    public int          nloop;                                              /// 当前中心服分包数量 从1开始
    public int          nNum;                                               /// 该数据包挂接的游戏服数量
    [MarshalAs          (UnmanagedType.ByValArray, SizeConst = 10)]
    public GameSrvInfo[] gameSrvInfoList;                                   /// 中心服挂接的游戏服列表信息
}


[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct                  SYNC_LOGIN_LS_GS_Time                                                   // 中心服向游戏服同步时间                      
{
    public Head         Head;
    public long         timeZone;                                           /// 与UTC的时差
    public long         curentTime;                                         /// 当前时间
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           SYNC_CLOCK                                                              // 服务器同步特殊时间点,时间戳                 
{
    public Head         Head;
    public int          time;                                               /// 特殊时间同步,时间戳
}


//-----------------------<< 返回玩家(战队)信息 >>-----------------------------------------------------------------------
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           LOGIN_PlayerInfo                                                        // 玩家(角色)信息
{
    public int          nPlayerLv;                                          /// 玩家等级
    public int          nPLayerIcon;                                        /// 玩家头像
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szPlayerName;                                       /// 玩家名称
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           SYNC_LOGIN_AllPlayerInfo                                                // 该账号下所有玩家(角色)信息(合服后会有多个角色)
{
    public Head         Head;
    public int          nNum;                                               /// 玩家帐号数量(合服后有多个)
    [MarshalAs          (UnmanagedType.ByValArray, SizeConst = 23)]
    public LOGIN_PlayerInfo[] PlayerData;                                   /// 各服玩家信息
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]

//-----------------------<< 根据返回的玩家信息,创建角色或者登录角色 >>----------------------------------------------------
public struct           REQ_CREATEROLE                                                          // 角色创建请求                                
{
    public Head         Head;
    public int          nRoleHeroID;                                        /// 玩家主角英雄 
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szPlayerName;                                       /// 玩家名称
}
[StructLayout           (LayoutKind.Sequential,Pack = 1,CharSet = CharSet.Ansi)]
public struct           RET_CREATEROLE                                                          // 返回角色创建请求                             
{
    public Head         Head;
    public int          nErrorID;                                           /// 错误码:0:创建成功 1:预检验成功  ....错误编号
}
[StructLayout           (LayoutKind.Sequential,Pack = 1,CharSet = CharSet.Ansi)]
public struct           REQ_ENTER_GAME                                                          // 进入游戏请求                                 
{
    public Head         Head;
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szPlayerName;                                       /// 玩家名称
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]    
    public string       szCheckOutText;                                     /// 校验码
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_ENTER_GAME                                                          // 进入游戏请求回调                             
{
    public Head         Head;
    public int          nErrorID;
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           CLGC_CLIENT_READY                                                       // 客户端准备完毕,请求主界面信息                 
{
    public Head         Head;
    public int          Extra;                                              /// 错误码:0:成功 1....错误编号
}

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]                         
public struct           DataForMainUI                                                           // 主界面所需要的信息                           
{
    public Head         Head;
    public int          nplayerID;                                          /// 玩家ID
    public int          nPlayerIcon;                                        /// 玩家头像图标
    public int          nPlayerLv;                                          /// 玩家等级
    public int          nPlayerExp;                                         /// 玩家经验
    public int          nStamina;                                           /// 玩家体力

    public int          nSkillPoint;                                        /// 玩家技能点
    public int          nVIPLv;                                             /// VIP等级
    public int          nRegTime;                                           /// 注册时间戳
    public int          NewGuideIndex;                                      /// 新手引导标记(步数)
    public int          BuyedCoinsTimes;                                    /// 已购买金币次数

    public int          BuyedStaminaTimes;                                  /// 已购买体力次数
    public int          BuyedSkillTimes;                                    /// 已购买技能点次数
    public int          FriendStaminaCount;                                 /// 已领取好友体力数
    public int          nLogInGameDays;                                     /// 登录游戏天数
    public int          nLvRewardGot;                                       /// 已领取等级奖励

    public int          TalentLv;                                           /// 当前天赋等级
    public int          TalentPoints;                                       /// 当前天赋点数
    public int          TalentAllGotPoints;                                 /// 累积获得的天赋点数
    public int          WingRate;                                           /// 翅膀祝福率
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szPlayerName;                                       /// 玩家名称
}

[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           REQ_PLAYER_BUY_SOMETHING                                                // 购买请求                                    
{
    public Head         Head;
    public int          nID;                                                /// 占位
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_PLAYER_BUY_SOMETHING                                                // 返回购买请求                                
{
    public Head         Head;
    public int          nErrorID;                                           /// 错误码
    public int          nType;                                              /// 类型(区别回调哪个请求)
    public long         lCoins;                                             /// 金币
}


[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)] 
public struct           REQ_PLAYER_RENAME                                                       // 更改玩家名称请求                            
{
    public Head         Head;
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 32)]
    public string       szNewName;                                          /// 新玩家名称
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_PLAYER_RENAME                                                       // 返回更改玩家名称请求                        
{
    public Head         Head;
    public int          nErrorID;
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           REQ_PLAYER_UpLvAward                                                    // 领取升级礼包请求                            
{
    public Head         Head;
    public int          nLv;                                                /// 玩家等级
}
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           RET_PLAYER_UpLvAward                                                    // 返回领取升级礼包请求                         
{
    public Head         Head;
    public int          nErrorID;                                           /// 错误码 0:成功 1 ......
}


#region****************************************************//   存疑代码   //***************************************************************
[StructLayout           (LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct           REQ_LOGIN_RETEAM                                                        // *** 更改阵容???? ***
{
    public Head         Head;
    [MarshalAs          (UnmanagedType.ByValTStr, SizeConst = 256)]
    public string       team;                                                                   // 玩家名称
}

#endregion
public enum eBuyType                                                                            // 购买类型
{ 
    BuyStamina  = 5,                                    // 购买体力 
    BuyCoins    = 6,                                    // 购买金币
    BuySkill    = 7                                     // 购买技能点
}