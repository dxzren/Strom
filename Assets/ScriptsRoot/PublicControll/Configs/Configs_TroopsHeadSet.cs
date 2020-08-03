using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;


public partial class Configs_TroopsHeadSetData           //（战队头像）客户端配置结构体
{
    public int troopsID { set; get; }       //指向战队头像ID--主键
    public int type { set; get; }           //类别
    public string head70 { set; get; }      //头像70
    public string head84 { set; get; }      //头像84
}

public partial class Configs_TroopsHeadSet               //（战队头像）客户端数据集合
{
    static Configs_TroopsHeadSet _sInstance;                                                                             //创建（战队头像）集合实例
    public static Configs_TroopsHeadSet sInstance
    {
        get 
        {
            if (_sInstance == null) _sInstance = new Configs_TroopsHeadSet();
            return _sInstance;
        }
    }

    Dictionary<int, Configs_TroopsHeadSetData> _TroopsHeadSetData = new Dictionary<int, Configs_TroopsHeadSetData>();    //（战队头像）字典集合
    public Dictionary<int, Configs_TroopsHeadSetData> mTroopsHeadSetData { get { return _TroopsHeadSetData; } }

    public Configs_TroopsHeadSetData GetTroopsHeadSetDataByTroopsID(int troopsID)                                        //根据战队头像（troopsID）读取相对应的配置
    {
        if (_TroopsHeadSetData.ContainsKey(troopsID))
            return _TroopsHeadSetData[troopsID];
        return null;
    }
    
    public void InitConfig(string configData)                                                                            //初始化配置信息
    {
        JsonObject data = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(configData);
        foreach (KeyValuePair<string ,object>element in data)
        {
            JsonObject body = (JsonObject)element.Value;
            Configs_TroopsHeadSetData cd = new Configs_TroopsHeadSetData();
            int key = Util.ParseToInt(element.Key);
            cd.troopsID = key;
            cd.head70 = Util.GetStringKeyValue(body, "head70");
            cd.head84 = Util.GetStringKeyValue(body, "head84");
            cd.type = Util.GetIntKeyValue(body, "type");
            if (mTroopsHeadSetData.ContainsKey(key) == false)
                mTroopsHeadSetData.Add(key, cd);
        }
    }
}

