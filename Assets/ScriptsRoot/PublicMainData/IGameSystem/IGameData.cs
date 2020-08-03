using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public interface IGameData
{
    bool                                EMailSysRed                     { set; get; }                                       // 1.邮件红点
    bool                                MallSysRed                      { set; get; }                                       // 2.商城红点
    bool                                TaskSysRed                      { set; get; }                                       // 3.任务红点
    bool                                ChatSysRed                      { set; get; }                                       // 4.消息红点
    bool                                ActivitySysRed                  { set; get; }                                       // 5.活动红点

    bool                                MercSysRed                      { set; get; }                                       // 6.佣兵红点
    bool                                FriendSysRed                    { set; get; }                                       // 7.好友红点
    bool                                PlayerSysRed                    { set; get; }                                       // 8.战队红点
    bool                                WingSysRed                      { set; get; }                                       // 9.翅膀红点
    bool                                HeroSysRed                      { set; get; }                                       // 10.英雄红点

    bool                                IsExitPanel                     { set; get; }                                       // 是否弹出退出面板
    long                                subServerT                      { set; get; }                                       // 服务器同步时间差

    UIAtlas                             GetGameAtlas                    (string key);                                       // 获取游戏中的图集

    List<PanelObj>                      StartPanelList();                                                                   // 开始场景中的Panel队列
    List<PanelObj>                      MainPanelList();                                                                    // 主场景  中的Panel队列
    List<PanelObj>                      BattlePanelList();                                                                  // 战斗场景中的Panel队列

    void                                AtlasInit(string atlasName);                                                        // 初始化图集（AtlasName:图集路径）  

    Dictionary<string, UIAtlas>         AtlasList                       { set; get; }                                       // 图集列表
    Dictionary<int,int[][]>             CheckPointDropDic               { get; }                                            // 战斗结果(本地保存)_key:ID value[0] 道具ID value[1] 未掉落数量
    Dictionary<MainSystem, SystemShowClick> MainBtnStateDic             { set; get; }                                       // 主界面按钮状态

}