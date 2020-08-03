/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (场景特效配置)客户端配置结构体
            /// </summary>
            public partial class Configs_SceneEffectInfoData 
             { 
                /// <summary>
                /// 场景资源--主键
                /// </summary>
                public string SceneName{get;set;}

                
                /// <summary>
                /// 场景摄像机配置
                /// </summary>
                public string SceneCameraSetting { get;set; }
                /// <summary>
                /// 场景特效名称
                /// </summary>
                public string SceneEffectName { get;set; }
                /// <summary>
                /// 场景摄像机特效名称
                /// </summary>
                public string SceneCameraEffect { get;set; }
            } 
            /// <summary>
            /// (场景特效配置)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SceneEffectInfo
            { 

                static Configs_SceneEffectInfo _sInstance;
                public static Configs_SceneEffectInfo sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SceneEffectInfo();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (场景特效配置)字典集合
                /// </summary>
                public Dictionary<string, Configs_SceneEffectInfoData> mSceneEffectInfoDatas
                {
                    get { return _SceneEffectInfoDatas; }
                }

                /// <summary>
                /// (场景特效配置)字典集合
                /// </summary>
                Dictionary<string, Configs_SceneEffectInfoData> _SceneEffectInfoDatas = new Dictionary<string, Configs_SceneEffectInfoData>();

                /// <summary>
                /// 根据SceneName读取对应的配置信息
                /// </summary>
                /// <param name="SceneName">配置的SceneName</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SceneEffectInfoData GetSceneEffectInfoDataBySceneName(string SceneName)
                {
                    if (_SceneEffectInfoDatas.ContainsKey(SceneName))
                    {
                        return _SceneEffectInfoDatas[SceneName];
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
  Configs_SceneEffectInfoData cd = new Configs_SceneEffectInfoData(); 
  string key = element.Key; 
  cd.SceneName = key; 
  cd.SceneCameraSetting =  Util.GetStringKeyValue(body,"SceneCameraSetting"); 
  cd.SceneEffectName =  Util.GetStringKeyValue(body,"SceneEffectName"); 
  cd.SceneCameraEffect =  Util.GetStringKeyValue(body,"SceneCameraEffect"); 
  
 if (mSceneEffectInfoDatas.ContainsKey(key) == false)
 mSceneEffectInfoDatas.Add(key, cd);
  }
 //Debug.Log(mSceneEffectInfoDatas.Count);
}

            }