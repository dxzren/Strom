using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public class MainRoot : ContextView
{
    [Inject]
    public IPlayer              InPlayer                    { get; set; }
    [Inject]
    public ICheckPointSys       InChcekPoint                { get; set; }

    private void Awake()
    {
        context                 = new MainContext(this);
    }
    private void Start()
    {
        //if ( !PanelManager.sInstance.IsShowPanel( PanelManager.sInstance.GetScene(SceneType.Main ),UIPanelConfig.MainMallPanel))                        /// 是否显示相同的面板 -Main
        //{
        //    if ( InPlayer.GuideProces < 420 )
        //    {
        //        InChcekPoint.chapterGoToID                  = 1;                                                                                        /// 前往章节ID
        //        PanelManager.sInstance.dispatcher.Dispatch( CheckPointEvent.REQ_CheckPointInfo_Event, UIPanelConfig.CheckPointSelectPanel );            /// 关卡信息
        //        if ( !InPlayer.IsGuide )                                                                                                                /// 是否强制引导
        //        {   PanelManager.sInstance.dispatcher.Dispatch( GuideEvent.StartSpcificGuide_Event, InPlayer.GuideProces);   }                          /// 打开特点的引导界面
        //    }
        //}
        //else
           PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.MainUIPanel);                                                                   /// 打开主界面面板
    }

}
