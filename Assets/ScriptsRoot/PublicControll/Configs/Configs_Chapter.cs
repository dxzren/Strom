/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (章节表)客户端配置结构体
            /// </summary>
            public partial class Configs_ChapterData 
             { 
                /// <summary>
                /// 章节ID--主键
                /// </summary>
                public int ChapterID{get;set;}

                
                /// <summary>
                /// 解锁等级
                /// </summary>
                public List<int> UnlockLevel { get;set; }
                /// <summary>
                /// 最大关卡ID
                /// </summary>
                public List<int> BigID { get;set; }
                /// <summary>
                /// 最小关卡ID
                /// </summary>
                public List<int> LittleID { get;set; }
                /// <summary>
                /// 章节名
                /// </summary>
                public string ChapterName { get;set; }
                /// <summary>
                /// 世界地图
                /// </summary>
                public string WorldMap { get;set; }
                /// <summary>
                /// 章节背景
                /// </summary>
                public string ChapterScene1 { get;set; }
                /// <summary>
                /// 战斗场景
                /// </summary>
                public string BattleScene { get;set; }
                /// <summary>
                /// 章节图标
                /// </summary>
                public string ChapterIcon { get;set; }
            } 
            /// <summary>
            /// (章节表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Chapter
            { 

                static Configs_Chapter _sInstance;
                public static Configs_Chapter sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Chapter();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (章节表)字典集合
                /// </summary>
                public Dictionary<int, Configs_ChapterData> mChapterDatas
                {
                    get { return _ChapterDatas; }
                }

                /// <summary>
                /// (章节表)字典集合
                /// </summary>
                Dictionary<int, Configs_ChapterData> _ChapterDatas = new Dictionary<int, Configs_ChapterData>();

                /// <summary>
                /// 根据ChapterID读取对应的配置信息
                /// </summary>
                /// <param name="ChapterID">配置的ChapterID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ChapterData GetChapterDataByChapterID(int ChapterID)
                {
                    if (_ChapterDatas.ContainsKey(ChapterID))
                    {
                        return _ChapterDatas[ChapterID];
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
  Configs_ChapterData cd = new Configs_ChapterData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ChapterID = key; 
 
 string[] UnlockLevelStrs= Util.GetStringKeyValue(body, "UnlockLevel").TrimStart('{').TrimEnd('}',',').Split(',');
cd.UnlockLevel = new List<int>();
foreach(string UnlockLevelStr in UnlockLevelStrs)  cd.UnlockLevel.Add(Util.ParseToInt(UnlockLevelStr)); 
 

 string[] BigIDStrs= Util.GetStringKeyValue(body, "BigID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BigID = new List<int>();
foreach(string BigIDStr in BigIDStrs)  cd.BigID.Add(Util.ParseToInt(BigIDStr)); 
 

 string[] LittleIDStrs= Util.GetStringKeyValue(body, "LittleID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.LittleID = new List<int>();
foreach(string LittleIDStr in LittleIDStrs)  cd.LittleID.Add(Util.ParseToInt(LittleIDStr)); 
 
 cd.ChapterName =  Util.GetStringKeyValue(body,"ChapterName"); 
  cd.WorldMap =  Util.GetStringKeyValue(body,"WorldMap"); 
  cd.ChapterScene1 =  Util.GetStringKeyValue(body,"ChapterScene1"); 
  cd.BattleScene =  Util.GetStringKeyValue(body,"BattleScene"); 
  cd.ChapterIcon =  Util.GetStringKeyValue(body,"ChapterIcon"); 
  
 if (mChapterDatas.ContainsKey(key) == false)
 mChapterDatas.Add(key, cd);
  }
 //Debug.Log(mChapterDatas.Count);
}

            }