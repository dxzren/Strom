using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   点击面板外围,此面板自动隐藏面板 <Lv_30>   </summary>
public class HidePanel30FloorViewMediator : EventMediator
{
    [Inject]
    public HidePanel30FloorView             view { set; get; }

    public override void                    OnRegister()                                
    {
        view.OnInit();
        view.dispatcher.AddListener         ( view.self_click, SelfClick);              /// 注册UI监听
    }
    public override void                    OnRemove()
    {
        view.dispatcher.RemoveListener      ( view.self_click, SelfClick);
    }
    private void                            SelfClick()                                 /// UI点击处理
    {
        if(view.target.CompareTo("")!= 0)
        {
            PanelManager.sInstance.HidePanel(view.panelShowScene, view.target);         /// 隐藏目标面板
        }
    }
}