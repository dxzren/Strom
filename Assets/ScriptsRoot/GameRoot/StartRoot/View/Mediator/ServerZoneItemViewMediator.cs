using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class ServerZoneItemViewMediator : EventMediator
{
    [Inject]
    public ServerZoneItemView   View        { set; get; }
    [Inject]
    public IStartData           startData   { set; get; }

    public override void OnRegister()
    {
        View.Init();
        View.dispatcher.AddListener     (View.SelectClick_Event, SelectClickHandler);
    }
    public override void OnRemove()
    {
        View.dispatcher.RemoveListener  (View.SelectClick_Event, SelectClickHandler);
    }

    public void SelectClickHandler()
    {
        startData.centerServerID = View.serverID;
        if (View.name.text == "推荐服务器")
        {
            dispatcher.Dispatch(StartEvent.GridCallViewClick_Event);
        }
        else
        {
            dispatcher.Dispatch(StartEvent.GridCallViewClick_Event, View.serverID);
        }
    }
}
