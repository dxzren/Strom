/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (公会副本章节表)客户端配置结构体
            /// </summary>
            public partial class Configs_SocietyChapterData 
             { 
                /// <summary>
                /// 章节ID--主键
                /// </summary>
                public int ChapterID{get;set;}

                
                /// <summary>
                /// 章节名
                /// </summary>
                public string ChapterName { get;set; }
                /// <summary>
                /// 解锁等级
                /// </summary>
                public int UnlockLevel { get;set; }
                /// <summary>
                /// 章节背景
                /// </summary>
                public string ChapterScene1 { get;set; }
                /// <summary>
                /// 战斗场景
                /// </summary>
                public string BattleScene { get;set; }
                /// <summary>
                /// 最小关卡ID
                /// </summary>
                public int LittleID { get;set; }
                /// <summary>
                /// 最大关卡ID
                /// </summary>
                public int BigID { get;set; }
                /// <summary>
                /// 7日通关额外奖励公会币
                /// </summary>
                public int ExtraGiftID { get;set; }
                /// <summary>
                /// 开启消耗活跃度
                /// </summary>
                public int LivenessConsume { get;set; }
            } 
            /// <summary>
            /// (公会副本章节表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SocietyChapter
            { 

                static Configs_SocietyChapter _sInstance;
                public static Configs_SocietyChapter sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SocietyChapter();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (公会副本章节表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SocietyChapterData> mSocietyChapterDatas
                {
                    get { return _SocietyChapterDatas; }
                }

                /// <summary>
                /// (公会副本章节表)字典集合
                /// </summary>
                Dictionary<int, Configs_SocietyChapterData> _SocietyChapterDatas = new Dictionary<int, Configs_SocietyChapterData>();

                /// <summary>
                /// 根据ChapterID读取对应的配置信息
                /// </summary>
                /// <param name="ChapterID">配置的ChapterID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SocietyChapterData GetSocietyChapterDataByChapterID(int ChapterID)
                {
                    if (_SocietyChapterDatas.ContainsKey(ChapterID))
                    {
                        return _SocietyChapterDatas[ChapterID];
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
  Configs_SocietyChapterData cd = new Configs_SocietyChapterData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ChapterID = key; 
  cd.ChapterName =  Util.GetStringKeyValue(body,"ChapterName"); 
  cd.UnlockLevel =  Util.GetIntKeyValue(body,"UnlockLevel"); 
  cd.ChapterScene1 =  Util.GetStringKeyValue(body,"ChapterScene1"); 
  cd.BattleScene =  Util.GetStringKeyValue(body,"BattleScene"); 
  cd.LittleID =  Util.GetIntKeyValue(body,"LittleID"); 
  cd.BigID =  Util.GetIntKeyValue(body,"BigID"); 
  cd.ExtraGiftID =  Util.GetIntKeyValue(body,"ExtraGiftID"); 
  cd.LivenessConsume =  Util.GetIntKeyValue(body,"LivenessConsume"); 
  
 if (mSocietyChapterDatas.ContainsKey(key) == false)
 mSocietyChapterDatas.Add(key, cd);
  }
 //Debug.Log(mSocietyChapterDatas.Count);
}

            }