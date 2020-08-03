using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary> 关卡确认面板视图 </summary>

public class CheckPointConfirmPanelView : EventView
{
    public string               LineupClick_Event               = "LineupClick_Event";          // 阵容选择 点击事件
    public string               ChallengeClick_Event            = "ChallengeClick_Event";       // 挑战关卡 点击事件
    public string               ExitClick_Event                 = "ExitClick_Event";            // 退出面板 点击事件

    public int                  CP_ID                           = 0;
    public UISprite             LineupBtn, ChallengeBtn,ExitBtn;
    public GameObject           Top;
    public GameObject           NpcIconList, AwardIconList;                                     // Npc图标列表,掉落奖励图标列表

    public void ViewInit()
    {
        UIEventListener.Get(LineupBtn.gameObject).onClick                  = LineupClick;                  // 阵容选择 点击监听
        UIEventListener.Get(ChallengeBtn.gameObject).onClick               = ChallengeClick;               // 挑战关卡 点击监听
        UIEventListener.Get(ExitBtn.gameObject).onClick                    = ExitClick;                    // 退出面板 点击监听
    }
    private void                LineupClick         (GameObject obj)                            // 阵容选择 点击  
    {
        dispatcher.Dispatch(LineupClick_Event);
    }
    private void                ChallengeClick      (GameObject obj)                            // 挑战关卡 点击  
    {
        dispatcher.Dispatch(ChallengeClick_Event);
    }
    private void                ExitClick           (GameObject obj)                            // 退出面板 点击  
    {
        dispatcher.Dispatch(ExitClick_Event);
    }
}
