/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (界面信息表)客户端配置结构体
            /// </summary>
            public partial class Configs_PanelInformationData 
             { 
                /// <summary>
                /// Panel名称--主键
                /// </summary>
                public string PanelName{get;set;}

                
                /// <summary>
                /// 当前界面层级
                /// </summary>
                public int PanelFloor { get;set; }
                /// <summary>
                /// 位移起始坐标
                /// </summary>
                public List<int> BeginCoordinate { get;set; }
                /// <summary>
                /// 结束坐标
                /// </summary>
                public List<int> EndCoordinate { get;set; }
            } 
            /// <summary>
            /// (界面信息表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_PanelInformation
            { 

                static Configs_PanelInformation _sInstance;
                public static Configs_PanelInformation sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_PanelInformation();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (界面信息表)字典集合
                /// </summary>
                public Dictionary<string, Configs_PanelInformationData> mPanelInformationDatas
                {
                    get { return _PanelInformationDatas; }
                }

                /// <summary>
                /// (界面信息表)字典集合
                /// </summary>
                Dictionary<string, Configs_PanelInformationData> _PanelInformationDatas = new Dictionary<string, Configs_PanelInformationData>();

                /// <summary>
                /// 根据PanelName读取对应的配置信息
                /// </summary>
                /// <param name="PanelName">配置的PanelName</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_PanelInformationData GetPanelInformationDataByPanelName(string PanelName)
                {
                    if (_PanelInformationDatas.ContainsKey(PanelName))
                    {
                        return _PanelInformationDatas[PanelName];
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
  Configs_PanelInformationData cd = new Configs_PanelInformationData(); 
  string key = element.Key; 
  cd.PanelName = key; 
  cd.PanelFloor =  Util.GetIntKeyValue(body,"PanelFloor"); 
 
 string[] BeginCoordinateStrs= Util.GetStringKeyValue(body, "BeginCoordinate").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BeginCoordinate = new List<int>();
foreach(string BeginCoordinateStr in BeginCoordinateStrs)  cd.BeginCoordinate.Add(Util.ParseToInt(BeginCoordinateStr)); 
 

 string[] EndCoordinateStrs= Util.GetStringKeyValue(body, "EndCoordinate").TrimStart('{').TrimEnd('}',',').Split(',');
cd.EndCoordinate = new List<int>();
foreach(string EndCoordinateStr in EndCoordinateStrs)  cd.EndCoordinate.Add(Util.ParseToInt(EndCoordinateStr)); 
 
 
 if (mPanelInformationDatas.ContainsKey(key) == false)
 mPanelInformationDatas.Add(key, cd);
  }
 //Debug.Log(mPanelInformationDatas.Count);
}

            }