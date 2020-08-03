/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (任务表)客户端配置结构体
            /// </summary>
            public partial class Configs_TaskDataData 
             { 
                /// <summary>
                /// 任务ID--主键
                /// </summary>
                public int TaskID{get;set;}

                
                /// <summary>
                /// 任务分类
                /// </summary>
                public int TaskClassify { get;set; }
                /// <summary>
                /// 特殊标识
                /// </summary>
                public int SpecialIdentification { get;set; }
                /// <summary>
                /// 开放等级
                /// </summary>
                public int LimitData { get;set; }
                /// <summary>
                /// 前往系统
                /// </summary>
                public int GotoSystem { get;set; }
                /// <summary>
                /// 任务目标
                /// </summary>
                public int Target { get;set; }
                /// <summary>
                /// 任务描述
                /// </summary>
                public string TargetDescribe { get;set; }
                /// <summary>
                /// 初始进度ID
                /// </summary>
                public int FirstSchedule { get;set; }
                /// <summary>
                /// 任务图标
                /// </summary>
                public string TaskIcon { get;set; }
                /// <summary>
                /// 任务名称
                /// </summary>
                public string TaskName { get;set; }
            } 
            /// <summary>
            /// (任务表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_TaskData
            { 

                static Configs_TaskData _sInstance;
                public static Configs_TaskData sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_TaskData();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (任务表)字典集合
                /// </summary>
                public Dictionary<int, Configs_TaskDataData> mTaskDataDatas
                {
                    get { return _TaskDataDatas; }
                }

                /// <summary>
                /// (任务表)字典集合
                /// </summary>
                Dictionary<int, Configs_TaskDataData> _TaskDataDatas = new Dictionary<int, Configs_TaskDataData>();

                /// <summary>
                /// 根据TaskID读取对应的配置信息
                /// </summary>
                /// <param name="TaskID">配置的TaskID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_TaskDataData GetTaskDataDataByTaskID(int TaskID)
                {
                    if (_TaskDataDatas.ContainsKey(TaskID))
                    {
                        return _TaskDataDatas[TaskID];
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
  Configs_TaskDataData cd = new Configs_TaskDataData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.TaskID = key; 
  cd.TaskClassify =  Util.GetIntKeyValue(body,"TaskClassify"); 
  cd.SpecialIdentification =  Util.GetIntKeyValue(body,"SpecialIdentification"); 
  cd.LimitData =  Util.GetIntKeyValue(body,"LimitData"); 
  cd.GotoSystem =  Util.GetIntKeyValue(body,"GotoSystem"); 
  cd.Target =  Util.GetIntKeyValue(body,"Target"); 
  cd.TargetDescribe =  Util.GetStringKeyValue(body,"TargetDescribe"); 
  cd.FirstSchedule =  Util.GetIntKeyValue(body,"FirstSchedule"); 
  cd.TaskIcon =  Util.GetStringKeyValue(body,"TaskIcon"); 
  cd.TaskName =  Util.GetStringKeyValue(body,"TaskName"); 
  
 if (mTaskDataDatas.ContainsKey(key) == false)
 mTaskDataDatas.Add(key, cd);
  }
 //Debug.Log(mTaskDataDatas.Count);
}

            }