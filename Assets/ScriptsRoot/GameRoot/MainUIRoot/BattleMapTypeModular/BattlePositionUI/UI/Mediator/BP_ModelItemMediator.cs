using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class BP_ModelItemMediator : EventMediator
{
    [Inject]
    public BP_ModelItemView     View                        { set; get; }
    [Inject]
    public IPlayer              InPlayer                    { set; get; }
    [Inject]
    public IPersetLineupData    InBattlePos                 { set; get; }


    public override void        OnRegister()
    {
        View.ViewInit();
        BaseInit();

        View.dispatcher.AddListener(View.ModelClick_Event, ModelClickHandler);
    }
    public override void        OnRemove()
    {
        View.dispatcher.RemoveListener(View.ModelClick_Event, ModelClickHandler);
    }

    private void                ModelClickHandler()
    {
        foreach (var Item in InPlayer.BattleTypeToLineUp(InBattlePos.BattleType))                              // 确认位置 是否有英雄
        {
            if (Item.Key == View.PosNum)
            {
                View.TheHeroD                           = Item.Value;
            }
        }

        InBattlePos.TempHero_D                          = View.TheHeroD;                                        // 指定临时英雄数据
        dispatcher.Dispatch     (BattlePosEvent.HeroIconClickDownHero_Evnet);                                   // 预置阵容 (英雄图标点击事件)-    

    }
    private void BaseInit()
    {
        GetLineupPos();
    }
    private void GetLineupPos()
    {
        //BattlePosData = 
    }
}