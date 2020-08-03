using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;

public class UIAnimation
{   
    public static string        LoadOverEvent               = "LoadOverEvent";                  // 当前界面数据准备好以后，就发送这个事件，将界面显示出来

    static UIAnimation          _Instance                   = null;
    public static UIAnimation   Instance()                                                      // 实例                                               
    {
        if(_Instance == null)
        {
            _Instance = new UIAnimation();
        }
        return _Instance;
    }

    GameObject              tempBlackIns        = null;
    UISprite                tempSprite          = null;

    public void BlackToNormal (GameObject obj,bool isShow)                               // 1.创建动画：从黑屏到正常亮度渐变 || 销毁动画：正常亮度到黑屏渐变 isShow:true 显示，false 消失           
    {
        Debug.Log("UIAnimation__BlackToNormal :");
        UnityEngine.Object black                = Util.Load(UIPanelConfig.BlackPanel);
        GameObject blackIns                     = (GameObject)MonoBehaviour.Instantiate(black);
        blackIns.name                           = "blackPanel";
        blackIns.transform.parent               = obj.transform;
        blackIns.transform.localPosition        = Vector3.zero;
        blackIns.transform.localRotation        = Quaternion.identity;
        blackIns.transform.localScale           = Vector3.one;
        black                                   = null;
        UISprite sprite                         = blackIns.GetComponentInChildren<UISprite>();
        TweenAlpha alpha                        = sprite.gameObject.AddComponent<TweenAlpha>();
        alpha.duration                          = UIAnimationConfig.BlackToNomarl_duration;
        if(isShow)
        {
            alpha.from                          = 1;
            alpha.to                            = 0.1f;
            EventDelegate.Add(alpha.onFinished, delegate ()
            {
                sprite                          = null;
                MonoBehaviour.DestroyImmediate(blackIns);
            },true);
        }
        else
        {
            alpha.from                          = 0.1f;
            alpha.to                            = 1;
            EventDelegate.Add(alpha.onFinished, delegate ()
            {
                sprite                          = null;
                MonoBehaviour.DestroyImmediate(blackIns);
                obj.SetActive(false);
            }, true);
        }
    }

    public void BlackToNormal_assist()                                                                          //   _播放动画                                                                                             
    {
        Debug.Log("BlackToNormal_assist:");
        try
        {
            TweenAlpha alpha = tempSprite.gameObject.AddComponent<TweenAlpha>();
            alpha.duration = UIAnimationConfig.BlackToNomarl_duration;
            alpha.from = 1;
            alpha.to = 0.1f;
            EventDelegate.Add(alpha.onFinished, delegate ()
             {
                 tempSprite = null;
                 MonoBehaviour.DestroyImmediate(tempBlackIns);
             },true);
        }
        catch(Exception ex)
        {

        }
    }
                                                                                                 
    public void BlackToNormalMain(GameObject hidePanel,GameObject obj,bool isShow)                          //   [主界面定制] 创建动画：从黑屏到正常亮度渐变 || 销毁动画：正常亮度到黑屏渐变                           
    {
        UnityEngine.Object black = Util.Load(UIPanelConfig.BlackPanel);
        GameObject blackIns = (GameObject)MonoBehaviour.Instantiate(black);
        blackIns.name = "blackPanel";
        blackIns.transform.parent = hidePanel.transform;
        blackIns.transform.localPosition = Vector3.zero;
        blackIns.transform.localRotation = Quaternion.identity;
        blackIns.transform.localScale = Vector3.one;
        black = null;
        UISprite sprite = blackIns.GetComponentInChildren<UISprite>();
        TweenAlpha alpha = sprite.gameObject.AddComponent<TweenAlpha>();
        alpha.duration = UIAnimationConfig.BlackToNomarl_duration;
        alpha.from = 0.1f;
        alpha.to   = 1;
        EventDelegate.Add(alpha.onFinished, delegate ()
         {
             obj.SetActive(true);
             BlackToNormal(obj, isShow);
         }, true);
    }

    public void StarUpBlackToNormal(GameObject obj,bool isShow)                          //   [升星升阶界面打开和关闭的定制逻辑] 创建动画：从黑屏到正常亮度渐变 || 销毁动画：正常亮度到黑屏渐变     
    {
        UnityEngine.Object black = Util.Load(UIPanelConfig.BlackPanel);
        GameObject blackIns = (GameObject)MonoBehaviour.Instantiate(black);
        blackIns.name = "blackPanel";
        blackIns.transform.parent = obj.transform;
        blackIns.transform.localPosition = Vector3.zero;
        blackIns.transform.localRotation = Quaternion.identity;
        blackIns.transform.localScale = Vector3.one;
        black = null;
        UISprite sprite = blackIns.GetComponentInChildren<UISprite>();
        if(isShow)
        {
            TweenAlpha alpha = sprite.gameObject.AddComponent<TweenAlpha>();
            alpha.duration = UIAnimationConfig.BlackToNomarl_duration;
            alpha.from = 1f;
            alpha.to = 0.1f;
            EventDelegate.Add(alpha.onFinished, delegate ()
             {
                 MonoBehaviour.DestroyImmediate(blackIns);
             }, true);
        }
        else
        {
            TweenAlpha alpha = sprite.gameObject.AddComponent<TweenAlpha>();
            alpha.duration = UIAnimationConfig.BlackToNomarl_duration;
            alpha.from = 0.1f;
            alpha.to   = 1f;
            EventDelegate.Add(alpha.onFinished, delegate ()
             {
                 MonoBehaviour.DestroyImmediate(blackIns);
             }, true);
        }
    }

    public void TransAlpha_TransScale(GameObject obj,bool isShow)                                               // 2.创建动画：有无到有+由小变大 < 销毁动画：由有到无+有大变小 >                                           
    {
        TransAlpha(obj, isShow);
        TransScale(obj, isShow);
    }
    public void TransAlpha(GameObject obj,bool isShow)                                                          //   _创建动画：由无到有 || 销毁动画：由有到无 >                                                           
    {
        if(isShow)
        {
            TweenAlpha          alpha                       = obj.AddComponent<TweenAlpha>();
            alpha.duration                                  = UIAnimationConfig.Alpha_duration;
            alpha.from                                      = 0f;
            alpha.to                                        = 1f;
            EventDelegate.Add(alpha.onFinished, delegate ()
             {
                 MonoBehaviour.DestroyImmediate(alpha);
             }, true);
        }
        else
        {
            TweenAlpha alpha = obj.AddComponent<TweenAlpha>();
            alpha.duration = UIAnimationConfig.Alpha_duration_0;
            alpha.from = 1f;
            alpha.to   = 0f;
            EventDelegate.Add(alpha.onFinished, delegate ()
             {
                 if (obj != null)
                 {
                     MonoBehaviour.DestroyImmediate(obj);
                 }
             }, true);
        }
    }
    public void TransScale(GameObject obj,bool isShow)                                                      //   _创建动画：由小变大 || 销毁动画：由大变小 >                                                           
    {
        if(isShow)
        {
            TweenScale scale        = obj.AddComponent<TweenScale>();
            scale.duration          = UIAnimationConfig.Scale_duration;
            scale.from              = new Vector3(0.1f,0.1f,0.1f);
            scale.to                = new Vector3(1f, 1f, 1f);

        }
        else
        {
            TweenScale scale        = obj.AddComponent<TweenScale>();
            scale.duration          = UIAnimationConfig.Scale_duration;
            scale.from              = new Vector3(1f, 1f, 1f);
            scale.to                = new Vector3(0.11f,0.1f, 0.11f);
            EventDelegate.Add       (scale.onFinished, delegate ()
            {
                if (obj != null)
                {
                    MonoBehaviour.DestroyImmediate(obj);
                }
            }, true);
        }
    }
    public void EquipTransAlphaBack(GameObject obj)                                                             //   [装备界面定制，勿乱用] 创建动画：由无到有 || 销毁动画：由有到无                                       
    {
        TweenAlpha alpha = obj.AddComponent<TweenAlpha>();
        alpha.duration = UIAnimationConfig.Alpha_duration - 0.1f;
        alpha.from = 1f;
        alpha.to = 0f;
        EventDelegate.Add(alpha.onFinished, delegate ()
         {
             obj.SetActive(false);
         }, true);
    }

    public void LineMove(GameObject obj, float[] from, float[] to )                                            // 3.创建动画：直线移动 销毁动画：直线返回                                                                 
    {
        TweenPosition           position                    = obj.AddComponent<TweenPosition>();
        position.duration                                   = UIAnimationConfig.LineMove_duration;
        position.from                                       = new Vector3(from[0], from[1], from[2]);
        position.to                                         = new Vector3(to[0], to[1], to[2]);
        EventDelegate.Add                                   ( position.onFinished, delegate ()
         {
             MonoBehaviour.DestroyImmediate(position);
         }, true);
    }
    public void LineMove(GameObject obj, float[] from, float[] to, bool isShow)                                 //   [背包附属界面使用] 创建动画：直线移动 销毁动画：直线返回                                              
    {
        TweenPosition           position                    = obj.AddComponent<TweenPosition>();
        position.duration                                   = UIAnimationConfig.LineMove_duration;
        position.from                                       = new Vector3(from[0], from[1], from[2]);
        position.to                                         = new Vector3(to[0], to[1], to[2]);
        EventDelegate.Add                                   (position.onFinished, delegate ()
        {
            if (!isShow)
            {
                MonoBehaviour.DestroyImmediate(obj);
            }
            else
            {
                MonoBehaviour.DestroyImmediate(position);
            }
        }, true);
    }

}