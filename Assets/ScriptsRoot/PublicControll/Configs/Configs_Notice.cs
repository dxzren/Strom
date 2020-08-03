/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公告表)客户端配置结构体
            /// </summary>
            public partial class Configs_NoticeData 
             { 
                /// <summary>
                /// 公告ID--主键
                /// </summary>
                public int NoticeID{get;set;}

                
                /// <summary>
                /// 公告标题
                /// </summary>
                public string NoticeTitle { get;set; }
                /// <summary>
                /// 公告内容
                /// </summary>
                public string NoticeContent { get;set; }
                /// <summary>
                /// 署名
                /// </summary>
                public string NoticeSign { get;set; }
            } 
            /// <summary>
            /// (公告表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Notice
            { 

                static Configs_Notice _sInstance;
                public static Configs_Notice sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Notice();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公告表)字典集合
                /// </summary>
                public Dictionary<int, Configs_NoticeData> mNoticeDatas
                {
                    get { return _NoticeDatas; }
                }

                /// <summary>
                /// (公告表)字典集合
                /// </summary>
                Dictionary<int, Configs_NoticeData> _NoticeDatas = new Dictionary<int, Configs_NoticeData>();

                /// <summary>
                /// 根据NoticeID读取对应的配置信息
                /// </summary>
                /// <param name="NoticeID">配置的NoticeID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_NoticeData GetNoticeDataByNoticeID(int NoticeID)
                {
                    if (_NoticeDatas.ContainsKey(NoticeID))
                    {
                        return _NoticeDatas[NoticeID];
                    }
                    return null;
                }

               /// <summary>
/// 初始化配置信息
/// </summary>
/// <param name="configData">配置文件内容</param>
 public void InitConfiguration(string configData) 
 {
  JsonObject data = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(configData);
 foreach (KeyValuePair<string, object> element in data) 
 { 
  JsonObject body = (JsonObject)element.Value;
  Configs_NoticeData cd = new Configs_NoticeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.NoticeID = key; 
  cd.NoticeTitle =  Util.GetStringKeyValue(body,"NoticeTitle"); 
  cd.NoticeContent =  Util.GetStringKeyValue(body,"NoticeContent"); 
  cd.NoticeSign =  Util.GetStringKeyValue(body,"NoticeSign"); 
  
 if (mNoticeDatas.ContainsKey(key) == false)
 mNoticeDatas.Add(key, cd);
  }
 //Debug.Log(mNoticeDatas.Count);
}

            }