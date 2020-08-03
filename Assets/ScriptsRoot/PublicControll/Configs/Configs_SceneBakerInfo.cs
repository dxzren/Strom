/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (场景烘焙贴图相关信息)客户端配置结构体
            /// </summary>
            public partial class Configs_SceneBakerInfoData 
             { 
                /// <summary>
                /// 场景资源--主键
                /// </summary>
                public string SceneName{get;set;}

                
                /// <summary>
                /// 场景烘焙贴图数量
                /// </summary>
                public int SceneBakerNum { get;set; }
                /// <summary>
                /// 场景烘焙贴图名称1
                /// </summary>
                public string SceneBakerName1 { get;set; }
                /// <summary>
                /// 场景烘焙贴图序号1
                /// </summary>
                public int SceneBakerIndex1 { get;set; }
                /// <summary>
                /// 场景烘焙贴图偏移1
                /// </summary>
                public List<float> SceneBakerOffsetPos1 { get;set; }
                /// <summary>
                /// 场景烘焙贴图名称2
                /// </summary>
                public string SceneBakerName2 { get;set; }
                /// <summary>
                /// 场景烘焙贴图序号2
                /// </summary>
                public int SceneBakerIndex2 { get;set; }
                /// <summary>
                /// 场景烘焙贴图偏移2
                /// </summary>
                public List<float> SceneBakerOffsetPos2 { get;set; }
                /// <summary>
                /// 场景烘焙贴图名称3
                /// </summary>
                public string SceneBakerName3 { get;set; }
                /// <summary>
                /// 场景烘焙贴图序号3
                /// </summary>
                public int SceneBakerIndex3 { get;set; }
                /// <summary>
                /// 场景烘焙贴图偏移3
                /// </summary>
                public List<float> SceneBakerOffsetPos3 { get;set; }
                /// <summary>
                /// 场景烘焙贴图名称4
                /// </summary>
                public string SceneBakerName4 { get;set; }
                /// <summary>
                /// 场景烘焙贴图序号4
                /// </summary>
                public int SceneBakerIndex4 { get;set; }
                /// <summary>
                /// 场景烘焙贴图偏移4
                /// </summary>
                public List<float> SceneBakerOffsetPos4 { get;set; }
                /// <summary>
                /// 场景烘焙贴图名称5
                /// </summary>
                public string SceneBakerName5 { get;set; }
                /// <summary>
                /// 场景烘焙贴图序号5
                /// </summary>
                public int SceneBakerIndex5 { get;set; }
                /// <summary>
                /// 场景烘焙贴图偏移5
                /// </summary>
                public List<float> SceneBakerOffsetPos5 { get;set; }
                /// <summary>
                /// 场景烘焙贴图名称6
                /// </summary>
                public string SceneBakerName6 { get;set; }
                /// <summary>
                /// 场景烘焙贴图序号6
                /// </summary>
                public int SceneBakerIndex6 { get;set; }
                /// <summary>
                /// 场景烘焙贴图偏移6
                /// </summary>
                public List<float> SceneBakerOffsetPos6 { get;set; }
            } 
            /// <summary>
            /// (场景烘焙贴图相关信息)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SceneBakerInfo
            { 

                static Configs_SceneBakerInfo _sInstance;
                public static Configs_SceneBakerInfo sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SceneBakerInfo();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (场景烘焙贴图相关信息)字典集合
                /// </summary>
                public Dictionary<string, Configs_SceneBakerInfoData> mSceneBakerInfoDatas
                {
                    get { return _SceneBakerInfoDatas; }
                }

                /// <summary>
                /// (场景烘焙贴图相关信息)字典集合
                /// </summary>
                Dictionary<string, Configs_SceneBakerInfoData> _SceneBakerInfoDatas = new Dictionary<string, Configs_SceneBakerInfoData>();

                /// <summary>
                /// 根据SceneName读取对应的配置信息
                /// </summary>
                /// <param name="SceneName">配置的SceneName</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SceneBakerInfoData GetSceneBakerInfoDataBySceneName(string SceneName)
                {
                    if (_SceneBakerInfoDatas.ContainsKey(SceneName))
                    {
                        return _SceneBakerInfoDatas[SceneName];
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
  Configs_SceneBakerInfoData cd = new Configs_SceneBakerInfoData(); 
  string key = element.Key; 
  cd.SceneName = key; 
  cd.SceneBakerNum =  Util.GetIntKeyValue(body,"SceneBakerNum"); 
  cd.SceneBakerName1 =  Util.GetStringKeyValue(body,"SceneBakerName1"); 
  cd.SceneBakerIndex1 =  Util.GetIntKeyValue(body,"SceneBakerIndex1"); 
 
 string[] SceneBakerOffsetPos1Strs= Util.GetStringKeyValue(body, "SceneBakerOffsetPos1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.SceneBakerOffsetPos1  = new List<float>();
foreach(string SceneBakerOffsetPos1Str in SceneBakerOffsetPos1Strs)  cd.SceneBakerOffsetPos1.Add(Util.ParseToFloat(SceneBakerOffsetPos1Str)); 
 
 cd.SceneBakerName2 =  Util.GetStringKeyValue(body,"SceneBakerName2"); 
  cd.SceneBakerIndex2 =  Util.GetIntKeyValue(body,"SceneBakerIndex2"); 
 
 string[] SceneBakerOffsetPos2Strs= Util.GetStringKeyValue(body, "SceneBakerOffsetPos2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.SceneBakerOffsetPos2  = new List<float>();
foreach(string SceneBakerOffsetPos2Str in SceneBakerOffsetPos2Strs)  cd.SceneBakerOffsetPos2.Add(Util.ParseToFloat(SceneBakerOffsetPos2Str)); 
 
 cd.SceneBakerName3 =  Util.GetStringKeyValue(body,"SceneBakerName3"); 
  cd.SceneBakerIndex3 =  Util.GetIntKeyValue(body,"SceneBakerIndex3"); 
 
 string[] SceneBakerOffsetPos3Strs= Util.GetStringKeyValue(body, "SceneBakerOffsetPos3").TrimStart('{').TrimEnd('}',',').Split(',');
cd.SceneBakerOffsetPos3  = new List<float>();
foreach(string SceneBakerOffsetPos3Str in SceneBakerOffsetPos3Strs)  cd.SceneBakerOffsetPos3.Add(Util.ParseToFloat(SceneBakerOffsetPos3Str)); 
 
 cd.SceneBakerName4 =  Util.GetStringKeyValue(body,"SceneBakerName4"); 
  cd.SceneBakerIndex4 =  Util.GetIntKeyValue(body,"SceneBakerIndex4"); 
 
 string[] SceneBakerOffsetPos4Strs= Util.GetStringKeyValue(body, "SceneBakerOffsetPos4").TrimStart('{').TrimEnd('}',',').Split(',');
cd.SceneBakerOffsetPos4  = new List<float>();
foreach(string SceneBakerOffsetPos4Str in SceneBakerOffsetPos4Strs)  cd.SceneBakerOffsetPos4.Add(Util.ParseToFloat(SceneBakerOffsetPos4Str)); 
 
 cd.SceneBakerName5 =  Util.GetStringKeyValue(body,"SceneBakerName5"); 
  cd.SceneBakerIndex5 =  Util.GetIntKeyValue(body,"SceneBakerIndex5"); 
 
 string[] SceneBakerOffsetPos5Strs= Util.GetStringKeyValue(body, "SceneBakerOffsetPos5").TrimStart('{').TrimEnd('}',',').Split(',');
cd.SceneBakerOffsetPos5  = new List<float>();
foreach(string SceneBakerOffsetPos5Str in SceneBakerOffsetPos5Strs)  cd.SceneBakerOffsetPos5.Add(Util.ParseToFloat(SceneBakerOffsetPos5Str)); 
 
 cd.SceneBakerName6 =  Util.GetStringKeyValue(body,"SceneBakerName6"); 
  cd.SceneBakerIndex6 =  Util.GetIntKeyValue(body,"SceneBakerIndex6"); 
 
 string[] SceneBakerOffsetPos6Strs= Util.GetStringKeyValue(body, "SceneBakerOffsetPos6").TrimStart('{').TrimEnd('}',',').Split(',');
cd.SceneBakerOffsetPos6  = new List<float>();
foreach(string SceneBakerOffsetPos6Str in SceneBakerOffsetPos6Strs)  cd.SceneBakerOffsetPos6.Add(Util.ParseToFloat(SceneBakerOffsetPos6Str)); 
 
 
 if (mSceneBakerInfoDatas.ContainsKey(key) == false)
 mSceneBakerInfoDatas.Add(key, cd);
  }
 //Debug.Log(mSceneBakerInfoDatas.Count);
}

            }