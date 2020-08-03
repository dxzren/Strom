using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class StaminaTipsPanelMediator : EventMediator
{
    [Inject]
    public StaminaTipsPanelView View                    { set; get; }
    public override void OnRegister()
    {

    }
    public override void OnRemove()
    {

    }
}