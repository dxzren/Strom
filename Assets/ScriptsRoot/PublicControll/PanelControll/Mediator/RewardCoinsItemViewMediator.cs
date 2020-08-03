using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.mediation.api;


public class RewardCoinsItemViewMediator : EventMediator
{
    [Inject]
    public RewardCoinsItemView RCItemView { set; get; }

    public override void OnRegister()
    {
        RCItemView.onInit();
    }
    public override void OnRemove()
    {

    }
}