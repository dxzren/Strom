/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公会图标表)客户端配置结构体
            /// </summary>
            public partial class Configs_SocietyIconData 
             { 
                /// <summary>
                /// 图标ID--主键
                /// </summary>
                public int IconID{get;set;}

                
                /// <summary>
                /// 图集标识
                /// </summary>
                public int IconSign { get;set; }
                /// <summary>
                /// 图标84
                /// </summary>
                public string Icon84 { get;set; }
            } 
            /// <summary>
            /// (公会图标表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SocietyIcon
            { 

                static Configs_SocietyIcon _sInstance;
                public static Configs_SocietyIcon sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SocietyIcon();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公会图标表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SocietyIconData> mSocietyIconDatas
                {
                    get { return _SocietyIconDatas; }
                }

                /// <summary>
                /// (公会图标表)字典集合
                /// </summary>
                Dictionary<int, Configs_SocietyIconData> _SocietyIconDatas = new Dictionary<int, Configs_SocietyIconData>();

                /// <summary>
                /// 根据IconID读取对应的配置信息
                /// </summary>
                /// <param name="IconID">配置的IconID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SocietyIconData GetSocietyIconDataByIconID(int IconID)
                {
                    if (_SocietyIconDatas.ContainsKey(IconID))
                    {
                        return _SocietyIconDatas[IconID];
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
  Configs_SocietyIconData cd = new Configs_SocietyIconData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.IconID = key; 
  cd.IconSign =  Util.GetIntKeyValue(body,"IconSign"); 
  cd.Icon84 =  Util.GetStringKeyValue(body,"Icon84"); 
  
 if (mSocietyIconDatas.ContainsKey(key) == false)
 mSocietyIconDatas.Add(key, cd);
  }
 //Debug.Log(mSocietyIconDatas.Count);
}

            }