using UnityEngine;
using System.Collections;
using StormBattle;

///-------------------------------------------------------------------------------------------------------------------------/// <summary> 英雄展示 </summary>
public class RoleShowUI : MonoBehaviour
{
    
    public static string            CurrentDuelRankMusic            = "";
    public UISprite                 Icon_Sp;
    public UILabel                  Type_La, Des_La, Name_La, Up_La, Down_La;
    public UITexture                Then_Tex, Hero_Tex, IconObj_Tex;

    public TweenRotation            IconRotate;
    public TweenAlpha               Tween_0, Tween_1, Tween_2;
    public TweenScale               Scale, Scale_1;
    public UISprite[]               StarArr_Sp;

    public GameObject               CloseBtnObj, GuideObj_1, GuideObj_2, BegainObj;
    public System.Action            Callback                        = null;


    private void Start()
    {
        UIEventListener.Get(CloseBtnObj).onClick                    = CloseClick;                                           ///
        BattleControll.sInstance.CurrentGameSpeed                   = 0;                                                    ///
        BaseInit();                                                                                                         ///

        if(BattleParmConfig.IsAutoBattle)                                                                                   // 自动战斗设置  
        {
            BattleControll.sInstance.RealTimerRunning.Run(4.5f, () =>
             {
                 Callback();
                 PanelManager.sInstance.HidePanel(SceneType.Battle, BattleResStrName.PanelName_RoleShowUI);
             });
        }
    }

    public void                     BaseInit()                                                                              // 基础初始化    
    {
        BegainObj.GetComponent<TweenPosition>().SetOnFinished(() =>                                                         // 
        {
            Tween_0.PlayForward();
            Scale_1.PlayForward();
        });                                                    

        Tween_0.SetOnFinished   (() =>                                                                                      // 
        {
            Scale.PlayForward();
        });

        Scale.SetOnFinished     (() =>                                                                                      // 
        {
            IconObj_Tex.color = new Color(255, 255, 255, 255);
            IconRotate.PlayForward();
        });

        IconRotate.SetOnFinished(() =>                                                                                      // 
        {
            GuideObj_1.SetActive(true);
            Tween_1.PlayForward();
        });

        Tween_1.SetOnFinished   (() =>                                                                                      // 
        {
            GuideObj_2.SetActive(true);
            Tween_2.PlayForward();
        });

    }
    public void                     CloseClick(GameObject inObj = null)                                                     // 关闭点击      
    {
        Callback();
        PanelManager.sInstance.HidePanel(SceneType.Battle, BattleResStrName.PanelName_RoleShowUI);        
    }
    public void                     HeroInit(int inHeroID)                                                                  // 英雄初始化    
    {
        Configs_HeroData            TheHero_C                       = Configs_Hero.sInstance.GetHeroDataByHeroID(inHeroID);
        if(inHeroID != 0)
        {
            Hero_Tex.mainTexture                                    = Resources.Load("Texture/DialogIcon/" + TheHero_C.Dialoguebody) as Texture;
            Name_La.text                                            = Language.GetValue(TheHero_C.HeroName);
            Des_La.text                                             = Language.GetValue(TheHero_C.HeroDes);
            switch(TheHero_C.Profession)
            {
                case 1:
                    Type_La.text = "战士";
                    Icon_Sp.spriteName = "careericon_zhanshi_new";
                    break;
                case 2:
                    Type_La.text = "刺客";
                    Icon_Sp.spriteName = "careericon_cike_new";
                    break;
                case 3:
                    Type_La.text = "法师";
                    Icon_Sp.spriteName = "careericon_fashi_new";
                    break;
                case 4:
                    Type_La.text = "射手";
                    Icon_Sp.spriteName = "careericon_sheshou_new";
                    break;
                case 5:
                    Type_La.text = "辅助";
                    Icon_Sp.spriteName = "careericon_fuzhu_new";
                    break;
            }
            int                     TheCount                        = TheHero_C.InitialStar;
            for(int i = 0; i < 5; i++ )                                                             
            {
                if (i < TheCount)   StarArr_Sp[i].enabled           = true;
                else                StarArr_Sp[i].enabled           = false;
            }
        }
    }

    private float                   _ClipTime                       = 0.3f;
    private const string            _Path                           = "Audio/{0}";
    AudioClip                       _TheClip                        = null;


}
