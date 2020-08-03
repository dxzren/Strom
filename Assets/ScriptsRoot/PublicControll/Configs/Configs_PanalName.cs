/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (全屏界面名称)客户端配置结构体
            /// </summary>
            public partial class Configs_PanalNameData 
             { 
                /// <summary>
                /// 序号--主键
                /// </summary>
                public int ID{get;set;}

                
                /// <summary>
                /// 界面名称
                /// </summary>
                public string Name { get;set; }
                /// <summary>
                /// 背景音乐
                /// </summary>
                public string BGMusic { get;set; }
            } 
            /// <summary>
            /// (全屏界面名称)客户端配置数据集合类
            /// </summary>
            public partial class Configs_PanalName
            { 

                static Configs_PanalName _sInstance;
                public static Configs_PanalName sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_PanalName();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (全屏界面名称)字典集合
                /// </summary>
                public Dictionary<int, Configs_PanalNameData> mPanalNameDatas
                {
                    get { return _PanalNameDatas; }
                }

                /// <summary>
                /// (全屏界面名称)字典集合
                /// </summary>
                Dictionary<int, Configs_PanalNameData> _PanalNameDatas = new Dictionary<int, Configs_PanalNameData>();

                /// <summary>
                /// 根据ID读取对应的配置信息
                /// </summary>
                /// <param name="ID">配置的ID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_PanalNameData GetPanalNameDataByID(int ID)
                {
                    if (_PanalNameDatas.ContainsKey(ID))
                    {
                        return _PanalNameDatas[ID];
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
  Configs_PanalNameData cd = new Configs_PanalNameData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ID = key; 
  cd.Name =  Util.GetStringKeyValue(body,"Name"); 
  cd.BGMusic =  Util.GetStringKeyValue(body,"BGMusic"); 
  
 if (mPanalNameDatas.ContainsKey(key) == false)
 mPanalNameDatas.Add(key, cd);
  }
 //Debug.Log(mPanalNameDatas.Count);
}

            }