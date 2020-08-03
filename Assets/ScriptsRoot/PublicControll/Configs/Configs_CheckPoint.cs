/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (关卡数据表)客户端配置结构体
            /// </summary>
            public partial class Configs_CheckPointData 
             { 
                /// <summary>
                /// 关卡ID--主键
                /// </summary>
                public int CheckPointID{get;set;}

                
                /// <summary>
                /// 章节类型
                /// </summary>
                public int ChapterType { get;set; }
                /// <summary>
                /// 所属章节
                /// </summary>
                public int GenusChapter { get;set; }
                /// <summary>
                /// 关卡类型
                /// </summary>
                public int CheckPointType { get;set; }
                /// <summary>
                /// 关卡序号
                /// </summary>
                public int CheckPointNumber { get;set; }
                /// <summary>
                /// 消耗体力
                /// </summary>
                public int PhysicalExertion { get;set; }
                /// <summary>
                /// 每日次数
                /// </summary>
                public int NumberReset { get;set; }
                /// <summary>
                /// 金币
                /// </summary>
                public int Gold { get;set; }
                /// <summary>
                /// 战队经验
                /// </summary>
                public int LeadingExperience { get;set; }
                /// <summary>
                /// 英雄经验
                /// </summary>
                public int HeroExperience { get;set; }
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
                /// 对话组ID
                /// </summary>
                public List<int> DialogueGroupID { get;set; }
                /// <summary>
                /// 首战掉落
                /// </summary>
                public int FirstDrop { get;set; }
                /// <summary>
                /// 英雄掉落类型
                /// </summary>
                public int HeroDropType { get;set; }
                /// <summary>
                /// 英雄掉落ID
                /// </summary>
                public List<int> HeroDropID { get;set; }
                /// <summary>
                /// 英雄掉落概率常数（10w）
                /// </summary>
                public int HeroProb { get;set; }
                /// <summary>
                /// 掉落ID
                /// </summary>
                public int DropID { get;set; }
                /// <summary>
                /// 隐藏掉落
                /// </summary>
                public int HiddenDrop { get;set; }
                /// <summary>
                /// 解锁/锁定关卡
                /// </summary>
                public int UnlockLockID { get;set; }
                /// <summary>
                /// 头目展示
                /// </summary>
                public int BOSSShow { get;set; }
                /// <summary>
                /// 建筑X坐标
                /// </summary>
                public float BuildingCoordinateX { get;set; }
                /// <summary>
                /// 建筑Y坐标
                /// </summary>
                public float BuildingCoordinateY { get;set; }
                /// <summary>
                /// 关卡名
                /// </summary>
                public string CheckPointName { get;set; }
                /// <summary>
                /// 剧情描述
                /// </summary>
                public string PlotDescription { get;set; }
                /// <summary>
                /// 关卡建筑名
                /// </summary>
                public string CheckPointBuildingName { get;set; }
                /// <summary>
                /// 战斗场景
                /// </summary>
                public string BattleScene { get;set; }
            } 
            /// <summary>
            /// (关卡数据表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_CheckPoint
            { 

                static Configs_CheckPoint _sInstance;
                public static Configs_CheckPoint sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_CheckPoint();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (关卡数据表)字典集合
                /// </summary>
                public Dictionary<int, Configs_CheckPointData> mCheckPointDatas
                {
                    get { return _CheckPointDatas; }
                }

                /// <summary>
                /// (关卡数据表)字典集合
                /// </summary>
                Dictionary<int, Configs_CheckPointData> _CheckPointDatas = new Dictionary<int, Configs_CheckPointData>();

                /// <summary>
                /// 根据CheckPointID读取对应的配置信息
                /// </summary>
                /// <param name="CheckPointID">配置的CheckPointID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_CheckPointData GetCheckPointDataByCheckPointID(int CheckPointID)
                {
                    if (_CheckPointDatas.ContainsKey(CheckPointID))
                    {
                        return _CheckPointDatas[CheckPointID];
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
  Configs_CheckPointData cd = new Configs_CheckPointData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.CheckPointID = key; 
  cd.ChapterType =  Util.GetIntKeyValue(body,"ChapterType"); 
  cd.GenusChapter =  Util.GetIntKeyValue(body,"GenusChapter"); 
  cd.CheckPointType =  Util.GetIntKeyValue(body,"CheckPointType"); 
  cd.CheckPointNumber =  Util.GetIntKeyValue(body,"CheckPointNumber"); 
  cd.PhysicalExertion =  Util.GetIntKeyValue(body,"PhysicalExertion"); 
  cd.NumberReset =  Util.GetIntKeyValue(body,"NumberReset"); 
  cd.Gold =  Util.GetIntKeyValue(body,"Gold"); 
  cd.LeadingExperience =  Util.GetIntKeyValue(body,"LeadingExperience"); 
  cd.HeroExperience =  Util.GetIntKeyValue(body,"HeroExperience"); 
 
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
 
 string[] DialogueGroupIDStrs= Util.GetStringKeyValue(body, "DialogueGroupID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.DialogueGroupID = new List<int>();
foreach(string DialogueGroupIDStr in DialogueGroupIDStrs)  cd.DialogueGroupID.Add(Util.ParseToInt(DialogueGroupIDStr)); 
 
 cd.FirstDrop =  Util.GetIntKeyValue(body,"FirstDrop"); 
  cd.HeroDropType =  Util.GetIntKeyValue(body,"HeroDropType"); 
 
 string[] HeroDropIDStrs= Util.GetStringKeyValue(body, "HeroDropID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.HeroDropID = new List<int>();
foreach(string HeroDropIDStr in HeroDropIDStrs)  cd.HeroDropID.Add(Util.ParseToInt(HeroDropIDStr)); 
 
 cd.HeroProb =  Util.GetIntKeyValue(body,"HeroProb"); 
  cd.DropID =  Util.GetIntKeyValue(body,"DropID"); 
  cd.HiddenDrop =  Util.GetIntKeyValue(body,"HiddenDrop"); 
  cd.UnlockLockID =  Util.GetIntKeyValue(body,"UnlockLockID"); 
  cd.BOSSShow =  Util.GetIntKeyValue(body,"BOSSShow"); 
  cd.BuildingCoordinateX =  Util.GetFloatKeyValue(body,"BuildingCoordinateX"); 
  cd.BuildingCoordinateY =  Util.GetFloatKeyValue(body,"BuildingCoordinateY"); 
  cd.CheckPointName =  Util.GetStringKeyValue(body,"CheckPointName"); 
  cd.PlotDescription =  Util.GetStringKeyValue(body,"PlotDescription"); 
  cd.CheckPointBuildingName =  Util.GetStringKeyValue(body,"CheckPointBuildingName"); 
  cd.BattleScene =  Util.GetStringKeyValue(body,"BattleScene"); 
  
 if (mCheckPointDatas.ContainsKey(key) == false)
 mCheckPointDatas.Add(key, cd);
  }
 //Debug.Log(mCheckPointDatas.Count);
}

            }