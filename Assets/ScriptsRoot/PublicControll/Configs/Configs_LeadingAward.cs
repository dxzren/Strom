/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (等级宝箱奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_LeadingAwardData 
             { 
                /// <summary>
                /// 宝箱ID--主键
                /// </summary>
                public int BoxID{get;set;}

                
                /// <summary>
                /// 需求等级
                /// </summary>
                public int NeedLevel { get;set; }
                /// <summary>
                /// 礼包ID
                /// </summary>
                public int GiftID { get;set; }
            } 
            /// <summary>
            /// (等级宝箱奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_LeadingAward
            { 

                static Configs_LeadingAward _sInstance;
                public static Configs_LeadingAward sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_LeadingAward();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (等级宝箱奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_LeadingAwardData> mLeadingAwardDatas
                {
                    get { return _LeadingAwardDatas; }
                }

                /// <summary>
                /// (等级宝箱奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_LeadingAwardData> _LeadingAwardDatas = new Dictionary<int, Configs_LeadingAwardData>();

                /// <summary>
                /// 根据BoxID读取对应的配置信息
                /// </summary>
                /// <param name="BoxID">配置的BoxID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_LeadingAwardData GetLeadingAwardDataByBoxID(int BoxID)
                {
                    if (_LeadingAwardDatas.ContainsKey(BoxID))
                    {
                        return _LeadingAwardDatas[BoxID];
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
  Configs_LeadingAwardData cd = new Configs_LeadingAwardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.BoxID = key; 
  cd.NeedLevel =  Util.GetIntKeyValue(body,"NeedLevel"); 
  cd.GiftID =  Util.GetIntKeyValue(body,"GiftID"); 
  
 if (mLeadingAwardDatas.ContainsKey(key) == false)
 mLeadingAwardDatas.Add(key, cd);
  }
 //Debug.Log(mLeadingAwardDatas.Count);
}

            }