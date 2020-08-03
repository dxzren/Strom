/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (各系统购买次数消耗表)客户端配置结构体
            /// </summary>
            public partial class Configs_NumberConsumeDiamondData 
             { 
                /// <summary>
                /// 购买次数--主键
                /// </summary>
                public int BuyNumber{get;set;}

                
                /// <summary>
                /// 购买金币花费钻石
                /// </summary>
                public int GoldConsume { get;set; }
                /// <summary>
                /// 购买体力花费钻石
                /// </summary>
                public int PhysicalPowerConsume { get;set; }
                /// <summary>
                /// 购买技能点花费钻石
                /// </summary>
                public int SkillPointConsume { get;set; }
                /// <summary>
                /// 重置精英关卡花费钻石
                /// </summary>
                public int ResetCheckPoint { get;set; }
                /// <summary>
                /// 秘境塔购买次数花费钻石
                /// </summary>
                public int MysteriousTowerConsume { get;set; }
                /// <summary>
                /// 竞技场购买次数花费
                /// </summary>
                public int JJCConsume { get;set; }
            } 
            /// <summary>
            /// (各系统购买次数消耗表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_NumberConsumeDiamond
            { 

                static Configs_NumberConsumeDiamond _sInstance=null;
                public static Configs_NumberConsumeDiamond sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_NumberConsumeDiamond();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (各系统购买次数消耗表)字典集合
                /// </summary>
                public Dictionary<int, Configs_NumberConsumeDiamondData> mNumberConsumeDiamondDatas
                {
                    get { return _NumberConsumeDiamondDatas; }
                }

                /// <summary>
                /// (各系统购买次数消耗表)字典集合
                /// </summary>
                Dictionary<int, Configs_NumberConsumeDiamondData> _NumberConsumeDiamondDatas = new Dictionary<int, Configs_NumberConsumeDiamondData>();

                /// <summary>
                /// 根据BuyNumber读取对应的配置信息
                /// </summary>
                /// <param name="BuyNumber">配置的BuyNumber</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_NumberConsumeDiamondData GetNumberConsumeDiamondDataByBuyNumber(int BuyNumber)
                {
                    if (_NumberConsumeDiamondDatas.ContainsKey(BuyNumber))
                    {
                        return _NumberConsumeDiamondDatas[BuyNumber];
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
  Configs_NumberConsumeDiamondData cd = new Configs_NumberConsumeDiamondData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.BuyNumber = key; 
  cd.GoldConsume =  Util.GetIntKeyValue(body,"GoldConsume"); 
  cd.PhysicalPowerConsume =  Util.GetIntKeyValue(body,"PhysicalPowerConsume"); 
  cd.SkillPointConsume =  Util.GetIntKeyValue(body,"SkillPointConsume"); 
  cd.ResetCheckPoint =  Util.GetIntKeyValue(body,"ResetCheckPoint"); 
  cd.MysteriousTowerConsume =  Util.GetIntKeyValue(body,"MysteriousTowerConsume"); 
  cd.JJCConsume =  Util.GetIntKeyValue(body,"JJCConsume"); 
  
 if (mNumberConsumeDiamondDatas.ContainsKey(key) == false)
 mNumberConsumeDiamondDatas.Add(key, cd);
  }
 //Debug.Log(mNumberConsumeDiamondDatas.Count);
}

            }