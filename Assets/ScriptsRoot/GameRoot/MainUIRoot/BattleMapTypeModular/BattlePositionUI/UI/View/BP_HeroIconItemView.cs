using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary> 英雄预置阵容 英雄图标Item </summary>

public class BP_HeroIconItemView : EventView
{
    public int                  TheHeroID                       = 0;                                /// 英雄ID
    public string               HeroIconClick_Event             = "HeroIconClick_Event";            /// 英雄点击事件

    public UISprite             HeroIconFrame, HeroIcon, IconMask, SelectedLabel, ElementType;      /// 英雄头像,图标,遮罩,选中标签,元素类型
    public UIGrid               StarNumGrid;                                                        /// 英雄星级

    public void                 ViewInit()                                                          // 
    {   UIEventListener.Get     (this.transform.gameObject).onClick  = HeroIconClick;            }

    private void                HeroIconClick(GameObject obj)                                       // 英雄图标 点击  
    {
        dispatcher.Dispatch     (HeroIconClick_Event);
    }
}