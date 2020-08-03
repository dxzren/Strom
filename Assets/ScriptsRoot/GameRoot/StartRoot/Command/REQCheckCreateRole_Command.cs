using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;

/// <summary>  校验创建角色请求  </summary>
public class REQCheckCreateRole_Command : EventCommand
{
    [Inject]
    public IPlayer              player                  { set; get; }
    [Inject]
    public IStartData           startData               { set; get; } 
    [Inject]
    public ISocket              socket                  { set; get; }

    public override void Execute()
    {
        PanelManager.sInstance.ShowLoadingPanel();                                              /// 加载界面
        Send_REQ_CREATEROLE_MSG();                                                              /// 发送角色创建验证请求 消息
        Debug.Log                   ("创建角色预检验请求!");
    }
    private void Send_REQ_CREATEROLE_MSG()                                                      // 发送角色创建验证请求 消息   
    {
        REQ_CREATEROLE              CreateRoleMsg           = new REQ_CREATEROLE();             /// 角色创建 消息数据类型
        CreateRoleMsg.Head.size                             = (short)Marshal.SizeOf(CreateRoleMsg);
        CreateRoleMsg.Head.type1                            = (short)eMsgType._MSG_PLAYER_MODULE;
        CreateRoleMsg.Head.type2                            = (short)PLAYER_CMD.PLAYER_REQ_CHECK_CREATE_ROLE;
        CreateRoleMsg.nRoleHeroID                           = startData.tempHeroID;
        CreateRoleMsg.szPlayerName                          = player.PlayerName;
        byte[]                      sendCreateRole          = Util.StructToBytes(CreateRoleMsg);
        socket.SendRequest          ( sendCreateRole );
    }
}
