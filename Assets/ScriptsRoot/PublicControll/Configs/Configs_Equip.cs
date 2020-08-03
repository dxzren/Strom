/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (装备)客户端配置结构体
            /// </summary>
            public partial class Configs_EquipData 
             { 
                /// <summary>
                /// 装备ID--主键
                /// </summary>
                public int EquipID{get;set;}

                
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
                /// 秘境塔价格
                /// </summary>
                public int MysteriousTowerPrice { get;set; }
                /// <summary>
                /// 对应碎片
                /// </summary>
                public int FragmentID { get;set; }
                /// <summary>
                /// 装备类型
                /// </summary>
                public int EquipType { get;set; }
                /// <summary>
                /// 合成类型
                /// </summary>
                public int CompoundType { get;set; }
                /// <summary>
                /// 穿戴等级
                /// </summary>
                public int DressLev { get;set; }
                /// <summary>
                /// 装备品质
                /// </summary>
                public int EquipQuality { get;set; }
                /// <summary>
                /// 属性项1
                /// </summary>
                public int Attribute1 { get;set; }
                /// <summary>
                /// 属性值1
                /// </summary>
                public int AttributeValue1 { get;set; }
                /// <summary>
                /// 属性项2
                /// </summary>
                public int Attribute2 { get;set; }
                /// <summary>
                /// 属性值2
                /// </summary>
                public int AttributeValue2 { get;set; }
                /// <summary>
                /// 属性项3
                /// </summary>
                public int Attribute3 { get;set; }
                /// <summary>
                /// 属性值3
                /// </summary>
                public int AttributeValue3 { get;set; }
                /// <summary>
                /// 属性项4
                /// </summary>
                public int Attribute4 { get;set; }
                /// <summary>
                /// 属性值4
                /// </summary>
                public int AttributeValue4 { get;set; }
                /// <summary>
                /// 属性项5
                /// </summary>
                public int Attribute5 { get;set; }
                /// <summary>
                /// 属性值5
                /// </summary>
                public int AttributeValue5 { get;set; }
                /// <summary>
                /// 属性项6
                /// </summary>
                public int Attribute6 { get;set; }
                /// <summary>
                /// 属性值6
                /// </summary>
                public int AttributeValue6 { get;set; }
                /// <summary>
                /// 合成材料
                /// </summary>
                public List<int> Material { get;set; }
                /// <summary>
                /// 合成费用
                /// </summary>
                public int Cost { get;set; }
                /// <summary>
                /// 附魔经验
                /// </summary>
                public int EnchantExp { get;set; }
                /// <summary>
                /// 出售价格
                /// </summary>
                public int SellPrice { get;set; }
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
                public string EquipDes { get;set; }
                /// <summary>
                /// 装备图标_84
                /// </summary>
                public string EquipIcon_84 { get;set; }
                /// <summary>
                /// 装备名
                /// </summary>
                public string EquipName { get;set; }
            } 
            /// <summary>
            /// (装备)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Equip
            { 

                static Configs_Equip _sInstance;
                public static Configs_Equip sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Equip();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (装备)字典集合
                /// </summary>
                public Dictionary<int, Configs_EquipData> mEquipDatas
                {
                    get { return _EquipDatas; }
                }

                /// <summary>
                /// (装备)字典集合
                /// </summary>
                Dictionary<int, Configs_EquipData> _EquipDatas = new Dictionary<int, Configs_EquipData>();

                /// <summary>
                /// 根据EquipID读取对应的配置信息
                /// </summary>
                /// <param name="EquipID">配置的EquipID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_EquipData GetEquipDataByEquipID(int EquipID)
                {
                    if (_EquipDatas.ContainsKey(EquipID))
                    {
                        return _EquipDatas[EquipID];
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
  Configs_EquipData cd = new Configs_EquipData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.EquipID = key; 
  cd.PropType =  Util.GetIntKeyValue(body,"PropType"); 
  cd.GoldBuy =  Util.GetIntKeyValue(body,"GoldBuy"); 
  cd.HeavenPrice =  Util.GetIntKeyValue(body,"HeavenPrice"); 
  cd.SocietyPrice =  Util.GetIntKeyValue(body,"SocietyPrice"); 
  cd.JJCPrice =  Util.GetIntKeyValue(body,"JJCPrice"); 
  cd.MysteriousTowerPrice =  Util.GetIntKeyValue(body,"MysteriousTowerPrice"); 
  cd.FragmentID =  Util.GetIntKeyValue(body,"FragmentID"); 
  cd.EquipType =  Util.GetIntKeyValue(body,"EquipType"); 
  cd.CompoundType =  Util.GetIntKeyValue(body,"CompoundType"); 
  cd.DressLev =  Util.GetIntKeyValue(body,"DressLev"); 
  cd.EquipQuality =  Util.GetIntKeyValue(body,"EquipQuality"); 
  cd.Attribute1 =  Util.GetIntKeyValue(body,"Attribute1"); 
  cd.AttributeValue1 =  Util.GetIntKeyValue(body,"AttributeValue1"); 
  cd.Attribute2 =  Util.GetIntKeyValue(body,"Attribute2"); 
  cd.AttributeValue2 =  Util.GetIntKeyValue(body,"AttributeValue2"); 
  cd.Attribute3 =  Util.GetIntKeyValue(body,"Attribute3"); 
  cd.AttributeValue3 =  Util.GetIntKeyValue(body,"AttributeValue3"); 
  cd.Attribute4 =  Util.GetIntKeyValue(body,"Attribute4"); 
  cd.AttributeValue4 =  Util.GetIntKeyValue(body,"AttributeValue4"); 
  cd.Attribute5 =  Util.GetIntKeyValue(body,"Attribute5"); 
  cd.AttributeValue5 =  Util.GetIntKeyValue(body,"AttributeValue5"); 
  cd.Attribute6 =  Util.GetIntKeyValue(body,"Attribute6"); 
  cd.AttributeValue6 =  Util.GetIntKeyValue(body,"AttributeValue6"); 
 
 string[] MaterialStrs= Util.GetStringKeyValue(body, "Material").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Material = new List<int>();
foreach(string MaterialStr in MaterialStrs)  cd.Material.Add(Util.ParseToInt(MaterialStr)); 
 
 cd.Cost =  Util.GetIntKeyValue(body,"Cost"); 
  cd.EnchantExp =  Util.GetIntKeyValue(body,"EnchantExp"); 
  cd.SellPrice =  Util.GetIntKeyValue(body,"SellPrice"); 
  cd.Path1 =  Util.GetIntKeyValue(body,"Path1"); 
  cd.Path2 =  Util.GetIntKeyValue(body,"Path2"); 
  cd.Path3 =  Util.GetIntKeyValue(body,"Path3"); 
  cd.Path4 =  Util.GetIntKeyValue(body,"Path4"); 
  cd.EquipDes =  Util.GetStringKeyValue(body,"EquipDes"); 
  cd.EquipIcon_84 =  Util.GetStringKeyValue(body,"EquipIcon_84"); 
  cd.EquipName =  Util.GetStringKeyValue(body,"EquipName"); 
  
 if (mEquipDatas.ContainsKey(key) == false)
 mEquipDatas.Add(key, cd);
  }
 //Debug.Log(mEquipDatas.Count);
}

            }