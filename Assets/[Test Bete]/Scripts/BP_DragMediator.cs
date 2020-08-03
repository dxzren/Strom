using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class BP_DragMediator : EventMediator
{
    [Inject]
    public BP_DragView View { set; get; }


    public override void OnRegister()
    {
        base.OnRegister();
    }

}