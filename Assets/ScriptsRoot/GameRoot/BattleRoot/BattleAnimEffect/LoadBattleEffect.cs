using UnityEngine;
using System.Collections;
namespace StormBattle
{    
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  加载战斗特效 </summary>
    public class LoadBattleEffect : MonoBehaviour
    {
        public string                   EffectName                      = "";                                               /// 特效名称
        void Start()
        {
            string                      path                            = "Prefabs/BattleUIEffect/" + EffectName;           /// 特效路径
            UnityEngine.Object          TempObj                         = Util.Load(path);                                  /// 引擎对象
            GameObject                  TheObj                          = Instantiate(TempObj) as GameObject;               /// 游戏对象
            if (TheObj != null)
            {
                TheObj.transform.parent                                 = this.transform;                                   /// 父级对象
                TheObj.transform.localPosition                          = Vector3.one;                                      /// 位置
                TheObj.transform.localScale                             = Vector3.zero;                                     /// 缩放
                TheObj.transform.localRotation                          = Quaternion.identity;                              /// 旋转
            }
            TempObj                                                     = null;
        }

    }
}

