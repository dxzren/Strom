using UnityEngine;
using System.Collections;

/// <summary>  背包消息命令  </summary>
enum BAG_CMD
{
    REQ_BAG_List            = 1,                        // 背包数据列表请求
    REQ_BAG_SellItem        = 2,                        // 抽出物品请求
    REQ_BAG_MergeItem       = 3,                        // 合成物品请求
    REQ_BAG_UseItem         = 4,                        // 使用物品请求

    RET_BAG_List            = 51,                       // 背包数据列表回调
    RET_BAG_SellItem        = 52,                       // 出售物品回调
    RET_BAG_MergeItem       = 53,                       // 合成物品回调
    RET_BAG_UseItem         = 54,                       // 使用物品回调
    RET_BAG_SoulBag         = 56,                       // 魂石包回调

    RET_Push_BAG_ChangeItem = 101                       // 服务器主动推送 物品改变
}