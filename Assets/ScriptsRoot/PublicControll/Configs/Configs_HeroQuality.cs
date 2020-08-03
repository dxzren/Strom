/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (英雄品质)客户端配置结构体
            /// </summary>
            public partial class Configs_HeroQualityData 
             { 
                /// <summary>
                /// 品质ID--主键
                /// </summary>
                public int QualityID{get;set;}

                
                /// <summary>
                /// 力量
                /// </summary>
                public int Strength { get;set; }
                /// <summary>
                /// 敏捷
                /// </summary>
                public int Agility { get;set; }
                /// <summary>
                /// 智力
                /// </summary>
                public int Mentality { get;set; }
                /// <summary>
                /// 装备1
                /// </summary>
                public int Equip1 { get;set; }
                /// <summary>
                /// 装备2
                /// </summary>
                public int Equip2 { get;set; }
                /// <summary>
                /// 装备3
                /// </summary>
                public int Equip3 { get;set; }
                /// <summary>
                /// 装备4
                /// </summary>
                public int Equip4 { get;set; }
                /// <summary>
                /// 装备5
                /// </summary>
                public int Equip5 { get;set; }
                /// <summary>
                /// 装备6
                /// </summary>
                public int Equip6 { get;set; }
            } 
            /// <summary>
            /// (英雄品质)客户端配置数据集合类
            /// </summary>
            public partial class Configs_HeroQuality
            { 

                static Configs_HeroQuality _sInstance;
                public static Configs_HeroQuality sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_HeroQuality();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (英雄品质)字典集合
                /// </summary>
                public Dictionary<int, Configs_HeroQualityData> mHeroQualityDatas
                {
                    get { return _HeroQualityDatas; }
                }

                /// <summary>
                /// (英雄品质)字典集合
                /// </summary>
                Dictionary<int, Configs_HeroQualityData> _HeroQualityDatas = new Dictionary<int, Configs_HeroQualityData>();

                /// <summary>
                /// 根据QualityID读取对应的配置信息
                /// </summary>
                /// <param name="QualityID">配置的QualityID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_HeroQualityData GetHeroQualityDataByQualityID(int QualityID)
                {
                    if (_HeroQualityDatas.ContainsKey(QualityID))
                    {
                        return _HeroQualityDatas[QualityID];
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
  Configs_HeroQualityData cd = new Configs_HeroQualityData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.QualityID = key; 
  cd.Strength =  Util.GetIntKeyValue(body,"Strength"); 
  cd.Agility =  Util.GetIntKeyValue(body,"Agility"); 
  cd.Mentality =  Util.GetIntKeyValue(body,"Mentality"); 
  cd.Equip1 =  Util.GetIntKeyValue(body,"Equip1"); 
  cd.Equip2 =  Util.GetIntKeyValue(body,"Equip2"); 
  cd.Equip3 =  Util.GetIntKeyValue(body,"Equip3"); 
  cd.Equip4 =  Util.GetIntKeyValue(body,"Equip4"); 
  cd.Equip5 =  Util.GetIntKeyValue(body,"Equip5"); 
  cd.Equip6 =  Util.GetIntKeyValue(body,"Equip6"); 
  
 if (mHeroQualityDatas.ContainsKey(key) == false)
 mHeroQualityDatas.Add(key, cd);
  }
 //Debug.Log(mHeroQualityDatas.Count);
}

            }