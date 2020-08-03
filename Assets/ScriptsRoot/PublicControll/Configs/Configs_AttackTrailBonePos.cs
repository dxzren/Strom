/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (攻击轨迹骨骼发出点)客户端配置结构体
            /// </summary>
            public partial class Configs_AttackTrailBonePosData 
             { 
                /// <summary>
                /// 英雄资源ID--主键
                /// </summary>
                public int HeroResourceID{get;set;}

                
                /// <summary>
                /// 普通攻击轨迹发出点
                /// </summary>
                public string NormalTrailBones2 { get;set; }
                /// <summary>
                /// 普通攻击轨迹发出偏移位置
                /// </summary>
                public List<float> NormalTrailBonesPos2 { get;set; }
                /// <summary>
                /// 大招轨迹发出点
                /// </summary>
                public string UltimateTrailBones { get;set; }
                /// <summary>
                /// 大招轨迹发出偏移位置
                /// </summary>
                public List<float> UltimateTrailBonesPos { get;set; }
                /// <summary>
                /// 主动技能1轨迹发出点
                /// </summary>
                public string ActiveTrailBones1 { get;set; }
                /// <summary>
                /// 主动技能1轨迹发出点偏移位置
                /// </summary>
                public List<float> ActiveTrailBonesPos1 { get;set; }
                /// <summary>
                /// 主动技能2轨迹发出点
                /// </summary>
                public string ActiveTrailBones2 { get;set; }
                /// <summary>
                /// 主动技能2轨迹发出点偏移位置
                /// </summary>
                public List<float> ActiveTrailBonesPos2 { get;set; }
            } 
            /// <summary>
            /// (攻击轨迹骨骼发出点)客户端配置数据集合类
            /// </summary>
            public partial class Configs_AttackTrailBonePos
            { 

                static Configs_AttackTrailBonePos _sInstance;
                public static Configs_AttackTrailBonePos sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_AttackTrailBonePos();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (攻击轨迹骨骼发出点)字典集合
                /// </summary>
                public Dictionary<int, Configs_AttackTrailBonePosData> mAttackTrailBonePosDatas
                {
                    get { return _AttackTrailBonePosDatas; }
                }

                /// <summary>
                /// (攻击轨迹骨骼发出点)字典集合
                /// </summary>
                Dictionary<int, Configs_AttackTrailBonePosData> _AttackTrailBonePosDatas = new Dictionary<int, Configs_AttackTrailBonePosData>();

                /// <summary>
                /// 根据HeroResourceID读取对应的配置信息
                /// </summary>
                /// <param name="HeroResourceID">配置的HeroResourceID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_AttackTrailBonePosData GetAttackTrailBonePosDataByHeroResourceID(int HeroResourceID)
                {
                    if (_AttackTrailBonePosDatas.ContainsKey(HeroResourceID))
                    {
                        return _AttackTrailBonePosDatas[HeroResourceID];
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
  Configs_AttackTrailBonePosData cd = new Configs_AttackTrailBonePosData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.HeroResourceID = key; 
  cd.NormalTrailBones2 =  Util.GetStringKeyValue(body,"NormalTrailBones2"); 
 
 string[] NormalTrailBonesPos2Strs= Util.GetStringKeyValue(body, "NormalTrailBonesPos2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.NormalTrailBonesPos2  = new List<float>();
foreach(string NormalTrailBonesPos2Str in NormalTrailBonesPos2Strs)  cd.NormalTrailBonesPos2.Add(Util.ParseToFloat(NormalTrailBonesPos2Str)); 
 
 cd.UltimateTrailBones =  Util.GetStringKeyValue(body,"UltimateTrailBones"); 
 
 string[] UltimateTrailBonesPosStrs= Util.GetStringKeyValue(body, "UltimateTrailBonesPos").TrimStart('{').TrimEnd('}',',').Split(',');
cd.UltimateTrailBonesPos  = new List<float>();
foreach(string UltimateTrailBonesPosStr in UltimateTrailBonesPosStrs)  cd.UltimateTrailBonesPos.Add(Util.ParseToFloat(UltimateTrailBonesPosStr)); 
 
 cd.ActiveTrailBones1 =  Util.GetStringKeyValue(body,"ActiveTrailBones1"); 
 
 string[] ActiveTrailBonesPos1Strs= Util.GetStringKeyValue(body, "ActiveTrailBonesPos1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.ActiveTrailBonesPos1  = new List<float>();
foreach(string ActiveTrailBonesPos1Str in ActiveTrailBonesPos1Strs)  cd.ActiveTrailBonesPos1.Add(Util.ParseToFloat(ActiveTrailBonesPos1Str)); 
 
 cd.ActiveTrailBones2 =  Util.GetStringKeyValue(body,"ActiveTrailBones2"); 
 
 string[] ActiveTrailBonesPos2Strs= Util.GetStringKeyValue(body, "ActiveTrailBonesPos2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.ActiveTrailBonesPos2  = new List<float>();
foreach(string ActiveTrailBonesPos2Str in ActiveTrailBonesPos2Strs)  cd.ActiveTrailBonesPos2.Add(Util.ParseToFloat(ActiveTrailBonesPos2Str)); 
 
 
 if (mAttackTrailBonePosDatas.ContainsKey(key) == false)
 mAttackTrailBonePosDatas.Add(key, cd);
  }
 //Debug.Log(mAttackTrailBonePosDatas.Count);
}

            }