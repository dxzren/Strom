using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace StormBattle
{

    public class                    RealTimer : MonoBehaviour
    {
        private List<RealTimerTask> RealTimerList                   = new List<RealTimerTask>();                            // 

        public void                 Run(float inDurat, Action inCallback = null)
        {
            RealTimerTask           TheTimerTask                    = new RealTimerTask();

            this.enabled                                            = true;
            BattleControll.sInstance.CurrentGameSpeed               = BattleParmConfig.TimeScaleZero;
            TheTimerTask.Callback                                   = inCallback;
            TheTimerTask.Dration                                    = inDurat;
            TheTimerTask.StartTime                                  = Time.realtimeSinceStartup;
            RealTimerList.Add(TheTimerTask);
        }
        private void Update()
        {
            foreach(var Item in RealTimerList)
            {
                if ( Time.realtimeSinceStartup - Item.StartTime >= Item.Dration)
                {    Complete(Item);                                }                       
            }
        }
        private void                Complete(RealTimerTask inTask)                                                          // 完成   
        {
            RealTimerList.Remove(inTask);
            {
                if (RealTimerList.Count < 1)
                {
                    this.enabled = false;
                    {
                        if (!BattleControll.sInstance.IsBattlePause)
                        {    BattleControll.sInstance.CurrentGameSpeed = BattleControll.sInstance.CurrentGameSpeed;   }
                    }
                    inTask.Callback();
                }
            }
        }
        public class                RealTimerTask                                                                           // 
        {
            public float            StartTime                       = 0;
            public float            Dration                         = 0;
            public Action           Callback                        = null;
        }

    }
}
