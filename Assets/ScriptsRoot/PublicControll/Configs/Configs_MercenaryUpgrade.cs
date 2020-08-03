/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (佣兵升级经验表)客户端配置结构体
            /// </summary>
            public partial class Configs_MercenaryUpgradeData 
             { 
                /// <summary>
                /// 当前等级--主键
                /// </summary>
                public int CurrentLevel{get;set;}

                
                /// <summary>
                /// 所需经验
                /// </summary>
                public int Consumption { get;set; }
            } 
            /// <summary>
            /// (佣兵升级经验表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_MercenaryUpgrade
            { 

                static Configs_MercenaryUpgrade _sInstance;
                public static Configs_MercenaryUpgrade sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_MercenaryUpgrade();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (佣兵升级经验表)字典集合
                /// </summary>
                public Dictionary<int, Configs_MercenaryUpgradeData> mMercenaryUpgradeDatas
                {
                    get { return _MercenaryUpgradeDatas; }
                }

                /// <summary>
                /// (佣兵升级经验表)字典集合
                /// </summary>
                Dictionary<int, Configs_MercenaryUpgradeData> _MercenaryUpgradeDatas = new Dictionary<int, Configs_MercenaryUpgradeData>();

                /// <summary>
                /// 根据CurrentLevel读取对应的配置信息
                /// </summary>
                /// <param name="CurrentLevel">配置的CurrentLevel</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_MercenaryUpgradeData GetMercenaryUpgradeDataByCurrentLevel(int CurrentLevel)
                {
                    if (_MercenaryUpgradeDatas.ContainsKey(CurrentLevel))
                    {
                        return _MercenaryUpgradeDatas[CurrentLevel];
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
  Configs_MercenaryUpgradeData cd = new Configs_MercenaryUpgradeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.CurrentLevel = key; 
  cd.Consumption =  Util.GetIntKeyValue(body,"Consumption"); 
  
 if (mMercenaryUpgradeDatas.ContainsKey(key) == false)
 mMercenaryUpgradeDatas.Add(key, cd);
  }
 //Debug.Log(mMercenaryUpgradeDatas.Count);
}

            }