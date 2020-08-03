/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (抽卡魂石卡库)客户端配置结构体
            /// </summary>
            public partial class Configs_LibrarySoulData 
             { 
                /// <summary>
                /// 魂石ID--主键
                /// </summary>
                public int SoulID{get;set;}

                
                /// <summary>
                /// 初始星级
                /// </summary>
                public int InitialStar { get;set; }
                /// <summary>
                /// 权重
                /// </summary>
                public int Weight { get;set; }
                /// <summary>
                /// 人类标识
                /// </summary>
                public int Human { get;set; }
                /// <summary>
                /// 精灵标识
                /// </summary>
                public int ELF { get;set; }
                /// <summary>
                /// 矮人标识
                /// </summary>
                public int Dwarves { get;set; }
            } 
            /// <summary>
            /// (抽卡魂石卡库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_LibrarySoul
            { 

                static Configs_LibrarySoul _sInstance;
                public static Configs_LibrarySoul sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_LibrarySoul();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (抽卡魂石卡库)字典集合
                /// </summary>
                public Dictionary<int, Configs_LibrarySoulData> mLibrarySoulDatas
                {
                    get { return _LibrarySoulDatas; }
                }

                /// <summary>
                /// (抽卡魂石卡库)字典集合
                /// </summary>
                Dictionary<int, Configs_LibrarySoulData> _LibrarySoulDatas = new Dictionary<int, Configs_LibrarySoulData>();

                /// <summary>
                /// 根据SoulID读取对应的配置信息
                /// </summary>
                /// <param name="SoulID">配置的SoulID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_LibrarySoulData GetLibrarySoulDataBySoulID(int SoulID)
                {
                    if (_LibrarySoulDatas.ContainsKey(SoulID))
                    {
                        return _LibrarySoulDatas[SoulID];
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
  Configs_LibrarySoulData cd = new Configs_LibrarySoulData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.SoulID = key; 
  cd.InitialStar =  Util.GetIntKeyValue(body,"InitialStar"); 
  cd.Weight =  Util.GetIntKeyValue(body,"Weight"); 
  cd.Human =  Util.GetIntKeyValue(body,"Human"); 
  cd.ELF =  Util.GetIntKeyValue(body,"ELF"); 
  cd.Dwarves =  Util.GetIntKeyValue(body,"Dwarves"); 
  
 if (mLibrarySoulDatas.ContainsKey(key) == false)
 mLibrarySoulDatas.Add(key, cd);
  }
 //Debug.Log(mLibrarySoulDatas.Count);
}

            }