/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (抽卡装备卡库)客户端配置结构体
            /// </summary>
            public partial class Configs_LibraryEquipData 
             { 
                /// <summary>
                /// 装备ID--主键
                /// </summary>
                public int EquipID{get;set;}

                
                /// <summary>
                /// 装备品质
                /// </summary>
                public int EquipQuality { get;set; }
                /// <summary>
                /// 权重
                /// </summary>
                public int Weight { get;set; }
            } 
            /// <summary>
            /// (抽卡装备卡库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_LibraryEquip
            { 

                static Configs_LibraryEquip _sInstance;
                public static Configs_LibraryEquip sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_LibraryEquip();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (抽卡装备卡库)字典集合
                /// </summary>
                public Dictionary<int, Configs_LibraryEquipData> mLibraryEquipDatas
                {
                    get { return _LibraryEquipDatas; }
                }

                /// <summary>
                /// (抽卡装备卡库)字典集合
                /// </summary>
                Dictionary<int, Configs_LibraryEquipData> _LibraryEquipDatas = new Dictionary<int, Configs_LibraryEquipData>();

                /// <summary>
                /// 根据EquipID读取对应的配置信息
                /// </summary>
                /// <param name="EquipID">配置的EquipID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_LibraryEquipData GetLibraryEquipDataByEquipID(int EquipID)
                {
                    if (_LibraryEquipDatas.ContainsKey(EquipID))
                    {
                        return _LibraryEquipDatas[EquipID];
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
  Configs_LibraryEquipData cd = new Configs_LibraryEquipData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.EquipID = key; 
  cd.EquipQuality =  Util.GetIntKeyValue(body,"EquipQuality"); 
  cd.Weight =  Util.GetIntKeyValue(body,"Weight"); 
  
 if (mLibraryEquipDatas.ContainsKey(key) == false)
 mLibraryEquipDatas.Add(key, cd);
  }
 //Debug.Log(mLibraryEquipDatas.Count);
}

            }