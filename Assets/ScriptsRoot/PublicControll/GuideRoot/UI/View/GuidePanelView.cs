using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>  新手引导视图 (暂时不开启) </summary>
public class GuidePanelView : EventView
{
    public string               GuideMask_Event                 = "GuideMask_Event";            /// 引导遮罩
    public string               GuideMoveToNext_Event           = "GuideMoveToNext_Event";      /// 引导移动到下一级
    public string               GuideHalo_Event                 = "GuideHalo_Event";            /// 引导光环
    public string               EndGuide_Event                  = "EndGuide_Event";             /// 结束引导

    public float                setTime                         = 0.8f;
    public UILabel              LeftTalkerName,  RightTalkerName,  GuiderWords,
                                LeftTalkerWords, RightTalkerWords, WordsOverWords;
    public UITexture            LeftTalkerIcon,  RightTalkerIcon,  GiderIcon;
    public UISprite             Hand, Halo;
    public GameObject           Mask, LeftTalker, RightTalker, Guider, WordsOver, WholeMask, 
                                MoveToNext, GuoduMask, EndGuide, TipsHalo, HalfAlphaMask; 
    public GameObject[]         MaskList;

    public void OnInit()
    {
        UIEventListener.Get(WholeMask.gameObject).onClick       = ViewClick;                    // 视图点击
        UIEventListener.Get(MoveToNext.gameObject).onClick      = MoveToNextClick;              // 移动到下一级点击
        UIEventListener.Get(EndGuide).onClick                   = EndGuideClick;                // 结束引导点击

        for (int i = 0; i < MaskList.Length; i++ )
        {
            UIEventListener.Get(MaskList[i]).onClick            = HaloClick;                    // 光环点击
        }
    }



    public void                 ViewClick ( GameObject obj )                                    // 视图点击
    {   dispatcher.Dispatch ( GuideMask_Event );           }            

    public void                 MoveToNextClick ( GameObject obj )                              // 移动到下一级点击
    {   dispatcher.Dispatch ( GuideMoveToNext_Event);      }  

    public void                 EndGuideClick ( GameObject obj )                                // 结束引导点击
    {   dispatcher.Dispatch ( EndGuide_Event );            }

    public void                 HaloClick ( GameObject obj )                                    // 光环点击
    {   dispatcher.Dispatch ( GuideHalo_Event );           }
}
