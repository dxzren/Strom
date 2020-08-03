using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRechargeData
{
    int rechargeID { set; get; }                                            // 正在充值的商品ID
    string rechargeURL { set; get; }                                        // 服务器充值地址

    bool isRec { set; get; }                                                // 是否正在充值,限制按钮点击
    bool isFromMainUI { set; get; }                                         // 是否从主界面头像VIP进入

    Dictionary<int,int> RechargeCardTimes { set; get; }                     // 充值卡充值次数
    System.Action Recharge { set; get; }                                    // 服务器请求完成后执行的充值方法
}