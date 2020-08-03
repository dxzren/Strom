/* 
*CodeBuilder自动生成代码 
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using System;

/// <summary>
/// (被动技能)客户端配置结构体
/// </summary>
public partial class Configs_PassiveSkillData
{
    /// <summary>
    /// 被动技能ID--主键
    /// </summary>
    public int PassiveSkillID { get; set; }


    /// <summary>
    /// 效果类型
    /// </summary>
    public int EffectType { get; set; }
    /// <summary>
    /// 范围类型
    /// </summary>
    public int RangeType { get; set; }
    /// <summary>
    /// 作用范围
    /// </summary>
    public int EffectRange { get; set; }
    /// <summary>
    /// 基础值
    /// </summary>
    public float BaseValue { get; set; }
    /// <summary>
    /// 成长值
    /// </summary>
    public float UpValue { get; set; }
    /// <summary>
    /// 数值描述
    /// </summary>
    public string ValueDes { get; set; }
    /// <summary>
    /// 文字描述
    /// </summary>
    public string TextDes { get; set; }
    /// <summary>
    /// 图标84
    /// </summary>
    public string Icon84 { get; set; }
    /// <summary>
    /// 技能名
    /// </summary>
    public string SkillName { get; set; }
    /// <summary>
    /// 特效名称
    /// </summary>
    public string SpecialEffects { get; set; }
    /// <summary>
    /// 被动技能百分比
    /// </summary>
    public float PerValue { get; set; }

  
}
/// <summary>
/// (被动技能)客户端配置数据集合类
/// </summary>
public partial class Configs_PassiveSkill
{

    static Configs_PassiveSkill _sInstance;
    public static Configs_PassiveSkill sInstance
    {
        get
        {
            if (_sInstance == null) _sInstance = new Configs_PassiveSkill();
            return _sInstance;
        }
    }

    /// <summary>
    /// (被动技能)字典集合
    /// </summary>
    public Dictionary<int, Configs_PassiveSkillData> mPassiveSkillDatas
    {
        get { return _PassiveSkillDatas; }
    }

    /// <summary>
    /// (被动技能)字典集合
    /// </summary>
    Dictionary<int, Configs_PassiveSkillData> _PassiveSkillDatas = new Dictionary<int, Configs_PassiveSkillData>();

    /// <summary>
    /// 根据PassiveSkillID读取对应的配置信息
    /// </summary>
    /// <param name="PassiveSkillID">配置的PassiveSkillID</param>
    /// <returns>配置类，无则返回null</returns>
    public Configs_PassiveSkillData GetPassiveSkillDataByPassiveSkillID(int PassiveSkillID)
    {
        if (_PassiveSkillDatas.ContainsKey(PassiveSkillID))
        {
            return _PassiveSkillDatas[PassiveSkillID];
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
            Configs_PassiveSkillData cd = new Configs_PassiveSkillData();
            int key = Util.ParseToInt(element.Key);
            cd.PassiveSkillID = key;
            cd.EffectType = Util.GetIntKeyValue(body, "EffectType");
            cd.RangeType = Util.GetIntKeyValue(body, "RangeType");
            cd.EffectRange = Util.GetIntKeyValue(body, "EffectRange");
            cd.BaseValue = Util.GetFloatKeyValue(body, "BaseValue");
            cd.UpValue = Util.GetFloatKeyValue(body, "UpValue");
            cd.ValueDes = Util.GetStringKeyValue(body, "ValueDes");
            cd.TextDes = Util.GetStringKeyValue(body, "TextDes");
            cd.Icon84 = Util.GetStringKeyValue(body, "Icon84");
            cd.SkillName = Util.GetStringKeyValue(body, "SkillName");
            cd.SpecialEffects = Util.GetStringKeyValue(body, "SpecialEffects");
            cd.PerValue = Util.GetFloatKeyValue(body, "PerValue");
            if (mPassiveSkillDatas.ContainsKey(key) == false)
                mPassiveSkillDatas.Add(key, cd);
        }
        //Debug.Log(mPassiveSkillDatas.Count);
    }

}