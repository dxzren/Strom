/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (签到表)客户端配置结构体
            /// </summary>
            public partial class Configs_SignInData 
             { 
                /// <summary>
                /// 签到ID--主键
                /// </summary>
                public int SignID{get;set;}

                
                /// <summary>
                /// VIP等级
                /// </summary>
                public int VIPLevel { get;set; }
                /// <summary>
                /// VIP倍数
                /// </summary>
                public int VIPMultiple { get;set; }
                /// <summary>
                /// 礼包ID
                /// </summary>
                public int GiftID { get;set; }
                /// <summary>
                /// 奖励数量
                /// </summary>
                public int PropNumber { get;set; }
            } 
            /// <summary>
            /// (签到表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_SignIn
            { 

                static Configs_SignIn _sInstance;
                public static Configs_SignIn sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_SignIn();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (签到表)字典集合
                /// </summary>
                public Dictionary<int, Configs_SignInData> mSignInDatas
                {
                    get { return _SignInDatas; }
                }

                /// <summary>
                /// (签到表)字典集合
                /// </summary>
                Dictionary<int, Configs_SignInData> _SignInDatas = new Dictionary<int, Configs_SignInData>();

                /// <summary>
                /// 根据SignID读取对应的配置信息
                /// </summary>
                /// <param name="SignID">配置的SignID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_SignInData GetSignInDataBySignID(int SignID)
                {
                    if (_SignInDatas.ContainsKey(SignID))
                    {
                        return _SignInDatas[SignID];
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
  Configs_SignInData cd = new Configs_SignInData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.SignID = key; 
  cd.VIPLevel =  Util.GetIntKeyValue(body,"VIPLevel"); 
  cd.VIPMultiple =  Util.GetIntKeyValue(body,"VIPMultiple"); 
  cd.GiftID =  Util.GetIntKeyValue(body,"GiftID"); 
  cd.PropNumber =  Util.GetIntKeyValue(body,"PropNumber"); 
  
 if (mSignInDatas.ContainsKey(key) == false)
 mSignInDatas.Add(key, cd);
  }
 //Debug.Log(mSignInDatas.Count);
}

            }