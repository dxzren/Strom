using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;
/// <summary>  获取玩家昵称  </summary>
public class GetNickName_Command : EventCommand
{
    [Inject]
    public IStartData startData { set; get; }

    public override void Execute()
    {
        startData.randomName = Util.RandomRoleName();

        Debug.Log("得到一个随机的名字");
        dispatcher.Dispatch(StartEvent.GetNickNameEventCallBack_Event);     // 发送事件 获取随机名称回调
    }
}
	
