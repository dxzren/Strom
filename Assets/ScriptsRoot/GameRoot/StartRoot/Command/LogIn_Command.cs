using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;
/// <summary>   登录游戏    </summary>
public class LogIn_Command : EventCommand
{
    [Inject]
    public IPlayer                  player              { set; get; }
    [Inject]
    public IStartData               startData           { set; get; }
    [Inject]
    public ISocket                  socket              { set; get; }

    public static bool              checkVersion        = false;

    public override void            Execute()
    {
        SocketClient.isServerList   = false;                                                    /// 接收服务器列表

        socket.SocketThreadQuit();                                                              /// 关闭线程
        if(!string.IsNullOrEmpty    ( startData.testIP ))                                       /// 连接测试服        
        {
            socket.SocketConnection ( startData.testIP, startData.testPort, false);
            Debug.Log               ("IP = " + startData.testIP + "Prot = " + startData.testPort);
        }
        else                                                                                    /// 连接登录服        
        {
            socket.SocketConnection ( Define.LOGIN_IP, Define.LOGIN_PORT, false);               /// 连接登录服
            Debug.Log               ("IP = " + Define.LOGIN_IP + "Prot = " + Define.LOGIN_PORT);
        }

        PanelManager.sInstance.ShowLoadingPanel();                                              /// 展示加载面板
        REQ_LOGIN_LogIn_LS Msg      = new REQ_LOGIN_LogIn_LS();
        Msg.Head.size               = (short)Marshal.SizeOf(Msg);
        Msg.Head.type1              = (short)eMsgType._MSG_LOGIN_CLIENT_LS;
        Msg.Head.type2              = (short)LOGIN_CLIENT_LS_CMD.CLLS_REQ_LOGIN;

        Msg.nGameVersion            = GetVersion();                                             /// 获取版本信息
        Msg.nCentralServerID        = startData.centerServerID;                                 /// 登录服ID
        Msg.nGameServerID           = startData.gameServerID;                                   /// 游戏服ID

        Msg.sAccount                = Util.EncryptAccount(startData.account);                   /// 加密帐号
        Msg.sPassword               = startData.password;                                       /// 密码
        Msg.sYiJieChannelID         = startData.channelID;                                      /// 渠道ID
        Msg.sMac                    = Util.GetMacAddress();                                     /// 机器MAC地址

        player.ServerName           = startData.gameServerName;                                 /// 玩家服务器名称
        player.ServerCenterID       = startData.centerServerID;                                 /// 玩家中心服ID
        player.GameServerID         = startData.gameServerID;                                   /// 玩家游戏服ID

        Debuger.Log                 ("中心服ID   : " + startData.centerServerID + "分线ID: " + startData.gameServerID);
        Debug.Log                   ("AccountInfo: Account = " + startData.account + "; Password = " + startData.password);
        Debuger.Log                 ("GameVersion: " + Msg.nGameVersion);

        byte[]                      sendData = Util.StructToBytes(Msg);
        socket.SendRequest          (sendData);
        Debug.Log                   ("获取登录验证码请求");
    }

    public int                      GetVersion()                                                // 获取版本号                             
    {
        if(checkVersion)
        {
            return Util.ParseToInt  (Application.version.Replace(".", ""));   // 字符转为int
        }
        else
        {
            return 1;
        }
    }
}