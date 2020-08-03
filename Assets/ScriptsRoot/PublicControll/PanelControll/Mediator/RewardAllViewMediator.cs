using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RewardAllViewMediator : EventMediator
{
    [Inject]
    public RewardAllView RAView { set; get; }
    [Inject]
    public IPlayer playerData { set; get; }

    public override void OnRegister()
    {
        RAView.onInit();
        RAView.dispatcher.AddListener(RAView.sureButton_Event, SureButtonHandler);
        RAView.dispatcher.AddListener(RAView.closeButton_Event, CloseButtonHandler);
        UIAnimation.Instance().TransAlpha(this.gameObject, true);
        UIAnimation.Instance().TransScale(this.gameObject, true);
    }

    public override void OnRemove()
    {
        RAView.dispatcher.RemoveListener(RAView.sureButton_Event, SureButtonHandler);
    }

    public void SureButtonHandler()
    {
        dispatcher.Dispatch(EventSignal.Reward_sure_Event);
        CloseButtonHandler();
    }

    public void CloseButtonHandler()
    {
        UIAnimation.Instance().TransAlpha(this.gameObject, false);
        UIAnimation.Instance().TransScale(this.gameObject, false);

        //if(GameObject.Find (UIPanelConfig.TaskPanel) != null)     // 任务系统
        //{
        //    try
        //    {
        //        int formolLv = GameObject.Find(UIPanelConfig.TaskPanel).GetComponent<TaskViewMediator>().FormolLv;
        //        if(playerData.PlayerLevel != formolLv)
        //        {
        //            LeaderLevelUp LLvUp = PanelManager.sInstance.ShowPanel(SceneType.Main, SystemConfig.PanelName_UpLv).GetComponent<LeaderLevelUp>();
        //            LLvUp.SetInfor(playerData, playerData.PlayerLevel - formolLv);
        //            GameObject.Find(UIPanelConfig.TaskPanel).GetComponent<TaskViewMediator>().FormolLv = playerData.PlayerLevel;
        //        }
        //    }
        //    catch(System.Exception)
        //    {
        //    }
        //}
    }



}
