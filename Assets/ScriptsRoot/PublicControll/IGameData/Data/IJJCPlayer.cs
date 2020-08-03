using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IJJCPlayer
{
    int PlayerType { set; get; }                                            // 01.玩家类型(1:真人,2:预制NPC)
    int ID { set; get; }                                                    // 02.玩家ID
    int IconID { set; get; }                                                // 03.头像图标ID
    int Index { set; get; }                                                 // 04.玩家位置标记
    int Score { set; get; }                                                 // 05.玩家积分
    int JJCCoins { set; get; }                                              // 06.玩家竞技场金币

    int JJCRefreshTimes { set; get; }                                       // 07.竞技场刷新次数
    int Rank { set; get; }                                                  // 08.排名
    int GradeLevel { set; get; }                                            // 09.段位等级
    int GradeChallanged { set; get; }                                       // 10.已挑战段位
    int TimesLeft { set; get; }                                             // 11.挑战剩余次数
    int TimesBuyed { set; get; }                                            // 12.已购买挑战次数

    float RefreshStaminaCDSec { set; get;}                                  // 体力刷新时间倒计时
    float RefreshJJCCDSec { set; get; }                                     // 刷新CD倒计时
            
    string PlayerName { set; get; }                                         // 玩家名称
    string GuildID { set; get; }                                            // 公会名称

    List<IHeroData> GetLineUpForDefance { set; get; }                       // 玩家的防御阵容
}