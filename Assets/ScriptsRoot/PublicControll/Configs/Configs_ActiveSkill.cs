/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (主动技能)客户端配置结构体
            /// </summary>
            public partial class Configs_ActiveSkillData 
             { 
                /// <summary>
                /// 主动技能ID--主键
                /// </summary>
                public int ActiveSkillID{get;set;}

                
                /// <summary>
                /// 技能名
                /// </summary>
                public string SkillName { get;set; }
                /// <summary>
                /// 释放时机
                /// </summary>
                public int ReleaseTime { get;set; }
                /// <summary>
                /// 效果范围
                /// </summary>
                public string Range { get;set; }
                /// <summary>
                /// 效果1_1
                /// </summary>
                public string Effect1_1 { get;set; }
                /// <summary>
                /// 伤害公式类型1_1
                /// </summary>
                public int FormulaType1_1 { get;set; }
                /// <summary>
                /// 伤害系数1_1
                /// </summary>
                public float Coefficient1_1 { get;set; }
                /// <summary>
                /// CD1_1
                /// </summary>
                public int CD1_1 { get;set; }
                /// <summary>
                /// 基础值1_1
                /// </summary>
                public float BaseValue1_1 { get;set; }
                /// <summary>
                /// 成长值1_1
                /// </summary>
                public float UpValue1_1 { get;set; }
                /// <summary>
                /// 效果1_2
                /// </summary>
                public string Effect1_2 { get;set; }
                /// <summary>
                /// 伤害公式类型1_2
                /// </summary>
                public int FormulaType1_2 { get;set; }
                /// <summary>
                /// 伤害系数1_2
                /// </summary>
                public float Coefficient1_2 { get;set; }
                /// <summary>
                /// CD1_2
                /// </summary>
                public int CD1_2 { get;set; }
                /// <summary>
                /// 基础值1_2
                /// </summary>
                public float BaseValue1_2 { get;set; }
                /// <summary>
                /// 成长值1_2
                /// </summary>
                public float UpValue1_2 { get;set; }
                /// <summary>
                /// 数值描述
                /// </summary>
                public string ValueDes { get;set; }
                /// <summary>
                /// 文字描述
                /// </summary>
                public string TextDes { get;set; }
                /// <summary>
                /// 技能图标84
                /// </summary>
                public string Icon84 { get;set; }
                /// <summary>
                /// 绑定位置1
                /// </summary>
                public int Position1 { get;set; }
                /// <summary>
                /// BUFF效果1
                /// </summary>
                public string Expression1 { get;set; }
                /// <summary>
                /// 绑定位置2
                /// </summary>
                public int Position2 { get;set; }
                /// <summary>
                /// BUFF效果2
                /// </summary>
                public string Expression2 { get;set; }
            } 
            /// <summary>
            /// (主动技能)客户端配置数据集合类
            /// </summary>
            public partial class Configs_ActiveSkill
            { 

                static Configs_ActiveSkill _sInstance;
                public static Configs_ActiveSkill sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_ActiveSkill();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (主动技能)字典集合
                /// </summary>
                public Dictionary<int, Configs_ActiveSkillData> mActiveSkillDatas
                {
                    get { return _ActiveSkillDatas; }
                }

                /// <summary>
                /// (主动技能)字典集合
                /// </summary>
                Dictionary<int, Configs_ActiveSkillData> _ActiveSkillDatas = new Dictionary<int, Configs_ActiveSkillData>();

                /// <summary>
                /// 根据ActiveSkillID读取对应的配置信息
                /// </summary>
                /// <param name="ActiveSkillID">配置的ActiveSkillID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_ActiveSkillData GetActiveSkillDataByActiveSkillID(int ActiveSkillID)
                {
                    if (_ActiveSkillDatas.ContainsKey(ActiveSkillID))
                    {
                        return _ActiveSkillDatas[ActiveSkillID];
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
  Configs_ActiveSkillData cd = new Configs_ActiveSkillData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.ActiveSkillID = key; 
  cd.SkillName =  Util.GetStringKeyValue(body,"SkillName"); 
  cd.ReleaseTime =  Util.GetIntKeyValue(body,"ReleaseTime"); 
  cd.Range =  Util.GetStringKeyValue(body,"Range"); 
  cd.Effect1_1 =  Util.GetStringKeyValue(body,"Effect1_1"); 
  cd.FormulaType1_1 =  Util.GetIntKeyValue(body,"FormulaType1_1"); 
  cd.Coefficient1_1 =  Util.GetFloatKeyValue(body,"Coefficient1_1"); 
  cd.CD1_1 =  Util.GetIntKeyValue(body,"CD1_1"); 
  cd.BaseValue1_1 =  Util.GetFloatKeyValue(body,"BaseValue1_1"); 
  cd.UpValue1_1 =  Util.GetFloatKeyValue(body,"UpValue1_1"); 
  cd.Effect1_2 =  Util.GetStringKeyValue(body,"Effect1_2"); 
  cd.FormulaType1_2 =  Util.GetIntKeyValue(body,"FormulaType1_2"); 
  cd.Coefficient1_2 =  Util.GetFloatKeyValue(body,"Coefficient1_2"); 
  cd.CD1_2 =  Util.GetIntKeyValue(body,"CD1_2"); 
  cd.BaseValue1_2 =  Util.GetFloatKeyValue(body,"BaseValue1_2"); 
  cd.UpValue1_2 =  Util.GetFloatKeyValue(body,"UpValue1_2"); 
  cd.ValueDes =  Util.GetStringKeyValue(body,"ValueDes"); 
  cd.TextDes =  Util.GetStringKeyValue(body,"TextDes"); 
  cd.Icon84 =  Util.GetStringKeyValue(body,"Icon84"); 
  cd.Position1 =  Util.GetIntKeyValue(body,"Position1"); 
  cd.Expression1 =  Util.GetStringKeyValue(body,"Expression1"); 
  cd.Position2 =  Util.GetIntKeyValue(body,"Position2"); 
  cd.Expression2 =  Util.GetStringKeyValue(body,"Expression2"); 
  
 if (mActiveSkillDatas.ContainsKey(key) == false)
 mActiveSkillDatas.Add(key, cd);
  }
 //Debug.Log(mActiveSkillDatas.Count);
}

            }