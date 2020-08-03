/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (竞技场周奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_JJCWeekAwardData 
             { 
                /// <summary>
                /// 奖励档次--主键
                /// </summary>
                public int RewardGrade{get;set;}

                
                /// <summary>
                /// 排名范围
                /// </summary>
                public List<int> RankScope { get;set; }
                /// <summary>
                /// 指向礼包ID
                /// </summary>
                public int GiftID { get;set; }
            } 
            /// <summary>
            /// (竞技场周奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_JJCWeekAward
            { 

                static Configs_JJCWeekAward _sInstance;
                public static Configs_JJCWeekAward sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_JJCWeekAward();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (竞技场周奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_JJCWeekAwardData> mJJCWeekAwardDatas
                {
                    get { return _JJCWeekAwardDatas; }
                }

                /// <summary>
                /// (竞技场周奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_JJCWeekAwardData> _JJCWeekAwardDatas = new Dictionary<int, Configs_JJCWeekAwardData>();

                /// <summary>
                /// 根据RewardGrade读取对应的配置信息
                /// </summary>
                /// <param name="RewardGrade">配置的RewardGrade</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_JJCWeekAwardData GetJJCWeekAwardDataByRewardGrade(int RewardGrade)
                {
                    if (_JJCWeekAwardDatas.ContainsKey(RewardGrade))
                    {
                        return _JJCWeekAwardDatas[RewardGrade];
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
  Configs_JJCWeekAwardData cd = new Configs_JJCWeekAwardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.RewardGrade = key; 
 
 string[] RankScopeStrs= Util.GetStringKeyValue(body, "RankScope").TrimStart('{').TrimEnd('}',',').Split(',');
cd.RankScope = new List<int>();
foreach(string RankScopeStr in RankScopeStrs)  cd.RankScope.Add(Util.ParseToInt(RankScopeStr)); 
 
 cd.GiftID =  Util.GetIntKeyValue(body,"GiftID"); 
  
 if (mJJCWeekAwardDatas.ContainsKey(key) == false)
 mJJCWeekAwardDatas.Add(key, cd);
  }
 //Debug.Log(mJJCWeekAwardDatas.Count);
}

            }