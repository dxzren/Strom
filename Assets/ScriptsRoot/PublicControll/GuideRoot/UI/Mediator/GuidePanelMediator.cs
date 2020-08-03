using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary>  新手引导视图操作 (暂时不开启) </summary>

public class GuidePanelMediator : EventMediator
{
    [Inject]
    public GuidePanelView       View                    { get; set; }
//    [Inject]
//    public IGuideData           InGuideData             { get; set; }
    [Inject]
    public IPlayer              InPlayer                { get; set; }
    [Inject]
    public ICheckPointSys       InCheckPointSys         { get; set; }

    public int                  GuideNumber             = 0;

    Configs_BeginnersGuideData  TheBeginGuideData       = new Configs_BeginnersGuideData();     // 开始引导配置数据
    Configs_DialogueData        TheDialogData           = new Configs_DialogueData();           // 对话配置数据
    UIScrollView                ScrollViewToClose;                                              // 滚动视图

    public override void OnRegister()
    {
        View.OnInit();

//        TheBeginGuideData       = InGuideData.CurrentData;
        TheDialogData           = Configs_Dialogue.sInstance.GetDialogueDataByDialogueID(TheBeginGuideData.DialogueID[0]);

        if ( TheBeginGuideData.DialogueID[0] == 0 )
        {    Invoke("OnInit",View.setTime);       }
        else
        {    OnInit();                            }
    }

    public void                 OnInit()                                                        // 初始化本条引导                               
    {
//        ButtonSet();
//        DialogSet();
//        End();
        InvokeRepeating ("ButtonSet",.8f,0.5f);
    }
}
