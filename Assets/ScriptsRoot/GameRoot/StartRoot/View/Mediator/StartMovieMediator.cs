using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   开始动画    </summary>

public class StartMovieMediator : EventMediator
{
    [Inject]
    public StartMovie View { set; get; }

    public override void OnRegister()
    {
        PanelManager.sInstance.LoadOverHandler_10Planel(this.gameObject.name);
        View.Init();
    }
    public override void OnRemove()
    {
    }
}


