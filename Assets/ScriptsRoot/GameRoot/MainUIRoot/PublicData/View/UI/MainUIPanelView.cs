using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

///-------------------------------------------------------------------------------------------------------------------------/// <summary> 主界面UI视图 </summary>
public class MainUIPanelView : EventView
{
    [Inject]
    public IPlayer              InPlayer                        { set; get; }
    [Inject]
    public IGameData            InGameData                      { set; get; }

    public string               HeroSysClick_Event              = "HeroSysClick_Event";                                     /// 英雄系统点击
    public string               BagSysClick_Event               = "BagSysClick_Event";                                      /// 背包系统点击
    public string               CheckPointSysClick_Event        = "CheckPointSysClick_Event";                               /// 关卡系统点击
    public string               MallSysClick_Event              = "MallSysClick_Event";                                     /// 商城系统点击
    public string               MallFreeStart_Event             = "MallFreeStart_Event";                                    /// 商城免费计时开始

    public int                  propID, propType;                                                                           /// 道具ID,道具类型
    public UILabel              PlayerName, PlayerLv, PlayerVipLv, PlayerExpNum;                                            /// 玩家名称,玩家等级,玩家VIP等级,玩家经验
    public UISlider             PlayerExpSlider;                                                                            /// 玩家经验进度
    public UISprite             PlayerIcon;                                                                                 /// 玩家头像
    public UITexture            Background;                                                                                 /// 主界面背景图
    public UIInput              GM_Command;                                                                                 /// GM_指令

    public GameObject           TipsPanelObj, ChatPanelObj;                                                                 /// 提示面板, 聊天面板
    public GameObject           HeroSysBtn, BagSysBtn, CheckPointSysBtn, MallSysBtn;                                        /// 英雄系统按钮, 背包系统按钮, 关卡按钮, 商城按钮
    public GameObject           Left, Right, Top, Bottom;                                                                   /// 左,右,上,下
    public SystemHorn           SystemHorn;                                                                                 /// 系统喇叭_广播

    public UILabel              HeroLabel, BagLabel, BagLockLabel, CheckPointLabel, MallLabel, MallLockLabel;               /// 系统名称
    public GameObject           HeroBtnObj, BagBtnObj, CheckPointBtnObj, MallBtnObj, MallBtnBgObj;

    public List<Transform>      TF_List                     = new List<Transform>();                                        /// (Transform)列表

    private List<int>           SortList                    = new List<int>();                                              /// 排行榜列表
    private List<GameObject>    TheBtnHideList              = new List<GameObject>();                                       /// 主界面隐藏按钮列表
    private Dictionary<int, GameObject> TheBtnList          = new Dictionary<int, GameObject>();                            /// 按钮字典
    public void                 Init()                                                          
    {
        InitAllBtnSet();                                                                        /// 初始化所有系统按钮设置
        UIEventListener.Get(HeroSysBtn).onClick             = HeroSysClick;                     /// 英雄系统点击 监听
        UIEventListener.Get(BagSysBtn).onClick              = BagSysClick;                      /// 背包系统点击 监听
        UIEventListener.Get(CheckPointSysBtn).onClick       = CheckPointSysClick;               /// 关卡系统点击 监听
        UIEventListener.Get(MallSysBtn).onClick             = MallSysClick;                     /// 商城系统点击 监听
    }

    private void                InitAllBtnSet()                                                 // 初始化所有系统按钮设置   
    {
        InitBtnSet( MainSystem.HeroSys );                                                       /// 英雄系统
        InitBtnSet( MainSystem.BagSys );                                                        /// 背包系统
        InitBtnSet( MainSystem.CheckPoint );                                                    /// 关卡系统
        InitBtnSet( MainSystem.MallSys );                                                       /// 商城系统
        BtnSortShow();
    }
    private void                InitBtnSet ( MainSystem sysName)                                // 初始化系统按钮          
    {
        UILabel                 TheNameLabel                   = null;
        UILabel                 TheNameLockLabel               = null;
        GameObject              TheBackFrame                   = null;
        GameObject              TheBtn                         = null;
        GameObject              TheBtnObj                      = null;                          // 显示隐藏按钮
        SystemShowClick         ShowClick                      = MainUiShowManger.sInstance.    // 主界面系统显示点击_设置
                                                                 ShowClickLvSet(InPlayer,sysName);  
        switch(sysName)                                                                         /// 系统按钮名称,对象设置 
        {
            case MainSystem.HeroSys:                            // 英雄系统 
                {
                    TheNameLabel = HeroLabel;
                    TheBtn       = HeroSysBtn;
                    TheBtnObj    = HeroBtnObj;
                    break;
                }
            case MainSystem.BagSys:                             // 背包系统 
                {
                    TheNameLabel        = BagLabel;
                    TheNameLockLabel    = BagLockLabel;
                    TheBtn              = BagSysBtn;
                    TheBtnObj           = BagBtnObj;
                    break;
                }
            case MainSystem.CheckPoint:                         // 关卡系统 
                {
                    TheNameLabel        = CheckPointLabel;
                    TheBtn              = CheckPointSysBtn;
                    TheBtnObj           = CheckPointBtnObj;
                    break;
                }
            case MainSystem.MallSys:                            // 商城系统 
                {
                    TheNameLabel        = MallLabel;
                    TheNameLockLabel    = MallLockLabel;
                    TheBackFrame        = MallBtnBgObj;
                    TheBtn              = MallSysBtn;
                    TheBtnObj = MallBtnObj;
                    break;
                }
        }
        if ( ShowClick.isClickLevel)                                                            /// 达到开启等级
        {
            if ( TheBtn != null )                                                               // 按钮设置
            {    TheBtn.GetComponent<BoxCollider>().enabled = true;     }

            if ( TheNameLabel != null )                                                         // 按钮名称设置 
            {
                TheNameLabel.enabled = true;
                TheNameLabel.text = ShowClick.viewName;
            }

            if ( TheBackFrame != null )                                                         // 按钮背框设置
            {    TheBackFrame.SetActive(true);          }    

            if ( TheNameLockLabel != null )                                                     // 名称锁定设置
            {    TheNameLockLabel.gameObject.SetActive(false); }

            if ( ShowClick.systemBtnType == SystemBtnType.UIBtn )                               // UI类型按钮_排列设置
            {
                if ( TheBtnObj != null )
                {
                    SortList.Add(ShowClick.sortID);
                    TheBtnList.Add(ShowClick.sortID, TheBtnObj);
                }
            }

            if ( InGameData.MainBtnStateDic.ContainsKey(sysName))                               // 主界面按钮状态字典(设置)
            {    InGameData.MainBtnStateDic[sysName] = ShowClick;        }
            else
            {    InGameData.MainBtnStateDic.Add(sysName, ShowClick);     }
        }
        else
        {
            if ( TheBtn != null )                                                               // 按钮隐藏
            {    BtnHide(TheBtn);   }
            if ( ShowClick.isShowLevel )                                                        // 按钮达到显示等级
            {
                if ( TheNameLockLabel != null )                                                 // 显示等级开启提示
                {    TheNameLockLabel.text  = ShowClick.LimitLevel + "级开启";  }
                if ( ShowClick.systemBtnType == SystemBtnType.UIBtn )                           // UI类型按钮_排列设置
                {
                    if ( TheBtnObj != null )
                    {
                        SortList.Add        ( ShowClick.sortID );
                        TheBtnList.Add      ( ShowClick.sortID, TheBtnObj);
                    }
                }
                if ( InGameData.MainBtnStateDic.ContainsKey(sysName))
                {    InGameData.MainBtnStateDic[sysName] = ShowClick;       }
                else
                {    InGameData.MainBtnStateDic.Add(sysName,ShowClick);     }
            }
            else
            {
                if ( TheNameLockLabel != null )                                                 // 锁定信息不显示
                {    TheNameLockLabel.gameObject.SetActive(false);          }

                if ( ShowClick.systemBtnType == SystemBtnType.UIBtn )                           // UI类型_不显示
                {
                    if ( BagBtnObj != null )
                    {    BagBtnObj.SetActive(false);    }  
                }
                if ( TheNameLabel != null )                                                     // 按钮名称不显示
                {    TheNameLabel.enabled = false;      }

                if ( TheBackFrame != null )                                                     // 背框不显示
                {    TheBackFrame.SetActive(false);     } 
                if ( InGameData.MainBtnStateDic.ContainsKey(sysName))                           // 添加到主界面按钮状态字典
                {    InGameData.MainBtnStateDic[sysName] = ShowClick;       }
                else
                {    InGameData.MainBtnStateDic.Add(sysName,ShowClick);     }                 
            }
        }

    }
    private void                BtnHide (GameObject btnObj)                                     // 按钮隐藏                
    {
        TheBtnHideList.Add      (btnObj);
        if ( IsInvoking ("BtnHideRun"))
        {    CancelInvoke("BtnHideRun");}
        Invoke("BtnHideRun",0.1f );
    }
    private void                BtnHideRun()                                                    // 隐藏按钮<激活区域-关闭>  
    {
        for ( int i = 0; i < TheBtnHideList.Count; i++ )
        {    TheBtnHideList[i].GetComponent<BoxCollider>().enabled = false;     }
        TheBtnHideList.Clear();
    }
    private void                BtnSortShow()                                                   // UI按钮排列设置          
    {
        SortList.Sort();
        for ( int i = 0; i < SortList.Count;i++ )
        {
            TheBtnList[SortList[i]].transform.parent            = TF_List[i];
            TheBtnList[SortList[i]].transform.localEulerAngles  = Vector3.zero;
            TheBtnList[SortList[i]].transform.localPosition     = Vector3.zero;
            TheBtnList[SortList[i]].transform.localScale        = Vector3.one;
        }
    }
    private void                HeroSysClick ( GameObject obj )                                 // 英雄系统点击            
    {
        if ( !IsClickLv(MainSystem.HeroSys))                return;
        dispatcher.Dispatch     ( HeroSysClick_Event );
    }
    private void                BagSysClick ( GameObject obj)                                   // 背包系统点击            
    {
        if ( !IsClickLv(MainSystem.BagSys))                return;
        dispatcher.Dispatch     ( BagSysClick_Event );
    }
    private void                CheckPointSysClick ( GameObject obj)                            // 关卡系统点击            
    {
        if ( !IsClickLv(MainSystem.CheckPoint))             return;
        dispatcher.Dispatch     ( CheckPointSysClick_Event);
    }
    private void                MallSysClick ( GameObject obj )                                 // 商城点击               
    {
        if ( !IsClickLv(MainSystem.MallSys))                return;
        dispatcher.Dispatch     ( MallSysClick_Event );
    }
    private bool                IsClickLv( MainSystem SysName )                                 // 是否到达点击等级        
    {
        SystemShowClick         TheSysShow = MainUiShowManger.sInstance.ShowClickLvSet( InPlayer,SysName);
        return                  TheSysShow.isClickLevel;
    }
}
