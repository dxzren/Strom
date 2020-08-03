using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
/// <summary>  注册帐号绑定  </summary>

public class AccBind_Command : EventCommand
{
    [Inject]
    public IRequest             netRequest          { set; get; }
    [Inject]
    public IStartData           startData           { set; get; }


    public override void        Execute()
    {
        PanelManager.sInstance.ShowLoadingPanel();
        netRequest.dispatcher.AddListener(HttpRequest.RESPONSE_MESSAGE, OnResponse);            /// 与服务器建立通讯

        WWWForm                 form                    = new WWWForm();
        SavedAccountInfos       AccInfo                 = Util.GetAccFile("SavedAccountInofs");
        form.AddField           ("oldacc", AccInfo.Account);
        form.AddField           ("oldpwd", AccInfo.Password);
        form.AddField           ("newacc", startData.account);
        form.AddField           ("newpwd1", startData.password);
        form.AddField           ("newpwd2", startData.password);

        netRequest.RequestServo ("bingacc", form);
    }

    public void                 OnResponse  ( IEvent evt)
    {
        netRequest.dispatcher.RemoveListener( HttpRequest.RESPONSE_MESSAGE, OnResponse);        /// 与服务器建立通讯

        string                  strEvt                  = evt.data.ToString();                  /// 处理服务器回调消息
        LitJson.JsonData        newJsonData             = LitJson.JsonMapper.ToObject(strEvt);
        Debug.Log               (strEvt.ToString());

        if (((IDictionary)newJsonData).Contains("0"))                                           /// 绑定成功               
        {
            Debug.Log           ("0 = "    + newJsonData["0"]);
            Debug.Log           ("账户 = " + newJsonData["acc"]);
            Debug.Log           ("密码 = " + newJsonData["pwd"]);

            SavedAccountInfos   AccInfo                 = new SavedAccountInfos();
            AccInfo.Account                             = (string)newJsonData["acc"];
            AccInfo.Password                            = (string)newJsonData["pwd"];
            AccInfo.Tempporary                          = "hn";
            Util.SaveFile       (AccInfo, "SavedAccountInfos");                                 /// 更新本地帐号文件

            PanelManager.sInstance.HidePanel(SceneType.Start, UIPanelConfig.RegisterPanel);     /// 隐藏注册面板
            PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.GameEnterPanel);    /// 游戏进入面板
        }
        else                                                                                    /// 打印错误信息           
        {
            for(int i = 1; i < 8; i ++)
            {
                if(((IDictionary)newJsonData).Contains(i.ToString()))
                {
                    Debug.Log(newJsonData[i.ToString()]);
                    PanelManager.sInstance.ShowNoticePanel((string)newJsonData[i.ToString()]);
                    break;
                }
            }
        }
        PanelManager.sInstance.HideLoadingPanel();
    }
}