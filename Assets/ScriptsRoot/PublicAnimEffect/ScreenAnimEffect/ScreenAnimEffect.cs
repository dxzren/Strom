using UnityEngine;
using System.Collections;
using StormBattle;

///-------------------------------------------------------------------------------------------------------------------------/// <summary> 屏幕动画  </summary>
public class ScreenAnimEffect:MonoBehaviour
{
    private static ScreenAnimEffect        _sInstance               = new ScreenAnimEffect();                               /// 实例化
    public static ScreenAnimEffect         sInstance
    {
        set { if (sInstance == null) sInstance                      = _sInstance; }
        get { return                _sInstance; }
    }

    public void                     ShowStripOutAnim_E()                                                                    // 百叶窗动画(条形抽出)
    {
        int                         TheLast                         = 0;
        Object                      NewObj                          = Util.Load(BattleResStrName.PanelName_StripAnim_E);
        GameObject                  TheObj                          = GameObject.Instantiate(NewObj) as GameObject;
        NewObj                                                      = null;

        TheObj.transform.SetParent  (GameObject.Find("TempGameObj").transform);
        TheObj.name                                                 = "StripAnim";
        TheObj.transform.localScale                                 = Vector3.one;
        for (int i = 0; i < TheObj.transform.childCount; i++)                                                               /// 分辨率适应    
        {
            TheObj.transform.GetChild(i).GetComponent<UISprite>().width     = Screen.width * 2;
            TheObj.transform.GetChild(i).GetComponent<UISprite>().height    = Screen.height / 16;
            TheObj.transform.GetChild(i).GetComponent<UISprite>().enabled   = true;
            TheLast                 = i;
            
        }
        TheObj.transform.GetChild(TheLast).GetComponent<TweenRotation>().onFinished.Add                                     /// 变换完成后 子级对象隐藏
            (new EventDelegate(()   =>{ TheObj.SetActive(false); }));
        TheObj.SetActive(true);
    }

}
