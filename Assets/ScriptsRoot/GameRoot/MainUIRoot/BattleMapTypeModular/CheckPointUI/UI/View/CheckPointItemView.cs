using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary> 关卡面板项设置 </summary>

public class CheckPointItemView : EventView
{
    public string               CP_BtnClick_Event           = "CP_BtnClick_Event";              // 关卡点击

    public int                  CP_ID                       = 0;
    public UISprite             CP_BG_Frame, CP_Icon, StarNum, Star_1, Star_2, Star_3;
    public GameObject           CP_Btn, Cp_FrameBtn;
    public void ViewInit()
    {
        UIEventListener.Get(CP_Btn).onClick                 = CP_BtnClick;                      // 关卡点击
        UIEventListener.Get(Cp_FrameBtn).onClick            = CP_BtnClick;                      // 关卡框点击
    }
    private void                CP_BtnClick ( GameObject obj)                                   // 关卡点击
    {    dispatcher.Dispatch    (CP_BtnClick_Event);    }

}
