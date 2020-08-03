using UnityEngine;
using System.Collections;
using StormBattle;
///-------------------------------------------------------------------------------------------------------------------------/// <summary> 摄像机动画  </summary>
public class CameraAnimEffect
{
    private CameraAnimEffect            _sInstance                      = new CameraAnimEffect();                           /// 实例化
    public CameraAnimEffect             sInstance
    {
        get
        {   return              _sInstance;  }
        set
        {   if (sInstance == null)      sInstance                       = _sInstance;   }
    }
    
    public float                        TargetHightFix                  = 0.5f;                                             /// 修正目标高度(中心为底的对象)
    public float                        TimeScaleZoomIn                 = 0.45f;                                            /// 镜头拉近 时间拉伸
         
    public void                         CameraZoomIn    ( Camera inCamera, Vector3 inTargetPosV3, int inTime )              // 摄影机镜头 拉近 目标坐标
    {
        float                           FromViewField                   = inCamera.fieldOfView;             
        float                           ToViewField                     = FromViewField * 0.45f;
        GameObject                      TheCameraObj                    = inCamera.gameObject;
        BattleEffect   TheEffect        = ResourceKit.sInstance.GetEffect(BattleResStrName.CameraZoomInEffect);             /// 镜头拉近特效

        Object                          NewObj                          = Util.Load(BattleResStrName.PanelName_ScreenZoomIn);/// 实例化 镜头拉近参数面板
        GameObject                      TheObj                          = GameObject.Instantiate(NewObj) as GameObject;
        if (TheObj != null)             TheObj.transform.SetParent      (GameObject.Find("TempGameObj").transform);
        TweenPosition                   TheTweenPos                     = TheObj.GetComponent<TweenPosition>();
                                                                                                                            ///< |镜头区域拉近 > 
        Time.timeScale                                                  = TimeScaleZoomIn;                                  /// 时间拉升
        TheEffect.duration              = inTime;                                                                           /// 持续时间
        TheEffect.PlayEffect            (inCamera.transform, Vector3.zero);                                                 /// 播放特效

        TweenFOV                        TheTFOV                         = TheCameraObj.GetComponent<TweenFOV>();            /// 变换视图区域
        OverTween(TheTFOV);                                         

        TheTFOV                         = TheCameraObj.AddComponent<TweenFOV>();                                            /// 添加变换组件
        TheTFOV.animationCurve          = TheTweenPos.animationCurve;                                                       /// 获取实例 变换曲线参数
        TheTFOV.ignoreTimeScale         = false;                                                                            /// 时间拉伸影响(false)'
        TheTFOV.style                   = UITweener.Style.Once;                                                             /// 一次
        TheTFOV.from                    = FromViewField;                                                                    /// 起始范围
        TheTFOV.to                      = ToViewField;                                                                      /// 目标范围
        TheTFOV.duration                = TheEffect.duration;                                                               /// 持续时间

        TheTFOV.onFinished.Clear        ();
        TheTFOV.onFinished.Add          (new EventDelegate(delegate()
        {
            BattleControll.sInstance.CurrentGameSpeed                   = BattleControll.sInstance.CurrentGameSpeed;
            TheTFOV.enabled                                             = false;
        }));
        TheTFOV.enabled                                                 = true;
        TheTFOV.ResetToBeginning        ();
        TheTFOV.PlayForward             ();
                                                                                                                            ///< |镜头移动 拉近目标 > 
        Vector3                         FromPosV3                       = TheCameraObj.transform.position;
        Vector3                         ToPosV3                         = inTargetPosV3 + new Vector3(0,TargetHightFix,0);  //
        
        TweenPosition                   TheTP                           = TheCameraObj.AddComponent<TweenPosition>();       //
        OverTween(TheTP);

        TheTP                           = TheCameraObj.AddComponent<TweenPosition>();
        TheTP.animationCurve            = TheTweenPos.animationCurve;
        TheTP.style                     = UITweener.Style.Once;
        TheTP.from                      = FromPosV3;
        TheTP.to                        = ToPosV3;
        TheTP.duration                  = TheEffect.duration;

        TheTP.onFinished.Clear();
        TheTP.onFinished.Add(new EventDelegate(delegate()
        {
            BattleControll.sInstance.CurrentGameSpeed                   = BattleControll.sInstance.CurrentGameSpeed;
            TheTP.enabled                                               = false;
        }));
        TheTP.enabled                                                   = true;
        TheTP.ResetToBeginning();
        TheTP.PlayForward();

        Object.Destroy(NewObj);
        Object.Destroy(TheObj);
    }
    public void                         CameraViration  ( Camera inCamera )                                                 // 震屏效果     
    {
        GameObject                      TheCameraObj                    = inCamera.gameObject;
        Vector3                         CameraPosV3                     = inCamera.transform.localPosition;
        Object                          NewObj          = Util.Load (BattleResStrName.PanelName_ScreenVibration);
        GameObject                      TheObj          = GameObject.Instantiate(NewObj) as GameObject;

        TweenPosition                   TheTPos                         = TheObj.GetComponent<TweenPosition>();
        TweenPosition                   TheTP                           = TheCameraObj.GetComponent<TweenPosition>();
        OverTween(TheTP);

        TheTP.animationCurve            = TheTPos.animationCurve;
        TheTP.ignoreTimeScale           = false;
        TheTP.style                     = UITweener.Style.Once;
        TheTP.from                      = TheCameraObj.transform.localPosition;
        TheTP.to                        = new Vector3(CameraPosV3.x, CameraPosV3.y - 0.3f,CameraPosV3.z );
        TheTP.duration                  = TheTPos.duration;

        TheTP.onFinished.Clear();
        TheTP.onFinished.Add(new EventDelegate(delegate()
        {
            TheCameraObj.transform.localPosition                        = CameraPosV3;
            TheTP.enabled                                               = false;
            Object.Destroy(TheTP);
        }));
        TheTP.enabled                                                   = true;
        TheTP.ResetToBeginning();
        TheTP.PlayForward();

        Object.Destroy(NewObj);
        Object.Destroy(TheObj);
    }
    public static void                  OverTween       ( UITweener inTween )                                               // 结束变换     
    {
        if ( inTween != null )
        {
            if (inTween.tweenFactor < 1 )
            {
                inTween.Sample(1, true);                                                                                    /// 参数取样 (Factor,onFinished)
                inTween.onFinished.ForEach(P => P.Execute());                                                               /// 事件重载
            }
            Object.Destroy(inTween);                                                                        
        }
    }
}