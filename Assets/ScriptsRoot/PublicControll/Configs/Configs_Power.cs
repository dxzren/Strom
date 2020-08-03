/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (战力系数)客户端配置结构体
            /// </summary>
            public partial class Configs_PowerData 
             { 
                /// <summary>
                /// 属性项--主键
                /// </summary>
                public int Attribute{get;set;}

                
                /// <summary>
                /// 战力系数
                /// </summary>
                public float PowerNum { get;set; }
            } 
            /// <summary>
            /// (战力系数)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Power
            { 

                static Configs_Power _sInstance;
                public static Configs_Power sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Power();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (战力系数)字典集合
                /// </summary>
                public Dictionary<int, Configs_PowerData> mPowerDatas
                {
                    get { return _PowerDatas; }
                }

                /// <summary>
                /// (战力系数)字典集合
                /// </summary>
                Dictionary<int, Configs_PowerData> _PowerDatas = new Dictionary<int, Configs_PowerData>();

                /// <summary>
                /// 根据Attribute读取对应的配置信息
                /// </summary>
                /// <param name="Attribute">配置的Attribute</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_PowerData GetPowerDataByAttribute(int Attribute)
                {
                    if (_PowerDatas.ContainsKey(Attribute))
                    {
                        return _PowerDatas[Attribute];
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
  Configs_PowerData cd = new Configs_PowerData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.Attribute = key; 
  cd.PowerNum =  Util.GetFloatKeyValue(body,"PowerNum"); 
  
 if (mPowerDatas.ContainsKey(key) == false)
 mPowerDatas.Add(key, cd);
  }
 //Debug.Log(mPowerDatas.Count);
}

            }