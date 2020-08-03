/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (技能消耗)客户端配置结构体
            /// </summary>
            public partial class Configs_SkillConsumeData 
             { 
                /// <summary>
                /// 技能等级--主键
                /// </summary>
                public int SkillLevel{get;set;}

                
                /// <summary>
                /// 大招消耗
                /// </summary>
                public int UltSkillConsume { get;set; }
                /// <summary>
                /// 主动技能1消耗
                /// </summary>
                public int ActiveSkill1Consume { get;set; }
                /// <summary>
                /// 主动技能2消耗
                /// </summary>
                public int ActiveSkill2Consume { get;set; }
                /// <summary>
                /// 被动技能消耗
                /// </summary>
                public int PassiveSkillConsume { get;set; }
            } 
            /// <summary>
            /// (技能消耗)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SkillConsume
            { 

                static Configs_SkillConsume _sInstance;
                public static Configs_SkillConsume sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SkillConsume();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (技能消耗)字典集合
                /// </summary>
                public Dictionary<int, Configs_SkillConsumeData> mSkillConsumeDatas
                {
                    get { return _SkillConsumeDatas; }
                }

                /// <summary>
                /// (技能消耗)字典集合
                /// </summary>
                Dictionary<int, Configs_SkillConsumeData> _SkillConsumeDatas = new Dictionary<int, Configs_SkillConsumeData>();

                /// <summary>
                /// 根据SkillLevel读取对应的配置信息
                /// </summary>
                /// <param name="SkillLevel">配置的SkillLevel</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SkillConsumeData GetSkillConsumeDataBySkillLevel(int SkillLevel)
                {
                    if (_SkillConsumeDatas.ContainsKey(SkillLevel))
                    {
                        return _SkillConsumeDatas[SkillLevel];
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
  Configs_SkillConsumeData cd = new Configs_SkillConsumeData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.SkillLevel = key; 
  cd.UltSkillConsume =  Util.GetIntKeyValue(body,"UltSkillConsume"); 
  cd.ActiveSkill1Consume =  Util.GetIntKeyValue(body,"ActiveSkill1Consume"); 
  cd.ActiveSkill2Consume =  Util.GetIntKeyValue(body,"ActiveSkill2Consume"); 
  cd.PassiveSkillConsume =  Util.GetIntKeyValue(body,"PassiveSkillConsume"); 
  
 if (mSkillConsumeDatas.ContainsKey(key) == false)
 mSkillConsumeDatas.Add(key, cd);
  }
 //Debug.Log(mSkillConsumeDatas.Count);
}

            }