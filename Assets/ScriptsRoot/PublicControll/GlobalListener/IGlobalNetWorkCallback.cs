using UnityEngine;
using System.Collections;

public interface IGlobalNetWorkCallback
{
    void                AddHero(EventBase obj);                        // 添加英雄
    void                ChangeCurrency(EventBase obj);                 // 玩家货币更新
    void                ChangeItemData(EventBase obj);                 // 玩家物品数据变化
    void                ChangePlayerAttr(EventBase obj);               // 玩家属性变化
    void                ChangeHeroLvExp(EventBase obj);                // 英雄经验变化

    void                ReLogIn(EventBase obj);                        // 重新登录(本地触发了重新登录机制,先自动重连，连不上，再启动手动重新登录机制)
    void                NetNotGood(EventBase obj);                     // 网络不好
    void                ErrorCode(EventBase obj);                      // 错误码

}