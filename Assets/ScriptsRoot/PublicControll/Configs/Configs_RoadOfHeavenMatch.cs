/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (天堂之路匹配表)客户端配置结构体
            /// </summary>
            public partial class Configs_RoadOfHeavenMatchData 
             { 
                /// <summary>
                /// 调取批次--主键
                /// </summary>
                public int Batch{get;set;}

                
                /// <summary>
                /// 战力下限
                /// </summary>
                public int PowerDown { get;set; }
                /// <summary>
                /// 战力上限
                /// </summary>
                public int PowerUp { get;set; }
                /// <summary>
                /// 调取数量
                /// </summary>
                public int Number { get;set; }
            } 
            /// <summary>
            /// (天堂之路匹配表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_RoadOfHeavenMatch
            { 

                static Configs_RoadOfHeavenMatch _sInstance;
                public static Configs_RoadOfHeavenMatch sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_RoadOfHeavenMatch();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (天堂之路匹配表)字典集合
                /// </summary>
                public Dictionary<int, Configs_RoadOfHeavenMatchData> mRoadOfHeavenMatchDatas
                {
                    get { return _RoadOfHeavenMatchDatas; }
                }

                /// <summary>
                /// (天堂之路匹配表)字典集合
                /// </summary>
                Dictionary<int, Configs_RoadOfHeavenMatchData> _RoadOfHeavenMatchDatas = new Dictionary<int, Configs_RoadOfHeavenMatchData>();

                /// <summary>
                /// 根据Batch读取对应的配置信息
                /// </summary>
                /// <param name="Batch">配置的Batch</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_RoadOfHeavenMatchData GetRoadOfHeavenMatchDataByBatch(int Batch)
                {
                    if (_RoadOfHeavenMatchDatas.ContainsKey(Batch))
                    {
                        return _RoadOfHeavenMatchDatas[Batch];
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
  Configs_RoadOfHeavenMatchData cd = new Configs_RoadOfHeavenMatchData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.Batch = key; 
  cd.PowerDown =  Util.GetIntKeyValue(body,"PowerDown"); 
  cd.PowerUp =  Util.GetIntKeyValue(body,"PowerUp"); 
  cd.Number =  Util.GetIntKeyValue(body,"Number"); 
  
 if (mRoadOfHeavenMatchDatas.ContainsKey(key) == false)
 mRoadOfHeavenMatchDatas.Add(key, cd);
  }
 //Debug.Log(mRoadOfHeavenMatchDatas.Count);
}

            }