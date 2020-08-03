/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (职业加成表)客户端配置结构体
            /// </summary>
            public partial class Configs_OccupationalAdditionData 
             { 
                /// <summary>
                /// 序号--主键
                /// </summary>
                public int ID{get;set;}

                
                /// <summary>
                /// 属性项
                /// </summary>
                public int Strength { get;set; }
                /// <summary>
                /// 属性值
                /// </summary>
                public int Agility { get;set; }
                /// <summary>
                /// 职业等级图标
                /// </summary>
                public string Icon { get;set; }
                /// <summary>
                /// 职业名称
                /// </summary>
                public string Name { get;set; }
            } 
            /// <summary>
            /// (职业加成表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_OccupationalAddition
            { 

                static Configs_OccupationalAddition _sInstance;
                public static Configs_OccupationalAddition sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_OccupationalAddition();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (职业加成表)字典集合
                /// </summary>
                public Dictionary<int, Configs_OccupationalAdditionData> mOccupationalAdditionDatas
                {
                    get { return _OccupationalAdditionDatas; }
                }

                /// <summary>
                /// (职业加成表)字典集合
                /// </summary>
                Dictionary<int, Configs_OccupationalAdditionData> _OccupationalAdditionDatas = new Dictionary<int, Configs_OccupationalAdditionData>();

                /// <summary>
                /// 根据ID读取对应的配置信息
                /// </summary>
                /// <param name="ID">配置的ID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_OccupationalAdditionData GetOccupationalAdditionDataByID(int ID)
                {
                    if (_OccupationalAdditionDatas.ContainsKey(ID))
                    {
                        return _OccupationalAdditionDatas[ID];
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
  Configs_OccupationalAdditionData cd = new Configs_OccupationalAdditionData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ID = key; 
  cd.Strength =  Util.GetIntKeyValue(body,"Strength"); 
  cd.Agility =  Util.GetIntKeyValue(body,"Agility"); 
  cd.Icon =  Util.GetStringKeyValue(body,"Icon"); 
  cd.Name =  Util.GetStringKeyValue(body,"Name"); 
  
 if (mOccupationalAdditionDatas.ContainsKey(key) == false)
 mOccupationalAdditionDatas.Add(key, cd);
  }
 //Debug.Log(mOccupationalAdditionDatas.Count);
}

            }