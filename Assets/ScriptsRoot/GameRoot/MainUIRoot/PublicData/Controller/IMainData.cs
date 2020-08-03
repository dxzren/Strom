using UnityEngine;
using System.Collections;

public interface IMainData
{
    int currentbuyCoinsNum { set; get; }                // 本次购买得到的金币
    int coinsCrit { set; get; }                         // 本次点击的暴击倍数       
    string randomName { set; get; }                     // 服务器得到的随机名称

    bool isMusicOpen { set; get; }                      // 音乐开关
    bool isSoundOpen { set; get; }                      // 音效开关
    bool isCanGetAward { set; get; }                    // 是否有可领奖励
}