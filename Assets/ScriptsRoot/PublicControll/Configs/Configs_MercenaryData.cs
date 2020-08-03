/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (佣兵数据表)客户端配置结构体
            /// </summary>
            public partial class Configs_MercenaryDataData 
             { 
                /// <summary>
                /// 佣兵ID--主键
                /// </summary>
                public int MercenaryID{get;set;}

                
                /// <summary>
                /// 英雄种族
                /// </summary>
                public int HeroRace { get;set; }
                /// <summary>
                /// 下阶ID
                /// </summary>
                public int NextID { get;set; }
                /// <summary>
                /// 佣兵阶数
                /// </summary>
                public int Mercenarylvl { get;set; }
                /// <summary>
                /// 限制进阶等级
                /// </summary>
                public int LimitLevel { get;set; }
                /// <summary>
                /// 进阶材料ID
                /// </summary>
                public int PropID { get;set; }
                /// <summary>
                /// 进阶材料需求
                /// </summary>
                public int UpdateCost { get;set; }
                /// <summary>
                /// 对应品质
                /// </summary>
                public int MercenaryQuality { get;set; }
                /// <summary>
                /// 佣兵星级
                /// </summary>
                public int MercenaryStar { get;set; }
            } 
            /// <summary>
            /// (佣兵数据表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_MercenaryData
            { 

                static Configs_MercenaryData _sInstance;
                public static Configs_MercenaryData sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_MercenaryData();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (佣兵数据表)字典集合
                /// </summary>
                public Dictionary<int, Configs_MercenaryDataData> mMercenaryDataDatas
                {
                    get { return _MercenaryDataDatas; }
                }

                /// <summary>
                /// (佣兵数据表)字典集合
                /// </summary>
                Dictionary<int, Configs_MercenaryDataData> _MercenaryDataDatas = new Dictionary<int, Configs_MercenaryDataData>();

                /// <summary>
                /// 根据MercenaryID读取对应的配置信息
                /// </summary>
                /// <param name="MercenaryID">配置的MercenaryID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_MercenaryDataData GetMercenaryDataDataByMercenaryID(int MercenaryID)
                {
                    if (_MercenaryDataDatas.ContainsKey(MercenaryID))
                    {
                        return _MercenaryDataDatas[MercenaryID];
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
  Configs_MercenaryDataData cd = new Configs_MercenaryDataData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.MercenaryID = key; 
  cd.HeroRace =  Util.GetIntKeyValue(body,"HeroRace"); 
  cd.NextID =  Util.GetIntKeyValue(body,"NextID"); 
  cd.Mercenarylvl =  Util.GetIntKeyValue(body,"Mercenarylvl"); 
  cd.LimitLevel =  Util.GetIntKeyValue(body,"LimitLevel"); 
  cd.PropID =  Util.GetIntKeyValue(body,"PropID"); 
  cd.UpdateCost =  Util.GetIntKeyValue(body,"UpdateCost"); 
  cd.MercenaryQuality =  Util.GetIntKeyValue(body,"MercenaryQuality"); 
  cd.MercenaryStar =  Util.GetIntKeyValue(body,"MercenaryStar"); 
  
 if (mMercenaryDataDatas.ContainsKey(key) == false)
 mMercenaryDataDatas.Add(key, cd);
  }
 //Debug.Log(mMercenaryDataDatas.Count);
}

            }