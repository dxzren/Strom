using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;

/// <summary>  服务器列表请求  </summary>
public class GetServerList_Command : EventCommand
{
    [Inject]
    public IRequest             netRequeset         { set; get; }
    [Inject]
    public IStartData           startData           { set; get; }
    [Inject]
    public ISocket              socket              { set; get; }

    public override void        Execute()
    {
        Debug.Log               ("GetServerList_Command !");
        SocketClient.isServerList                   =   true;                                   /// 接收服务器列表
        if( Util.GetIPFile      ("hasSelected")     ==  null)                                   /// 获取服务器登录信息文件 
        {
            startData.isLocalAcc                    =   false;                                  /// 没有本地帐号信息
        }
        else                     
        {   startData.isLocalAcc                    =   true;   }

        startData.centerServerList.Clear();                                                     /// 清空服务器列表信息
        socket.SocketThreadQuit();                                                              /// 关闭端口线程

        if (!string.IsNullOrEmpty( startData.testIP ))                                          /// 测试服进行连接         
        {
            Debug.Log( "TestIP= " + startData.testIP + "TestPort= " + startData.testPort);
            socket.SocketConnection(startData.testIP, startData.testPort, false);
        }
        else                                                                                    /// 登录服进行连接         
        {
            Debug.Log("LOGIN_IP = " + Define.LOGIN_IP + "LOGIN_IP = " + Define.LOGIN_PORT );
            socket.SocketConnection( Define.LOGIN_IP, Define.LOGIN_PORT, false );
        }
                                                  
        PanelManager.sInstance.ShowLoadingPanel();                                              /// 登录服进行通信
        REQ_LOGIN_SRV_List      SrvListMsg          = new REQ_LOGIN_SRV_List();
        SrvListMsg.Head.size                        = (short)Marshal.SizeOf( SrvListMsg );
        SrvListMsg.Head.type1                       = (short)eMsgType._MSG_LOGIN_CLIENT_LS;
        SrvListMsg.Head.type2                       = (short)LOGIN_CLIENT_LS_CMD.CLLS_REQ_SRV_LIST;
        SrvListMsg.Random                           = 1;

        byte[]                  sendSrvListMsg      = Util.StructToBytes( SrvListMsg );
        socket.SendRequest      ( sendSrvListMsg );                                             /// 发送服务器列表请求消息
        Debug.Log               ("获取服务器列表请求 = " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }
}
