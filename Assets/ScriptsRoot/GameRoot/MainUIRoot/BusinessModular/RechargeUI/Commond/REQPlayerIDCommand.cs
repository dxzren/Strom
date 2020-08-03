using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;
/// <summary> 首充信息请求--玩家ID请求 </summary>


public class REQPlayerIDCommand : EventCommand
{

    [Inject]
    public ISocket socket { set; get; }

    public override void Execute()
    {
        REQ_RECHARGE_PLAYERID TempPlayerID = new REQ_RECHARGE_PLAYERID();
        TempPlayerID.Head.size  = (short)Marshal.SizeOf(TempPlayerID);
        TempPlayerID.Head.type1 = (short)eMsgType._MSG_RECHARGE_MODULE;
        TempPlayerID.Head.type2 = (short)eReCharge_CMD.RECHARGE_REQ_PlayerID;
        TempPlayerID.a = '0';
        byte[] playerIDMsg = Util.StructToBytes(TempPlayerID);              // 将类型传化成字节数组
        socket.SendRequest(playerIDMsg);                                    // 客户端发送请求

        Debug.LogWarning ("|| 服务器发送 < 51/3 >|| -- 玩家帐号ID 请求发送--(充值记录)!");
    }
}