using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
/// <summary>
/// UIButton 点击变暗动画
/// 直接拖拽到Button的物体上即可
/// </summary>
public class ShowButtonClickDimEffect : MonoBehaviour
{
    public UISprite button;

	void Start () {
        UIButtonColor animation = this.gameObject.GetComponent<UIButtonColor>();
        if (animation == null)
        {
            animation = this.gameObject.AddComponent<UIButtonColor>();
        }
        animation.tweenTarget = button.gameObject;
        animation.hover = new Color(1,1,1);
        animation.pressed = new Color(100f/255, 100f/255, 100f/255);
	}
}
