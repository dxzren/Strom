using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RewardPropItemViewMediator : EventMediator
{
    [Inject]
    public RewardPropItemView RPView { set; get; }

    public override void OnRegister()
    {
        RPView.onInit();
    }
    public override void OnRemove()
    {

    }
}

