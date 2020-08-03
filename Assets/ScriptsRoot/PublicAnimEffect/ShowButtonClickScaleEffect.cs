using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
/// <summary>
/// UIButton返回按钮的缩放动画
/// 直接拖拽到Button的物体上即可
/// </summary>
public class ShowButtonClickScaleEffect : MonoBehaviour
{
    
	void Start () {
        UIButtonScale animation = this.gameObject.GetComponent<UIButtonScale>();
        if (animation == null)
        {
            animation = this.gameObject.AddComponent<UIButtonScale>();
        }
        animation.hover = new Vector3(1, 1, 1);
        animation.pressed = new Vector3(0.9f, 0.9f, 0.9f);
            
	}
}
