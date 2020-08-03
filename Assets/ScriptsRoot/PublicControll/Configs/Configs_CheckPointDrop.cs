/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (关卡掉落表)客户端配置结构体
            /// </summary>
            public partial class Configs_CheckPointDropData 
             { 
                /// <summary>
                /// 掉落ID--主键
                /// </summary>
                public int DropID{get;set;}

                
                /// <summary>
                /// 掉落1类型
                /// </summary>
                public int TypeID1 { get;set; }
                /// <summary>
                /// 道具1ID
                /// </summary>
                public int PropID1 { get;set; }
                /// <summary>
                /// 掉落1常数（10w）
                /// </summary>
                public int Probability1 { get;set; }
                /// <summary>
                /// 道具1数量
                /// </summary>
                public int Number1 { get;set; }
                /// <summary>
                /// 掉落2类型
                /// </summary>
                public int TypeID2 { get;set; }
                /// <summary>
                /// 道具2ID
                /// </summary>
                public int PropID2 { get;set; }
                /// <summary>
                /// 掉落2常数
                /// </summary>
                public int Probability2 { get;set; }
                /// <summary>
                /// 道具2数量
                /// </summary>
                public int Number2 { get;set; }
                /// <summary>
                /// 掉落3类型
                /// </summary>
                public int TypeID3 { get;set; }
                /// <summary>
                /// 道具3ID
                /// </summary>
                public int PropID3 { get;set; }
                /// <summary>
                /// 掉落3常数
                /// </summary>
                public int Probability3 { get;set; }
                /// <summary>
                /// 道具3数量
                /// </summary>
                public int Number3 { get;set; }
                /// <summary>
                /// 掉落4类型
                /// </summary>
                public int TypeID4 { get;set; }
                /// <summary>
                /// 道具4ID
                /// </summary>
                public int PropID4 { get;set; }
                /// <summary>
                /// 掉落4常数
                /// </summary>
                public int Probability4 { get;set; }
                /// <summary>
                /// 道具4数量
                /// </summary>
                public int Number4 { get;set; }
                /// <summary>
                /// 掉落5类型
                /// </summary>
                public int TypeID5 { get;set; }
                /// <summary>
                /// 道具5ID
                /// </summary>
                public int PropID5 { get;set; }
                /// <summary>
                /// 掉落5常数
                /// </summary>
                public int Probability5 { get;set; }
                /// <summary>
                /// 道具5数量
                /// </summary>
                public int Number5 { get;set; }
            } 
            /// <summary>
            /// (关卡掉落表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_CheckPointDrop
            { 

                static Configs_CheckPointDrop _sInstance;
                public static Configs_CheckPointDrop sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_CheckPointDrop();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (关卡掉落表)字典集合
                /// </summary>
                public Dictionary<int, Configs_CheckPointDropData> mCheckPointDropDatas
                {
                    get { return _CheckPointDropDatas; }
                }

                /// <summary>
                /// (关卡掉落表)字典集合
                /// </summary>
                Dictionary<int, Configs_CheckPointDropData> _CheckPointDropDatas = new Dictionary<int, Configs_CheckPointDropData>();

                /// <summary>
                /// 根据DropID读取对应的配置信息
                /// </summary>
                /// <param name="DropID">配置的DropID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_CheckPointDropData GetCheckPointDropDataByDropID(int DropID)
                {
                    if (_CheckPointDropDatas.ContainsKey(DropID))
                    {
                        return _CheckPointDropDatas[DropID];
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
  Configs_CheckPointDropData cd = new Configs_CheckPointDropData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.DropID = key; 
  cd.TypeID1 =  Util.GetIntKeyValue(body,"TypeID1"); 
  cd.PropID1 =  Util.GetIntKeyValue(body,"PropID1"); 
  cd.Probability1 =  Util.GetIntKeyValue(body,"Probability1"); 
  cd.Number1 =  Util.GetIntKeyValue(body,"Number1"); 
  cd.TypeID2 =  Util.GetIntKeyValue(body,"TypeID2"); 
  cd.PropID2 =  Util.GetIntKeyValue(body,"PropID2"); 
  cd.Probability2 =  Util.GetIntKeyValue(body,"Probability2"); 
  cd.Number2 =  Util.GetIntKeyValue(body,"Number2"); 
  cd.TypeID3 =  Util.GetIntKeyValue(body,"TypeID3"); 
  cd.PropID3 =  Util.GetIntKeyValue(body,"PropID3"); 
  cd.Probability3 =  Util.GetIntKeyValue(body,"Probability3"); 
  cd.Number3 =  Util.GetIntKeyValue(body,"Number3"); 
  cd.TypeID4 =  Util.GetIntKeyValue(body,"TypeID4"); 
  cd.PropID4 =  Util.GetIntKeyValue(body,"PropID4"); 
  cd.Probability4 =  Util.GetIntKeyValue(body,"Probability4"); 
  cd.Number4 =  Util.GetIntKeyValue(body,"Number4"); 
  cd.TypeID5 =  Util.GetIntKeyValue(body,"TypeID5"); 
  cd.PropID5 =  Util.GetIntKeyValue(body,"PropID5"); 
  cd.Probability5 =  Util.GetIntKeyValue(body,"Probability5"); 
  cd.Number5 =  Util.GetIntKeyValue(body,"Number5"); 
  
 if (mCheckPointDropDatas.ContainsKey(key) == false)
 mCheckPointDropDatas.Add(key, cd);
  }
 //Debug.Log(mCheckPointDropDatas.Count);
}

            }