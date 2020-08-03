using UnityEngine;
using System.Collections;

/// <summary> 商城系统事件 </summary>
public class MallEvent
{
    public static string REQ_OnceDraw_Event                     = "REQ_OnceDraw_Event";                             // 单次抽奖请求
    public static string RET_OnceDraw_Event                     = "RET_OnceDraw_Event";                             // 单次抽奖回调
    public static string REQ_TenTimesDraw_Event                 = "REQ_TenTimesDraw_Event";                         // 十连请求
    public static string RET_TenTimesDraw_Event                 = "RET_TenTimesDraw_Event";                         // 十连回调
    public static string REQ_CoinDrawAgain_Event                = "REQ_CoinDrawAgain_Event";                        // 金币再次抽奖
    public static string REQ_DiamondDrawAgain_Event             = "REQ_DiamondDrawAgain_Event";                     // 钻石再次抽奖
    public static string REQ_CoinTenTimesDrawAgain_Event        = "REQ_CoinTenTimesDrawAgain_Event ";               // 金币十连重复
    public static string REQ_DiamondTenTimesDrawAgain_Event     = "REQ_DiamondTenTimesDrawAgain_Event ";            // 钻石十连重复
}
