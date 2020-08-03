/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (主角升级表及体力上限)客户端配置结构体
            /// </summary>
            public partial class Configs_LeadingUpgradeData 
             { 
                /// <summary>
                /// 当前等级--主键
                /// </summary>
                public int CurrentLevel{get;set;}

                
                /// <summary>
                /// 所需经验
                /// </summary>
                public int Consumption { get;set; }
                /// <summary>
                /// 体力上限
                /// </summary>
                public int PhysicalLimit { get;set; }
                /// <summary>
                /// 赠送体力
                /// </summary>
                public int GivePhysical { get;set; }
                /// <summary>
                /// 解锁功能
                /// </summary>
                public List<string> UnlockSystem { get;set; }
            } 
            /// <summary>
            /// (主角升级表及体力上限)客户端配置数据集合类
            /// </summary>
            public partial class Configs_LeadingUpgrade
            { 

                static Configs_LeadingUpgrade _sInstance;
                public static Configs_LeadingUpgrade sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_LeadingUpgrade();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (主角升级表及体力上限)字典集合
                /// </summary>
                public Dictionary<int, Configs_LeadingUpgradeData> mLeadingUpgradeDatas
                {
                    get { return _LeadingUpgradeDatas; }
                }

                /// <summary>
                /// (主角升级表及体力上限)字典集合
                /// </summary>
                Dictionary<int, Configs_LeadingUpgradeData> _LeadingUpgradeDatas = new Dictionary<int, Configs_LeadingUpgradeData>();

                /// <summary>
                /// 根据CurrentLevel读取对应的配置信息
                /// </summary>
                /// <param name="CurrentLevel">配置的CurrentLevel</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_LeadingUpgradeData GetLeadingUpgradeDataByCurrentLevel(int CurrentLevel)
                {
                    if (_LeadingUpgradeDatas.ContainsKey(CurrentLevel))
                    {
                        return _LeadingUpgradeDatas[CurrentLevel];
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
  Configs_LeadingUpgradeData cd = new Configs_LeadingUpgradeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.CurrentLevel = key; 
  cd.Consumption =  Util.GetIntKeyValue(body,"Consumption"); 
  cd.PhysicalLimit =  Util.GetIntKeyValue(body,"PhysicalLimit"); 
  cd.GivePhysical =  Util.GetIntKeyValue(body,"GivePhysical"); 
 
 string[] UnlockSystemStrs= Util.GetStringKeyValue(body, "UnlockSystem").TrimStart('{').TrimEnd('}',',').Split(',');
cd.UnlockSystem = new List<string>();
foreach(string UnlockSystemStr in UnlockSystemStrs)  cd.UnlockSystem.Add(UnlockSystemStr); 
 
 
 if (mLeadingUpgradeDatas.ContainsKey(key) == false)
 mLeadingUpgradeDatas.Add(key, cd);
  }
 //Debug.Log(mLeadingUpgradeDatas.Count);
}

            }