/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (天堂之路数据表)客户端配置结构体
            /// </summary>
            public partial class Configs_RoadOfHeavenData 
             { 
                /// <summary>
                /// 序号--主键
                /// </summary>
                public int ID{get;set;}

                
                /// <summary>
                /// 基础系数
                /// </summary>
                public int BaseModulus { get;set; }
                /// <summary>
                /// 战力系数
                /// </summary>
                public int FightModulus { get;set; }
                /// <summary>
                /// 天堂币
                /// </summary>
                public int Currency { get;set; }
                /// <summary>
                /// 道具/装备/卷轴数量
                /// </summary>
                public int Number { get;set; }
                /// <summary>
                /// 白色品质权重
                /// </summary>
                public int QualityWeight1 { get;set; }
                /// <summary>
                /// 绿色品质权重
                /// </summary>
                public int QualityWeight2 { get;set; }
                /// <summary>
                /// 蓝色品质权重
                /// </summary>
                public int QualityWeight3 { get;set; }
                /// <summary>
                /// 紫色品质权重
                /// </summary>
                public int QualityWeight4 { get;set; }
                /// <summary>
                /// 灵魂石权重
                /// </summary>
                public int SoulWeight { get;set; }
                /// <summary>
                /// 英雄权重
                /// </summary>
                public int HeroWeight { get;set; }
                /// <summary>
                /// 建筑资源
                /// </summary>
                public string BuildingName { get;set; }
                /// <summary>
                /// 建筑X坐标
                /// </summary>
                public float BuildingCoordinateX { get;set; }
                /// <summary>
                /// 建筑Y坐标
                /// </summary>
                public float BuildingCoordinateY { get;set; }
            } 
            /// <summary>
            /// (天堂之路数据表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_RoadOfHeaven
            { 

                static Configs_RoadOfHeaven _sInstance;
                public static Configs_RoadOfHeaven sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_RoadOfHeaven();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (天堂之路数据表)字典集合
                /// </summary>
                public Dictionary<int, Configs_RoadOfHeavenData> mRoadOfHeavenDatas
                {
                    get { return _RoadOfHeavenDatas; }
                }

                /// <summary>
                /// (天堂之路数据表)字典集合
                /// </summary>
                Dictionary<int, Configs_RoadOfHeavenData> _RoadOfHeavenDatas = new Dictionary<int, Configs_RoadOfHeavenData>();

                /// <summary>
                /// 根据ID读取对应的配置信息
                /// </summary>
                /// <param name="ID">配置的ID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_RoadOfHeavenData GetRoadOfHeavenDataByID(int ID)
                {
                    if (_RoadOfHeavenDatas.ContainsKey(ID))
                    {
                        return _RoadOfHeavenDatas[ID];
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
  Configs_RoadOfHeavenData cd = new Configs_RoadOfHeavenData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ID = key; 
  cd.BaseModulus =  Util.GetIntKeyValue(body,"BaseModulus"); 
  cd.FightModulus =  Util.GetIntKeyValue(body,"FightModulus"); 
  cd.Currency =  Util.GetIntKeyValue(body,"Currency"); 
  cd.Number =  Util.GetIntKeyValue(body,"Number"); 
  cd.QualityWeight1 =  Util.GetIntKeyValue(body,"QualityWeight1"); 
  cd.QualityWeight2 =  Util.GetIntKeyValue(body,"QualityWeight2"); 
  cd.QualityWeight3 =  Util.GetIntKeyValue(body,"QualityWeight3"); 
  cd.QualityWeight4 =  Util.GetIntKeyValue(body,"QualityWeight4"); 
  cd.SoulWeight =  Util.GetIntKeyValue(body,"SoulWeight"); 
  cd.HeroWeight =  Util.GetIntKeyValue(body,"HeroWeight"); 
  cd.BuildingName =  Util.GetStringKeyValue(body,"BuildingName"); 
  cd.BuildingCoordinateX =  Util.GetFloatKeyValue(body,"BuildingCoordinateX"); 
  cd.BuildingCoordinateY =  Util.GetFloatKeyValue(body,"BuildingCoordinateY"); 
  
 if (mRoadOfHeavenDatas.ContainsKey(key) == false)
 mRoadOfHeavenDatas.Add(key, cd);
  }
 //Debug.Log(mRoadOfHeavenDatas.Count);
}

            }