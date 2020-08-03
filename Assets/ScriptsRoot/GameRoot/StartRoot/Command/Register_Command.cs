using UnityEngine;
using System.Collections;
using System;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

public class Register_Command : EventCommand
{
    [Inject]
    public IRequest          netRequist      { set; get; }
    [Inject]
    public IStartData           startData       { set; get; }

    public override void        Execute()
    {
        PanelManager.sInstance. ShowLoadingPanel();
        netRequist.dispatcher.  AddListener(HttpRequest .RESPONSE_MESSAGE, OnResponse);           // 添加回调消息监听

        WWWForm form            = new WWWForm();
        string  acc             = Util.GetMD5(startData.account);                               /// 帐号
        string  pwd             = Util.GetMD5(startData.password);                              /// 密码
        Debug.Log               ("MD5 = " + acc);
        form.AddField           ("accname", startData.account);                                 /// 帐号
        form.AddField           ("accpwd1", startData.password);                                /// 密码1
        form.AddField           ("accpwd2", startData.password);                                /// 密码2

        netRequist.RequestServo ("regist", form);                                               // 发送注册请求
        Debug.Log               ("注册请求");
    }

    public void OnResponse      (IEvent evt )
    {
        Debug.Log               ("注册回调");
        netRequist.dispatcher.  AddListener(HttpRequest .RESPONSE_MESSAGE, OnResponse);

        string evtData          = evt.data.ToString();
        Debug.Log(evtData.      ToString());
        LitJson.JsonData        newJsonData;
        try
        {
            newJsonData         = LitJson.JsonMapper.ToObject(evtData);
        }
        catch(Exception e)
        {
            Debug.Log           (e.ToString());
            return;
        }

        if(((IDictionary)       newJsonData).Contains("0"))                                     // 注册成功
        {
            Debug.Log           ("0 = "         + newJsonData["0"]);
            Debug.Log           ("帐号= "       + newJsonData["acc"]);
            Debug.Log           ("密码= "       + newJsonData["pwd"]);
            SavedAccountInfos   SaveAcc         = new SavedAccountInfos();                                          /// 新建账号信息文件类型
            SaveAcc.            Account         = (string)newJsonData["acc"];                                       /// 本地帐号保存
            SaveAcc.            Password        = (string)newJsonData["pwd"];                                       /// 本地密码保存
            SaveAcc.            Tempporary      = "hn";
            Util.               SaveFile        ( SaveAcc, "SavedAccountInfos");                                    /// 保存帐号信息文件到本地

            startData.          account         = (string)newJsonData ["acc"];                                      /// MSG读取 账号
            startData.          password        = (string)newJsonData ["pwd"];                                      /// MSG读取 密码
            dispatcher.         Dispatch        ( StartEvent.RefreshAcct_Event );                                   /// 更新帐号信息

            PanelManager.       sInstance.      HidePanel(SceneType.Start, UIPanelConfig.RegisterPanel);            /// 隐藏注册面板
            PanelManager.       sInstance.      ShowPanel(SceneType.Start, UIPanelConfig.GameEnterPanel);           /// 显示游戏进入面板
            GameObject          LogInPanel      = GameObject.Find("LogInOrRegisterPanel");

            if( LogInPanel      != null)
            {   LogInPanel.     SetActive(false);

            }
        }
        else
        {
            Debug.Log           ("注册失败!");
            for (int i = 1; i < 8; i++)
            {
                if(((IDictionary)newJsonData).Contains(i.ToString()))
                {
                    Debug.Log   (newJsonData[i.ToString()]);
                    PanelManager.sInstance.ShowNoticePanel((string)newJsonData[i.ToString()]);
                    break;
                }
            }
        }
        PanelManager.sInstance. HideLoadingPanel();
    }
}