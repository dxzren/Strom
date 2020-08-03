using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

/// <summary> 关卡选择面板 </summary>
public class CheckPointSelectPanelView : EventView
{
    public int                  TheCheckPointID;                                                /// 关卡ID
    public int                  TheChapterID;                                                   /// 章节ID 

    public string               ExitClick_Event             = "ExitClick_Event";                /// 退出       点击
    public string               NormalClick_Event           = "NormalClick_Event";              /// 普通关卡    点击
    public string               EliteClick_Event            = "EliteClick_Event";               /// 精英关卡    点击

    public UITexture            ChapterMap;
    public GameObject           ExitBtn, NormalBtn, EliteBtn;
    public UILabel              ChapterName, ChapterNum;

    public void ViewInit()
    {
        UIEventListener.Get(ExitBtn).onClick                = ExitClick;                        /// 退出       点击
        UIEventListener.Get(NormalBtn).onClick              = NormalClick;                      /// 普通关卡    点击
        UIEventListener.Get(EliteBtn).onClick               = EliteClick;                       /// 精英关卡    点击

    }
    private void ExitClick ( GameObject obj)                                                    // 退出       点击
    {   dispatcher.Dispatch     (ExitClick_Event);       }   
    private void NormalClick( GameObject obj)                                                   // 普通关卡    点击
    {   dispatcher.Dispatch     (NormalClick_Event);     }
    private void EliteClick ( GameObject obj)                                                   // 精英关卡    点击
    {   dispatcher.Dispatch     (EliteClick_Event);      }


}
