using UnityEngine;
using System.Collections;

public class MainData : IMainData
{
    private int _buyedCoinsNum = 0;                     
    private int _coinsCrit = 1;         

    private string _randdomNickName = "";

    private bool _isMusicOpen = false;
    private bool _isSoundOpen = false;
    private bool _isCangetAward = false;

    public int currentbuyCoinsNum                       // 本次购买金币数量           
    {
        set { this._buyedCoinsNum = value; }
        get { return this._buyedCoinsNum; }
    }
    public int coinsCrit                                // 本次购买金币暴击倍数       
    {
        set { this._coinsCrit = value; }
        get { return this._coinsCrit; }
    }
    public string randomName                            // 服务器得到的随机名称       
    {
        set { this._randdomNickName = value; }
        get { return _randdomNickName; }
    }

    public bool isMusicOpen                             // 背景音乐开关              
    {
        set { this._isMusicOpen = value; }
        get { return this._isMusicOpen; }
    }
    public bool isSoundOpen                             // 音效开关                  
    {
        set { this._isSoundOpen = value; }
        get { return this.isSoundOpen; }
    }
    public bool isCanGetAward                           // 是否有可领取奖励           
    {
        set { this._isCangetAward = value; }
        get { return this._isCangetAward; }
    }
}
