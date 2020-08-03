using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public class Language
{
    static Dictionary<string, string> dis = new Dictionary<string, string>();

    public static void LoadConfig(string msg)                                                               // 加载配置                 
    {
        JsonObject data = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(msg);
        foreach (KeyValuePair<string,object> element in data)
        {
            JsonObject body = (JsonObject)element.Value;
            string key = element.Key;
            string value = Util.GetStringKeyValue(body, "value");
            if (dis.ContainsKey(key) == false)
            {
                dis.Add(key, value);
            }
            else
            {
                dis[key] = value;
            }
               
        }
    }
    public static string GetValue(string key)
    {
        return dis.ContainsKey(key) ? dis[key].ToString() : key;
    }
    public static string GetValue(string key,object temp)
    {
        string value = GetValue(key);
        return string.Format(value, temp);
    }
    public static string GetValue(string key,object temp1,object temp2)
    {
        string value = GetValue(key);
        return string.Format(value, temp1, temp2);
    }
    public static string GetValue(string key,object temp1,object temp2,object temp3)
    {
        string value = GetValue(key);
        return string.Format(value, temp1, temp2, temp3);
    }
    public static string GetValue(string key,object temp1,object temp2,object temp3,object temp4)
    {
        string value = GetValue(key);
        return string.Format(value, temp1, temp2, temp3, temp4);
    }

}

