using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary> 场景切换加载视图 </summary>
public class SceneLoadingView : MonoBehaviour
{
    public static GameObject    HideObj;
    public UILabel              LoadLabel, LoadTips;
    public UISlider             LoadSlider;
    public UITexture            Load_BG;
    public GameObject           LoadGameObj;

    AsyncOperation              TheAsyncOp;

    private void Start          ()                                                                                          // 初始化            
    {
        HideObj                                             = this.gameObject;
        HideThis                                            (false);
        SceneController.SetLoadingGameObj                   (LoadGameObj);
        StartCoroutine                                      (LoadScene());
        LoadInit();
        Util.BackToNomarl                                   (this.gameObject);
    }
    private void OnGUI          ()                                                                                          // 异步读取场景,刷新UI
    {
        LoadLabel.text                                      = SceneController.progressValue + "%";
        LoadSlider.value                                    = SceneController.progressValue / 100f;
    }


    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
    public static void          HideThis        (bool hide)                                                                 // 隐藏当前UI   
    {   HideObj.SetActive(!hide);   }
    private IEnumerator         LoadScene       ()                                                                          // 加载场景     
    {
        SceneController.progressValue                       = 0;
        if (SceneController.GetScenName() == "Battle")                                                                      // 战斗场景异步加载
        { TheAsyncOp = SceneManager.LoadSceneAsync(SceneController.GetScenName(), LoadSceneMode.Additive); }
        else
        {
            if ( !IsInvoking("Test"))
            {    InvokeRepeating("Test", 0.5f, 0.02f);      }

            while ((SceneController.progressValue < 100))                                                                   /// 场景加载进度
            {   yield return null;                          }
            try
            {   SceneManager.LoadScene ( SceneController.GetScenName(), LoadSceneMode.Single);}
            catch ( System.Exception e)
            {
                Debug.Log("error = " + e.ToString());
                throw;
            }
            CameraSetManager.sInstance.AdaptiveUI();                                                                        /// 自适应屏幕设置
        }
    }
    private void                LoadInit        ()                                                                          // 加载初始化   
    {
        int a                                               = Random.Range(1, 4);
        Load_BG.mainTexture                                 = (Texture)Util.Load("Texture/LogIn/Loding_0" + a.ToString());
        int max                                             = Configs_LoadingTips.sInstance.mLoadingTipsDatas.Count;
        int b                                               = Random.Range(1, max);
        LoadTips.text                                       = Language.GetValue(Configs_LoadingTips.
                                                              sInstance.GetLoadingTipsDataByTipsID(b).TipsDes);
        GameObject TheEffect                                = ( GameObject)Instantiate(Util.Load(
                                                                "Prefabs/UIEffect/uieffect_loading" + a.ToString()));
        if (TheEffect != null )                             TheEffect.transform.SetParent(Load_BG.transform);
        TheEffect.transform.localScale                      = Vector3.one;

    } 
    private void                Test            ()                                                                          // 测试        
    {
        if ( SceneController.progressValue < 100 )
        {
            short TheIndex                                  = (short)Random.Range(1, 3);
            SceneController.progressValue                   += TheIndex;
            if( SceneController.progressValue > 100 )
            {   SceneController.progressValue = 100;}
        }
        else
        { CancelInvoke("Test"); }
    }

    #endregion
}
