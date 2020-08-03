/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (NPC阵容表)客户端配置结构体
            /// </summary>
            public partial class Configs_NPCArrayData 
             { 
                /// <summary>
                /// 阵容ID--主键
                /// </summary>
                public int ArrayID{get;set;}

                
                /// <summary>
                /// 1号位
                /// </summary>
                public int Number1 { get;set; }
                /// <summary>
                /// 1号大招标识
                /// </summary>
                public int SkillNumber1 { get;set; }
                /// <summary>
                /// 2号位
                /// </summary>
                public int Number2 { get;set; }
                /// <summary>
                /// 2号大招标识
                /// </summary>
                public int SkillNumber2 { get;set; }
                /// <summary>
                /// 3号位
                /// </summary>
                public int Number3 { get;set; }
                /// <summary>
                /// 3号大招标识
                /// </summary>
                public int SkillNumber3 { get;set; }
                /// <summary>
                /// 4号位
                /// </summary>
                public int Number4 { get;set; }
                /// <summary>
                /// 4号大招标识
                /// </summary>
                public int SkillNumber4 { get;set; }
                /// <summary>
                /// 5号位
                /// </summary>
                public int Number5 { get;set; }
                /// <summary>
                /// 5号大招标识
                /// </summary>
                public int SkillNumber5 { get;set; }
                /// <summary>
                /// 6号位
                /// </summary>
                public int Number6 { get;set; }
                /// <summary>
                /// 6号大招标识
                /// </summary>
                public int SkillNumber6 { get;set; }
                /// <summary>
                /// 7号位
                /// </summary>
                public int Number7 { get;set; }
                /// <summary>
                /// 7号大招标识
                /// </summary>
                public int SkillNumber7 { get;set; }
                /// <summary>
                /// 8号位
                /// </summary>
                public int Number8 { get;set; }
                /// <summary>
                /// 8号大招标识
                /// </summary>
                public int SkillNumber8 { get;set; }
                /// <summary>
                /// 9号位
                /// </summary>
                public int Number9 { get;set; }
                /// <summary>
                /// 9号大招标识
                /// </summary>
                public int SkillNumber9 { get;set; }
            } 
            /// <summary>
            /// (NPC阵容表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_NPCArray
            { 

                static Configs_NPCArray _sInstance;
                public static Configs_NPCArray sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_NPCArray();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (NPC阵容表)字典集合
                /// </summary>
                public Dictionary<int, Configs_NPCArrayData> mNPCArrayDatas
                {
                    get { return _NPCArrayDatas; }
                }

                /// <summary>
                /// (NPC阵容表)字典集合
                /// </summary>
                Dictionary<int, Configs_NPCArrayData> _NPCArrayDatas = new Dictionary<int, Configs_NPCArrayData>();

                /// <summary>
                /// 根据ArrayID读取对应的配置信息
                /// </summary>
                /// <param name="ArrayID">配置的ArrayID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_NPCArrayData GetNPCArrayDataByArrayID(int ArrayID)
                {
                    if (_NPCArrayDatas.ContainsKey(ArrayID))
                    {
                        return _NPCArrayDatas[ArrayID];
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
  Configs_NPCArrayData cd = new Configs_NPCArrayData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ArrayID = key; 
  cd.Number1 =  Util.GetIntKeyValue(body,"Number1"); 
  cd.SkillNumber1 =  Util.GetIntKeyValue(body,"SkillNumber1"); 
  cd.Number2 =  Util.GetIntKeyValue(body,"Number2"); 
  cd.SkillNumber2 =  Util.GetIntKeyValue(body,"SkillNumber2"); 
  cd.Number3 =  Util.GetIntKeyValue(body,"Number3"); 
  cd.SkillNumber3 =  Util.GetIntKeyValue(body,"SkillNumber3"); 
  cd.Number4 =  Util.GetIntKeyValue(body,"Number4"); 
  cd.SkillNumber4 =  Util.GetIntKeyValue(body,"SkillNumber4"); 
  cd.Number5 =  Util.GetIntKeyValue(body,"Number5"); 
  cd.SkillNumber5 =  Util.GetIntKeyValue(body,"SkillNumber5"); 
  cd.Number6 =  Util.GetIntKeyValue(body,"Number6"); 
  cd.SkillNumber6 =  Util.GetIntKeyValue(body,"SkillNumber6"); 
  cd.Number7 =  Util.GetIntKeyValue(body,"Number7"); 
  cd.SkillNumber7 =  Util.GetIntKeyValue(body,"SkillNumber7"); 
  cd.Number8 =  Util.GetIntKeyValue(body,"Number8"); 
  cd.SkillNumber8 =  Util.GetIntKeyValue(body,"SkillNumber8"); 
  cd.Number9 =  Util.GetIntKeyValue(body,"Number9"); 
  cd.SkillNumber9 =  Util.GetIntKeyValue(body,"SkillNumber9"); 
  
 if (mNPCArrayDatas.ContainsKey(key) == false)
 mNPCArrayDatas.Add(key, cd);
  }
 //Debug.Log(mNPCArrayDatas.Count);
}

            }