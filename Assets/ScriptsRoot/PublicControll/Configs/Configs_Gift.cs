/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (礼包表)客户端配置结构体
            /// </summary>
            public partial class Configs_GiftData 
             { 
                /// <summary>
                /// 礼包ID--主键
                /// </summary>
                public int GiftID{get;set;}

                
                /// <summary>
                /// 礼包类型
                /// </summary>
                public int Type { get;set; }
                /// <summary>
                /// 物品类型
                /// </summary>
                public List<int> PropType { get;set; }
                /// <summary>
                /// 物品ID
                /// </summary>
                public List<int> PropID { get;set; }
                /// <summary>
                /// 物品数量
                /// </summary>
                public List<int> Number { get;set; }
            } 
            /// <summary>
            /// (礼包表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Gift
            { 

                static Configs_Gift _sInstance;
                public static Configs_Gift sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Gift();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (礼包表)字典集合
                /// </summary>
                public Dictionary<int, Configs_GiftData> mGiftDatas
                {
                    get { return _GiftDatas; }
                }

                /// <summary>
                /// (礼包表)字典集合
                /// </summary>
                Dictionary<int, Configs_GiftData> _GiftDatas = new Dictionary<int, Configs_GiftData>();

                /// <summary>
                /// 根据GiftID读取对应的配置信息
                /// </summary>
                /// <param name="GiftID">配置的GiftID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_GiftData GetGiftDataByGiftID(int GiftID)
                {
                    if (_GiftDatas.ContainsKey(GiftID))
                    {
                        return _GiftDatas[GiftID];
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
  Configs_GiftData cd = new Configs_GiftData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.GiftID = key; 
  cd.Type =  Util.GetIntKeyValue(body,"Type"); 
 
 string[] PropTypeStrs= Util.GetStringKeyValue(body, "PropType").TrimStart('{').TrimEnd('}',',').Split(',');
cd.PropType = new List<int>();
foreach(string PropTypeStr in PropTypeStrs)  cd.PropType.Add(Util.ParseToInt(PropTypeStr)); 
 

 string[] PropIDStrs= Util.GetStringKeyValue(body, "PropID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.PropID = new List<int>();
foreach(string PropIDStr in PropIDStrs)  cd.PropID.Add(Util.ParseToInt(PropIDStr)); 
 

 string[] NumberStrs= Util.GetStringKeyValue(body, "Number").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Number = new List<int>();
foreach(string NumberStr in NumberStrs)  cd.Number.Add(Util.ParseToInt(NumberStr)); 
 
 
 if (mGiftDatas.ContainsKey(key) == false)
 mGiftDatas.Add(key, cd);
  }
 //Debug.Log(mGiftDatas.Count);
}

            }