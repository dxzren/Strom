/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (战队头像表)客户端配置结构体
            /// </summary>
            public partial class Configs_TroopsHeadSData 
             { 
                /// <summary>
                /// 指向战队头像ID--主键
                /// </summary>
                public int TroopsID{get;set;}

                
                /// <summary>
                /// 头像70
                /// </summary>
                public string head70 { get;set; }
                /// <summary>
                /// 头像84
                /// </summary>
                public string head84 { get;set; }
                /// <summary>
                /// 类别
                /// </summary>
                public int Type { get;set; }
            } 
            /// <summary>
            /// (战队头像表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_TroopsHeadS
            { 

                static Configs_TroopsHeadS _sInstance;
                public static Configs_TroopsHeadS sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_TroopsHeadS();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (战队头像表)字典集合
                /// </summary>
                public Dictionary<int, Configs_TroopsHeadSData> mTroopsHeadSDatas
                {
                    get { return _TroopsHeadSDatas; }
                }

                /// <summary>
                /// (战队头像表)字典集合
                /// </summary>
                Dictionary<int, Configs_TroopsHeadSData> _TroopsHeadSDatas = new Dictionary<int, Configs_TroopsHeadSData>();

                /// <summary>
                /// 根据TroopsID读取对应的配置信息
                /// </summary>
                /// <param name="TroopsID">配置的TroopsID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_TroopsHeadSData GetTroopsHeadSDataByTroopsID(int TroopsID)
                {
                    if (_TroopsHeadSDatas.ContainsKey(TroopsID))
                    {
                        return _TroopsHeadSDatas[TroopsID];
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
  Configs_TroopsHeadSData cd = new Configs_TroopsHeadSData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.TroopsID = key; 
  cd.head70 =  Util.GetStringKeyValue(body,"head70"); 
  cd.head84 =  Util.GetStringKeyValue(body,"head84"); 
  cd.Type =  Util.GetIntKeyValue(body,"Type"); 
  
 if (mTroopsHeadSDatas.ContainsKey(key) == false)
 mTroopsHeadSDatas.Add(key, cd);
  }
 //Debug.Log(mTroopsHeadSDatas.Count);
}

            }