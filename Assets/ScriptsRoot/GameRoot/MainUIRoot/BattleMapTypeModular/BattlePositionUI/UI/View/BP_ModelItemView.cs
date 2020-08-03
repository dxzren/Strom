using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class BP_ModelItemView : EventView
{
    [Inject]
    public IPersetLineupData    InBattleM                   { set; get; }
    [Inject]
    public IPlayer              InPalyer                    { set; get; }

    public string               ModelClick_Event            = "ModelClick_Event";

    public UICamera             UICamera;
    public IHeroData            TheHeroD;                                                       /// 当前英雄数据
    public PosNumType           PosNum;                                                         /// 当前阵容位置

    private GameObject          SourceParent;                                                   /// 最后的父级对象   <Drag_Drop>
    private string              SourceParent_tag;                                               /// 父级坐标         <Drag_Drop>
    public void                 ViewInit()  
    {
        GetLineupPos();                                                                         /// 获取 前阵容位置
        GetHeroData();                                                                          /// 获取 位置英雄数据
        UIEventListener.Get(this.gameObject).onClick        = ModelClick;                       /// 模型点击
    }
    private void                GetLineupPos()                                                  // 当前对象父级别 获取阵容位置(LineupPos) 
    {
        int                     PosNum = this.transform.parent.GetComponent<LineupPosIndex>().LineupPos_Index;
    }
    private void                GetHeroData()                                                   // 获取当前阵容类型<当前位置>英雄数据     
    {
        foreach(var Item in InPalyer.BattleTypeToLineUp(InBattleM.BattleType))
        {
            if (Item.Key == PosNum)     TheHeroD            = Item.Value;
        }
    }
    private void                ModelClick(GameObject obj)                                      // 模型点击                             
    {
        dispatcher.Dispatch     (ModelClick_Event);
    }

    public void                 OnDragStart()                                                   // 拖动初始化    
    {
        GetLineupPos();                                                                         /// 获取 前阵容位置
        GetHeroData();                                                                          /// 获取 当前阵容位置
        if (this.transform.parent.childCount == 2)                                              /// 当前位置已加载英雄模型
        {
            SourceParent        = this.transform.parent.gameObject;                             /// 原始父级对象
            SourceParent_tag    = SourceParent.tag;                                             /// 原始坐标标记
            Debug.Log("OnDrageStart__" + "tag: " + SourceParent_tag);
        }
    }

    public void                 OnDrag(Vector2 delta)                                           // 拖动过程     
    {
        GetLineupPos();
        this.transform.localPosition                = new Vector3(this.transform.localPosition.x,                   /// 设置坐标
                                                              this.transform.localPosition.y, -1000);
        if ( this.transform.parent.childCount == 2)
        {
            this.transform.parent.GetChild(1).OverlayPosition(this.transform);
        }
        GameObject              surface             = UICamera.hoveredObject;                       
    }

    public void                 OnDragEnd()                                                     // 拖动结束     
    {
        Debug.Log("OnDrageEnd");
        GetLineupPos();                                                                                             /// 获取 前阵容位置
        GetHeroData();                                                                                              /// 获取 当前阵容位置
        GameObject              surface             = UICamera.hoveredObject;
        if (    surface != null     )
            surface.transform.GetComponent<BP_ModelItemView>().GetLineupPos();

        if (this.transform.parent.childCount == 2 )                                                                 /// 当前子集(2) 已有<.Hero> 
        {
            if ( surface != null && surface.tag == "Item")
            {
                if      ( surface.transform.parent.tag == SourceParent_tag && surface.transform.parent.childCount == 2)         /// 目标 子集(2) 已有<.Hero> 
                {
                    InPalyer.ExChangeBattlePosition(PosNum, surface.transform.GetComponent                                     /// 战斗阵容英雄互换 <.英雄数据互换.>
                                                    <BP_ModelItemView>().PosNum, InBattleM.BattleType);
                    this.transform.SetParent(surface.transform.parent.transform);                                               /// 当前父级设置为     目标父级
                    SourceParent.transform.GetChild(0).SetParent(this.transform.parent.transform);                              /// 原始父级子集[0]    设置为当前父级

                    surface.transform.parent.GetChild(1).SetParent(SourceParent.transform);                                     /// 目标对象子集<.英雄模型.> 设置原始父级对象_为父级
                    surface.transform.SetParent(SourceParent.transform);                                                        /// 目标对象设置            设置原始父级对象_为父级

                    surface.gameObject.transform.SetAsFirstSibling();                                                           /// TF to List(0) 
                    surface.transform.localPosition                 = Vector3.zero;                                             /// 设置 目标对象 Pos
                    SourceParent.transform.GetChild(1).localPosition= new Vector3(0, 0, -80);                                   /// 设置 原始对象 Pos

                    GameObject  ExchangeObj                         = InBattleM.BattlePosSetDic[surface.transform.GetComponent  /// 获取目标模型对象
                                                                      <BP_ModelItemView>().PosNum].HeroModel;                    
                    IHeroData   ExchangeHeroD                       = InBattleM.BattlePosSetDic[surface.transform.GetComponent  /// 获取目标英雄数据
                                                                       <BP_ModelItemView>().PosNum].BP_HeroData;
                    InBattleM.BattlePosSetDic[PosNum].HeroModel    = ExchangeObj;                                              /// 设置 当前模型对象
                    InBattleM.BattlePosSetDic[PosNum].BP_HeroData  = ExchangeHeroD;                                            /// 设置 当前英雄数据

                    InBattleM.BattlePosSetDic[surface.transform.GetComponent<BP_ModelItemView>().PosNum].HeroModel             /// 设置 目标英雄数据
                                                                    = InBattleM.BattlePosSetDic[PosNum].HeroModel;
                    InBattleM.BattlePosSetDic[surface.transform.GetComponent<BP_ModelItemView>().PosNum].BP_HeroData           /// 设置 目标英雄数据
                                                                    = InBattleM.BattlePosSetDic[PosNum].BP_HeroData;
                }
                else if ( surface.transform.parent.tag == SourceParent_tag && surface.transform.parent.childCount == 1)         /// 目标 子集(1) 没有<.Hero>
                {
                    InPalyer.UpBattle(surface.transform.GetComponent<BP_ModelItemView>().PosNum, TheHeroD, InBattleM.BattleType);

                    InBattleM.BattlePosSetDic[surface.transform.GetComponent<BP_ModelItemView>().PosNum].HeroModel             /// 设置 目标英雄数据
                                                                    = InBattleM.BattlePosSetDic[PosNum].HeroModel;
                    InBattleM.BattlePosSetDic[surface.transform.GetComponent<BP_ModelItemView>().PosNum].BP_HeroData           /// 设置 目标英雄数据
                                                                    = InBattleM.BattlePosSetDic[PosNum].BP_HeroData;
                    InBattleM.BattlePosSetDic[PosNum].HeroModel    = null;                                                     /// 设置 当前模型对象
                    InBattleM.BattlePosSetDic[PosNum].BP_HeroData  = null;                                                     /// 设置 当前英雄数据

                    this.transform.SetParent(surface.transform.parent.transform);                                               /// 设置当前父级
                    SourceParent.transform.GetChild(0).SetParent(surface.transform.parent.transform);                           /// 设目标子集父级
                    surface.transform.SetParent(SourceParent.transform);                                                        /// 设置目标父级
                    surface.transform.localPosition                 = Vector3.zero;                                             /// 设置目标POS
                }
                else if ( surface.transform.parent.tag != SourceParent_tag)                                                     /// 放置目标类型限制,提示信息
                {               PanelManager.sInstance.ShowNoticePanel(Language.GetValue("FeedbackMsg034"));         }            
            }
            else
            {   this.gameObject.transform.SetAsFirstSibling(); }                                                                /// Obj ToList first
            this.gameObject.transform.SetAsFirstSibling();                                                                      /// 对象目标排列最前
            this.transform.parent.GetChild(1).localPosition = Vector3.zero;                                                     /// 初始化模型坐标
        }
        Debug.Log("OnDrageEnd_________End!");
        this.transform.localPosition                        = new Vector3(0, -30, 0);
        if (this.transform.parent.childCount == 2 )                                                                             /// 初始化英雄模型 Pos坐标
        {
            this.transform.parent.GetChild(1).localPosition = new Vector3(0, 0, -80);                           
        }
        //dispatcher.Dispatch(BattlePosEvent.UpdateFightForce_Event);                                                           /// 更新战斗力
        GetLineupPos();                                                                                                         /// 获取 前阵容位置
    }
}