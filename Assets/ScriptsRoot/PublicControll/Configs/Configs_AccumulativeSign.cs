/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (累计签到奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_AccumulativeSignData 
             { 
                /// <summary>
                /// 累计签到ID--主键
                /// </summary>
                public int SUMSignID{get;set;}

                
                /// <summary>
                /// 累计天数
                /// </summary>
                public int Days { get;set; }
                /// <summary>
                /// 礼包ID
                /// </summary>
                public int GiftID { get;set; }
            } 
            /// <summary>
            /// (累计签到奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_AccumulativeSign
            { 

                static Configs_AccumulativeSign _sInstance;
                public static Configs_AccumulativeSign sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_AccumulativeSign();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (累计签到奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_AccumulativeSignData> mAccumulativeSignDatas
                {
                    get { return _AccumulativeSignDatas; }
                }

                /// <summary>
                /// (累计签到奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_AccumulativeSignData> _AccumulativeSignDatas = new Dictionary<int, Configs_AccumulativeSignData>();

                /// <summary>
                /// 根据SUMSignID读取对应的配置信息
                /// </summary>
                /// <param name="SUMSignID">配置的SUMSignID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_AccumulativeSignData GetAccumulativeSignDataBySUMSignID(int SUMSignID)
                {
                    if (_AccumulativeSignDatas.ContainsKey(SUMSignID))
                    {
                        return _AccumulativeSignDatas[SUMSignID];
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
  Configs_AccumulativeSignData cd = new Configs_AccumulativeSignData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.SUMSignID = key; 
  cd.Days =  Util.GetIntKeyValue(body,"Days"); 
  cd.GiftID =  Util.GetIntKeyValue(body,"GiftID"); 
  
 if (mAccumulativeSignDatas.ContainsKey(key) == false)
 mAccumulativeSignDatas.Add(key, cd);
  }
 //Debug.Log(mAccumulativeSignDatas.Count);
}

            }