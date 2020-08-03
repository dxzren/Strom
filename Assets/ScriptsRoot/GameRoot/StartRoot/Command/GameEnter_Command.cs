using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using System.Runtime.InteropServices;
/// <summary>  进入游戏服务器,游戏角色列表请求 </summary>
public class GameEnter_Command : EventCommand
{
    [Inject]
    public IRequest             netRequest              { set; get; }
    [Inject]
    public IStartData           startData               { set; get; }
    [Inject]
    public ISocket              socket                  { set; get; }

    public override void        Execute()
    {
        Debug.Log               ( startData.tempIP + "///////////" + startData.tempPort );

        socket.SocketThreadQuit ();                                                             /// 关闭端口线程
        socket.SocketConnection ( startData.tempIP, startData.tempPort, true );                 /// 连接游戏服务器

        Debuger.Log             ("进入游戏服务器,游戏角色列表请求: " + startData.tempIP + "/" + startData.tempPort);
        PanelManager.sInstance. ShowLoadingPanel();
        ACC_LOGIN_GS            AccLogInGsMsg           = new ACC_LOGIN_GS();
        AccLogInGsMsg.Head.     size                    = (short)Marshal.SizeOf(AccLogInGsMsg);
        AccLogInGsMsg.Head.     type1                   = (short)eMsgType._MSG_LOGIN_CLIENT_GS;
        AccLogInGsMsg.Head.     type2                   = (short)LOGIN_CLIENT_GS_CMD.CLGS_LOGIN;
        AccLogInGsMsg.          szAccount               = startData.account;
        AccLogInGsMsg.          szCheckCode             = startData.checkCode;

        byte[]                  sendAccLogInGsMsg       = Util.StructToBytes(AccLogInGsMsg);
        socket.SendRequest      ( sendAccLogInGsMsg );                                          /// 发送进入游戏服请求消息
        Debuger.Log             ("进入游戏服务器,游戏角色列表请求");
    }
    
}