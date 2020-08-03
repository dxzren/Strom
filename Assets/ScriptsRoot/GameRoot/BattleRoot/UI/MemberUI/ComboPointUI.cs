using UnityEngine;
using LinqTools;
using System.Collections;
using System.Collections.Generic;
namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------// <summary> 连击UI </summary>
    public class ComboPointUI : MonoBehaviour
    {
        public UILabel              ComboNum_La, DamageNum_La;                                                              /// 连击数字, 伤害数字
        public UITexture            StatePic_Tex;                                                                           /// 连击_评价
        public GameObject           ComboPointUIObj, PartGroupObj;                                                          /// This对象,特效组对象

        private int                 num                             = 0;                                                    /// 连击数
        private float               time                            = 1.2f;                                                 /// 时间
        private List<Texture2D>     StatePicList                    = new List<Texture2D>();                                /// 连击_评价图片组

        public void Start()
        {
            StatePicList.Add        (Resources.Load("Texture/Battle/haibucuo") as Texture2D);
            StatePicList.Add        (Resources.Load("Texture/Battle/feichanghao") as Texture2D);
            StatePicList.Add        (Resources.Load("Texture/Battle/henjingcai") as Texture2D);
        }
        public int                  CombineNum                                                                              // 连击数字       
        {
            set
            {
                num                 = value;
                CallDisplay();
            }
            get                     { return num;}
        }

        private void                CallDisplay()                                                                           // 显示          
        {
            if ( num >= 2)
            {
                ComboNum_La.text                                   = num.ToString();
                DamageNum_La.text                                 = (num * 5).ToString();

                PartGroupObj.SetActive(false);
                ComboPointUIObj.SetActive(true);

                TweenScale[]        TSArr                           = ComboNum_La.gameObject.GetComponents<TweenScale>();
                if (TSArr.Length != 0)                              TSArr.ToList().ForEach(T => Destroy(T));
                ComboNum_La.gameObject.transform.localScale        = Vector3.one * 2;
                TweenScale          TheTS                           = ComboNum_La.gameObject.AddComponent<TweenScale>();
                TheTS.from                                          = Vector3.one * 2;
                TheTS.to                                            = Vector3.one;
                TheTS.duration                                      = 0.1f;
                TheTS.delay                                         = 0;
                TheTS.onFinished.Add                                ( new EventDelegate(() => 
                {   ComboNum_La.gameObject.transform.localScale    = Vector3.one; }));

                TweenAlpha          TheTA                           = ComboPointUIObj.GetComponent<TweenAlpha>();
                if (TheTA != null )                                 Destroy(TheTA);
                ComboPointUIObj.GetComponent<UISprite>().alpha           = 1;
                SetTime();

                TweenPosition       TheTP                           = StatePic_Tex.gameObject.GetComponent<TweenPosition>();
                if ( TheTP != null )                                Destroy(TheTP);
            }
            if      ( num >= 15 )
            {
                StatePic_Tex.gameObject.SetActive(true);
                StatePic_Tex.mainTexture                                = StatePicList[2];
                StateAnim();
            }
            else if ( num >= 10 && num < 15 )
            {
                StatePic_Tex.gameObject.SetActive(true);
                StatePic_Tex.mainTexture                                = StatePicList[1];
                StateAnim();
            }
            else if ( num >= 0 && num < 10)
            {
                StatePic_Tex.gameObject.SetActive(true);
                StatePic_Tex.mainTexture                                = StatePicList[0];
                StateAnim();
            }
            else                                                    StatePic_Tex.gameObject.SetActive(false);
        }
        private void                StateAnim()                                                                             // 状态动作       
        {
            TweenPosition           TheTP                          = StatePic_Tex.gameObject.GetComponent<TweenPosition>();
            if ( TheTP == null )
            {
                TheTP                                               = StatePic_Tex.gameObject.AddComponent<TweenPosition>();
                StatePic_Tex.gameObject.transform.localPosition     = new Vector3(250, StatePic_Tex.gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
                TheTP.from                                          = new Vector3(250, StatePic_Tex.gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
                TheTP.to                                            = new Vector3(66f, StatePic_Tex.gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
                TheTP.duration                                      = 0.1f;
                TheTP.delay                                         = 0;
                TheTP.onFinished.Add                                ( new EventDelegate(()=> 
                {   StatePic_Tex.gameObject.transform.localPosition     = new Vector3(250, StatePic_Tex.gameObject.transform.localPosition.y, gameObject.transform.localPosition.z); }));
            }
        }
        private void                SetTime()                                                                               // 设置重置时间   
        {
            CancelInvoke("ComboReset");
            Invoke("ComboReset", time);
        }
        private void                ComboReset()                                                                            // 重置设置       
        {
            TweenAlpha          TheTA                               = ComboPointUIObj.GetComponent<TweenAlpha>();
            if (TheTA != null)                                      Destroy(TheTA);
            TheTA                                                   = ComboPointUIObj.AddComponent<TweenAlpha>();
            TheTA.from                                              = 1;
            TheTA.to                                                = 0;
            TheTA.duration                                          = 0.3f;
            num                                                     = 0;
        }
    }

}
