/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (翅膀)客户端配置结构体
            /// </summary>
            public partial class Configs_WingData 
             { 
                /// <summary>
                /// 翅膀序号--主键
                /// </summary>
                public int WingNum{get;set;}

                
                /// <summary>
                /// 进阶材料ID
                /// </summary>
                public int PropID { get;set; }
                /// <summary>
                /// 进阶材料数量
                /// </summary>
                public int UpdateNumber { get;set; }
                /// <summary>
                /// 晋级ID
                /// </summary>
                public int NextLevelID { get;set; }
                /// <summary>
                /// 祝福值上限
                /// </summary>
                public int MaxBless { get;set; }
                /// <summary>
                /// 低祝福值
                /// </summary>
                public int LowBless { get;set; }
                /// <summary>
                /// 低概率(千分比)
                /// </summary>
                public int LowProbability { get;set; }
                /// <summary>
                /// 高祝福值
                /// </summary>
                public int HighBless { get;set; }
                /// <summary>
                /// 高概率(千分比)
                /// </summary>
                public int HighProbability { get;set; }
                /// <summary>
                /// 祝福基础值
                /// </summary>
                public int BaseBless { get;set; }
                /// <summary>
                /// 浮动上限
                /// </summary>
                public int MaxFloating { get;set; }
                /// <summary>
                /// 暴击概率(千分比)
                /// </summary>
                public int CriticalProbability { get;set; }
                /// <summary>
                /// 指向英雄ID
                /// </summary>
                public int HeroID { get;set; }
                /// <summary>
                /// 翅膀类型
                /// </summary>
                public int WingType { get;set; }
                /// <summary>
                /// 翅膀阶数
                /// </summary>
                public int WingQuality { get;set; }
                /// <summary>
                /// 生命
                /// </summary>
                public int Blood { get;set; }
                /// <summary>
                /// 物理攻击
                /// </summary>
                public int PhysicalAttack { get;set; }
                /// <summary>
                /// 物理护甲
                /// </summary>
                public int PhysicalArmor { get;set; }
                /// <summary>
                /// 魔法强度
                /// </summary>
                public int MagicAttack { get;set; }
                /// <summary>
                /// 魔法抗性
                /// </summary>
                public int MagicArmor { get;set; }
                /// <summary>
                /// 物理暴击
                /// </summary>
                public int PhysicalCritical { get;set; }
                /// <summary>
                /// 翅膀描述
                /// </summary>
                public string WingDes { get;set; }
                /// <summary>
                /// 翅膀名称
                /// </summary>
                public string WingName { get;set; }
                /// <summary>
                /// 翅膀图标_100
                /// </summary>
                public string WingIcon_100 { get;set; }
                /// <summary>
                /// 翅膀绑定骨骼路径
                /// </summary>
                public string WingBonePath { get;set; }
                /// <summary>
                /// 翅膀相对骨骼偏移
                /// </summary>
                public List<float> WingBonePos { get;set; }
                /// <summary>
                /// 翅膀特效名称
                /// </summary>
                public string WingEffectNmae { get;set; }
                /// <summary>
                /// 翅膀待机动作
                /// </summary>
                public string WingStandbyAction { get;set; }
                /// <summary>
                /// 翅膀升阶动作
                /// </summary>
                public string WingPromoteAction { get;set; }
                /// <summary>
                /// 翅膀跑步动作
                /// </summary>
                public string WingRunAction { get;set; }
            } 
            /// <summary>
            /// (翅膀)客户端配置数据集合类
            /// </summary>
            public partial class Configs_Wing
            { 

                static Configs_Wing _sInstance;
                public static Configs_Wing sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_Wing();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (翅膀)字典集合
                /// </summary>
                public Dictionary<int, Configs_WingData> mWingDatas
                {
                    get { return _WingDatas; }
                }

                /// <summary>
                /// (翅膀)字典集合
                /// </summary>
                Dictionary<int, Configs_WingData> _WingDatas = new Dictionary<int, Configs_WingData>();

                /// <summary>
                /// 根据WingNum读取对应的配置信息
                /// </summary>
                /// <param name="WingNum">配置的WingNum</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_WingData GetWingDataByWingNum(int WingNum)
                {
                    if (_WingDatas.ContainsKey(WingNum))
                    {
                        return _WingDatas[WingNum];
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
  Configs_WingData cd = new Configs_WingData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.WingNum = key; 
  cd.PropID =  Util.GetIntKeyValue(body,"PropID"); 
  cd.UpdateNumber =  Util.GetIntKeyValue(body,"UpdateNumber"); 
  cd.NextLevelID =  Util.GetIntKeyValue(body,"NextLevelID"); 
  cd.MaxBless =  Util.GetIntKeyValue(body,"MaxBless"); 
  cd.LowBless =  Util.GetIntKeyValue(body,"LowBless"); 
  cd.LowProbability =  Util.GetIntKeyValue(body,"LowProbability"); 
  cd.HighBless =  Util.GetIntKeyValue(body,"HighBless"); 
  cd.HighProbability =  Util.GetIntKeyValue(body,"HighProbability"); 
  cd.BaseBless =  Util.GetIntKeyValue(body,"BaseBless"); 
  cd.MaxFloating =  Util.GetIntKeyValue(body,"MaxFloating"); 
  cd.CriticalProbability =  Util.GetIntKeyValue(body,"CriticalProbability"); 
  cd.HeroID =  Util.GetIntKeyValue(body,"HeroID"); 
  cd.WingType =  Util.GetIntKeyValue(body,"WingType"); 
  cd.WingQuality =  Util.GetIntKeyValue(body,"WingQuality"); 
  cd.Blood =  Util.GetIntKeyValue(body,"Blood"); 
  cd.PhysicalAttack =  Util.GetIntKeyValue(body,"PhysicalAttack"); 
  cd.PhysicalArmor =  Util.GetIntKeyValue(body,"PhysicalArmor"); 
  cd.MagicAttack =  Util.GetIntKeyValue(body,"MagicAttack"); 
  cd.MagicArmor =  Util.GetIntKeyValue(body,"MagicArmor"); 
  cd.PhysicalCritical =  Util.GetIntKeyValue(body,"PhysicalCritical"); 
  cd.WingDes =  Util.GetStringKeyValue(body,"WingDes"); 
  cd.WingName =  Util.GetStringKeyValue(body,"WingName"); 
  cd.WingIcon_100 =  Util.GetStringKeyValue(body,"WingIcon_100"); 
  cd.WingBonePath =  Util.GetStringKeyValue(body,"WingBonePath"); 
 
 string[] WingBonePosStrs= Util.GetStringKeyValue(body, "WingBonePos").TrimStart('{').TrimEnd('}',',').Split(',');
cd.WingBonePos  = new List<float>();
foreach(string WingBonePosStr in WingBonePosStrs)  cd.WingBonePos.Add(Util.ParseToFloat(WingBonePosStr)); 
 
 cd.WingEffectNmae =  Util.GetStringKeyValue(body,"WingEffectNmae"); 
  cd.WingStandbyAction =  Util.GetStringKeyValue(body,"WingStandbyAction"); 
  cd.WingPromoteAction =  Util.GetStringKeyValue(body,"WingPromoteAction"); 
  cd.WingRunAction =  Util.GetStringKeyValue(body,"WingRunAction"); 
  
 if (mWingDatas.ContainsKey(key) == false)
 mWingDatas.Add(key, cd);
  }
 //Debug.Log(mWingDatas.Count);
}

            }