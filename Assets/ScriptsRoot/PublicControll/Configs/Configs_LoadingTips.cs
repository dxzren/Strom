/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (加载提示表)客户端配置结构体
            /// </summary>
            public partial class Configs_LoadingTipsData 
             { 
                /// <summary>
                /// 提示ID--主键
                /// </summary>
                public int TipsID{get;set;}

                
                /// <summary>
                /// 提示描述
                /// </summary>
                public string TipsDes { get;set; }
            } 
            /// <summary>
            /// (加载提示表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_LoadingTips
            { 

                static Configs_LoadingTips _sInstance;
                public static Configs_LoadingTips sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_LoadingTips();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (加载提示表)字典集合
                /// </summary>
                public Dictionary<int, Configs_LoadingTipsData> mLoadingTipsDatas
                {
                    get { return _LoadingTipsDatas; }
                }

                /// <summary>
                /// (加载提示表)字典集合
                /// </summary>
                Dictionary<int, Configs_LoadingTipsData> _LoadingTipsDatas = new Dictionary<int, Configs_LoadingTipsData>();

                /// <summary>
                /// 根据TipsID读取对应的配置信息
                /// </summary>
                /// <param name="TipsID">配置的TipsID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_LoadingTipsData GetLoadingTipsDataByTipsID(int TipsID)
                {
                    if (_LoadingTipsDatas.ContainsKey(TipsID))
                    {
                        return _LoadingTipsDatas[TipsID];
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
  Configs_LoadingTipsData cd = new Configs_LoadingTipsData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.TipsID = key; 
  cd.TipsDes =  Util.GetStringKeyValue(body,"TipsDes"); 
  
 if (mLoadingTipsDatas.ContainsKey(key) == false)
 mLoadingTipsDatas.Add(key, cd);
  }
 //Debug.Log(mLoadingTipsDatas.Count);
}

            }