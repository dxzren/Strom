/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (新手引导)客户端配置结构体
            /// </summary>
            public partial class Configs_BeginnersGuideData 
             { 
                /// <summary>
                /// 编号--主键
                /// </summary>
                public int Number{get;set;}

                
                /// <summary>
                /// 类型
                /// </summary>
                public int Type { get;set; }
                /// <summary>
                /// 对话ID
                /// </summary>
                public List<int> DialogueID { get;set; }
                /// <summary>
                /// 可点击区域类型
                /// </summary>
                public string ClickAreaType { get;set; }
                /// <summary>
                /// 结束条件类型
                /// </summary>
                public string EndConditionType { get;set; }
                /// <summary>
                /// 进度标识
                /// </summary>
                public string Schedule { get;set; }
                /// <summary>
                /// 控件名称
                /// </summary>
                public string ControlName { get;set; }
                /// <summary>
                /// 下一引导ID
                /// </summary>
                public int NextNumber { get;set; }
                /// <summary>
                /// 需要引导进入过程
                /// </summary>
                public List<int> LeadIntoTheProcess { get;set; }
                /// <summary>
                /// 引导员坐标
                /// </summary>
                public List<int> GuideXy { get;set; }
                /// <summary>
                /// 监视控件
                /// </summary>
                public string MonitorControl { get;set; }
                /// <summary>
                /// 旁白
                /// </summary>
                public string girlVoice { get;set; }
            } 
            /// <summary>
            /// (新手引导)客户端配置数据集合类
            /// </summary>
            public partial class Configs_BeginnersGuide
            { 

                static Configs_BeginnersGuide _sInstance;
                public static Configs_BeginnersGuide sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_BeginnersGuide();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (新手引导)字典集合
                /// </summary>
                public Dictionary<int, Configs_BeginnersGuideData> mBeginnersGuideDatas
                {
                    get { return _BeginnersGuideDatas; }
                }

                /// <summary>
                /// (新手引导)字典集合
                /// </summary>
                Dictionary<int, Configs_BeginnersGuideData> _BeginnersGuideDatas = new Dictionary<int, Configs_BeginnersGuideData>();

                /// <summary>
                /// 根据Number读取对应的配置信息
                /// </summary>
                /// <param name="Number">配置的Number</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_BeginnersGuideData GetBeginnersGuideDataByNumber(int Number)
                {
                    if (_BeginnersGuideDatas.ContainsKey(Number))
                    {
                        return _BeginnersGuideDatas[Number];
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
  Configs_BeginnersGuideData cd = new Configs_BeginnersGuideData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.Number = key; 
  cd.Type =  Util.GetIntKeyValue(body,"Type"); 
 
 string[] DialogueIDStrs= Util.GetStringKeyValue(body, "DialogueID").TrimStart('{').TrimEnd('}',',').Split(',');
cd.DialogueID = new List<int>();
foreach(string DialogueIDStr in DialogueIDStrs)  cd.DialogueID.Add(Util.ParseToInt(DialogueIDStr)); 
 
 cd.ClickAreaType =  Util.GetStringKeyValue(body,"ClickAreaType"); 
  cd.EndConditionType =  Util.GetStringKeyValue(body,"EndConditionType"); 
  cd.Schedule =  Util.GetStringKeyValue(body,"Schedule"); 
  cd.ControlName =  Util.GetStringKeyValue(body,"ControlName"); 
  cd.NextNumber =  Util.GetIntKeyValue(body,"NextNumber"); 
 
 string[] LeadIntoTheProcessStrs= Util.GetStringKeyValue(body, "LeadIntoTheProcess").TrimStart('{').TrimEnd('}',',').Split(',');
cd.LeadIntoTheProcess = new List<int>();
foreach(string LeadIntoTheProcessStr in LeadIntoTheProcessStrs)  cd.LeadIntoTheProcess.Add(Util.ParseToInt(LeadIntoTheProcessStr)); 
 

 string[] GuideXyStrs= Util.GetStringKeyValue(body, "GuideXy").TrimStart('{').TrimEnd('}',',').Split(',');
cd.GuideXy = new List<int>();
foreach(string GuideXyStr in GuideXyStrs)  cd.GuideXy.Add(Util.ParseToInt(GuideXyStr)); 
 
 cd.MonitorControl =  Util.GetStringKeyValue(body,"MonitorControl"); 
  cd.girlVoice =  Util.GetStringKeyValue(body,"girlVoice"); 
  
 if (mBeginnersGuideDatas.ContainsKey(key) == false)
 mBeginnersGuideDatas.Add(key, cd);
  }
 //Debug.Log(mBeginnersGuideDatas.Count);
}

            }