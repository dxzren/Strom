using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using strange.extensions.mediation.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

public class PanelManager : EventView
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher         IEDispatcher                    { get; set; }
    [Inject]
    public IGameData                gameData                        { set; get; }
    [Inject]
    public IPlayer                  player                          { set; get; }

    public float                    x, y, Type, ID;
    public Vector3                  V;
    public static PanelManager      sInstance                       = null;
    public string                   sceneName                       = "";

    new void Awake()                
    {
        sInstance = this;
    }
    new void Start()                
    {
        Debug.Log                   ("05>    PanelManager.Start()加载Start场景内容!");
        switch (sceneName)               
        {
            case "Start":
            case "LogIn":
                LoadScenes          ( SceneType.Start );
                Debuger.Log("PanelManager-- Start()--:  启动场景:Start ");
                break;
            case "Main":
                LoadScenes          ( SceneType.Main );
                Debuger.Log("PanelManager-- Start()--:  启动场景:Main ");
                break;
            case "Battle":
                Debuger.Log("PanelManager-- Start()--:  启动场景:Battle ");
                LoadScenes          ( SceneType.Battle );
                break;
            default:
                Debug.LogError      ( "启动场景失败!" );
                break;
        }
    }

    #region================================================||  通用界面设置  ||=============================================================

    public List<GameObject>         DialogPaneList          = new List<GameObject>();
    public GameObject               LoadingPanel            = null;                                                         // 加载页面对象

    public void                     Deal( SceneType scene,  string panelName,List<PanelObj> panelList,GameObject panel)     // 更换面板                 
    {
//        Debug.Log( "Deal_!" );
        if ( Hide30Panel( panelName ))                                                                              /// 30LvPanel 自动点击关闭      
        {
            Debug.Log( "This 30 Panel !" );
            UnityEngine.Object      theObj              = Resources.Load( UIPanelConfig.HidePanel30Floor);
            if (theObj != null)
            {
                Debug.Log( "This 30 Panel != null");
                GameObject GObj = Instantiate(theObj) as GameObject;
                GObj.SetActive                          (false);
                GObj.transform.parent                   = panel.transform;
                GObj.transform.localScale               = Vector3.one;
                GObj.transform.localEulerAngles         = Vector3.zero;
                GObj.transform.localPosition            = Vector3.zero;
                HidePanel30FloorView HP30View           = GObj.GetComponent < HidePanel30FloorView >();
                HP30View.target                         = panelName;
                HP30View.panelShowScene                 = scene;
                GObj.SetActive                          ( true);
            }

        }

        panelList.Add           (new PanelObj(panelName, panel));                                                   /// 对象面板添加到列表
        GameObject              HidePanel           = null;
        if ( panelList.Count >= 2 )                                                                                 /// 要显示第一个pannel，不走这逻辑
        { HidePanel          = ((PanelObj)panelList[panelList.Count - 2]).gameObj;   }

        /// dispatcher.Dispatch(GuideEvent.PanelChanged_Event, panelName);                                          /// 面板更换

        //if (Configs_PanelInformation.sInstance.mPanelInformationDatas.ContainsKey(panelName))                       /// 给指定Panel加载动画        
        //{
        //    Debug.Log("PanelLv:" + Configs_PanelInformation.sInstance.GetPanelInformationDataByPanelName(panelName).PanelFloor);
        //    Configs_PanelInformationData PanelData = Configs_PanelInformation.sInstance.GetPanelInformationDataByPanelName(panelName);
        //    if    ( PanelData.PanelFloor == 10 )                                                                    /// 黑屏渐变到正常                 
        //    {
        //        Debug.Log("PanelFloor_10");
        //        if( !dispatcher.HasListener (UIAnimation.LoadOverEvent, UIAnimation.Instance().BlackToNormal_assist))
        //        {
        //            dispatcher.AddListener (UIAnimation.LoadOverEvent, UIAnimation.Instance().BlackToNormal_assist);
        //        }

        //        if( HidePanel == null)

        //        {
        //            UIAnimation.Instance().BlackToNormal(panel, true);
        //        }
        //        else
        //        {
        //            panel.SetActive(false);
        //            UIAnimation.Instance().BlackToNormalMain(HidePanel, panel, true);
        //            if (HidePanel.name == UIPanelConfig.MainMallPanel)
        //            {
        //                MainUIPanelMediator MainUIM = HidePanel.GetComponent<MainUIPanelMediator>();
        //                if ( MainUIM != null )
        //                { MainUIM.PlayOutUIAnima(); }
        //            }
          
        //        }

        //    }

        //    else if ( PanelData.PanelFloor == 11 )                                                                  /// 渐渐显示
        //    { Debug.Log("PanelFloor_10"); UIAnimation.Instance().TransAlpha(panel, true); }

        //    else if ( PanelData.PanelFloor == 20 )                                                                  /// 渐渐显示 + 由大到小
        //    {
        //        Debug.Log("PanelFloor_20");
        //        UIAnimation.Instance().TransAlpha(panel, true);
        //        UIAnimation.Instance().TransScale(panel, true);
        //    }
        //    else if ( PanelData.PanelFloor == 21 )                                                                  /// 向右滑入（聊天界面，界面逻辑处理）
        //    { }
        //    else if ( PanelData.PanelFloor == 30 )                                                                  /// 渐渐显示 + 由大到小
        //    {
        //        Debug.Log("PanelFloor_30");
        //        UIAnimation.Instance().TransAlpha(panel, true);
        //        UIAnimation.Instance().TransScale(panel, true);
        //    }
        //    else if ( PanelData.PanelFloor == 32 )                                                                  /// 不在此处理，在界面逻辑内处理
        //    { }
        //    else if ( PanelData.PanelFloor == 40 )                                                                  /// 渐渐显示
        //    { Debug.Log("PanelFloor_40"); UIAnimation.Instance().TransAlpha(panel, true); }
        //}

    }

    private bool                    Hide30Panel             ( string panelName)                                             // 所有30界面点击隐藏       
    {
        Configs_PanelInformationData paneldata = Configs_PanelInformation.sInstance.GetPanelInformationDataByPanelName(panelName);
        if( paneldata != null)
        {
            if( paneldata.PanelFloor == 30)
            { return true; }
        }
        return false;
    }

    //-----------------------<< 加载和清空界面栈(场景Scene) >>-------------------------------------------------------------------------------
    public void                     LoadScenes              ( SceneType scene )                                             // 加载界面栈               
    {
        List<PanelObj>              PanelList               = null;                             /// 界面栈_面板列表
        switch (scene)
        {
            case SceneType.Start:
                PanelList           = gameData.StartPanelList();
                break;
            case SceneType.Main:
                PanelList           = gameData.MainPanelList();
                break;
            case SceneType.Battle:
                PanelList           = gameData.BattlePanelList();
                break;
        }

        List<PanelObj>              NewPanelList            = new List<PanelObj>();             /// 界面列表添加到新面板列表
        foreach(PanelObj PanelObj   in PanelList)
        {
            NewPanelList.Add        ( new PanelObj(PanelObj.name, PanelObj.gameObj));           /// 添加到列表
            if(PanelObj.gameObj     != null)
            {
                DestroyImmediate    ( PanelObj.gameObj );                                       /// 立即销毁对象
            }
        }

        PanelList.Clear();                                                                      /// 加载界面栈 面板队列列表
        foreach ( PanelObj obj in   NewPanelList )
        {
            UnityEngine.Object      PanelObj                = Util.Load(obj.name);                                  
            GameObject              TempPanel               = Instantiate(PanelObj) as GameObject;
            TempPanel.transform.    parent                  = gameObject.transform;
            TempPanel.transform.    localScale              = Vector3.one;
            TempPanel.transform.    localEulerAngles        = Vector3.zero;
            TempPanel.transform.    localPosition           = TempPanel.transform.position;
            PanelList.Add                                   (new PanelObj(obj.name, TempPanel));
            
            if (Hide30Panel(obj.name))                                                          /// 隐藏30面板
            {
                UnityEngine.Object  TempObj                 = Resources.Load(UIPanelConfig.HidePanel30Floor);
                if (TempObj != null)
                {
                    GameObject      TempGameObj             = Instantiate(TempObj) as GameObject;
                    TempGameObj.SetActive                   (false);
                    TempGameObj.transform.parent            = TempPanel.transform;
                    TempGameObj.transform.localScale        = Vector3.one;
                    TempGameObj.transform.localEulerAngles  = Vector3.zero;
                    TempGameObj.transform.localPosition     = Vector3.zero;
                    HidePanel30FloorView HidePanel30View    = TempGameObj.GetComponent<HidePanel30FloorView>();
                    HidePanel30View.target                  = obj.name;
                    HidePanel30View.panelShowScene          = scene;
                    TempGameObj.SetActive                   (true);
                }
            }   
            if (    PanelList.Count > 0 )
            {       BigPanelMask ( PanelList,( PanelList[PanelList.Count - 1]).name);    }               
        }

        foreach ( PanelObj Panel in PanelList)
        {
            if (  Panel.gameObj != null)
            {     DestroyImmediate ( Panel.gameObj );  }
        }
        NewPanelList.Clear();
    }

    public List<PanelObj>           GetScene                ( SceneType scene )                                             // 获取界面栈               
    {
        List<PanelObj> Panels = null;
        switch(scene)
        {
            case SceneType.Start:
                Panels = gameData.StartPanelList();
                break;
            case SceneType.Main:
                Panels = gameData.MainPanelList();
                break;
            case SceneType.Battle:
                Panels = gameData.BattlePanelList();
                break;
        }
        return Panels;
    }
    public void                     ClearPanels             ( SceneType scene )                                             // 清空界面栈               
    {
        List<PanelObj> panelList = null;
        switch(scene)
        {
            case SceneType.Start:
                panelList = gameData.StartPanelList();
                break;
            case SceneType.Main:
                panelList = gameData.StartPanelList();
                break;
            case SceneType.Battle:
                panelList = gameData.StartPanelList();
                break;
        }
        foreach (PanelObj obj in panelList)
        {
            Util.DestoryImmediate(obj.gameObj);
        }
        panelList.Clear();               
    }
    

    //-----------------------<< 显示和关闭对话面板 >>----------------------------------------------------------------------------------------

    /// <summary> 显示对话面板  text: 显示内容 , okText: 按钮文字 </summary>
    public GameObject               ShowDialogPanel         ( string  text,  string  okText,  Action OKCallBack = null, 
                                                              Action  CancelCallBack = null,  string Canceltext = "取消",
                                                              bool    HideCancel     = false)                                                          
    {
        gameData.IsExitPanel = true;                                                            // 弹出退出面板
        UnityEngine.Object tempPanel = Util.Load(UIPanelConfig.DialogPanel);                    // 加载对话面板
        GameObject theDialogPanel = tempPanel as GameObject;
        if(theDialogPanel.activeSelf)       
        {
           theDialogPanel.SetActive(false);                                                     // 关闭活动
        }
        GameObject dialogPanel = Instantiate(theDialogPanel) as GameObject;
        dialogPanel.name = "diglog";
        dialogPanel.transform.parent = gameObject.transform;
        dialogPanel.transform.localScale = Vector3.one;                                         // 对象局部缩放为 初始值 1
        dialogPanel.transform.eulerAngles = Vector3.zero;                                       // 对象旋转角度为 初始值 0
        dialogPanel.transform.position = Vector3.zero;                                          // 对象位移坐标为 初始值 0

        UnityEngine.Object mask = Util.Load(UIPanelConfig.MaskPanel);
        GameObject theMask = Instantiate(mask) as GameObject;
        theMask.transform.parent = gameObject.transform;                                        // 转换对象父级
        theMask.transform.localScale = Vector3.one;                                             // 对象局部缩放为 初始值 1
        theMask.transform.localEulerAngles = Vector3.zero;                                      // 对象旋转角度为 初始值 0
        theMask.transform.localPosition = Vector3.zero;                                         // 对象位移坐标为 初始值 0

        DialogPanelView view = dialogPanel.GetComponent<DialogPanelView>();
        view.OKCallBack = OKCallBack;
        view.CancelCallBack = CancelCallBack;
        view.cancel.GetComponentInChildren<UILabel>().text = Canceltext;
        view.shenming1.text = text;
        view.confirmText.text = okText;

        if(HideCancel)                      
        {
            view.cancel.SetActive(false);
            view.removeClickBackGround = true;
        }

        theDialogPanel = null;
        if(DialogPaneList.Count > 0)        
        {
            DialogPaneList[DialogPaneList.Count - 1].SetActive(false);
        }
        DialogPaneList.Add(dialogPanel);
        dialogPanel.SetActive(true);
        Destroy(theMask, 0.31f);
        return dialogPanel;

    }
    public void                     CloseDialogPanel        ( GameObject obj )                                              // 关闭对话面板             
    {
        if(DialogPaneList.IndexOf(obj) == DialogPaneList.Count - 1)
        {
            DialogPaneList.Remove(obj);
            UIAnimation.Instance().TransAlpha(obj, false);
            UIAnimation.Instance().TransScale(obj, false);
        }
        else
        {
            obj.SetActive(false);
        }
        if(DialogPaneList.Count > 0)
        {
            DialogPaneList[DialogPaneList.Count - 1].SetActive(true);
        }
    }
    public void                     CloseAllDialogPanel()                                                                   // 关闭所有对面面板         
    {
        for(int i = 0;i < DialogPaneList.Count;i++)
        {
            DialogPaneList[i].SetActive(false);
            DialogPaneList.Remove(DialogPaneList[i]);
            UIAnimation.Instance().TransAlpha(DialogPaneList[i], false);
            UIAnimation.Instance().TransScale(DialogPaneList[i], false);
        }
    }



    //-----------------------<< 显示和隐藏界面 >>-------------------------------------------------------------------------------------------
    public bool                     IsShowPanel             ( List<PanelObj>panelList,string panelName)                     // 是否显示相同的面板Panel   
    {
        foreach ( PanelObj obj in panelList )
        {
            if  ( panelName == obj.name )               return true;
        }
        return false;
    }
    public GameObject               ShowPanel               ( SceneType scene,  string  panelName)                          // 显示面板                 
    {
        Debug.Log                   ("ShowPanel: " + panelName);
        List<PanelObj>              panelList       = null;
        switch(scene)
        {
            case SceneType.Start:
                panelList                           = gameData.StartPanelList();
                break;
            case SceneType.Main:
                panelList                           = gameData.MainPanelList();
                break;
            case SceneType.Battle:
                panelList                           = gameData.BattlePanelList();
                break;
        }
        if (panelList.Count != 0 && ((PanelObj)panelList[panelList.Count - 1]).name == panelName)                   /// 不允许连续显示一样的面板     
        {
            return null;
        }
        if (IsShowPanel(GetScene(scene), panelName))                                                                /// 打开已存在界面,先把之前的移除                         
        {
            foreach(PanelObj obj in panelList)
            {
                if(obj.name == panelName)
                {
                    panelList.Remove(obj);
                    DestroyImmediate(obj.gameObj);
                    break;
                }
            }
        }

        UnityEngine.Object      tempPanel           = Util.Load(panelName);
        GameObject              panel               = null;
        if(tempPanel != null)
        {
            panel                                   = Instantiate(tempPanel) as GameObject;
            panel.name                              = panelName;
            panel.transform.parent                  = gameObject.transform;
            panel.transform.localScale              = Vector3.one;
            panel.transform.localEulerAngles        = Vector3.zero;
            panel.transform.localPosition           = panel.transform.position;
            Deal(scene, panelName, panelList, panel);
            tempPanel                               = null;
        }
        return panel;
    }
    public GameObject               ShowPanel               ( SceneType scene,  string  panelName,int panelLevel)           // 显示面板                 
    {
        GameObject                  panelObj                = ShowPanel(scene, panelName);
        UIPanel                     panel                   = panelObj.GetComponent<UIPanel>();
        panel.depth                                         = panelLevel;
        return panelObj;
    }
    public void                     HidePanel               ( SceneType scene,  string  panelName)                          // 隐藏面板                 
    {
        Debug.Log                   (" Hide: " + panelName + "_Panle !");
        if(!IsShowPanel             (GetScene(scene),panelName))
        {
            return;
        }
        List<PanelObj>              panelList               = null;
        switch (scene)
        {
            case SceneType.Start:
                panelList           = gameData.StartPanelList();
                break;
            case SceneType.Main:
                panelList           = gameData.MainPanelList();
                break;
            case SceneType.Battle:
                panelList           = gameData.BattlePanelList();
                break;
        }

        GameObject              GObj                        = null;
        foreach                 ( PanelObj PObj in panelList)                        
        {
            if(PObj.name == panelName)
            {
                GObj            = PObj.gameObj;
                panelList.Remove(PObj);
                break;
            }
        }
        Util.DestoryImmediate   ( GObj );                                                                                   /// 销毁对象
        
    }

    public void                     ShowLoadingPanel            ( float     time    = 2f)                                   // 显示加载界面             
    {
        if (LoadingPanel != null)
        {
            return;
        }

        CancelInvoke                                        ("showLoad");
        Invoke                                              ("showLoad", time);
        UnityEngine.Object      tempPanel                   = Util.Load(UIPanelConfig.ShortLoadingPanel);
        GameObject              panel                       = Instantiate(tempPanel) as GameObject;
        panel.name                                          = UIPanelConfig.ShortLoadingPanel;
        panel.transform.parent                              = gameObject.transform;
        panel.transform.localScale                          = Vector3.one;
        panel.transform.localEulerAngles                    = Vector3.zero;
        panel.transform.localPosition                       = panel.transform.position;
        LoadingPanel                                        = panel;
    }
    public void                     ShowNoticePanel             ( string    text,   TipsType type = TipsType.Normal)        // 显示通知,片刻即消失       
    {
        UnityEngine.Object tempPanel = Util.Load(UIPanelConfig.WarningPanel);
        GameObject panel = Instantiate(tempPanel) as GameObject;
        if(type == TipsType.Gold)
        {
            panel.transform.GetComponentInChildren<UISprite>().spriteName = "tishikuang_1";
        }
        panel.name                                                  = UIPanelConfig.WarningPanel;
        panel.transform.parent                                      = gameObject.transform;
        panel.transform.localScale                                  = Vector3.one;
        panel.transform.localEulerAngles                            = Vector3.zero;
        panel.transform.localPosition                               = new Vector3(0, 0, -2000);
        panel.GetComponent<WarningPanel>().label.text               = text;
    }
    public void                     ShowViewEmptyInfoPanel      ( Vector3   pos,    string tex)                             // 显示空信息面板视图        
    {
        UnityEngine.Object      TempPanle                   = Util.Load   ( UIPanelConfig.ViewEmptyInfoPanel );         /// 加载视图
        GameObject              Panel                       = Instantiate(TempPanle) as GameObject;                     /// 实例化对象
        Panel.name                                          = "ViewEmptyInfoPanel";                                     /// 视图名称
        Panel.transform.parent                              = gameObject.transform;                                     /// 指定父级
        Panel.transform.localEulerAngles                    = Vector3.zero;
        Panel.transform.localScale                          = Vector3.one;
        Panel.transform.localPosition                       = pos;
        Panel.GetComponent<ViewEmptyInfoPanel>().
              info.text                                     = tex;
        TempPanle                                           = null;
    }
    public void                     ShowReConnectionPanel       ( SceneType scene)                                          // 断线重连界面             
    {
        if(LoadingPanel != null)
        {
            return;
        }
        UnityEngine.Object tempPanel = Util.Load(UIPanelConfig.ShortLoadingPanel);
        GameObject panel = Instantiate(tempPanel) as GameObject;

        panel.name = UIPanelConfig.ShortLoadingPanel;
        panel.transform.parent = gameObject.transform;
        panel.transform.localScale = Vector3.one;
        panel.transform.localEulerAngles = Vector3.zero;
        panel.transform.localPosition = panel.transform.position;
        LoadingPanel = panel;
    }

    private void                    showLoad()                                                                              // 短加载显示设置           
    {
        LoadingPanel.GetComponent<ShortLoading>().SetShow();                                                            /// 短加载显示设置
    }
    //-----------------------<< 界面显示动画 >>-------------------------------------------------------------------------------------------

    public void                 LoadOverHandler_10Planel    ( string PanelName )                                            // 加载完数据,10级界面动画    
    {
        foreach (Configs_PanelInformationData PanelInfodata in Configs_PanelInformation.sInstance.mPanelInformationDatas.Values)
        {
            if(PanelInfodata.PanelName == PanelName)
            {
                if (PanelInfodata.PanelFloor == 10)                                                                     ///黑屏渐变到正常
                {
                    Debug.Log("dispatcher.Dispatch(UIAnimation.LoadOverEvent); ");
                    dispatcher.Dispatch(UIAnimation.LoadOverEvent);
                }
                else
                    break;
                break;
            }
        }
        Debug.Log("LoadOverHandler_10Planel :");
    }
    public void                 HideLoadingPanel()                                                                          // 隐藏加载界面              
    {
        if(this != null)
        {
            CancelInvoke ("showLoad" );                     /// 取消短加载界面线程
            if( LoadingPanel != null )                      /// 销毁Loading界面
            {   DestroyImmediate( LoadingPanel); }
        }
    }
    private void                BigPanelMask                ( List<PanelObj> scenePanelList,string panelName )              // 大屏遮挡逻辑              
    {
        Debug.Log("BigPanelMask :");
        ///     < 用于显示 (界面栈中，出现了整屏的界面，那么在这个界面下面的界面就会被销毁)<"panelName">正显示panel的名字，栈顶 >
        foreach (Configs_PanalNameData key in Configs_PanalName.sInstance.mPanalNameDatas.Values)
        {
            Debug.Log("Key.name : " + key.Name.ToString() + "_____________Panel: " + panelName);
            if ( key.Name == panelName )
            {
                for(int i = 0;i < scenePanelList.Count; i++)                
                {
                    PanelObj TempObj = scenePanelList[i];
                    if(TempObj.name != key.Name)
                    {
                        if(TempObj.gameObj != null)
                        {
                            Util.DestoryImmediate(TempObj.gameObj);
                        }
                    }
                    else
                    {

                        break;
                    }

                }
                break;
            }
        }
    }
    private void                BigPanelMask_2              ( List<PanelObj> PanelList,string PanelName,SceneType scene)    // 大屏遮挡逻辑_2            
    {
        /// <summary>
        /// 大屏遮挡逻辑，用于隐藏
        /// 如果屏幕的界面栈中，要销毁的这个界面是大屏界面，就要根据逻辑找到最接近栈顶的大屏，并且恢复这个大屏之上的所有界面
        /// </summary>
        /// <param name="panelName">正显示panel的名字，栈顶</param>
        bool                    isBig               = false;                                                        /// 是否全屏界面

        foreach (   Configs_PanalNameData key in Configs_PanalName.sInstance.mPanalNameDatas.Values )               /// 全面屏界面 检索           
        {
            if (    key.Name == PanelName)                                      
            {                   isBig               = true;   break;  }
        }

        if (isBig)                                                                                                  /// 检索列表下一个界面是否全屏 
        {
            List<PanelObj> TempPanelList = new List<PanelObj>();
            bool                 isNextBig          = false;

            for ( int i = PanelList.Count - 1; i >= 0; i++ )
            {
                foreach ( Configs_PanalNameData key in Configs_PanalName.sInstance.mPanalNameDatas.Values)
                {
                    if  ( PanelList[i].name == key.Name )
                    {     isNextBig                 = true;   break;  }
                }
                TempPanelList.Insert(0, PanelList[i]);
                if      ( isNextBig )               break;
            }

            foreach     ( PanelObj key in PanelList )                                                               /// 列表对象为空的面板 指定对象   
            {
                if      ( key.gameObj == null )
                {
                    UnityEngine.Object TempObj = Util.Load(key.name);
                    GameObject TempPanel = Instantiate(TempObj) as GameObject;
                    TempPanel.name = key.name;
                    TempPanel.transform.parent = gameObject.transform;
                    TempPanel.transform.localEulerAngles = Vector3.zero;
                    TempPanel.transform.localScale = Vector3.one;
                    TempPanel.transform.position = Vector3.zero;
                    key.gameObj = TempPanel;

                    if  ( Hide30Panel(key.name))                                                                    /// 是否30界面    
                    {
                        UnityEngine.Object TempObj_2 = Resources.Load(UIPanelConfig.HidePanel30Floor);
                        if (TempObj_2 != null)
                        {
                            GameObject TempPanel_2 = Instantiate(TempObj) as GameObject;
                            TempPanel_2.SetActive(false);
                            TempPanel_2.transform.parent = TempPanel.transform;
                            TempPanel_2.transform.localEulerAngles = Vector3.zero;
                            TempPanel_2.transform.localScale = Vector3.one;
                            TempPanel_2.transform.position = Vector3.zero;
                            HidePanel30FloorView TempHide30View = TempPanel_2.GetComponent<HidePanel30FloorView>();
                            TempHide30View.target = key.name;
                            TempHide30View.panelShowScene = scene;
                            TempPanel_2.SetActive(false);
                        }
                    }
                    if      ( Configs_PanelInformation.sInstance.mPanelInformationDatas.ContainsKey(key.name))      /// 检测最外层的是不是需要从黑屏转变为正常特效
                    {
                        if  ( Configs_PanelInformation.sInstance.mPanelInformationDatas[key.name].PanelFloor == 10)
                        { UIAnimation.Instance().BlackToNormal(TempPanel, true);   }
                    }
                }
            }
        }
    }

    //-----------------------<< 显示提示面板(Tips) >>-------------------------------------------------------------------------------------
    /// <summary> 显示介绍panel 生命周期：按下会弹出，抬起会消失，所以无需手动关闭
    public Configs_CheckPointData       CheckPointD;                                                                        /// 关卡数据
    Dictionary<GameObject, GameObject>  TipsPanelDic        = new Dictionary<GameObject, GameObject>();                     /// 提示面板字典

    public void                 ShowTipsPanel               ( GameObject  obj,    bool    isOver,     int id,
                                                              ItemType    type,   Configs_CheckPointData  checkPointData,
                                                              bool        isLevelBox = false)                               // 显示提示面板(Tips)   
    {
        if(!isOver)
        {
            CancelInvoke("Indei");
            if(TipsPanelDic.ContainsKey(obj) && TipsPanelDic[obj] != null)
            {
                GameObject.DestroyImmediate(TipsPanelDic[obj]);
                TipsPanelDic.Remove(obj);
            }
            return;
        }

        x = obj.transform.position.x;
        y = obj.transform.position.y;
        V = obj.GetComponent<BoxCollider>().size;
        ID = id;
        CheckPointD = checkPointData;
        Type = (int)type;

        UnityEngine.Object TempPanel = Util.Load(UIPanelConfig.TipsPanel);
        GameObject TPPanel = Instantiate(TempPanel) as GameObject;
        TPPanel.name = "tips";                                                                                      // 面板名称
        TPPanel.transform.parent = gameObject.transform;                                                            // 指定父级
        TPPanel.transform.localScale = Vector3.one;                                                                 // 局部缩放参数
        TPPanel.transform.localEulerAngles = Vector3.zero;                                                          // 局部角度参数
        TPPanel.transform.position = new Vector3(x, y, 0);                                                          // 局部位移参数
        TPPanel.GetComponent<TipsPanelView>().SetData(obj, V.x, (int)V.y, (int)ID, (int)Type, CheckPointD);         // 获取提示面板视图组件

        if(isLevelBox)
        {
            TPPanel.GetComponent<TipsPanelView>().boxTips = true; 
        }
        if(!TipsPanelDic.ContainsKey(obj) || TipsPanelDic[obj] == null)
        {
            TipsPanelDic.Add(obj, TPPanel);
        }
        else
        {
            GameObject.DestroyImmediate(TipsPanelDic[obj]);
            TipsPanelDic[obj] = TPPanel;
        }
        TempPanel = null;

    }
    public void                 ShowTipsPanel               ( GameObject  obj,    bool    isOver,     int id, 
                                                              ItemType    type,   Configs_CheckPointData  checkPointData, 
                                                              float       pos_z )                                           // 显示提示面板(Tips)重载
    {
        if (!isOver)
        {
            CancelInvoke("Indei");
            if (TipsPanelDic.ContainsKey(obj) && TipsPanelDic[obj] != null)
            {
                GameObject.DestroyImmediate(TipsPanelDic[obj]);
                TipsPanelDic.Remove(obj);
            }
            return;
        }

        x = obj.transform.position.x;
        y = obj.transform.position.y;
        V = obj.GetComponent<BoxCollider>().size;
        ID = id;
        CheckPointD = checkPointData;
        Type = (int)type;

        UnityEngine.Object TempPanel = Util.Load(UIPanelConfig.TipsPanel);
        GameObject TPPanel = Instantiate(TempPanel) as GameObject;
        TPPanel.name = "tips";                                                                                      // 面板名称
        TPPanel.transform.parent = gameObject.transform;                                                            // 指定父级
        TPPanel.transform.localScale = Vector3.one;                                                                 // 局部缩放参数
        TPPanel.transform.localEulerAngles = Vector3.zero;                                                          // 局部角度参数
        TPPanel.transform.position = new Vector3(x, y, 0);                                                          // 局部位移参数
        TPPanel.GetComponent<TipsPanelView>().SetData(obj, V.x, (int)V.y, (int)ID, (int)Type, CheckPointD);         // 获取提示面板视图组件

 
        if (!TipsPanelDic.ContainsKey(obj) || TipsPanelDic[obj] == null)
        {
            TipsPanelDic.Add(obj, TPPanel);
        }
        else
        {
            GameObject.DestroyImmediate(TipsPanelDic[obj]);
            TipsPanelDic[obj] = TPPanel;
        }
        TempPanel = null;
        Invoke("ResetPanel", 0.1f);
    }
    void                        ResetPanel                  ( GameObject  panel,  float   pos_z)                            
    {
        if(panel != null)
        {
            panel.transform.position = new Vector3(panel.transform.position.x, panel.transform.position.y, pos_z);
            panel = null;
        }
    }


                                                                                                                            ///<| ShowReward:展示奖励界面 |>
    public void                 ShowAwardView               ( List<int> giftIDList, List<int> giftNum = null)               // 展示奖励视图             
    {
        Dictionary<CurrencyType, int> _CurrencyDic = new Dictionary<CurrencyType, int>();
        List<AwardProp> _AwardPropList = new List<AwardProp>();             // 奖励道具列表

        List<int> _PropTypeList = new List<int>();                          // 道具类型列表 
        List<int> _PropIDList   = new List<int>();                          // 道具ID列表
        List<int> _PropNumList  = new List<int>();                          // 道具数量列表

        for( int i = 0; i < giftIDList.Count;i++)
        {
            Configs_GiftData _GiftData = Configs_Gift.sInstance.GetGiftDataByGiftID(giftIDList[i]);
            for(int i_1 = 0;i_1 < _GiftData.PropType.Count;i_1++ )
            {
                _PropTypeList.Add(_GiftData.PropType[i_1]);
            }
            for(int i_2 = 0;i_2 < _GiftData.PropID.Count;i_2++)
            {
                _PropIDList.Add(_GiftData.PropID[i_2]);
            }
            for(int i_3 = 0; i_3 < _GiftData.Number.Count;i_3++)
            {
                if( giftNum != null && giftNum.Count == giftIDList.Count )
                {
                    _PropNumList.Add(_GiftData.Number[i_3] * giftNum[i]);
                }
                else
                {
                    _PropNumList.Add(_GiftData.Number[i_3]);
                }
            }
        }
        for( int i = 0;i < _PropTypeList.Count;i++)
        {
            ItemType _propType = ItemType.None;
            switch(_propType)
            {
                case ItemType.equip:                                   // 1.装备
                case ItemType.equipFragment:                           // 2.装备碎片
                case ItemType.scroll:                                  // 3.卷轴
                case ItemType.scrollFragment:                          // 4.卷轴碎片
                case ItemType.soul:                                    // 5.魂石

                case ItemType.coinsprop:                               // 6.金币道具
                case ItemType.heroExpProp:                             // 7.英雄经验道具
                case ItemType.medalExpProp:                            // 8.勋章经验道具
                case ItemType.wingExpProp:                             // 9.翅膀经验道具
                case ItemType.ticket:                                  // 10.扫荡券
                        
                case ItemType.wing:                                    // 11.翅膀
                case ItemType.staminaProp:                             // 12.体力道具
                case ItemType.protectedstone:                          // 13.保护石
                case ItemType.jinjiestone:                             // 14.进阶石
                case ItemType.mercExpProp:                             // 15.佣兵道具

                case ItemType.SkillProp:                               // 16.技能点道具
                case ItemType.soulbag:                                 // 17.魂石包
                case ItemType.diamondsbag:                             // 18.钻石包

                case ItemType.hero:                                    // 105.英雄
                    AwardProp _AwardProp = new AwardProp();
                    _AwardProp.propCount = _PropNumList[i];
                    _AwardProp.propID = _PropIDList[i];
                    _AwardProp.propType = _propType;
                    _AwardPropList.Add(_AwardProp);
                    break;
                case ItemType.diamonds:                                // 101.钻石
                case ItemType.coins:                                   // 102.金币
                case ItemType.stamina:                                 // 103.体力
                case ItemType.playerExp:                               // 104.玩家经验

                case ItemType.heroExp:                                 // 106.英雄经验
                case ItemType.SecretTowerCoins:                        // 107.秘境塔金币
                case ItemType.JJCCoins:                                // 108.竞技场金币
                case ItemType.ParadiseRoadCoins:                       // 109.天堂之路金币
                case ItemType.WingExp:                                 // 110.天使遗羽(翅膀升级)
                    _CurrencyDic.Add((CurrencyType)_PropTypeList[i], _PropNumList[i]);
                    break;
            }
        }
        ShowAwardPanel(_CurrencyDic, _AwardPropList, "确定");
    }
    public void                 ShowAwardPanel              ( Dictionary<CurrencyType, int>    currencyDic,                 // 显示奖励面板
                                                              List< AwardProp > awardPropList, string BtnText)                                         
    {
        GameObject TempRDPanel = (GameObject)Util.Load(UIPanelConfig.RewardAllPanel);           /// 所有奖励面板
        if(TempRDPanel.activeSelf)                                                      
        {
            TempRDPanel.SetActive(false);
        }
        GameObject RAPaenl = Instantiate(TempRDPanel) as GameObject;
        UnityEngine.Object TempMKPanel = Util.Load(UIPanelConfig.MaskPanel);
        GameObject MaskPObj = Instantiate(TempMKPanel) as GameObject;
        MaskPObj.SetActive(true);

        RAPaenl.SetActive(false);                                                               /// 设置奖励面板参数
        RAPaenl.name = "RewardAllPanel";
        RAPaenl.transform.parent = gameObject.transform;
        RAPaenl.transform.localScale = Vector3.one;
        RAPaenl.transform.localEulerAngles = Vector3.zero;
        RAPaenl.transform.localPosition = Vector3.zero;

        MaskPObj.transform.parent = gameObject.transform;                                       /// 设置遮罩面板参数
        MaskPObj.transform.localScale = Vector3.one;
        MaskPObj.transform.localEulerAngles = Vector3.zero;
        MaskPObj.transform.localPosition = Vector3.zero;

        RewardAllView RDAllview = RAPaenl.GetComponent<RewardAllView>();                        /// 奖励面板添加视图组件
        RDAllview.allPanel.SetActive(false);
        RDAllview.currencyPanel.SetActive(false);
        RDAllview.propPanel.SetActive(false);
        RDAllview.sureLabel.text = BtnText;
        RDAllview.sureLabel2.text = BtnText;
        RDAllview.sureLabel3.text = BtnText;
        TempRDPanel = null;                                                                     /// 释放临时面板对象
        TempMKPanel = null;

        if(awardPropList.Count == 0)                                                            /// 货币领取界面               
        {
            if(currencyDic.Count <= 3)
            {
                RDAllview.allPanel.SetActive(false);
                RDAllview.currencyPanel.SetActive(true);
                RDAllview.propPanel.SetActive(false);

                CurrencyCenter(RDAllview.listGridCurrency.gameObject, currencyDic.Count);       // 货币奖励居中显示(货币类型)
                                                                                                // 加载货币信息
                GameObject TempObj = (GameObject)Util.Load(UIPanelConfig.RewardCoinsItem);
                foreach (KeyValuePair<CurrencyType,int> KVP in currencyDic)
                {
                    GameObject Item = Instantiate(TempObj) as GameObject;
                    Item.name = UIPanelName.RewardCoinsItemName;
                    Item.GetComponent<UIDragScrollView>().enabled = false;                      // touch_触摸或滚动视图
                    RDAllview.listGridCurrency.AddChild(Item.transform);

                    Item.transform.localPosition = Vector3.zero;
                    Item.transform.localEulerAngles = Vector3.zero;
                    Item.transform.localScale = Vector3.one;
                    RewardCoinsItemView RCIView = Item.GetComponent<RewardCoinsItemView>();

                    switch((int)KVP.Key)
                    {
                        case (int)ItemType.diamonds:                                            // 钻石
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.diamonds);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.coins:                                               // 金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.coins);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.stamina:                                             // 体力
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.stamina);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.playerExp:                                           // 玩家经验
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.playerExp);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;

                        case (int)ItemType.SecretTowerCoins:                                    // 秘境塔金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.SecretTowerCoins);
                            RCIView.CurrencyIcon.MakePixelPerfect();
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.JJCCoins:                                            // 竞技场金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.JJCCoins);
                            RCIView.CurrencyIcon.MakePixelPerfect();
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.ParadiseRoadCoins:                                   // 天堂之路金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.ParadiseRoadCoins);
                            RCIView.CurrencyIcon.MakePixelPerfect();
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        default:
                            break;
                    }
                    RDAllview.listGridCurrency.repositionNow = true;                            // 下次重新定位
                    RDAllview.listGridCurrency.Reposition();                                    // 重新定位
                }
                TempObj = null;                                                                 // 释放临时对象空间
            }
            else
            {
                RDAllview.allPanel.SetActive(true);
                RDAllview.currencyPanel.SetActive(false);
                RDAllview.propPanel.SetActive(false);
                RDAllview.listGrid1.gameObject.SetActive(true);

                SetGrid2Pos(RDAllview.listGrid2.gameObject, currencyDic.Count, awardPropList.Count);
                                                                                                // 加载货币信息
                GameObject TempObj = (GameObject)Util.Load(UIPanelConfig.RewardCoinsItem);
                foreach (KeyValuePair<CurrencyType, int> KVP in currencyDic)
                {
                    GameObject Item = Instantiate(TempObj) as GameObject;
                    Item.name = UIPanelName.RewardCoinsItemName;
                    Item.GetComponent<UIDragScrollView>().enabled = false;                      // touch_触摸或滚动视图
                    RDAllview.listGridCurrency.AddChild(Item.transform);

                    Item.transform.localPosition = Vector3.zero;
                    Item.transform.localEulerAngles = Vector3.zero;
                    Item.transform.localScale = Vector3.one;
                    RewardCoinsItemView RCIView = Item.GetComponent<RewardCoinsItemView>();

                    switch ((int)KVP.Key)
                    {
                        case (int)ItemType.diamonds:                                            // 钻石
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.diamonds);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.coins:                                               // 金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.coins);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.stamina:                                             // 体力
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.stamina);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.playerExp:                                           // 玩家经验
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.playerExp);
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;

                        case (int)ItemType.SecretTowerCoins:                                    // 秘境塔金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.SecretTowerCoins);
                            RCIView.CurrencyIcon.MakePixelPerfect();
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.JJCCoins:                                            // 竞技场金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.JJCCoins);
                            RCIView.CurrencyIcon.MakePixelPerfect();
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        case (int)ItemType.ParadiseRoadCoins:                                   // 天堂之路金币
                            RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.ParadiseRoadCoins);
                            RCIView.CurrencyIcon.MakePixelPerfect();
                            RCIView.CurrecnyCount.text = KVP.Value.ToString();
                            break;
                        default:
                            break;
                    }
                    RDAllview.listGridCurrency.repositionNow = true;                            // 下次重新定位
                    RDAllview.listGridCurrency.Reposition();                                    // 重新定位
                }
                TempObj = null;                                                                 // 释放临时对象空间

            }

        }

        else if (awardPropList.Count <= 4 && currencyDic.Count == 0)                            /// 道具领取界面               
        {
            RDAllview.allPanel.SetActive(false);
            RDAllview.currencyPanel.SetActive(false);
            RDAllview.propPanel.SetActive(true);
            PropCenter(RDAllview.listGridProp.gameObject, awardPropList.Count);                 // 道具奖励居中显示
            UIGrid PropGrid = RDAllview.listGridProp.GetComponent<UIGrid>();

            GameObject TempObj = (GameObject)Util.Load(UIPanelConfig.RewardPropItem);
            foreach(AwardProp Prop_Key in awardPropList )
            {
                GameObject RPPanel = Instantiate(TempObj) as GameObject;    // 实例
                RPPanel.name = UIPanelName.RewardPropItemName;                  // 面板名称
                RPPanel.GetComponent<UIDragScrollView>().enabled = false;   // touch_触摸或滚动视图
                PropGrid.AddChild(RPPanel.transform);                       // 道具格子

                RPPanel.transform.localEulerAngles = Vector3.zero;          // 设置角度参数
                RPPanel.transform.localPosition = Vector3.zero;             // 设置位置参数
                RPPanel.transform.localScale = Vector3.one;                 // 设置缩放参数
                RewardPropItemView PropView = RPPanel.GetComponent<RewardPropItemView>();

                PropView.propID = Prop_Key.propID;
                PropView.propType = Prop_Key.propType;
                switch(Prop_Key.propType)
                {
                    case ItemType.equip:                                    // 装备                                                                                      
                    case ItemType.scroll:                                   // 卷轴                    
                        {
                            Dictionary<int, Configs_EquipData> EquipDataDic = Configs_Equip.sInstance.mEquipDatas;
                            Configs_EquipData CEquipData = EquipDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);       // 装备框图集       
                            PropView.propIconFrame.spriteName = PropToSpriteName(CEquipData.EquipQuality );     // 装备框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.EquipIcon84);           // 装备Icon图集
                            PropView.propIcon.spriteName = CEquipData.EquipIcon_84;                             // 装备Icon图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 装备数量
                        }
                        break;
                    case ItemType.equipFragment:                            // 装备碎片
                    case ItemType.scrollFragment:                           // 卷轴碎片                 
                        {
                            Dictionary<int, Configs_FragmentData> FragDataDic = Configs_Fragment.sInstance.mFragmentDatas;
                            Configs_FragmentData CFragData = FragDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);       // 碎片框图集       
                            PropView.propIconFrame.spriteName = FragToSpriteName(CFragData.FragmentQuality);    // 碎片框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.EquipIcon84);           // 碎片Icon图集
                            PropView.propIcon.spriteName = CFragData.FragmentIcon_84;                           // 碎片Icon图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 碎片数量
                        }
                        break;
                    case ItemType.soul:                                     // 英雄魂石                 
                        {
                            Dictionary<int, Configs_SoulData> SoulDataDic = Configs_Soul.sInstance.mSoulDatas;
                            Configs_SoulData CSoulData = SoulDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);     // 魂石框图集
                            PropView.propIconFrame.spriteName = GetSpriteName(SpriteType.SoulFrame);            // 魂石框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);          // 魂石Icon图集
                            PropView.propIcon.spriteName = CSoulData.head84;                                    // 魂石Icon图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 魂石数量
                        }
                        break;

                    case ItemType.coinsprop:                                
                    case ItemType.heroExpProp:
                    case ItemType.medalExpProp:
                    case ItemType.wingExpProp:
                    case ItemType.ticket:

                    case ItemType.wing:
                    case ItemType.staminaProp:
                    case ItemType.protectedstone:
                    case ItemType.jinjiestone:
                    case ItemType.mercExpProp:

                    case ItemType.SkillProp:
                    case ItemType.soulbag:
                    case ItemType.diamondsbag:                              // 所有道具(Prop 6 - 18)    
                        {
                            Dictionary<int, Configs_PropData> PropDataDic = Configs_Prop.sInstance.mPropDatas;
                            Configs_PropData CPropData = PropDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);       // 魂道具框图集
                            PropView.propIconFrame.spriteName = PropToSpriteName(CPropData.PropQuality);        // 道具框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);            // 道具ICON图集
                            PropView.propIcon.spriteName = CPropData.PropIcon84;                                // 道具ICON图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 道具数量
                        }
                        break;
                    case ItemType.hero:                                     // 英雄
                        {
                            Dictionary<int, Configs_HeroData> HeroDataDic = Configs_Hero.sInstance.mHeroDatas;
                            Configs_HeroData CHeroData = HeroDataDic[Prop_Key.propID];
                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);
                            PropView.propIconFrame.spriteName = GetSpriteName(SpriteType.HeroFrame);
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);
                            PropView.propIcon.spriteName = CHeroData.head84;
                            PropView.propCount.text = Prop_Key.propCount.ToString();
                        }
                        break;
                }
                PropGrid.repositionNow = true;
                PropGrid.Reposition();
            }
            TempObj = null;
        }
                                                                                                
        else                                                                                    /// 所有物品领取面板           
        {
            RDAllview.allPanel.SetActive(true);
            RDAllview.currencyPanel.SetActive(false);
            RDAllview.propPanel.SetActive(false);

            if(currencyDic.Count == 0)
            {
                RDAllview.listGrid1.gameObject.SetActive(false);
                SetGrid2Pos(RDAllview.listGrid2.gameObject, currencyDic.Count, awardPropList.Count);
            }
            else
            {
                RDAllview.listGrid1.gameObject.SetActive(true);
                switch(currencyDic.Count)
                {
                    case 1:
                        RDAllview.listGrid1.gameObject.transform.localPosition = new Vector3
                            (-23.7f, RDAllview.listGrid1.gameObject.transform.localPosition.y, RDAllview.listGrid1.gameObject.transform.localPosition.z);
                        break;
                    case 2:
                        RDAllview.listGrid1.gameObject.transform.localPosition = new Vector3
                            (-98.3f, RDAllview.listGrid1.gameObject.transform.localPosition.y, RDAllview.listGrid1.gameObject.transform.localPosition.z);
                        break;
                    case 3:
                        RDAllview.listGrid1.gameObject.transform.localPosition = new Vector3
                            (-98.3f, RDAllview.listGrid1.gameObject.transform.localPosition.y, RDAllview.listGrid1.gameObject.transform.localPosition.z);
                        break;
                }
                SetGrid2Pos(RDAllview.listGrid2.gameObject, currencyDic.Count, awardPropList.Count);
            }
            GameObject TempRCPanel = (GameObject)Util.Load(UIPanelConfig.RewardCoinsItem);
            foreach(KeyValuePair<CurrencyType,int> key in currencyDic)
            {
                GameObject Item = Instantiate(TempRCPanel) as GameObject;
                Item.name = UIPanelName.RewardCoinsItemName;
                Item.GetComponent<UIDragScrollView>().enabled = false;                      // touch_触摸或滚动视图
                RDAllview.listGridCurrency.AddChild(Item.transform);

                Item.transform.localPosition = Vector3.zero;
                Item.transform.localEulerAngles = Vector3.zero;
                Item.transform.localScale = Vector3.one;
                RewardCoinsItemView RCIView = Item.GetComponent<RewardCoinsItemView>();

                switch ((int)key.Key)
                {
                    case (int)ItemType.diamonds:                                            // 钻石
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.diamonds);
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;
                    case (int)ItemType.coins:                                               // 金币
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.coins);
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;
                    case (int)ItemType.stamina:                                             // 体力
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.stamina);
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;
                    case (int)ItemType.playerExp:                                           // 玩家经验
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.playerExp);
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;

                    case (int)ItemType.SecretTowerCoins:                                    // 秘境塔金币
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.SecretTowerCoins);
                        RCIView.CurrencyIcon.MakePixelPerfect();
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;
                    case (int)ItemType.JJCCoins:                                            // 竞技场金币
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.JJCCoins);
                        RCIView.CurrencyIcon.MakePixelPerfect();
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;
                    case (int)ItemType.ParadiseRoadCoins:                                   // 天堂之路金币
                        RCIView.CurrencyIcon.spriteName = this.GetItemTypeSpriteName(ItemType.ParadiseRoadCoins);
                        RCIView.CurrencyIcon.MakePixelPerfect();
                        RCIView.CurrecnyCount.text = key.Value.ToString();
                        break;
                    default:
                        break;
                }
                RDAllview.listGridCurrency.repositionNow = true;                            // 下次重新定位
                RDAllview.listGridCurrency.Reposition();                                    // 重新定位
            }
            TempRCPanel = null;

            UIGrid PropGrid = RDAllview.listGridProp.GetComponent<UIGrid>();

            GameObject TempObj = (GameObject)Util.Load(UIPanelConfig.RewardPropItem);
            foreach (AwardProp Prop_Key in awardPropList)
            {
                GameObject RPPanel = Instantiate(TempObj) as GameObject;    // 实例
                RPPanel.name = UIPanelName.RewardPropItemName;              // 面板名称
                RPPanel.GetComponent<UIDragScrollView>().enabled = false;   // touch_触摸或滚动视图
                PropGrid.AddChild(RPPanel.transform);                       // 道具格子

                RPPanel.transform.localEulerAngles = Vector3.zero;          // 设置角度参数
                RPPanel.transform.localPosition = Vector3.zero;             // 设置位置参数
                RPPanel.transform.localScale = Vector3.one;                 // 设置缩放参数
                RewardPropItemView PropView = RPPanel.GetComponent<RewardPropItemView>();

                PropView.propID = Prop_Key.propID;
                PropView.propType = Prop_Key.propType;
                switch (Prop_Key.propType)
                {
                    case ItemType.equip:                                    // 装备                                                                                      
                    case ItemType.scroll:                                   // 卷轴                    
                        {
                            Dictionary<int, Configs_EquipData> EquipDataDic = Configs_Equip.sInstance.mEquipDatas;
                            Configs_EquipData CEquipData = EquipDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);       // 装备框图集       
                            PropView.propIconFrame.spriteName = PropToSpriteName(CEquipData.EquipQuality);      // 装备框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.EquipIcon84);           // 装备Icon图集
                            PropView.propIcon.spriteName = CEquipData.EquipIcon_84;                             // 装备Icon图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 装备数量
                        }
                        break;
                    case ItemType.equipFragment:                            // 装备碎片
                    case ItemType.scrollFragment:                           // 卷轴碎片                 
                        {
                            Dictionary<int, Configs_FragmentData> FragDataDic = Configs_Fragment.sInstance.mFragmentDatas;
                            Configs_FragmentData CFragData = FragDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);       // 碎片框图集       
                            PropView.propIconFrame.spriteName = FragToSpriteName(CFragData.FragmentQuality);    // 碎片框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.EquipIcon84);           // 碎片Icon图集
                            PropView.propIcon.spriteName = CFragData.FragmentIcon_84;                           // 碎片Icon图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 碎片数量
                        }
                        break;
                    case ItemType.soul:                                     // 英雄魂石                 
                        {
                            Dictionary<int, Configs_SoulData> SoulDataDic = Configs_Soul.sInstance.mSoulDatas;
                            Configs_SoulData CSoulData = SoulDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);     // 魂石框图集
                            PropView.propIconFrame.spriteName = GetSpriteName(SpriteType.SoulFrame);            // 魂石框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);          // 魂石Icon图集
                            PropView.propIcon.spriteName = CSoulData.head84;                                    // 魂石Icon图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 魂石数量
                        }
                        break;

                    case ItemType.coinsprop:
                    case ItemType.heroExpProp:
                    case ItemType.medalExpProp:
                    case ItemType.wingExpProp:
                    case ItemType.ticket:

                    case ItemType.wing:
                    case ItemType.staminaProp:
                    case ItemType.protectedstone:
                    case ItemType.jinjiestone:
                    case ItemType.mercExpProp:

                    case ItemType.SkillProp:
                    case ItemType.soulbag:
                    case ItemType.diamondsbag:                              // 所有道具(Prop 6 - 18)    
                        {
                            Dictionary<int, Configs_PropData> PropDataDic = Configs_Prop.sInstance.mPropDatas;
                            Configs_PropData CPropData = PropDataDic[Prop_Key.propID];

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);       // 道具框图集
                            PropView.propIconFrame.spriteName = PropToSpriteName(CPropData.PropQuality);        // 道具框图集名称
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);            // 道具ICON图集
                            PropView.propIcon.spriteName = CPropData.PropIcon84;                                // 道具ICON图集名称
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 道具数量
                        }
                        break;
                    case ItemType.hero:                                     // 英雄
                        {
                            Dictionary<int, Configs_HeroData> HeroDataDic = Configs_Hero.sInstance.mHeroDatas;  
                            Configs_HeroData CHeroData = HeroDataDic[Prop_Key.propID];                          

                            PropView.propIconFrame.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);     // 英雄框图集
                            PropView.propIconFrame.spriteName = GetSpriteName(SpriteType.HeroFrame);            // 英雄框图集标签名
                            PropView.propIcon.atlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);          // 英雄图标图集
                            PropView.propIcon.spriteName = CHeroData.head84;                                    // 英雄图标图集标签名
                            PropView.propCount.text = Prop_Key.propCount.ToString();                            // 数量
                        }
                        break;
                }
                PropGrid.repositionNow = true;
                PropGrid.Reposition();
            }
            TempObj = null;

        }
        _AwardPanelShow.Add(RAPaenl);
        if(!IsInvoking("SetAwardShow"))                                                         /// 是否挂起该方法             
        {       
            InvokeRepeating("SetAwardShow", 0.3f, 0.3f);                                        // 以时间设置调用方法
        }
        Destroy(MaskPObj, 0.31f);
    }

    #region================================================||   ShowRewardPrivate -- 展示奖励私有模块< 函数_声明 >      ||<FourNode>================================================
    List<GameObject>            _AwardPanelShow             = new List < GameObject >();

    void                        SetAwardshow()                                                                              // 设置奖励显示             
    {
        if(_AwardPanelShow.Count > 0)
        {
            _AwardPanelShow[0].SetActive(true);
            _AwardPanelShow.Remove(_AwardPanelShow[0]);
        }
        else
        {
            CancelInvoke("SetAwardShow");
        }
    }
    void                        SetGrid2Pos                 ( GameObject target, int currencyNum,int awardPropNum )         // 第二个格子位置           
    {
        if(currencyNum == 0)
        {
            LeftRightCenter(target, awardPropNum);
            UpDownCenter(target, awardPropNum);
            return;
        }
        if(currencyNum > 0 && currencyNum <= 3)
        {
            LeftRightCenter(target, awardPropNum);
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, 4.3f, target.transform.localPosition.z);
            return;
        }
        if(currencyNum > 3 && currencyNum <= 6)
        {
            LeftRightCenter(target, awardPropNum);
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, -18.6f, target.transform.localPosition.z);
        }
        if(currencyNum > 6)
        {
            LeftRightCenter(target, awardPropNum);
            float theX = (float)currencyNum / 3f;
            int theY = (int)theX;
            float theZ = (float)theY;
            if(theZ < theX)
            {
                theY += 1;
            }
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, (-18.6f - 36f *(float)(theY - 2)), target.transform.localPosition.z);
            return;
        }
    }
    void                        UpDownCenter                ( GameObject target,          int num)                          // 上下居中                 
    {
        if(target.GetComponent<UIGrid>() != null)
        {
            if(num == 0)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
                target.transform.localPosition = new Vector3(target.transform.localPosition.x, 17.6f, target.transform.localPosition.z);
            }
        }
    }
    void                        LeftRightCenter             ( GameObject target,          int awardPropNum)                 // 奖励货币居中             
    {
        if(target.GetComponent<UIGrid>() != null)
        {
            switch(awardPropNum)
            {
                case 0:
                    target.SetActive(true);
                    break;
                case 1:
                    target.SetActive(true);
                    target.transform.localPosition = new Vector3(6f, target.transform.localPosition.y, target.transform.localPosition.z);
                    break;
                case 2:
                    target.SetActive(true);
                    target.transform.localPosition = new Vector3(-44f, target.transform.localPosition.y, target.transform.localPosition.z);
                    break;
                case 3:
                    target.SetActive(true);
                    target.transform.localPosition = new Vector3(-94f, target.transform.localPosition.y, target.transform.localPosition.z);
                    break;
                case 4:
                    target.SetActive(true);
                    target.transform.localPosition = new Vector3(-143f, target.transform.localPosition.y, target.transform.localPosition.z);
                    break;
                default:
                    target.SetActive(true);
                    target.transform.localPosition = new Vector3(-143f, target.transform.localPosition.y, target.transform.localPosition.z);
                    break;
            }
        }
    }
    void                        PropCenter                  ( GameObject propGrid,        int awardPropNum)                 // 道具奖励居中显示         
    {
        switch(awardPropNum)
        {
            case 1:
                propGrid.transform.localPosition = new Vector3(6.2f, propGrid.transform.localPosition.y, propGrid.transform.localPosition.z);
                break;
            case 2:
                propGrid.transform.localPosition = new Vector3(-44.2f, propGrid.transform.localPosition.y, propGrid.transform.localPosition.z);
                break;
            case 3:
                propGrid.transform.localPosition = new Vector3(-93.5f, propGrid.transform.localPosition.y, propGrid.transform.localPosition.z);
                break;
            case 4:
                propGrid.transform.localPosition = new Vector3(-142.3f, propGrid.transform.localPosition.y, propGrid.transform.localPosition.z);
                break;
        }
    }
    void                        CurrencyCenter              ( GameObject currencyGrid,    int awardCurrencNum)              // 货币奖励居中显示         
    {
        switch(awardCurrencNum)
        {
            case 1:
                currencyGrid.transform.localPosition = new Vector3(-29.2f, currencyGrid.transform.localPosition.y, currencyGrid.transform.localPosition.z);
                break;
            case 2:
                currencyGrid.transform.localPosition = new Vector3(-83.1f, currencyGrid.transform.localPosition.y, currencyGrid.transform.localPosition.z);
                break;
            case 3:
                currencyGrid.transform.localPosition = new Vector3(-133.1f, currencyGrid.transform.localPosition.y, currencyGrid.transform.localPosition.z);
                break;
        }
    }
    #endregion

    //-----------------------<< 获取图集标签名(spriteName) >>----------------------------------------------------------------------------

    public string               GetSpriteName               ( SpriteType spriteType )                                       // 获取图集名称(根据图集类型)   
    {
        switch (spriteType)
        {
            case SpriteType.PF_White:   return "daoju-bai-84";                       // 道具白框
            case SpriteType.PF_Green:   return "daoju-lv-83";                        // 道具绿框
            case SpriteType.PF_Blue:    return "daoju-lan-84";                       // 道具蓝框
            case SpriteType.PF_Purple:  return "daoju-zi-83";                        // 道具紫框
            case SpriteType.PF_Gold:    return "daoju-jin-84";                       // 道具金框

            // < FragmentFrame 碎片品质边框 >
            case SpriteType.FF_White:   return "suipian-bai-84";                     // 碎片白框
            case SpriteType.FF_Green:   return "suipian-lv-84";                      // 碎片绿框
            case SpriteType.FF_Blue:    return "suipian-lan-84";                     // 碎片蓝框
            case SpriteType.FF_Purple:  return "suipian-zi-84";                      // 碎片紫框
            case SpriteType.FF_Gold:    return "suipian-jin-84";                     // 碎片金框

            // < HeroFrame 英雄品质边框 >
            case SpriteType.HF_White:   return "touxiang-bai-84";                    // 英雄白框
            case SpriteType.HF_Green:   return "touxiang-lv-84";                     // 英雄绿框
            case SpriteType.HF_Green1:  return "touxiang-lv+1-84";                   // 英雄绿框+1
            case SpriteType.HF_Blue:    return "touxiang-lan-84";                    // 英雄蓝框
            case SpriteType.HF_Blue1:   return "touxiang-lan+1-84";                  // 英雄篮框+1
            case SpriteType.HF_Blue2:   return "touxiang-lan+2-84";                  // 英雄篮框+2
            case SpriteType.HF_Purple:  return "touxiang-zi-84";                     // 英雄紫框
            case SpriteType.HF_Purple1: return "touxiang-zi+1-84";                   // 英雄紫框+1
            case SpriteType.HF_Purple2: return "touxiang-zi+2-84";                   // 英雄紫框+2
            case SpriteType.HF_Purple3: return "touxiang-zi+3-84";                   // 英雄紫框+3
            case SpriteType.HF_Gold:    return "touxiang-jin-84";                    // 英雄金框

            case SpriteType.Currency_Diamond:   return "prop_zuanshi_84";            // 钻石图集名
            case SpriteType.Currency_Coins:     return "currency_jinbi-da";          // 金币图集名称
            case SpriteType.SoulFrame:          return "hunshikuang-84";           // 魂石框
            case SpriteType.HeroFrame:          return "touxiang-bai-84";            // 英雄框
            default:
                Debug.LogError("Unknown spriteName" + spriteType.ToString());
                return "daoju-bai-84";
        }
    }
    public string               PropToSpriteName            ( int equipQuality )                                            // 道具品质获取外框品质图集名称  
    {
        switch (equipQuality)
        {
            case (int)ItemQuality.White: return GetSpriteName(SpriteType.PF_White);
            case (int)ItemQuality.Green: return GetSpriteName(SpriteType.PF_Green);
            case (int)ItemQuality.Blue: return GetSpriteName(SpriteType.PF_Blue);
            case (int)ItemQuality.Puru: return GetSpriteName(SpriteType.PF_Purple);
            case (int)ItemQuality.Gold: return GetSpriteName(SpriteType.PF_Gold);
            default:
                Debug.Log("EquipQuality is Error!");
                return GetSpriteName(SpriteType.PF_White);
        }
    }
    public string               FragToSpriteName            ( int fragQuality )                                             // 碎片品质获取图集名称         
    {
        switch (fragQuality)
        {
            case (int)ItemQuality.White: return GetSpriteName(SpriteType.FF_White);
            case (int)ItemQuality.Green: return GetSpriteName(SpriteType.FF_Green);
            case (int)ItemQuality.Blue: return GetSpriteName(SpriteType.FF_Blue);
            case (int)ItemQuality.Puru: return GetSpriteName(SpriteType.FF_Purple);
            case (int)ItemQuality.Gold: return GetSpriteName(SpriteType.FF_Gold);
            default:
                Debug.Log("FragQuality is Error!");
                return GetSpriteName(SpriteType.FF_White);
        }
    }
    public string               GetItemTypeSpriteName       ( ItemType itemType )                                           // 获取物品类型的SpriteName     
    {
        switch(itemType)
        {
            case ItemType.diamonds:         return "currency_daimond";
            case ItemType.coins:            return "currency_coins";
            case ItemType.stamina:          return "currency_stamina";
            case ItemType.playerExp:        return "currency_Playerexp";
            case ItemType.SecretTowerCoins: return "currency_SecretTowerCoins";
            case ItemType.JJCCoins:         return "currency_JJCConins";
            case ItemType.ParadiseRoadCoins:return "currency_paradiseroadCoins";
            case ItemType.WingExp:          return "currency_wingexp";
            default:
                return "";
        }
    }
    public string               HeroQualitySpriteName       ( HeroQuality heroQ )                                           // 根据英雄品质获取图集标签名    
    {
        switch (heroQ)
        {
            case HeroQuality.White:
                return "touxiang-bai-84";
            case HeroQuality.Green:
                return "touxiang-lv-84";
            case HeroQuality.Green1:
                return "touxiang-lv+1-84";
            case HeroQuality.Blue:
                return "touxiang-lan-84";
            case HeroQuality.Blue1:
                return "touxiang-lan+1-84";
            case HeroQuality.Blue2:
                return "touxiang-lan+2-84";
            case HeroQuality.Purple:
                return "touxiang-zi-84";
            case HeroQuality.Purple1:
                return "touxiang-zi+1-84";
            case HeroQuality.Purple2:
                return "touxiang-zi+2-84";
            case HeroQuality.Purple3:
                return "touxiang-zi+3-84";
            case HeroQuality.Gold:
                return "touxiang-jin-84";
            default:
                return null;
        }
    }
    public string               GetElementTypeSpriteName    ( ElementType elementType )                                     // 根据元素类型获取图集标签名    
    {
        switch (elementType)
        {
            case ElementType.Ice: return "bing";
            case ElementType.Fire: return "huo";
            case ElementType.Thunder: return "lei";
            default:
                Debug.LogError("Unknown spriteName" + elementType.ToString());
                return "bing";
        }
    }
}
#endregion
public class                        PanelObj                                            // 面板对象类型                 
{
    public                      PanelObj(string name, GameObject obj)
    {
        this.name               = name;
        this.gameObj            = obj;
    }
    public string               name;                                               // 面板名称
    public GameObject           gameObj;                                            // 面板对象

}
public enum                         SpriteType                                          // 图集名称管理                 
{
                                                        /// <| PropFrame 道具品质边框 >
    PF_White                    = 101,               // 道具白框
    PF_Green                    = 102,               // 道具绿框
    PF_Blue                     = 103,               // 道具蓝框
    PF_Purple                   = 104,               // 道具紫框
    PF_Gold                     = 105,               // 道具金框

                                                        /// <| FragmentFrame 碎片品质边框 >
    FF_White                    = 201,               // 碎片白框
    FF_Green                    = 202,               // 碎片绿框
    FF_Blue                     = 203,               // 碎片蓝框
    FF_Purple                   = 204,               // 碎片紫框
    FF_Gold                     = 205,               // 碎片金框

                                                        /// <| HeroFrame 英雄品质边框 >
    HF_White                    = 301,               // 英雄白框
    HF_Green                    = 302,               // 英雄绿框
    HF_Green1                   = 303,               // 英雄绿框+1
    HF_Blue                     = 304,               // 英雄蓝框
    HF_Blue1                    = 305,               // 英雄篮框+1
    HF_Blue2                    = 306,               // 英雄篮框+2
    HF_Purple                   = 307,               // 英雄紫框
    HF_Purple1                  = 308,               // 英雄紫框+1
    HF_Purple2                  = 309,               // 英雄紫框+2
    HF_Purple3                  = 310,               // 英雄紫框+3
    HF_Gold                     = 311,               // 英雄金框


    Currency_Diamond            = 401,              // 钻石图集名
    Currency_Coins              = 402,              // 金币图集名称
    SoulFrame                   = 403,              // 魂石框
    HeroFrame                   = 404               // 英雄框


}
public enum                         ElementType                                         // 元素类型                     
{
    Ice                         = 1,                                                /// 冰
    Fire                        = 2,                                                /// 火
    Thunder                     = 3                                                 /// 雷
}
public enum                         SceneType                                           // 场景展示面板                 
{
    Start,                                                                          /// 1.启动登录场景
    Main,                                                                           /// 2.主界面场景
    Battle                                                                          /// 3.战斗场景
}
public enum                         TipsType                                            // 提示类型                     
{
    Normal                      = 1,                                                /// 正常
    Gold                        = 2,                                                ///
}