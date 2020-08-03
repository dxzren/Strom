/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (NPC属性调整表)客户端配置结构体
            /// </summary>
            public partial class Configs_NPCAbilityModifierData 
             { 
                /// <summary>
                /// 属性调整ID--主键
                /// </summary>
                public int AdjustID{get;set;}

                
                /// <summary>
                /// 生命
                /// </summary>
                public int Blood { get;set; }
                /// <summary>
                /// 物理攻击
                /// </summary>
                public int PhysicalAttack { get;set; }
                /// <summary>
                /// 魔法强度
                /// </summary>
                public int MagicAttack { get;set; }
                /// <summary>
                /// 物理护甲
                /// </summary>
                public int PhysicalArmor { get;set; }
                /// <summary>
                /// 魔法抗性
                /// </summary>
                public int MagicArmor { get;set; }
            } 
            /// <summary>
            /// (NPC属性调整表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_NPCAbilityModifier
            { 

                static Configs_NPCAbilityModifier _sInstance;
                public static Configs_NPCAbilityModifier sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_NPCAbilityModifier();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (NPC属性调整表)字典集合
                /// </summary>
                public Dictionary<int, Configs_NPCAbilityModifierData> mNPCAbilityModifierDatas
                {
                    get { return _NPCAbilityModifierDatas; }
                }

                /// <summary>
                /// (NPC属性调整表)字典集合
                /// </summary>
                Dictionary<int, Configs_NPCAbilityModifierData> _NPCAbilityModifierDatas = new Dictionary<int, Configs_NPCAbilityModifierData>();

                /// <summary>
                /// 根据AdjustID读取对应的配置信息
                /// </summary>
                /// <param name="AdjustID">配置的AdjustID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_NPCAbilityModifierData GetNPCAbilityModifierDataByAdjustID(int AdjustID)
                {
                    if (_NPCAbilityModifierDatas.ContainsKey(AdjustID))
                    {
                        return _NPCAbilityModifierDatas[AdjustID];
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
  Configs_NPCAbilityModifierData cd = new Configs_NPCAbilityModifierData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.AdjustID = key; 
  cd.Blood =  Util.GetIntKeyValue(body,"Blood"); 
  cd.PhysicalAttack =  Util.GetIntKeyValue(body,"PhysicalAttack"); 
  cd.MagicAttack =  Util.GetIntKeyValue(body,"MagicAttack"); 
  cd.PhysicalArmor =  Util.GetIntKeyValue(body,"PhysicalArmor"); 
  cd.MagicArmor =  Util.GetIntKeyValue(body,"MagicArmor"); 
  
 if (mNPCAbilityModifierDatas.ContainsKey(key) == false)
 mNPCAbilityModifierDatas.Add(key, cd);
  }
 //Debug.Log(mNPCAbilityModifierDatas.Count);
}

            }