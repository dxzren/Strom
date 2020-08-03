/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (英雄魂石)客户端配置结构体
            /// </summary>
            public partial class Configs_SoulData 
             { 
                /// <summary>
                /// 魂石ID--主键
                /// </summary>
                public int SoulID{get;set;}

                
                /// <summary>
                /// 初始星级
                /// </summary>
                public int InitialStar { get;set; }
                /// <summary>
                /// 金币购买价格
                /// </summary>
                public int GoldBuy { get;set; }
                /// <summary>
                /// 钻石购买价格
                /// </summary>
                public int DiamondBuy { get;set; }
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
                /// 道具类型
                /// </summary>
                public int PropType { get;set; }
                /// <summary>
                /// 合成数量
                /// </summary>
                public int Num { get;set; }
                /// <summary>
                /// 目标卡牌
                /// </summary>
                public int Target { get;set; }
                /// <summary>
                /// 产出类型
                /// </summary>
                public int PathType { get;set; }
                /// <summary>
                /// 产出路径1
                /// </summary>
                public int Path1 { get;set; }
                /// <summary>
                /// 产出路径2
                /// </summary>
                public int Path2 { get;set; }
                /// <summary>
                /// 产出路径3
                /// </summary>
                public int Path3 { get;set; }
                /// <summary>
                /// 出售价格
                /// </summary>
                public int SellPrice { get;set; }
                /// <summary>
                /// 描述
                /// </summary>
                public string SoulDes { get;set; }
                /// <summary>
                /// 头像84
                /// </summary>
                public string head84 { get;set; }
                /// <summary>
                /// 魂石名
                /// </summary>
                public string SoulName { get;set; }
            } 
            /// <summary>
            /// (英雄魂石)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Soul
            { 

                static Configs_Soul _sInstance;
                public static Configs_Soul sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Soul();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (英雄魂石)字典集合
                /// </summary>
                public Dictionary<int, Configs_SoulData> mSoulDatas
                {
                    get { return _SoulDatas; }
                }

                /// <summary>
                /// (英雄魂石)字典集合
                /// </summary>
                Dictionary<int, Configs_SoulData> _SoulDatas = new Dictionary<int, Configs_SoulData>();

                /// <summary>
                /// 根据SoulID读取对应的配置信息
                /// </summary>
                /// <param name="SoulID">配置的SoulID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SoulData GetSoulDataBySoulID(int SoulID)
                {
                    if (_SoulDatas.ContainsKey(SoulID))
                    {
                        return _SoulDatas[SoulID];
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
  Configs_SoulData cd = new Configs_SoulData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.SoulID = key; 
  cd.InitialStar =  Util.GetIntKeyValue(body,"InitialStar"); 
  cd.GoldBuy =  Util.GetIntKeyValue(body,"GoldBuy"); 
  cd.DiamondBuy =  Util.GetIntKeyValue(body,"DiamondBuy"); 
  cd.HeavenPrice =  Util.GetIntKeyValue(body,"HeavenPrice"); 
  cd.SocietyPrice =  Util.GetIntKeyValue(body,"SocietyPrice"); 
  cd.JJCPrice =  Util.GetIntKeyValue(body,"JJCPrice"); 
  cd.PropType =  Util.GetIntKeyValue(body,"PropType"); 
  cd.Num =  Util.GetIntKeyValue(body,"Num"); 
  cd.Target =  Util.GetIntKeyValue(body,"Target"); 
  cd.PathType =  Util.GetIntKeyValue(body,"PathType"); 
  cd.Path1 =  Util.GetIntKeyValue(body,"Path1"); 
  cd.Path2 =  Util.GetIntKeyValue(body,"Path2"); 
  cd.Path3 =  Util.GetIntKeyValue(body,"Path3"); 
  cd.SellPrice =  Util.GetIntKeyValue(body,"SellPrice"); 
  cd.SoulDes =  Util.GetStringKeyValue(body,"SoulDes"); 
  cd.head84 =  Util.GetStringKeyValue(body,"head84"); 
  cd.SoulName =  Util.GetStringKeyValue(body,"SoulName"); 
  
 if (mSoulDatas.ContainsKey(key) == false)
 mSoulDatas.Add(key, cd);
  }
 //Debug.Log(mSoulDatas.Count);
}

            }