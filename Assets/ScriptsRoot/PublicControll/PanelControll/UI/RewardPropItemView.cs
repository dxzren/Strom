using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class RewardPropItemView : EventView
{

    public int propID;
    public ItemType propType;
    public UISprite propIcon, propIconFrame;
    public UILabel propCount;

    public void onInit()
    {
        UIEventListener.Get(this.gameObject).onPress = ShowTips;
    }
    void ShowTips(GameObject obj,bool show)
    {
        PanelManager.sInstance.ShowTipsPanel(gameObject, show, propID, propType, null);
    }
}