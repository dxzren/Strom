using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

///-------------------------------------------------------------------------------------------------------------------------/// <summary> 关卡系统消息回调处理 </summary>
public class CheckPointNetWorkCallback : ICheckPointNetWorkCallback
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher         dispatcher                      { set; get; }
    [Inject]
    public IPlayer                  player                          { set; get; }
    [Inject]
    public ICheckPointSys           checkPointSys                   { set; get; }
    [Inject]
    public IPersetLineupData        InBattlePos                     { set;get; }


    public void                     OnGetStarBoxResponse            ( EventBase obj)                                        // 领取星级宝箱回调              
    {
        PanelManager.sInstance.HideLoadingPanel();

        RET_CHECKPOINT_StarBoxAward ReStarBox                       = (RET_CHECKPOINT_StarBoxAward)obj.eventValue;
        if(ReStarBox.StarBoxAwardLv > 0)
        {
            Debug.Log("领取星级宝箱成功:");
            if(checkPointSys.CurrentCP_Type == ChapterType.Normal)                                                          // 已领取普通星级宝箱等级    
            {
                checkPointSys.gotNormalStarBoxAward                 = ReStarBox.StarBoxAwardLv;
            }
            if(checkPointSys.CurrentCP_Type == ChapterType.Elite)                                                           // 已领取精英星级宝箱等级 
            {
                checkPointSys.gotEliteStarBoxAward                  = ReStarBox.StarBoxAwardLv;
            }
            dispatcher.Dispatch(CheckPointEvent.RET_GetStarBox_Event);                                                      // 领取星级宝箱回调事件
        }
        else
        {
            PanelManager.sInstance.ShowNoticePanel("领取星级宝箱失败!");
            Debug.Log               ("领取星级宝箱失败!");
        }

    }
    public void                     OnPassedMaxNorCHKP_IDResponse   ( EventBase obj)                                        // 通过的最大普通关卡ID回调       
    {
        RET_CHECKPOINT_PassedNorMaxCP_ID ReNorMaxCPID               = (RET_CHECKPOINT_PassedNorMaxCP_ID)obj.eventValue;
        Debug.Log                   ("通过的普通关卡最大关卡ID回调: Total:"+ CP_ResponseTotal);

        PanelManager.sInstance.HideLoadingPanel();                                                                          // 隐藏加载界面
        player.NormalMaxCheckPointHistory.ID                        = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(ReNorMaxCPID.nCheckPointID + 1)
                                                                    == null ? ReNorMaxCPID.nCheckPointID : ReNorMaxCPID.nCheckPointID + 1;
        CP_ResponseTotal++;
        IfCompeleted                (CheckPointMsgType.CHKP_RET_PassedMaxNormalID.ToString());
    }
    public void                     OnNormalCheckPointInfoResponse  ( EventBase obj)                                        // 普通大关卡信息回调            
    {

        Debug.Log                   ("普通大关卡信息回调! ");
        PanelManager.sInstance.HideLoadingPanel();
        RET_CHECKPOINT_Normal_Info  ReNormalInfo                    = (RET_CHECKPOINT_Normal_Info)obj.eventValue;

        OnNormalCounter                                             += ReNormalInfo.thisSize;
        for (int i = 0; i < ReNormalInfo.thisSize; i++)
        {
            if ( !player.GetNormalCheckPointStars.ContainsKey(ReNormalInfo.CheckPointNormalList[i].nCheckPointID) &&
                 Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(ReNormalInfo.CheckPointNormalList[i].nCheckPointID) != null)
            {
                player.GetNormalCheckPointStars.Add(ReNormalInfo.CheckPointNormalList[i].nCheckPointID, ReNormalInfo.CheckPointNormalList[i].starLv);
            }
        }
        if (OnNormalCounter == ReNormalInfo.total)
        {
            Debug.Log               ("普通大关卡信息回调! Total:" + CP_ResponseTotal);
            CP_ResponseTotal++;
            IfCompeleted            (CheckPointMsgType.CHKP_RET_NormalInof.ToString());
        }
    }
    public void                     OnEliteCheckPointInfoResponse   ( EventBase obj)                                        // 精英关卡信息回调              
    {
        Debug.Log                   ("精英关卡信息回调.");
        RET_CHECKPOINT_Elite_Info ReEliteInfo                   = (RET_CHECKPOINT_Elite_Info)obj.eventValue;
        bool                        isFind                      = false;

        OnEliteCounter              += ReEliteInfo.thisSize;
        for(int i = 0;i < ReEliteInfo.thisSize;i++)
        {
           for(int j = 0; j < checkPointSys.EliteCheckPointList.Count;j++)
            {
                if(ReEliteInfo.CheckPointEliteList[i].nCheckPointID == checkPointSys.EliteCheckPointList[j].nCheckPointID)
                {
                    checkPointSys.EliteCheckPointList.Remove(checkPointSys.EliteCheckPointList[j]);
                    checkPointSys.EliteCheckPointList.Add(ReEliteInfo.CheckPointEliteList[i]);
                    isFind                                      = true;
                }
            }
           if(!isFind)
            {
                checkPointSys.EliteCheckPointList.Add(ReEliteInfo.CheckPointEliteList[i]);
            }
        }
        if ( ReEliteInfo.total == OnEliteCounter )
        {
            List<CHECKPOINT_DATA_Elite> Sorted                  = new List<CHECKPOINT_DATA_Elite>();
            for ( int i = 0; i < checkPointSys.EliteCheckPointList.Count; i++)
            {
                if ( !checkPointSys.EliteChallangeTimesDic.ContainsKey(checkPointSys.EliteCheckPointList[i].nCheckPointID))
                {   checkPointSys.EliteChallangeTimesDic.Add(checkPointSys.EliteCheckPointList[i].
                    nCheckPointID, checkPointSys.EliteCheckPointList[i].challengeTimes);
                }
                for ( int j = 1001; j < 1251; j++ )
                {
                    if ( checkPointSys.EliteCheckPointList[i].nCheckPointID == j)
                    {
                        if ( !player.GetNormalCheckPointStars.ContainsKey(j))
                        {
                            player.GetEliteCheckPointStars.Add(j, checkPointSys.EliteCheckPointList[i].starLv);
                            Sorted.Add(checkPointSys.EliteCheckPointList[i]);
                            break;
                        }
                    }
                }
            }
            if (ReEliteInfo.total == 0)
            {   player.EliteMaxCheckPointHistory.ID = 1001; }
            else
            {
                if (player.EliteMaxCheckPointHistory.ID == 0)
                {   player.EliteMaxCheckPointHistory.ID = Sorted[Sorted.Count - 1].nCheckPointID + 1; }
            }
            CP_ResponseTotal++;
            IfCompeleted(CheckPointMsgType.CHKP_RET_EliteInfo.ToString());
        }

    }

    public void                     OnNorChapterResponse            ( EventBase obj )                                       // 普通章节数据回调              
    {
        Debug.Log               ("普通章节数据回调!");
        RET_CHECKPOINT_Nor_Chaper   TheNorChatper                   = (RET_CHECKPOINT_Nor_Chaper)obj.eventValue;

        checkPointSys.gotNormalStarBoxAward                         = TheNorChatper.gotNormalStarBoxLv;
        checkPointSys.gotEliteStarBoxAward                          = TheNorChatper.gotEliteStarBoxLv;
        CP_ResponseTotal++;
        Debug.Log               ("普通章节数据回调! CP_ResponseTotal:"+ CP_ResponseTotal);
        IfCompeleted                                                ( CheckPointMsgType.CHKP_RET_NormalStarBox.ToString());
    }
    public void                     OnEliteChatperResponse          ( EventBase obj )                                       // 精英章节回调                  
    {
        Debug.Log               ("精英章节数据回调");
        RET_CHECKPOINT_Elite_Chaper TheEliteCapter                  = (RET_CHECKPOINT_Elite_Chaper)obj.eventValue;

        for (int i = 0; i < 18; i++ )
        {   checkPointSys.GotEliteStartAwardList().Add(0);          }
        for (int i = 0; i < TheEliteCapter.thisSize;i++)
        {   checkPointSys.GotEliteStartAwardList()[i] = TheEliteCapter.Elite_GotStarBoxList[i]; }

        CP_ResponseTotal++;
        IfCompeleted            ( CheckPointMsgType.CHKP_RET_EliteStarBox.ToString() );                                    // 
    }
    public void                     OnChallengeCheckPointResponse   ( EventBase obj)                                        // 挑战关卡回调 (42/56)          
    {
        Debug.Log                   ("Response (42/56): OnChallengeCheckPoint -- 挑战关卡回调");
        RET_CHECKPOINT_Challenge    ReChallenge                     = (RET_CHECKPOINT_Challenge)obj.eventValue;             /// 回调数据类型

        PanelManager.sInstance.HideLoadingPanel();                                                                          /// 隐藏加载面板
        if (ReChallenge.isSuccess == 0)                                                                                     /// 返回<:0_可以挑战> 
        {
            dispatcher.Dispatch     (BattleEvent.LoadDataForCheckPoint_Event, checkPointSys.currentCheckPointID);           /// 加载关卡数据
//          player.SaveLineUpFile   (player.BattleTypeToLineUp(InBattlePos.BattleType), BattleType.CheckPoint);             /// 保存阵容到本地
//          player.BattleTypeToLineUp(InBattlePos.BattleType).Clear();                                                      /// 清理缓存战斗阵容
        }
        else                        Debug.Log("Fail! [ ResponseData: 1]");                                                  /// 挑战失败
    }
    public void                     OnResetChallengeTimesResponse   ( EventBase obj)                                        // 重置挑战次数回调              
    {
        RET_CHECKPOINT_ResetChallengeTimes ReChallengeTimes         = (RET_CHECKPOINT_ResetChallengeTimes)obj.eventValue;
        Debug.Log("重置精英关卡挑战次数回调:");

        PanelManager.sInstance.HideLoadingPanel();
        if(ReChallengeTimes.isSuccess == 0)
        {
            Debug.Log("重置精英关卡挑战次数 成功");
            for(int i = 0;i < checkPointSys.EliteCheckPointList.Count;i++)
            {
                if(checkPointSys.EliteCheckPointList[i].nCheckPointID == checkPointSys.currentCheckPointID)
                {
                    CHECKPOINT_DATA_Elite newCHKP_Data              = new CHECKPOINT_DATA_Elite();
                    newCHKP_Data.challengeTimes                     = 0;
                    newCHKP_Data.nCheckPointID                      = checkPointSys.EliteCheckPointList[i].nCheckPointID;
                    newCHKP_Data.resetTimes                         = checkPointSys.EliteCheckPointList[i].resetTimes;
                    newCHKP_Data.resetTimes++;
                    newCHKP_Data.starLv                             = checkPointSys.EliteCheckPointList[i].starLv;
                    checkPointSys.EliteCheckPointList[i]            = newCHKP_Data; 
                }

            }
            checkPointSys.EliteChallangeTimesDic[checkPointSys.currentCheckPointID]     = 0;
            dispatcher.Dispatch     (EventSignal.BattleFinished_Event);              
        }
        else
        {
            Debug.Log               ("重置挑战次数 失败!");
            PanelManager.sInstance.ShowNoticePanel("重置挑战次数失败!");
        }
    }

    public void                     OnSweepTimesErrorResponse       ( EventBase obj)                                        // 扫荡次数错误回调              
    {

    }
    public void                     OnSweepResponse                 ( EventBase obj)                                        // 扫荡回调                     
    {
        RET_CHECKPOINT_Sweep        ReSweep                         = (RET_CHECKPOINT_Sweep)obj.eventValue;
        Debug.Log("扫荡次数一致,返回扫荡结果.");

        PanelManager.sInstance.HideLoadingPanel();
        if (checkPointSys.sweepTimes == 1)                                                                                  // 一次扫荡:物品数据列表 -> 扫荡结果列表
        {
            ItemData[] TempItemDataList = new ItemData[ReSweep.thisSize];

            for (int i = 0; i < ReSweep.thisSize; i++)
            {
                TempItemDataList[0] = ReSweep.ItemDataList[i];
            }
            checkPointSys.SweepResult.Add(TempItemDataList);
            dispatcher.Dispatch(CheckPointEvent.RET_Sweep_Event);

            if (checkPointSys.CurrentCP_Type == ChapterType.Elite)
            {
                checkPointSys.EliteChallangeTimesDic[checkPointSys.currentCheckPointID]++;                                  // 精英关卡挑战次数增加
            }
            Debug.Log("扫荡一次完成.");
        }
        else if (checkPointSys.sweepTimes == 10)                                                                            // 十次扫荡
        {
            ItemData[] TempItemDataList = new ItemData[ReSweep.thisSize];
            for (int i = 0; i < ReSweep.thisSize; i++)
            {
                TempItemDataList[i] = ReSweep.ItemDataList[i];
            }
            checkPointSys.SweepResult.Add(TempItemDataList);
            sweepCounted++;
            if (sweepCounted == 10)
            {
                sweepCounted = 0;
                dispatcher.Dispatch(CheckPointEvent.RET_Sweep_Event);
                Debug.Log("扫荡十次完成.");
            }
        }
        else
        {
            ItemData[] TempItemDataList = new ItemData[ReSweep.thisSize];
            for (int i = 0; i < ReSweep.thisSize; i++)
            {
                TempItemDataList[0] = ReSweep.ItemDataList[i];
            }
            checkPointSys.SweepResult.Add(TempItemDataList);
            sweepCounted++;
            if(sweepCounted == checkPointSys.sweepTimes)
            {
                checkPointSys.EliteChallangeTimesDic[checkPointSys.currentCheckPointID] += checkPointSys.sweepTimes;
                sweepCounted = 0;
                dispatcher.Dispatch(CheckPointEvent.RET_Sweep_Event);
                Debug.Log("精英关卡扫荡完成.");
            }
        }
        dispatcher.Dispatch(EventSignal.BattleFinished_Event);                                  // 战斗结束
    }

    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
    private int                     CP_ResponseTotal                = 0;                                                    /// 关卡信息回调累计
    private int                     OnNormalCounter                 = 0;                                                    /// 普通关卡包 累计
    private int                     OnEliteCounter                  = 0;                                                    /// 精英关卡包 累计
    private int                     sweepCounted                    = 0;                                                    /// 已经扫荡次数
    private void                    IfCompeleted                    (string msg_type2_Str )                                 // 关卡所有信息数据_接收完成       
    {
        if ( CP_ResponseTotal == 4 )
        {
            PanelManager.sInstance.HideLoadingPanel();
            Debuger.Log("关卡信息全部接受完成!");
            if(checkPointSys.chapterGoToID != 0)
            {
                checkPointSys.chapterID                     = checkPointSys.chapterGoToID;
                checkPointSys.chapterGoToID                 = 0;
            }
            CP_ResponseTotal                                = 0;
            OnNormalCounter                                 = 0;
            OnEliteCounter                                  = 0;
            if (checkPointSys.checkPointPanelName != null)
                PanelManager.sInstance.ShowPanel(SceneType.Main, checkPointSys.checkPointPanelName);
        }
    }
    #endregion
}
