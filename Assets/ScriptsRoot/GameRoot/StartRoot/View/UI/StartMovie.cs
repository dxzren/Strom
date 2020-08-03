using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.SceneManagement;

public class StartMovie : EventView
{
    public void Init()
    {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        StartCoroutine(PlayStartAnimation());
        //NewSoundManager.sInstance.PlayMusicAudio( NewSoundManager.sInstance.GetSoundNameByPanelName(
                                                    "UIs/LogIn/Word"), NewSoundType.loop);
#endif
#if UNITY_EDITOR
        HideMovie();
#endif
    }
    void ClickIT (GameObject go)
    {
        UIEventListener.Get(this.gameObject).onClick = null;                /// 注册UI按钮
        StopAllCoroutines();                                                /// 停止所有的协同程序
        HideMovie();
    }
    IEnumerator PlayStartAnimation()                                        /// 播放动画
    {
        Handheld.PlayFullScreenMovie    ("startshow.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        yield return new WaitForSeconds ( 0.3f);
        HideMovie();
;    }
    void HideMovie()
    {
        PanelManager.sInstance.HidePanel(SceneType.Start, "UIs/LogIn/StartMovie");
        SceneManager.LoadScene("LogIn");
    }
}
