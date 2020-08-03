/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (英雄模板)客户端配置结构体
            /// </summary>
            public partial class Configs_HeroData 
             { 
                /// <summary>
                /// 英雄ID--主键
                /// </summary>
                public int HeroID{get;set;}

                
                /// <summary>
                /// 对应魂石ID
                /// </summary>
                public int SoulID { get;set; }
                /// <summary>
                /// 指向战队头像ID
                /// </summary>
                public int TroopsID { get;set; }
                /// <summary>
                /// 初始翅膀ID
                /// </summary>
                public int WingID { get;set; }
                /// <summary>
                /// 初始专属英雄
                /// </summary>
                public int InitializeHeroID { get;set; }
                /// <summary>
                /// 英雄种族
                /// </summary>
                public int HeroRace { get;set; }
                /// <summary>
                /// 性别
                /// </summary>
                public int Gender { get;set; }
                /// <summary>
                /// 英雄类型
                /// </summary>
                public int HeroType { get;set; }
                /// <summary>
                /// 职业类型
                /// </summary>
                public int Profession { get;set; }
                /// <summary>
                /// 职业类别
                /// </summary>
                public int Profession2 { get;set; }
                /// <summary>
                /// 相克属性
                /// </summary>
                public int Polarity { get;set; }
                /// <summary>
                /// 战斗位置
                /// </summary>
                public int Position { get;set; }
                /// <summary>
                /// 初始星级
                /// </summary>
                public int InitialStar { get;set; }
                /// <summary>
                /// 生命
                /// </summary>
                public int Blood { get;set; }
                /// <summary>
                /// 物理攻击
                /// </summary>
                public int PhysicalAttack { get;set; }
                /// <summary>
                /// 魔法强度
                /// </summary>
                public int MagicAttack { get;set; }
                /// <summary>
                /// 物理护甲
                /// </summary>
                public int PhysicalArmor { get;set; }
                /// <summary>
                /// 魔法抗性
                /// </summary>
                public int MagicArmor { get;set; }
                /// <summary>
                /// 物理暴击
                /// </summary>
                public int PhysicalCritical { get;set; }
                /// <summary>
                /// 1星
                /// </summary>
                public int Star1 { get;set; }
                /// <summary>
                /// 2星
                /// </summary>
                public int Star2 { get;set; }
                /// <summary>
                /// 3星
                /// </summary>
                public int Star3 { get;set; }
                /// <summary>
                /// 4星
                /// </summary>
                public int Star4 { get;set; }
                /// <summary>
                /// 5星
                /// </summary>
                public int Star5 { get;set; }
                /// <summary>
                /// 白
                /// </summary>
                public int White { get;set; }
                /// <summary>
                /// 绿
                /// </summary>
                public int Green { get;set; }
                /// <summary>
                /// 绿1
                /// </summary>
                public int Green1 { get;set; }
                /// <summary>
                /// 蓝
                /// </summary>
                public int Blue { get;set; }
                /// <summary>
                /// 蓝1
                /// </summary>
                public int Blue1 { get;set; }
                /// <summary>
                /// 蓝2
                /// </summary>
                public int Blue2 { get;set; }
                /// <summary>
                /// 紫
                /// </summary>
                public int Purple { get;set; }
                /// <summary>
                /// 紫1
                /// </summary>
                public int Purple1 { get;set; }
                /// <summary>
                /// 紫2
                /// </summary>
                public int Purple2 { get;set; }
                /// <summary>
                /// 紫3
                /// </summary>
                public int Purple3 { get;set; }
                /// <summary>
                /// 金
                /// </summary>
                public int Gold { get;set; }
                /// <summary>
                /// 大招技能
                /// </summary>
                public int UltSkill { get;set; }
                /// <summary>
                /// 主动技能1
                /// </summary>
                public int ActiveSkill1 { get;set; }
                /// <summary>
                /// 主动技能2
                /// </summary>
                public int ActiveSkill2 { get;set; }
                /// <summary>
                /// 被动技能
                /// </summary>
                public int PassiveSkill { get;set; }
                /// <summary>
                /// 初始/最大天赋ID
                /// </summary>
                public List<int> TalentID { get;set; }
                /// <summary>
                /// 英雄描述
                /// </summary>
                public string HeroDes { get;set; }
                /// <summary>
                /// 美术资源
                /// </summary>
                public int Resource { get;set; }
                /// <summary>
                /// 半身像
                /// </summary>
                public string body { get;set; }
                /// <summary>
                /// 头像84
                /// </summary>
                public string head84 { get;set; }
                /// <summary>
                /// 对话半身像
                /// </summary>
                public string Dialoguebody { get;set; }
                /// <summary>
                /// 英雄名
                /// </summary>
                public string HeroName { get;set; }
                /// <summary>
                /// 英雄职业特点
                /// </summary>
                public string HeroCharacter { get;set; }
                /// <summary>
                /// 声音
                /// </summary>
                public string Sound { get;set; }
                /// <summary>
                /// 天赋技能
                /// </summary>
                public List<int> Talent { get;set; }
                /// <summary>
                /// Boss特殊技能
                /// </summary>
                public int BossSkill { get;set; }
                /// <summary>
                /// 佣兵光环
                /// </summary>
                public List<int> Aura { get;set; }
                /// <summary>
                /// 震屏循环位置
                /// </summary>
                public int ShockScreen { get;set; }
            } 
            /// <summary>
            /// (英雄模板)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Hero
            { 

                static Configs_Hero _sInstance;
                public static Configs_Hero sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Hero();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (英雄模板)字典集合
                /// </summary>
                public Dictionary<int, Configs_HeroData> mHeroDatas
                {
                    get { return _HeroDatas; }
                }

                /// <summary>
                /// (英雄模板)字典集合
                /// </summary>
                Dictionary<int, Configs_HeroData> _HeroDatas = new Dictionary<int, Configs_HeroData>();

                /// <summary>
                /// 根据HeroID读取对应的配置信息
                /// </summary>
                /// <param name="HeroID">配置的HeroID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_HeroData GetHeroDataByHeroID(int HeroID)
                {
                    if (_HeroDatas.ContainsKey(HeroID))
                    {
                        return _HeroDatas[HeroID];
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
  Configs_HeroData cd = new Configs_HeroData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.HeroID = key; 
  cd.SoulID =  Util.GetIntKeyValue(body,"SoulID"); 
  cd.TroopsID =  Util.GetIntKeyValue(body,"TroopsID"); 
  cd.WingID =  Util.GetIntKeyValue(body,"WingID"); 
  cd.InitializeHeroID =  Util.GetIntKeyValue(body,"InitializeHeroID"); 
  cd.HeroRace =  Util.GetIntKeyValue(body,"HeroRace"); 
  cd.Gender =  Util.GetIntKeyValue(body,"Gender"); 
  cd.HeroType =  Util.GetIntKeyValue(body,"HeroType"); 
  cd.Profession =  Util.GetIntKeyValue(body,"Profession"); 
  cd.Profession2 =  Util.GetIntKeyValue(body,"Profession2"); 
  cd.Polarity =  Util.GetIntKeyValue(body,"Polarity"); 
  cd.Position =  Util.GetIntKeyValue(body,"Position"); 
  cd.InitialStar =  Util.GetIntKeyValue(body,"InitialStar"); 
  cd.Blood =  Util.GetIntKeyValue(body,"Blood"); 
  cd.PhysicalAttack =  Util.GetIntKeyValue(body,"PhysicalAttack"); 
  cd.MagicAttack =  Util.GetIntKeyValue(body,"MagicAttack"); 
  cd.PhysicalArmor =  Util.GetIntKeyValue(body,"PhysicalArmor"); 
  cd.MagicArmor =  Util.GetIntKeyValue(body,"MagicArmor"); 
  cd.PhysicalCritical =  Util.GetIntKeyValue(body,"PhysicalCritical"); 
  cd.Star1 =  Util.GetIntKeyValue(body,"Star1"); 
  cd.Star2 =  Util.GetIntKeyValue(body,"Star2"); 
  cd.Star3 =  Util.GetIntKeyValue(body,"Star3"); 
  cd.Star4 =  Util.GetIntKeyValue(body,"Star4"); 
  cd.Star5 =  Util.GetIntKeyValue(body,"Star5"); 
  cd.White =  Util.GetIntKeyValue(body,"White"); 
  cd.Green =  Util.GetIntKeyValue(body,"Green"); 
  cd.Green1 =  Util.GetIntKeyValue(body,"Green1"); 
  cd.Blue =  Util.GetIntKeyValue(body,"Blue"); 
  cd.Blue1 =  Util.GetIntKeyValue(body,"Blue1"); 
  cd.Blue2 =  Util.GetIntKeyValue(body,"Blue2"); 
  cd.Purple =  Util.GetIntKeyValue(body,"Purple"); 
  cd.Purple1 =  Util.GetIntKeyValue(body,"Purple1"); 
  cd.Purple2 =  Util.GetIntKeyValue(body,"Purple2"); 
  cd.Purple3 =  Util.GetIntKeyValue(body,"Purple3"); 
  cd.Gold =  Util.GetIntKeyValue(body,"Gold"); 
  cd.UltSkill =  Util.GetIntKeyValue(body,"UltSkill"); 
  cd.ActiveSkill1 =  Util.GetIntKeyValue(body,"ActiveSkill1"); 
  cd.ActiveSkill2 =  Util.GetIntKeyValue(body,"ActiveSkill2"); 
  cd.PassiveSkill =  Util.GetIntKeyValue(body,"PassiveSkill"); 
 
 string[] TalentIDStrs= Util.GetStringKeyValue(body, "TalentID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.TalentID = new List<int>();
foreach(string TalentIDStr in TalentIDStrs)  cd.TalentID.Add(Util.ParseToInt(TalentIDStr)); 
 
 cd.HeroDes =  Util.GetStringKeyValue(body,"HeroDes"); 
  cd.Resource =  Util.GetIntKeyValue(body,"Resource"); 
  cd.body =  Util.GetStringKeyValue(body,"body"); 
  cd.head84 =  Util.GetStringKeyValue(body,"head84"); 
  cd.Dialoguebody =  Util.GetStringKeyValue(body,"Dialoguebody"); 
  cd.HeroName =  Util.GetStringKeyValue(body,"HeroName"); 
  cd.HeroCharacter =  Util.GetStringKeyValue(body,"HeroCharacter"); 
  cd.Sound =  Util.GetStringKeyValue(body,"Sound"); 
 
 string[] TalentStrs= Util.GetStringKeyValue(body, "Talent").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Talent = new List<int>();
foreach(string TalentStr in TalentStrs)  cd.Talent.Add(Util.ParseToInt(TalentStr)); 
 
 cd.BossSkill =  Util.GetIntKeyValue(body,"BossSkill"); 
 
 string[] AuraStrs= Util.GetStringKeyValue(body, "Aura").TrimStart('{').TrimEnd('}',',').Split(',');
cd.Aura = new List<int>();
foreach(string AuraStr in AuraStrs)  cd.Aura.Add(Util.ParseToInt(AuraStr)); 
 
 cd.ShockScreen =  Util.GetIntKeyValue(body,"ShockScreen"); 
  
 if (mHeroDatas.ContainsKey(key) == false)
 mHeroDatas.Add(key, cd);
  }
 //Debug.Log(mHeroDatas.Count);
 }

            }