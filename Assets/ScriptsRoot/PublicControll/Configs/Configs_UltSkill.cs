/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;
 
            /// <summary>
            /// (大招技能)客户端配置结构体
            /// </summary>
            public partial class Configs_UltSkillData 
             { 
                /// <summary>
                /// 大招技能ID--主键
                /// </summary>
                public int UltSkillID{get;set;}

                
                /// <summary>
                /// 技能名
                /// </summary>
                public string SkillName { get;set; }
                /// <summary>
                /// 连击数
                /// </summary>
                public int DoubleHitNum { get;set; }
                /// <summary>
                /// 效果范围1
                /// </summary>
                public string Range1 { get;set; }
                /// <summary>
                /// 效果范围2
                /// </summary>
                public string Range2 { get;set; }
                /// <summary>
                /// 效果范围3
                /// </summary>
                public string Range3 { get;set; }
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
                /// 效果2_1
                /// </summary>
                public string Effect2_1 { get;set; }
                /// <summary>
                /// 伤害公式类型2_1
                /// </summary>
                public int FormulaType2_1 { get;set; }
                /// <summary>
                /// 伤害系数2_1
                /// </summary>
                public float Coefficient2_1 { get;set; }
                /// <summary>
                /// CD2_1
                /// </summary>
                public int CD2_1 { get;set; }
                /// <summary>
                /// 基础值2_1
                /// </summary>
                public float BaseValue2_1 { get;set; }
                /// <summary>
                /// 成长值2_1
                /// </summary>
                public float UpValue2_1 { get;set; }
                /// <summary>
                /// 效果2_2
                /// </summary>
                public string Effect2_2 { get;set; }
                /// <summary>
                /// 伤害公式类型2_2
                /// </summary>
                public int FormulaType2_2 { get;set; }
                /// <summary>
                /// 伤害系数2_2
                /// </summary>
                public float Coefficient2_2 { get;set; }
                /// <summary>
                /// CD2_2
                /// </summary>
                public int CD2_2 { get;set; }
                /// <summary>
                /// 基础值2_2
                /// </summary>
                public float BaseValue2_2 { get;set; }
                /// <summary>
                /// 成长值2_2
                /// </summary>
                public float UpValue2_2 { get;set; }
                /// <summary>
                /// 效果3_1
                /// </summary>
                public string Effect3_1 { get;set; }
                /// <summary>
                /// 伤害公式类型3_1
                /// </summary>
                public int FormulaType3_1 { get;set; }
                /// <summary>
                /// 伤害系数3_1
                /// </summary>
                public float Coefficient3_1 { get;set; }
                /// <summary>
                /// CD3_1
                /// </summary>
                public int CD3_1 { get;set; }
                /// <summary>
                /// 基础值3_1
                /// </summary>
                public float BaseValue3_1 { get;set; }
                /// <summary>
                /// 成长值3_1
                /// </summary>
                public float UpValue3_1 { get;set; }
                /// <summary>
                /// 效果3_2
                /// </summary>
                public string Effect3_2 { get;set; }
                /// <summary>
                /// 伤害公式类型3_2
                /// </summary>
                public int FormulaType3_2 { get;set; }
                /// <summary>
                /// 伤害系数3_2
                /// </summary>
                public float Coefficient3_2 { get;set; }
                /// <summary>
                /// CD3_2
                /// </summary>
                public int CD3_2 { get;set; }
                /// <summary>
                /// 基础值3_2
                /// </summary>
                public float BaseValue3_2 { get;set; }
                /// <summary>
                /// 成长值3_2
                /// </summary>
                public float UpValue3_2 { get;set; }
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
            /// (大招技能)客户端配置数据集合类
            /// </summary>
            public partial class Configs_UltSkill
            { 

                static Configs_UltSkill _sInstance;
                public static Configs_UltSkill sInstance
                {
                    get
                    {
                        if (_sInstance == null) _sInstance = new Configs_UltSkill();
                        return _sInstance;
                    }
                }

                /// <summary>
                /// (大招技能)字典集合
                /// </summary>
                public Dictionary<int, Configs_UltSkillData> mUltSkillDatas
                {
                    get { return _UltSkillDatas; }
                }

                /// <summary>
                /// (大招技能)字典集合
                /// </summary>
                Dictionary<int, Configs_UltSkillData> _UltSkillDatas = new Dictionary<int, Configs_UltSkillData>();

                /// <summary>
                /// 根据UltSkillID读取对应的配置信息
                /// </summary>
                /// <param name="UltSkillID">配置的UltSkillID</param>
                /// <returns>配置类，无则返回null</returns>
                public Configs_UltSkillData GetUltSkillDataByUltSkillID(int UltSkillID)
                {
                    if (_UltSkillDatas.ContainsKey(UltSkillID))
                    {
                        return _UltSkillDatas[UltSkillID];
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
  Configs_UltSkillData cd = new Configs_UltSkillData(); 
  int key = Util.ParseToInt(element.Key); 
  cd.UltSkillID = key; 
  cd.SkillName =  Util.GetStringKeyValue(body,"SkillName"); 
  cd.DoubleHitNum =  Util.GetIntKeyValue(body,"DoubleHitNum"); 
  cd.Range1 =  Util.GetStringKeyValue(body,"Range1"); 
  cd.Range2 =  Util.GetStringKeyValue(body,"Range2"); 
  cd.Range3 =  Util.GetStringKeyValue(body,"Range3"); 
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
  cd.Effect2_1 =  Util.GetStringKeyValue(body,"Effect2_1"); 
  cd.FormulaType2_1 =  Util.GetIntKeyValue(body,"FormulaType2_1"); 
  cd.Coefficient2_1 =  Util.GetFloatKeyValue(body,"Coefficient2_1"); 
  cd.CD2_1 =  Util.GetIntKeyValue(body,"CD2_1"); 
  cd.BaseValue2_1 =  Util.GetFloatKeyValue(body,"BaseValue2_1"); 
  cd.UpValue2_1 =  Util.GetFloatKeyValue(body,"UpValue2_1"); 
  cd.Effect2_2 =  Util.GetStringKeyValue(body,"Effect2_2"); 
  cd.FormulaType2_2 =  Util.GetIntKeyValue(body,"FormulaType2_2"); 
  cd.Coefficient2_2 =  Util.GetFloatKeyValue(body,"Coefficient2_2"); 
  cd.CD2_2 =  Util.GetIntKeyValue(body,"CD2_2"); 
  cd.BaseValue2_2 =  Util.GetFloatKeyValue(body,"BaseValue2_2"); 
  cd.UpValue2_2 =  Util.GetFloatKeyValue(body,"UpValue2_2"); 
  cd.Effect3_1 =  Util.GetStringKeyValue(body,"Effect3_1"); 
  cd.FormulaType3_1 =  Util.GetIntKeyValue(body,"FormulaType3_1"); 
  cd.Coefficient3_1 =  Util.GetFloatKeyValue(body,"Coefficient3_1"); 
  cd.CD3_1 =  Util.GetIntKeyValue(body,"CD3_1"); 
  cd.BaseValue3_1 =  Util.GetFloatKeyValue(body,"BaseValue3_1"); 
  cd.UpValue3_1 =  Util.GetFloatKeyValue(body,"UpValue3_1"); 
  cd.Effect3_2 =  Util.GetStringKeyValue(body,"Effect3_2"); 
  cd.FormulaType3_2 =  Util.GetIntKeyValue(body,"FormulaType3_2"); 
  cd.Coefficient3_2 =  Util.GetFloatKeyValue(body,"Coefficient3_2"); 
  cd.CD3_2 =  Util.GetIntKeyValue(body,"CD3_2"); 
  cd.BaseValue3_2 =  Util.GetFloatKeyValue(body,"BaseValue3_2"); 
  cd.UpValue3_2 =  Util.GetFloatKeyValue(body,"UpValue3_2"); 
  cd.ValueDes =  Util.GetStringKeyValue(body,"ValueDes"); 
  cd.TextDes =  Util.GetStringKeyValue(body,"TextDes"); 
  cd.Icon84 =  Util.GetStringKeyValue(body,"Icon84"); 
  cd.Position1 =  Util.GetIntKeyValue(body,"Position1"); 
  cd.Expression1 =  Util.GetStringKeyValue(body,"Expression1"); 
  cd.Position2 =  Util.GetIntKeyValue(body,"Position2"); 
  cd.Expression2 =  Util.GetStringKeyValue(body,"Expression2"); 
  
 if (mUltSkillDatas.ContainsKey(key) == false)
 mUltSkillDatas.Add(key, cd);
  }
 //Debug.Log(mUltSkillDatas.Count);
}

            }