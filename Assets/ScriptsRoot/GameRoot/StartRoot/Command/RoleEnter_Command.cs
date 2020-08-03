using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;
/// <summary>  角色进入  </summary>
public class RoleEnter_Command : EventCommand
{
    [Inject]
    public IPlayer              player                  { set; get; }
    [Inject]
    public IStartData           startData               { set; get; }
    [Inject]
    public ISocket              socket                  { set; get; }

    public override void Execute()
    {
        PanelManager.sInstance.ShowLoadingPanel();                                              /// 显示加载界面
        Send_REQ_ENTER_GAME_MSG();                                                              /// 发送角色进入消息    < 31/2 >
        Debug.LogWarning               ("---------------:客户端请求_< 31/2 >: RoleEnter_Command  (以角色身份登录游戏服务器)");
    }
    private void Send_REQ_ENTER_GAME_MSG()                                                      // 发送角色进入消息     < 31/2 >
    {
        REQ_ENTER_GAME          EnterGameMsg            = new REQ_ENTER_GAME();
        EnterGameMsg.Head.size                          = (short)Marshal.SizeOf(EnterGameMsg);
        EnterGameMsg.Head.type1                         = (short)eMsgType._MSG_PLAYER_MODULE;
        EnterGameMsg.Head.type2                         = (short)PLAYER_CMD.PLAYER_REQ_ENTER_GAME;
        EnterGameMsg.szPlayerName                       = player.PlayerName;
        EnterGameMsg.szCheckOutText                     = startData.checkCode;
        byte[]                  sendEnterGame           = Util.StructToBytes(EnterGameMsg);                                
        socket.SendRequest      (sendEnterGame);                                                 /// 发送角色进入消息
    }
}