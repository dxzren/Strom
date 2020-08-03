/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (竞技场段位及奖励表)客户端配置结构体
            /// </summary>
            public partial class Configs_JJCLevelAndAwardData 
             { 
                /// <summary>
                /// 段位等级--主键
                /// </summary>
                public int HonorLevel{get;set;}

                
                /// <summary>
                /// 所需积分
                /// </summary>
                public int NeedExploit { get;set; }
                /// <summary>
                /// 属性调整百分比
                /// </summary>
                public int AbilityModifier { get;set; }
                /// <summary>
                /// 每日段位礼包ID
                /// </summary>
                public int LevelGiftID { get;set; }
                /// <summary>
                /// 段位挑战奖励钻石
                /// </summary>
                public int HonorAwardDiamond { get;set; }
                /// <summary>
                /// 段位中文名
                /// </summary>
                public string Name { get;set; }
                /// <summary>
                /// 段位名称
                /// </summary>
                public string LevelName { get;set; }
                /// <summary>
                /// 等级图标
                /// </summary>
                public string LvlIcon { get;set; }
    /// <summary>
    /// 魂石数量
    /// </summary>
    public int SoulNumber { get; set; }
} 
            /// <summary>
            /// (竞技场段位及奖励表)客户端配置数据集合类
            /// </summary>
            public partial class Configs_JJCLevelAndAward
            { 

                static Configs_JJCLevelAndAward _sInstance;
                public static Configs_JJCLevelAndAward sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_JJCLevelAndAward();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (竞技场段位及奖励表)字典集合
                /// </summary>
                public Dictionary<int, Configs_JJCLevelAndAwardData> mJJCLevelAndAwardDatas
                {
                    get { return _JJCLevelAndAwardDatas; }
                }

                /// <summary>
                /// (竞技场段位及奖励表)字典集合
                /// </summary>
                Dictionary<int, Configs_JJCLevelAndAwardData> _JJCLevelAndAwardDatas = new Dictionary<int, Configs_JJCLevelAndAwardData>();

                /// <summary>
                /// 根据HonorLevel读取对应的配置信息
                /// </summary>
                /// <param name="HonorLevel">配置的HonorLevel</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_JJCLevelAndAwardData GetJJCLevelAndAwardDataByHonorLevel(int HonorLevel)
                {
                    if (_JJCLevelAndAwardDatas.ContainsKey(HonorLevel))
                    {
                        return _JJCLevelAndAwardDatas[HonorLevel];
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
  Configs_JJCLevelAndAwardData cd = new Configs_JJCLevelAndAwardData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.HonorLevel = key; 
  cd.NeedExploit =  Util.GetIntKeyValue(body,"NeedExploit"); 
  cd.AbilityModifier =  Util.GetIntKeyValue(body,"AbilityModifier"); 
  cd.LevelGiftID =  Util.GetIntKeyValue(body,"LevelGiftID"); 
  cd.HonorAwardDiamond =  Util.GetIntKeyValue(body,"HonorAwardDiamond"); 
  cd.Name =  Util.GetStringKeyValue(body,"Name"); 
  cd.LevelName =  Util.GetStringKeyValue(body,"LevelName"); 
  cd.LvlIcon =  Util.GetStringKeyValue(body,"LvlIcon");
            cd.SoulNumber = Util.GetIntKeyValue(body, "SoulNumber");
            if (mJJCLevelAndAwardDatas.ContainsKey(key) == false)
 mJJCLevelAndAwardDatas.Add(key, cd);
  }
 //Debug.Log(mJJCLevelAndAwardDatas.Count);
}

            }