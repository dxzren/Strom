/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (模型常驻特效)客户端配置结构体
            /// </summary>
            public partial class Configs_ModelPermanentEffectData 
             { 
                /// <summary>
                /// 资源ID--主键
                /// </summary>
                public int ResourceID{get;set;}

                
                /// <summary>
                /// 绑定骨骼点1
                /// </summary>
                public string BindingBone1 { get;set; }
                /// <summary>
                /// 绑定骨骼点偏移位置1
                /// </summary>
                public List<float> BindingBonePosition1 { get;set; }
                /// <summary>
                /// 绑定骨骼点旋转角度1
                /// </summary>
                public List<float> BindingBoneRotate1 { get;set; }
                /// <summary>
                /// 添加特效名称1
                /// </summary>
                public string AddBoneEffect1 { get;set; }
                /// <summary>
                /// 绑定骨骼点2
                /// </summary>
                public string BindingBone2 { get;set; }
                /// <summary>
                /// 绑定骨骼点偏移位置2
                /// </summary>
                public List<float> BindingBonePosition2 { get;set; }
                /// <summary>
                /// 绑定骨骼点旋转角度2
                /// </summary>
                public List<float> BindingBoneRotate2 { get;set; }
                /// <summary>
                /// 添加特效名称2
                /// </summary>
                public string AddBoneEffect2 { get;set; }
                /// <summary>
                /// 绑定骨骼点3
                /// </summary>
                public string BindingBone3 { get;set; }
                /// <summary>
                /// 绑定骨骼点偏移位置3
                /// </summary>
                public List<float> BindingBonePosition3 { get;set; }
                /// <summary>
                /// 绑定骨骼点旋转角度3
                /// </summary>
                public List<float> BindingBoneRotate3 { get;set; }
                /// <summary>
                /// 添加特效名称3
                /// </summary>
                public string AddBoneEffect3 { get;set; }
                /// <summary>
                /// 绑定骨骼点4
                /// </summary>
                public string BindingBone4 { get;set; }
                /// <summary>
                /// 绑定骨骼点偏移位置4
                /// </summary>
                public List<float> BindingBonePosition4 { get;set; }
                /// <summary>
                /// 绑定骨骼点旋转角度4
                /// </summary>
                public List<float> BindingBoneRotate4 { get;set; }
                /// <summary>
                /// 添加特效名称4
                /// </summary>
                public string AddBoneEffect4 { get;set; }
                /// <summary>
                /// 绑定骨骼点5
                /// </summary>
                public string BindingBone5 { get;set; }
                /// <summary>
                /// 绑定骨骼点偏移位置5
                /// </summary>
                public List<float> BindingBonePosition5 { get;set; }
                /// <summary>
                /// 绑定骨骼点旋转角度5
                /// </summary>
                public List<float> BindingBoneRotate5 { get;set; }
                /// <summary>
                /// 添加特效名称5
                /// </summary>
                public string AddBoneEffect5 { get;set; }
                /// <summary>
                /// 绑定骨骼点6
                /// </summary>
                public string BindingBone6 { get;set; }
                /// <summary>
                /// 绑定骨骼点偏移位置6
                /// </summary>
                public List<float> BindingBonePosition6 { get;set; }
                /// <summary>
                /// 绑定骨骼点旋转角度6
                /// </summary>
                public List<float> BindingBoneRotate6 { get;set; }
                /// <summary>
                /// 添加特效名称6
                /// </summary>
                public string AddBoneEffect6 { get;set; }
            } 
            /// <summary>
            /// (模型常驻特效)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ModelPermanentEffect
            { 

                static Configs_ModelPermanentEffect _sInstance;
                public static Configs_ModelPermanentEffect sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ModelPermanentEffect();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (模型常驻特效)字典集合
                /// </summary>
                public Dictionary<int, Configs_ModelPermanentEffectData> mModelPermanentEffectDatas
                {
                    get { return _ModelPermanentEffectDatas; }
                }

                /// <summary>
                /// (模型常驻特效)字典集合
                /// </summary>
                Dictionary<int, Configs_ModelPermanentEffectData> _ModelPermanentEffectDatas = new Dictionary<int, Configs_ModelPermanentEffectData>();

                /// <summary>
                /// 根据ResourceID读取对应的配置信息
                /// </summary>
                /// <param name="ResourceID">配置的ResourceID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ModelPermanentEffectData GetModelPermanentEffectDataByResourceID(int ResourceID)
                {
                    if (_ModelPermanentEffectDatas.ContainsKey(ResourceID))
                    {
                        return _ModelPermanentEffectDatas[ResourceID];
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
  Configs_ModelPermanentEffectData cd = new Configs_ModelPermanentEffectData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ResourceID = key; 
  cd.BindingBone1 =  Util.GetStringKeyValue(body,"BindingBone1"); 
 
 string[] BindingBonePosition1Strs= Util.GetStringKeyValue(body, "BindingBonePosition1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBonePosition1  = new List<float>();
foreach(string BindingBonePosition1Str in BindingBonePosition1Strs)  cd.BindingBonePosition1.Add(Util.ParseToFloat(BindingBonePosition1Str)); 
 

 string[] BindingBoneRotate1Strs= Util.GetStringKeyValue(body, "BindingBoneRotate1").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBoneRotate1  = new List<float>();
foreach(string BindingBoneRotate1Str in BindingBoneRotate1Strs)  cd.BindingBoneRotate1.Add(Util.ParseToFloat(BindingBoneRotate1Str)); 
 
 cd.AddBoneEffect1 =  Util.GetStringKeyValue(body,"AddBoneEffect1"); 
  cd.BindingBone2 =  Util.GetStringKeyValue(body,"BindingBone2"); 
 
 string[] BindingBonePosition2Strs= Util.GetStringKeyValue(body, "BindingBonePosition2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBonePosition2  = new List<float>();
foreach(string BindingBonePosition2Str in BindingBonePosition2Strs)  cd.BindingBonePosition2.Add(Util.ParseToFloat(BindingBonePosition2Str)); 
 

 string[] BindingBoneRotate2Strs= Util.GetStringKeyValue(body, "BindingBoneRotate2").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBoneRotate2  = new List<float>();
foreach(string BindingBoneRotate2Str in BindingBoneRotate2Strs)  cd.BindingBoneRotate2.Add(Util.ParseToFloat(BindingBoneRotate2Str)); 
 
 cd.AddBoneEffect2 =  Util.GetStringKeyValue(body,"AddBoneEffect2"); 
  cd.BindingBone3 =  Util.GetStringKeyValue(body,"BindingBone3"); 
 
 string[] BindingBonePosition3Strs= Util.GetStringKeyValue(body, "BindingBonePosition3").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBonePosition3  = new List<float>();
foreach(string BindingBonePosition3Str in BindingBonePosition3Strs)  cd.BindingBonePosition3.Add(Util.ParseToFloat(BindingBonePosition3Str)); 
 

 string[] BindingBoneRotate3Strs= Util.GetStringKeyValue(body, "BindingBoneRotate3").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBoneRotate3  = new List<float>();
foreach(string BindingBoneRotate3Str in BindingBoneRotate3Strs)  cd.BindingBoneRotate3.Add(Util.ParseToFloat(BindingBoneRotate3Str)); 
 
 cd.AddBoneEffect3 =  Util.GetStringKeyValue(body,"AddBoneEffect3"); 
  cd.BindingBone4 =  Util.GetStringKeyValue(body,"BindingBone4"); 
 
 string[] BindingBonePosition4Strs= Util.GetStringKeyValue(body, "BindingBonePosition4").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBonePosition4  = new List<float>();
foreach(string BindingBonePosition4Str in BindingBonePosition4Strs)  cd.BindingBonePosition4.Add(Util.ParseToFloat(BindingBonePosition4Str)); 
 

 string[] BindingBoneRotate4Strs= Util.GetStringKeyValue(body, "BindingBoneRotate4").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBoneRotate4  = new List<float>();
foreach(string BindingBoneRotate4Str in BindingBoneRotate4Strs)  cd.BindingBoneRotate4.Add(Util.ParseToFloat(BindingBoneRotate4Str)); 
 
 cd.AddBoneEffect4 =  Util.GetStringKeyValue(body,"AddBoneEffect4"); 
  cd.BindingBone5 =  Util.GetStringKeyValue(body,"BindingBone5"); 
 
 string[] BindingBonePosition5Strs= Util.GetStringKeyValue(body, "BindingBonePosition5").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBonePosition5  = new List<float>();
foreach(string BindingBonePosition5Str in BindingBonePosition5Strs)  cd.BindingBonePosition5.Add(Util.ParseToFloat(BindingBonePosition5Str)); 
 

 string[] BindingBoneRotate5Strs= Util.GetStringKeyValue(body, "BindingBoneRotate5").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBoneRotate5  = new List<float>();
foreach(string BindingBoneRotate5Str in BindingBoneRotate5Strs)  cd.BindingBoneRotate5.Add(Util.ParseToFloat(BindingBoneRotate5Str)); 
 
 cd.AddBoneEffect5 =  Util.GetStringKeyValue(body,"AddBoneEffect5"); 
  cd.BindingBone6 =  Util.GetStringKeyValue(body,"BindingBone6"); 
 
 string[] BindingBonePosition6Strs= Util.GetStringKeyValue(body, "BindingBonePosition6").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBonePosition6  = new List<float>();
foreach(string BindingBonePosition6Str in BindingBonePosition6Strs)  cd.BindingBonePosition6.Add(Util.ParseToFloat(BindingBonePosition6Str)); 
 

 string[] BindingBoneRotate6Strs= Util.GetStringKeyValue(body, "BindingBoneRotate6").TrimStart('{').TrimEnd('}',',').Split(',');
cd.BindingBoneRotate6  = new List<float>();
foreach(string BindingBoneRotate6Str in BindingBoneRotate6Strs)  cd.BindingBoneRotate6.Add(Util.ParseToFloat(BindingBoneRotate6Str)); 
 
 cd.AddBoneEffect6 =  Util.GetStringKeyValue(body,"AddBoneEffect6"); 
  
 if (mModelPermanentEffectDatas.ContainsKey(key) == false)
 mModelPermanentEffectDatas.Add(key, cd);
  }
 //Debug.Log(mModelPermanentEffectDatas.Count);
}

            }