/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (活动表)客户端配置结构体
            /// </summary>
            public partial class Configs_ActivityData 
             { 
                /// <summary>
                /// 活动ID--主键
                /// </summary>
                public int ActivityID{get;set;}

                
                /// <summary>
                /// 活动类别
                /// </summary>
                public int Type { get;set; }
                /// <summary>
                /// 活动时效
                /// </summary>
                public int Time { get;set; }
                /// <summary>
                /// 开启天数
                /// </summary>
                public List<int> OpenDay { get;set; }
                /// <summary>
                /// 前置条件vip
                /// </summary>
                public int PreconditionVip { get;set; }
                /// <summary>
                /// 前置条件钻石
                /// </summary>
                public int PreconditionDiamond { get;set; }
                /// <summary>
                /// 活动名称
                /// </summary>
                public string ActivityName { get;set; }
                /// <summary>
                /// 活动图标
                /// </summary>
                public string ActivityIcon { get;set; }
                /// <summary>
                /// UI类型
                /// </summary>
                public int UIType { get;set; }
                /// <summary>
                /// 条件描述
                /// </summary>
                public string ConditionDes { get;set; }
                /// <summary>
                /// 活动描述
                /// </summary>
                public string ActivityDes { get;set; }
                /// <summary>
                /// 前端显示类别
                /// </summary>
                public int ShowType { get;set; }
                /// <summary>
                /// 新活动标识
                /// </summary>
                public int NewSign { get;set; }
            } 
            /// <summary>
            /// (活动表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Activity
            { 

                static Configs_Activity _sInstance;
                public static Configs_Activity sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Activity();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (活动表)字典集合
                /// </summary>
                public Dictionary<int, Configs_ActivityData> mActivityDatas
                {
                    get { return _ActivityDatas; }
                }

                /// <summary>
                /// (活动表)字典集合
                /// </summary>
                Dictionary<int, Configs_ActivityData> _ActivityDatas = new Dictionary<int, Configs_ActivityData>();

                /// <summary>
                /// 根据ActivityID读取对应的配置信息
                /// </summary>
                /// <param name="ActivityID">配置的ActivityID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ActivityData GetActivityDataByActivityID(int ActivityID)
                {
                    if (_ActivityDatas.ContainsKey(ActivityID))
                    {
                        return _ActivityDatas[ActivityID];
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
  Configs_ActivityData cd = new Configs_ActivityData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ActivityID = key; 
  cd.Type =  Util.GetIntKeyValue(body,"Type"); 
  cd.Time =  Util.GetIntKeyValue(body,"Time"); 
 
 string[] OpenDayStrs= Util.GetStringKeyValue(body, "OpenDay").TrimStart('{').TrimEnd('}',',').Split(',');
cd.OpenDay = new List<int>();
foreach(string OpenDayStr in OpenDayStrs)  cd.OpenDay.Add(Util.ParseToInt(OpenDayStr)); 
 
 cd.PreconditionVip =  Util.GetIntKeyValue(body,"PreconditionVip"); 
  cd.PreconditionDiamond =  Util.GetIntKeyValue(body,"PreconditionDiamond"); 
  cd.ActivityName =  Util.GetStringKeyValue(body,"ActivityName"); 
  cd.ActivityIcon =  Util.GetStringKeyValue(body,"ActivityIcon"); 
  cd.UIType =  Util.GetIntKeyValue(body,"UIType"); 
  cd.ConditionDes =  Util.GetStringKeyValue(body,"ConditionDes"); 
  cd.ActivityDes =  Util.GetStringKeyValue(body,"ActivityDes"); 
  cd.ShowType =  Util.GetIntKeyValue(body,"ShowType"); 
  cd.NewSign =  Util.GetIntKeyValue(body,"NewSign"); 
  
 if (mActivityDatas.ContainsKey(key) == false)
 mActivityDatas.Add(key, cd);
  }
 //Debug.Log(mActivityDatas.Count);
}

            }