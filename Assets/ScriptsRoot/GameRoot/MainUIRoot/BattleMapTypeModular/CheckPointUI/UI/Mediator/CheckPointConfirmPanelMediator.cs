using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
/// <summary> 关卡确认面板 </summary>

public class CheckPointConfirmPanelMediator : EventMediator
{
    [Inject]
    public CheckPointConfirmPanelView View                      { set; get; }
    [Inject]
    public ICheckPointSys       InCheckP_Sys                    { set; get; }
    [Inject]
    public IPersetLineupData    InBattlePosM                    { set; get; }

    public override void        OnRegister()
    {
        View.ViewInit();
        BaseInit();

        View.dispatcher.AddListener(View.ExitClick_Event,               ExitClickHandler);                          // 退出点击 处理
        View.dispatcher.AddListener(View.LineupClick_Event,             LineupClickHandler);                        // 阵容点击 处理
        View.dispatcher.AddListener(View.ChallengeClick_Event,          ChallengeClickHandler);                     // 挑战点击 处理
    }
    public override void        OnRemove()
    {
        View.dispatcher.RemoveListener(View.ExitClick_Event,            ExitClickHandler);                          // 退出点击 处理
        View.dispatcher.RemoveListener(View.LineupClick_Event,          LineupClickHandler);                        // 阵容点击 处理
        View.dispatcher.RemoveListener(View.ChallengeClick_Event,       ChallengeClickHandler);                     // 挑战点击 处理
    }


    private void                BaseInit()                                                      //  基础初始化
    {
        View.CP_ID              = InCheckP_Sys.currentCheckPointID;                             /// 关卡配置ID
        CP_IconShowInit();                                                                      /// 关卡图标显示  初始化

    }
    private void                ExitClickHandler()
    {
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.CheckPointSelectPanel);
        PanelManager.sInstance.HidePanel(SceneType.Main, UIPanelConfig.CheckPointConfirmPanel);
    }
    private void                LineupClickHandler()
    {

        InBattlePosM.BattleType = BattleType.CheckPoint;
        InBattlePosM.configID   = InCheckP_Sys.currentCheckPointID;
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.BattlePositionPanel);
        PanelManager.sInstance.HidePanel(SceneType.Main, UIPanelConfig.CheckPointConfirmPanel);
    }
    private void                ChallengeClickHandler()
    { }
    private void                CP_IconShowInit()                                               // 关卡初始化信息                 
    {
        NpcIconListInit();                                                                      /// Npc图标      初始化显示
        AwardIconListInit();                                                                    /// 奖励物品图标  初始化显示   
     }
    private void                NpcIconListInit()                                               // 关卡_Npc头像图标  显示 (初始化) 
    {
        int     CP_LineupID     = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).ArrayID[0];              /// Npc阵容配置ID
        int     NpcQuality      = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).NPCQuality;              /// Npc品质
        int     NpcStar         = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).NPCStar;                 /// Npc星级

        string                  FrameQuality                = "";                                                                   /// Npc品质框名称
        switch                  (NpcQuality)                                                                                        /// Npc品质获取 品质框        
            {
                case 1:         FrameQuality                = "touxiang-bai-84";        break;
                case 2:
                case 3:         FrameQuality                = "touxiang-lv-84";         break;
                case 4:
                case 5:
                case 6:         FrameQuality                = "touxiang-lan-84";        break;
                case 7:
                case 8:
                case 9:
                case 11:        FrameQuality               = "touxiang-zi-84";          break;
                case 12:        FrameQuality               = "touxiang-jin-84";         break;
                default:        FrameQuality               = "touxiang-jin-84";         break;

            }

        List<Vector3>           NpcPOSList                  = new List<Vector3>();                                                  /// Npc图像坐标                                    
        NpcPOSList.Add                                      ( new Vector3( 15f, 75f, 0));                                           /// Npc坐标_1                             
        NpcPOSList.Add                                      ( new Vector3(128f, 75f, 0));                                           /// Npc坐标_2
        NpcPOSList.Add                                      ( new Vector3(240f, 75f, 0));                                           /// Npc坐标_3

        List<int>               CP_NpcIDList                = new List<int>();                                                      /// 关卡 NpcID 列表(除去空位[0])
        Dictionary<int,int>     CP_LineupNPCID_Dic          = new Dictionary<int, int>();                                           /// 阵容 NpcID 字典

        if ( CP_LineupID != 0 )                                                                                                     /// 阵容列表NPC_ID 添加进字典 
        {
            int                 TheNpcID                    = 0;
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number1;
            CP_LineupNPCID_Dic.Add(1, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number2;
            CP_LineupNPCID_Dic.Add(2, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number3;
            CP_LineupNPCID_Dic.Add(3, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number4;
            CP_LineupNPCID_Dic.Add(4, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number5;
            CP_LineupNPCID_Dic.Add(5, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number6;
            CP_LineupNPCID_Dic.Add(6, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number7;
            CP_LineupNPCID_Dic.Add(7, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number8;
            CP_LineupNPCID_Dic.Add(8, TheNpcID);
            TheNpcID            = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(CP_LineupID).Number9;
            CP_LineupNPCID_Dic.Add(9, TheNpcID);
        }
        foreach(var Item in CP_LineupNPCID_Dic)                                                                                     /// 添加关卡成员 到列表       
        {
            if(Item.Value != 0)
            { CP_NpcIDList.Add(Item.Value); }
        }

        if ( Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).BOSSShow != 0)                                /// Boss关卡                 
        {

            for ( int i = 0; i < 2;i++ )
            {
                string          NpcSpriteName               = Configs_Hero.sInstance.GetHeroDataByHeroID(CP_NpcIDList[i]).head84;   /// Npc 头像名称
                Object          TempItem                    = Resources.Load(UIPanelConfig.CheckPointIconItem);                     /// 加载   关卡图标视图面板
                GameObject      TheItem                     = Instantiate(TempItem) as GameObject;                                  /// 实例化 关卡图标视图面板

                TheItem.gameObject.transform.SetParent      (View.NpcIconList.transform);                                           /// 关卡图标视图 指定父级
                TheItem.name                                = "NpcIcon"+ i;                                                         /// 图标面板 名称
                TheItem.transform.localScale                = Vector3.one;                                                          /// 初始化 缩放比例
                TheItem.transform.localPosition             = NpcPOSList[i];                                                        /// 设置   图标坐标

                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName = FrameQuality;                              /// 品质框
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName = NpcSpriteName;                             /// Npc头像名称

                TempItem        = null;
            }
            if (Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).BOSSShow != 0)
            {
                int             BossID                      = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).BOSSShow;
                string          NpcSpriteName               = Configs_Hero.sInstance.GetHeroDataByHeroID(BossID).head84;            /// Boss 头像名称
                Object          TempItem                    = Resources.Load(UIPanelConfig.CheckPointIconItem);                     /// 加载   关卡图标视图面板
                GameObject      TheItem                     = Instantiate(TempItem) as GameObject;                                  /// 实例化 关卡图标视图面板

                TheItem.gameObject.transform.SetParent      (View.NpcIconList.transform);                                           /// 关卡图标视图 指定父级
                TheItem.name                                = "BossIcon";                                                           /// 图标面板 名称
                TheItem.transform.localScale                = Vector3.one;                                                          /// 初始化 缩放比例
                TheItem.transform.localPosition             = NpcPOSList[2];                                                        /// 设置   图标坐标

                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName              = FrameQuality;                 /// 品质框
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName              = NpcSpriteName;                /// Npc头像名称
                TheItem.GetComponent<CheckPointIconItemViewMono>().Boss_Label.enabled               = true;                         /// Boss标志显示
                TheItem.GetComponent<CheckPointIconItemViewMono>().GetComponent<UISprite>().enabled = true;                         /// Boss框  显示

                TempItem        = null;
            }           
        }
        else                                                                                                                        /// Npc小关卡 (没BOSS)       
        {
            for ( int i = 0; i < 3; i++ )
            {
                string          NpcSpriteName               = Configs_Hero.sInstance.GetHeroDataByHeroID(CP_NpcIDList[i]).head84;   /// Npc 头像名称
                Object          TempItem                    = Resources.Load(UIPanelConfig.CheckPointIconItem);                     /// 加载   关卡图标视图面板
                GameObject      TheItem                     = Instantiate(TempItem) as GameObject;                                  /// 实例化 关卡图标视图面板

                TheItem.gameObject.transform.SetParent      (View.NpcIconList.transform);                                           /// 关卡图标视图 指定父级
                TheItem.name                                = "NpcIcon"+ i;                                                         /// 图标面板 名称
                TheItem.transform.localScale                = Vector3.one;                                                          /// 初始化 缩放比例
                TheItem.transform.localPosition             = NpcPOSList[i];                                                        /// 设置   图标坐标

                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName = FrameQuality;                              /// 品质框
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName = NpcSpriteName;                             /// Npc头像名称
            }
        }
            
    }
    private void                AwardIconListInit()                                             // 关卡_掉落奖励物品 显示 (初始化) 
    {

        int     CP_DropID       = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).DropID;          /// 关卡掉落配置ID
        int     ItemQuality     = 0;
        string                  ItemFrameQuality            = "";
        string                  ItemIconName                = "";
        List<int>               CP_DropItemIDList           = new List<int>();                                              /// 关卡掉落物品ID列表
        List<Vector3>           ItemPOSList                 = new List<Vector3>();                                          /// 物品图像坐标 列表
        ItemPOSList.Add                                     ( new Vector3(-50f,-77,0));                                     /// 物品坐标_1
        ItemPOSList.Add                                     ( new Vector3( 41f,-77,0));                                     /// 物品坐标_2
        ItemPOSList.Add                                     ( new Vector3(132f,-77,0));                                     /// 物品坐标_3
        ItemPOSList.Add                                     ( new Vector3(223f,-77,0));                                     /// 物品坐标_4
        ItemPOSList.Add                                     ( new Vector3(314f,-77,0));                                     /// 物品坐标_5

        if  (CP_DropID != 0)                                                                                                /// 加载掉落物品 ID列表         
        {
            int tempItemID = 0;
            tempItemID = Configs_CheckPointDrop.sInstance.GetCheckPointDropDataByDropID(CP_DropID).PropID1;
            if (tempItemID != 0)                            CP_DropItemIDList.Add(tempItemID);
            tempItemID = Configs_CheckPointDrop.sInstance.GetCheckPointDropDataByDropID(CP_DropID).PropID2;
            if (tempItemID != 0)                            CP_DropItemIDList.Add(tempItemID);
            tempItemID = Configs_CheckPointDrop.sInstance.GetCheckPointDropDataByDropID(CP_DropID).PropID3;
            if (tempItemID != 0)                            CP_DropItemIDList.Add(tempItemID);
            tempItemID = Configs_CheckPointDrop.sInstance.GetCheckPointDropDataByDropID(CP_DropID).PropID4;
            if (tempItemID != 0)                            CP_DropItemIDList.Add(tempItemID);
            tempItemID = Configs_CheckPointDrop.sInstance.GetCheckPointDropDataByDropID(CP_DropID).PropID5;
            if (tempItemID != 0)                            CP_DropItemIDList.Add(tempItemID);
        }
        if (Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).ChapterType == 1)                      /// 普通章节 图标显示设置       
        {

            for (int i = 0; i < CP_DropItemIDList.Count ; i++ )
            {
                if ( CP_DropItemIDList[i] < 60000 )                                                                         /// 最后一项道具奖励_结束循环 
                {
                    ItemIconName = Configs_Prop.sInstance.GetPropDataByPropID(CP_DropItemIDList[i]).PropIcon84;                 /// 道具图标名称
                    ItemQuality  = Configs_Prop.sInstance.GetPropDataByPropID(CP_DropItemIDList[i]).PropQuality;                /// 获取道具品质

                    switch (ItemQuality)                                                                                        /// 获取道具框品质 
                    {
                        case 1: ItemFrameQuality = "daoju-bai-84"; break;
                        case 2: ItemFrameQuality = "daoju-lv-84"; break;
                        case 3: ItemFrameQuality = "daoju-lan-84"; break;
                        case 4: ItemFrameQuality = "daoju-zi-84"; break;
                        case 5: ItemFrameQuality = "daoju-jin-84"; break;
                    }

                    Object          PropObj                     = Resources.Load(UIPanelConfig.CheckPointIconItem);
                    GameObject      TheProp                     = Instantiate(PropObj) as GameObject;

                    TheProp.gameObject.transform.SetParent      (View.AwardIconList.transform);
                    TheProp.name                                = "ItemIcon_"+i;
                    TheProp.transform.localScale                = new Vector3(0.9f,0.9f,0.9f);
                    TheProp.transform.localPosition             = ItemPOSList[i];

                    TheProp.GetComponent<CheckPointIconItemViewMono>().BG_Frame.atlas               = Util.LoadAtlas(AtlasConfig.PropIcon70);
                    TheProp.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName          = ItemFrameQuality;
                    TheProp.GetComponent<CheckPointIconItemViewMono>().ItemIcon.atlas               = Util.LoadAtlas(AtlasConfig.PropIcon70);
                    TheProp.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName          = ItemIconName;

                    PropObj = null;
                    break;
                }

                ItemIconName    = Configs_Equip.sInstance.GetEquipDataByEquipID(CP_DropItemIDList[i]).EquipIcon_84;         /// 装备图标名称
                ItemQuality     = Configs_Equip.sInstance.GetEquipDataByEquipID(CP_DropItemIDList[i]).EquipQuality;         /// 获取装备品质
                switch (ItemQuality)                                                                                        /// 获取装备框品质 
                {
                    case 1: ItemFrameQuality = "daoju-bai-84"; break;
                    case 2: ItemFrameQuality = "daoju-lv-84"; break;
                    case 3: ItemFrameQuality = "daoju-lan-84"; break;
                    case 4: ItemFrameQuality = "daoju-zi-84"; break;
                    case 5: ItemFrameQuality = "daoju-jin-84"; break;
                }

                Object          TempObj                     = Resources.Load(UIPanelConfig.CheckPointIconItem);             /// 加载物品面板
                GameObject      TheItem                     = Instantiate(TempObj) as GameObject;                           /// 物品面板实例

                TheItem.gameObject.transform.SetParent      (View.AwardIconList.transform);                                 /// 设置面板父级对象
                TheItem.name                                = "ItemIcon_"+i;                                                /// 面板名称
                TheItem.transform.localScale                = new Vector3(0.9f,0.9f,0.9f);                                  /// 面板局部缩放比例
                TheItem.transform.localPosition             = ItemPOSList[i];                                               /// 面板坐标

                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.atlas               = Util.LoadAtlas(AtlasConfig.PropIcon70);       /// 加载背框图集
                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName          = ItemFrameQuality;                             /// 设置图集 名称
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.atlas               = Util.LoadAtlas(AtlasConfig.EquipIcon84);      /// 加载图标图集
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName          = ItemIconName;                                 /// 设置图集 名称

                TempObj         = null;                                                                                                         /// 临时对象 null
                if              (i == 4)                    break;                                                                              /// 展示上限(5个)
            }
        }
        else                                                                                                                /// 精英章节 图标显示设置       
        {
            if (Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).BOSSShow != 0)
            {
                int     HeroID          = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID(View.CP_ID).BOSSShow;    /// 获取英雄ID
                ItemIconName            = Configs_Hero.sInstance.GetHeroDataByHeroID(HeroID).head84;                            /// 英雄图标名称
                ItemFrameQuality        = "hunshikuang-84";                                                                     /// 魂石背框名称


                Object                  TempObj             = Resources.Load(UIPanelConfig.CheckPointIconItem);                 /// 加载物品面板
                GameObject              TheItem             = Instantiate(TempObj) as GameObject;                               /// 物品面板实例

                TheItem.gameObject.transform.SetParent      (View.AwardIconList.transform);                                     /// 设置面板父级对象
                TheItem.name                                = "ItemIcon_HeroSoul";                                              /// 面板名称
                TheItem.transform.localScale                = new Vector3(0.9f,0.9f,0.9f);                                      /// 面板局部缩放比例
                TheItem.transform.localPosition             = ItemPOSList[0];                                                   /// 面板坐标

                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.atlas               = Util.LoadAtlas(AtlasConfig.HeroHeadIcon);     /// 加载背框图集
                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName          = ItemFrameQuality;                             /// 设置图集 名称
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.atlas               = Util.LoadAtlas(AtlasConfig.HeroHeadIcon);     /// 加载图标图集
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName          = ItemIconName;                                 /// 设置图集 名称

                TempObj = null;
            }
            for (int i = 0; i < CP_DropItemIDList.Count ; i++ )
            {
                if (CP_DropItemIDList[i] < 60000)                                                                               /// 最后一项道具奖励_结束循环
                {
                    ItemIconName        = Configs_Prop.sInstance.GetPropDataByPropID(CP_DropItemIDList[i]).PropIcon84;          /// 道具图标名称
                    ItemQuality         = Configs_Prop.sInstance.GetPropDataByPropID(CP_DropItemIDList[i]).PropQuality;         /// 获取道具品质

                    switch (ItemQuality)                                                                                        /// 获取道具框品质 
                    {
                        case 1: ItemFrameQuality = "daoju-bai-84"; break;
                        case 2: ItemFrameQuality = "daoju-lv-84"; break;
                        case 3: ItemFrameQuality = "daoju-lan-84"; break;
                        case 4: ItemFrameQuality = "daoju-zi-84"; break;
                        case 5: ItemFrameQuality = "daoju-jin-84"; break;
                    }

                    Object PropObj      = Resources.Load(UIPanelConfig.CheckPointIconItem);
                    GameObject TheProp  = Instantiate(PropObj) as GameObject;

                    TheProp.gameObject.transform.SetParent  (View.AwardIconList.transform);
                    TheProp.name                            = "ItemIcon_" + i;
                    TheProp.transform.localScale            = new Vector3(0.9f, 0.9f, 0.9f);
                    TheProp.transform.localPosition         = ItemPOSList[i];

                    TheProp.GetComponent<CheckPointIconItemViewMono>().BG_Frame.atlas           = Util.LoadAtlas(AtlasConfig.PropIcon70);
                    TheProp.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName      = ItemFrameQuality;
                    TheProp.GetComponent<CheckPointIconItemViewMono>().ItemIcon.atlas           = Util.LoadAtlas(AtlasConfig.PropIcon70);
                    TheProp.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName      = ItemIconName;

                    PropObj = null;
                    break;
                }
                ItemIconName            = Configs_Equip.sInstance.GetEquipDataByEquipID(CP_DropItemIDList[i]).EquipIcon_84;     /// 装备图标名称
                ItemQuality             = Configs_Equip.sInstance.GetEquipDataByEquipID(CP_DropItemIDList[i]).EquipQuality;     /// 获取装备品质
                switch (ItemQuality)                                                                                            /// 获取装备框品质 
                {
                    case 1:     ItemFrameQuality            = "daoju-bai-84";       break;
                    case 2:     ItemFrameQuality            = "daoju-lv-84";        break;
                    case 3:     ItemFrameQuality            = "daoju-lan-84";       break;
                    case 4:     ItemFrameQuality            = "daoju-zi-84";        break;
                    case 5:     ItemFrameQuality            = "daoju-jin-84";       break;
                }

                Object          TempObj                     = Resources.Load(UIPanelConfig.CheckPointIconItem);                 /// 设置面板父级对象
                GameObject      TheItem                     = Instantiate(TempObj) as GameObject;                               /// 物品面板实例
                    
                TheItem.gameObject.transform.SetParent      (View.AwardIconList.transform);                                     /// 设置面板父级对象
                TheItem.name                                = "ItemIcon_"+i;                                                    /// 面板名称
                TheItem.transform.localScale                = new Vector3(0.9f,0.9f,0.9f);                                      /// 面板局部缩放比例
                TheItem.transform.localPosition             = ItemPOSList[i+1];                                                 /// 面板坐标

                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.atlas               = Util.LoadAtlas(AtlasConfig.PropIcon70);       /// 加载背框图集
                TheItem.GetComponent<CheckPointIconItemViewMono>().BG_Frame.spriteName          = ItemFrameQuality;                             /// 设置图集 名称
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.atlas               = Util.LoadAtlas(AtlasConfig.EquipIcon84);      /// 加载图标图集
                TheItem.GetComponent<CheckPointIconItemViewMono>().ItemIcon.spriteName          = ItemIconName;                                 /// 设置图集 名称

                TempObj = null;
                if              (i == 3)                    break;
            }
        }
    }

}
