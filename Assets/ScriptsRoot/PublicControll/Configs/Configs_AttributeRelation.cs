/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (属性关系)客户端配置结构体
            /// </summary>
            public partial class Configs_AttributeRelationData 
             { 
                /// <summary>
                /// 序号--主键
                /// </summary>
                public int Num{get;set;}

                
                /// <summary>
                /// 职业类型
                /// </summary>
                public int Profession { get;set; }
                /// <summary>
                /// 属性
                /// </summary>
                public int Attribute { get;set; }
                /// <summary>
                /// 生命
                /// </summary>
                public int Blood { get;set; }
                /// <summary>
                /// 物理攻击
                /// </summary>
                public float PhysicalAttack { get;set; }
                /// <summary>
                /// 魔法强度
                /// </summary>
                public float MagicAttack { get;set; }
                /// <summary>
                /// 物理护甲
                /// </summary>
                public float PhysicalArmor { get;set; }
                /// <summary>
                /// 魔法抗性
                /// </summary>
                public float MagicArmor { get;set; }
                /// <summary>
                /// 物理暴击
                /// </summary>
                public float PhysicalCritical { get;set; }
            } 
            /// <summary>
            /// (属性关系)客户端配置数据集合类
            /// </summary>
            public partial class Configs_AttributeRelation
            { 

                static Configs_AttributeRelation _sInstance;
                public static Configs_AttributeRelation sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_AttributeRelation();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (属性关系)字典集合
                /// </summary>
                public Dictionary<int, Configs_AttributeRelationData> mAttributeRelationDatas
                {
                    get { return _AttributeRelationDatas; }
                }

                /// <summary>
                /// (属性关系)字典集合
                /// </summary>
                Dictionary<int, Configs_AttributeRelationData> _AttributeRelationDatas = new Dictionary<int, Configs_AttributeRelationData>();

                /// <summary>
                /// 根据Num读取对应的配置信息
                /// </summary>
                /// <param name="Num">配置的Num</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_AttributeRelationData GetAttributeRelationDataByNum(int Num)
                {
                    if (_AttributeRelationDatas.ContainsKey(Num))
                    {
                        return _AttributeRelationDatas[Num];
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
  Configs_AttributeRelationData cd = new Configs_AttributeRelationData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.Num = key; 
  cd.Profession =  Util.GetIntKeyValue(body,"Profession"); 
  cd.Attribute =  Util.GetIntKeyValue(body,"Attribute"); 
  cd.Blood =  Util.GetIntKeyValue(body,"Blood"); 
  cd.PhysicalAttack =  Util.GetFloatKeyValue(body,"PhysicalAttack"); 
  cd.MagicAttack =  Util.GetFloatKeyValue(body,"MagicAttack"); 
  cd.PhysicalArmor =  Util.GetFloatKeyValue(body,"PhysicalArmor"); 
  cd.MagicArmor =  Util.GetFloatKeyValue(body,"MagicArmor"); 
  cd.PhysicalCritical =  Util.GetFloatKeyValue(body,"PhysicalCritical"); 
  
 if (mAttributeRelationDatas.ContainsKey(key) == false)
 mAttributeRelationDatas.Add(key, cd);
  }
 //Debug.Log(mAttributeRelationDatas.Count);
}

            }