using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;

/// <summary>  创建角色请求  </summary>
public class CreateRole_Command : EventCommand
{
    [Inject]
    public IStartData           startData               { set; get; }
    [Inject]
    public IPlayer              player                  { set; get; }
    [Inject]
    public ISocket              socket                  { set; get; }

    public override void Execute()
    {
        PanelManager.sInstance.ShowLoadingPanel();
        Send_REQ_CREATEROLE_MSG();                                                              /// 发送角色创建请求 消息 
    }
    private void Send_REQ_CREATEROLE_MSG()                                                      // 发送角色创建请求 消息
    {
        Debug.Log("创建角色请求!--: < 31/1 >");
        REQ_CREATEROLE          CreateRoleMsg       = new REQ_CREATEROLE();
        CreateRoleMsg.Head.size                     = (short)Marshal.SizeOf(CreateRoleMsg);
        CreateRoleMsg.Head.type1                    = (short)eMsgType._MSG_PLAYER_MODULE;
        CreateRoleMsg.Head.type2                    = (short)PLAYER_CMD.PLAYER_REQ_CREATE_ROLE;
        CreateRoleMsg.nRoleHeroID                   = startData.tempHeroID;
        CreateRoleMsg.szPlayerName                  = player.PlayerName;

        byte[]                  sendCreateRoleMsg   = Util.StructToBytes(CreateRoleMsg);
        socket.SendRequest      (sendCreateRoleMsg);
    }

}
