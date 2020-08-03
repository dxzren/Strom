/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (道具)客户端配置结构体
            /// </summary>
            public partial class Configs_PropData 
             { 
                /// <summary>
                /// 道具ID--主键
                /// </summary>
                public int PropID{get;set;}

                
                /// <summary>
                /// 金币购买价格
                /// </summary>
                public int GoldBuy { get;set; }
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
                /// <summary>
                /// 秘境塔价格
                /// </summary>
                public int MysteriousTowerPrice { get;set; }
                /// <summary>
                /// 道具类型
                /// </summary>
                public int PropType { get;set; }
                /// <summary>
                /// 道具品质
                /// </summary>
                public int PropQuality { get;set; }
                /// <summary>
                /// 效果值
                /// </summary>
                public int Value { get;set; }
                /// <summary>
                /// 出售价格
                /// </summary>
                public int SellPrice { get;set; }
                /// <summary>
                /// 道具描述
                /// </summary>
                public string PropDes { get;set; }
                /// <summary>
                /// 道具图标84
                /// </summary>
                public string PropIcon84 { get;set; }
                /// <summary>
                /// 道具名
                /// </summary>
                public string PropName { get;set; }
            } 
            /// <summary>
            /// (道具)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Prop
            { 

                static Configs_Prop _sInstance;
                public static Configs_Prop sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Prop();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (道具)字典集合
                /// </summary>
                public Dictionary<int, Configs_PropData> mPropDatas
                {
                    get { return _PropDatas; }
                }

                /// <summary>
                /// (道具)字典集合
                /// </summary>
                Dictionary<int, Configs_PropData> _PropDatas = new Dictionary<int, Configs_PropData>();

                /// <summary>
                /// 根据PropID读取对应的配置信息
                /// </summary>
                /// <param name="PropID">配置的PropID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_PropData GetPropDataByPropID(int PropID)
                {
                    if (_PropDatas.ContainsKey(PropID))
                    {
                        return _PropDatas[PropID];
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
  Configs_PropData cd = new Configs_PropData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.PropID = key; 
  cd.GoldBuy =  Util.GetIntKeyValue(body,"GoldBuy"); 
  cd.HeavenPrice =  Util.GetIntKeyValue(body,"HeavenPrice"); 
  cd.SocietyPrice =  Util.GetIntKeyValue(body,"SocietyPrice"); 
  cd.JJCPrice =  Util.GetIntKeyValue(body,"JJCPrice"); 
  cd.MysteriousTowerPrice =  Util.GetIntKeyValue(body,"MysteriousTowerPrice"); 
  cd.PropType =  Util.GetIntKeyValue(body,"PropType"); 
  cd.PropQuality =  Util.GetIntKeyValue(body,"PropQuality"); 
  cd.Value =  Util.GetIntKeyValue(body,"Value"); 
  cd.SellPrice =  Util.GetIntKeyValue(body,"SellPrice"); 
  cd.PropDes =  Util.GetStringKeyValue(body,"PropDes"); 
  cd.PropIcon84 =  Util.GetStringKeyValue(body,"PropIcon84"); 
  cd.PropName =  Util.GetStringKeyValue(body,"PropName"); 
  
 if (mPropDatas.ContainsKey(key) == false)
 mPropDatas.Add(key, cd);
  }
 //Debug.Log(mPropDatas.Count);
}

            }