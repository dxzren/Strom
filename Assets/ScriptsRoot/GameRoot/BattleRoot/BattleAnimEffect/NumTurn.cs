using UnityEngine;
using System.Collections;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  数字增加/减少 渐变动画 </summary>
    public class NumTurn : MonoBehaviour
    {
        public int              Speed                           = 1;                            // 速度
        public UILabel          Num_Label;                                                      // 数字UI

        private int             _From                           = 0;                            // 起始值
        private int             _TurnValue                      = 0;                            // 变化值

        public void             Turn(int inFrom, int inTurnVale)
        {
            CancelInvoke();
            _From                                               = inFrom;
            _TurnValue                                          = inTurnVale;
            Num_Label.text                                      = inFrom.ToString();
            if                  (inTurnVale > 0)                TurnUp();
            else                                                TurnDown();
        }
        private void            TurnUp()                                                        // 数字增加 
        {
            if (_TurnValue > Speed )
            {
                _From                                           += Speed;
                _TurnValue                                      -= Speed;
            }
            else
            {
                _From                                           += _TurnValue;
                _TurnValue                                      = 0;
            }
            Num_Label.text                                      = _From.ToString();
            Invoke                                              ("TurnUp",0.05f);
        }

        private void            TurnDown()                                                      // 数字减少 
        {
            if (_TurnValue < 0)
            {
                if (-_TurnValue > Speed )
                {
                    _From                                       -= Speed;
                    _TurnValue                                  += Speed;
                }
                else
                {
                    _From                                       -= _TurnValue;
                    _TurnValue                                  = 0;
                }
                Num_Label.text                                  = _From.ToString();
                Invoke                                          ("TurnDown", 0.1f);
            }
        }

    }
}
