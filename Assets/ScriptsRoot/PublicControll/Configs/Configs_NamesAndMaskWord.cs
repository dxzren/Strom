/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (随机名字库及屏蔽字库)客户端配置结构体
            /// </summary>
            public partial class Configs_NamesAndMaskWordData 
             { 
                /// <summary>
                /// 序号--主键
                /// </summary>
                public int Number{get;set;}

                
                /// <summary>
                /// 屏蔽字
                /// </summary>
                public string MaskWord { get;set; }
                /// <summary>
                /// 形容词
                /// </summary>
                public string Adjective { get;set; }
                /// <summary>
                /// 称谓
                /// </summary>
                public string Appellation { get;set; }
                /// <summary>
                /// 名字
                /// </summary>
                public string Name { get;set; }
            } 
            /// <summary>
            /// (随机名字库及屏蔽字库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_NamesAndMaskWord
            { 

                static Configs_NamesAndMaskWord _sInstance;
                public static Configs_NamesAndMaskWord sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_NamesAndMaskWord();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (随机名字库及屏蔽字库)字典集合
                /// </summary>
                public Dictionary<int, Configs_NamesAndMaskWordData> mNamesAndMaskWordDatas
                {
                    get { return _NamesAndMaskWordDatas; }
                }

                /// <summary>
                /// (随机名字库及屏蔽字库)字典集合
                /// </summary>
                Dictionary<int, Configs_NamesAndMaskWordData> _NamesAndMaskWordDatas = new Dictionary<int, Configs_NamesAndMaskWordData>();

                /// <summary>
                /// 根据Number读取对应的配置信息
                /// </summary>
                /// <param name="Number">配置的Number</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_NamesAndMaskWordData GetNamesAndMaskWordDataByNumber(int Number)
                {
                    if (_NamesAndMaskWordDatas.ContainsKey(Number))
                    {
                        return _NamesAndMaskWordDatas[Number];
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
  Configs_NamesAndMaskWordData cd = new Configs_NamesAndMaskWordData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.Number = key; 
  cd.MaskWord =  Util.GetStringKeyValue(body,"MaskWord"); 
  cd.Adjective =  Util.GetStringKeyValue(body,"Adjective"); 
  cd.Appellation =  Util.GetStringKeyValue(body,"Appellation"); 
  cd.Name =  Util.GetStringKeyValue(body,"Name"); 
  
 if (mNamesAndMaskWordDatas.ContainsKey(key) == false)
 mNamesAndMaskWordDatas.Add(key, cd);
  }
 //Debug.Log(mNamesAndMaskWordDatas.Count);
}

            }