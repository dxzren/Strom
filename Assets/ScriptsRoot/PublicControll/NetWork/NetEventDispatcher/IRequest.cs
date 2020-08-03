using UnityEngine;
using SimpleJson;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;

public interface IRequest
{
    IEventDispatcher    dispatcher              { set; get; }                                   // 请求完成分发

                                                                                                // 新拓展方法 (route: 路由 body: jsonObject等) 
    void                RequestServo            (string route, WWWForm body);                   // 请求伺服: 实例:RequestServo ("servr.login",new{uid="",password=""});
    void                RequestGameServer       (string route, WWWForm body);                   // 请求游戏服 实例:RequestGameServer("game.cardsHandler.getCardInfor",new {"cardid ="});
}