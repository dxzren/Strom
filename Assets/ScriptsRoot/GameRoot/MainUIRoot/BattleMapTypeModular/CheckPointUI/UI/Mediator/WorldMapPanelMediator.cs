using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class WorldMapPanelMediator : EventMediator
{
    [Inject]
    public WorldMapPanelView    View                        { set; get; }
    [Inject]
    public ICheckPointSys       InCheckP_Sys                { set; get; }
    [Inject]
    public IPlayer              InPlayer                    { set; get; }

    public override void        OnRegister()
    {
        View.Init();
        WorldMapSet();

        View.dispatcher.AddListener ( View.WorldMapExitClick_Event,         WorldMapExitClickHandler );             /// 退出Btn     点击
        View.dispatcher.AddListener ( View.ChapterMapClick_Event,           ChapterMapClickHandler );               /// 地图章节    点击
        View.dispatcher.AddListener ( EventSignal.BattleFinished_Event,     WorldMapSet );                          /// 战斗结算 -- 地图章节设置

        dispatcher.AddListener ( EventSignal.UpdateInfo_Event,              WorldMapSet );                          /// 更新信息   -- 地图章节设置
        dispatcher.AddListener ( GlobalEvent.UIAnimationEvent,              UIAnimationStart );                     /// UI_界面动画

        UIAnimationStart();                                                                                         /// 退出<Btn>_坐上角飞入动画

        PanelManager.sInstance.LoadOverHandler_10Planel(gameObject.name);                                           /// 加载玩数据展示当前界面
        Debug.Log("世界地图加载完毕!");
    }
    public override void        OnRemove()
    {
        View.dispatcher.RemoveListener( View.WorldMapExitClick_Event,         WorldMapExitClickHandler );           /// 退出Btn     点击
        View.dispatcher.RemoveListener( View.ChapterMapClick_Event,           ChapterMapClickHandler );             /// 地图章节    点击
        View.dispatcher.RemoveListener( EventSignal.BattleFinished_Event,     WorldMapSet );                        /// 战斗结算 -- 地图章节设置

        dispatcher.RemoveListener( EventSignal.UpdateInfo_Event, WorldMapSet );                                     /// 更新信息   -- 地图章节设置
        dispatcher.RemoveListener( GlobalEvent.UIAnimationEvent, UIAnimationStart );                                /// UI动画播发 -- 退出<Btn>_坐上角飞入动画
    }
    private void                WorldMapExitClickHandler()                                                          // 退出Btn    点击处理          
    {
        Debug.Log("WorldMapExitClickHandler");
        UIAnimation.Instance().LineMove ( View.ExitBtn,                                                             /// 横向位移
                               new float[] { UIAnimationConfig.Back_toX,   UIAnimationConfig.Back_toY,   UIAnimationConfig.Back_toZ},
                               new float[] { UIAnimationConfig.Back_fromX, UIAnimationConfig.Back_fromY, UIAnimationConfig.Back_fromZ });
        PanelManager.sInstance.HidePanel ( SceneType.Main, UIPanelConfig.WorldMapPanel );                           /// 隐藏世界地图面板
        PanelManager.sInstance.ShowPanel(SceneType.Main, UIPanelConfig.MainUIPanel);                                /// 打开主界面面板
    }
    private void                ChapterMapClickHandler()                                                            // 地图章节   点击              
    {
        InCheckP_Sys.chapterID = View.TheChapterID;                                                                 /// 设置      当前章节ID
        if ( (InCheckP_Sys.chapterID) > Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID (              /// 重置当前关卡类型:普通
                                InPlayer.EliteMaxCheckPointHistory.ID).GenusChapter  &&  InCheckP_Sys.CurrentCP_Type == ChapterType.Elite)
            {    InCheckP_Sys.CurrentCP_Type = ChapterType.Normal;   }
        Debug.Log("InCheckP_Sys.CurrentCP_Type = ChapterType.Normal"+ InCheckP_Sys.CurrentCP_Type.ToString());
        PanelManager.sInstance.ShowPanel ( SceneType.Main, UIPanelConfig.CheckPointSelectPanel);                    /// 打开关卡选择面板
        Util.DestoryImmediate(View.gameObject);                                                                     /// 摧毁当前面板
    }
    private void                WorldMapSet()                                                                       // 地图章节设置                  
    {
        int                     center                      = 0;                                                    /// 中心章节
        int                     chaPterTotal                = InCheckP_Sys.chapterTotal;                            /// 章节总数
        for (int i = 0; i < 13; i++ )                                                                               /// 所有章节设置
        {
            int                 theChapterID                = Util.ParseToInt(View.MapList[i].name);                            /// 章节ID
            int                 PlayerPassMaxCheckP         = Configs_CheckPoint.sInstance.GetCheckPointDataByCheckPointID      /// 最新关卡进度
                                                                (InPlayer.NormalMaxCheckPointHistory.ID).GenusChapter;
            if ( InPlayer.PlayerLevel >= Configs_Chapter.sInstance.GetChapterDataByChapterID( theChapterID ).UnlockLevel[0])    /// 玩家等级大于章节解锁等级 
            {
                View.MapList[i].SetActive                   (true);                                                 ///
                View.MapList[i].transform.GetChild(0).GetComponent<UILabel>().text  = 
                                "第" + ToChineseNumber( theChapterID ) + " 章" + "\n" + Language.GetValue(
                                Configs_Chapter.sInstance.GetChapterDataByChapterID( theChapterID ).ChapterName);
                View.MapList[i].transform.GetChild(0).transform.GetChild(0).GetComponent<UILabel>().text = "";

                View.MapPartList[i].SetActive               (true);                                                 ///
                if ( theChapterID > PlayerPassMaxCheckP )
                {
                    View.MapList[i].GetComponent<BoxCollider>().enabled                                         = false;
                    View.MapList[i].transform.GetChild(0).transform.GetChild(0).GetComponent<UILabel>().text    = "未开启";
                    View.MapPartList[i].SetActive(false);
                }
                else
                {   center      = i; }
            }
            else 
            {
                View.MapList[i].SetActive                   ( true );
                View.MapList[i].GetComponent<UIButton>().isEnabled                                              = false;
                View.MapList[i].transform.GetChild(0).GetComponent<UILabel>().text =
                                "第" + ToChineseNumber(theChapterID) + "章" + "\n" +
                                Language.GetValue(Configs_Chapter.sInstance.GetChapterDataByChapterID(theChapterID).ChapterName);
                View.MapList[i].transform.GetChild(0).transform.GetChild(0).GetComponent<UILabel>().text =
                                Configs_Chapter.sInstance.GetChapterDataByChapterID(theChapterID).UnlockLevel[0] + "级开启";
                break;
            }
        }
        if ( center == 11 )     center++;
        Util.MoveToCurrentCP    (View.MapList[center], 2);                                      /// 屏幕中心移动到当前关卡章节
    }
    private void                UIAnimationStart()                                                                  // 播发:退出<Btn>_坐上角飞入动画  
    {
        View.ExitBtn.transform.localPosition = new Vector3( UIAnimationConfig.Back_fromX,
                                                            UIAnimationConfig.Back_fromY,
                                                            UIAnimationConfig.Back_fromZ );
        Invoke ("PlayUIAnimation", UIAnimationConfig.BlackToNomarl_duration);
    }
    private void                PlayUIAnimation()                                                                   // 退出<Btn>_坐上角飞入动画线程   
    {   FlyInEffect.BackButtonFlyInEffect( View.ExitBtn);   }

    private string              ToChineseNumber ( int chapterID )                                                   // 转化:中文数字                 
    {
        switch (chapterID)
        {
            case 1:             return "一";
            case 2:             return "二";
            case 3:             return "三";
            case 4:             return "四";
            case 5:             return "五";
            case 6:             return "六";
            case 7:             return "七";
            case 8:             return "八";
            case 9:             return "九";
            case 10:            return "十";
            case 11:            return "十一";
            case 12:            return "十二";
            case 13:            return "十三";
            case 14:            return "十四";
            case 15:            return "十五";
            case 16:            return "十六";
            case 17:            return "十七";
            case 18:            return "十八";
            case 19:            return "十九";
            case 20:            return "二十";
            default:            return "Error: " + chapterID.ToString();                  
        }
    }
    
}