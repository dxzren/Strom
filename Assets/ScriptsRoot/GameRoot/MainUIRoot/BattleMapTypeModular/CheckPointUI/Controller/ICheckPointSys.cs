using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> 关卡系统数据接口 </summary>
public interface ICheckPointSys
{
    int                         chapterTotal { set; get; }                                          // 章节总数
    int                         chapterGoToID { set; get; }                                         // 通往章节关卡ID 
    int                         chapterID { set; get; }                                             // 章节ID
    int                         currentCheckPointID { set; get; }                                   // 当前关卡ID 
    int                         gotNormalStarBoxAward { set; get; }                                 // 已经领取普通星级宝箱奖励最大等级
    int                         gotEliteStarBoxAward { set; get; }                                  // 已经领取精英星级宝箱奖励最大等级
    int                         sweepTimes { set; get; }                                            // 扫荡次数
    int                         sweepLimit { set; get; }                                            // 扫荡次数上限

    string                      checkPointPanelName { set; get; }                                   // 关卡面板名称

    bool                        stopGuide { set; get; }                                            // 是否停止引导
    bool                        isEliteGuide { set; get; }                                         // 是否精英关卡引导
    bool                        isSweeping { set; get; }                                           // 避免重复扫荡

    ChapterType                 CurrentCP_Type { set; get; }                                        // 当前关卡类型

    List <int>                  GotEliteStartAwardList();                                           // 精英章节已领取奖励列表
    List<ItemData[]>            SweepResult { set; get; }                                           // 扫荡结果(物品列表)
    List<CHECKPOINT_DATA_Elite> EliteCheckPointList { set; get; }                                   // 精英关卡数据列表
    Dictionary<int,int>         EliteChallangeTimesDic { set; get; }                                // 精英关卡挑战次数字典

}