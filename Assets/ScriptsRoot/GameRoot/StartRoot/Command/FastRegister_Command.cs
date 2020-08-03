using UnityEngine;
using System.Collections;
using System;
using LitJson;
using SimpleJson;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

/// <summary>  快速注册  </summary>
public class FastRegister_Command : EventCommand
{
    [Inject]
    public IRequest             netRequset          { set; get; }
    [Inject]
    public IStartData           startData           { set; get; }

    public override void        Execute()
    {
        PanelManager.sInstance. ShowLoadingPanel();                                             /// 显示加载面板
        netRequset.dispatcher.  AddListener( HttpRequest.RESPONSE_MESSAGE, OnResponse);         /// 添加监听..与登录服通讯

        WWWForm                 form                = new WWWForm();
        form.AddField           ("key", Util.GetMD5("123456789"));
        netRequset.RequestServo ("fastregist", form);
                                  
        Debug.Log               (Util.GetMD5("123456789"));
        Debug.Log               ("快速注册请求");
    }

    public void OnResponse      ( IEvent evt)                                                   // 处理服务器回调消息 
    {
        Debug.Log               ("快速注册回调");

        netRequset.dispatcher.RemoveListener( HttpRequest .RESPONSE_MESSAGE, OnResponse);       /// 移除WEB回调信息

        string                  msgData             = evt.data.ToString();                      /// 处理服务器回调消息
        LitJson.JsonData        newJsonData;                                                       
        try
        {
            newJsonData                             = LitJson.JsonMapper.ToObject(msgData);
        }
        catch (Exception e)
        {
            Debug.Log           (e.ToString());
            return;
        }

        if(((IDictionary)newJsonData).Contains("0"))                                            /// 快速注册成功      
        {
            Debug.Log           ("0 =  " + newJsonData["0"]);
            Debug.Log           ("账户= " + newJsonData["acc"]);
            Debug.Log           ("密码= " + newJsonData["pwd"]);

            startData.account                       = (string)newJsonData["acc"];               /// 更新帐号到游戏数据
            startData.password                      = (string)newJsonData["pwd"];               /// 更新密码到游戏数据
            SavedAccountInfos   AccInfoFile         = new SavedAccountInfos();          
            AccInfoFile.Account                     = (string)newJsonData["acc"];                                  
            AccInfoFile.Password                    = (string)newJsonData["pwd"];
            Util.SaveFile       ( AccInfoFile, "SavedAccountInfos");                            /// 保存帐号信息文件到本地

            PanelManager.sInstance.HidePanel(SceneType.Start, UIPanelConfig.LogInOrRegisterPanel);  /// 隐藏登录注册面板
            PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.GameEnterPanel);        /// 显示游戏进入面板
        }
        else                                                                                    /// 快速注册失败      
        {
            Debug.Log           ("快速注册失败");
            for (int i = 0; i < 8; i++)
            {
                if(((IDictionary)newJsonData).Contains(i.ToString()))
                {
                    Debug.Log   (newJsonData[i.ToString()]);
                    PanelManager.sInstance.ShowNoticePanel((string)newJsonData[i.ToString()]);
                    break;                      
                }
            }
        }
    }
}