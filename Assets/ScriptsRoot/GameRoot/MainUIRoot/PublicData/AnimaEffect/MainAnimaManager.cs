using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary> 主界面 2D 动画管理 </summary>
public class MainAnimaManager : MonoBehaviour
{
    public int                  time;                                                           // 线程启动延迟时间设置
    public bool                 disappear;                                                      //
    public List<Vector3>        PositionList                = new List<Vector3>();              //
    public List<string>         NameList                    = new List<string>();               //

    private int                 aimPos = 1;                                                     // 目标位置
    private int                 nameIndex = 0;                                                  //
    private bool                animaGo                     = true;                             // 动画运行
    private TweenPosition       ThePosTween;                                                    // 位移对象
    private UISpriteAnimation   TheUISpriteAnima;                                               // UI动画对象

    private void Start()
    {
        ThePosTween             = transform.GetComponent<TweenPosition>();
        TheUISpriteAnima        = transform.GetComponent<UISpriteAnimation>();
        InvokeRepeating         ("ResetAnima", time,time);
    }

    private void                ResetAnima()                                                    // 重置动画
    {
        transform.GetComponent<UISprite>().MakePixelPerfect();                                  // 调节合适像素效果的比例
        if ( nameIndex == NameList.Count - 1 )
        {    nameIndex = 0;     }
        else
        {    nameIndex += 1;    }
        TheUISpriteAnima.namePrefix     = NameList[nameIndex];                                  // 设置名称前缀
        Destroy(ThePosTween);
        ThePosTween                     = transform.gameObject.AddComponent<TweenPosition>();   // 区间位移
        ThePosTween.from                = transform.localPosition;                              // 源坐标
        ThePosTween.duration            = time;                                                 // 持续时间

        if      ( animaGo && aimPos == PositionList.Count - 1)                                   
        {
            aimPos                      -= 1;
            animaGo                     = false;
        }
        else if ( !animaGo && aimPos == 0)                                                       
        {
            aimPos += 1;
            animaGo = false;
            if ( disappear )
            {
                CancelInvoke            ("ResetAnima");
                transform.GetComponent<UISprite>().enabled  = false;
                Destroy                 (ThePosTween);
                Invoke                  ("ResetAnima_2", 2 );
                return;    
            }
        }
        else                                                                                     
        {
            if ( animaGo)
            {    aimPos += 1;   }
            else
            {    aimPos -= 1;   }
        }
        ThePosTween.to                  = PositionList[aimPos];                                 // 目的坐标
        transform.GetComponent<UISprite>().MakePixelPerfect();                                  // 调节合适像素效果的比例
        ThePosTween.PlayForward();                                                              // 播放位移
    }
    private void                ResetAnima_2()
    {
        ThePosTween                                         = transform.gameObject.AddComponent<TweenPosition>();
        transform.GetComponent<UISprite>().enabled          = true;
        ThePosTween.from                                    = PositionList[0];
        ThePosTween.to                                      = PositionList[1];
        transform.GetComponent<TweenPosition>().duration    = time;
        transform.GetComponent<TweenPosition>().PlayForward();
        InvokeRepeating                                     ("ResetAnima", time, time);
    }
}
