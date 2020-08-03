using UnityEngine;
using System;
using System.Collections;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 轨迹移动 </summary>
    public interface ITrialMove      
    {
        float       MoveSpeed       { set; get; }                                                                           /// 移动速度

        float       Move            ( GameObject inObj, Vector3 inFrom, Vector3 inTo, float inTimer,                        // 对象移动
                                      Action inCallback = null, bool inIgnoreScale = false);
        float       MoveTo          ( GameObject inObj, Vector3 inTo, float inTimer,                                        // 移动到目标
                                      Action inCallback = null, bool inIgnoreScale = false);
        void        Stop();                                                                                                 // 停止移动
        void        Over();                                                                                                 // 结束移动
    }

    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 抛物线移动 </summary>
    public class ParabolaTrialMove   : ITrialMove
    {
        public static ITrialMove BuildMovable()
        {
            return ParabolaTrialMove.BuildMovable();
        }
        public float                MoveSpeed { get; set; }


        private float               rate                            = 0.4f;                                                 /// 比率
        private GameObject          MoveGoObj;                                                                              /// 移动对象
        private Action              Callback;                                                                               /// 封装完成回调
        private iTween.EaseType     EaseType                        = iTween.EaseType.linear;                               /// 松弛类型

        private                     ParabolaTrialMove()                                                                     //  构造函数 
        { MoveSpeed = BattleParmConfig.ParabolaSpeed; }

        public float                Move ( GameObject inObj, Vector3 inFrom, Vector3 inTo, float inTimer,                   //  对象移动
                                           Action inCallback = null, bool inIgnoreScale = false)
        {
            iTween                  TheiTween = inObj.GetComponent<iTween>();
            if (TheiTween != null) MonoBehaviour.Destroy(TheiTween);

            MoveGoObj               = inObj;                                                                        /// 移动对象
            Callback                = inCallback;                                                                   /// 完成回调

            if (inTimer == 0)       inTimer = Vector3.Distance(inFrom, inTo) / MoveSpeed;                           /// 持续时间 = 距离/移动速度

            Vector3[]               ThePosArr                       = new Vector3[3];                               /// 坐标组
            ThePosArr[0]                                            = inFrom;
            ThePosArr[1]                                            = inFrom + (inTo - inFrom) * rate + new Vector3(0, 1.5f, 0);
            ThePosArr[2]                                            = inTo;

            Hashtable               TheHash                         = new Hashtable();                              /// 哈希表
            TheHash.Add("id", "1");                                                                                 /// id
            TheHash.Add("path", ThePosArr);                                                                         /// 路径
            TheHash.Add("time", inTimer);                                                                           /// 时间
            TheHash.Add("easeType", EaseType);                                                                      /// 松弛类型
            TheHash.Add("islocal", true);                                                                           /// 是否局部
            TheHash.Add("movetopath", false);                                                                       /// 是否移动到路径
            TheHash.Add("ignoretimescale", inIgnoreScale);                                                          /// 是否无视缩放

            if (inCallback != null) TheHash.Add("oncomplete", inCallback);                                          /// 完成 回调
            iTween.MoveTo(inObj, TheHash);                                                                          /// MoveTo

            return inTimer;                                                                                         /// 返回持续时间
        }

        public float                MoveTo ( GameObject inObj, Vector3 inTo, float inTimer,                                 // 移动到目标
                                             Action inCallback = null, bool inIgnoreScale = false)
        {
            return                  Move(MoveGoObj, MoveGoObj.transform.localPosition, inTo, inTimer, inCallback, inIgnoreScale);
        }

        public void Stop()                                                                                                  // 停止移动 
        {
            if (MoveGoObj == null)  return;
            iTween.Stop(MoveGoObj);
        }
        public void Over()                                                                                                  // 结束移动 
        {
            if (MoveGoObj == null)  return;
            iTween.Stop(MoveGoObj);
            if (Callback != null) Callback();
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 直线移动 </summary>
    public class StraightTrialMove   : ITrialMove
    {
        public static ITrialMove    BuildMovable()
        {
            return new StraightTrialMove();
        }

        public float                MoveSpeed { get; set; }                                                                 /// 移动速度
        GameObject                  MoveObj;                                                                                /// 移动对象

        private                     StraightTrialMove()                                                                          // 构造函数
        {   MoveSpeed = 3; }

        public float                Move    ( GameObject inObj, Vector3 inFrom, Vector3 inTo, float inTimer,                // 对象移动
                                              Action inCallback = null, bool inIgnoreScale = false)
        {
            MoveObj                 = inObj;
            BattleParmConfig.OverTween(inObj.GetComponent<TweenPosition>());

            TweenPosition           TheTweenPos                     = inObj.AddComponent<TweenPosition>();
            if (inCallback != null) TheTweenPos.AddOnFinished       (new EventDelegate(() => inCallback()));
            TheTweenPos.from                                        = inFrom;
            TheTweenPos.to                                          = inTo;
            TheTweenPos.duration                                    = inTimer > 0 ? inTimer : Vector3.Distance ( inFrom, inTo) / MoveSpeed;
            TheTweenPos.ignoreTimeScale                             = inIgnoreScale;
            TheTweenPos.PlayForward();

            return TheTweenPos.duration;
        }

        public float MoveTo (GameObject inObj, Vector3 inTo, float inTimer, Action inCallback = null, bool inIgnoreScale = false )
        {
            return Move(inObj, inObj.transform.localPosition, inTo, inTimer, inCallback, inIgnoreScale);
        }

        public void Stop()                                                                                                  // 停止移动 
        {
            if (MoveObj == null)                                    return;
            TweenPosition           TheTweenPos                     = MoveObj.GetComponent<TweenPosition>();
            if (TheTweenPos != null)                                GameObject.Destroy(TheTweenPos);
        }
        public void Over()                                                                                                  // 结束移动 
        {
            if ( MoveObj == null )                                  return;
            TweenPosition           TheTweenPos                     = MoveObj.GetComponent<TweenPosition>();
            BattleParmConfig.OverTween                              (TheTweenPos);
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 闪电链移动 </summary>
    public class ThunderTrialMove    
    {
        GameObject                  MoveObj;                                                                                /// 移动对象
        public                      ThunderTrialMove() { }                                                                  /// 构造函数                                 

        public float                Move( GameObject inObj,  Transform inFromTF, Transform inToTF,                          //  特效移动
                                          float      inTime, Action    inCallback = null )
        {
            MoveObj                                                 = inObj;
            ThunderPalyer           TheTunderMem                    = inObj.GetComponent<ThunderPalyer>();
            if ( TheTunderMem != null )
            {
                TheTunderMem.Stop();
                MonoBehaviour.Destroy(TheTunderMem);
            }

            TheTunderMem                                            = inObj.AddComponent<ThunderPalyer>();
            TheTunderMem.StartEffect( inObj, inFromTF, inToTF, inTime, ()=>
            {
                if (TheTunderMem != null)
                {
                    TheTunderMem.Stop();
                    GameObject.Destroy(TheTunderMem);
                    if (inCallback != null) inCallback();
                }
            });
            return inTime;
        }

        public void                 Stop()                                                                                  //  停止 
        {
            if ( MoveObj == null )                                  return;

            ThunderPalyer           TheThunderMem                   = MoveObj.GetComponent<ThunderPalyer>();
            if ( TheThunderMem != null)
            {
                TheThunderMem.Stop();
                GameObject.Destroy(TheThunderMem);
            }
        }

        public void                 Over()                                                                                  //  结束 
        {
            if ( MoveObj == null )                                  return;

            ThunderPalyer           TheThunderMem                   = MoveObj.GetComponent<ThunderPalyer>();
            if ( TheThunderMem != null)
            {
                TheThunderMem.Over();
                GameObject.Destroy(TheThunderMem);
            }
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 摄像机移动 </summary>
    public class CameraTrialMove     : ITrialMove
    {
        public float                MoveSpeed                       { set; get; }
        public static ITrialMove    BuildMovable()  
        {       return new          CameraTrialMove();                            }
        private                     CameraTrialMove()                                                                            // 构造函数 
        {       MoveSpeed           = 3;                            }

        public float                Move    ( GameObject inCameraObj,Vector3 inFrom,Vector3 inTo,float inTimer, Action callback = null,bool ignoreScale = false)    // 对象 From_MoveTo_To  
        {
            BattleControll.sInstance.StartCoroutine(CameraMove(inCameraObj, inFrom, inTo, inTimer, callback, false));
            return inTimer;
        }
        public float                MoveTo  ( GameObject inCameraObj,Vector3 inTo,float inTimer, Action callback = null, bool ignoreScale = false)                  // 对接坐标移动 目标坐标 
        {
            return                  Move (inCameraObj, inCameraObj.transform.localPosition, inTo, inTimer, callback, ignoreScale);
        }
        public void                 Stop    ()
        {
            if ( MoveCameraObj == null )                            return;
            TweenPosition           TheTP                           = MoveCameraObj.GetComponent<TweenPosition>();
            if ( TheTP != null )                                    GameObject.Destroy(TheTP);
        }
        public void                 Over    ()
        {
            if ( MoveCameraObj == null )                            return;
            TweenPosition           TheTP                           = MoveCameraObj.GetComponent<TweenPosition>();
            BattleParmConfig.OverTween(TheTP);         
        }
        


        private float               ViewDistance                    = 4;                                                    /// 视野调整距离
        private GameObject          MoveCameraObj;                                                                          /// 移动对象
        IEnumerator                 CameraMove(GameObject inCameraObj, Vector3 inFrom, Vector3 inTo, float inTimer, Action callback, bool ignoreScale)              // 
        {
            MoveCameraObj                                           = inCameraObj;
            BattleParmConfig.OverTween(inCameraObj.GetComponent<TweenPosition>());
            TweenPosition           TheTween                        = inCameraObj.AddComponent<TweenPosition>();

            if (callback != null)   TheTween.AddOnFinished(new EventDelegate(() => callback()));

            TheTween.from                                           = inFrom;
            TheTween.to                                             = inTo;
            TheTween.duration                                       = inTimer;
            TheTween.duration                                       = inTimer > 0 ? inTimer : Vector3.Distance(inFrom, inTo) / inTimer;
            TheTween.ignoreTimeScale                                = ignoreScale;
            TheTween.PlayForward();

            if (inCameraObj.GetComponent<Camera>() != null)
            {
                float               Time1                           = ViewDistance / (Vector3.Distance(inFrom,inTo) / inTimer);
                float               Time2                           = inTimer - Time1;
                TweenFOV            TheFOV                          = inCameraObj.GetComponent<TweenFOV>();

                if (TheFOV != null)                                 MonoBehaviour.Destroy(TheFOV);
                TheFOV                                              = inCameraObj.AddComponent<TweenFOV>();
                TheFOV.from                                         = inCameraObj.GetComponent<Camera>().fieldOfView;
                TheFOV.to                                           = BattleParmConfig.CameraToFov;
                TheFOV.ignoreTimeScale                              = ignoreScale;
                TheFOV.duration = Time1;
                TheFOV.PlayForward();
                yield return new WaitForSeconds(Time2);
                TheFOV.PlayReverse();
            }
        }
    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 闪电链成员 </summary>
    public class ThunderPalyer      : MonoBehaviour
    {

        float                       Timer                           = 8;                                                    /// 持续时间
        float                       Alfatimer                       = 0;                                                    ///
        bool                        IsComplete                      = true;                                                 /// 是否完成

        GameObject EffeObj;                                                                                                 /// 特效对象
        Transform                   From_TF;                                                                                /// 起点 TF
        Transform                   To_TF;                                                                                  /// 终点 TF
        Transform                   BeginBone                       = null;                                                 /// 开始Bone TF
        Transform                   EndBone                         = null;                                                 /// 结束Bone TF
        System.Action               OnComplete;                                                                             /// 完成后 委托方法


        private void Update()
        {
            if (!IsComplete)
            {
                Alfatimer               += Time.deltaTime;
                EndBone.localPosition   = BeginBone.InverseTransformPoint(To_TF.position) * (Mathf.Min(Alfatimer / Timer, 1));
                if ( Alfatimer >= Timer + BattleParmConfig.ThunderHoldTime )
                {
                    IsComplete          = true;
                    if ( OnComplete != null ) OnComplete();
                    Destroy(this);
                }
            }
        }
        public void                 StartEffect ( GameObject inEffeObj, Transform inFromTF, Transform inToTF,               // 启动特效
                                                  float inTime, System.Action inOnComplete)
        {
            EffeObj                 = inEffeObj;                                                /// 特效对象
            From_TF                 = inFromTF;                                                 /// 起点 TF
            To_TF                   = inToTF;                                                   /// 终点 TF
            Timer                   = inTime;                                                   /// 持续时间
            OnComplete              = inOnComplete;                                             /// 完成后 委托方法

            Alfatimer               = 0;                                                        /// 
            IsComplete              = true;                                                     /// 是否完成

            BeginBone               = GetBoneTF( EffeObj.transform, "Bone001");                 /// 开始Bone TF
            EndBone                 = GetBoneTF( EffeObj.transform, "Bone002");                 /// 结束Bone TF

            if ( BeginBone != null && EndBone != null )
            {
                EffeObj.transform.parent                            = From_TF;
                BeginBone.localPosition                             = Vector3.zero;
                BeginBone.localRotation                             = Quaternion.identity;
                EndBone.localPosition                               = Vector3.zero;
                EndBone.localRotation                               = Quaternion.identity;

                Alfatimer                                           = 0;
                IsComplete                                          = false;
            }
        }
        
        public void                 Stop()                                                                                  // 停止       
        {
            if (!IsComplete)
            {
                IsComplete                                          = true;
                Destroy(this);
            }
        }

        public void                 Over()                                                                                  // 结束       
        {
            if (!IsComplete)
            {
                IsComplete                                          = true;
                if (OnComplete != null)                             OnComplete();
                Destroy(this);
            }
        } 

        Transform                   GetBoneTF(Transform inParent, string ObjName)                                           // 获取BoneTF 
        {
            Transform               TheTF                           = null;
            foreach ( var Item in inParent.GetComponentsInChildren<Transform>())
            {
                if (Item.name == ObjName)                           return Item;
            }
            return                  TheTF;
        }
    }

}

  