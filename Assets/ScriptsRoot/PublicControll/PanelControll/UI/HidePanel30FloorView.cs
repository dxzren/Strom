using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   点击面板外围,此面板自动隐藏面板 <Lv_30>   </summary>
public class HidePanel30FloorView : EventView
{
    public string               target                      = "";                               /// 隐藏目标面板名称
    public string               self_click                  = "self_click";                     /// 点击

    public SceneType            panelShowScene;

    public void OnInit()
    {
        UIEventListener.Get     ( gameObject ).onClick      = SelfClick;                        /// 监听点击
    }
    void SelfClick              ( GameObject obj )
    {
        dispatcher.Dispatch     ( self_click );
    }

}