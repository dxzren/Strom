using UnityEngine;
using System;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

public class DialogPanelViewMediator : EventMediator
{
    [Inject]
    public DialogPanelView view { set; get; }
    [Inject]
    public IGameData game { set; get; }

    public override void OnRegister()
    {
        view.Init();
        view.GetComponent<UIPanel>().startingRenderQueue = (5000);

        view.dispatcher.AddListener(view.Event_OnOKClick, OkClick);
        view.dispatcher.AddListener(view.Event_OnCancelClick, CancelClick);
        UIAnimation.Instance().TransAlpha(this.gameObject, true);
        UIAnimation.Instance().TransAlpha(this.gameObject, true);
    }

    public override void OnRemove()
    {
        view.dispatcher.RemoveListener(view.Event_OnOKClick, OkClick);
        view.dispatcher.RemoveListener(view.Event_OnCancelClick, CancelClick);

        if(game.IsExitPanel)                                                                        // 如果是退出面板执行对应逻辑                
        {
            game.IsExitPanel = false;
        }
    }

    public void OkClick()
    {
        CancelClick();
    }

    public void CancelClick()
    {
        PanelManager.sInstance.CloseDialogPanel(view.gameObject);
    }
}

