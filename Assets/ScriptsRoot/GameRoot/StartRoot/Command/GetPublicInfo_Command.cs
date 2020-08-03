using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;
/// <summary>  游戏公告请求  </summary>

public class GetPublicInfo_Command : EventCommand
{
    [Inject]
    public IRequest             netRequest          { set; get; }
    [Inject]
    public IStartData           startData           { set; get; }
    [Inject]
    public ISocket              socket              { set; get; }

    public override void        Execute()
    {
        PanelManager.sInstance. ShowLoadingPanel();                                             // 与登录服通讯

        REQ_LOGIN_SRV_List      PublicInfoMsg       = new REQ_LOGIN_SRV_List();
        PublicInfoMsg.Head.size                     = (short)Marshal.SizeOf(PublicInfoMsg);
        PublicInfoMsg.Head.type1                    = (short)eMsgType._MSG_LOGIN_CLIENT_LS;
        PublicInfoMsg.Head.type2                    = (short)LOGIN_CLIENT_LS_CMD.CLLS_REQ_PUBLIC;
        PublicInfoMsg.Random                        = 1;

        byte[]                  sendPublicMsg       = Util.StructToBytes(PublicInfoMsg);
        socket.SendRequest      ( sendPublicMsg );                                              // 发送公告请求
    }

}