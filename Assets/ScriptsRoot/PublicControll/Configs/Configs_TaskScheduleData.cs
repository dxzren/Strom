/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (任务进度表)客户端配置结构体
            /// </summary>
            public partial class Configs_TaskScheduleDataData 
             { 
                /// <summary>
                /// 进度ID--主键
                /// </summary>
                public int ScheduleID{get;set;}

                
                /// <summary>
                /// 下个进度ID
                /// </summary>
                public int NextSchedule { get;set; }
                /// <summary>
                /// 任务ID
                /// </summary>
                public int TaskID { get;set; }
                /// <summary>
                /// 任务进度
                /// </summary>
                public int TaskSchedule { get;set; }
                /// <summary>
                /// 条件1
                /// </summary>
                public int Condition1 { get;set; }
                /// <summary>
                /// 条件2
                /// </summary>
                public int Condition2 { get;set; }
                /// <summary>
                /// 任务目标
                /// </summary>
                public int Target { get;set; }
                /// <summary>
                /// 任务分类
                /// </summary>
                public int TaskClassify { get;set; }
                /// <summary>
                /// 开放等级
                /// </summary>
                public int LimitData { get;set; }
                /// <summary>
                /// 职业任务标识
                /// </summary>
                public int OccupationTask { get;set; }
                /// <summary>
                /// 奖励礼包ID
                /// </summary>
                public int GiftID { get;set; }
            } 
            /// <summary>
            /// (任务进度表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_TaskScheduleData
            { 

                static Configs_TaskScheduleData _sInstance;
                public static Configs_TaskScheduleData sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_TaskScheduleData();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (任务进度表)字典集合
                /// </summary>
                public Dictionary<int, Configs_TaskScheduleDataData> mTaskScheduleDataDatas
                {
                    get { return _TaskScheduleDataDatas; }
                }

                /// <summary>
                /// (任务进度表)字典集合
                /// </summary>
                Dictionary<int, Configs_TaskScheduleDataData> _TaskScheduleDataDatas = new Dictionary<int, Configs_TaskScheduleDataData>();

                /// <summary>
                /// 根据ScheduleID读取对应的配置信息
                /// </summary>
                /// <param name="ScheduleID">配置的ScheduleID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_TaskScheduleDataData GetTaskScheduleDataDataByScheduleID(int ScheduleID)
                {
                    if (_TaskScheduleDataDatas.ContainsKey(ScheduleID))
                    {
                        return _TaskScheduleDataDatas[ScheduleID];
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
  Configs_TaskScheduleDataData cd = new Configs_TaskScheduleDataData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ScheduleID = key; 
  cd.NextSchedule =  Util.GetIntKeyValue(body,"NextSchedule"); 
  cd.TaskID =  Util.GetIntKeyValue(body,"TaskID"); 
  cd.TaskSchedule =  Util.GetIntKeyValue(body,"TaskSchedule"); 
  cd.Condition1 =  Util.GetIntKeyValue(body,"Condition1"); 
  cd.Condition2 =  Util.GetIntKeyValue(body,"Condition2"); 
  cd.Target =  Util.GetIntKeyValue(body,"Target"); 
  cd.TaskClassify =  Util.GetIntKeyValue(body,"TaskClassify"); 
  cd.LimitData =  Util.GetIntKeyValue(body,"LimitData"); 
  cd.OccupationTask =  Util.GetIntKeyValue(body,"OccupationTask"); 
  cd.GiftID =  Util.GetIntKeyValue(body,"GiftID"); 
  
 if (mTaskScheduleDataDatas.ContainsKey(key) == false)
 mTaskScheduleDataDatas.Add(key, cd);
  }
 //Debug.Log(mTaskScheduleDataDatas.Count);
}

            }