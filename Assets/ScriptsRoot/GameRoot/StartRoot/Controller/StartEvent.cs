using UnityEngine;
using System.Collections;

/// <summary> 登录开始事件 </summary>
public class StartEvent
{
    // 加载游戏信息
    public static string version_Event                  = "version_Event";                      // 版本
    public static string CheckRes_Event                 = "CheckRes_Event";                     // 校验资源
    public static string REQLoadConfig_Event            = "REQLoadConfig_Event";                // 加载配置文件请求
    public static string RETLoadConfig_Event            = "RETLoadConfig_Event";                // 加载配置文件回调
    public static string Process_Event                  = "Process_Event";                      // 加载进度
    public static string SetLoadTitle_Event             = "SetLoadTitle_Event";                 // 设置加载标题
    public static string Hide_Event                     = "Hide_Event";                         // 隐藏

    // 登录注册界面
    public static string RefreshAcct_Event              = "RefreshAcct_Event";                  // 刷新账号
    public static string Register_Event                 = "Register_Event";                     // 注册
    public static string FastRegister_Event             = "FastRegigter_Event";                 // 快速注册
    public static string AccBind_Event                  = "AccBind_Event";                      // 注册帐号绑定
    public static string LogIn_Event                    = "LogIn_Event";                        // 登录
    public static string ClientReady_Event              = "ClientReady_Event";                  // 客户端已经准备好接受数据的信号
    public static string GameEnter_Event                = "GameEnter_Event";                    // 进入游戏服务器

    // 进入游戏服务器获得角色信息或者创建角色
    public static string GetPublicInfo_Event            = "GetPublicInfo_Event";                // 获取公告信息
    public static string GetServerList_Event            = "GetServerList_Event";                // 获取服务器列表
    public static string GridCallViewClick_Event        = "GridCallViewClick_Event";            // 点击左侧服务器列表，向右侧发送信息
    public static string RefreshServerName_Event        = "RefreshServerName_Event";            // 刷新服务器名称
    public static string RefreshPublic_Event            = "RefreshPublic_Event";                // 更新公告
    public static string RoleEnter_Event                = "RoleEnter_Event";                    // 以选择的角色信息进入游戏

    public static string ProcesRoleData_Event           = "ProcesRoleData_Event";               // 创建角色数据过程
    public static string GetNickName_Event              = "GetNickName_Event";                  // 获取随机角色名
    public static string GetNickNameEventCallBack_Event = "GetNickNameEventCallBack_Event";     // 获取随机角色名回调
    public static string CreateRole_Event               = "CreateRole_Event";                   // 创建角色
    public static string REQCheckCreateRole_Event       = "REQCheckCreateRole_Event";           // 校验创建角色
    // 启动初始战斗
    public static string StartBattleBegin_Event         = "StartBattleBegin_Event";             // 启动战斗开始事件
}