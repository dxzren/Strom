/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (竞技场预设NPC)客户端配置结构体
            /// </summary>
            public partial class Configs_JJCNPCData 
             { 
                /// <summary>
                /// NPCID--主键
                /// </summary>
                public int NPCID{get;set;}

                
                /// <summary>
                /// 初始积分
                /// </summary>
                public int InitialScore { get;set; }
                /// <summary>
                /// NPC名称
                /// </summary>
                public string NPCName { get;set; }
                /// <summary>
                /// 头像指向
                /// </summary>
                public int NPCIcon { get;set; }
                /// <summary>
                /// NPC战队等级
                /// </summary>
                public int NPCLevel { get;set; }
                /// <summary>
                /// 阵容ID
                /// </summary>
                public int BattleArrayID { get;set; }
                /// <summary>
                /// 主动技能1等级
                /// </summary>
                public int Talent1Level { get;set; }
                /// <summary>
                /// 英雄等级
                /// </summary>
                public int HeroLevel { get;set; }
                /// <summary>
                /// 英雄星级
                /// </summary>
                public int HeroStar { get;set; }
                /// <summary>
                /// 英雄品质
                /// </summary>
                public int HeroQulity { get;set; }
            } 
            /// <summary>
            /// (竞技场预设NPC)客户端配置数据集合类
            /// </summary>
            public partial class Configs_JJCNPC
            { 

                static Configs_JJCNPC _sInstance;
                public static Configs_JJCNPC sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_JJCNPC();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (竞技场预设NPC)字典集合
                /// </summary>
                public Dictionary<int, Configs_JJCNPCData> mJJCNPCDatas
                {
                    get { return _JJCNPCDatas; }
                }

                /// <summary>
                /// (竞技场预设NPC)字典集合
                /// </summary>
                Dictionary<int, Configs_JJCNPCData> _JJCNPCDatas = new Dictionary<int, Configs_JJCNPCData>();

                /// <summary>
                /// 根据NPCID读取对应的配置信息
                /// </summary>
                /// <param name="NPCID">配置的NPCID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_JJCNPCData GetJJCNPCDataByNPCID(int NPCID)
                {
                    if (_JJCNPCDatas.ContainsKey(NPCID))
                    {
                        return _JJCNPCDatas[NPCID];
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
  Configs_JJCNPCData cd = new Configs_JJCNPCData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.NPCID = key; 
  cd.InitialScore =  Util.GetIntKeyValue(body,"InitialScore"); 
  cd.NPCName =  Util.GetStringKeyValue(body,"NPCName"); 
  cd.NPCIcon =  Util.GetIntKeyValue(body,"NPCIcon"); 
  cd.NPCLevel =  Util.GetIntKeyValue(body,"NPCLevel"); 
  cd.BattleArrayID =  Util.GetIntKeyValue(body,"BattleArrayID"); 
  cd.Talent1Level =  Util.GetIntKeyValue(body,"Talent1Level"); 
  cd.HeroLevel =  Util.GetIntKeyValue(body,"HeroLevel"); 
  cd.HeroStar =  Util.GetIntKeyValue(body,"HeroStar"); 
  cd.HeroQulity =  Util.GetIntKeyValue(body,"HeroQulity"); 
  
 if (mJJCNPCDatas.ContainsKey(key) == false)
 mJJCNPCDatas.Add(key, cd);
  }
 //Debug.Log(mJJCNPCDatas.Count);
}

            }