using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
/// <summary>
/// 左上角的返回按钮的飞入动画
/// 直接拖拽到Button的物体上即可
/// </summary>
public class FlyInEffect
{

    public static void BackButtonFlyInEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Back_fromX, UIAnimationConfig.Back_fromY, UIAnimationConfig.Back_fromZ }, new float[] { UIAnimationConfig.Back_toX, UIAnimationConfig.Back_toY, UIAnimationConfig.Back_toZ });
    }


    public static void BottomButtonFlyInEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Bottom_fromX, UIAnimationConfig.Bottom_fromY, UIAnimationConfig.Bottom_fromZ }, new float[] { UIAnimationConfig.Bottom_toX, UIAnimationConfig.Bottom_toY, UIAnimationConfig.Bottom_toZ });
    }

    public static void BottomButtonFlyOutEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj,  new float[] { UIAnimationConfig.Bottom_toX, UIAnimationConfig.Bottom_toY, UIAnimationConfig.Bottom_toZ },new float[] { UIAnimationConfig.Bottom_fromX, UIAnimationConfig.Bottom_fromY, UIAnimationConfig.Bottom_fromZ });
    }

    
    public static void LeftButtonFlyInEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Left_fromX, UIAnimationConfig.Left_fromY, UIAnimationConfig.Left_fromZ }, new float[] { UIAnimationConfig.Left_toX, UIAnimationConfig.Left_toY, UIAnimationConfig.Left_toZ });
    }

    public static void LeftButtonFlyOutEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj,  new float[] { UIAnimationConfig.Left_toX, UIAnimationConfig.Left_toY, UIAnimationConfig.Left_toZ },new float[] { UIAnimationConfig.Left_fromX, UIAnimationConfig.Left_fromY, UIAnimationConfig.Left_fromZ });
    }

    public static void RightButtonFlyInEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Right_fromX, UIAnimationConfig.Right_fromY, UIAnimationConfig.Right_fromZ }, new float[] { UIAnimationConfig.Right_toX, UIAnimationConfig.Right_toY, UIAnimationConfig.Right_toZ });
    }
    public static void RightButtonFlyOutEffect(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Right_toX, UIAnimationConfig.Right_toY, UIAnimationConfig.Right_toZ }, new float[] { UIAnimationConfig.Right_fromX, UIAnimationConfig.Right_fromY, UIAnimationConfig.Right_fromZ });
    }


    public static void TopButtonFlyInEffect(GameObject objj)
    {

        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Top_fromX, UIAnimationConfig.Top_fromY, UIAnimationConfig.Top_fromZ }, new float[] { UIAnimationConfig.Top_toX, UIAnimationConfig.Top_toY, UIAnimationConfig.Top_toZ });
    }

    public static void TopButtonFlyOutEffect(GameObject objj)
    {

        UIAnimation.Instance().LineMove(objj,  new float[] { UIAnimationConfig.Top_toX, UIAnimationConfig.Top_toY, UIAnimationConfig.Top_toZ },new float[] { UIAnimationConfig.Top_fromX, UIAnimationConfig.Top_fromY, UIAnimationConfig.Top_fromZ });
    }
    /////////////////////////////////////////////////
    public static void TopButtonFlyInEffect_AC(GameObject objj)
    {

        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Top_fromX_AC, UIAnimationConfig.Top_fromY_AC, UIAnimationConfig.Top_fromZ_AC }, new float[] { UIAnimationConfig.Top_toX_AC, UIAnimationConfig.Top_toY_AC, UIAnimationConfig.Top_toZ_AC });
    }
    public static void BackButtonFlyInEffect_AC(GameObject objj)
    {
        UIAnimation.Instance().LineMove(objj, new float[] { UIAnimationConfig.Back_fromX_AC, UIAnimationConfig.Back_fromY_AC, UIAnimationConfig.Back_fromZ_AC }, new float[] { UIAnimationConfig.Back_toX_AC, UIAnimationConfig.Back_toY_AC, UIAnimationConfig.Back_toZ_AC });
    }
    //////////////////////////////////////////////////

}
