using UnityEngine;
using System.Collections;

///-------------------------------------------------------------------------------------------------------------------------/// <summary> 进度条追赶动画 </summary>

public class FollowSlider : MonoBehaviour
{
    public UISlider                 TargetSlider;                                                                           /// 目标进度条

    private const float             CatchSpeed                      = 1f;                                                   /// 捕获速度
    private UISlider                TheSlider ;                                                                             /// 本体进度条
	void Start ()
    {
        TheSlider                   = this.GetComponent<UISlider>();                                                        /// 初始化
	}

	void Update ()
    {
	    if (TheSlider != null)
        {
            if          (TheSlider.value < TargetSlider.value)    TheSlider.value       = TargetSlider.value;               /// ( 跟随目标进度                 )
            else if     (TheSlider.value > TargetSlider.value)    TheSlider.value       -= CatchSpeed * Time.deltaTime;     /// ( 进度 - 捕抓速度 * 最后一帧秒数) 
        }
	}
}
