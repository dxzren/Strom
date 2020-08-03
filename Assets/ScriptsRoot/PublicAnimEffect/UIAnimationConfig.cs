using UnityEngine;
using System.Collections;

public class UIAnimationConfig
{
    public const float BlackToNomarl_duration = 0.25f;              // 黑白界面动画切换时间
    public const float Alpha_duration = 0.3f;                       // 透明动画切换时间
    public const float Alpha_duration_0 = 0;                        // 透明动画，消失时出现蓝屏（透明时间大于消失时间，导致场景为空显示蓝屏）
    public const float Scale_duration = 0.2f;                       // 缩放动画切换时间
    public const float LineMove_duration = 0.2f;                    // 直线移动切换时间

    public static float Back_fromX = -438f;                         // 返回按钮  左上角飞入起点和终点
    public static float Back_fromY = 434.22f;
    public static float Back_fromZ = 0;

    public static float Back_toX = -438f;                           
    public static float Back_toY = 301;
    public static float Back_toZ = 0;

    public static float Top_fromX = -23.69f;                        // 顶部金币，经验，体力条 飞入的起点和终点
    public static float Top_fromY = 416.4f;
    public static float Top_fromZ = 0;

    public static float Top_toX = -23.69f;
    public static float Top_toY = 306f;
    public static float Top_toZ = 0;

    public static float Left_fromX = -639.9f;                       // 左边条 飞入的起点和终点
    public static float Left_fromY = -2.1f;
    public static float Left_fromZ = 0;

    public static float Left_toX = -514.48f;
    public static float Left_toY = -2.1f;
    public static float Left_toZ = 0;

    public static float Right_fromX = 681.3f;                       // 右边条 飞入的起点和终点
    public static float Right_fromY = 22f;
    public static float Right_fromZ = 0;

    public static float Right_toX = 503.14f;
    public static float Right_toY = 22f;
    public static float Right_toZ = 0;

    public static float Bottom_fromX = -23.7f;                      // 底部条 飞入的起点和终点
    public static float Bottom_fromY = -484.3f;
    public static float Bottom_fromZ = 0;

    public static float Bottom_toX = -23.7f;                           
    public static float Bottom_toY = -262.3f;
    public static float Bottom_toZ = 0;

    public static float List_fromX = 0;                             // List列表 飞入的起点和终点
    public static float List_fromY = -550.72f;
    public static float List_fromZ = 0;

    public static float List_toX = 0;                             // List列表 飞入的起点和终点
    public static float List_toY = 0;
    public static float List_toZ = 0;

    //------------<< 活动专用 >>---------------------------------------------------------------------------------------------------
    public static float Back_fromX_AC = -520f;                      // 返回按钮  左上角飞入起点和终点
    public static float Back_fromY_AC = 450.22f;
    public static float Back_fromZ_AC = 0;

    public static float Back_toX_AC = -520f;                        
    public static float Back_toY_AC = 301f;
    public static float Back_toZ_AC = 0;

    public static float Top_fromX_AC = -104f;                       // 顶部金币，经验，体力条 飞入的起点和终点
    public static float Top_fromY_AC = 420f;
    public static float Top_fromZ_AC = 0;

    public static float Top_toX_AC = -104f;
    public static float Top_toY_AC = 420f;
    public static float Top_toZ_AC = 0;

}
