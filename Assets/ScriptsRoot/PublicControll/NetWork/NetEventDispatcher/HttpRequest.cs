using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
/// <summary>   WEB 请求   </summary>
public class HttpRequest  : IRequest
{
    public static string        RESPONSE_MESSAGE        = "HTTP_RESPONSE_MESSAGE";              // WEB 回调

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject           contextView             { set; get; }
    [Inject]
    public IEventDispatcher     dispatcher              { set; get; }
    [Inject]
    public IServers             servers                 { set; get; }                           // 服务器地址

    public void                 RequestServo            ( string route, WWWForm form)           // 请求伺服     
    {
        string                  url                     = servers.ServoAddress.ToString() + "storm_reg/" + route.TrimStart('/') + ".php";
        MonoBehaviour           root                    = contextView.GetComponent<MonoBehaviour>();
        root.StartCoroutine     (RealRequest( url, form ));
    }
    public void                 RequestGameServer       ( string route, WWWForm form)           // 请求游戏服
    {          }
    private IEnumerator         RealRequest             (string url, WWWForm form)              // 重新请求     
    {
        WWW                     request                 = new WWW(url, form);
        yield return            request;
        if ( request.error != null )
        {
            Debug.LogError("www.Error,function is " + url + "Info: " + request.error);
            dispatcher.Dispatch(RESPONSE_MESSAGE, "{error:" + request.error + "}");
        }
        else
        {
            dispatcher.Dispatch(RESPONSE_MESSAGE, request.text);
        }
    }
}
