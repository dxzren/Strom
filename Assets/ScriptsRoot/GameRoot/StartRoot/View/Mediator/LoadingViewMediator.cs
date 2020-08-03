using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

public class LoadingViewMediator : EventMediator
{
    float   processHE   = 0;
    string  titleValue  = "";

    [Inject]
    public LoadingView View { set; get; }

    public override void OnRegister()       
    {
        Debug.Log("06>    LoadingViewMediator.OnRegister()");
        View.Init();
        dispatcher.AddListener(StartEvent.Process_Event, ProcessHandler);                       // 监听进度并处理
        dispatcher.AddListener(StartEvent.SetLoadTitle_Event, SetTitleHandler);                 // 监听设置标题并处理
        dispatcher.AddListener(StartEvent.Hide_Event, HideEventHandler);                        // 监听隐藏并处理

        CameraSetManager.sInstance.AdaptiveUI();                                                // 自适应UI

        Invoke("StartCheckRes", 0.1f);                                                          // 停留0.1秒是将界面先显示,再加载资源
        InvokeRepeating("ShowTitle", 0, 0.5f);                                                  // 显示标题
        InvokeRepeating("ShowProcess", 0, 0.025f);                                              // 显示进度值
    }
    public override void OnRemove()         
    {
        dispatcher.RemoveListener(StartEvent.Process_Event, ProcessHandler);
        dispatcher.RemoveListener(StartEvent.SetLoadTitle_Event, SetTitleHandler);
        dispatcher.RemoveListener(StartEvent.Hide_Event, HideEventHandler);
    }

    public void StartCheckRes()                                                                 // 开始检测资源             
    {
        Debug.Log("07>    LoadingViewMediator.StartCheckRes()  开始检测资源");
        dispatcher.Dispatch(StartEvent.CheckRes_Event);                                         // 检测资源事件
        Util.LoadLocalText("LoadingTips", Configs_LoadingTips.sInstance.InitConfiguration);     // 加载本地文本 Tips

        int max     = Configs_LoadingTips.sInstance.mLoadingTipsDatas.Count;                    // 随机Tips信息
        int value   = Random.Range(1, max);
        View.TipsLabel.text = Language.GetValue(Configs_LoadingTips.sInstance.GetLoadingTipsDataByTipsID(value).TipsDes);
                                    
    }

    void RandomBGp()                                                                            // 随机加载背景图片         
    {
        int value = Random.Range(1, 4);
        View.BackgroundTexture.mainTexture = (Texture)Util.Load("Texture/LogIn/Loading_0" + value.ToString());
    }

    public void HideEventHandler()                                                              // 隐藏处理                 
    {
        this.gameObject.SetActive(false);
    }
    public void ProcessHandler(IEvent evt)                                                      // 加载进度处理             
    {
//        Debug.Log("Process...");
        processHE = (float)evt.data;
    }                                                   
    void ShowProcess()                                                                          // 显示进度                 
    {
        View.ProgressSlider.value   = processHE;
        View.UpdatingProgress.text  = (int)(processHE * 100) + "%";
    }
    public void SetTitleHandler(IEvent evt)                                                     // 设置标题处理             
    {
        titleValue = (string)evt.data;
    }                                                 
    public void ShowTitle()                                                                     // 显示标题                 
    {
        View.title.text = titleValue;
    }

}
