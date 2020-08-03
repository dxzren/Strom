/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (活动奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_ActivityAwardData 
             { 
                /// <summary>
                /// 奖励ID--主键
                /// </summary>
                public int AwardID{get;set;}

                
                /// <summary>
                /// 活动ID
                /// </summary>
                public int ActivityID { get;set; }
                /// <summary>
                /// 活动种类
                /// </summary>
                public int Kind { get;set; }
                /// <summary>
                /// 开启时间
                /// </summary>
                public int OpenTime { get;set; }
                /// <summary>
                /// 条件类型
                /// </summary>
                public int Type { get;set; }
                /// <summary>
                /// 领奖条件
                /// </summary>
                public int Condition { get;set; }
                /// <summary>
                /// 领奖条件2
                /// </summary>
                public int Condition2 { get;set; }
                /// <summary>
                /// 礼包ID
                /// </summary>
                public List<int> GiftID { get;set; }
                /// <summary>
                /// 礼包价格
                /// </summary>
                public int Price { get;set; }
                /// <summary>
                /// 刷新标识
                /// </summary>
                public int Refresh { get;set; }
                /// <summary>
                /// 礼包权重
                /// </summary>
                public List<int> Weight { get;set; }
                /// <summary>
                /// 限制类型
                /// </summary>
                public int LimitType { get;set; }
                /// <summary>
                /// 限制次数
                /// </summary>
                public int LimitNumber { get;set; }
                /// <summary>
                /// 七日奖励图标
                /// </summary>
                public string icon { get;set; }
    /// <summary>
    /// 礼包名称
    /// </summary>
                public string ActivityName { get; set; }
            } 
            /// <summary>
            /// (活动奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ActivityAward
            { 

                static Configs_ActivityAward _sInstance;
                public static Configs_ActivityAward sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ActivityAward();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (活动奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_ActivityAwardData> mActivityAwardDatas
                {
                    get { return _ActivityAwardDatas; }
                }

                /// <summary>
                /// (活动奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_ActivityAwardData> _ActivityAwardDatas = new Dictionary<int, Configs_ActivityAwardData>();

                /// <summary>
                /// 根据AwardID读取对应的配置信息
                /// </summary>
                /// <param name="AwardID">配置的AwardID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ActivityAwardData GetActivityAwardDataByAwardID(int AwardID)
                {
                    if (_ActivityAwardDatas.ContainsKey(AwardID))
                    {
                        return _ActivityAwardDatas[AwardID];
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
  Configs_ActivityAwardData cd = new Configs_ActivityAwardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.AwardID = key; 
  cd.ActivityID =  Util.GetIntKeyValue(body,"ActivityID"); 
  cd.Kind =  Util.GetIntKeyValue(body,"Kind"); 
 // cd.OpenTime =  Util.GetIntKeyValue(body,"OpenTime"); 
  cd.Type =  Util.GetIntKeyValue(body,"Type"); 
  cd.Condition =  Util.GetIntKeyValue(body,"Condition"); 
  cd.Condition2 =  Util.GetIntKeyValue(body,"Condition2"); 
 
 string[] GiftIDStrs= Util.GetStringKeyValue(body, "GiftID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.GiftID = new List<int>();
foreach(string GiftIDStr in GiftIDStrs)  cd.GiftID.Add(Util.ParseToInt(GiftIDStr)); 
 
 cd.Price =  Util.GetIntKeyValue(body,"Price"); 
  cd.Refresh =  Util.GetIntKeyValue(body,"Refresh"); 
 
 string[] WeightStrs= Util.GetStringKeyValue(body, "Weight").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Weight = new List<int>();
foreach(string WeightStr in WeightStrs)  cd.Weight.Add(Util.ParseToInt(WeightStr)); 
 
 cd.LimitType =  Util.GetIntKeyValue(body,"LimitType"); 
  cd.LimitNumber =  Util.GetIntKeyValue(body,"LimitNumber"); 
  cd.icon =  Util.GetStringKeyValue(body,"icon");
            cd.ActivityName = Util.GetStringKeyValue(body, "ActivityName");


            if (mActivityAwardDatas.ContainsKey(key) == false)
 mActivityAwardDatas.Add(key, cd);
  }
 //Debug.Log(mActivityAwardDatas.Count);
}

            }