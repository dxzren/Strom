using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class ServerZoneItemView : EventMediator
{
    public UILabel          name;                                           // 推荐服务器名称
    public int              serverID;                                       // 服务器ID
    public string           SelectClick_Event = "SelectClick_Event";        // 选择点击事件
    public void Init()
    {
        UIEventListener.Get(this.gameObject).onClick = ViewClick;
    }

    public void ViewClick(GameObject obj)                                   // 视图点击
    {
        dispatcher.Dispatch(SelectClick_Event);
    }

}