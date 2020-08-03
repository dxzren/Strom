using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
/// <summary> 战斗阵容 位置预设 </summary>

public class BattlePositionPanelView : EventView
{
    public string               ExitClick_Event         = "ExitClick_Event ";                   /// 退出界面
    public string               BattleSatrtClick_Event  = "attleSatrtClick_Event";              /// 战斗启动

    public UISprite             ExitBtn,BattleStart;                                            /// 退出,战斗启动 [Btn]
    public UISprite             AllHeroBtn, FrontHero, MiddleHeroBtn, BackHeroBtn;              /// 切换英雄类型  [Btn]

    public UILabel              AllBtn_On, Front_On, Middle_On, Back_On;                        /// 英雄类型切换 按钮Text
    public UIGrid               HeroIconGrid;                                                   

    public UIScrollView         IconItemScrollView;                                             /// 图标遮挡拖动滑块
    public GameObject           Top, Middle, Bottom ;                                           /// 界面 模块分类

    public List<GameObject>     HeroModelList                   = new List<GameObject>();       /// 集合列表
    public List<Transform>      PartPosList                     = new List<Transform>();        /// 特效列表

    public void ViewInit()
    {
        UIEventListener.Get(ExitBtn.gameObject).onClick         = ExitClick;                    /// 退出      [Btn]
        UIEventListener.Get(BattleStart.gameObject).onClick     = BattleStartClick;             /// 战斗启动  [Btn]
    }

    private void                ExitClick( GameObject obj)                                      // 退出界面 
    {
        dispatcher.Dispatch(ExitClick_Event);
    }
    private void                BattleStartClick( GameObject obj)                               // 战斗启动 
    {
        dispatcher.Dispatch(BattleSatrtClick_Event);
    }
}