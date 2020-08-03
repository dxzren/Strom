/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (商店装备卷轴库)客户端配置结构体
            /// </summary>
            public partial class Configs_ShopEquipData 
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
            /// (商店装备卷轴库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ShopEquip
            { 

                static Configs_ShopEquip _sInstance;
                public static Configs_ShopEquip sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ShopEquip();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (商店装备卷轴库)字典集合
                /// </summary>
                public Dictionary<int, Configs_ShopEquipData> mShopEquipDatas
                {
                    get { return _ShopEquipDatas; }
                }

                /// <summary>
                /// (商店装备卷轴库)字典集合
                /// </summary>
                Dictionary<int, Configs_ShopEquipData> _ShopEquipDatas = new Dictionary<int, Configs_ShopEquipData>();

                /// <summary>
                /// 根据PropID读取对应的配置信息
                /// </summary>
                /// <param name="PropID">配置的PropID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ShopEquipData GetShopEquipDataByPropID(int PropID)
                {
                    if (_ShopEquipDatas.ContainsKey(PropID))
                    {
                        return _ShopEquipDatas[PropID];
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
  Configs_ShopEquipData cd = new Configs_ShopEquipData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.PropID = key; 
  cd.ProType =  Util.GetIntKeyValue(body,"ProType"); 
  cd.Quality =  Util.GetIntKeyValue(body,"Quality"); 
  cd.HeavenPrice =  Util.GetIntKeyValue(body,"HeavenPrice"); 
  cd.SocietyPrice =  Util.GetIntKeyValue(body,"SocietyPrice"); 
  cd.JJCPrice =  Util.GetIntKeyValue(body,"JJCPrice"); 
  
 if (mShopEquipDatas.ContainsKey(key) == false)
 mShopEquipDatas.Add(key, cd);
  }
 //Debug.Log(mShopEquipDatas.Count);
}

            }