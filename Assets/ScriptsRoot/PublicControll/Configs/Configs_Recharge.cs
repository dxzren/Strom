/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (充值表)客户端配置结构体
            /// </summary>
            public partial class Configs_RechargeData 
             { 
                /// <summary>
                /// 商品ID--主键
                /// </summary>
                public int GoodsID{get;set;}

                
                /// <summary>
                /// SKUID
                /// </summary>
                public string SKUID { get;set; }
                /// <summary>
                /// 商品类型
                /// </summary>
                public int GoodsType { get;set; }
                /// <summary>
                /// 经验值
                /// </summary>
                public int EmpiricalValue { get;set; }
                /// <summary>
                /// 人民币价格
                /// </summary>
                public float RMB { get;set; }
                /// <summary>
                /// 钻石数量
                /// </summary>
                public int Value { get;set; }
                /// <summary>
                /// 首次翻倍标识
                /// </summary>
                public int Limit { get;set; }
                /// <summary>
                /// 首次购买钻石
                /// </summary>
                public int FirstValue { get;set; }
                /// <summary>
                /// 商品名称
                /// </summary>
                public string GoodsName { get;set; }
                /// <summary>
                /// 首次商品描述
                /// </summary>
                public string FirstDescribe { get;set; }
                /// <summary>
                /// 商品描述
                /// </summary>
                public string Describe { get;set; }
                /// <summary>
                /// 商品图标
                /// </summary>
                public string Icon { get;set; }
            } 
            /// <summary>
            /// (充值表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Recharge
            { 

                static Configs_Recharge _sInstance;
                public static Configs_Recharge sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Recharge();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (充值表)字典集合
                /// </summary>
                public Dictionary<int, Configs_RechargeData> mRechargeDatas
                {
                    get { return _RechargeDatas; }
                }

                /// <summary>
                /// (充值表)字典集合
                /// </summary>
                Dictionary<int, Configs_RechargeData> _RechargeDatas = new Dictionary<int, Configs_RechargeData>();

                /// <summary>
                /// 根据GoodsID读取对应的配置信息
                /// </summary>
                /// <param name="GoodsID">配置的GoodsID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_RechargeData GetRechargeDataByGoodsID(int GoodsID)
                {
                    if (_RechargeDatas.ContainsKey(GoodsID))
                    {
                        return _RechargeDatas[GoodsID];
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
  Configs_RechargeData cd = new Configs_RechargeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.GoodsID = key; 
  cd.SKUID =  Util.GetStringKeyValue(body,"SKUID"); 
  cd.GoodsType =  Util.GetIntKeyValue(body,"GoodsType"); 
  cd.EmpiricalValue =  Util.GetIntKeyValue(body,"EmpiricalValue"); 
  cd.RMB =  Util.GetFloatKeyValue(body,"RMB"); 
  cd.Value =  Util.GetIntKeyValue(body,"Value"); 
  cd.Limit =  Util.GetIntKeyValue(body,"Limit"); 
  cd.FirstValue =  Util.GetIntKeyValue(body,"FirstValue"); 
  cd.GoodsName =  Util.GetStringKeyValue(body,"GoodsName"); 
  cd.FirstDescribe =  Util.GetStringKeyValue(body,"FirstDescribe"); 
  cd.Describe =  Util.GetStringKeyValue(body,"Describe"); 
  cd.Icon =  Util.GetStringKeyValue(body,"Icon"); 
  
 if (mRechargeDatas.ContainsKey(key) == false)
 mRechargeDatas.Add(key, cd);
  }
 //Debug.Log(mRechargeDatas.Count);
}

            }