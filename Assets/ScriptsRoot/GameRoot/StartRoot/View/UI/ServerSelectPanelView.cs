using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   服务器选区面板     </summary>
public class ServerSelectPanelView : EventView
{
    public GameObject       CloseSpaceBtn, SelectBtn;
    public UILabel          LastServer;
    public UIGrid           ListerGrid, ItemGrid;
    public Transform        serverListFather;
    public string           SelectButton_Event              = "SelectButton_Event";
    public string           CloseSpace_Event                = "CloseSpace_Event";

    public void             Init()                              
    {
        UIEventListener.Get(SelectBtn).onClick              = SelectClick;
        UIEventListener.Get(CloseSpaceBtn).onClick          = CloseSpaceClick;
    }
    public void             SelectClick(GameObject obj)         
    {
        dispatcher.Dispatch(SelectButton_Event);
    }
    public void             CloseSpaceClick(GameObject obj)     
    {
        dispatcher.Dispatch(CloseSpace_Event);
    }
}
