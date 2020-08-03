/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (商人装备库)客户端配置结构体
            /// </summary>
            public partial class Configs_MerchantEquipData 
             { 
                /// <summary>
                /// 道具ID--主键
                /// </summary>
                public int PropID{get;set;}

                
                /// <summary>
                /// 道具类型
                /// </summary>
                public int PropType { get;set; }
                /// <summary>
                /// 道具单价/金币
                /// </summary>
                public int GoldBuy { get;set; }
                /// <summary>
                /// 权重
                /// </summary>
                public int Weight { get;set; }
                /// <summary>
                /// 道具品质
                /// </summary>
                public int PropQuality { get;set; }
                /// <summary>
                /// 装备等级
                /// </summary>
                public int DressLev { get;set; }
            } 
            /// <summary>
            /// (商人装备库)客户端配置数据集合类
            /// </summary>
            public partial class Configs_MerchantEquip
            { 

                static Configs_MerchantEquip _sInstance;
                public static Configs_MerchantEquip sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_MerchantEquip();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (商人装备库)字典集合
                /// </summary>
                public Dictionary<int, Configs_MerchantEquipData> mMerchantEquipDatas
                {
                    get { return _MerchantEquipDatas; }
                }

                /// <summary>
                /// (商人装备库)字典集合
                /// </summary>
                Dictionary<int, Configs_MerchantEquipData> _MerchantEquipDatas = new Dictionary<int, Configs_MerchantEquipData>();

                /// <summary>
                /// 根据PropID读取对应的配置信息
                /// </summary>
                /// <param name="PropID">配置的PropID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_MerchantEquipData GetMerchantEquipDataByPropID(int PropID)
                {
                    if (_MerchantEquipDatas.ContainsKey(PropID))
                    {
                        return _MerchantEquipDatas[PropID];
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
  Configs_MerchantEquipData cd = new Configs_MerchantEquipData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.PropID = key; 
  cd.PropType =  Util.GetIntKeyValue(body,"PropType"); 
  cd.GoldBuy =  Util.GetIntKeyValue(body,"GoldBuy"); 
  cd.Weight =  Util.GetIntKeyValue(body,"Weight"); 
  cd.PropQuality =  Util.GetIntKeyValue(body,"PropQuality"); 
  cd.DressLev =  Util.GetIntKeyValue(body,"DressLev"); 
  
 if (mMerchantEquipDatas.ContainsKey(key) == false)
 mMerchantEquipDatas.Add(key, cd);
  }
 //Debug.Log(mMerchantEquipDatas.Count);
}

            }