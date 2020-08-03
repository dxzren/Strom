using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using CodeStage.AntiCheat.ObscuredTypes;
/// <summary> 战斗阵容 位置预设  </summary>

public class BattlePositionPanelMediator : EventMediator
{
    [Inject]
    public BattlePositionPanelView  View                            { set; get; }
    [Inject]
    public IPlayer                  InPlayer                        { set; get; }
    [Inject]
    public IPersetLineupData        InBattlePos                     { set; get; }                             

    BP_HeroItemShowType             HeroItemShowType                = BP_HeroItemShowType.AllHeroList;                      // (默认展示-所有英雄)

    public override void            OnRegister                  ()                                                          // 初始化程序              
    {
        View.ViewInit();                                                                                                    // 视图控件 初始化
        BaseInit();                                                                                                         // 基本信息 初始化
        AllHeroClickHandler();                                                                                              // 英雄图标排列显示(默认全体模式) 

        View.dispatcher.AddListener(View.ExitClick_Event,                       ExitClickHandler);                          // 退出     [Btn]
        View.dispatcher.AddListener(View.BattleSatrtClick_Event,                BattleStartClickHandler);                   // 战斗启动  [Btn]

        dispatcher.AddListener(BattlePosEvent.HeroIconClickUpHero_Evnet,        IconClickHeroUpHandler);                    // 英雄上阵<数据+展示>  [Btn]
        dispatcher.AddListener(BattlePosEvent.HeroIconClickDownHero_Evnet,      IconClickHeroDownHandler);                  // 英雄下阵<数据+展示>  [Btn]
    }
    public override void            OnRemove                    ()                                                          // 移除监听                
    {

        View.dispatcher.RemoveListener(View.ExitClick_Event,                    ExitClickHandler);                          // 退出     [Btn]
        View.dispatcher.AddListener(View.BattleSatrtClick_Event,                BattleStartClickHandler);                   // 战斗启动  [Btn]

        dispatcher.RemoveListener(BattlePosEvent.HeroIconClickUpHero_Evnet,     IconClickHeroUpHandler);                    // 英雄头像  [Btn]
        dispatcher.RemoveListener(BattlePosEvent.HeroIconClickDownHero_Evnet,   IconClickHeroDownHandler);                  // 英雄头像  [Btn]

    }

    public void                     BaseInit                    ()                                                          // 基础数据_初始化          
    {
        BattleMapLineUpInit();                                                                                              /// 战斗地图阵容 初始化
        InitPosSet();                                                                                                       /// 阵容预置设置 初始化(位置特效等.)
        LoadHeroModelList();                                                                                                /// 加载英雄列表 模型
    }
    public void                     ExitClickHandler            ()                                                          // 退出     [Btn]          
    {
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.CheckPointConfirmPanel);                             /// 显示 关卡确认面板
        Util.BackToNomarl(this.transform.parent.transform.gameObject);                                                      /// 黑屏转正常
        PanelManager.sInstance.HidePanel(SceneType.Main, UIPanelConfig.BattlePositionPanel);                                /// 隐藏 阵容预置面板

    }
    public void                     BattleStartClickHandler     ()                                                          // 战斗启动  [Btn]         
    {
        if (InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).Count < 1)
        {    PanelManager.sInstance.ShowNoticePanel("最少上阵一名英雄");           return; }

        switch (InBattlePos.BattleType)
        {
            case BattleType.CheckPoint:
                {
                    print           ("进入关卡战斗流程");
//                  InPlayer.SaveLineUpFile(InPlayer.BattleTypeToLineUp(InBattlePos.BattleType), BattleType.CheckPoint);    /// 保存关卡阵容文件到本地
                    dispatcher.Dispatch(CheckPointEvent.REQ_CheckPointChallenge_Event);                                     /// 挑战关卡事件
                    break;
                }
            default:                break;
        }
    }
    public void                     AllHeroClickHandler         ()                                                          // 全体英雄  [Btn]         
    {
        HeroItemShowType            = BP_HeroItemShowType.AllHeroList;                                                      // 当前英雄列表 显示类型
        TabBtnShow();                                                                                                       // 英雄选择     切换按钮
        CreateHeroItemList(HeroItemShowType);                                                                               // 创建 英雄图标选择列表
    }

    public void                     IconClickHeroUpHandler      ()                                                          // 英雄上阵<数据+展示>[Btn] 
    {
        Configs_HeroData            TheHeroD = Configs_Hero.sInstance.GetHeroDataByHeroID(InBattlePos.TempHero_D.ID);       /// 当前英雄 配置数据
        Debug.Log                   ("上阵英雄: " + TheHeroD.HeroID);
        if ( (InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).ContainsValue(InBattlePos.TempHero_D)) == false)          /// 点击英雄不在阵容
        {
            PosNumType          ThePos                          = PosNumType.Null;                                          /// 阵容位置 变量
            switch (TheHeroD.Position)                                                                                      /// 英雄战斗位置(前,中,后)
            {
                case 1: ThePos = JoinPos(PosNumType.Front_1_Our, PosNumType.Front_2_Our, PosNumType.Front_3_Our);   break;  /// 前排 安排入场位置 
                case 2: ThePos = JoinPos(PosNumType.Front_1_Our, PosNumType.Front_2_Our, PosNumType.Front_3_Our);   break;  /// 中排 安排入场位置 
                case 3: ThePos = JoinPos(PosNumType.Front_1_Our, PosNumType.Front_2_Our, PosNumType.Front_3_Our);   break;  /// 后排 安排入场位置 
            }
                                                                          
            if (ThePos == PosNumType.Null)                                                                                  /// 没有位置入场 更新信号返回
            {   dispatcher.Dispatch(EventSignal.UpdateInfo_Event);      return; }
            Debug.Log("场景空位 Pos: " + ThePos);

            InPlayer.UpBattle       (ThePos,InBattlePos.TempHero_D,InBattlePos.BattleType);                                 /// 当英雄添加 进战斗阵容
            InBattlePos.TempHero_D.IsInBattle                   = true;                                                     /// 当前英雄 已上阵

            Configs_ActionAndEffectData TheActEffeD             = Configs_ActionAndEffect.sInstance.                        /// 英雄动作特效配置数据
                                                                  GetActionAndEffectDataByResourceID(TheHeroD.Resource);

            InBattlePos.BattlePosSetDic[ThePos].BP_HeroData     = InBattlePos.TempHero_D;                                   /// 预置阵容位置 填入英雄数据
            LoadHeroModel           (TheActEffeD,TheHeroD.HeroID,ThePos);                                                   /// 加载英雄模型
            dispatcher.Dispatch     (EventSignal.UpdateInfo_Event);                                                         /// 更新信号
        }
    }
    public void                     IconClickHeroDownHandler    ()                                                          // 英雄下阵 [Btn]          
    {
        IHeroData                   TheHeroD                    = InBattlePos.TempHero_D;                                   /// 英雄数据
        PosNumType                  ThePos                      = PosNumType.Null;                                          /// 阵容位置

        foreach( var Item in InPlayer.BattleTypeToLineUp(InBattlePos.BattleType))
        {
            if ( Item.Value.ID == TheHeroD.ID )          ThePos = Item.Key;
        }

        InBattlePos.BattlePosSetDic[ThePos]                     = null;                                                     /// 预置阵容 英雄数据 设置null
        InBattlePos.BattlePosSetDic[ThePos].HeroModel           = null;                                                     /// 预置阵容 英雄模型 设置null
        Util.DestoryImmediate(InBattlePos.AllHeroObjDic[TheHeroD.ID]);                                                      /// 摧毁 _GameObject 
        InPlayer.DownBattle(ThePos, InBattlePos.BattleType);                                                                /// 战斗阵容列表 删除英雄
    }

    #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
    private void                    BattleMapLineUpInit         ()                                                          // 战斗地图阵容初始化    
    {
        Dictionary<PosNumType, IHeroData> TheCurrentLineup          = InPlayer.GetLineUpFile(InBattlePos.BattleType);       /// 获取本地文件 战斗地图阵容

        if      ( InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).Count != 0 )                                          /// 获取缓存 战斗地图阵容 <null则读取本地文件阵容>
        {         TheCurrentLineup  = InPlayer.BattleTypeToLineUp( InBattlePos.BattleType );          }
        else if ( InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).Count == 0)                                           /// 本地阵容 保存到缓存阵容 
        {
            if  ( TheCurrentLineup == null )                                                                                /// 本地阵容为null 创建初始阵容
            {
                Dictionary<PosNumType, IHeroData> TheHeroDataList   = new Dictionary<PosNumType, IHeroData>();              /// 英雄数据列表
                if      ( InPlayer.PlayerRoleHero.ID == 100001  )                                                           /// 冰系法师初始阵容 
                {
                    foreach (var Item in InPlayer.HeroList)                                                                 /// 添加初始英雄到阵容   
                    {
                        if (Item.ID == 100001)
                        {
                            TheCurrentLineup.Add( PosNumType.Front_2_Our,Item);
                        }
                        if (Item.ID == 100004)
                        {
                            TheCurrentLineup.Add( PosNumType.Back_3_Our,Item);
                        }
                    }
                }
                else if ( InPlayer.PlayerRoleHero.ID == 100002  )                                                           /// 射手女王初始阵容 
                {
                    foreach (var Item in InPlayer.HeroList)                                                                 /// 添加初始英雄到阵容   
                    {
                        if (Item.ID == 100002)
                        {
                            TheCurrentLineup.Add( PosNumType.Front_2_Our,Item);
                        }
                        if (Item.ID == 100006)
                        {
                            TheCurrentLineup.Add( PosNumType.Back_3_Our,Item);
                        }
                    }
                }
                else if ( InPlayer.PlayerRoleHero.ID == 100003  )                                                           /// 矮人战士初始阵容 
                {

                    foreach(var Item in InPlayer.HeroList)                                                                  /// 添加初始英雄到阵容  
                    {
                        if (Item.ID == 100003)
                        {
                            TheCurrentLineup.Add( PosNumType.Back_2_Our,Item);
                        }
                        if (Item.ID == 100008)
                        {
                            TheCurrentLineup.Add( PosNumType.Middle_3_Our,Item);
                        }
                    }
                }
            }
            foreach ( var Item in TheCurrentLineup )                                                                        /// 缓存阵容更新     
            {
                if (!InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).ContainsKey(Item.Key))
                {
                    InPlayer.BattleTypeToLineUp(InBattlePos.BattleType).Add(Item.Key,Item.Value);
                }
            }
        }
    }
    private void                    InitPosSet                  ()                                                          // 阵容预置设置 初始化   
    {
        Dictionary<PosNumType, IHeroData> TheCurrentLineup      = InPlayer.BattleTypeToLineUp(InBattlePos.BattleType);      /// 读取<战斗地图>缓存阵容
        InBattlePos.BattlePosSetDic.Clear();                                                                                /// 清理 预置阵容设置
        for(int i = 1;i < 10;i++)                                                                                           /// 完成预置阵容设置 
        {
            IHeroData               TheHeroData                 = null;                                                     /// 英雄数据变量
            foreach(var Item in TheCurrentLineup)
            {
                if (Item.Key == (PosNumType)i)                  TheHeroData = Item.Value;
            }
            InBattlePos.BattlePosSetDic.Add((PosNumType)i, new BattlePosSet()                                               /// 配置阵容位置 预置配置 
            {
                TableEffect         = View.PartPosList[i-1].GetChild(0).gameObject,                                         /// 常驻特效
                UpEffect            = View.PartPosList[i-1].GetChild(1).gameObject,                                         /// 上阵特效
                BP_HeroData         = TheHeroData,                                                                          /// 英雄数据
                HeroModel           = null,                                                                                 /// 英雄模型
                HeroContainer       = View.HeroModelList[i-1]                                                               /// 英雄集合
            });
        }

    }
    private void                    LoadHeroModelList           ()                                                          // 加载英雄列表 模型     
    {
        Dictionary<PosNumType, IHeroData> TheCurrentLineup      = InPlayer.BattleTypeToLineUp(InBattlePos.BattleType);      /// 读取<战斗地图>缓存阵容

        foreach ( var Item in TheCurrentLineup)                                                                             /// 加载英雄列表 模型 
        {
            Configs_ActionAndEffectData TheActEffeD     = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID
                                                          (Configs_Hero.sInstance.GetHeroDataByHeroID(Item.Value.ID).Resource);
            LoadHeroModel(TheActEffeD, Item.Value.ID, Item.Key);                                                            /// 加载单个英雄模型

        }
    }
    private void                    LoadHeroModel ( Configs_ActionAndEffectData actEffcD, int heroID, PosNumType inPosNum)  // 加载英雄 模型         
    {
        if ( heroID == InPlayer.PlayerRoleHero.ID)                                                                          /// 确认主角英雄是否上阵
        {    InBattlePos.isRoleUpLineup                         = true; }
#if (UPDATERES || !UNITY_EDITOR) && !Force_Local
        StartCoroutine(Util.LoadHeroAsync(actEffcD,80,(GameObject obj) =>
        {
            Util.PlayAnima(obj, actEffcD.StandbyAction);
            obj.SetActive                                       (true);
            obj.transform.localPosition                         = new Vector3(0, -8000, -80);
            obj.transform.SetParent(InBattlePos.BattlePosSetDic[inPosNum].HeroContainer.transform);                         /// 指定对象父级
            AddInLoadHero                                       (heroID, obj);
            obj.name                                            = "Hero" + heroID.ToString();
            InBattlePos.BattlePosSetDic[inPosNum].HeroModel     = obj;                                                      /// 模型对象添加到 配置字典 
            obj.transform.localPosition                         = new Vector3(0,0,-80f);
            obj.transform.localEulerAngles                      = new Vector3(12f,120f,-15f);
        }));
#else
        GameObject TempObj                                      = Util.LoadRes(actEffcD, 80) as GameObject;                 /// 加载模型特效资源
        Util.PlayAnima                                          (TempObj, actEffcD.StandbyAction);                          /// 播放模型动画
        TempObj.transform.localPosition                         = new Vector3(0, -8000, -80);                               /// 设置模型坐标
        TempObj.transform.SetParent(InBattlePos.BattlePosSetDic[inPosNum].HeroContainer.transform);                         /// 指定对象父级 

        InBattlePos.BattlePosSetDic[inPosNum].HeroModel         = TempObj;                                                  /// 模型对象添加到 配置字典 
        AddInLoadHero                                           (heroID, TempObj);                                          /// obj对象 添加进配置 英雄Obj列表

        TempObj.transform.localPosition                         = new Vector3(0, 0, -80);                                   /// 设置 坐标
        TempObj.transform.localEulerAngles                      = new Vector3(12f, 120f, -15f);                             /// 设置 旋转角度
#endif
    }

    private void                    AddInLoadHero(int inHeroID,GameObject inObj)                                            // 添加进配置 英雄Obj列表 
    {
        if( InBattlePos.AllHeroObjDic.ContainsKey(inHeroID))                                                                /// Obj对象列表 添加Obj对象
        {   InBattlePos.AllHeroObjDic[inHeroID] = inObj; }
        else
        {   InBattlePos.AllHeroObjDic.Add(inHeroID, inObj); }
    }
    private void                    TabBtnShow()                                                                            // 英雄类型切换显示       
    {
        switch( HeroItemShowType )
        {
            case BP_HeroItemShowType.AllHeroList:                           // 全体激活显示   
                {
                    View.AllHeroBtn.GetComponent<UISprite>().spriteName     = "weixuanzhongyeqian";
                    View.AllBtn_On.enabled                                  = true;
                    View.Front_On.enabled                                   = false;
                    View.Middle_On.enabled                                  = false;
                    View.Back_On.enabled                                    = false;
                    break;
                }
            case BP_HeroItemShowType.FrontHeroList:                         // 前排排激活显示 
                {   
                    View.AllHeroBtn.GetComponent<UISprite>().spriteName     = "weixuanzhongyeqian";
                    View.AllBtn_On.enabled                                  = false;
                    View.Front_On.enabled                                   = true;
                    View.Middle_On.enabled                                  = false;
                    View.Back_On.enabled                                    = false;
                    break;
                }
            case BP_HeroItemShowType.MiddleHeroList:                        // 中排激活显示   
                {
                    View.AllHeroBtn.GetComponent<UISprite>().spriteName     = "weixuanzhongyeqian";
                    View.AllBtn_On.enabled                                  = false;
                    View.Front_On.enabled                                   = false;
                    View.Middle_On.enabled                                  = true;
                    View.Back_On.enabled                                    = false;
                    break;
                }
            case BP_HeroItemShowType.BackHeroList:                          // 后排激活显示   
                {
                    View.AllHeroBtn.GetComponent<UISprite>().spriteName     = "weixuanzhongyeqian";
                    View.AllBtn_On.enabled                                  = false;
                    View.Front_On.enabled                                   = false;
                    View.Middle_On.enabled                                  = false;
                    View.Back_On.enabled                                    = true;
                    break;
                }
        }

    }
    private void                    CreateHeroItemList(BP_HeroItemShowType inItemType)                                      // 创建英雄头像列表       
    {
        List<IHeroData>             TheHeroD_List                = new List<IHeroData>();

        if (inItemType != BP_HeroItemShowType.AllHeroList)                                                                  /// 当前选择英雄列表 
        {
            foreach ( var Item in InPlayer.HeroList)
            {
                int ItemPosType     = Configs_Hero.sInstance.GetHeroDataByHeroID(Item.ID).Position;
                if (  ItemPosType == (int)BP_HeroItemShowType.AllHeroList )
                {   TheHeroD_List.Add(Item);                                }
            }
        }
        else                                                                                                                /// 全体英雄        
        {
            TheHeroD_List           = InPlayer.HeroList;
        }

        Util.HeroListSort           (TheHeroD_List);                                                                        /// 英雄列表 显示排序(星级>品质>等级)

        if (TheHeroD_List.Contains(InPlayer.PlayerRoleHero))                                                                /// 如果主角英雄在列表中,插入最前  
        {
            IHeroData TempRole = InPlayer.PlayerRoleHero;
            TheHeroD_List.Remove(TempRole);
            TheHeroD_List.Insert(0, TempRole);
        }

        for ( int i = 0; i < View.HeroIconGrid.gameObject.transform.childCount;)                                            /// 清理当前所有子级别头像 
        {
            DestroyImmediate(View.HeroIconGrid.gameObject.transform.GetChild(i).gameObject);
        }

        Object                      TempBP_Item                 = Util.Load(UIPanelConfig.BP_HeroIconItem);                 /// 加载所有英雄图标项
        for ( int i = 0; i < TheHeroD_List.Count; i++ )
        {
            IHeroData               TempHeroD                   = TheHeroD_List[i];
            GameObject              TheObj                      = Instantiate(TempBP_Item) as GameObject;
            TheObj.gameObject.name                              = "HeroIcon"+ TempHeroD.ID;
            TheObj.GetComponent<UIDragScrollView>().scrollView  = View.IconItemScrollView;
            TheObj.transform.SetParent                          (View.HeroIconGrid.transform);
            TheObj.transform.localPosition                      = Vector3.zero;
            TheObj.transform.localScale                         = Vector3.one;
  
            TheObj.GetComponent<BP_HeroIconItemView>().TheHeroID = TempHeroD.ID;
        }
        View.HeroIconGrid.repositionNow                         = true;                                                     /// Grid下次更新
    }

    private PosNumType              JoinPos(PosNumType inPos1, PosNumType inPos2, PosNumType inPos3)                        // 判定英雄模型 入场位置  
    {
        bool                        isPos_1                     = true;                                                     // 位置1 初始空
        bool                        isPos_2                     = true;                                                     // 位置2 初始空
        bool                        isPos_3                     = true;                                                     // 位置3 初始空

        Dictionary<PosNumType, IHeroData> TheLineupList         = InPlayer.BattleTypeToLineUp(InBattlePos.BattleType);      // 获取当前战斗阵容

        foreach( var Item in TheLineupList )                                                                                /// 遍历阵容位置 
        {
            if (Item.Key == inPos1)       isPos_1               = false;
            if (Item.Key == inPos2)       isPos_2               = false;
            if (Item.Key == inPos3)       isPos_3               = false;
        }
        if ( isPos_1 == true )      {    return inPos1; }
        if ( isPos_2 == true )      {    return inPos2; }
        if ( isPos_3 == true )      {    return inPos3; }

        PanelManager.sInstance.ShowNoticePanel(Language.GetValue("FeedbackMsg030"));                                        /// 提示:位置全满,返回空位
        Debug.Log(" 位置已满_当前位置只能 配置3个英雄!");
        return                      PosNumType.Null;
    }
    #endregion
}