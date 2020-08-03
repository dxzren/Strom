/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (勋章等级上限)客户端配置结构体
            /// </summary>
            public partial class Configs_MedalUpperData 
             { 
                /// <summary>
                /// 英雄品质--主键
                /// </summary>
                public int HeroQuality{get;set;}

                
                /// <summary>
                /// 勋章阶数上限
                /// </summary>
                public int MedalLevelUpper { get;set; }
            } 
            /// <summary>
            /// (勋章等级上限)客户端配置数据集合类
            /// </summary>
            public partial class Configs_MedalUpper
            { 

                static Configs_MedalUpper _sInstance;
                public static Configs_MedalUpper sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_MedalUpper();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (勋章等级上限)字典集合
                /// </summary>
                public Dictionary<int, Configs_MedalUpperData> mMedalUpperDatas
                {
                    get { return _MedalUpperDatas; }
                }

                /// <summary>
                /// (勋章等级上限)字典集合
                /// </summary>
                Dictionary<int, Configs_MedalUpperData> _MedalUpperDatas = new Dictionary<int, Configs_MedalUpperData>();

                /// <summary>
                /// 根据HeroQuality读取对应的配置信息
                /// </summary>
                /// <param name="HeroQuality">配置的HeroQuality</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_MedalUpperData GetMedalUpperDataByHeroQuality(int HeroQuality)
                {
                    if (_MedalUpperDatas.ContainsKey(HeroQuality))
                    {
                        return _MedalUpperDatas[HeroQuality];
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
  Configs_MedalUpperData cd = new Configs_MedalUpperData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.HeroQuality = key; 
  cd.MedalLevelUpper =  Util.GetIntKeyValue(body,"MedalLevelUpper"); 
  
 if (mMedalUpperDatas.ContainsKey(key) == false)
 mMedalUpperDatas.Add(key, cd);
  }
 //Debug.Log(mMedalUpperDatas.Count);
}

            }