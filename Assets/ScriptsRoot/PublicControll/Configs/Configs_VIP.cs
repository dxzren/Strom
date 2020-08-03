/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (VIP表)客户端配置结构体
            /// </summary>
            public partial class Configs_VIPData 
             { 
                /// <summary>
                /// vip等级--主键
                /// </summary>
                public int VIP{get;set;}

                
                /// <summary>
                /// 需要经验
                /// </summary>
                public int Experience { get;set; }
                /// <summary>
                /// 购买体力
                /// </summary>
                public int PhysicalPower { get;set; }
                /// <summary>
                /// 点金次数
                /// </summary>
                public int Money { get;set; }
                /// <summary>
                /// 重置精英关卡
                /// </summary>
                public int CheckpointReset { get;set; }
                /// <summary>
                /// 竞技场门票
                /// </summary>
                public int JJCReset { get;set; }
                /// <summary>
                /// 技能点购买次数
                /// </summary>
                public int Skill { get;set; }
                /// <summary>
                /// 远征加成
                /// </summary>
                public int ExpeditionAdjust { get;set; }
                /// <summary>
                /// 技能点上限
                /// </summary>
                public int SkillLimit { get;set; }
                /// <summary>
                /// 秘境塔次数
                /// </summary>
                public int MysteriousTower { get;set; }
                /// <summary>
                /// 公会膜拜次数
                /// </summary>
                public int SocietyWorship { get;set; }
                /// <summary>
                /// 解锁功能
                /// </summary>
                public List<string> UnlockFunction { get;set; }
            } 
            /// <summary>
            /// (VIP表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_VIP
            { 

                static Configs_VIP _sInstance;
                public static Configs_VIP sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_VIP();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (VIP表)字典集合
                /// </summary>
                public Dictionary<int, Configs_VIPData> mVIPDatas
                {
                    get { return _VIPDatas; }
                }

                /// <summary>
                /// (VIP表)字典集合
                /// </summary>
                Dictionary<int, Configs_VIPData> _VIPDatas = new Dictionary<int, Configs_VIPData>();

                /// <summary>
                /// 根据VIP读取对应的配置信息
                /// </summary>
                /// <param name="VIP">配置的VIP</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_VIPData GetVIPDataByVIP(int VIP)
                {
                    if (_VIPDatas.ContainsKey(VIP))
                    {
                        return _VIPDatas[VIP];
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
  Configs_VIPData cd = new Configs_VIPData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.VIP = key; 
  cd.Experience =  Util.GetIntKeyValue(body,"Experience"); 
  cd.PhysicalPower =  Util.GetIntKeyValue(body,"PhysicalPower"); 
  cd.Money =  Util.GetIntKeyValue(body,"Money"); 
  cd.CheckpointReset =  Util.GetIntKeyValue(body,"CheckpointReset"); 
  cd.JJCReset =  Util.GetIntKeyValue(body,"JJCReset"); 
  cd.Skill =  Util.GetIntKeyValue(body,"Skill"); 
  cd.ExpeditionAdjust =  Util.GetIntKeyValue(body,"ExpeditionAdjust"); 
  cd.SkillLimit =  Util.GetIntKeyValue(body,"SkillLimit"); 
  cd.MysteriousTower =  Util.GetIntKeyValue(body,"MysteriousTower"); 
  cd.SocietyWorship =  Util.GetIntKeyValue(body,"SocietyWorship"); 
 
 string[] UnlockFunctionStrs= Util.GetStringKeyValue(body, "UnlockFunction").TrimStart('{').TrimEnd('}',',').Split(',');
cd.UnlockFunction = new List<string>();
foreach(string UnlockFunctionStr in UnlockFunctionStrs)  cd.UnlockFunction.Add(UnlockFunctionStr); 
 
 
 if (mVIPDatas.ContainsKey(key) == false)
 mVIPDatas.Add(key, cd);
  }
 //Debug.Log(mVIPDatas.Count);
}

            }