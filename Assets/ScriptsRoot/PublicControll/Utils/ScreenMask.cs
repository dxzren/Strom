using UnityEngine;
using System.Collections;

public class ScreenMask : MonoBehaviour
{
    private float lastWidth     = 0f;                       // 尾端宽度
    private float lastHeight    = 0f;                      // 尾端高度

    void OnEnable()
    {
        InvokeRepeating("SetScreenMask", 0, 4);
    }

    public void SetScreenMask()
    {
        if(lastWidth != Screen.width || lastHeight != Screen.height)
        {
            Vector2 setTB = new Vector2(Screen.width, Mathf.CeilToInt(Screen.height - Screen.width / 16f * 9f));        // 新建 按现宽度 16/9 重置高度
            transform.FindChild("TopMask").GetComponent<RectTransform>().sizeDelta = setTB;                             // 设置顶部
            transform.FindChild("BottomMask").GetComponent<RectTransform>().sizeDelta = setTB;                          // 设置底部

            Vector2 setLR = new Vector2(Mathf.CeilToInt(Screen.width + 6 - Screen.height / 9f * 16f), Screen.height);   
            transform.FindChild("LeftMask").GetComponent<RectTransform>().sizeDelta = setLR;
            transform.FindChild("RightMask").GetComponent<RectTransform>().sizeDelta = setLR;

            lastWidth = Screen.width;
            lastHeight = Screen.height;
        }
    }
}
