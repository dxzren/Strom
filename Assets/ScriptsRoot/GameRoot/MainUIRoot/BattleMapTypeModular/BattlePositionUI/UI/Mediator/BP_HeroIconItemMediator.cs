using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class BP_HeroIconItemMediator : EventMediator
{
    [Inject]
    public BP_HeroIconItemView      View                        { set; get; }
    [Inject]
    public IPlayer                  InPlayer                    { set; get; }
    [Inject]
    public IPersetLineupData        InBattlePos                 { set; get; }

    bool                            isBtnOn                     = false;                        // 按钮激活状态
    public override void            OnRegister()                
    {
        View.ViewInit();
        BaseInit();
        View.dispatcher.AddListener(View.HeroIconClick_Event, HeroIconClickHandler);

    }  
    public override void            OnRemove()                  
    {
        View.dispatcher.AddListener(View.HeroIconClick_Event, HeroIconClickHandler);
    }
    private void                    BaseInit()                  
    {
        IHeroData                   TheHeroD                = InPlayer.GetHeroByID(View.TheHeroID);                 /// 获取英雄数据
        View.GetComponent<UISprite>().spriteName            = Util.GetQualityFrameName(TheHeroD.Quality);           /// 获取英雄边框 图集子名
        View.HeroIcon.spriteName    = Configs_Hero.sInstance.GetHeroDataByHeroID(View.TheHeroID).head84;            /// 需要头像     图集子名
        switch(Configs_Hero.sInstance.GetHeroDataByHeroID(View.TheHeroID).Polarity)                                 /// 元素属性( 冰,火,雷 )    
        {
            case (int)Polarity.Ice:      View.ElementType.spriteName = "departicon_ice";         break;             /// 冰
            case (int)Polarity.Fire:     View.ElementType.spriteName = "departicon_fire";        break;             /// 火
            case (int)Polarity.Thunder:  View.ElementType.spriteName = "departicon_thunder";     break;             /// 雷
        }
        for (int i = 0; i < View.StarNumGrid.GetChildList().Count; i++)                                             /// 清除星级Grid 子项       
        {
            Destroy(View.StarNumGrid.GetChild(i).gameObject);
        }
        for (int i = 0; i < TheHeroD.HeroStar; i++)                                                                 /// 星级Grid 子项添加       
        {
            UIAtlas                 TheAtlas = Util.LoadAtlas(AtlasConfig.FreeWay) ;
            UISprite                Star = NGUITools.AddSprite(View.StarNumGrid.gameObject, TheAtlas, "xing");
            Star.height             = 20;
            Star.width              = 20;
            Star.depth              = 30;
        }
        IconBtnStateInit();
    }
    private void                    IconBtnStateInit()                                                              // 图像上阵状态 初始化       
    {
        foreach(var Item in InBattlePos.BattlePosSetDic)                                                            /// 当前英雄状态(是否上阵状态)  
        {
            if(Item.Value.BP_HeroData != null)
            {
                if (Item.Value.BP_HeroData.ID == View.TheHeroID)
                {
                    View.IconMask.enabled = true;
                    View.SelectedLabel.enabled = true;
                    isBtnOn = true;
                }
            }
        }
    }
    private void                    HeroIconClickHandler()                                                          // 英雄图标 点击 (上下阵 英雄)
    {
        int onBtnCount              = InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).Count;                    /// 上阵英雄数量
        InBattlePos.TempHero_D      = InPlayer.GetHeroByID(View.TheHeroID);                                         /// 存储当前选中英雄数据

        if ( isBtnOn == false )                                                                                     /// 选中激活 上阵当前英雄
        {
            if ( onBtnCount < 5 )                                                                                   /// 阵容少于五 上阵 
            {
                View.IconMask.enabled                   = true;
                View.SelectedLabel.enabled              = true;
                isBtnOn                                 = true;
                dispatcher.Dispatch(BattlePosEvent.HeroIconClickUpHero_Evnet);                                      /// 点击头像 上阵英雄 <配置并展示>
            }
            else
            {
            PanelManager.sInstance.ShowNoticePanel("上阵人数已经满 五人");
            }
        }
        else                                                                                                        /// 取消激活 下阵当前英雄
        {
            View.IconMask.enabled                       = false;
            View.SelectedLabel.enabled                  = false;
            isBtnOn                                     = false;
            dispatcher.Dispatch(BattlePosEvent.HeroIconClickDownHero_Evnet);                                        /// 点击头像 上阵英雄 <配置并展示>
        }

    }
}