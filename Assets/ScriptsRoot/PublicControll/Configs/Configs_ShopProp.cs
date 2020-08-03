/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (商店道具库)客户端配置结构体
            /// </summary>
            public partial class Configs_ShopPropData 
             { 
                /// <summary>
                /// 物品ID--主键
                /// </summary>
                public int PropID{get;set;}

                
                /// <summary>
                /// 物品类别
                /// </summary>
                public int ProType { get;set; }
                /// <summary>
                /// 物品品质
                /// </summary>
                public int Quality { get;set; }
                /// <summary>
                /// 数量
                /// </summary>
                public int Number { get;set; }
                /// <summary>
                /// 天堂之路价格
                /// </summary>
                public int HeavenPrice { get;set; }
                /// <summary>
                /// 公会价格
                /// </summary>
                public int SocietyPrice { get;set; }
                /// <summary>
                /// 竞技场价格
                /// </summary>
                public int JJCPrice { get;set; }
            } 
            /// <summary>
            /// (商店道具库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ShopProp
            { 

                static Configs_ShopProp _sInstance;
                public static Configs_ShopProp sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ShopProp();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (商店道具库)字典集合
                /// </summary>
                public Dictionary<int, Configs_ShopPropData> mShopPropDatas
                {
                    get { return _ShopPropDatas; }
                }

                /// <summary>
                /// (商店道具库)字典集合
                /// </summary>
                Dictionary<int, Configs_ShopPropData> _ShopPropDatas = new Dictionary<int, Configs_ShopPropData>();

                /// <summary>
                /// 根据PropID读取对应的配置信息
                /// </summary>
                /// <param name="PropID">配置的PropID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ShopPropData GetShopPropDataByPropID(int PropID)
                {
                    if (_ShopPropDatas.ContainsKey(PropID))
                    {
                        return _ShopPropDatas[PropID];
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
  Configs_ShopPropData cd = new Configs_ShopPropData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.PropID = key; 
  cd.ProType =  Util.GetIntKeyValue(body,"ProType"); 
  cd.Quality =  Util.GetIntKeyValue(body,"Quality"); 
  cd.Number =  Util.GetIntKeyValue(body,"Number"); 
  cd.HeavenPrice =  Util.GetIntKeyValue(body,"HeavenPrice"); 
  cd.SocietyPrice =  Util.GetIntKeyValue(body,"SocietyPrice"); 
  cd.JJCPrice =  Util.GetIntKeyValue(body,"JJCPrice"); 
  
 if (mShopPropDatas.ContainsKey(key) == false)
 mShopPropDatas.Add(key, cd);
  }
 //Debug.Log(mShopPropDatas.Count);
}

            }