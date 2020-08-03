using UnityEngine;
using System.Collections;
/// <summary> 关卡系统消息回调接口 </summary>
public interface ICheckPointNetWorkCallback
{
    void OnGetStarBoxResponse(EventBase obj);                               // 领取星级宝箱回调
    void OnPassedMaxNorCHKP_IDResponse(EventBase obj);                      // 通过的最大普通关卡ID回调
    void OnNormalCheckPointInfoResponse(EventBase obj);                     // 普通大关卡信息回调
    void OnEliteCheckPointInfoResponse(EventBase obj);                      // 精英关卡信息回调
    void OnNorChapterResponse(EventBase obj);                               // 普通章节数据回调
    void OnEliteChatperResponse(EventBase obj);                             // 精英章节数据回调

    void OnChallengeCheckPointResponse(EventBase obj);                      // 挑战关卡回调
    void OnResetChallengeTimesResponse(EventBase obj);                      // 重置挑战次数回调
    void OnSweepTimesErrorResponse(EventBase obj);                          // 扫荡次数错误回调
    void OnSweepResponse(EventBase obj);                                    // 扫荡回调

}