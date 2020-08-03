/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (星级宝箱奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_StarBoxAwardData 
             { 
                /// <summary>
                /// 宝箱奖励ID--主键
                /// </summary>
                public int BoxAwardID{get;set;}

                
                /// <summary>
                /// 章节类型
                /// </summary>
                public int ChapterType { get;set; }
                /// <summary>
                /// 所需星数
                /// </summary>
                public int NeedStar { get;set; }
                /// <summary>
                /// 职业标识
                /// </summary>
                public int OccupationBox { get;set; }
                /// <summary>
                /// 礼包ID
                /// </summary>
                public List<int> GiftID { get;set; }
            } 
            /// <summary>
            /// (星级宝箱奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_StarBoxAward
            { 

                static Configs_StarBoxAward _sInstance;
                public static Configs_StarBoxAward sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_StarBoxAward();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (星级宝箱奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_StarBoxAwardData> mStarBoxAwardDatas
                {
                    get { return _StarBoxAwardDatas; }
                }

                /// <summary>
                /// (星级宝箱奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_StarBoxAwardData> _StarBoxAwardDatas = new Dictionary<int, Configs_StarBoxAwardData>();

                /// <summary>
                /// 根据BoxAwardID读取对应的配置信息
                /// </summary>
                /// <param name="BoxAwardID">配置的BoxAwardID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_StarBoxAwardData GetStarBoxAwardDataByBoxAwardID(int BoxAwardID)
                {
                    if (_StarBoxAwardDatas.ContainsKey(BoxAwardID))
                    {
                        return _StarBoxAwardDatas[BoxAwardID];
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
  Configs_StarBoxAwardData cd = new Configs_StarBoxAwardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.BoxAwardID = key; 
  cd.ChapterType =  Util.GetIntKeyValue(body,"ChapterType"); 
  cd.NeedStar =  Util.GetIntKeyValue(body,"NeedStar"); 
  cd.OccupationBox =  Util.GetIntKeyValue(body,"OccupationBox"); 
 
 string[] GiftIDStrs= Util.GetStringKeyValue(body, "GiftID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.GiftID = new List<int>();
foreach(string GiftIDStr in GiftIDStrs)  cd.GiftID.Add(Util.ParseToInt(GiftIDStr)); 
 
 
 if (mStarBoxAwardDatas.ContainsKey(key) == false)
 mStarBoxAwardDatas.Add(key, cd);
  }
 //Debug.Log(mStarBoxAwardDatas.Count);
}

            }