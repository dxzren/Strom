/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (签到英雄魂石表)客户端配置结构体
            /// </summary>
            public partial class Configs_SignSoulData 
             { 
                /// <summary>
                /// 月份--主键
                /// </summary>
                public int month{get;set;}

                
                /// <summary>
                /// 碎片ID
                /// </summary>
                public int FragmentID { get;set; }
            } 
            /// <summary>
            /// (签到英雄魂石表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SignSoul
            { 

                static Configs_SignSoul _sInstance;
                public static Configs_SignSoul sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SignSoul();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (签到英雄魂石表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SignSoulData> mSignSoulDatas
                {
                    get { return _SignSoulDatas; }
                }

                /// <summary>
                /// (签到英雄魂石表)字典集合
                /// </summary>
                Dictionary<int, Configs_SignSoulData> _SignSoulDatas = new Dictionary<int, Configs_SignSoulData>();

                /// <summary>
                /// 根据month读取对应的配置信息
                /// </summary>
                /// <param name="month">配置的month</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SignSoulData GetSignSoulDataBymonth(int month)
                {
                    if (_SignSoulDatas.ContainsKey(month))
                    {
                        return _SignSoulDatas[month];
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
  Configs_SignSoulData cd = new Configs_SignSoulData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.month = key; 
  cd.FragmentID =  Util.GetIntKeyValue(body,"FragmentID"); 
  
 if (mSignSoulDatas.ContainsKey(key) == false)
 mSignSoulDatas.Add(key, cd);
  }
 //Debug.Log(mSignSoulDatas.Count);
}

            }