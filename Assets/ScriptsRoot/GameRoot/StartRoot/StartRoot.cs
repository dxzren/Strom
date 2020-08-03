using UnityEngine;
using System;
using System.Collections;
using strange.extensions.context.impl;

public class StartRoot : ContextView
{
    void Awake()
    {
        Debug.Log               ("01>    StartRoot.Util.CheckLocalPath()  检测本地资源目录!");
        Util.CheckLocalPath();                                                                                                  // 检测本地资源目录

        Debug.Log               ("02>    StartRoot.CreateDataRoot()  创建游戏数据源!");
        CreateDataRoot();                                                                                                       // 创建游戏框架

        Debug.Log               ("04>    StartRoot.context = new StartContext(this)实例 - 初始化绑定<视图-控制>_<事件-命令>");
        context                 = new StartContext(this);                                                                       // StartContext实例 - 初始化绑定<视图-控制>_<事件-命令>
    }
    private void CreateDataRoot()                                                                                               // 创建游戏框架        
    {
        if (GameObject.FindGameObjectWithTag("Respawn") == null)                                                                // 查找游戏对象，返回标签列表，否则返回空   
        {
            Debug.Log                                       ("Prefab_01>    加载 RootData.Prefab");
            GameObject          go                          = Instantiate(Util.Load("Prefabs/RootData")) as GameObject;         // 加载资源请求 RootData                  
            go.name                                         = "RootData";
            DontDestroyOnLoad(go);                                                                                              // 不重复加载            
            Debug.Log                                       ("Prefab_02>    加载 ScreenMask.Prefab");
            GameObject          screenMask                  = Instantiate(Util.Load("Prefabs/ScreenMask")) as GameObject;       // 加载资源     ScreenMask   
            screenMask.name                                 = "ScreenMask";
            DontDestroyOnLoad   (screenMask);                                                                                   // 不重复加载 
            Application.runInBackground                     = true;                                                             // 切换后,后台继续运行
        }
    }

}

