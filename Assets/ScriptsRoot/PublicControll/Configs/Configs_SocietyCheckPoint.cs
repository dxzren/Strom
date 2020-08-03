/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公会副本关卡数据表)客户端配置结构体
            /// </summary>
            public partial class Configs_SocietyCheckPointData 
             { 
                /// <summary>
                /// 关卡ID--主键
                /// </summary>
                public int CheckPointID{get;set;}

                
                /// <summary>
                /// 关卡名
                /// </summary>
                public string CheckPointName { get;set; }
                /// <summary>
                /// 所属章节
                /// </summary>
                public int GenusChapter { get;set; }
                /// <summary>
                /// 关卡序号
                /// </summary>
                public int CheckPointNumber { get;set; }
                /// <summary>
                /// 剧情描述
                /// </summary>
                public string PlotDescription { get;set; }
                /// <summary>
                /// 阵容ID
                /// </summary>
                public List<int> ArrayID { get;set; }
                /// <summary>
                /// 怪物品质
                /// </summary>
                public int NPCQuality { get;set; }
                /// <summary>
                /// 怪物星级
                /// </summary>
                public int NPCStar { get;set; }
                /// <summary>
                /// 怪物等级
                /// </summary>
                public int NPCLevel { get;set; }
                /// <summary>
                /// 大招等级
                /// </summary>
                public int UltSkill { get;set; }
                /// <summary>
                /// 主动技能1等级
                /// </summary>
                public int ActiveSkill1Level { get;set; }
                /// <summary>
                /// 主动技能2等级
                /// </summary>
                public int ActiveSkill2Level { get;set; }
                /// <summary>
                /// 被动技能等级
                /// </summary>
                public int PassiveSkillLevel { get;set; }
                /// <summary>
                /// 头目位置
                /// </summary>
                public List<int> BossID { get;set; }
                /// <summary>
                /// NPC属性调整
                /// </summary>
                public int NPCAdjust { get;set; }
                /// <summary>
                /// 头目调整属性
                /// </summary>
                public int BossAdjust { get;set; }
                /// <summary>
                /// 血量倍数
                /// </summary>
                public int BloodCoefficient { get;set; }
                /// <summary>
                /// 关卡建筑名
                /// </summary>
                public string CheckPointBuildingName { get;set; }
                /// <summary>
                /// 建筑X坐标
                /// </summary>
                public float BuildingCoordinateX { get;set; }
                /// <summary>
                /// 建筑Y坐标
                /// </summary>
                public float BuildingCoordinateY { get;set; }
                /// <summary>
                /// 装备掉落
                /// </summary>
                public List<int> EquipDropID { get;set; }
                /// <summary>
                /// 装备分布
                /// </summary>
                public List<int> EquipDistribution { get;set; }
            } 
            /// <summary>
            /// (公会副本关卡数据表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SocietyCheckPoint
            { 

                static Configs_SocietyCheckPoint _sInstance;
                public static Configs_SocietyCheckPoint sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SocietyCheckPoint();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公会副本关卡数据表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SocietyCheckPointData> mSocietyCheckPointDatas
                {
                    get { return _SocietyCheckPointDatas; }
                }

                /// <summary>
                /// (公会副本关卡数据表)字典集合
                /// </summary>
                Dictionary<int, Configs_SocietyCheckPointData> _SocietyCheckPointDatas = new Dictionary<int, Configs_SocietyCheckPointData>();

                /// <summary>
                /// 根据CheckPointID读取对应的配置信息
                /// </summary>
                /// <param name="CheckPointID">配置的CheckPointID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SocietyCheckPointData GetSocietyCheckPointDataByCheckPointID(int CheckPointID)
                {
                    if (_SocietyCheckPointDatas.ContainsKey(CheckPointID))
                    {
                        return _SocietyCheckPointDatas[CheckPointID];
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
  Configs_SocietyCheckPointData cd = new Configs_SocietyCheckPointData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.CheckPointID = key; 
  cd.CheckPointName =  Util.GetStringKeyValue(body,"CheckPointName"); 
  cd.GenusChapter =  Util.GetIntKeyValue(body,"GenusChapter"); 
  cd.CheckPointNumber =  Util.GetIntKeyValue(body,"CheckPointNumber"); 
  cd.PlotDescription =  Util.GetStringKeyValue(body,"PlotDescription"); 
 
 string[] ArrayIDStrs= Util.GetStringKeyValue(body, "ArrayID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.ArrayID = new List<int>();
foreach(string ArrayIDStr in ArrayIDStrs)  cd.ArrayID.Add(Util.ParseToInt(ArrayIDStr)); 
 
 cd.NPCQuality =  Util.GetIntKeyValue(body,"NPCQuality"); 
  cd.NPCStar =  Util.GetIntKeyValue(body,"NPCStar"); 
  cd.NPCLevel =  Util.GetIntKeyValue(body,"NPCLevel"); 
  cd.UltSkill =  Util.GetIntKeyValue(body,"UltSkill"); 
  cd.ActiveSkill1Level =  Util.GetIntKeyValue(body,"ActiveSkill1Level"); 
  cd.ActiveSkill2Level =  Util.GetIntKeyValue(body,"ActiveSkill2Level"); 
  cd.PassiveSkillLevel =  Util.GetIntKeyValue(body,"PassiveSkillLevel"); 
 
 string[] BossIDStrs= Util.GetStringKeyValue(body, "BossID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BossID = new List<int>();
foreach(string BossIDStr in BossIDStrs)  cd.BossID.Add(Util.ParseToInt(BossIDStr)); 
 
 cd.NPCAdjust =  Util.GetIntKeyValue(body,"NPCAdjust"); 
  cd.BossAdjust =  Util.GetIntKeyValue(body,"BossAdjust"); 
  cd.BloodCoefficient =  Util.GetIntKeyValue(body,"BloodCoefficient"); 
  cd.CheckPointBuildingName =  Util.GetStringKeyValue(body,"CheckPointBuildingName"); 
  cd.BuildingCoordinateX =  Util.GetFloatKeyValue(body,"BuildingCoordinateX"); 
  cd.BuildingCoordinateY =  Util.GetFloatKeyValue(body,"BuildingCoordinateY"); 
 
 string[] EquipDropIDStrs= Util.GetStringKeyValue(body, "EquipDropID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.EquipDropID = new List<int>();
foreach(string EquipDropIDStr in EquipDropIDStrs)  cd.EquipDropID.Add(Util.ParseToInt(EquipDropIDStr)); 
 

 string[] EquipDistributionStrs= Util.GetStringKeyValue(body, "EquipDistribution").TrimStart('{').TrimEnd('}',',').Split(',');
cd.EquipDistribution = new List<int>();
foreach(string EquipDistributionStr in EquipDistributionStrs)  cd.EquipDistribution.Add(Util.ParseToInt(EquipDistributionStr)); 
 
 
 if (mSocietyCheckPointDatas.ContainsKey(key) == false)
 mSocietyCheckPointDatas.Add(key, cd);
  }
 //Debug.Log(mSocietyCheckPointDatas.Count);
}

            }