using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class CheckPointItemMediator : EventMediator
{
    [Inject]
    public CheckPointItemView       View                        { set; get; }
    [Inject]
    public ICheckPointSys           InCheckP_Sys                { set; get; }

    public override void            OnRegister()
    {
        View.ViewInit();

        View.dispatcher.AddListener(View.CP_BtnClick_Event,     CP_BtnClickHandler);    // 关卡点击
    }
    public override void            OnRemove()
    {
        View.dispatcher.RemoveListener(View.CP_BtnClick_Event,  CP_BtnClickHandler);    // 关卡点击
    }
    private void                    CP_BtnClickHandler()
    {
        Debug.Log("CP_BtnClickHandler");
        InCheckP_Sys.currentCheckPointID                        = View.CP_ID;                       /// 当前关卡ID设置
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.CheckPointConfirmPanel);     /// 打开关卡确认面板
        PanelManager.sInstance.HidePanel(SceneType.Main, UIPanelConfig.CheckPointSelectPanel);      /// 关闭关卡选择面板
    }
}