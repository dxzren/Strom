using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
/// <summary> 关卡选择界面 </summary>

public class CheckPointSelectPanelView_bak : EventView
{
    [Inject]
    public IGameData            InGameData                  { set; get; }
    [Inject]
    public IPlayer              InPlayer                    { set; get; }
    [Inject]
    public ICheckPointSys       InCheckP_Sys                { set; get; }

    public string               AwardBoxClick_Event         = "AwardBoxClick_Event";            // 奖励宝箱
    public string               TakeOutClick_Event          = "TakeOutClick_Event";             // 展开页签 点击
    public string               TakeInClick_Event           = "TakeInClick_Event";              // 收缩页签 点击
    public string               ExitClick_Event             = "ExitClick_Event";                // 退出点击
    public string               LeftClick_Event             = "Left_Event";                     // 左边点击
    public string               RightClick_Event            = "Right_Event";                    // 右边点击
    public string               NormalClick_Event           = "NormalClick_Event";              // 普通点击
    public string               EliteClick_Event            = "EliteClick_Event";               // 精英点击
    public string               SelectHeroClick_Event       = "SelectHeroClick_Event";          // 英雄点击
    public string               BagClick_Event              = "BagClick_Event";                 // 背包点击


    public UILabel              CheckP_Name, CheckP_Num, StarCount;
    public UILabel              BagName;

    public GameObject           Left, Right,Top, Bottom, Mask;
    public GameObject           HeroBtn, BagBtn, TakeOutBtn, TakeInBtn;
    public GameObject           BagBtnObj;
    public GameObject           AwardBoxBtn, NormalBtn, EliteBtn, ExitBtn, CurrentMapItem, BackGround;

    public List<TweenAlpha>     TweenAlphaList;                                                 // 转换 通道列表
    public List<TweenPosition>  TweenPosList;                                                   // 转换 坐标列表

    public  int                 showSysNum                  = 0;                                // 显示系统数量
    private List<GameObject>    BtnHideList                 = new List<GameObject>();           // 隐藏按钮列表
    public void                 Init()
    {
        UIEventListener.Get(AwardBoxBtn).onClick            = AwardBoxClick;                    // 奖励宝箱
        UIEventListener.Get(TakeOutBtn).onClick             = TakeOutClick;                     // 展开页签 点击
        UIEventListener.Get(TakeInBtn).onClick              = TakeInClick;                      // 收缩页签 点击
        UIEventListener.Get(ExitBtn).onClick                = ExitClick;                        // 退出点击
        UIEventListener.Get(Left).onClick                   = LeftClick;                        // 左边点击
        UIEventListener.Get(Right).onClick                  = RightClick;                       // 右边点击
        UIEventListener.Get(NormalBtn).onClick              = NormalClick;                      // 普通点击
        UIEventListener.Get(EliteBtn).onClick               = EliteClick;                       // 精英点击
        UIEventListener.Get(HeroBtn).onClick                = HeroClick;                        // 英雄点击
        UIEventListener.Get(BagBtn).onClick                 = BagClick;                         // 背包点击
    }
    public void InitAllBtn()                                                                    // 初始化所有按钮  
    {
        InitBtnRun              (MainSystem.BagSys);
    }

    private void                InitBtnRun( MainSystem sysName)                                 // 初始化按钮      
    {
        SystemShowClick         TheSysShowClick             = MainUiShowManger.sInstance.ShowClickLvSet(InPlayer,sysName);
        UILabel                 TheNameLabel                = null;
        GameObject              TheBtn                      = null;
        GameObject              TheBtnObj                   = null;

        switch(sysName)
        {
            case MainSystem.BagSys:
                {
                    TheNameLabel    = BagName;
                    TheBtn          = BagBtn;
                    TheBtnObj       = BagBtnObj;
                    break;
                }
        }
        if (TheSysShowClick.isClickLevel)                                                       /// 达到激活等级      
        {
            showSysNum++;   
            if ( TheBtn != null )                                                               /// 点击(激活)
            {    TheBtn.GetComponent<BoxCollider>().enabled = true;             }
            if ( TheNameLabel != null )                                                         /// 显示按钮名称  
            {
                TheNameLabel.enabled                        = true;
                TheNameLabel.text                           = TheSysShowClick.viewName;
            }   

            if ( InGameData.MainBtnStateDic.ContainsKey( sysName))                              /// Add主界面按钮状态字典
            {    InGameData.MainBtnStateDic[sysName]        = TheSysShowClick;  }
            else
            {    InGameData.MainBtnStateDic.Add(sysName,TheSysShowClick);       }
        }
        else
        {
            if ( TheBtn != null )                                                               /// 添加到隐藏按钮列表,并隐藏
            {    BtnHide( TheBtn );  }
            if ( TheSysShowClick.isShowLevel )                                                  /// 到达显示等级       
            {
                showSysNum++;
                if ( TheNameLabel != null)
                {
                    TheNameLabel.text = TheSysShowClick.isShowLevel + "级开启";
                    TheNameLabel.color = new Color(199f / 255f, 196f / 255f, 177f / 255f, 1f);
                }
                if (InGameData.MainBtnStateDic.ContainsKey(sysName))                            /// Add主界面按钮状态字典
                { InGameData.MainBtnStateDic[sysName] = TheSysShowClick; }
                else
                { InGameData.MainBtnStateDic.Add(sysName, TheSysShowClick); }
            }
            else                                                                                /// 未达到显示等级     
            {
                if ( TheSysShowClick.systemBtnType == SystemBtnType.UIBtn )                     /// 隐藏按钮    
                {
                    if ( BagBtnObj != null )
                    {    TheBtnObj.SetActive (false);}
                }
                if ( TheNameLabel != null )
                {    TheNameLabel.enabled = false;}
                if (InGameData.MainBtnStateDic.ContainsKey(sysName))                            /// Add主界面按钮状态字典
                { InGameData.MainBtnStateDic[sysName] = TheSysShowClick; }
                else
                { InGameData.MainBtnStateDic.Add(sysName, TheSysShowClick); }
            }
        }

    }
    private void                BtnHide( GameObject obj )                                       // 添加到隐藏列表,并取消激活状态
    {
        BtnHideList.Add (obj);
        if ( IsInvoking("BtnHideRun"))
        {    CancelInvoke("BtnHideRun"); }
        Invoke("BtnHideRun",4f );
    }
    private void                BtnHideRun()                                                    // 取消列表所有按钮 激活状态
    {
        for ( int i = 0; i < BtnHideList.Count; i++ )
        {    BtnHideList[i].GetComponent<BoxCollider>().enabled = false;}
    }

    public void                 AwardBoxClick ( GameObject obj )                                // 奖励宝箱
    {   dispatcher.Dispatch( AwardBoxClick_Event);          }

    public void                 TakeOutClick ( GameObject obj )                                 // 展开页签 点击
    {   dispatcher.Dispatch( TakeOutClick_Event);       }

    public void                 TakeInClick ( GameObject obj )                                  // 收缩页签 点击
    {   dispatcher.Dispatch( TakeInClick_Event);        }

    public void                 ExitClick ( GameObject obj )                                    // 退出点击
    {   dispatcher.Dispatch( ExitClick_Event );         }

    public void                 LeftClick ( GameObject obj )                                    // 左边点击
    {   dispatcher.Dispatch ( LeftClick_Event );        }

    public void                 RightClick ( GameObject obj )                                   // 右边点击
    {   dispatcher.Dispatch ( RightClick_Event );       }

    public void                 NormalClick ( GameObject obj )                                  // 普通点击
    {   dispatcher.Dispatch ( NormalClick_Event);       }

    public void                 EliteClick ( GameObject obj )                                   // 精英点击
    {   dispatcher.Dispatch ( ExitClick_Event);         }

    public void                 HeroClick ( GameObject obj )                                    // 英雄点击
    {    dispatcher.Dispatch ( SelectHeroClick_Event);  }

    public void                 BagClick ( GameObject obj )                                     // 背包点击 
    {
        SystemShowClick         TheSysShowClick             = null;
        if ( InGameData.MainBtnStateDic.ContainsKey (MainSystem.BagSys))
        {    TheSysShowClick = InGameData.MainBtnStateDic[MainSystem.BagSys];   }

        if ( TheSysShowClick == null && TheSysShowClick.isClickLevel )                          /// 到达点击等级
        {    dispatcher.Dispatch (BagClick_Event);                              }            
    }
}