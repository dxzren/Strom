/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (碎片)客户端配置结构体
            /// </summary>
            public partial class Configs_FragmentData 
             { 
                /// <summary>
                /// 碎片ID--主键
                /// </summary>
                public int FragmentID{get;set;}

                
                /// <summary>
                /// 道具类型
                /// </summary>
                public int PropType { get;set; }
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
                /// 出售价格
                /// </summary>
                public int SellPrice { get;set; }
                /// <summary>
                /// 合成数量
                /// </summary>
                public int Num { get;set; }
                /// <summary>
                /// 碎片类型
                /// </summary>
                public int FragmentType { get;set; }
                /// <summary>
                /// 碎片品质
                /// </summary>
                public int FragmentQuality { get;set; }
                /// <summary>
                /// 目标卡牌
                /// </summary>
                public int Target { get;set; }
                /// <summary>
                /// 合成费用
                /// </summary>
                public int Cost { get;set; }
                /// <summary>
                /// 附魔经验
                /// </summary>
                public int EnchantExp { get;set; }
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
                /// 产出路径4
                /// </summary>
                public int Path4 { get;set; }
                /// <summary>
                /// 描述
                /// </summary>
                public string FragmentDes { get;set; }
                /// <summary>
                /// 碎片图标_84
                /// </summary>
                public string FragmentIcon_84 { get;set; }
                /// <summary>
                /// 碎片名
                /// </summary>
                public string FragmentName { get;set; }
            } 
            /// <summary>
            /// (碎片)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Fragment
            { 

                static Configs_Fragment _sInstance;
                public static Configs_Fragment sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Fragment();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (碎片)字典集合
                /// </summary>
                public Dictionary<int, Configs_FragmentData> mFragmentDatas
                {
                    get { return _FragmentDatas; }
                }

                /// <summary>
                /// (碎片)字典集合
                /// </summary>
                Dictionary<int, Configs_FragmentData> _FragmentDatas = new Dictionary<int, Configs_FragmentData>();

                /// <summary>
                /// 根据FragmentID读取对应的配置信息
                /// </summary>
                /// <param name="FragmentID">配置的FragmentID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_FragmentData GetFragmentDataByFragmentID(int FragmentID)
                {
                    if (_FragmentDatas.ContainsKey(FragmentID))
                    {
                        return _FragmentDatas[FragmentID];
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
  Configs_FragmentData cd = new Configs_FragmentData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.FragmentID = key; 
  cd.PropType =  Util.GetIntKeyValue(body,"PropType"); 
  cd.GoldBuy =  Util.GetIntKeyValue(body,"GoldBuy"); 
  cd.HeavenPrice =  Util.GetIntKeyValue(body,"HeavenPrice"); 
  cd.SocietyPrice =  Util.GetIntKeyValue(body,"SocietyPrice"); 
  cd.JJCPrice =  Util.GetIntKeyValue(body,"JJCPrice"); 
  cd.SellPrice =  Util.GetIntKeyValue(body,"SellPrice"); 
  cd.Num =  Util.GetIntKeyValue(body,"Num"); 
  cd.FragmentType =  Util.GetIntKeyValue(body,"FragmentType"); 
  cd.FragmentQuality =  Util.GetIntKeyValue(body,"FragmentQuality"); 
  cd.Target =  Util.GetIntKeyValue(body,"Target"); 
  cd.Cost =  Util.GetIntKeyValue(body,"Cost"); 
  cd.EnchantExp =  Util.GetIntKeyValue(body,"EnchantExp"); 
  cd.Path1 =  Util.GetIntKeyValue(body,"Path1"); 
  cd.Path2 =  Util.GetIntKeyValue(body,"Path2"); 
  cd.Path3 =  Util.GetIntKeyValue(body,"Path3"); 
  cd.Path4 =  Util.GetIntKeyValue(body,"Path4"); 
  cd.FragmentDes =  Util.GetStringKeyValue(body,"FragmentDes"); 
  cd.FragmentIcon_84 =  Util.GetStringKeyValue(body,"FragmentIcon_84"); 
  cd.FragmentName =  Util.GetStringKeyValue(body,"FragmentName"); 
  
 if (mFragmentDatas.ContainsKey(key) == false)
 mFragmentDatas.Add(key, cd);
  }
 //Debug.Log(mFragmentDatas.Count);
}

            }