/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (对话表)客户端配置结构体
            /// </summary>
            public partial class Configs_DialogueData 
             { 
                /// <summary>
                /// 对话ID--主键
                /// </summary>
                public int DialogueID{get;set;}

                
                /// <summary>
                /// 对话类型
                /// </summary>
                public int DialogueType { get;set; }
                /// <summary>
                /// 对话序号
                /// </summary>
                public int Number { get;set; }
                /// <summary>
                /// 对话内容
                /// </summary>
                public string Dialogue { get;set; }
                /// <summary>
                /// 讲话人
                /// </summary>
                public int Speaker { get;set; }
            } 
            /// <summary>
            /// (对话表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Dialogue
            { 

                static Configs_Dialogue _sInstance;
                public static Configs_Dialogue sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Dialogue();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (对话表)字典集合
                /// </summary>
                public Dictionary<int, Configs_DialogueData> mDialogueDatas
                {
                    get { return _DialogueDatas; }
                }

                /// <summary>
                /// (对话表)字典集合
                /// </summary>
                Dictionary<int, Configs_DialogueData> _DialogueDatas = new Dictionary<int, Configs_DialogueData>();

                /// <summary>
                /// 根据DialogueID读取对应的配置信息
                /// </summary>
                /// <param name="DialogueID">配置的DialogueID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_DialogueData GetDialogueDataByDialogueID(int DialogueID)
                {
                    if (_DialogueDatas.ContainsKey(DialogueID))
                    {
                        return _DialogueDatas[DialogueID];
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
  Configs_DialogueData cd = new Configs_DialogueData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.DialogueID = key; 
  cd.DialogueType =  Util.GetIntKeyValue(body,"DialogueType"); 
  cd.Number =  Util.GetIntKeyValue(body,"Number"); 
  cd.Dialogue =  Util.GetStringKeyValue(body,"Dialogue"); 
  cd.Speaker =  Util.GetIntKeyValue(body,"Speaker"); 
  
 if (mDialogueDatas.ContainsKey(key) == false)
 mDialogueDatas.Add(key, cd);
  }
 //Debug.Log(mDialogueDatas.Count);
}

            }