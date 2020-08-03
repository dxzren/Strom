/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (天堂之路宝箱物品库)客户端配置结构体
            /// </summary>
            public partial class Configs_ROHAwardData 
             { 
                /// <summary>
                /// 物品ID--主键
                /// </summary>
                public int PropID{get;set;}

                
                /// <summary>
                /// 物品类别
                /// </summary>
                public int ProType { get;set; }
                /// <summary>
                /// 物品品质
                /// </summary>
                public int Quality { get;set; }
                /// <summary>
                /// 权重
                /// </summary>
                public int Weight { get;set; }
            } 
            /// <summary>
            /// (天堂之路宝箱物品库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ROHAward
            { 

                static Configs_ROHAward _sInstance;
                public static Configs_ROHAward sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ROHAward();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (天堂之路宝箱物品库)字典集合
                /// </summary>
                public Dictionary<int, Configs_ROHAwardData> mROHAwardDatas
                {
                    get { return _ROHAwardDatas; }
                }

                /// <summary>
                /// (天堂之路宝箱物品库)字典集合
                /// </summary>
                Dictionary<int, Configs_ROHAwardData> _ROHAwardDatas = new Dictionary<int, Configs_ROHAwardData>();

                /// <summary>
                /// 根据PropID读取对应的配置信息
                /// </summary>
                /// <param name="PropID">配置的PropID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ROHAwardData GetROHAwardDataByPropID(int PropID)
                {
                    if (_ROHAwardDatas.ContainsKey(PropID))
                    {
                        return _ROHAwardDatas[PropID];
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
  Configs_ROHAwardData cd = new Configs_ROHAwardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.PropID = key; 
  cd.ProType =  Util.GetIntKeyValue(body,"ProType"); 
  cd.Quality =  Util.GetIntKeyValue(body,"Quality"); 
  cd.Weight =  Util.GetIntKeyValue(body,"Weight"); 
  
 if (mROHAwardDatas.ContainsKey(key) == false)
 mROHAwardDatas.Add(key, cd);
  }
 //Debug.Log(mROHAwardDatas.Count);
}

            }