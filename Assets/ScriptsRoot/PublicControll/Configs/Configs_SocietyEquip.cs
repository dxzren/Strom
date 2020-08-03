/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公会装备仓库表)客户端配置结构体
            /// </summary>
            public partial class Configs_SocietyEquipData 
             { 
                /// <summary>
                /// 序号--主键
                /// </summary>
                public int Number{get;set;}

                
                /// <summary>
                /// 装备ID
                /// </summary>
                public int EquipID { get;set; }
                /// <summary>
                /// 需求等级
                /// </summary>
                public int Level { get;set; }
                /// <summary>
                /// 所属章节
                /// </summary>
                public int Chapter { get;set; }
            } 
            /// <summary>
            /// (公会装备仓库表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SocietyEquip
            { 

                static Configs_SocietyEquip _sInstance;
                public static Configs_SocietyEquip sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SocietyEquip();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公会装备仓库表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SocietyEquipData> mSocietyEquipDatas
                {
                    get { return _SocietyEquipDatas; }
                }

                /// <summary>
                /// (公会装备仓库表)字典集合
                /// </summary>
                Dictionary<int, Configs_SocietyEquipData> _SocietyEquipDatas = new Dictionary<int, Configs_SocietyEquipData>();

                /// <summary>
                /// 根据Number读取对应的配置信息
                /// </summary>
                /// <param name="Number">配置的Number</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SocietyEquipData GetSocietyEquipDataByNumber(int Number)
                {
                    if (_SocietyEquipDatas.ContainsKey(Number))
                    {
                        return _SocietyEquipDatas[Number];
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
  Configs_SocietyEquipData cd = new Configs_SocietyEquipData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.Number = key; 
  cd.EquipID =  Util.GetIntKeyValue(body,"EquipID"); 
  cd.Level =  Util.GetIntKeyValue(body,"Level"); 
  cd.Chapter =  Util.GetIntKeyValue(body,"Chapter"); 
  
 if (mSocietyEquipDatas.ContainsKey(key) == false)
 mSocietyEquipDatas.Add(key, cd);
  }
 //Debug.Log(mSocietyEquipDatas.Count);
}

            }