using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
///---------------------------------------------------------------------------------------------------------// <summary> 场景控制 </summary>

public class SceneController
{

    public static short             progressValue                   = 0;                                                    /// 加载进度值
    public static Action            LoadSceneCallback               = null;                                                 /// 加载场景回调 封装方法 
    public static GameObject        Scene                           = null;                                                 /// 场景对象
    public static GameObject        SceneSetting                    = null;                                                 /// 场景设置对象
    public static Camera            SceneCamera                     = null;                                                 /// 场景摄像机


    private static string           GSceneName                      = "";                                                   /// 场景对象名称
    private static string           currentBattleScene              = "";                                                   /// 当前战斗场景名称
    private static GameObject       LoadingGameObject               = null;                                                 /// 加载对象

    private static IEnumerator      LoadScene               (string sceneName)                                              // 加载场景                    
    {
        AsyncOperation              AO                              = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return                AO;
    }
    public static string            GetScenName()                                                                           // 获取场景名称                
    {
        return GSceneName;
    }

    public static void              LoadScene               (string sceneName,LoadSceneType type = LoadSceneType.Normal)    // 加载目标场景                
    {
        GSceneName                  = sceneName;
        Debuger.Log                 ("SceneController.LoadScene :" + sceneName);
        SceneManager.LoadScene      ("Loading");
    }
    public static void              SetLoadingGameObj       (GameObject obj)                                                // 设置加载对象                
    {
        LoadingGameObject           = obj;
    }
    public static void              SetLoadProgress         (short progress)                                                // 设置加载进度                
    {
        progressValue               = progress;
    }
    public static void              SceneLoadComplete()                                                                     // 销毁加载场景资源            
    {
        MonoBehaviour.DestroyImmediate(LoadingGameObject);                          
        CameraSetManager.sInstance.AdaptiveUI();                                                                            // 自适应UI
    }
    public static void              LoadBattleSceneAsync    (string sceneName,Action callback)                              // 异步加载战斗场景            
    {
        SceneManager.LoadScene      (sceneName, LoadSceneMode.Additive);
        LoadSceneCallback           = callback;
    }
    public static void              UnLoadBattleScene       (string sceneName = "")                                         // 卸载战斗场景                
    {
        if(sceneName == "" || sceneName == currentBattleScene)
        {
            SceneManager.UnloadScene(currentBattleScene);
        }
        else
        {
            SceneManager.UnloadScene(sceneName);
        }
        Scene                       = null;
        SceneCamera                 = null;
        SceneSetting                = null;
    }

}

public enum LoadSceneType                                                                                                   // 加载场景类型               
{
    WithProgressBar,                                                                                                        /// 手动控制销毁加载场景
    Normal
}