/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (系统导航表)客户端配置结构体
            /// </summary>
            public partial class Configs_SystemNavigationData 
             { 
                /// <summary>
                /// 系统ID--主键
                /// </summary>
                public int SystemID{get;set;}

                
                /// <summary>
                /// 显示等级
                /// </summary>
                public int ShowLevel { get;set; }
                /// <summary>
                /// 开放等级
                /// </summary>
                public int LimitLevel { get;set; }
                /// <summary>
                /// VIP等级
                /// </summary>
                public int Viplevel { get;set; }
                /// <summary>
                /// 系统名称
                /// </summary>
                public string SystemName { get;set; }
                /// <summary>
                /// 对应Panel
                /// </summary>
                public string Panel { get;set; }
                /// <summary>
                /// 升级显示图标
                /// </summary>
                public string LevelUpIcon { get;set; }
                /// <summary>
                /// 图标尺寸
                /// </summary>
                public float IconSize { get;set; }
                /// <summary>
                /// 开启系统描述
                /// </summary>
                public string SystemDes { get;set; }
            } 
            /// <summary>
            /// (系统导航表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SystemNavigation
            { 

                static Configs_SystemNavigation _sInstance;
                public static Configs_SystemNavigation sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SystemNavigation();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (系统导航表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SystemNavigationData> mSystemNavigationDatas
                {
                    get { return _SystemNavigationDatas; }
                }

                /// <summary>
                /// (系统导航表)字典集合
                /// </summary>
                Dictionary<int, Configs_SystemNavigationData> _SystemNavigationDatas = new Dictionary<int, Configs_SystemNavigationData>();

                /// <summary>
                /// 根据SystemID读取对应的配置信息
                /// </summary>
                /// <param name="SystemID">配置的SystemID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SystemNavigationData GetSystemNavigationDataBySystemID(int SystemID)
                {
                    if (_SystemNavigationDatas.ContainsKey(SystemID))
                    {
                        return _SystemNavigationDatas[SystemID];
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
  Configs_SystemNavigationData cd = new Configs_SystemNavigationData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.SystemID = key; 
  cd.ShowLevel =  Util.GetIntKeyValue(body,"ShowLevel"); 
  cd.LimitLevel =  Util.GetIntKeyValue(body,"LimitLevel"); 
  cd.Viplevel =  Util.GetIntKeyValue(body,"Viplevel"); 
  cd.SystemName =  Util.GetStringKeyValue(body,"SystemName"); 
  cd.Panel =  Util.GetStringKeyValue(body,"Panel"); 
  cd.LevelUpIcon =  Util.GetStringKeyValue(body,"LevelUpIcon"); 
  cd.IconSize =  Util.GetFloatKeyValue(body,"IconSize"); 
  cd.SystemDes =  Util.GetStringKeyValue(body,"SystemDes"); 
  
 if (mSystemNavigationDatas.ContainsKey(key) == false)
 mSystemNavigationDatas.Add(key, cd);
  }
 //Debug.Log(mSystemNavigationDatas.Count);
}

            }