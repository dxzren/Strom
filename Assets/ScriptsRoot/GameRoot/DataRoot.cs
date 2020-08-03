using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public class DataRoot : ContextView
{
    void Awake()
    {
        Debug.Log("03>.DataContext实例 - 初始化绑定:< 视图-控制 >__< 事件-命令 >__< 消息-事件 >!");
        context = new DataContext(this);                                    // DataContext实例 - 初始化绑定<视图-控制>_<事件-命令>_<消息-事件>
    }
}
