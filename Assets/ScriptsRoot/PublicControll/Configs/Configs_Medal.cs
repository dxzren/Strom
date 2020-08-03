/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (勋章属性及升级所需经验)客户端配置结构体
            /// </summary>
            public partial class Configs_MedalData 
             { 
                /// <summary>
                /// 勋章阶数--主键
                /// </summary>
                public int MedalLevel{get;set;}

                
                /// <summary>
                /// 勋章名
                /// </summary>
                public string MedalName { get;set; }
                /// <summary>
                /// 力量百分比
                /// </summary>
                public int StrengthPercent { get;set; }
                /// <summary>
                /// 敏捷百分比
                /// </summary>
                public int AgilityPercent { get;set; }
                /// <summary>
                /// 智力百分比
                /// </summary>
                public int MentalityPercent { get;set; }
                /// <summary>
                /// 经验
                /// </summary>
                public int Exp { get;set; }
                /// <summary>
                /// 勋章图标_100
                /// </summary>
                public string MedalIcon_100 { get;set; }
                /// <summary>
                /// 勋章图标_155
                /// </summary>
                public string MedalIcon_155 { get;set; }
            } 
            /// <summary>
            /// (勋章属性及升级所需经验)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Medal
            { 

                static Configs_Medal _sInstance;
                public static Configs_Medal sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Medal();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (勋章属性及升级所需经验)字典集合
                /// </summary>
                public Dictionary<int, Configs_MedalData> mMedalDatas
                {
                    get { return _MedalDatas; }
                }

                /// <summary>
                /// (勋章属性及升级所需经验)字典集合
                /// </summary>
                Dictionary<int, Configs_MedalData> _MedalDatas = new Dictionary<int, Configs_MedalData>();

                /// <summary>
                /// 根据MedalLevel读取对应的配置信息
                /// </summary>
                /// <param name="MedalLevel">配置的MedalLevel</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_MedalData GetMedalDataByMedalLevel(int MedalLevel)
                {
                    if (_MedalDatas.ContainsKey(MedalLevel))
                    {
                        return _MedalDatas[MedalLevel];
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
  Configs_MedalData cd = new Configs_MedalData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.MedalLevel = key; 
  cd.MedalName =  Util.GetStringKeyValue(body,"MedalName"); 
  cd.StrengthPercent =  Util.GetIntKeyValue(body,"StrengthPercent"); 
  cd.AgilityPercent =  Util.GetIntKeyValue(body,"AgilityPercent"); 
  cd.MentalityPercent =  Util.GetIntKeyValue(body,"MentalityPercent"); 
  cd.Exp =  Util.GetIntKeyValue(body,"Exp"); 
  cd.MedalIcon_100 =  Util.GetStringKeyValue(body,"MedalIcon_100"); 
  cd.MedalIcon_155 =  Util.GetStringKeyValue(body,"MedalIcon_155"); 
  
 if (mMedalDatas.ContainsKey(key) == false)
 mMedalDatas.Add(key, cd);
  }
 //Debug.Log(mMedalDatas.Count);
}

            }