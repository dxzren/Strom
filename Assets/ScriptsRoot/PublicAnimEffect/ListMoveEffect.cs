using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
/// <summary>
/// 列表从下向上飞入
/// 
/// </summary>
public class ListMoveEffect : MonoBehaviour
{
    
	void Start () {
        return;
        //设置动画播放前的位置
        this.gameObject.transform.localPosition = new Vector3(UIAnimationConfig.List_fromX, UIAnimationConfig.List_fromY, UIAnimationConfig.List_fromZ);
        Invoke("startMove", UIAnimationConfig.BlackToNomarl_duration*2f);
	}

    /// <summary>
    /// 播放动画
    /// </summary>
    public void startMove() 
    {
        UIAnimation.Instance().LineMove(this.gameObject, new float[] { UIAnimationConfig.List_fromX, UIAnimationConfig.List_fromY, UIAnimationConfig.List_fromZ }, new float[] { UIAnimationConfig.List_toX, UIAnimationConfig.List_toY, UIAnimationConfig.List_toZ });
    }
}
