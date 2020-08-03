using UnityEngine;
using System.Collections;

public class CheckPointResponseHandler : IResponseHandler
{
    private static CheckPointResponseHandler    _Handler    = null;
    public static CheckPointResponseHandler     Instance()
    {
        if ( _Handler == null )
        {   _Handler = new CheckPointResponseHandler(); }
        return _Handler;
    }
    public bool ProtocolHandler ( byte[] msg, short type1, short type2 )
    {
        if ((eMsgType)type1 == eMsgType._MSG_CHECKPOINT_MODULE)                                 /// (42) CheckPoint-- 关卡
        {
            switch((CheckPointMsgType)type2)
            {
                case CheckPointMsgType.CHKP_RET_PassedMaxNormalID:                              /// (51) NorPassMaxCP-- 通过的普通最大关卡
                    {
                        RET_CHECKPOINT_PassedNorMaxCP_ID CurrentMaxNorID        = new RET_CHECKPOINT_PassedNorMaxCP_ID();
                        CurrentMaxNorID = (RET_CHECKPOINT_PassedNorMaxCP_ID)    Util.BytesToStruct( msg, msg.Length, CurrentMaxNorID.GetType());
                        NetEventDispatcher.Instance().DispathcEvent             ((int)eMsgType._MSG_CHECKPOINT_MODULE,
                                                                                 (int)CheckPointMsgType.CHKP_RET_PassedMaxNormalID, CurrentMaxNorID) ;
                        return true;
                    }
                case CheckPointMsgType.CHKP_RET_NormalInof:                                     /// (52) NorCP_Info--    普通关卡数据
                    {
                        RET_CHECKPOINT_Normal_Info NorCP_Data                   = new RET_CHECKPOINT_Normal_Info();
                        NorCP_Data = (RET_CHECKPOINT_Normal_Info)               Util.BytesToStruct ( msg, msg.Length, NorCP_Data.GetType());
                        NetEventDispatcher.Instance().DispathcEvent             ((int)eMsgType._MSG_CHECKPOINT_MODULE,
                                                                                 (int)CheckPointMsgType.CHKP_RET_NormalInof, NorCP_Data     );
                        return true;
                    }
                case CheckPointMsgType.CHKP_RET_EliteInfo:                                      /// (53) Elite_Info--    精英关卡数据
                    {
                        RET_CHECKPOINT_Elite_Info EliteCP_Data                  = new RET_CHECKPOINT_Elite_Info();
                        EliteCP_Data = (RET_CHECKPOINT_Elite_Info)              Util.BytesToStruct ( msg, msg.Length, EliteCP_Data.GetType());
                        NetEventDispatcher.Instance().DispathcEvent             ((int)eMsgType._MSG_CHECKPOINT_MODULE,
                                                                                 (int)CheckPointMsgType.CHKP_RET_EliteInfo, EliteCP_Data    );
                        return true;
                    }
                case CheckPointMsgType.CHKP_RET_NormalStarBox:                                  /// (54) NorChatperData-- 普通章节数据 (星级宝箱)
                    {
                        RET_CHECKPOINT_Nor_Chaper NorChatperData                = new RET_CHECKPOINT_Nor_Chaper();
                        NorChatperData = (RET_CHECKPOINT_Nor_Chaper)            Util.BytesToStruct ( msg, msg.Length, NorChatperData.GetType());
                        NetEventDispatcher.Instance().DispathcEvent             ((int)eMsgType._MSG_CHECKPOINT_MODULE,
                                                                                 (int)CheckPointMsgType.CHKP_RET_NormalStarBox, NorChatperData);
                        return true;
                    }
                case CheckPointMsgType.CHKP_RET_EliteStarBox:                                   /// (55) EliteChatperData-- 精英章节数据
                    {
                        RET_CHECKPOINT_Elite_Chaper EliteChatperData            = new RET_CHECKPOINT_Elite_Chaper();
                        EliteChatperData = (RET_CHECKPOINT_Elite_Chaper)        Util.BytesToStruct (msg, msg.Length, EliteChatperData.GetType());
                        NetEventDispatcher.Instance().DispathcEvent             ((int)eMsgType._MSG_CHECKPOINT_MODULE,
                                                                                 (int)CheckPointMsgType.CHKP_RET_EliteStarBox, EliteChatperData);
                        return true;
                    }
                case CheckPointMsgType.CHKP_RET_Challenge:                                      /// (56) ChallengeCP--      挑战关卡
                    {
                        RET_CHECKPOINT_Challenge ChallengeCP                    = new RET_CHECKPOINT_Challenge();
                        ChallengeCP = (RET_CHECKPOINT_Challenge)                Util.BytesToStruct (msg, msg.Length, ChallengeCP.GetType());
                        NetEventDispatcher.Instance().DispathcEvent             ((int)eMsgType._MSG_CHECKPOINT_MODULE,
                                                                                 (int)CheckPointMsgType.CHKP_REQ_Challenge, ChallengeCP);
                        return true;
                    }
                default:
                    return false;
            }
        }
        return false;
    }
}