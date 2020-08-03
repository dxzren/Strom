/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (秘境塔)客户端配置结构体
            /// </summary>
            public partial class Configs_MysteriousTowerData 
             { 
                /// <summary>
                /// 副本ID--主键
                /// </summary>
                public int DungeonID{get;set;}

                
                /// <summary>
                /// 开放等级
                /// </summary>
                public int LevelLimit { get;set; }
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
                public List<int> NPCLevel { get;set; }
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
                /// 掉落类型
                /// </summary>
                public List<int> PropType { get;set; }
                /// <summary>
                /// 掉落道具ID
                /// </summary>
                public List<int> PropID { get;set; }
                /// <summary>
                /// S评分掉落数量
                /// </summary>
                public List<int> Number4 { get;set; }
                /// <summary>
                /// A评分掉落数量
                /// </summary>
                public List<int> Number3 { get;set; }
                /// <summary>
                /// B评分掉落数量
                /// </summary>
                public List<int> Number2 { get;set; }
                /// <summary>
                /// C评分掉落数量
                /// </summary>
                public List<int> Number1 { get;set; }
                /// <summary>
                /// 每周最佳奖励
                /// </summary>
                public int BestAward { get;set; }
                /// <summary>
                /// 刷新最佳奖励
                /// </summary>
                public int RefreshBestAward { get;set; }
                /// <summary>
                /// 场景音乐
                /// </summary>
                public string TMusic { get;set; }
                /// <summary>
                /// 层级背景
                /// </summary>
                public string FloorScene { get;set; }
                /// <summary>
                /// 战斗场景
                /// </summary>
                public string FightScene { get;set; }
                /// <summary>
                /// 层级名称
                /// </summary>
                public string Name { get;set; }
                /// <summary>
                /// 层级描述
                /// </summary>
                public string Description { get;set; }
            } 
            /// <summary>
            /// (秘境塔)客户端配置数据集合类
            /// </summary>
            public partial class Configs_MysteriousTower
            { 

                static Configs_MysteriousTower _sInstance;
                public static Configs_MysteriousTower sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_MysteriousTower();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (秘境塔)字典集合
                /// </summary>
                public Dictionary<int, Configs_MysteriousTowerData> mMysteriousTowerDatas
                {
                    get { return _MysteriousTowerDatas; }
                }

                /// <summary>
                /// (秘境塔)字典集合
                /// </summary>
                Dictionary<int, Configs_MysteriousTowerData> _MysteriousTowerDatas = new Dictionary<int, Configs_MysteriousTowerData>();

                /// <summary>
                /// 根据DungeonID读取对应的配置信息
                /// </summary>
                /// <param name="DungeonID">配置的DungeonID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_MysteriousTowerData GetMysteriousTowerDataByDungeonID(int DungeonID)
                {
                    if (_MysteriousTowerDatas.ContainsKey(DungeonID))
                    {
                        return _MysteriousTowerDatas[DungeonID];
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
  Configs_MysteriousTowerData cd = new Configs_MysteriousTowerData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.DungeonID = key; 
  cd.LevelLimit =  Util.GetIntKeyValue(body,"LevelLimit"); 
 
 string[] ArrayIDStrs= Util.GetStringKeyValue(body, "ArrayID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.ArrayID = new List<int>();
foreach(string ArrayIDStr in ArrayIDStrs)  cd.ArrayID.Add(Util.ParseToInt(ArrayIDStr)); 
 
 cd.NPCQuality =  Util.GetIntKeyValue(body,"NPCQuality"); 
  cd.NPCStar =  Util.GetIntKeyValue(body,"NPCStar"); 
 
 string[] NPCLevelStrs= Util.GetStringKeyValue(body, "NPCLevel").TrimStart('{').TrimEnd('}',',').Split(',');
cd.NPCLevel = new List<int>();
foreach(string NPCLevelStr in NPCLevelStrs)  cd.NPCLevel.Add(Util.ParseToInt(NPCLevelStr)); 
 
 cd.UltSkill =  Util.GetIntKeyValue(body,"UltSkill"); 
  cd.ActiveSkill1Level =  Util.GetIntKeyValue(body,"ActiveSkill1Level"); 
  cd.ActiveSkill2Level =  Util.GetIntKeyValue(body,"ActiveSkill2Level"); 
  cd.PassiveSkillLevel =  Util.GetIntKeyValue(body,"PassiveSkillLevel"); 
 
 string[] BossIDStrs= Util.GetStringKeyValue(body, "BossID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BossID = new List<int>();
foreach(string BossIDStr in BossIDStrs)  cd.BossID.Add(Util.ParseToInt(BossIDStr)); 
 
 cd.NPCAdjust =  Util.GetIntKeyValue(body,"NPCAdjust"); 
  cd.BossAdjust =  Util.GetIntKeyValue(body,"BossAdjust"); 
 
 string[] PropTypeStrs= Util.GetStringKeyValue(body, "PropType").TrimStart('{').TrimEnd('}',',').Split(',');
cd.PropType = new List<int>();
foreach(string PropTypeStr in PropTypeStrs)  cd.PropType.Add(Util.ParseToInt(PropTypeStr)); 
 

 string[] PropIDStrs= Util.GetStringKeyValue(body, "PropID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.PropID = new List<int>();
foreach(string PropIDStr in PropIDStrs)  cd.PropID.Add(Util.ParseToInt(PropIDStr)); 
 

 string[] Number4Strs= Util.GetStringKeyValue(body, "Number4").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Number4 = new List<int>();
foreach(string Number4Str in Number4Strs)  cd.Number4.Add(Util.ParseToInt(Number4Str)); 
 

 string[] Number3Strs= Util.GetStringKeyValue(body, "Number3").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Number3 = new List<int>();
foreach(string Number3Str in Number3Strs)  cd.Number3.Add(Util.ParseToInt(Number3Str)); 
 

 string[] Number2Strs= Util.GetStringKeyValue(body, "Number2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Number2 = new List<int>();
foreach(string Number2Str in Number2Strs)  cd.Number2.Add(Util.ParseToInt(Number2Str)); 
 

 string[] Number1Strs= Util.GetStringKeyValue(body, "Number1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Number1 = new List<int>();
foreach(string Number1Str in Number1Strs)  cd.Number1.Add(Util.ParseToInt(Number1Str)); 
 
 cd.BestAward =  Util.GetIntKeyValue(body,"BestAward"); 
  cd.RefreshBestAward =  Util.GetIntKeyValue(body,"RefreshBestAward"); 
  cd.TMusic =  Util.GetStringKeyValue(body,"TMusic"); 
  cd.FloorScene =  Util.GetStringKeyValue(body,"FloorScene"); 
  cd.FightScene =  Util.GetStringKeyValue(body,"FightScene"); 
  cd.Name =  Util.GetStringKeyValue(body,"Name"); 
  cd.Description =  Util.GetStringKeyValue(body,"Description"); 
  
 if (mMysteriousTowerDatas.ContainsKey(key) == false)
 mMysteriousTowerDatas.Add(key, cd);
  }
 //Debug.Log(mMysteriousTowerDatas.Count);
}

            }