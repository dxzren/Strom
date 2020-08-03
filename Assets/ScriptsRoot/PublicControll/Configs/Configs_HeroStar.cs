/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (英雄星级)客户端配置结构体
            /// </summary>
            public partial class Configs_HeroStarData 
             { 
                /// <summary>
                /// 星级ID--主键
                /// </summary>
                public int StarID{get;set;}

                
                /// <summary>
                /// 力量成长
                /// </summary>
                public float StrengthGrowing { get;set; }
                /// <summary>
                /// 敏捷成长
                /// </summary>
                public float AgilityGrowing { get;set; }
                /// <summary>
                /// 智力成长
                /// </summary>
                public float MentalityGrowing { get;set; }
            } 
            /// <summary>
            /// (英雄星级)客户端配置数据集合类
            /// </summary>
            public partial class Configs_HeroStar
            { 

                static Configs_HeroStar _sInstance;
                public static Configs_HeroStar sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_HeroStar();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (英雄星级)字典集合
                /// </summary>
                public Dictionary<int, Configs_HeroStarData> mHeroStarDatas
                {
                    get { return _HeroStarDatas; }
                }

                /// <summary>
                /// (英雄星级)字典集合
                /// </summary>
                Dictionary<int, Configs_HeroStarData> _HeroStarDatas = new Dictionary<int, Configs_HeroStarData>();

                /// <summary>
                /// 根据StarID读取对应的配置信息
                /// </summary>
                /// <param name="StarID">配置的StarID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_HeroStarData GetHeroStarDataByStarID(int StarID)
                {
                    if (_HeroStarDatas.ContainsKey(StarID))
                    {
                        return _HeroStarDatas[StarID];
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
  Configs_HeroStarData cd = new Configs_HeroStarData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.StarID = key; 
  cd.StrengthGrowing =  Util.GetFloatKeyValue(body,"StrengthGrowing"); 
  cd.AgilityGrowing =  Util.GetFloatKeyValue(body,"AgilityGrowing"); 
  cd.MentalityGrowing =  Util.GetFloatKeyValue(body,"MentalityGrowing"); 
  
 if (mHeroStarDatas.ContainsKey(key) == false)
 mHeroStarDatas.Add(key, cd);
  }
 //Debug.Log(mHeroStarDatas.Count);
}

            }