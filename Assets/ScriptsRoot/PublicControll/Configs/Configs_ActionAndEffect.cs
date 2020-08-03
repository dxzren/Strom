/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (动作特效表)客户端配置结构体
            /// </summary>
            public partial class Configs_ActionAndEffectData 
             { 
                /// <summary>
                /// 资源ID--主键
                /// </summary>
                public int ResourceID{get;set;}

                
                /// <summary>
                /// 英雄3D模型
                /// </summary>
                public string HeroModel { get;set; }
                /// <summary>
                /// 普通攻击动作
                /// </summary>
                public string NormalAttackAciton { get;set; }
                /// <summary>
                /// 入场动作
                /// </summary>
                public string EntranceAction { get;set; }
                /// <summary>
                /// 冲刺动作
                /// </summary>
                public string SprintAction { get;set; }
                /// <summary>
                /// 佣兵动画场景动作
                /// </summary>
                public string MercenarySceneAction { get;set; }
                /// <summary>
                /// 大招技能（蓄力）动作
                /// </summary>
                public string StorageUltimateAttackAction { get;set; }
                /// <summary>
                /// 大招技能（攻击）动作1
                /// </summary>
                public string AggressUltimateAttackAction1 { get;set; }
                /// <summary>
                /// 主动技能1动作
                /// </summary>
                public string ActiveSkillAction1 { get;set; }
                /// <summary>
                /// 主动技能2动作
                /// </summary>
                public string ActiveSkillAction2 { get;set; }
                /// <summary>
                /// 待机动作
                /// </summary>
                public string StandbyAction { get;set; }
                /// <summary>
                /// 受击动作
                /// </summary>
                public string ImpactAction { get;set; }
                /// <summary>
                /// 跑步动作
                /// </summary>
                public string RunningAction { get;set; }
                /// <summary>
                /// 胜利动作
                /// </summary>
                public string VictorAction { get;set; }
                /// <summary>
                /// 击退动作
                /// </summary>
                public string RepalAction { get;set; }
                /// <summary>
                /// 浮空动作
                /// </summary>
                public string FloatingAction { get;set; }
                /// <summary>
                /// 死亡动作
                /// </summary>
                public string DeathAciton { get;set; }
                /// <summary>
                /// 死亡动作2
                /// </summary>
                public string DeathAction2 { get;set; }
                /// <summary>
                /// 展示待机动作
                /// </summary>
                public string ShowAction { get;set; }
                /// <summary>
                /// 展示休闲动作
                /// </summary>
                public string ShowFreeAction { get;set; }
                /// <summary>
                /// 展示休闲动作音效
                /// </summary>
                public string ShowFreeActionvoice { get;set; }
                /// <summary>
                /// 是否有浮空动作（临时字段）
                /// </summary>
                public int WhetherFloatAction { get;set; }
                /// <summary>
                /// 挑空静帧动作
                /// </summary>
                public string FloatFrameAction { get;set; }
                /// <summary>
                /// 挑空死亡动作
                /// </summary>
                public string FloatDeathAction { get;set; }
                /// <summary>
                /// 浮空受击动作
                /// </summary>
                public string FloatHitAction { get;set; }
                /// <summary>
                /// 浮空落地动作
                /// </summary>
                public string FloatLandAction { get;set; }
                /// <summary>
                /// 战斗结算时成功动作
                /// </summary>
                public string WinnerAction { get;set; }
                /// <summary>
                /// 战斗结算成功待机动作
                /// </summary>
                public string WinnerStandbyAction { get;set; }
                /// <summary>
                /// 战斗结算时失败动作
                /// </summary>
                public string LoserAction { get;set; }
                /// <summary>
                /// 战斗中释放BUFF动作
                /// </summary>
                public string BuffReleaseAction { get;set; }
                /// <summary>
                /// 创建角色待机动作
                /// </summary>
                public string CreateRoleStandbyAction { get;set; }
                /// <summary>
                /// 创建角色前跳动作
                /// </summary>
                public string CreateRoleJumpAction { get;set; }
                /// <summary>
                /// 前跳动作音效
                /// </summary>
                public string CreateRoleJumpActionVoice { get;set; }
                /// <summary>
                /// 创建角色跳回动作
                /// </summary>
                public string CreateRoleJumpBackAction { get;set; }
                /// <summary>
                /// 创建角色霸气动作
                /// </summary>
                public string CreateArrogantAction { get;set; }
                /// <summary>
                /// 霸气动作（展示动作）音效
                /// </summary>
                public string CreateArrogantActionVoice { get;set; }
                /// <summary>
                /// 创建角色休闲动作
                /// </summary>
                public string LeisureAction { get;set; }
                /// <summary>
                /// 出手攻击间隔
                /// </summary>
                public float AttackIntervalTime { get;set; }
                /// <summary>
                /// 创建角色时特效
                /// </summary>
                public string CreateRoleEffect { get;set; }
                /// <summary>
                /// 展示动作特效
                /// </summary>
                public string ShowFreeActionEffect { get;set; }
                /// <summary>
                /// 入场动作特效
                /// </summary>
                public string EntranceActionEffect { get;set; }
                /// <summary>
                /// 普通攻击效果
                /// </summary>
                public string NormalAttackEffect_10 { get;set; }
                /// <summary>
                /// 普通攻击轨迹特效
                /// </summary>
                public string NormalAttackEffect_20 { get;set; }
                /// <summary>
                /// 普通受击特效
                /// </summary>
                public string NormalAttackEffect_30 { get;set; }
                /// <summary>
                /// 普通攻击轨迹类型
                /// </summary>
                public int NormalAttackTrail { get;set; }
                /// <summary>
                /// 普通攻击受击效果播放类型
                /// </summary>
                public int NormalAttackBoneType { get;set; }
                /// <summary>
                /// 普通攻击音效配音
                /// </summary>
                public string NormalAttackVoice { get;set; }
                /// <summary>
                /// 佣兵动画场景特效
                /// </summary>
                public string MercenarySceneEffect { get;set; }
                /// <summary>
                /// 大招攻击特效蓄力阶段
                /// </summary>
                public string UltimateAttackEffect_00 { get;set; }
                /// <summary>
                /// 大招攻击特效攻击阶段1
                /// </summary>
                public string UltimateAttackEffect1_10 { get;set; }
                /// <summary>
                /// 大招攻击轨迹特效1
                /// </summary>
                public string UltimateAttackEffect1_20 { get;set; }
                /// <summary>
                /// 大招受击特效1
                /// </summary>
                public string UltimateAttackEffect1_30 { get;set; }
                /// <summary>
                /// 大招技能特效轨迹1
                /// </summary>
                public int UltimateEffectTrajectory1 { get;set; }
                /// <summary>
                /// 大招技能特效受击范围1
                /// </summary>
                public int UltimateEffectRange1 { get;set; }
                /// <summary>
                /// 大招技能攻击轨迹类型1
                /// </summary>
                public int UltimateEffectTrail1 { get;set; }
                /// <summary>
                /// 大招攻击受击效果播放类型
                /// </summary>
                public int UltimateAttackBoneType { get;set; }
                /// <summary>
                /// 大招受击是否为多次打击播1次
                /// </summary>
                public int UltimateWhetherCastOnce { get;set; }
                /// <summary>
                /// 大招攻击类型
                /// </summary>
                public int UltimateAttackType { get;set; }
                /// <summary>
                /// 大招技能攻击音效配音1
                /// </summary>
                public string UltimateEffectVoice1 { get;set; }
                /// <summary>
                /// 主动技能攻击特效1
                /// </summary>
                public string ActiveAttackEffect1_10 { get;set; }
                /// <summary>
                /// 主动技能轨迹特效1
                /// </summary>
                public string ActiveAttackEffect1_20 { get;set; }
                /// <summary>
                /// 主动技能受击特效1
                /// </summary>
                public string ActiveAttackEffect1_30 { get;set; }
                /// <summary>
                /// 主动技能特效轨迹1
                /// </summary>
                public int ActiveEffectTrajectory1 { get;set; }
                /// <summary>
                /// 主动技能特效受击范围1
                /// </summary>
                public int ActiveEffectRange1 { get;set; }
                /// <summary>
                /// 主动技能1轨迹类型
                /// </summary>
                public int ActiveEffectTrail1 { get;set; }
                /// <summary>
                /// 主动技能1受击效果播放类型
                /// </summary>
                public int ActiveAttackBoneType1 { get;set; }
                /// <summary>
                /// 主动技能1音效配音
                /// </summary>
                public string ActiveEffectVoice1 { get;set; }
                /// <summary>
                /// 主动技能攻击特效2
                /// </summary>
                public string ActiveAttackEffect2_10 { get;set; }
                /// <summary>
                /// 主动技能轨迹特效2
                /// </summary>
                public string ActiveAttackEffect2_20 { get;set; }
                /// <summary>
                /// 主动技能受击特效2
                /// </summary>
                public string ActiveAttackEffect2_30 { get;set; }
                /// <summary>
                /// 主动技能受击特效轨迹2
                /// </summary>
                public int ActiveEffectTrajectory2 { get;set; }
                /// <summary>
                /// 主动技能特效受击范围2
                /// </summary>
                public int ActiveEffectRange2 { get;set; }
                /// <summary>
                /// 主动技能2轨迹类型
                /// </summary>
                public int ActiveEffectTrail2 { get;set; }
                /// <summary>
                /// 主动技能2受击效果播放类型
                /// </summary>
                public int ActiveAttackBoneType2 { get;set; }
                /// <summary>
                /// 主动技能2音效配音
                /// </summary>
                public string ActiveEffectVoice2 { get;set; }
                /// <summary>
                /// Buff释放效果特效
                /// </summary>
                public string BuffEffect { get;set; }
                /// <summary>
                /// Buff释放受击效果
                /// </summary>
                public string BuffStrikeEffect { get;set; }
                /// <summary>
                /// Buff受击效果播放类型
                /// </summary>
                public int BuffStrikeBoneType { get;set; }
                /// <summary>
                /// 是否播放跑步特效
                /// </summary>
                public int IsRunningEffect { get;set; }
                /// <summary>
                /// 普通帧数
                /// </summary>
                public int NormalFrame { get;set; }
                /// <summary>
                /// 普通关键帧
                /// </summary>
                public List<int> NormalKeys { get;set; }
                /// <summary>
                /// 大招技能帧数1
                /// </summary>
                public int UltimateSkillFrame1 { get;set; }
                /// <summary>
                /// 大招技能关键帧1
                /// </summary>
                public List<int> UltimateSkillKeys1 { get;set; }
                /// <summary>
                /// 主动技能1帧数
                /// </summary>
                public int ActiveSkillFrame1 { get;set; }
                /// <summary>
                /// 主动技能1关键帧
                /// </summary>
                public List<int> ActiveSkillKey1 { get;set; }
                /// <summary>
                /// 主动技能2帧数
                /// </summary>
                public int ActiveSkillFrame2 { get;set; }
                /// <summary>
                /// 主动技能2关键帧
                /// </summary>
                public List<int> ActiveSkillKey2 { get;set; }
                /// <summary>
                /// 主角释放Buff帧数
                /// </summary>
                public int BuffEffectFrame { get;set; }
                /// <summary>
                /// 主角释放Buff关键帧
                /// </summary>
                public List<int> BuffEffectKey { get;set; }
                /// <summary>
                /// 绑定受击骨骼点
                /// </summary>
                public string StrikeBonePos { get;set; }
                /// <summary>
                /// 模型调整大小
                /// </summary>
                public float ModelAdjust { get;set; }
                /// <summary>
                /// 英雄模型调整大小
                /// </summary>
                public float HeroModelAdjust { get;set; }
                /// <summary>
                /// 英雄血条高度（米为单位）
                /// </summary>
                public float HeroBloodHeight { get;set; }
            } 
            /// <summary>
            /// (动作特效表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ActionAndEffect
            { 

                static Configs_ActionAndEffect _sInstance;
                public static Configs_ActionAndEffect sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ActionAndEffect();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (动作特效表)字典集合
                /// </summary>
                public Dictionary<int, Configs_ActionAndEffectData> mActionAndEffectDatas
                {
                    get { return _ActionAndEffectDatas; }
                }

                /// <summary>
                /// (动作特效表)字典集合
                /// </summary>
                Dictionary<int, Configs_ActionAndEffectData> _ActionAndEffectDatas = new Dictionary<int, Configs_ActionAndEffectData>();

                /// <summary>
                /// 根据ResourceID读取对应的配置信息
                /// </summary>
                /// <param name="ResourceID">配置的ResourceID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ActionAndEffectData GetActionAndEffectDataByResourceID(int ResourceID)
                {
                    if (_ActionAndEffectDatas.ContainsKey(ResourceID))
                    {
                        return _ActionAndEffectDatas[ResourceID];
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
  Configs_ActionAndEffectData cd = new Configs_ActionAndEffectData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ResourceID = key; 
  cd.HeroModel =  Util.GetStringKeyValue(body,"HeroModel"); 
  cd.NormalAttackAciton =  Util.GetStringKeyValue(body,"NormalAttackAciton"); 
  cd.EntranceAction =  Util.GetStringKeyValue(body,"EntranceAction"); 
  cd.SprintAction =  Util.GetStringKeyValue(body,"SprintAction"); 
  cd.MercenarySceneAction =  Util.GetStringKeyValue(body,"MercenarySceneAction"); 
  cd.StorageUltimateAttackAction =  Util.GetStringKeyValue(body,"StorageUltimateAttackAction"); 
  cd.AggressUltimateAttackAction1 =  Util.GetStringKeyValue(body,"AggressUltimateAttackAction1"); 
  cd.ActiveSkillAction1 =  Util.GetStringKeyValue(body,"ActiveSkillAction1"); 
  cd.ActiveSkillAction2 =  Util.GetStringKeyValue(body,"ActiveSkillAction2"); 
  cd.StandbyAction =  Util.GetStringKeyValue(body,"StandbyAction"); 
  cd.ImpactAction =  Util.GetStringKeyValue(body,"ImpactAction"); 
  cd.RunningAction =  Util.GetStringKeyValue(body,"RunningAction"); 
  cd.VictorAction =  Util.GetStringKeyValue(body,"VictorAction"); 
  cd.RepalAction =  Util.GetStringKeyValue(body,"RepalAction"); 
  cd.FloatingAction =  Util.GetStringKeyValue(body,"FloatingAction"); 
  cd.DeathAciton =  Util.GetStringKeyValue(body,"DeathAciton"); 
  cd.DeathAction2 =  Util.GetStringKeyValue(body,"DeathAction2"); 
  cd.ShowAction =  Util.GetStringKeyValue(body,"ShowAction"); 
  cd.ShowFreeAction =  Util.GetStringKeyValue(body,"ShowFreeAction"); 
  cd.ShowFreeActionvoice =  Util.GetStringKeyValue(body,"ShowFreeActionvoice"); 
  cd.WhetherFloatAction =  Util.GetIntKeyValue(body,"WhetherFloatAction"); 
  cd.FloatFrameAction =  Util.GetStringKeyValue(body,"FloatFrameAction"); 
  cd.FloatDeathAction =  Util.GetStringKeyValue(body,"FloatDeathAction"); 
  cd.FloatHitAction =  Util.GetStringKeyValue(body,"FloatHitAction"); 
  cd.FloatLandAction =  Util.GetStringKeyValue(body,"FloatLandAction"); 
  cd.WinnerAction =  Util.GetStringKeyValue(body,"WinnerAction"); 
  cd.WinnerStandbyAction =  Util.GetStringKeyValue(body,"WinnerStandbyAction"); 
  cd.LoserAction =  Util.GetStringKeyValue(body,"LoserAction"); 
  cd.BuffReleaseAction =  Util.GetStringKeyValue(body,"BuffReleaseAction"); 
  cd.CreateRoleStandbyAction =  Util.GetStringKeyValue(body,"CreateRoleStandbyAction"); 
  cd.CreateRoleJumpAction =  Util.GetStringKeyValue(body,"CreateRoleJumpAction"); 
  cd.CreateRoleJumpActionVoice =  Util.GetStringKeyValue(body,"CreateRoleJumpActionVoice"); 
  cd.CreateRoleJumpBackAction =  Util.GetStringKeyValue(body,"CreateRoleJumpBackAction"); 
  cd.CreateArrogantAction =  Util.GetStringKeyValue(body,"CreateArrogantAction"); 
  cd.CreateArrogantActionVoice =  Util.GetStringKeyValue(body,"CreateArrogantActionVoice"); 
  cd.LeisureAction =  Util.GetStringKeyValue(body,"LeisureAction"); 
  cd.AttackIntervalTime =  Util.GetFloatKeyValue(body,"AttackIntervalTime"); 
  cd.CreateRoleEffect =  Util.GetStringKeyValue(body,"CreateRoleEffect"); 
  cd.ShowFreeActionEffect =  Util.GetStringKeyValue(body,"ShowFreeActionEffect"); 
  cd.EntranceActionEffect =  Util.GetStringKeyValue(body,"EntranceActionEffect"); 
  cd.NormalAttackEffect_10 =  Util.GetStringKeyValue(body,"NormalAttackEffect_10"); 
  cd.NormalAttackEffect_20 =  Util.GetStringKeyValue(body,"NormalAttackEffect_20"); 
  cd.NormalAttackEffect_30 =  Util.GetStringKeyValue(body,"NormalAttackEffect_30"); 
  cd.NormalAttackTrail =  Util.GetIntKeyValue(body,"NormalAttackTrail"); 
  cd.NormalAttackBoneType =  Util.GetIntKeyValue(body,"NormalAttackBoneType"); 
  cd.NormalAttackVoice =  Util.GetStringKeyValue(body,"NormalAttackVoice"); 
  cd.MercenarySceneEffect =  Util.GetStringKeyValue(body,"MercenarySceneEffect"); 
  cd.UltimateAttackEffect_00 =  Util.GetStringKeyValue(body,"UltimateAttackEffect_00"); 
  cd.UltimateAttackEffect1_10 =  Util.GetStringKeyValue(body,"UltimateAttackEffect1_10"); 
  cd.UltimateAttackEffect1_20 =  Util.GetStringKeyValue(body,"UltimateAttackEffect1_20"); 
  cd.UltimateAttackEffect1_30 =  Util.GetStringKeyValue(body,"UltimateAttackEffect1_30"); 
  cd.UltimateEffectTrajectory1 =  Util.GetIntKeyValue(body,"UltimateEffectTrajectory1"); 
  cd.UltimateEffectRange1 =  Util.GetIntKeyValue(body,"UltimateEffectRange1"); 
  cd.UltimateEffectTrail1 =  Util.GetIntKeyValue(body,"UltimateEffectTrail1"); 
  cd.UltimateAttackBoneType =  Util.GetIntKeyValue(body,"UltimateAttackBoneType"); 
  cd.UltimateWhetherCastOnce =  Util.GetIntKeyValue(body,"UltimateWhetherCastOnce"); 
  cd.UltimateAttackType =  Util.GetIntKeyValue(body,"UltimateAttackType"); 
  cd.UltimateEffectVoice1 =  Util.GetStringKeyValue(body,"UltimateEffectVoice1"); 
  cd.ActiveAttackEffect1_10 =  Util.GetStringKeyValue(body,"ActiveAttackEffect1_10"); 
  cd.ActiveAttackEffect1_20 =  Util.GetStringKeyValue(body,"ActiveAttackEffect1_20"); 
  cd.ActiveAttackEffect1_30 =  Util.GetStringKeyValue(body,"ActiveAttackEffect1_30"); 
  cd.ActiveEffectTrajectory1 =  Util.GetIntKeyValue(body,"ActiveEffectTrajectory1"); 
  cd.ActiveEffectRange1 =  Util.GetIntKeyValue(body,"ActiveEffectRange1"); 
  cd.ActiveEffectTrail1 =  Util.GetIntKeyValue(body,"ActiveEffectTrail1"); 
  cd.ActiveAttackBoneType1 =  Util.GetIntKeyValue(body,"ActiveAttackBoneType1"); 
  cd.ActiveEffectVoice1 =  Util.GetStringKeyValue(body,"ActiveEffectVoice1"); 
  cd.ActiveAttackEffect2_10 =  Util.GetStringKeyValue(body,"ActiveAttackEffect2_10"); 
  cd.ActiveAttackEffect2_20 =  Util.GetStringKeyValue(body,"ActiveAttackEffect2_20"); 
  cd.ActiveAttackEffect2_30 =  Util.GetStringKeyValue(body,"ActiveAttackEffect2_30"); 
  cd.ActiveEffectTrajectory2 =  Util.GetIntKeyValue(body,"ActiveEffectTrajectory2"); 
  cd.ActiveEffectRange2 =  Util.GetIntKeyValue(body,"ActiveEffectRange2"); 
  cd.ActiveEffectTrail2 =  Util.GetIntKeyValue(body,"ActiveEffectTrail2"); 
  cd.ActiveAttackBoneType2 =  Util.GetIntKeyValue(body,"ActiveAttackBoneType2"); 
  cd.ActiveEffectVoice2 =  Util.GetStringKeyValue(body,"ActiveEffectVoice2"); 
  cd.BuffEffect =  Util.GetStringKeyValue(body,"BuffEffect"); 
  cd.BuffStrikeEffect =  Util.GetStringKeyValue(body,"BuffStrikeEffect"); 
  cd.BuffStrikeBoneType =  Util.GetIntKeyValue(body,"BuffStrikeBoneType"); 
  cd.IsRunningEffect =  Util.GetIntKeyValue(body,"IsRunningEffect"); 
  cd.NormalFrame =  Util.GetIntKeyValue(body,"NormalFrame"); 
 
 string[] NormalKeysStrs= Util.GetStringKeyValue(body, "NormalKeys").TrimStart('{').TrimEnd('}',',').Split(',');
cd.NormalKeys = new List<int>();
foreach(string NormalKeysStr in NormalKeysStrs)  cd.NormalKeys.Add(Util.ParseToInt(NormalKeysStr)); 
 
 cd.UltimateSkillFrame1 =  Util.GetIntKeyValue(body,"UltimateSkillFrame1"); 
 
 string[] UltimateSkillKeys1Strs= Util.GetStringKeyValue(body, "UltimateSkillKeys1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.UltimateSkillKeys1 = new List<int>();
foreach(string UltimateSkillKeys1Str in UltimateSkillKeys1Strs)  cd.UltimateSkillKeys1.Add(Util.ParseToInt(UltimateSkillKeys1Str)); 
 
 cd.ActiveSkillFrame1 =  Util.GetIntKeyValue(body,"ActiveSkillFrame1"); 
 
 string[] ActiveSkillKey1Strs= Util.GetStringKeyValue(body, "ActiveSkillKey1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.ActiveSkillKey1 = new List<int>();
foreach(string ActiveSkillKey1Str in ActiveSkillKey1Strs)  cd.ActiveSkillKey1.Add(Util.ParseToInt(ActiveSkillKey1Str)); 
 
 cd.ActiveSkillFrame2 =  Util.GetIntKeyValue(body,"ActiveSkillFrame2"); 
 
 string[] ActiveSkillKey2Strs= Util.GetStringKeyValue(body, "ActiveSkillKey2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.ActiveSkillKey2 = new List<int>();
foreach(string ActiveSkillKey2Str in ActiveSkillKey2Strs)  cd.ActiveSkillKey2.Add(Util.ParseToInt(ActiveSkillKey2Str)); 
 
 cd.BuffEffectFrame =  Util.GetIntKeyValue(body,"BuffEffectFrame"); 
 
 string[] BuffEffectKeyStrs= Util.GetStringKeyValue(body, "BuffEffectKey").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BuffEffectKey = new List<int>();
foreach(string BuffEffectKeyStr in BuffEffectKeyStrs)  cd.BuffEffectKey.Add(Util.ParseToInt(BuffEffectKeyStr)); 
 
 cd.StrikeBonePos =  Util.GetStringKeyValue(body,"StrikeBonePos"); 
  cd.ModelAdjust =  Util.GetFloatKeyValue(body,"ModelAdjust"); 
  cd.HeroModelAdjust =  Util.GetFloatKeyValue(body,"HeroModelAdjust"); 
  cd.HeroBloodHeight =  Util.GetFloatKeyValue(body,"HeroBloodHeight"); 
  
 if (mActionAndEffectDatas.ContainsKey(key) == false)
 mActionAndEffectDatas.Add(key, cd);
  }
 //Debug.Log(mActionAndEffectDatas.Count);
}

            }