/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (巨龙/巨兽表)客户端配置结构体
            /// </summary>
            public partial class Configs_DragonTrialData 
             { 
                /// <summary>
                /// 副本ID--主键
                /// </summary>
                public int dungeonID{get;set;}

                
                /// <summary>
                /// 类别
                /// </summary>
                public int Type { get;set; }
                /// <summary>
                /// 难度
                /// </summary>
                public int Difficulty { get;set; }
                /// <summary>
                /// 开放等级
                /// </summary>
                public int LevelLimit { get;set; }
                /// <summary>
                /// 消耗体力
                /// </summary>
                public int PhysicalExertion { get;set; }
                /// <summary>
                /// 战队经验
                /// </summary>
                public int LeadingExp { get;set; }
                /// <summary>
                /// 英雄经验
                /// </summary>
                public int HeroExp { get;set; }
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
                /// 掉落碎片ID
                /// </summary>
                public List<int> FragmentID1 { get;set; }
                /// <summary>
                /// 基础数量
                /// </summary>
                public List<int> BaseNumber { get;set; }
                /// <summary>
                /// 调整数量
                /// </summary>
                public List<int> AdjustNumber { get;set; }
                /// <summary>
                /// 击杀奖励
                /// </summary>
                public int KillReward { get;set; }
                /// <summary>
                /// 难度图标
                /// </summary>
                public string icon { get;set; }
                /// <summary>
                /// 战斗场景1
                /// </summary>
                public string BattleScene1 { get;set; }
                /// <summary>
                /// 战斗场景2
                /// </summary>
                public string BattleScene2 { get;set; }
                /// <summary>
                /// 场景描述
                /// </summary>
                public string ScenceDescription { get;set; }
                /// <summary>
                /// 模型
                /// </summary>
                public int mode { get;set; }
                /// <summary>
                /// 对话ID
                /// </summary>
                public List<int> DialogueID { get;set; }
                public int fireControl { set; get; }
    public int IceControl { set; get; }
    public int thunderControl { set; get; }
    public List<int> DragonTrialPassiveSkill { get; set; }
} 
            /// <summary>
            /// (巨龙/巨兽表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_DragonTrial
            { 

                static Configs_DragonTrial _sInstance;
                public static Configs_DragonTrial sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_DragonTrial();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (巨龙/巨兽表)字典集合
                /// </summary>
                public Dictionary<int, Configs_DragonTrialData> mDragonTrialDatas
                {
                    get { return _DragonTrialDatas; }
                }

                /// <summary>
                /// (巨龙/巨兽表)字典集合
                /// </summary>
                Dictionary<int, Configs_DragonTrialData> _DragonTrialDatas = new Dictionary<int, Configs_DragonTrialData>();

                /// <summary>
                /// 根据dungeonID读取对应的配置信息
                /// </summary>
                /// <param name="dungeonID">配置的dungeonID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_DragonTrialData GetDragonTrialDataBydungeonID(int dungeonID)
                {
                    if (_DragonTrialDatas.ContainsKey(dungeonID))
                    {
                        return _DragonTrialDatas[dungeonID];
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
  Configs_DragonTrialData cd = new Configs_DragonTrialData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.dungeonID = key; 
  cd.Type =  Util.GetIntKeyValue(body,"Type"); 
  cd.Difficulty =  Util.GetIntKeyValue(body,"Difficulty"); 
  cd.LevelLimit =  Util.GetIntKeyValue(body,"LevelLimit"); 
  cd.PhysicalExertion =  Util.GetIntKeyValue(body,"PhysicalExertion"); 
  cd.LeadingExp =  Util.GetIntKeyValue(body,"LeadingExp"); 
  cd.HeroExp =  Util.GetIntKeyValue(body,"HeroExp");
            cd.fireControl = Util.GetIntKeyValue(body, "fireControl");
  cd.IceControl = Util.GetIntKeyValue(body, "IceControl");
            cd.thunderControl = Util.GetIntKeyValue(body, "thunderControl");
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


            string[] PassiveSkills = Util.GetStringKeyValue(body, "DragonTrialPassiveSkill").TrimStart('{').TrimEnd('}', ',').Split(',');
            cd.DragonTrialPassiveSkill = new List<int>();
            foreach (string PassiveSkill in PassiveSkills) cd.DragonTrialPassiveSkill.Add(Util.ParseToInt(PassiveSkill));


            cd.NPCAdjust =  Util.GetIntKeyValue(body,"NPCAdjust"); 
  cd.BossAdjust =  Util.GetIntKeyValue(body,"BossAdjust"); 
 
 string[] FragmentID1Strs= Util.GetStringKeyValue(body, "FragmentID1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.FragmentID1 = new List<int>();
foreach(string FragmentID1Str in FragmentID1Strs)  cd.FragmentID1.Add(Util.ParseToInt(FragmentID1Str));

            



 string[] BaseNumberStrs= Util.GetStringKeyValue(body, "BaseNumber").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BaseNumber = new List<int>();
foreach(string BaseNumberStr in BaseNumberStrs)  cd.BaseNumber.Add(Util.ParseToInt(BaseNumberStr)); 
 

 string[] AdjustNumberStrs= Util.GetStringKeyValue(body, "AdjustNumber").TrimStart('{').TrimEnd('}',',').Split(',');
cd.AdjustNumber = new List<int>();
foreach(string AdjustNumberStr in AdjustNumberStrs)  cd.AdjustNumber.Add(Util.ParseToInt(AdjustNumberStr)); 
 
 cd.KillReward =  Util.GetIntKeyValue(body,"KillReward"); 
  cd.icon =  Util.GetStringKeyValue(body,"icon"); 
  cd.BattleScene1 =  Util.GetStringKeyValue(body,"BattleScene1"); 
  cd.BattleScene2 =  Util.GetStringKeyValue(body,"BattleScene2"); 
  cd.ScenceDescription =  Util.GetStringKeyValue(body,"ScenceDescription"); 
  cd.mode =  Util.GetIntKeyValue(body,"mode"); 
 
 string[] DialogueIDStrs= Util.GetStringKeyValue(body, "DialogueID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.DialogueID = new List<int>();
foreach(string DialogueIDStr in DialogueIDStrs)  cd.DialogueID.Add(Util.ParseToInt(DialogueIDStr)); 
 
 
 if (mDragonTrialDatas.ContainsKey(key) == false)
 mDragonTrialDatas.Add(key, cd);
  }
 //Debug.Log(mDragonTrialDatas.Count);
}

            }