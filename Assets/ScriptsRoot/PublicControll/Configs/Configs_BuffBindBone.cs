/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (Buff绑定骨骼位置)客户端配置结构体
            /// </summary>
            public partial class Configs_BuffBindBoneData 
             { 
                /// <summary>
                /// 资源ID--主键
                /// </summary>
                public int ResourceID{get;set;}

                
                /// <summary>
                /// Buff绑定骨骼点1
                /// </summary>
                public string BuffBondBone1 { get;set; }
                /// <summary>
                /// 相对偏移位置1
                /// </summary>
                public List<float> BuffRelatedPos1 { get;set; }
                /// <summary>
                /// 相对旋转位置1
                /// </summary>
                public List<float> BuffRelatedRotate1 { get;set; }
                /// <summary>
                /// Buff绑定骨骼点2
                /// </summary>
                public string BuffBondBone2 { get;set; }
                /// <summary>
                /// 相对偏移位置2
                /// </summary>
                public List<float> BuffRelatedPos2 { get;set; }
                /// <summary>
                /// 相对旋转位置2
                /// </summary>
                public List<float> BuffRelatedRotate2 { get;set; }
            } 
            /// <summary>
            /// (Buff绑定骨骼位置)客户端配置数据集合类
            /// </summary>
            public partial class Configs_BuffBindBone
            { 

                static Configs_BuffBindBone _sInstance;
                public static Configs_BuffBindBone sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_BuffBindBone();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (Buff绑定骨骼位置)字典集合
                /// </summary>
                public Dictionary<int, Configs_BuffBindBoneData> mBuffBindBoneDatas
                {
                    get { return _BuffBindBoneDatas; }
                }

                /// <summary>
                /// (Buff绑定骨骼位置)字典集合
                /// </summary>
                Dictionary<int, Configs_BuffBindBoneData> _BuffBindBoneDatas = new Dictionary<int, Configs_BuffBindBoneData>();

                /// <summary>
                /// 根据ResourceID读取对应的配置信息
                /// </summary>
                /// <param name="ResourceID">配置的ResourceID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_BuffBindBoneData GetBuffBindBoneDataByResourceID(int ResourceID)
                {
                    if (_BuffBindBoneDatas.ContainsKey(ResourceID))
                    {
                        return _BuffBindBoneDatas[ResourceID];
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
  Configs_BuffBindBoneData cd = new Configs_BuffBindBoneData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ResourceID = key; 
  cd.BuffBondBone1 =  Util.GetStringKeyValue(body,"BuffBondBone1"); 
 
 string[] BuffRelatedPos1Strs= Util.GetStringKeyValue(body, "BuffRelatedPos1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BuffRelatedPos1  = new List<float>();
foreach(string BuffRelatedPos1Str in BuffRelatedPos1Strs)  cd.BuffRelatedPos1.Add(Util.ParseToFloat(BuffRelatedPos1Str)); 
 

 string[] BuffRelatedRotate1Strs= Util.GetStringKeyValue(body, "BuffRelatedRotate1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BuffRelatedRotate1  = new List<float>();
foreach(string BuffRelatedRotate1Str in BuffRelatedRotate1Strs)  cd.BuffRelatedRotate1.Add(Util.ParseToFloat(BuffRelatedRotate1Str)); 
 
 cd.BuffBondBone2 =  Util.GetStringKeyValue(body,"BuffBondBone2"); 
 
 string[] BuffRelatedPos2Strs= Util.GetStringKeyValue(body, "BuffRelatedPos2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BuffRelatedPos2  = new List<float>();
foreach(string BuffRelatedPos2Str in BuffRelatedPos2Strs)  cd.BuffRelatedPos2.Add(Util.ParseToFloat(BuffRelatedPos2Str)); 
 

 string[] BuffRelatedRotate2Strs= Util.GetStringKeyValue(body, "BuffRelatedRotate2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BuffRelatedRotate2  = new List<float>();
foreach(string BuffRelatedRotate2Str in BuffRelatedRotate2Strs)  cd.BuffRelatedRotate2.Add(Util.ParseToFloat(BuffRelatedRotate2Str)); 
 
 
 if (mBuffBindBoneDatas.ContainsKey(key) == false)
 mBuffBindBoneDatas.Add(key, cd);
  }
 //Debug.Log(mBuffBindBoneDatas.Count);
}

            }