using UnityEngine;
using System;
using System.Collections;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  时间刻度 </summary>
    public class Clocker : MonoBehaviour
    {
        public TimeSpan             StartLength                     = TimeSpan.Zero;                                        /// 开始长度
        public Action<TimeSpan>     TheTicked                       = null;                                                 /// 时间回调

        private bool                _IsPause                        = false;                                                /// 是否暂停
        private TimeSpan            TimeLength                      = TimeSpan.Zero;                                        /// 时间长度

        public void                 ClockerStart    ()                                                                      // 计时开始
        {
            ClockReset();
            Invoke("Ticked", 1);
                    } 
        public void                 ClockReset      ()                                                                      // 计时重置
        {
            _IsPause                                                = false;
            TimeLength                                              = StartLength;
        }
        public TimeSpan             Stop            ()                                                                      // 停止   
        {
            _IsPause                                                = true;
            CancelInvoke                                            ("Ticked");
            return                                                  TimeLength;
        }
        public TimeSpan             Pause           ()                                                                      // 暂停   
        {
            _IsPause                                                = true;
            CancelInvoke                                            ("Ticked");
            return                                                  TimeLength;
        }
        public void                 Continue        ()                                                                      // 继续   
        {
            _IsPause                                                = false;
            Invoke                                                  ("Ticked", 1);
        }
        private void                Ticked          ()                                                                      // 
        {
            TimeLength                                              = TimeLength.Add(new TimeSpan(0, 0, 1));
            if (TheTicked != null)                                  TheTicked(TimeLength);
            if (!_IsPause )                                         Invoke("Ticked", 1);

        }
    }
}
