using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RewardAllView : EventView
{
    public string sureButton_Event = "SureButton_Event";
    public string closeButton_Event = "CloseButton_Event";

    public UILabel sureLabel, sureLabel2, sureLabel3;
    public UISprite BG;
    public UITexture upLine, downLine;
    public UIGrid listGrid1, listGrid2, listGridCurrency, listGridProp;

    public GameObject sureButton, closeButton, sureButton2, sureButton3, allPanel, currencyPanel, propPanel;


    public void onInit()
    {
        UIEventListener.Get(sureButton).onClick = SureButton;
        UIEventListener.Get(sureButton2).onClick = SureButton;
        UIEventListener.Get(sureButton3).onClick = SureButton;
        UIEventListener.Get(closeButton).onClick = CloseButton;
    }

    public void SureButton(GameObject obj)
    {

        dispatcher.Dispatch(sureButton_Event);
    }
    public void CloseButton(GameObject obj)
    {
        dispatcher.Dispatch(closeButton_Event);
    }
}