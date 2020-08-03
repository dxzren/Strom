using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class ServerLineItemView : EventView
{
    public GameObject           Item;                                                           // 服务器按钮
    public GameSrvInfo          ServerItem;                                                     // 服务器信息
    public UILabel              ServerName;                                                     // 服务器名称
    public UISprite             State;                                                          // 服务器状态

    public int                  CenterServerID;                                                 // 服务器ID
    public string               ItemEvent           = "ItemEvent";                              // 点击事件

    public void                 Init()
    {
        UIEventListener.Get(Item).onClick           = ItemClick;                                /// 服务器名称点击
        ServerName.text                             = ServerItem.szGameSrvName;                 /// 服务器名称
        
        if          ( ServerItem.nState == 0 )                                                  /// 维护
        {                       State.spriteName    = "weihu";      }
        else if     ( ServerItem.nState == 1 )                                                  /// 正常
        {                       State.spriteName    = "zhengchang"; }

        else if     ( ServerItem.nState == 2 || ServerItem.nState == 3 )                        /// 爆满
        {                       State.spriteName    = "maoman";     }

    }

    public void                 ItemClick( GameObject obj )                                     /// 服务器名点击
    {
        dispatcher.Dispatch     (ItemEvent);
    }
}