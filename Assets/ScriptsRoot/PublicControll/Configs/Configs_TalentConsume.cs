/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (天赋等级消耗表)客户端配置结构体
            /// </summary>
            public partial class Configs_TalentConsumeData 
             { 
                /// <summary>
                /// 天赋等级--主键
                /// </summary>
                public int TalentLvl{get;set;}

                
                /// <summary>
                /// 所需天赋点
                /// </summary>
                public int NeedNumber { get;set; }
            } 
            /// <summary>
            /// (天赋等级消耗表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_TalentConsume
            { 

                static Configs_TalentConsume _sInstance;
                public static Configs_TalentConsume sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_TalentConsume();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (天赋等级消耗表)字典集合
                /// </summary>
                public Dictionary<int, Configs_TalentConsumeData> mTalentConsumeDatas
                {
                    get { return _TalentConsumeDatas; }
                }

                /// <summary>
                /// (天赋等级消耗表)字典集合
                /// </summary>
                Dictionary<int, Configs_TalentConsumeData> _TalentConsumeDatas = new Dictionary<int, Configs_TalentConsumeData>();

                /// <summary>
                /// 根据TalentLvl读取对应的配置信息
                /// </summary>
                /// <param name="TalentLvl">配置的TalentLvl</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_TalentConsumeData GetTalentConsumeDataByTalentLvl(int TalentLvl)
                {
                    if (_TalentConsumeDatas.ContainsKey(TalentLvl))
                    {
                        return _TalentConsumeDatas[TalentLvl];
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
  Configs_TalentConsumeData cd = new Configs_TalentConsumeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.TalentLvl = key; 
  cd.NeedNumber =  Util.GetIntKeyValue(body,"NeedNumber"); 
  
 if (mTalentConsumeDatas.ContainsKey(key) == false)
 mTalentConsumeDatas.Add(key, cd);
  }
 //Debug.Log(mTalentConsumeDatas.Count);
}

            }