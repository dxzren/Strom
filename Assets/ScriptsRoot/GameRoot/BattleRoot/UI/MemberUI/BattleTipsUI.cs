using UnityEngine;
using System.Collections;
namespace StormBattle
{        
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 战斗提示面板 </summary>
    public class BattleTipsUI   : MonoBehaviour
    {
        public GameObject           HpAdd_Label;                                                                            /// 加血
        public GameObject           HpSub_Lable;                                                                            /// 血量减少
        public GameObject           HpAddBig_Label;                                                                         /// 加血大
        public GameObject           HpSubBig_Label;                                                                         /// 血量减少 大
        public GameObject           Energy_Label;                                                                           /// 能量 
        public GameObject           Word_Sprite;                                                                            /// 图标

        public GameObject           TipsHpAdd       ( string inText, GameObject inParent)                                   // 加血     提示    
        {
            return AddTipsText     (inText,HpAdd_Label,inParent);
        }
        public GameObject           TipsHpSub       ( string inText, GameObject inParent)                                   // 血量减少 提示    
        {
            return AddTipsText      (inText, HpSub_Lable, inParent);
        }
        public GameObject           TipsHpAddBig    ( string inText, GameObject inParent)                                   // 加血大   提示    
        {
            return AddTipsText      (inText,HpAddBig_Label,inParent);
        }
        public GameObject           TipsHpSubBig    ( string inText, GameObject inParent)                                   // 血量减少 提示    
        {
            return AddTipsText      (inText, HpSubBig_Label, inParent);
        }
        public GameObject           TipsEnergy      ( string inText, GameObject inParent)                                   // 能量     提示    
        {
            return                  AddTipsText(inText, Energy_Label, inParent);
        }
        public GameObject           TipsHitTarget   ( string inText, GameObject inParent)                                   // 命中     提示    
        {
            return AddTipsSprite    ( true, inText,Word_Sprite, inParent);
        }
        public GameObject           TipsMissTarget  ( string inText, GameObject inParent)                                   // 未命中   提示    
        {
            return AddTipsSprite    ( false,inText,Word_Sprite, inParent);
        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

        private string              GetRealSpriteName( string inText)                                                       // 获取图集子名   
        {
            switch ( inText)
            {
                case "攻速提升":                                return "gongsutisheng";
                case "伤害提升":                                return "shanghaitisheng";
                case "物理伤害提升":                             return "shanghaitisheng";
                case "魔法伤害提升":                             return "shanghaitisheng";
                case "普攻伤害提升":                             return "shanghaitisheng";
                case "物理攻击提升":                             return "wugongtisheng";
                case "魔法强度提升":                             return "faqiangtisheng";
                case "护甲提升":                                 return "hujiangtisheng";
                case "魔抗提升":                                 return "mokangtisheng";
                case "暴击提升":                                 return "baojitisheng";
                case "闪避":                                    return "shanbi";
                case "免疫":                                    return "mianyi";
                case "攻速降低":                                 return "gongsujiangdi";
                case "伤害减免":                                 return "shanghaijianmian";
                case "物理伤害减免":                             return "shanghaijianmian";
                case "物理攻击降低":                             return "wugongjiangdi";
                case "魔法强度降低":                             return "faqiangjiangdi";
                case "护甲降低":                                 return "hujiajiangdi";
                case "魔抗降低":                                 return "mokangjiangdi";
                case "暴击":                                    return "baoji";
                case "击杀奖励":                                 return "jishajiangli";
                default:
                    Debug.LogError("未找到文字提示对应图片，text=" + inText);
                    return "mianyi";
            }
        }
        private GameObject          AddTipsText( string inText, GameObject inTipsObj, GameObject inParent)                  // 添加提示文本   
        {
            GameObject              TheObj                          = MonoBehaviour.Instantiate(inTipsObj) as GameObject;               /// 实例化对象
            TheObj.transform.parent                                 = inParent.transform;                                               /// 指定父级对象
            TheObj.transform.localPosition                          = Vector3.zero;                                                     /// 位置坐标
            TheObj.transform.localRotation                          = Quaternion.identity;                                              /// 旋转值
            TheObj.transform.localScale                             = Vector3.one;                                                      /// 缩放值

            TheObj.GetComponent<UILabel>().text                     = inText;
            TheObj.GetComponent<UITweener>().onFinished.Add         ( new EventDelegate(() =>                                           //  缩放完成 后摧毁对象
            {
                GameObject.Destroy  ( TheObj );
            }));
            return                  TheObj;
        }
        private GameObject          AddTipsSprite( bool inIsGood, string inText, GameObject inTipsObj, GameObject inParent) // 添加提示图标   
        {
            GameObject              TheObj                          = MonoBehaviour.Instantiate(inTipsObj) as GameObject;               /// 实例化对象
            TheObj.transform.parent                                 = inParent.transform;                                               /// 指定父级对象
            TheObj.transform.localPosition                          = Vector3.zero;                                                     /// 位置坐标
            TheObj.transform.localRotation                          = Quaternion.identity;                                              /// 旋转值
            TheObj.transform.localScale                             = Vector3.one;                                                      /// 缩放值

            TheObj.GetComponent<UISprite>().spriteName              = GetRealSpriteName( inText ) + (inIsGood ? "_lan" : "_hong");      /// 图集子名
            TheObj.GetComponent<UISprite>().MakePixelPerfect();                                                                         /// 调整图片尺寸
            TheObj.GetComponent<UITweener>().onFinished.Add         ( new EventDelegate(() =>                                           //  缩放完成 后摧毁对象
            {
                GameObject.Destroy  ( TheObj );
            }));
            return                  TheObj;
        }
        #endregion
    }
}


