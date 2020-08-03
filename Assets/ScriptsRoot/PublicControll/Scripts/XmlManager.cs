using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public delegate void XmlLoadCallback(string configName, bool isError);

public class XMLManager
{
    static XMLManager _sInstance;
    public static XMLManager sInstance 
    {
        get
        {
            if(_sInstance == null)
            {
                _sInstance = new XMLManager();
            }
            return _sInstance;
        }
    }

    static Dictionary<string, XmlDocument> XmlDocDic = new Dictionary<string, XmlDocument>();

    public XmlCreateEl SoundSaveXml()                                                           // 音效管理                         
    {
        XmlCreateEl XmlFile = new XmlCreateEl();
        XmlFile.path = Application.persistentDataPath + "/" + "SoundM.xml";
        XmlFile.rootNode.Add("MusicStop", "1");      // 0:停止 1:播放
        XmlFile.rootNode.Add("SoundStop", "1");      // 0:停止 1:播放
        return XmlFile;
    }
    public XmlCreateEl NewNumSaveXml(string theCharNmae)                                        // 当前帐号登陆次数                 
    {
        XmlCreateEl XmlFile = new XmlCreateEl();
        XmlFile.path = Application.persistentDataPath + " /" + theCharNmae + "logInNum.xml";
        XmlFile.rootNode.Add("Num", "0");            // 点击过的新活动ID
        XmlFile.rootNode.Add("Year", "0");           // 点击过的新活动ID
        XmlFile.rootNode.Add("Month", "0");          // 点击过的新活动ID
        XmlFile.rootNode.Add("Day", "0");            // 点击过的新活动ID
        return XmlFile;
    }
    public XmlCreateEl NewActiveSaveXml(string theCharName)                                     // 新活动点击记录                   
    {
        XmlCreateEl XmlFile = new XmlCreateEl();
        XmlFile.path = Application.persistentDataPath + "/" + theCharName + ".xml";
        XmlFile.rootNode.Add("ClickedNewActiveID", "0");   
        return XmlFile;
    }
    public XmlCreateEl WingClickSaveXml(string theCharName)                                     // 翅膀点击记录                     
    {
        XmlCreateEl XmlFile = new XmlCreateEl();
        XmlFile.path = Application.persistentDataPath + "/" + theCharName + "(WingClick).xml";
        XmlFile.rootNode.Add("Clicked", "0");        // 点击过的新活动ID
        return XmlFile;
    }

    public void SetXmlValue(string path,string IDValue,string elementName,string elementVlalue) // 设置指定ID指定属性                
    {
        XmlDocument NewXml = GetXml(path);
        if(NewXml == null)
        {
            if(File.Exists(path))
            {
                NewXml = new XmlDocument();
                NewXml.Load(path);
                AddIntoDic(path, NewXml);
            }
            else
            {
                Debug.Log("指定xml文件不存在." + path);
                return;
            }
        }
    }
    public void SetXmlValue(string path, string IDValue, Dictionary<string, string> xmlDic)     // 设置指定ID指定属性                
    {
        XmlDocument NewXml = GetXml(path);
        if(NewXml == null)
        {
            if(File.Exists(path))
            {
                NewXml = new XmlDocument();
                NewXml.Load(path);
                AddIntoDic(path, NewXml);
            }
            else
            {
                Debug.Log("指定XML文件不存在" + path);
                return;
            }
        }
    }
    public void DeleteXml(string path)                                                          // 删除 XML文件                     
    {
        if(File.Exists(path))
        {
            if(XmlDocDic.ContainsKey(path))
            {
                XmlDocDic.Remove(path);
            }
            File.Delete(path);
        }
    }

    public bool CreateXml(XmlCreateEl var)                                                      // 创建XML文件                      
    {
        if (!File.Exists(var.path))
        {
            XmlDocument NEWXml = new XmlDocument();
            XmlElement root = NEWXml.CreateElement("root");
            NEWXml.AppendChild(root);
            XmlElement element = NEWXml.CreateElement("element");

            element.SetAttribute("Id", "1");
            foreach (string elementName in var.rootNode.Keys)
            {
                element.SetAttribute(elementName, var.rootNode[elementName]);
            }
            root.AppendChild(element);
            NEWXml.Save(var.path);
            AddIntoDic(var.path, NEWXml);
            Debug.Log("创建XML文件:" + var.path);
            return true;
        }
        else
        {
            Debug.Log(var.path + "文件已存在,不可创建");
            return false;
        }

    }
    public bool AddXmlElement(XmlCreateEl var)                                                  // 添加一条元素                      
    {
        XmlDocument NewXml = GetXml(var.path);
        if (NewXml == null)
        {
            if (File.Exists(var.path))
            {
                NewXml = new XmlDocument();
                NewXml.Load(var.path);
                AddIntoDic(var.path, NewXml);
            }
            else
            {
                Debug.Log("指定XML文件不存在." + var.path);
                return false;
            }
        }

        XmlNode root = NewXml.SelectSingleNode("root");
        if(root == null)
        {
            Debug.Log("未找到" + "root" + "根节点" + " -" + var.path);
            return false;
        }

        XmlNodeList NodeList = root.SelectNodes("element");
        int theCountElement = NodeList.Count;
        XmlElement Element = NewXml.CreateElement("element");
        Element.SetAttribute("Id", "" + (theCountElement + 1));

        foreach(string elementName in var.rootNode.Keys)
        {
            Element.SetAttribute(elementName, var.rootNode[elementName]);
        }
        root.AppendChild(Element);
        NewXml.Save(var.path);
        return true;
    }
    public bool DeleteXmlElement(string path,string IDValue)                                    // 删除指定ID元素                    
    {
        XmlDocument NewXml = GetXml(path);
        if(NewXml == null)
        {
            if(File.Exists(path))
            {
                NewXml = new XmlDocument();
                NewXml.Load(path);
                AddIntoDic(path, NewXml);
            }
            else
            {
                Debug.Log("指定xml文件不存在." + path);
                return false;
            }
        }

        XmlNode root = NewXml.SelectSingleNode("root");
        if(root == null)
        {
            Debug.Log("未找到" + "root" + "根节点" + " -" + path);
            return false;
        }

        XmlNodeList nodeList = root.SelectNodes("element");
        for(int i = 0;i < nodeList.Count;i++)
        {
            if(nodeList[i].Attributes["Id"].Value.CompareTo(IDValue) == 0)
            {
                XmlNode theNode = nodeList[i];
                root.RemoveChild(theNode);
                for(int j = 0;j < nodeList.Count;j++)
                {
                    nodeList[j].Attributes["ID"].Value = "" + j + 1;
                }
                NewXml.Save(path);
                break;
            }
        }
        return true;
    }

    public Dictionary<string, Dictionary<string, string>> ReadXml(XmlCreateEl var)// string: id属性值  Dictionary<string, string>:元素属性名 ，元素属性值
    {

        Dictionary<string, Dictionary<string, string>> theArray = new Dictionary<string, Dictionary<string, string>>();
        XmlDocument xml = GetXml(var.path);
        if (xml == null)
        {
            if (File.Exists(var.path))
            {
                xml = new XmlDocument();
                xml.Load(var.path);
                AddIntoDic(var.path, xml);
            }
            else
            {
                Debug.Log("指定xml文件不存在。" + var.path);
                return null;
            }
        }

        XmlNode root = xml.SelectSingleNode("root");
        if (root == null)
        {
            Debug.Log("未找到" + "root" + "根节点" + " -" + var.path);
            return null;
        }
        XmlNodeList nodeList = root.SelectNodes("element");
        if (nodeList == null || nodeList.Count <= 0)
        {
            Debug.Log("未找到" + "element" + "元素节点" + " -" + var.path);
            return null;
        }
        for (int j = 0; j < nodeList.Count; j++)
        {
            Dictionary<string, string> rootNode = new Dictionary<string, string>();
            string keys;
            if (nodeList[j].Attributes["Id"] != null)
            {
                keys = nodeList[j].Attributes["Id"].Value;
                foreach (string elementname in var.rootNode.Keys)
                {
                    if (nodeList[j].Attributes[elementname] == null)
                    {
                        Debug.Log("指定属性不存在" + elementname);
                    }
                    else
                    {
                        string thevalue = nodeList[j].Attributes[elementname].Value;
                        rootNode.Add(elementname, thevalue);
                    }
                }
                theArray.Add(keys, rootNode);
            }
        }
        if (theArray.Count > 0)
        {
            return theArray;
        }
        else
        {
            Debug.Log("数据为0，未读到指定数据");
            return null;
        }
    }

    public IEnumerable GetXml_x(string name, XmlLoadCallback callback)                          // 加载xml下的 指定文件              
    {
        string msg = "";
        ResourceRequest obj = Resources.LoadAsync<TextAsset>("Xml/" + name);
        yield return obj;

        TextAsset ta = (TextAsset)obj.asset;
        msg = ta.text;
        if (string.IsNullOrEmpty(msg) == false)
        {
            XmlDocument NewXml = new XmlDocument();
            NewXml.LoadXml(msg);
            AddIntoDic("Xml/" + name, NewXml);
            callback("Xml/" + name, false);
        }
        else
        {
            callback("Xml/" + name, true);
        }
  }

    void AddIntoDic(string path,XmlDocument xmlDoc)                                             // 添加进字典                        
    {
        if(XmlDocDic.ContainsKey(path))
        {
            if(XmlDocDic[path] == null)
            {
                XmlDocDic[path] = xmlDoc;               // XmlDocDic.Add(path,xml);
            }
        }
        else
        {
            XmlDocDic.Add(path, xmlDoc);
        }
    }
    XmlDocument GetXml(string path)                                                             // 获取XML                          
    {
        if(XmlDocDic.ContainsKey(path))
        {
            return XmlDocDic[path];
        }
        else
        {
            return null;
        }
    }
}

public class XmlCreateEl                                                    // XmlCreateNode.elementName = XmlCreateNone.value      
{
    public string path;                                 // "xx/name"
                                                        // root下的一行包含的元素属性及值 string1: 属性名 string2: 属性值
    public Dictionary<string, string> rootNode = new Dictionary<string, string>();
}