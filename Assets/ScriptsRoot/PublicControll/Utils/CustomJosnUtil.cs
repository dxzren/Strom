using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

/// <summary> JSon对象中取值 </summary>
public class CustomJsonUtil
{
    public static Dictionary<string, string> dis               = new Dictionary<string, string>();

    public static void          LoadConfig(string msg)                                          // 读取配置信息     
    {
        JsonObject              data                    = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(msg);
        foreach (KeyValuePair<string,object> element in data)
        {
            JsonObject          body                    = (JsonObject)element.Value;

            string              key                     = element.Key.ToString();
            string              value                   = Util.GetStringKeyValue(body, "value");

            if (dis.ContainsKey(key) == false)
            { dis.Add           (key, value);                           }
            else
            { dis[key]          = value;                                }
        }

    }

    public static string        GetValue (string key)                                           // 字典取值 <String>          
    {
        return                  dis.ContainsKey(key) ? dis[key].ToString() : key;
    }

    public static int           GetValueToInt (string key)                                      // 字典取值 <Toint>           
    {
        string                  value                   = GetValue(key);
        int                     temp                    = 0;
        Int32.TryParse          (value, out temp);
        return                  temp;
    }

    public static float         GetValueToFloat(string key)                                     // 字典取值 <Tofloat>         
    {
        string                  value                   = GetValue(key);
        float                   temp                    = 0.0f;
        float.TryParse          (value, out temp);
        return                  temp;

    } 
}
