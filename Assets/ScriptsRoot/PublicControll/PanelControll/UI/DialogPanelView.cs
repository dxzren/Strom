using UnityEngine;
using System.Collections;
using System;
using strange.extensions.mediation.impl;

public class DialogPanelView : EventView
{
    public string           Event_OnOKClick                 = "Event_OnOKClick";
    public string           Event_OnCancelClick             = "Event_OnCancelClick";

    public GameObject       ok, cancel, cancel2;
    public UILabel          shenming1, confirmText;

    [HideInInspector]
    public bool             removeClickBackGround           = false;  
    public Action           OKCallBack;
    public Action           CancelCallBack;

    private float           MovePanelValue                  = -4000f;                                   // 位移出界面值
    new void Awake()
    {
        transform.localPosition = new Vector3(  transform.position.x - MovePanelValue,                  // 位移出界面
                                                transform.position.y - MovePanelValue, 
                                                transform.position.z);
    }
    public void             Init()
    {
        UIEventListener.Get(ok).onClick                 = OnOKClick;
        UIEventListener.Get(cancel).onClick             = OnCancelClick;
        if(!removeClickBackGround)
        {
            UIEventListener.Get(cancel2).onClick        = OnCancelClick2;
        }
    }

    public void             OnOKClick( GameObject obj )
    {
        Debug.Log("ok");
        if(OKCallBack != null)
        {
            OKCallBack();
            dispatcher.Dispatch(Event_OnCancelClick);
            return;
        }
        dispatcher.Dispatch (Event_OnCancelClick);
    }
    public void             OnCancelClick( GameObject obj )
    {

        if(CancelCallBack != null)
        {
            CancelCallBack();
            dispatcher.Dispatch(Event_OnCancelClick);
            return;
        }
    }
    public void             OnCancelClick2( GameObject obj )
    {
        if(!removeClickBackGround)
        {
            OnCancelClick2(obj);
        }
    }
}
