/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (场景内动画)客户端配置结构体
            /// </summary>
            public partial class Configs_SceneAnimationData 
             { 
                /// <summary>
                /// 场景动画资源路径--主键
                /// </summary>
                public string SceneAnimationPath{get;set;}

                
                /// <summary>
                /// 场景动画名称
                /// </summary>
                public string SceneAnimationName { get;set; }
            } 
            /// <summary>
            /// (场景内动画)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SceneAnimation
            { 

                static Configs_SceneAnimation _sInstance;
                public static Configs_SceneAnimation sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SceneAnimation();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (场景内动画)字典集合
                /// </summary>
                public Dictionary<string, Configs_SceneAnimationData> mSceneAnimationDatas
                {
                    get { return _SceneAnimationDatas; }
                }

                /// <summary>
                /// (场景内动画)字典集合
                /// </summary>
                Dictionary<string, Configs_SceneAnimationData> _SceneAnimationDatas = new Dictionary<string, Configs_SceneAnimationData>();

                /// <summary>
                /// 根据SceneAnimationPath读取对应的配置信息
                /// </summary>
                /// <param name="SceneAnimationPath">配置的SceneAnimationPath</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SceneAnimationData GetSceneAnimationDataBySceneAnimationPath(string SceneAnimationPath)
                {
                    if (_SceneAnimationDatas.ContainsKey(SceneAnimationPath))
                    {
                        return _SceneAnimationDatas[SceneAnimationPath];
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
  Configs_SceneAnimationData cd = new Configs_SceneAnimationData(); 
  string key = element.Key; 
  cd.SceneAnimationPath = key; 
  cd.SceneAnimationName =  Util.GetStringKeyValue(body,"SceneAnimationName"); 
  
 if (mSceneAnimationDatas.ContainsKey(key) == false)
 mSceneAnimationDatas.Add(key, cd);
  }
 //Debug.Log(mSceneAnimationDatas.Count);
}

            }