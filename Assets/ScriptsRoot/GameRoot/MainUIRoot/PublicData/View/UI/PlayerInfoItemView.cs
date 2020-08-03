using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
/// <summary> 玩家信息视图展示 </summary>

public class PlayerInfoItemView : EventView
{
    [Inject]
    public IPlayer              InPlayer                    { set; get; }

    public string               BuyCoin_Event               = "BuyCoin_Event";
    public string               BuyDiamond_Event            = "BuyDiamond_Event";
    public string               BuyStamina_Event            = "BuyStamina_Event ";
    public string               PlayerIconClick_Event       = "PlayerIconClick_Event";
    public string               PlayerSetClick_Event        = "PlayerSetClick_Event";

    public GameObject           BuyCoinBtn, BuyDiamondBtn, BuyStaminaBtn;
    public UILabel              Diamond, Coin, Stamina;

    private long                theDiamonds                 = -1;
    private long                theCoins                    = -1;
    private long                theStamina                  = -1;
    public void Init()
    {
        UIEventListener.Get(BuyStaminaBtn).onClick          = BuyStaminaClick;
    }
    private void BuyStaminaClick ( GameObject obj )
    {

    }
}