using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
/// <summary>
/// UILabel逐字显示效果
/// 直接拖拽到UiLabel的物体上即可，UIlabel的text中必须为空
/// </summary>
public class ShowLabelEffect : MonoBehaviour
{
    private UILabel label = null;
	void Start () {
        label = this.gameObject.GetComponent<UILabel>();
        label.enabled = false;
        InvokeRepeating("ShowLable", 0f, 0.5f);
	}

    public void ShowLable()
    {
        if (!string.IsNullOrEmpty(label.text))
        {
            CancelInvoke("ShowLable");
            label.enabled = true;
            TypewriterEffect effect = this.gameObject.GetComponent<TypewriterEffect>();
            if (effect == null)
            {
                effect = this.gameObject.AddComponent<TypewriterEffect>();
            }
            effect.charsPerSecond = 125;
            effect.fadeInTime = 0;
            effect.delayOnPeriod = 0;
            effect.delayOnNewLine = 0;
            EventDelegate.Add(effect.onFinished, delegate()
            {
                MonoBehaviour.DestroyImmediate(effect);
            }, true);
        }
    }
}
