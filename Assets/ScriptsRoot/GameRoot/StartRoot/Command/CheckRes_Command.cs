using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;

/// <summary>  检测更新,启动网络全局监听  </summary>
public class CheckRes_Command : EventCommand
{
    public override void Execute()
    {
        Debug.Log("08>    CheckRes_Command.Execute()   StartGlobalListenerEvent 全局监听");
        dispatcher.Dispatch(GlobalEvent.StartGlobalListenerEvent);                              // 启动网络全局监听
        dispatcher.Dispatch(StartEvent.SetLoadTitle_Event, "检查更新......");                    // 设置加载标题Text "检查更新"
                                                                                                // (更新,unity编辑模式 )true ,局部强制_true
#if (UPDATERES || !UNITY_EDITOR) && !Force_Local
                        Updaters.Instance.CheckforResUpdate(() => {
#endif
        NetEventDispatcher.Instance().DispathcEvent(31542012, 31542012, null);                  // 加载资源
#if (UPDATERES || !UNITY_EDITOR) && !Force_Local
                        });
#endif
    }
}