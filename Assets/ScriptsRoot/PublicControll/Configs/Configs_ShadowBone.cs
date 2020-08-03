/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (影子骨骼路径)客户端配置结构体
            /// </summary>
            public partial class Configs_ShadowBoneData 
             { 
                /// <summary>
                /// 资源ID--主键
                /// </summary>
                public int ResourceID{get;set;}

                
                /// <summary>
                /// 影子骨骼路径
                /// </summary>
                public string ShadowBonePath { get;set; }
            } 
            /// <summary>
            /// (影子骨骼路径)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ShadowBone
            { 

                static Configs_ShadowBone _sInstance;
                public static Configs_ShadowBone sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ShadowBone();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (影子骨骼路径)字典集合
                /// </summary>
                public Dictionary<int, Configs_ShadowBoneData> mShadowBoneDatas
                {
                    get { return _ShadowBoneDatas; }
                }

                /// <summary>
                /// (影子骨骼路径)字典集合
                /// </summary>
                Dictionary<int, Configs_ShadowBoneData> _ShadowBoneDatas = new Dictionary<int, Configs_ShadowBoneData>();

                /// <summary>
                /// 根据ResourceID读取对应的配置信息
                /// </summary>
                /// <param name="ResourceID">配置的ResourceID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ShadowBoneData GetShadowBoneDataByResourceID(int ResourceID)
                {
                    if (_ShadowBoneDatas.ContainsKey(ResourceID))
                    {
                        return _ShadowBoneDatas[ResourceID];
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
  Configs_ShadowBoneData cd = new Configs_ShadowBoneData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ResourceID = key; 
  cd.ShadowBonePath =  Util.GetStringKeyValue(body,"ShadowBonePath"); 
  
 if (mShadowBoneDatas.ContainsKey(key) == false)
 mShadowBoneDatas.Add(key, cd);
  }
 //Debug.Log(mShadowBoneDatas.Count);
}

            }