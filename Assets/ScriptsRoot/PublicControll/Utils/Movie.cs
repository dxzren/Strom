using UnityEngine;
using System.Collections;

public class Movie : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    public MovieTexture movTexture;
#endif
    public GameObject StartPanel;
    public bool loop = false;

    public void test() { }
    void Start()
    {
        StartPanel.SetActive(true);                     // 去掉动画,显示界面
        Util.BackToNomarl (StartPanel);                 // 拷贝UIAnimation中的 BlackToNormal_assist渐隐动画
    }
}
