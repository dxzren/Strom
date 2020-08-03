using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using System.Runtime.InteropServices;
/// <summary> 关卡信息请求_Main </summary>

public class REQ_CheckPointInfo_Command : EventCommand
{
    [Inject]
    public ISocket              InSocket                    { set; get; }
    [Inject]
    public ICheckPointSys       InCheckP_Sys                { set; get; }
    [Inject]
    public IPlayer              InPlayer                    { set; get; }

    public override void        Execute()
    {
        InCheckP_Sys.checkPointPanelName                    = (string)evt.data;                 /// 事件数据
        CheckChapterGoToID();                                                                   /// 验证 目标章节ID
        PanelManager.sInstance.ShowLoadingPanel();                                              /// 加载界面
        Send_REQ_CHECKPOINT_Main_MSG();                                                         /// 发送 关卡信息请求

        Debuger.Log             ("获得关卡信息 " + InCheckP_Sys.checkPointPanelName);

    }
    private void                CheckChapterGoToID()                                            // 验证 目标章节ID     
    {
        if (InCheckP_Sys.chapterGoToID != 0)
        {
            if (Configs_Chapter.sInstance.GetChapterDataByChapterID(
                                InCheckP_Sys.chapterGoToID).UnlockLevel[0] > InPlayer.PlayerLevel)
            {
                PanelManager.sInstance.ShowNoticePanel("主角等级" + Configs_Chapter.sInstance.
                                GetChapterDataByChapterID(InCheckP_Sys.chapterGoToID).UnlockLevel[0] + "级解锁");
                InCheckP_Sys.chapterGoToID = 0;
                return;
            }
        }
    }
    private void                Send_REQ_CHECKPOINT_Main_MSG()                                  // 发送 关卡信息请求    
    {
        REQ_CHECKPOINT_Main     Msg                         = new REQ_CHECKPOINT_Main();
        Msg.Head.size                                       = (short)Marshal.SizeOf(Msg);
        Msg.Head.type1                                      = (short)eMsgType._MSG_CHECKPOINT_MODULE;
        Msg.Head.type2                                      = (short)CheckPointMsgType.CHKP_REQ_Main;
        Msg.temp                                            = 'a';
        byte[]                  SendMsg                     = Util.StructToBytes(Msg);
        InSocket.SendRequest    (SendMsg);
        Debuger.LogWarning("||发送服务器请求|| 关卡信息请求--: < 42 / 1>");
    }
}
