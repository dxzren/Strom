using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LinqTools;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  成员模型上方显示集合 </summary>
    public class MoveMemUI : MonoBehaviour
    {
        [HideInInspector]
        public GameObject           ModelUpObj                      = null;

        public float                bodyHeight                      = 2;

        private bool                isShow                          = true;

        void Update()
        {
            if (ModelUpObj != null && BattleControll.sInstance.MainCamera != null)                                            /// UI 位置
            {
                Vector3             ThePos                          = ModelUpObj.transform.position;
                ThePos.y                                            += bodyHeight;
                ThePos                                              = BattleControll.sInstance.MainCamera.WorldToScreenPoint(ThePos);
                ThePos.z                                            = 0;
                this.transform.position                             = BattleControll.sInstance.UICamera.ScreenToWorldPoint(ThePos);
            }
        }
        public void                 Show()                                                                                  // 显示血条,能量条
        {
            if(isShow)
            {
                isShow                                              = false;
                List<UISprite>      TheUiSpriteList                 = new List<UISprite>();

                TheUiSpriteList                                     = this.transform.GetComponentsInChildren<UISprite>().ToList();
                foreach(var Item in TheUiSpriteList)
                {
                    TweenAlpha      TheTA                           = Item.gameObject.GetComponent<TweenAlpha>(); 
                    if (TheTA != null )                             DestroyImmediate(TheTA);
                    Item.alpha                                      = 1;
                }
            }
        }
        public void                 Hide()                                                                                  // 隐藏血条,能量条
        {
            isShow                                                  = true;
            List<UISprite>      TheUiSpriteList                     = new List<UISprite>();

            TheUiSpriteList                                         = this.transform.GetComponentsInChildren<UISprite>().ToList();
            foreach(var Item in TheUiSpriteList)
            {
                TweenAlpha      TheTA                               = Item.gameObject.GetComponent<TweenAlpha>(); 
                if (TheTA == null )
                {
                    TheTA                                           = Item.gameObject.AddComponent<TweenAlpha>();
                    TheTA.from                                      = 1;
                    TheTA.to                                        = 0;
                    TheTA.duration                                  = 0.6f;
                }
            }
        }
    }
}
