using UnityEngine;
using System.Collections;

namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 战斗对话界面 </summary>
    public class BattleDialogUI : MonoBehaviour
    {
        public UILabel              Name_La, Content_La;
        public UITexture            Body_Tex;
        public TypewriterEffect     TypeWriter;
        public GameObject           NextBtnObj;

        System.Action               CloseCallback                   = null;

 
        void Start()
        {
            TypeWriter.onFinished.Add(new EventDelegate(() =>
            {
                UIEventListener.Get(NextBtnObj).onClick = NextBtnClick;
            }));
            UIEventListener.Get(NextBtnObj).onClick = (GameObject Go) => { TypeWriter.Finish(); };
        }
        public void                 ShowDialog (Configs_HeroData inHero_C,string inContent,string inPlayerName,System.Action callback = null)       // 展示对话
        {
            Name_La.text            = Language.GetValue(inHero_C.HeroName);                                                 /// 英雄名称
            Content_La.text         = string.Format(Language.GetValue(inContent), inPlayerName);                            /// 对话内容
            Body_Tex.mainTexture    = (Texture)Util.Load("Texture/DialogIcon/" + inHero_C.Dialoguebody);                    /// 英雄半身像
            CloseCallback           = callback;                                                                             /// 回调函数
        }

        public void                 NextBtnClick(GameObject inObj)
        {
            PanelManager.sInstance.HidePanel(SceneType.Battle, this.name);
            CloseCallback();
        }
    }

}
