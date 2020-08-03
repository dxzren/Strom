/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公会副本伤害排名奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_SocietyHarmRankingData 
             { 
                /// <summary>
                /// 奖励ID--主键
                /// </summary>
                public int RewardID{get;set;}

                
                /// <summary>
                /// 排名范围
                /// </summary>
                public List<int> Ranked { get;set; }
                /// <summary>
                /// 礼包ID
                /// </summary>
                public int GiftID { get;set; }
            } 
            /// <summary>
            /// (公会副本伤害排名奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SocietyHarmRanking
            { 

                static Configs_SocietyHarmRanking _sInstance;
                public static Configs_SocietyHarmRanking sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SocietyHarmRanking();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公会副本伤害排名奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SocietyHarmRankingData> mSocietyHarmRankingDatas
                {
                    get { return _SocietyHarmRankingDatas; }
                }

                /// <summary>
                /// (公会副本伤害排名奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_SocietyHarmRankingData> _SocietyHarmRankingDatas = new Dictionary<int, Configs_SocietyHarmRankingData>();

                /// <summary>
                /// 根据RewardID读取对应的配置信息
                /// </summary>
                /// <param name="RewardID">配置的RewardID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SocietyHarmRankingData GetSocietyHarmRankingDataByRewardID(int RewardID)
                {
                    if (_SocietyHarmRankingDatas.ContainsKey(RewardID))
                    {
                        return _SocietyHarmRankingDatas[RewardID];
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
  Configs_SocietyHarmRankingData cd = new Configs_SocietyHarmRankingData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.RewardID = key; 
 
 string[] RankedStrs= Util.GetStringKeyValue(body, "Ranked").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Ranked = new List<int>();
foreach(string RankedStr in RankedStrs)  cd.Ranked.Add(Util.ParseToInt(RankedStr)); 
 
 cd.GiftID =  Util.GetIntKeyValue(body,"GiftID"); 
  
 if (mSocietyHarmRankingDatas.ContainsKey(key) == false)
 mSocietyHarmRankingDatas.Add(key, cd);
  }
 //Debug.Log(mSocietyHarmRankingDatas.Count);
}

            }