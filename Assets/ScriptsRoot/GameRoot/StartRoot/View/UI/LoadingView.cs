using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class LoadingView : EventView
{
    public UISlider     ProgressSlider;                                     // 进度滑块
    public UILabel      UpdatingProgress, title, TipsLabel;                 // 更新滑块,标题,加载说明标签
    public UITexture    BackgroundTexture;                                  // 背景图片

    public void Init()
    {
        PlayerPrefs.DeleteAll();                                            // 去除所有键值
    }
}

