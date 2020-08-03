/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (抽卡英雄卡库)客户端配置结构体
            /// </summary>
            public partial class Configs_LibraryCardData 
             { 
                /// <summary>
                /// 英雄ID--主键
                /// </summary>
                public int HeroID{get;set;}

                
                /// <summary>
                /// 初始星级
                /// </summary>
                public int InitialStar { get;set; }
                /// <summary>
                /// 权重
                /// </summary>
                public int Weight { get;set; }
                /// <summary>
                /// 人类标识
                /// </summary>
                public int Human { get;set; }
                /// <summary>
                /// 精灵标识
                /// </summary>
                public int ELF { get;set; }
                /// <summary>
                /// 矮人标识
                /// </summary>
                public int Dwarves { get;set; }
            } 
            /// <summary>
            /// (抽卡英雄卡库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_LibraryCard
            { 

                static Configs_LibraryCard _sInstance;
                public static Configs_LibraryCard sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_LibraryCard();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (抽卡英雄卡库)字典集合
                /// </summary>
                public Dictionary<int, Configs_LibraryCardData> mLibraryCardDatas
                {
                    get { return _LibraryCardDatas; }
                }

                /// <summary>
                /// (抽卡英雄卡库)字典集合
                /// </summary>
                Dictionary<int, Configs_LibraryCardData> _LibraryCardDatas = new Dictionary<int, Configs_LibraryCardData>();

                /// <summary>
                /// 根据HeroID读取对应的配置信息
                /// </summary>
                /// <param name="HeroID">配置的HeroID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_LibraryCardData GetLibraryCardDataByHeroID(int HeroID)
                {
                    if (_LibraryCardDatas.ContainsKey(HeroID))
                    {
                        return _LibraryCardDatas[HeroID];
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
  Configs_LibraryCardData cd = new Configs_LibraryCardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.HeroID = key; 
  cd.InitialStar =  Util.GetIntKeyValue(body,"InitialStar"); 
  cd.Weight =  Util.GetIntKeyValue(body,"Weight"); 
  cd.Human =  Util.GetIntKeyValue(body,"Human"); 
  cd.ELF =  Util.GetIntKeyValue(body,"ELF"); 
  cd.Dwarves =  Util.GetIntKeyValue(body,"Dwarves"); 
  
 if (mLibraryCardDatas.ContainsKey(key) == false)
 mLibraryCardDatas.Add(key, cd);
  }
 //Debug.Log(mLibraryCardDatas.Count);
}

            }