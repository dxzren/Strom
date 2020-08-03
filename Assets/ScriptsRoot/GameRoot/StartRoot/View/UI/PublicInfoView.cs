using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>   登录公告信息视图     </summary>

public class PublicInfoView : EventView
{
    public GameObject               CancelBtn, Cancel_2Btn;                                                         /// 取消按钮,取消按钮_2
    public UIScrollView             ScrollGrid;                                                                     /// 滚动格
    public string                   OnCancelClick_Event                     = "OnCancelClick_Event";                /// 取消点击 

    public void                     Init()
    {
        UIEventListener.            Get( CancelBtn ).onClick                = CancelonClick;                        /// 注册 取消按钮
        UIEventListener.            Get( Cancel_2Btn ).onClick              = CancelonClick;                        /// 注册 取消按钮_2
    }
    public void                     CancelonClick (GameObject obj)
    {
        UIAnimation.Instance().     TransAlpha( this.gameObject, false);                                            /// 创建动画：由有到无
        UIAnimation.Instance().     TransScale( this.gameObject, false);                                            /// 创建动画：由大变小
    }
}
