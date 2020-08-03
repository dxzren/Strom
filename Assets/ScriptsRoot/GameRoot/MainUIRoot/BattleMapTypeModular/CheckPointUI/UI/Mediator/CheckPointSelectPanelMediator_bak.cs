using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using LinqTools;
/// <summary> 关卡选择面板 </summary>

public class CheckPointSelectPanelMediator_bak : EventMediator
{
    [Inject]
    public CheckPointSelectPanelView_bak  View              { set; get; }
    [Inject]
    public IGameData            InGameData                  { set; get;}
    [Inject]
    public IPlayer              InPlayer                    { set; get; }
    [Inject]
    public ICheckPointSys       InCheckP_Sys                { set; get; }

    private Object              CheckPointItem              = new Object();                         // 
    private GameObject          FormItem                    = new GameObject();                     // 
    public override void        OnRegister()
    {
        StarBoxInit();                                                                              /// 星级宝箱初始化
        CheckPointItem         = Resources.Load(UIPanelConfig.CheckPointItem);                      /// 关卡标签  
        View.Init();                                                                                /// 视图初始化
        ChapterItemInit();                                                                          /// 章节初始化
        BtnInit();                                                                                  /// 按钮初始化

        View.dispatcher.AddListener(View.AwardBoxClick_Event,           AwardBoxClickHandler);      /// 奖励宝箱 点击处理
        View.dispatcher.AddListener(View.TakeOutClick_Event,            TakeOutClickHandler);       /// 页签展开 点击处理
        View.dispatcher.AddListener(View.TakeInClick_Event,             TakeInClickHandler);        /// 页签收起 点击处理
        View.dispatcher.AddListener(View.ExitClick_Event,               ExitClickHandler);          /// 退出界面 点击处理
        View.dispatcher.AddListener(View.LeftClick_Event,               LeftClickHandler);          /// 向左翻页 点击处理
        View.dispatcher.AddListener(View.RightClick_Event,              RightClickHandler);         /// 向右翻页 点击处理
        View.dispatcher.AddListener(View.NormalClick_Event,             NormalClickHandler);        /// 普通关卡 点击处理
        View.dispatcher.AddListener(View.EliteClick_Event,              EliteClickHandler);         /// 精英关卡 点击处理
        View.dispatcher.AddListener(View.SelectHeroClick_Event,         SelectHeroClickHandler);    /// 英雄选择 点击处理

        dispatcher.AddListener(EventSignal.UpdateInfo_Event,            ChapterItemInit);           /// 数据更新: 章节数据初始化
        dispatcher.AddListener(GlobalEvent.UIAnimationEvent,            UIAnimaStart);              /// UI动画监听

        UIAnimaStart();                                                                             /// 播放UI动画

        PanelManager.sInstance.LoadOverHandler_10Planel(this.gameObject.name);                      /// 加载完数据展示当前界面
    }
    public override void        OnRemove()
    {
        View.dispatcher.RemoveListener(View.AwardBoxClick_Event,        AwardBoxClickHandler);      /// 奖励宝箱 点击处理
        View.dispatcher.RemoveListener(View.TakeOutClick_Event,         TakeOutClickHandler);       /// 页签展开 点击处理
        View.dispatcher.RemoveListener(View.TakeInClick_Event,          TakeInClickHandler);        /// 页签收起 点击处理
        View.dispatcher.RemoveListener(View.ExitClick_Event,            ExitClickHandler);          /// 退出界面 点击处理
        View.dispatcher.RemoveListener(View.LeftClick_Event,            LeftClickHandler);          /// 向左翻页 点击处理
        View.dispatcher.RemoveListener(View.RightClick_Event,           RightClickHandler);         /// 向右翻页 点击处理
        View.dispatcher.RemoveListener(View.NormalClick_Event,          NormalClickHandler);        /// 普通关卡 点击处理
        View.dispatcher.RemoveListener(View.EliteClick_Event,           EliteClickHandler);         /// 精英关卡 点击处理
        View.dispatcher.RemoveListener(View.SelectHeroClick_Event,      SelectHeroClickHandler);    /// 英雄选择 点击处理

        dispatcher.AddListener(EventSignal.UpdateInfo_Event,            ChapterItemInit);           /// 数据更新: 章节数据初始化
        dispatcher.AddListener(GlobalEvent.UIAnimationEvent,            UIAnimaStart);              /// UI动画监听

        StopAllCoroutines();                                                                        /// 停止所有的协同程序
    }
    private void                StarBoxInit()                                                       // 章节星级宝箱初始化   
    {
        #region |----------------------------|  章节星级显示  |-----------------------------
        int                     normalStartCount                      = 0;                          /// 普通星级累计
        int                     eliteStartCount                       = 0;                          /// 精英星级累计
        foreach ( int key in InPlayer.GetNormalCheckPointStars.Keys)
        {
            if ( Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(key).CheckPointType == 2 )
            {    continue;  }
            normalStartCount    += InPlayer.GetNormalCheckPointStars[key];
        }
        foreach ( int key in InPlayer.GetEliteCheckPointStars.Keys)
        {
            if (Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(key).CheckPointType == 2)
                continue;
            eliteStartCount     += InPlayer.GetEliteCheckPointStars[key];
        }
        if ( InCheckP_Sys.CurrentCP_Type == ChapterType.Normal )
        {    View.StarCount.text = normalStartCount.ToString() + "/" + CustomJsonUtil.GetValue("NormalChapterStarNumber"); }
        else
        {    View.StarCount.text = eliteStartCount.ToString() + "/" + CustomJsonUtil.GetValue("ElitistChapterStarNumber"); }
        #endregion

        View.AwardBoxBtn.GetComponent<UISprite>().spriteName        = "icon_box_84";
    }
    private void                ChapterItemInit()                                                   // 章节初始化          
    {
        View.CheckP_Num.text    = "第" + ToChineseNumber(Configs_Chapter.sInstance.
                                  GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterID) + "章";
        View.CheckP_Name.text   = Language.GetValue(Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterName);
        if ( View.BackGround.transform.childCount == 0 )
        {
            GameObject TheCP_Item                           = Instantiate(CheckPointItem) as GameObject;
            TheCP_Item.transform.SetParent(View.BackGround.transform);
            TheCP_Item.transform.localScale                 = Vector3.one;
            TheCP_Item.transform.localPosition              = Vector3.zero;
            View.CurrentMapItem                             = TheCP_Item;           
        }
        BtnInit();
    }
    private void                BtnInit()                                                           // 按钮初始化          
    {
        if (InCheckP_Sys.CurrentCP_Type == ChapterType.Normal )                                  /// 关卡类型按钮设置 ( 普通or精英 )
        {
            View.NormalBtn.transform.GetChild(0).gameObject.SetActive(true);
            View.EliteBtn.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            View.NormalBtn.transform.GetChild(0).gameObject.SetActive(false);
            View.EliteBtn.transform.GetChild(0).gameObject.SetActive(true);
        }
        if ( InCheckP_Sys.chapterID == 1 )                                                          /// 左翻按钮 初始化
        {    View.Left.SetActive(false);    }
        else
        {    View.Left.SetActive(true);     }

        if ( InCheckP_Sys.chapterID == 13 )                                                         /// 右翻按钮 初始化
        { View.Right.SetActive(false);    return; }

        else if ((InCheckP_Sys.chapterID + 1) > Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(
                 InPlayer.EliteMaxCheckPointHistory.ID).GenusChapter && InCheckP_Sys.CurrentCP_Type == ChapterType.Elite)
        {    View.Right.SetActive(false);           } 
        else
        {    View.Right.SetActive(true);            }
    }

    private void                AwardBoxClickHandler()                                              // 奖励宝箱 点击       
    {   PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.GetBoxPanel);   }
    private void                TakeOutClickHandler()                                               // 页签展开 点击处理    
    {
        if ( View.showSysNum > 0 )
        {
            Vector3 ThePos      = new Vector3(-306f + View.showSysNum * 103f, -18f, 1112f);
            View.TakeInBtn.transform.GetComponent<TweenPosition>().to   = ThePos;                   /// 设置 页签收起Btn位置
        }
        View.TakeInBtn.gameObject.SetActive(true);                                                  /// 页签收起Btn 激活
        View.TakeOutBtn.gameObject.SetActive(false);                                                /// 页签打开Btn 关闭
        View.HeroBtn.SetActive(true);                                                               /// 英雄Btn激活

        for ( int i = 0; i < View.TweenPosList.Count; i++ )                                         /// 设置激活
        {   View.TweenPosList[i].gameObject.SetActive(true);    }
        for ( int i = 0; i < View.TweenPosList.Count; i++)                                          /// 向前平移
        {   View.TweenPosList[i].PlayForward();                 }
        for ( int i = 0; i < View.TweenAlphaList.Count; i++ )                                       /// 透明通道设置
        {   View.TweenAlphaList[i].PlayForward();               }

        View.TakeInBtn.transform.GetComponent<TweenPosition>().PlayForward();
        View.TakeInBtn.transform.GetComponent<TweenAlpha>().PlayForward();
        View.Mask.transform.GetComponent<TweenAlpha>().PlayForward();
    }
    private void                TakeInClickHandler()                                                // 页签收起 点击处理    
    {
        View.TakeOutBtn.gameObject.SetActive(true);
        for ( int i = 0; i < View.TweenPosList.Count; i++ )                                         /// 反向位置
        {   View.TweenPosList[i].PlayReverse();                 }
        for ( int i = 0; i < View.TweenAlphaList.Count; i++ )                                       /// 反向位置
        {   View.TweenAlphaList[i].PlayReverse();               }
        View.TakeInBtn.transform.GetComponent<TweenPosition>().PlayReverse();
        View.TakeInBtn.transform.GetComponent<TweenAlpha>().PlayReverse();
        View.Mask.transform.GetComponent<TweenAlpha>().PlayReverse();
    }

    private void                ExitClickHandler()                                                  // 退出点击          
    {
                                                                                                    /// 按钮飞入飞出动画
        UIAnimation.Instance().LineMove(View.Top,       new float[] { UIAnimationConfig.Top_toX,    UIAnimationConfig.Top_toY,    UIAnimationConfig.Top_toZ },
                                                        new float[] { UIAnimationConfig.Top_fromX,  UIAnimationConfig.Top_fromY,  UIAnimationConfig.Top_fromZ });
        UIAnimation.Instance().LineMove(View.ExitBtn,   new float[] { UIAnimationConfig.Back_toX,   UIAnimationConfig.Back_toY,   UIAnimationConfig.Back_toZ },
                                                        new float[] { UIAnimationConfig.Back_fromX, UIAnimationConfig.Back_fromY, UIAnimationConfig.Back_fromZ });

        dispatcher.Dispatch(CheckPointEvent.REQ_Move_Event);                                        /// 位移事件
        PanelManager.sInstance.HidePanel ( SceneType.Main, UIPanelConfig.CheckPointSelectPanel );   /// 隐藏当前面板(关卡选择面板)
    }
    private void                LeftClickHandler()                                                  // 左翻点击          
    {
        InCheckP_Sys.chapterID--;                                                                                                       /// 当前章节-1

        View.CheckP_Num.text    = "第" + ToChineseNumber( Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterID) + "章";
        View.CheckP_Name.text   = Language.GetValue(Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterName);
        FormItem                                            = View.CurrentMapItem;                                                      /// 设置关卡视图
        GameObject              TheMapItem                  = Instantiate(CheckPointItem) as GameObject;                                /// 实例化
        TheMapItem.transform.SetParent                      (View.BackGround.transform);                                                /// 父级对象
        TheMapItem.transform.localScale                     = Vector3.one;                                                              /// 缩放比例
        TheMapItem.transform.localPosition                  = new Vector3(1140, 0, 0);                                                  /// 位移坐标
        View.CurrentMapItem     = TheMapItem;                                                                                           /// 当前关卡视图

        BtnInit();                                                                                                                      /// 初始化按钮
        View.Left.GetComponent<BoxCollider>().enabled       = false;                                                                    /// Left按钮激活
        View.Right.GetComponent<BoxCollider>().enabled      = false;                                                                    /// Right按钮激活

        InvokeRepeating         ("TweenLeft", 0, .01f);                                                                                 /// TweenLeft()
    }
    private void                RightClickHandler()                                                 // 右翻点击          
    {
        int NormalCP_UnLockLv   = Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID + 1).UnlockLevel[0];       /// 普通章节 解锁等级 
        int EliteCP_UnLockLv    = Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID + 1).UnlockLevel[1];       /// 精英章节 解锁等级

        if ( InCheckP_Sys.CurrentCP_Type == ChapterType.Normal && NormalCP_UnLockLv > InPlayer.PlayerLevel )                         /// 普通模式不满足解锁等级
        {    PanelManager.sInstance.ShowNoticePanel("主角等级" + NormalCP_UnLockLv + "级解锁" );               return;             } 
        if ( InCheckP_Sys.CurrentCP_Type == ChapterType.Elite && EliteCP_UnLockLv > InPlayer.PlayerLevel )                           /// 精英关卡不满足解锁等级
        {    PanelManager.sInstance.ShowNoticePanel("主角等级" + EliteCP_UnLockLv + "级解锁");                 return;             }
        if ( InCheckP_Sys.CurrentCP_Type == ChapterType.Normal && Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.  /// 普通最大关卡 大于 通关章节
                                 chapterID + 1).BigID[0] > InPlayer.NormalMaxCheckPointHistory.ID)

        InCheckP_Sys.chapterID++;                                                                                                       /// 当前章节+1
        View.CheckP_Num.text    = "第" + ToChineseNumber(Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterID) + "章";   /// 章节数
        View.CheckP_Name.text   = Language.GetValue(Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterName);             /// 章节名
        FormItem                                            = View.CurrentMapItem;                                                      /// 设置关卡视图
        GameObject              TheMapItem                  = Instantiate(CheckPointItem) as GameObject;                                /// 实例化
        TheMapItem.transform.SetParent                      (View.BackGround.transform);                                                /// 父级对象
        TheMapItem.transform.localScale                     = Vector3.one;                                                              /// 缩放比例
        TheMapItem.transform.localPosition                  = new Vector3(1140, 0, 0);                                                  /// 位移坐标
        View.CurrentMapItem     = TheMapItem;                                                                                           /// 当前关卡视图

        BtnInit();                                                                                                                      /// 初始化按钮
        View.Left.GetComponent<BoxCollider>().enabled       = false;                                                                    /// Left按钮激活
        View.Right.GetComponent<BoxCollider>().enabled      = false;                                                                    /// Right按钮激活

        InvokeRepeating("TweenRight", 0, .01f);                                                                                         /// TweenRight()
    }
    private void                TweenLeft()                                                         // 左翻             
    {
        FormItem.transform.localPosition                    = new Vector3(View.CurrentMapItem.transform.localPosition.x + 10,
                                                              FormItem.transform.localPosition.y);
        View.CurrentMapItem.transform.localPosition         = new Vector3(View.CurrentMapItem.transform.localPosition.x+10,
                                                              View.CurrentMapItem.transform.localPosition.y);
        if (View.CurrentMapItem.transform.localPosition.x >= 0 )
        {
            View.Left.GetComponent<BoxCollider>().enabled   = true;
            View.Right.GetComponent<BoxCollider>().enabled  = true;
            CancelInvoke        ("TweenLeft");
            DestroyImmediate    (FormItem);
        }
    }
    private void                TweenRight()                                                        // 右翻             
    {
        FormItem.transform.localPosition                    = new Vector3(View.CurrentMapItem.transform.localPosition.x + 10,
                                                              FormItem.transform.localPosition.y);
        View.CurrentMapItem.transform.localPosition         = new Vector3(View.CurrentMapItem.transform.localPosition.x+10,
                                                              View.CurrentMapItem.transform.localPosition.y);
        if (View.CurrentMapItem.transform.localPosition.x >= 0 )
        {
            View.Left.GetComponent<BoxCollider>().enabled   = true;
            View.Right.GetComponent<BoxCollider>().enabled  = true;
            CancelInvoke        ("TweenRight");
            DestroyImmediate    (FormItem);
        }

    }
    private void                NormalClickHandler()                                                // 普通关卡章节点击  
    {
        int CurrentCP_LimitLv           = Configs_Chapter.sInstance.GetChapterDataByChapterID(                      /// 通过最大关卡所属章节解锁等级
                                          Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(
                                          InPlayer.NormalMaxCheckPointHistory.ID).GenusChapter).UnlockLevel[0];

        InCheckP_Sys.CurrentCP_Type     = ChapterType.Normal;                                                    /// 设置当前关卡类型
        if ( CurrentCP_LimitLv <= InPlayer.PlayerLevel )                                                            /// 获取当前章节  
        {
            InCheckP_Sys.chapterID      = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(
                                          InPlayer.NormalMaxCheckPointHistory.ID).GenusChapter;
        }
        else
        {
            InCheckP_Sys.chapterID = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(
                                 InPlayer.NormalMaxCheckPointHistory.ID).GenusChapter - 1;
        }
        BtnInit();                                                                                                  /// 初始化按钮
        CheckPointItemInit();                                                                                       /// 初始化关卡项
    }
    private void                EliteClickHandler()                                                 // 精英关卡章节点击  
    {
        if ( InPlayer.PlayerLevel < Configs_Chapter.sInstance.GetChapterDataByChapterID(1).UnlockLevel[1])          /// 精英关卡开启最低等级
        {
            PanelManager.sInstance.ShowNoticePanel("精英难度" + Configs_Chapter.sInstance.
                                GetChapterDataByChapterID(1).UnlockLevel[1] + "级开启");
            return;
        }
        InCheckP_Sys.CurrentCP_Type = ChapterType.Elite;
        InCheckP_Sys.chapterID = Configs_Chapter.sInstance.GetChapterDataByChapterID(Configs_CheckPoint.
                                sInstance.GetCheckPointDataByCheckPointID(InPlayer.EliteMaxCheckPointHistory.ID).GenusChapter).UnlockLevel[1]
                                <= InPlayer.PlayerLevel ?
                                Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(InPlayer.EliteMaxCheckPointHistory.ID).GenusChapter :
                                Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(InPlayer.EliteMaxCheckPointHistory.ID).GenusChapter - 1;
        if ( InCheckP_Sys.CurrentCP_Type == ChapterType.Elite && Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).BigID[1] >
                                InPlayer.NormalMaxCheckPointHistory.ID)
        { InCheckP_Sys.chapterID--; }
        if ( InCheckP_Sys.chapterID == 0)
        {    InCheckP_Sys.chapterID++; }
        CheckPointItemInit();
        BtnInit();
    }
    private void                SelectHeroClickHandler()                                            // 英雄选择点击      
    {
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.HeroListShowPanel);              /// 展示英雄显示面板
    }
    private void                UIAnimaStart()                                                      // UI动画设置和启动  
    {
        View.Top.transform.localPosition        = new Vector3(UIAnimationConfig.Top_fromX, UIAnimationConfig.Top_fromY, UIAnimationConfig.Top_fromZ);
        View.ExitBtn.transform.localPosition    = new Vector3(UIAnimationConfig.Back_fromX,UIAnimationConfig.Back_fromY,UIAnimationConfig.Back_fromZ);
        Invoke("PlayUIAnimation", UIAnimationConfig.BlackToNomarl_duration);
    }
    private void                PlayUIAnimation()                                                   // 播放 动画         
    {
        FlyInEffect.BackButtonFlyInEffect(View.ExitBtn);
        FlyInEffect.TopButtonFlyInEffect(View.Top);
    }
    private void                CheckPointItemInit()                                                // 关卡设置初始化    
    {
        Destroy(View.CurrentMapItem);
        GameObject              TheMapItem                  = Instantiate(CheckPointItem) as GameObject;
        TheMapItem.transform.SetParent(View.BackGround.transform);
        TheMapItem.transform.localPosition                  = Vector3.zero;
        TheMapItem.transform.localScale                     = Vector3.one;
        View.CheckP_Num.text    = "第" + ToChineseNumber(InCheckP_Sys.chapterID) + "章";
        View.CheckP_Name.text   = Language.GetValue(Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterName);
        View.CurrentMapItem     = TheMapItem;
        StarBoxInit();
    }
    private string              ToChineseNumber(int chapterID)                                      // 转化:中文数字     
    {
        switch (chapterID)
        {
            case 1: return "一";
            case 2: return "二";
            case 3: return "三";
            case 4: return "四";
            case 5: return "五";
            case 6: return "六";
            case 7: return "七";
            case 8: return "八";
            case 9: return "九";
            case 10: return "十";
            case 11: return "十一";
            case 12: return "十二";
            case 13: return "十三";
            case 14: return "十四";
            case 15: return "十五";
            case 16: return "十六";
            case 17: return "十七";
            case 18: return "十八";
            case 19: return "十九";
            case 20: return "二十";
            default: return "Error: " + chapterID.ToString();
        }
    }
}