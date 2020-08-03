/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (技能特效骨骼点匹配)客户端配置结构体
            /// </summary>
            public partial class Configs_SkillBonesMatchData 
             { 
                /// <summary>
                /// 资源ID--主键
                /// </summary>
                public int ResourceID{get;set;}

                
                /// <summary>
                /// 大招蓄力骨骼点1
                /// </summary>
                public string UltimateHoldBones1 { get;set; }
                /// <summary>
                /// 大招蓄力添加特效1
                /// </summary>
                public string UltimateHoldAddEffects1 { get;set; }
                /// <summary>
                /// 大招蓄力骨骼点2
                /// </summary>
                public string UltimateHoldBones2 { get;set; }
                /// <summary>
                /// 大招蓄力添加特效2
                /// </summary>
                public string UltimateHoldAddEffects2 { get;set; }
                /// <summary>
                /// 大招蓄力骨骼点3
                /// </summary>
                public string UltimateHoldBones3 { get;set; }
                /// <summary>
                /// 大招蓄力添加特效3
                /// </summary>
                public string UltimateHoldAddEffects3 { get;set; }
                /// <summary>
                /// 大招攻击骨骼点1
                /// </summary>
                public string UltimateAttackBones1 { get;set; }
                /// <summary>
                /// 大招攻击添加特效1
                /// </summary>
                public string UltimateAttackAddEffects1 { get;set; }
                /// <summary>
                /// 大招攻击骨骼点2
                /// </summary>
                public string UltimateAttackBones2 { get;set; }
                /// <summary>
                /// 大招攻击添加特效2
                /// </summary>
                public string UltimateAttackAddEffects2 { get;set; }
                /// <summary>
                /// 大招攻击骨骼点3
                /// </summary>
                public string UltimateAttackBones3 { get;set; }
                /// <summary>
                /// 大招攻击添加特效3
                /// </summary>
                public string UltimateAttackAddEffects3 { get;set; }
                /// <summary>
                /// 主动技能1骨骼点1
                /// </summary>
                public string ActiveAttack1Bones1 { get;set; }
                /// <summary>
                /// 主动技能1添加特效1
                /// </summary>
                public string ActiveAttack1AddEffect1 { get;set; }
                /// <summary>
                /// 主动技能1骨骼点2
                /// </summary>
                public string ActiveAttack1Bones2 { get;set; }
                /// <summary>
                /// 主动技能1添加特效2
                /// </summary>
                public string ActiveAttack1AddEffect2 { get;set; }
                /// <summary>
                /// 主动技能1骨骼点3
                /// </summary>
                public string ActiveAttack1Bones3 { get;set; }
                /// <summary>
                /// 主动技能1添加特效3
                /// </summary>
                public string ActiveAttack1AddEffect3 { get;set; }
                /// <summary>
                /// 主动技能2骨骼点1
                /// </summary>
                public string ActiveAttack2Bones1 { get;set; }
                /// <summary>
                /// 主动技能2添加特效1
                /// </summary>
                public string ActiveAttack2AddEffect1 { get;set; }
                /// <summary>
                /// 主动技能2骨骼点2
                /// </summary>
                public string ActiveAttack2Bones2 { get;set; }
                /// <summary>
                /// 主动技能2添加特效2
                /// </summary>
                public string ActiveAttack2AddEffect2 { get;set; }
                /// <summary>
                /// 主动技能2骨骼点3
                /// </summary>
                public string ActiveAttack2Bones3 { get;set; }
                /// <summary>
                /// 主动技能2添加特效3
                /// </summary>
                public string ActiveAttack2AddEffect3 { get;set; }
            } 
            /// <summary>
            /// (技能特效骨骼点匹配)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SkillBonesMatch
            { 

                static Configs_SkillBonesMatch _sInstance;
                public static Configs_SkillBonesMatch sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SkillBonesMatch();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (技能特效骨骼点匹配)字典集合
                /// </summary>
                public Dictionary<int, Configs_SkillBonesMatchData> mSkillBonesMatchDatas
                {
                    get { return _SkillBonesMatchDatas; }
                }

                /// <summary>
                /// (技能特效骨骼点匹配)字典集合
                /// </summary>
                Dictionary<int, Configs_SkillBonesMatchData> _SkillBonesMatchDatas = new Dictionary<int, Configs_SkillBonesMatchData>();

                /// <summary>
                /// 根据ResourceID读取对应的配置信息
                /// </summary>
                /// <param name="ResourceID">配置的ResourceID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SkillBonesMatchData GetSkillBonesMatchDataByResourceID(int ResourceID)
                {
                    if (_SkillBonesMatchDatas.ContainsKey(ResourceID))
                    {
                        return _SkillBonesMatchDatas[ResourceID];
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
  Configs_SkillBonesMatchData cd = new Configs_SkillBonesMatchData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ResourceID = key; 
  cd.UltimateHoldBones1 =  Util.GetStringKeyValue(body,"UltimateHoldBones1"); 
  cd.UltimateHoldAddEffects1 =  Util.GetStringKeyValue(body,"UltimateHoldAddEffects1"); 
  cd.UltimateHoldBones2 =  Util.GetStringKeyValue(body,"UltimateHoldBones2"); 
  cd.UltimateHoldAddEffects2 =  Util.GetStringKeyValue(body,"UltimateHoldAddEffects2"); 
  cd.UltimateHoldBones3 =  Util.GetStringKeyValue(body,"UltimateHoldBones3"); 
  cd.UltimateHoldAddEffects3 =  Util.GetStringKeyValue(body,"UltimateHoldAddEffects3"); 
  cd.UltimateAttackBones1 =  Util.GetStringKeyValue(body,"UltimateAttackBones1"); 
  cd.UltimateAttackAddEffects1 =  Util.GetStringKeyValue(body,"UltimateAttackAddEffects1"); 
  cd.UltimateAttackBones2 =  Util.GetStringKeyValue(body,"UltimateAttackBones2"); 
  cd.UltimateAttackAddEffects2 =  Util.GetStringKeyValue(body,"UltimateAttackAddEffects2"); 
  cd.UltimateAttackBones3 =  Util.GetStringKeyValue(body,"UltimateAttackBones3"); 
  cd.UltimateAttackAddEffects3 =  Util.GetStringKeyValue(body,"UltimateAttackAddEffects3"); 
  cd.ActiveAttack1Bones1 =  Util.GetStringKeyValue(body,"ActiveAttack1Bones1"); 
  cd.ActiveAttack1AddEffect1 =  Util.GetStringKeyValue(body,"ActiveAttack1AddEffect1"); 
  cd.ActiveAttack1Bones2 =  Util.GetStringKeyValue(body,"ActiveAttack1Bones2"); 
  cd.ActiveAttack1AddEffect2 =  Util.GetStringKeyValue(body,"ActiveAttack1AddEffect2"); 
  cd.ActiveAttack1Bones3 =  Util.GetStringKeyValue(body,"ActiveAttack1Bones3"); 
  cd.ActiveAttack1AddEffect3 =  Util.GetStringKeyValue(body,"ActiveAttack1AddEffect3"); 
  cd.ActiveAttack2Bones1 =  Util.GetStringKeyValue(body,"ActiveAttack2Bones1"); 
  cd.ActiveAttack2AddEffect1 =  Util.GetStringKeyValue(body,"ActiveAttack2AddEffect1"); 
  cd.ActiveAttack2Bones2 =  Util.GetStringKeyValue(body,"ActiveAttack2Bones2"); 
  cd.ActiveAttack2AddEffect2 =  Util.GetStringKeyValue(body,"ActiveAttack2AddEffect2"); 
  cd.ActiveAttack2Bones3 =  Util.GetStringKeyValue(body,"ActiveAttack2Bones3"); 
  cd.ActiveAttack2AddEffect3 =  Util.GetStringKeyValue(body,"ActiveAttack2AddEffect3"); 
  
 if (mSkillBonesMatchDatas.ContainsKey(key) == false)
 mSkillBonesMatchDatas.Add(key, cd);
  }
 //Debug.Log(mSkillBonesMatchDatas.Count);
}

            }