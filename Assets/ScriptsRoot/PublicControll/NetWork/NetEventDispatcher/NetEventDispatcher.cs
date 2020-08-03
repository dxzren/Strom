using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
/// <summary> 用来分发各模块的处理结果 </summary>
public class NetEventDispatcher
{
    private static NetEventDispatcher _Instance;
    private List<EventBase> EventList = new List<EventBase>();
    private Dictionary<string, EventCallback> registedCallbacks = new Dictionary<string, EventCallback>();

    public static NetEventDispatcher Instance()       
                                                                                      
    {
        if(_Instance == null)
        {
            _Instance = new NetEventDispatcher();
        }
        return _Instance;
    }
    public delegate void EventCallback(EventBase eb);



    public void RegEventListener(int type1,int type2,EventCallback eventCallback)                   // 注册监听             
    {
        StringBuilder SB = new StringBuilder();
        SB.Append(type1);
        SB.Append("/");
        SB.Append(type2);
        string sEventName = SB.ToString();
        if(!registedCallbacks.ContainsKey(sEventName))
        {
            registedCallbacks.Add(sEventName, eventCallback);
        }
    }
    public void DispathcEvent(int type1,int type2,object eventValue)                                // 分发事件             
    {
        lock(EventList)
        {
            string eventName = type1 + "/" + type2;
            if(!registedCallbacks.ContainsKey(eventName))
            {
                return;
            }
            EventList.Add(new EventBase(eventName,eventValue));
        }
    }
    public void ClearAllCallbacks()                                                                 // 清理所有监听         
    {
        registedCallbacks.Clear();
    }
    public void OnTick()                                                                            // 倒计时函数           
    {
        lock(EventList)
        {
            for(int i = 0;i<EventList.Count;i++ )
            {
                EventBase EB = EventList[i];
                if(registedCallbacks.ContainsKey(EB.sEventName))
                {
                    EventCallback ECB = registedCallbacks[EB.sEventName];
                    if(ECB == null)
                    {
                        Debuger.Log("================未注册");
                        continue;
                    }
                    ECB(EB);
                }
            }
            EventList.Clear();
        }
    }
    public void UnRegEventListener(int type1, int type2, EventCallback eventCallback)               // 取消注册监听(暂未启用)
    { }
}

public class EventBase                                                      // 事件基础类
{
    public object eventValue;                           // 事件值 
    public string sEventName;                           // 事件名称

    public EventBase(string eventName)                  // 构造函数         
    {
        sEventName = eventName;
    }
    public EventBase(string evenName,object eValue)     // 构造函数         
    {
        eventValue = eValue;
        sEventName = evenName;
    }
}