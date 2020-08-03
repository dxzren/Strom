using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class CheckPointSelectPanelMediator : EventMediator
{
    [Inject]
    public CheckPointSelectPanelView    View                   { set; get; }
    [Inject]
    public ICheckPointSys               InCheckP_Sys            { set; get; }
    [Inject]
    public IPlayer                      InPlayer                { set; get; }


    public override void                OnRegister()
    {
 
        View.ViewInit();    
        ChapterItemInit();                                                                                              /// 章节初始化

        View.dispatcher.AddListener(View.ExitClick_Event,                   ExitClickHandler);                          /// 退出当前面板
        View.dispatcher.AddListener(View.NormalClick_Event,                 NormalClickHandler);                        /// 普通章节    点击
        View.dispatcher.AddListener(View.EliteClick_Event,                  EliteClickHandler);                         /// 精英章节    点击

    }
    public override void                OnRemove()
    {
        View.dispatcher.RemoveListener(View.ExitClick_Event,                ExitClickHandler);                          /// 退出当前界面
        View.dispatcher.RemoveListener(View.NormalClick_Event,              NormalClickHandler);                        ///
        View.dispatcher.RemoveListener(View.EliteClick_Event,               EliteClickHandler);                         ///
    }

    private void                        ChapterItemInit()                                                               // 章节初始化        
    {
        View.ChapterNum.text            = "第" + ToChineseNumber(Configs_Chapter.sInstance.                             /// 章节号码
                                          GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterID) + "章";
        View.ChapterName.text           = Language.GetValue(Configs_Chapter.sInstance.GetChapterDataByChapterID(        /// 章节名称
                                          InCheckP_Sys.chapterID).ChapterName);                                         
        View.ChapterMap.mainTexture     = (Texture)Util.Load("Texture/Level/" + Configs_Chapter.sInstance.              /// 加载章节地图
                                          GetChapterDataByChapterID(InCheckP_Sys.chapterID).ChapterScene1);

        Chapter_CP_ItemInit();                                                                                          /// 章节关卡设置初始化
    }
    private void                        Chapter_CP_ItemInit()                                                           // 章节关卡设置初始化 
    {
        if      ( InCheckP_Sys.CurrentCP_Type == ChapterType.Normal )                                                   /// 章节所有普通关卡设置 
        {
            int Nor_Min = Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).LittleID[0];      /// 当前章节最小关卡ID
            int Nor_Max = Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).BigID[0];         /// 当前章节最大关卡ID
            for ( int i = Nor_Min; i <= Nor_Max; i++)
            {
                bool            IsBigCP         = false;                                                                        /// 是否大关卡
                float           Pos_x           = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BuildingCoordinateX;  /// 关卡配置坐标
                float           Pos_y           = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BuildingCoordinateY;  /// 关卡配置坐标

                if ( Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID( i ).CheckPointType == 1 )
                {    IsBigCP = true;    }

                if      ( i < InPlayer.NormalMaxCheckPointHistory.ID )                                                          /// 已通 关卡显示设置
                {
                    int         StarNum         = 0;                                                                            /// 关卡星级                                  
                    foreach (var Item in InPlayer.GetNormalCheckPointStars)
                    {
                        if (Item.Key == i )
                        { StarNum = Item.Value; }
                    }
                    //Debug.Log   ("CP_ID:" + i +"/"+" StarNum: " + StarNum.ToString());
                    Object      TempObj         = Resources.Load(UIPanelConfig.CheckPointItem) as Object;                       /// 加载创建对象
                    GameObject  TheCP           = Instantiate(TempObj) as GameObject;                                           /// 实例化对象

                    TheCP.name                                                          = "CP_" + i;                            /// 面板名称
                    TheCP.transform.parent                                              = View.ChapterMap.transform;            /// 面板父级别 指定
                    TheCP.transform.localScale                                          = Vector3.one;                          /// 面板缩放 设置
                    TheCP.transform.transform.localPosition                             = new Vector3(Pos_x,Pos_y,0);           /// 面板位移 坐标设置
                    TheCP.GetComponent<UISprite>().spriteName                           = "xuedi_ludian3";                      /// 面板图集 名称
                    TheCP.GetComponent<UISprite>().MakePixelPerfect();                                                          /// 图标缩放-图像大小(最佳)

                    TheCP.GetComponent<CheckPointItemView>().CP_ID                      = i;                                    /// 关卡ID设置
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.enabled        = false;                                /// 背框显示关闭
                    TheCP.GetComponent<CheckPointItemView>().CP_Icon.enabled            = false;                                /// 图标显示关闭
                    TheCP.GetComponent<CheckPointItemView>().StarNum.gameObject.SetActive(false);                               /// 星级显示(关闭)
                    if ( IsBigCP )                                                                                              /// <| 大关卡设置 |>
                    {
                        TheCP.GetComponent<UISprite>().spriteName                       = "xuedi_ludianda";                     /// 面板图集 名称
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.enabled    = true;                                 /// 背框显示 显示
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.spriteName = "ludiankuang-touxiang";               /// 背框图集 名称
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.MakePixelPerfect();                                /// 背框 缩放-图像大小(最佳)
                        TheCP.GetComponent<CheckPointItemView>().CP_Icon.enabled        = true;                                 /// 图标显示 
                        int     HeroID          = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BOSSShow;     /// Boss图标 设置
                        TheCP.GetComponent<CheckPointItemView>().CP_Icon.spriteName     = Configs_Hero.sInstance.GetHeroDataByHeroID(HeroID).head84;
                        TheCP.GetComponent<CheckPointItemView>().StarNum.gameObject.SetActive(true);                            /// 星级显示激活
                        switch(StarNum)                                                                                         /// 星级显示    
                        {
                            case 1:
                                {
                                    TheCP.GetComponent<CheckPointItemView>().Star_1.enabled = true;
                                    TheCP.GetComponent<CheckPointItemView>().Star_2.enabled = false;
                                    TheCP.GetComponent<CheckPointItemView>().Star_3.enabled = false;
                                    break;
                                }
                            case 2:
                                {
                                    TheCP.GetComponent<CheckPointItemView>().Star_1.enabled = true;
                                    TheCP.GetComponent<CheckPointItemView>().Star_2.enabled = true;
                                    TheCP.GetComponent<CheckPointItemView>().Star_3.enabled = false;
                                    break;
                                }
                            case 3:
                                {
                                    TheCP.GetComponent<CheckPointItemView>().Star_1.enabled = true;
                                    TheCP.GetComponent<CheckPointItemView>().Star_2.enabled = true;
                                    TheCP.GetComponent<CheckPointItemView>().Star_3.enabled = true;
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    TheCP.SetActive                                                     (true);                                 /// 关卡显示
                    TempObj                                                             = null;
                }
                else if ( i == InPlayer.NormalMaxCheckPointHistory.ID)                                                          /// 当前 关卡显示设置   
                {
                    Object      TempObj         = Resources.Load(UIPanelConfig.CheckPointItem) as Object;                       /// 加载创建对象
                    GameObject  TheCP           = Instantiate(TempObj) as GameObject;                                           /// 实例化对象

                    TheCP.name                                                          = "CP_" + i;                            /// 面板名称
                    TheCP.transform.parent                                              = View.ChapterMap.transform;            /// 面板父级别 指定
                    TheCP.transform.localScale                                          = Vector3.one;                          /// 面板缩放 设置
                    TheCP.transform.transform.localPosition                             = new Vector3(Pos_x,Pos_y,0);           /// 面板位移 坐标设置
                    TheCP.GetComponent<UISprite>().spriteName                           = "xuedi_ludian3";                      /// 面板图集 名称
                    TheCP.GetComponent<UISprite>().MakePixelPerfect();                                                          /// 图标缩放-图像大小(最佳)

                    TheCP.GetComponent<CheckPointItemView>().CP_ID                      = i;                                    /// 关卡ID设置
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.enabled        = true;
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.spriteName     = "jiantou";
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.GetComponent   <TweenPosition>().enabled = true;       /// 关卡箭头移动动画 显示
                    TheCP.GetComponent<CheckPointItemView>().CP_Icon.enabled            = false;
                    TheCP.GetComponent<CheckPointItemView>().StarNum.gameObject.SetActive(false);
                    if ( IsBigCP )
                    {
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.enabled    = true;
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.spriteName = "ludiankuang-touxiang";
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.MakePixelPerfect();
                        TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.GetComponent<TweenPosition>().enabled = true;
                        TheCP.GetComponent<CheckPointItemView>().CP_Icon.enabled        = true;
                        int     HeroID          = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BOSSShow;
                        TheCP.GetComponent<CheckPointItemView>().CP_Icon.spriteName     =
                                                  Configs_Hero.sInstance.GetHeroDataByHeroID(HeroID).head84;
                        TheCP.GetComponent<CheckPointItemView>().StarNum.enabled        = true;
                    }

                    TheCP.SetActive                                                     (true);
                    TempObj                                                             = null;
                }
                else                                                                                                            /// 当前 关卡显示设置   
                {
                    Object      TempObj         = Resources.Load(UIPanelConfig.CheckPointItem) as Object;                       /// 加载创建对象
                    GameObject  TheCP           = Instantiate(TempObj) as GameObject;                                           /// 实例化对象
                    TheCP.name                                                          = "CP_" + i;                            /// 面板名称
                    TheCP.transform.parent                                              = View.ChapterMap.transform;            /// 面板父级别 指定
                    TheCP.transform.localScale                                          = Vector3.one;                          /// 面板缩放 设置
                    TheCP.transform.transform.localPosition                             = new Vector3(Pos_x,Pos_y,0);           /// 面板位移 坐标设置

                    TheCP.GetComponent<CheckPointItemView>().CP_ID                      = i;                                    /// 关卡ID设置
                    TheCP.SetActive                                                     (false);                                /// 关卡不显示
                    TempObj                                                             = null;
                }

            }

        }
        else if ( InCheckP_Sys.CurrentCP_Type == ChapterType.Elite  )                                                   /// 章节所有精英关卡设置   
        {
            Debug.Log           ("Elite Chapter!");
            int Eilte_Min = Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).LittleID[1];     /// 当前章节最小关卡ID
            int Eilte_Max = Configs_Chapter.sInstance.GetChapterDataByChapterID(InCheckP_Sys.chapterID).BigID[1];        /// 当前章节最大关卡ID

            for (int i = Eilte_Min; i <= Eilte_Max; i++)
            {
                float           Pos_x           = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BuildingCoordinateX;  /// 关卡配置坐标
                float           Pos_y           = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BuildingCoordinateY;  /// 关卡配置坐标
                Debug.Log       ("CP_ID: " + i + "|| MaxCP_ID: "+ InPlayer.EliteMaxCheckPointHistory.ID);
                if      (i <  InPlayer.EliteMaxCheckPointHistory.ID)
                {
                    Debug.Log("Elite_1" );
                    int         StarNum         = 0;                                                                            /// 关卡星级                                  
                    foreach     (var Item in InPlayer.GetEliteCheckPointStars)                                                  /// 关卡通关星级 
                    {
                        if (Item.Key == i )
                        { StarNum = Item.Value; }
                    }
                    Debug.Log   ("CP_ID:" + i + "/" + " StarNum: " + StarNum.ToString());
                    Object      TempObj         = Resources.Load(UIPanelConfig.CheckPointItem) as Object;                       /// 加载创建对象
                    GameObject  TheCP           = Instantiate(TempObj) as GameObject;                                           /// 实例化对象

                    TheCP.name                                                          = "CP_" + i;                            /// 面板名称
                    TheCP.transform.parent                                              = View.ChapterMap.transform;            /// 面板父级别 指定
                    TheCP.transform.localScale                                          = Vector3.one;                          /// 面板缩放 设置
                    TheCP.transform.transform.localPosition                             = new Vector3(Pos_x,Pos_y,0);           /// 面板位移 坐标设置
                    TheCP.GetComponent<UISprite>().spriteName                           = "xuedi_ludianda";                     /// 面板图集 名称
                    TheCP.GetComponent<UISprite>().MakePixelPerfect();                                                          /// 图标缩放-图像大小(最佳)

                    TheCP.GetComponent<CheckPointItemView>().CP_ID                      = i;                                    /// 关卡ID设置
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.spriteName     = "ludiankuang-touxiang";               /// 背框图集名称
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.MakePixelPerfect();                                    /// 背框 缩放-图像大小(最佳)
                    int HeroID                  = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BOSSShow;     /// 获取关卡Boss ID
                    TheCP.GetComponent<CheckPointItemView>().CP_Icon.spriteName =                                               /// 图像指定 BOss 图集名称
                                                  Configs_Hero.sInstance.GetHeroDataByHeroID(HeroID).head84;

                    switch (StarNum)                                                                                            /// 星级显示    
                    {
                        case 1:
                            {
                                TheCP.GetComponent<CheckPointItemView>().Star_1.enabled = true;
                                TheCP.GetComponent<CheckPointItemView>().Star_2.enabled = false;
                                TheCP.GetComponent<CheckPointItemView>().Star_3.enabled = false;
                                break;
                            }
                        case 2:
                            {
                                TheCP.GetComponent<CheckPointItemView>().Star_1.enabled = true;
                                TheCP.GetComponent<CheckPointItemView>().Star_2.enabled = true;
                                TheCP.GetComponent<CheckPointItemView>().Star_3.enabled = false;
                                break;
                            }
                        case 3:
                            {
                                TheCP.GetComponent<CheckPointItemView>().Star_1.enabled = true;
                                TheCP.GetComponent<CheckPointItemView>().Star_2.enabled = true;
                                TheCP.GetComponent<CheckPointItemView>().Star_3.enabled = true;
                                break;
                            }
                        default:
                            break;
                    }
                    TheCP.SetActive                                                     (true);
                    TempObj                                                             = null;
                }
                else if (i == InPlayer.EliteMaxCheckPointHistory.ID)
                {
                    Debug.Log   ("Elite_2");
                    Object      TempObj         = Resources.Load(UIPanelConfig.CheckPointItem) as Object;                       /// 加载创建对象
                    GameObject  TheCP           = Instantiate(TempObj) as GameObject;                                           /// 实例化对象

                    TheCP.name                                                          = "CP_" + i;                            /// 面板名称
                    TheCP.transform.parent                                              = View.ChapterMap.transform;            /// 面板父级别 指定
                    TheCP.transform.localScale                                          = Vector3.one;                          /// 面板缩放 设置
                    TheCP.transform.transform.localPosition                             = new Vector3(Pos_x,Pos_y,0);           /// 面板位移 坐标设置
                    TheCP.GetComponent<UISprite>().spriteName                           = "xuedi_ludianda";                     /// 面板图集 名称
                    TheCP.GetComponent<UISprite>().MakePixelPerfect();                                                          /// 图标缩放-图像大小(最佳)

                    TheCP.GetComponent<CheckPointItemView>().CP_ID                      = i;                                    /// 关卡ID设置
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.spriteName     = "ludiankuang-touxiang";               /// 背框 图集名称
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.MakePixelPerfect();                                    /// 背框 缩放-图像大小(最佳)
                    TheCP.GetComponent<CheckPointItemView>().CP_BG_Frame.GetComponent   <TweenPosition>().enabled = true;       /// 关卡箭头移动动画 显示
                    int HeroID                  = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(i).BOSSShow;     /// 获取关卡Boss ID
                    TheCP.GetComponent<CheckPointItemView>().CP_Icon.spriteName =                                               /// 图像指定 BOss 图集名称
                                                  Configs_Hero.sInstance.GetHeroDataByHeroID(HeroID).head84;
                    TheCP.GetComponent<CheckPointItemView>().StarNum.gameObject.SetActive (false);

                    TheCP.SetActive                                                     (true);
                    TempObj                                                             = null;
                }
                else
                {
                    Debug.Log("Elite_3");
                    Object      TempObj         = Resources.Load(UIPanelConfig.CheckPointItem) as Object;                       /// 加载创建对象
                    GameObject  TheCP           = Instantiate(TempObj) as GameObject;                                           /// 实例化对象
                    TheCP.name                                                          = "CP_" + i;                            /// 面板名称
                    TheCP.transform.parent                                              = View.ChapterMap.transform;            /// 面板父级别 指定
                    TheCP.transform.localScale                                          = Vector3.one;                          /// 面板缩放 设置
                    TheCP.transform.transform.localPosition                             = new Vector3(Pos_x,Pos_y,0);           /// 面板位移 坐标设置

                    TheCP.GetComponent<CheckPointItemView>().CP_ID                      = i;                                    /// 关卡ID设置
                    TheCP.SetActive                                                     (false);                                /// 关卡不显示
                    TempObj                                                             = null;
                }
            }
        }
    }
    private void                        ExitClickHandler()                                                              // 章节当前面板       
    {
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.WorldMapPanel);
        PanelManager.sInstance.HidePanel(SceneType.Main, UIPanelConfig.CheckPointSelectPanel);
    }
    private void                        NormalClickHandler()                                                            // 普通章节点击       
    {
        for(int i = 0;i < View.ChapterMap.gameObject.transform.childCount;)                                                 /// 清理当前所有关卡设置 
        {
            DestroyImmediate(View.ChapterMap.gameObject.transform.GetChild(i).gameObject);
        }
        InCheckP_Sys.CurrentCP_Type     = ChapterType.Normal;                                                               /// 设置章节类型
        int TheChapterID                = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(InPlayer.NormalMaxCheckPointHistory.ID).GenusChapter;
        if (InPlayer.PlayerLevel < Configs_Chapter.sInstance.GetChapterDataByChapterID(TheChapterID).UnlockLevel[0])
        {
            PanelManager.sInstance.ShowNoticePanel("普通关卡" + Configs_Chapter.sInstance.
                                        GetChapterDataByChapterID(TheChapterID).UnlockLevel[0] + "开启");
            return;
        }
        InCheckP_Sys.chapterID          = TheChapterID;                                                                     /// 目标章节 
        ChapterItemInit();                                                                                                  /// 章节初始化
        View.EliteBtn.GetComponent<BoxCollider>().enabled       = true;                                                     /// 精英章节 区域点击 (激活)
        View.EliteBtn.GetComponent<UISprite>().spriteName       = "xuanzenandu-xuanzhong";                                  /// 精英 图集名称
        View.NormalBtn.GetComponent<BoxCollider>().enabled      = false;                                                    /// 普通章节 区域点击 (未激活)
        View.NormalBtn.GetComponent<UISprite>().spriteName      = "xuanzenandu-xuanzhong (Copy)";                           /// 普通 图集名称

    }
    private void                        EliteClickHandler()                                                             // 精英章节点击       
    {
        
        for(int i = 0;i < View.ChapterMap.gameObject.transform.childCount;)                                                 /// 清理当前所有关卡设置 
        {
            DestroyImmediate(View.ChapterMap.gameObject.transform.GetChild(i).gameObject);
        }
        InCheckP_Sys.CurrentCP_Type     = ChapterType.Elite;                                                                /// 设置章节类型
        int TheChapterID                = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(InPlayer.EliteMaxCheckPointHistory.ID).GenusChapter;
        if (InPlayer.PlayerLevel < Configs_Chapter.sInstance.GetChapterDataByChapterID(TheChapterID).UnlockLevel[1])
        {
            PanelManager.sInstance.ShowNoticePanel("精英难度" + Configs_Chapter.sInstance.
                                        GetChapterDataByChapterID(TheChapterID).UnlockLevel[1] + "开启");
            return;
        }
        InCheckP_Sys.chapterID          = TheChapterID;                                                                     /// 目标章节 
        ChapterItemInit();                                                                                                  /// 章节初始化
        View.EliteBtn.GetComponent<BoxCollider>().enabled       = false;                                                    /// 精英章节 区域点击 (未激活)
        View.EliteBtn.GetComponent<UISprite>().spriteName       = "xuanzenandu-xuanzhong (Copy)";                           /// 精英 图集名称
        View.NormalBtn.GetComponent<BoxCollider>().enabled      = true;                                                     /// 普通章节 区域点击 (未激活)
        View.NormalBtn.GetComponent<UISprite>().spriteName      = "xuanzenandu-xuanzhong";                                  /// 普通 图集名称

    }
    private string                      ToChineseNumber(int chapterID)                                                  // 转化:中文数字      
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