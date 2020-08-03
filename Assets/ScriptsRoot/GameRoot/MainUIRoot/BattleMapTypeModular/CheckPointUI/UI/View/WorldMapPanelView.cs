using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

/// <summary> 关卡-世界地图面板视图 </summary>
public class WorldMapPanelView : EventView
{
    [Inject]
    public ICheckPointSys       InChcekP_Sys                { set; get; }

    public int                  TheChapterID                = 0;
    public string               WorldMapExitClick_Event     = "WorldMapExitClick_Event";        //
    public string               ChapterMapClick_Event       = "ChapterMapClick_Event";          //

    public GameObject[]         MapList                     = new GameObject[13];               // 章节地图列表
    public GameObject[]         MapPartList                 = new GameObject[13];               // 地图特效列表
    public GameObject           ExitBtn;                                                        // 退出按键

    public void Init()
    {
        foreach ( GameObject key in MapList )
        {    UIEventListener.Get (key).onClick  = CreateMapClick; }                             /// 创建章节地图 (Btn监听)
        UIEventListener.Get(ExitBtn).onClick    = ExitClick;                                    /// 退出点击     (Btn监听)
    }

    private void                CreateMapClick( GameObject obj )                                // 点击创建章节地图
    {
        TheChapterID            = Util.ParseToInt(obj.name);                                    /// 获取点击章节ID
        dispatcher.Dispatch     (ChapterMapClick_Event);
    }
    private void                ExitClick( GameObject obj )                                     // 退出点击
    {
        dispatcher.Dispatch(WorldMapExitClick_Event);
        Debug.Log("ExitClicked");
    }
}