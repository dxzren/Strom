using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RechargeData : IRechargeData
{
    private bool   _isRec = false;
    private bool   _isFromMainUI = false;

    private Action _recharge = () => { };
    private Dictionary<int, int> _rechargeCardTimes = new Dictionary<int, int>();

    private int    _rechargeID = 0;
    private string _rechargeURL = "";


    public bool isFromMainUI                                                                    // 是否从主界面头像VIP进入                         
    {
        set { this._isFromMainUI = value; ; }
        get { return this._isFromMainUI; }
    }

    public bool isRec                                                                           // 是否正在充值,限制按钮点击                        
    {
        set { this._isRec = value; }
        get { return this._isRec; }
    }

    public Action Recharge                                                                      // 服务器请求完成后执行的充值方法                    
    {
        set { this._recharge = value; }
        get { return this._recharge; }
    }

    public Dictionary<int, int> RechargeCardTimes                                               // 充值卡充值次数                                  
    {
        set { this._rechargeCardTimes = value; }
        get { return this._rechargeCardTimes; }
    }

    public int rechargeID                                                                       // 正在充值的商品ID                                
    {
        set { this._rechargeID = value; }
        get { return this._rechargeID; }
    }

    public string rechargeURL                                                                   // 服务器充值地址                                  
    {
        set { this._rechargeURL = value; }
        get { return this._rechargeURL; }
    }
}

