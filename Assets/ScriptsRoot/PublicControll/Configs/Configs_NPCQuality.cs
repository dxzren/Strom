/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (NPC品质表)客户端配置结构体
            /// </summary>
            public partial class Configs_NPCQualityData 
             { 
                /// <summary>
                /// 品质ID--主键
                /// </summary>
                public int QualityID{get;set;}

                
                /// <summary>
                /// 1力量
                /// </summary>
                public int Strength { get;set; }
                /// <summary>
                /// 2敏捷
                /// </summary>
                public int Agility { get;set; }
                /// <summary>
                /// 3智力
                /// </summary>
                public int Mentality { get;set; }
                /// <summary>
                /// 4生命
                /// </summary>
                public int Blood { get;set; }
                /// <summary>
                /// 5物理攻击
                /// </summary>
                public int PhysicalAttack { get;set; }
                /// <summary>
                /// 6魔法强度
                /// </summary>
                public int MagicAttack { get;set; }
                /// <summary>
                /// 7物理护甲
                /// </summary>
                public int PhysicalArmor { get;set; }
                /// <summary>
                /// 11物理暴击
                /// </summary>
                public int PhysicalCrit { get;set; }
                /// <summary>
                /// 12魔法暴击
                /// </summary>
                public int MagicCrit { get;set; }
                /// <summary>
                /// 13穿透物理护甲
                /// </summary>
                public int PenetratePhysicalArmor { get;set; }
                /// <summary>
                /// 14能量回复
                /// </summary>
                public int EnergyRegen { get;set; }
                /// <summary>
                /// 15生命回复
                /// </summary>
                public int BloodRegen { get;set; }
                /// <summary>
                /// 16吸血效果
                /// </summary>
                public int SuckBlood { get;set; }
                /// <summary>
                /// 17命中
                /// </summary>
                public int Hit { get;set; }
                /// <summary>
                /// 18闪避
                /// </summary>
                public int Dodge { get;set; }
                /// <summary>
                /// 19魔法抗性
                /// </summary>
                public int MagicArmor { get;set; }
            } 
            /// <summary>
            /// (NPC品质表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_NPCQuality
            { 

                static Configs_NPCQuality _sInstance;
                public static Configs_NPCQuality sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_NPCQuality();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (NPC品质表)字典集合
                /// </summary>
                public Dictionary<int, Configs_NPCQualityData> mNPCQualityDatas
                {
                    get { return _NPCQualityDatas; }
                }

                /// <summary>
                /// (NPC品质表)字典集合
                /// </summary>
                Dictionary<int, Configs_NPCQualityData> _NPCQualityDatas = new Dictionary<int, Configs_NPCQualityData>();

                /// <summary>
                /// 根据QualityID读取对应的配置信息
                /// </summary>
                /// <param name="QualityID">配置的QualityID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_NPCQualityData GetNPCQualityDataByQualityID(int QualityID)
                {
                    if (_NPCQualityDatas.ContainsKey(QualityID))
                    {
                        return _NPCQualityDatas[QualityID];
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
  Configs_NPCQualityData cd = new Configs_NPCQualityData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.QualityID = key; 
  cd.Strength =  Util.GetIntKeyValue(body,"Strength"); 
  cd.Agility =  Util.GetIntKeyValue(body,"Agility"); 
  cd.Mentality =  Util.GetIntKeyValue(body,"Mentality"); 
  cd.Blood =  Util.GetIntKeyValue(body,"Blood"); 
  cd.PhysicalAttack =  Util.GetIntKeyValue(body,"PhysicalAttack"); 
  cd.MagicAttack =  Util.GetIntKeyValue(body,"MagicAttack"); 
  cd.PhysicalArmor =  Util.GetIntKeyValue(body,"PhysicalArmor"); 
  cd.PhysicalCrit =  Util.GetIntKeyValue(body,"PhysicalCrit"); 
  cd.MagicCrit =  Util.GetIntKeyValue(body,"MagicCrit"); 
  cd.PenetratePhysicalArmor =  Util.GetIntKeyValue(body,"PenetratePhysicalArmor"); 
  cd.EnergyRegen =  Util.GetIntKeyValue(body,"EnergyRegen"); 
  cd.BloodRegen =  Util.GetIntKeyValue(body,"BloodRegen"); 
  cd.SuckBlood =  Util.GetIntKeyValue(body,"SuckBlood"); 
  cd.Hit =  Util.GetIntKeyValue(body,"Hit"); 
  cd.Dodge =  Util.GetIntKeyValue(body,"Dodge"); 
  cd.MagicArmor =  Util.GetIntKeyValue(body,"MagicArmor"); 
  
 if (mNPCQualityDatas.ContainsKey(key) == false)
 mNPCQualityDatas.Add(key, cd);
  }
 //Debug.Log(mNPCQualityDatas.Count);
}

            }