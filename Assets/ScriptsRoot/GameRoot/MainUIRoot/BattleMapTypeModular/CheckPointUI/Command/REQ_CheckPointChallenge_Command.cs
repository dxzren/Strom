using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;
/// <summary> 关卡挑战请求 命令 (42/2)</summary>
public class REQ_CheckPointChallenge_Command : EventCommand
{
    [Inject]
    public ISocket                  InSocket                        { set; get; }
    [Inject]
    public IPlayer                  InPlayer                        { set; get; }
    [Inject]
    public ICheckPointSys           InCheckP_Sys                    { set; get; }
    public override void            Execute()
    {
        Debug.Log                   ("[ Command_REQ_CHECKP_Challenge ]: 关卡挑战请求 ( REQ_Send: 42/2 )");
        PanelManager.sInstance.ShowLoadingPanel();                                                                  /// Loading...面板
        InPlayer.SaveLineUpFile(InPlayer.BattleTypeToLineUp(BattleType.CheckPoint), BattleType.CheckPoint);         /// 保存阵容到本地文件
        Send_REQ_CHECKP_Challenge();                                                                                /// 发送关卡挑战请求 < 42/2>
    }
    private void Send_REQ_CHECKP_Challenge()                                                                        // 发送关卡挑战请求 < 42/2>
    {
        REQ_CHECKPOINT_Challenge    CHECKP_ChallengeMsg             = new REQ_CHECKPOINT_Challenge();
        CHECKP_ChallengeMsg.Head.size                               = (short)Marshal.SizeOf(CHECKP_ChallengeMsg);
        CHECKP_ChallengeMsg.Head.type1                              = (short)eMsgType._MSG_CHECKPOINT_MODULE;
        CHECKP_ChallengeMsg.Head.type2                              = (short)CheckPointMsgType.CHKP_REQ_Challenge;
        CHECKP_ChallengeMsg.nCheckPointID                           = InCheckP_Sys.currentCheckPointID;

        byte[]                  sentArray                           = Util.StructToBytes(CHECKP_ChallengeMsg);
        InSocket.SendRequest    (sentArray);
    }
}

