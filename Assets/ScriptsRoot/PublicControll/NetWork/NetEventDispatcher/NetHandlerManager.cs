using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary> 用来添加网络的各个处理模块 </summary>
public class NetHandlerManager
{
    private static NetHandlerManager _Instance;
    public static NetHandlerManager Instance()                              
    {
        if (_Instance == null)
        {
            _Instance = new NetHandlerManager();
        }
        return _Instance;
    }


    private List<IResponseHandler> netHandlerList = new List<IResponseHandler>();

    public void AddHandler(IResponseHandler handler )                       
    {
        if(!netHandlerList.Contains(handler))
        {
            netHandlerList.Add(handler);
        }
    }

    public List<IResponseHandler> GetHandlerList()                          
    {
        return netHandlerList;
    }
}