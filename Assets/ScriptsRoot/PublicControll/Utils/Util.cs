using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WordHelper;                                                                               // 屏蔽字库（Utils/FilterWord.cs）
using LinqTools;
using LitJson;
using SimpleJson;
using strange.extensions.context.impl;
using CodeStage.AntiCheat.ObscuredTypes;

//          |````````````````````````````````````   Public Statement  ````````````````````````````````````````|
//          |_________________________________________________________________________________________________|

public delegate void                ProcessConfigData       ( string configData);               // 配置数据进度 ----- << 同步和异步加载Assert目录下的文本 >>
public delegate void                ReadConfigComplete      ( string configName, bool isError); // 读取配置完成 ----- << 同步和异步加载Assert目录下的文本 >>
public delegate void                StepCallBack            ( string resName);                  // 新手引导进度回调
public delegate bool                GuideEventHandler       ( int    Index);                    // 引导事件处理

public class Util
{
    #region================================================||  游戏登录系统                  ||=====================================================
    public static void              CheckLocalPath()                                            // 检测本地路径，如果不存在就创建目录  
    {
        if (!Directory.Exists(Define.RESOURCES_LOCAL_PATH))                                     /// 创建资源路径         
        {
            Directory.CreateDirectory(Define.RESOURCES_LOCAL_PATH);
        }
        if (!Directory.Exists(Define.GetLocalSavePath()))                                       /// 创建配置文件路径     
        {
            Directory.CreateDirectory(Define.GetLocalSavePath());
        }
    }
    public static List<string>      GameTexts               = new List<string>();               // NGUI(游戏使用的文本集合)
    public static List<string>      CollectText ( string a)                                     // 集合文字                         
    {
        if (!GameTexts.Contains(a))
        {
            GameTexts.Add(a);
        };
        return GameTexts;
    }

    public static UnityEngine.Object Load ( string path)                                        // 加载包中的资源  < Android >       
    {
        UnityEngine.Object      obj                         = null;
        if (path.Trim().EndsWith("/0") || path.Trim().EndsWith("/"))                            /// 检测字符串尾部字符("")为_true
        {
            return null;
        }
        string name = System.IO.Path.GetFileNameWithoutExtension(path);                         /// 从下载的资源查找目标,没有的话就用本地数据将有源路径全部过滤
#if     UNITY_ANDROID && !UNITY_EDITOR && !Force_Local                                          /// ANDROID--非Unity编辑路模式
        if      (path.Contains("RoleModel"))                                                    /// 分隔主角模型 路径
        {
            string[]            a                           = path.Split('/');
            string              model                       = a[a.length - 1];
            obj                 = ResourceManager.Instance.LoadAsset<UnityEngine.Objece>(AssetBundleName.commonhero, model);
        }
        else if (path.Contains("RoleEffect"))                                                   /// 分隔主角特效 路径
        {
            string[]            a                           = path.Split('/');
            string              model                       = a[a.length - 1];
            obj                 = ResourceManager.Instance.LoadAsset<UnityEngine.Objece>(AssetBundleName.effect, model);
        }
        else
        {
#endif
        obj                     = Resources.Load(path);                                         /// 加载包中的资源
#if     UNITY_ANDROID && !UNITY_EDITOR && !Force_Local
        }
#endif
        return                  obj;
    }
    public static ResourceRequest   LoadAsync<T> ( string path ) where T : UnityEngine.Object   // 同步加载类型 加载资源请求          
    {
        ResourceRequest             ResReq                  = null;
        if (path.Trim().EndsWith("/0") || path.Trim().EndsWith("/"))
        {
            return                  ResReq;
        }
        string                      name                    = System.IO.Path.GetFileNameWithoutExtension(path);
        ResReq                                              = Resources.LoadAsync<T>(path);
        return                      ResReq;
    }
    public static void              ReLogIn()                                                   // 重新登录                          
    {
        PanelManager.sInstance.ClearPanels(SceneType.Start);                // 清空 Start  场景数据
        PanelManager.sInstance.ClearPanels(SceneType.Main);                 // 清空 Main   场景数据
        PanelManager.sInstance.ClearPanels(SceneType.Battle);               // 清空 Battle 场景数据

        NetEventDispatcher.Instance().ClearAllCallbacks();                  // 切断所有监听
        GameObject                  obj                     = GameObject.FindWithTag("Respawn");
        MonoBehaviour.DestroyImmediate(obj);                                // 销毁所有数据
        MonoBehaviour.DestroyImmediate(GameObject.FindWithTag("Chat"));     // 销毁消息
        Debug.Log                   ("确认销毁_Respwn_chat");
        SceneController.LoadScene   ("Start");                              // 加载场景
        //StormLengend.Battle.SysTemConfig.ReSetting();
    }
    public static string            RandomRoleName()                                            // 获取随机昵称                      
     {
        int                         nameCount               = 450;
        string                      name                    = "";
        Dictionary<int, Configs_NamesAndMaskWordData> NickNameDataDic = Configs_NamesAndMaskWord.sInstance.mNamesAndMaskWordDatas;

        int                         rangeVlaue              = UnityEngine.Random.Range(1, 100);
        if (rangeVlaue <= 33)                                               // 33% + 形容词            
        {
            bool                    isGoon                  = true;
            while (isGoon)
            {
                int                 index                   = UnityEngine.Random.Range(1, nameCount);
                if (NickNameDataDic.ContainsKey(index))
                {
                    Configs_NamesAndMaskWordData NewNickName = NickNameDataDic[index];
                    if (NewNickName.Adjective != "0")
                    {
                        name += NewNickName.Adjective;
                        isGoon      = false;
                    }
                }
            }
        }
        else if (rangeVlaue > 33 && rangeVlaue <= 66)                       // 34% + 称谓             
        {
            bool                    isGoon                  = true;
            while (isGoon)
            {
                int                 index                   = UnityEngine.Random.Range(1, nameCount);
                if (NickNameDataDic.ContainsKey(index))
                {
                    Configs_NamesAndMaskWordData newNickName = NickNameDataDic[index];
                    if (newNickName.Appellation != "0")
                    {
                        name += newNickName.Appellation;
                        isGoon      = false;
                    }
                }
            }
        }
        else                                                                // 33% 形容词 + 称谓         
        {
            bool isGoon = true;
            while (isGoon)
            {
                int index = UnityEngine.Random.Range(1, nameCount);
                if (NickNameDataDic.ContainsKey(index))
                {
                    Configs_NamesAndMaskWordData NewNickName = NickNameDataDic[index];
                    if (NewNickName.Adjective != "0")
                    {
                        name += NewNickName.Adjective;
                        isGoon = false;
                    }
                }
            }

            bool isGoon1 = true;
            while (isGoon1)
            {
                int index = UnityEngine.Random.Range(1, nameCount);
                if (NickNameDataDic.ContainsKey(index))
                {
                    Configs_NamesAndMaskWordData newNickName = NickNameDataDic[index];
                    if (newNickName.Appellation != "0")
                    {
                        name += newNickName.Appellation;
                        isGoon1 = false;
                    }
                }
            }
        }
        bool isGoon3 = true;
        while(isGoon3)
        {
            int index = UnityEngine.Random.Range(1, nameCount);
            if (NickNameDataDic.ContainsKey(index))
            {
                Configs_NamesAndMaskWordData newNickName = NickNameDataDic[index];
                if (newNickName.Name != "0")
                {
                    name += newNickName.Name;
                    isGoon3 = false;
                }
            }
        }
        return name;    
    }
    #endregion

    #region================================================||  时间设置和管理<Time>          ||=====================================================
    public static string            ChangeTimeType  ( int time)                                 // 转换时间类型 <int> TO <String>                      
    {
        if(time >= 10 || time <0)
        {
            return time.ToString();
        }
        else
        {
            return ("0" + time);
        }
    }
    public static long              GetNowTime()                                                // 获取当前时间< 未加入服务器时间修正>                   
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return (Convert.ToInt64(ts.TotalMilliseconds));
    }
    public static long              GetNowTime      ( IGameData game)                           // 获取当前时间< 服务器同步修正后时间><0000:0:0:0:0:0:0> 
    {
        TimeSpan                    ts                      = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return (Convert.ToInt64(ts.TotalSeconds) + game.subServerT);
    }
    public static int[]             GetTime         ( IGameData game)                           // 获取当前时间< 小时 00:00:00>                         
    {
        System.DateTime nowTime = Util.StampToDateTime(GetNowTime(game));
        int[] time = new int[] { nowTime.Hour, nowTime.Minute, nowTime.Second };
        return time;
    }
    public static string            TimeSub         ( long time,int seconds,IGameData game)     // 倒计时显示 < 小时单位 00:00:00>                      
    {
        DateTime dt1 = Util.StampToDateTime(time + seconds);
        DateTime dt2 = Util.StampToDateTime(GetNowTime(game));
        System.TimeSpan ts = dt1.Subtract(dt2);
        if((ts.Days * 24) + ts.Hours < 0 || ts.Minutes < 0 || ts.Seconds< 0 )
        {
            return "00:00:00";
        }
        return ChangeTimeType((ts.Days * 24) + ts.Hours) + ":" + ChangeTimeType(ts.Minutes) + ":" + ChangeTimeType(ts.Seconds);

    }
    public static string            TimeSub         ( float seconds,IGameData game)             // 倒计时显示 < 分钟单位 00:00>                         
    {
        DateTime dt1 = Util.StampToDateTime(GetNowTime(game) + (int)Math.Floor(seconds));
        DateTime dt2 = Util.StampToDateTime(GetNowTime(game));
        System.TimeSpan ts = dt1.Subtract(dt2);
        if((ts.Days * 24) + ts.Hours < 0 || ts.Minutes < 0 || ts.Seconds < 0)
        {
            return "00:00";
        }
        string tsM = ts.Minutes.ToString().Length == 1 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString();
        string tsS = ts.Seconds.ToString().Length == 1 ? "0" + ts.Seconds.ToString() : ts.Seconds.ToString();
        return tsM + ":" + tsS;
    }
    public static long              DateTimeToStamp ( System.DateTime dateTime)                 // 将<DateTime>格式改为Unix时间戳                       
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (long)(dateTime - startTime).TotalSeconds;
    }
    public static DateTime          StampToDateTime ( long timeStamp)                           // 将Unix时间戳改为<DateTime>格式                       
    {
        DateTime                    dateTimeStart                   = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long                        lTime                           = long.Parse(timeStamp + "0000000");
        TimeSpan                    toNow                           = new TimeSpan(lTime);
        return                      dateTimeStart.Add(toNow);
    }

    public static float             GetTaskTime     ( IGameData game)                           // (任务系统定制) 获取当前时间                          
    {
        int[]                       time                            = GetTime(game);
        float                       taskT                           = time[0];
        return                      taskT;
    }
    public static float             GetTaskTime1    ( IGameData game)                           // (商人系统定制) 获取当前时间                          
    {
        int[]                       time                            = GetTime(game);
        float                       taskT                           = time[0];
        if(time[1] != 0)
        {
            taskT += 0.0f * time[1];
        }
        if(time[2] != 0)
        {
            taskT += 0.0001f * time[2];
        }
        return taskT;
    }

    #endregion

    #region================================================||  获取数据加载路径(不同平台)     ||=====================================================
    public static string            GetStreamAssetsDataPath()                                   // 获取流资源数据路径                                   
    {
        string path = "";
        
        if(Application.platform == RuntimePlatform.Android)
        {
            path = string.Format("file://{0}", Application.streamingAssetsPath);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer )
        {
            path = string.Format("file://{0}", Application.streamingAssetsPath);
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = string.Format("file:///{0}", Application.streamingAssetsPath);
        }
        else if(Application.platform  == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            path = string.Format("file://{0}", Application.streamingAssetsPath);
        }
        return path;
    }
    public static string            GetPersistentDataPath()                                     // 获取持久的资源数据路径                               
    {
        string path = "";

        if(Application.platform == RuntimePlatform.Android)
        {
            path = string.Format("file://{0}", Application.persistentDataPath);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = string.Format("file://{0}", Application.persistentDataPath);
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = string.Format("file:///{0}", Application.persistentDataPath);
        }
        else if(Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            path = string.Format("file://{0}", Application.persistentDataPath);
        }
        return path;
                
    }
    public static string            GetPersistentDataPath(string relativePath)                  // 关联的资源数据路径                                   
    {
        string path = "";

        if(Application.platform == RuntimePlatform.Android)
        {
            path = string.Format("file://{0}", relativePath);
        }
        else if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = string.Format("file://{0}", relativePath);
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor ||Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = string.Format("file:///{0}", relativePath);
        }
        else if(Application.platform == RuntimePlatform.OSXEditor ||Application.platform == RuntimePlatform.OSXPlayer)
        {
            path = string.Format("file://{0}", relativePath);
        }
        return path;
    }
    #endregion

    #region================================================||  文件的加载和保存处理           ||=====================================================
    #region-------------------------------------------------<  同步和异步加载Assert目录下的文本  >-------------------------------------------
    static public TextAsset         LoadLocalText     ( string  path,ProcessConfigData configData )                         // 同步加载Asset目录下的文本      
    {
        string                      msg                     = "";
        TextAsset                   obj                     = Resources.Load<TextAsset>("Config/" + path);
        msg                                                 = obj.text;
        if (string.IsNullOrEmpty(msg) == false)
        {
            configData              (msg);
        }
        return                      obj;
    }                                                                                    
    static public IEnumerator       AsyncLoadLocalText( Dictionary<string,ProcessConfigData> configDatas,                   // 异步加载Asset目录下的文本
                                                        ReadConfigComplete callBack = null )                  
    {

        string msg = "";
        AssetBundle ab = ResourceManager.Instance.GetAssetBundle(AssetBundleName.config);

        foreach (var item in configDatas)
        {
            AssetBundleRequest ABR = ab.LoadAssetAsync(item.Key, typeof(TextAsset));
            yield return ABR;
            TextAsset ta = (TextAsset)ABR.asset;

            if(ta == null)
            {
                if (callBack != null) callBack(item.Key, true);
                continue;
            }

       //-----------------------<< 加载Asset目录下的文本 >>----------------------------------------------------------------
            msg = ta.text;
            if(string.IsNullOrEmpty(msg)==false)
            {
                item.Value(msg);
                if (callBack != null) callBack(item.Key, false);
            }
            else
            {
                if (callBack != null) callBack(item.Key, true);
            }
        }
    }
    static public IEnumerator       AsyncLoadLocalText( string path, ProcessConfigData configData,                          // 异步加载本地文本
                                                        ReadConfigComplete callBack = null )           
    {

        string msg = "";
        ResourceRequest obj = Resources.LoadAsync<TextAsset>("Config/" + path);
        yield return obj;
        //加载Asset目录下的文本
        TextAsset ta = (TextAsset)obj.asset;
        if (ta != null)
        {
//            Debug.Log("ConfigsText != null");
            msg = ta.text;
        }

        if (string.IsNullOrEmpty(msg) == false)
        {
            configData(msg);
            if (callBack != null) callBack(path, false);
        }
        else
        {
            if (callBack != null) callBack(path, true);
        }
    }
    static public IEnumerator       AsyncLoadFileSystemText (   string path,ProcessConfigData configData,                   // 异步加载本地文件系统中的文本
                                                                ReadConfigComplete callBack = null)       
    {
        string msg = "";
        string root = GetPersistentDataPath(Define.RESOURCES_LOCAL_PATH);
        path = root + path;

        WWW www = new WWW(path);
        yield return www;

        if(www.error == null)
        {
            msg = www.text;
            Debuger.Log("Load LocalSystem Diretory" + path + " OK!");
        }
        else
        {
            Debuger.Log("Load LocalSystem Diretory Faild!" + "LoadSpecial(" + path + ") is error!" + www.error);
        }
        if(string.IsNullOrEmpty(msg) == false)
        {
            configData(msg);
            callBack(path, false);
        }
        else
        {
            callBack(path, true);
        }
    }
    #endregion

    public static void              SaveFile    (System.Object obj,string fileName)                                         // 保存文件                                                                   
    {
        string                      txt                         = JsonMapper.ToJson(obj);
        byte[]                      byteArray                   = System.Text.Encoding.UTF8.GetBytes(txt);
        File.WriteAllBytes          (Define.GetLocalSavePath() + fileName + ".cif", byteArray);
        Debuger.Log                 ("SaveFile Succeed: " + Define.GetLocalSavePath() + fileName + ".cif");
    } 
    public static List<string>      LoadFile    (string path,string name)                                                   // (读取文件)使用流形式读取      
    {
        StreamReader                StreamRead              = null;
        try
        {
            StreamRead                                      = File.OpenText(path + "//" + name);
        }
        catch(Exception e)                                                                      /// 路劲与名称为找到文件则直接返回空
        {
            Debuger.LogError        (e.Message);
            return null;
        }
        string                      line;
        List<string>                arrList                 = new List<string>();          
        while((line = StreamRead.ReadLine())!= null)                                            /// 一行一行读取
        {
            arrList.Add(line);
        }
        StreamRead.Close();                                                                     /// 关闭文件流
        StreamRead.Dispose();                                                                   /// 摧毁流
        return                      arrList;                                                    /// 返回字符串列表
    }
    public static void              CreateFile  (string path,string name,string[] info)                                     // 创建文件                     
    {
        try
        {
            StreamWriter WriterStm;
            FileInfo fileT = new FileInfo(path + "//" + name);
            if(!fileT.Exists)
            {
                WriterStm = fileT.CreateText();                                                 // 不存在就创建
            }
            else
            {
                WriterStm = fileT.AppendText();                                                 // 存在就打开
            }
            for(int i = 0; i< info.Length;i++)
            {
                WriterStm.WriteLine(info[i]);
            }
            WriterStm.Close();
            WriterStm.Dispose();
        }
        catch
        {
            Debuger.Log(" create file is error ");
        }
    }
    public static void              DeleteFile  (string path,string name)                                                   // 删除文件                     
    {
        File.Delete(path + "//" + name);
    }

    public static ServerInfo        GetIPFile   (string fileName)                                                           // 获取登录服务器信息数据        
    {
        if(File.Exists(Define.GetLocalSavePath() + fileName + ".cif"))
        {
            string                  txt             = "";
            ServerInfo              SrvName         = new ServerInfo();
            txt                                     = File.ReadAllText(Define.GetLocalSavePath() + fileName + ".cif", System.Text.Encoding.UTF8);
            try
            {
                JsonData            JD              = JsonMapper.ToObject(txt);
                SrvName.IP                          = (string)JD["IP"];
                SrvName.serverName                  = (string)JD["serverName"];
                SrvName.gameServerID                = (int)JD   ["gameServerID"];
                SrvName.centerServerID              = (int)JD   ["centerServerID"];

                SrvName.port                        = (int)JD   ["port"];
                return              SrvName;
            }
            catch(System.Exception e)
            {
                Debuger.Log(e.ToString());
                Debuger.Log("本地文件读取异常");
                return null;
            }
        }
        else
        {
            return null;
        }
    }
    public static SavedAccountInfos GetAccFile  (string fileName)                                                           // 获取本地保存的帐号信息文件    
    {
        SavedAccountInfos           NewAccInfo              = new SavedAccountInfos();
        if(File.Exists              (Define.GetLocalSavePath() + fileName + ".cif"))
        {
            string                  Thetxt = "";

            Thetxt                  = File.ReadAllText(Define.GetLocalSavePath() + fileName + ".cif", System.Text.Encoding.UTF8);
            try
            {
                JsonData            TempJsonData            = JsonMapper.ToObject(Thetxt);
                NewAccInfo.Account                          = (string)TempJsonData["Account"];
                NewAccInfo.Password                         = (string)TempJsonData["Password"];
                return              NewAccInfo;
            }
            catch(System.Exception e)
            {
                Debug.Log           (e.ToString());
                Debug.Log           ("本地文件读取异常");
                return              NewAccInfo;
            }
        }
        else
        {   return                  NewAccInfo;      }
    }
    public static List<int>         GetLineUpIDFile     (string inFileName)                                                 // 获取阵容英雄ID列表  文件      
    {
        if(File.Exists (Define.GetLocalSavePath() + inFileName + ".cif"))
        {
            List<int>               IDList                          = new List<int>();
            string                  txt                             = "";
            txt                     = File.ReadAllText(Define.GetLocalSavePath() + inFileName + ".cif", System.Text.Encoding.UTF8);
            try
            {
                JsonData            JD                              = JsonMapper.ToObject(txt);
                for (int i = 0; i < JD.Count; i++)
                {
                    IDList.Add((int)(int)JD[i]);
                }
                return IDList;
            } 
            catch(System.Exception e)
            {
                Debuger.Log(e.ToString());
                Debuger.Log("本地阵容文件读取异常");
                return null;
            }
        }
        else
        {
            Debuger.LogError("本地阵容英雄ID列表文件读取失败");
            return null;
        }
    }
    public static List<int>         GetLineUpPosFile    (string inFileName)                                                 // 获取阵容位置Pos列表 文件      
    {
        if(File.Exists ( Define.GetLocalSavePath() + inFileName + ".cif"))
        {
            List<int>               PosList                         = new List<int>();
            string                  TheStr                          = "";
            TheStr                  = File.ReadAllText( Define.GetLocalSavePath() + inFileName + ".cif",System.Text.Encoding.UTF8);
            try
            {
                JsonData            JD                              = JsonMapper.ToObject(TheStr);
                for(int i = 0; i < JD.Count; i++)
                {
                    PosList.Add((int)JD[i]);
                }
                return              PosList;
            }
            catch(Exception e)
            {
                Debuger.Log(e.ToString());
                Debuger.LogError    ("本地阵容文件读取异常");
                return null;
            }
        }
        else                                                                                                                // 读取错误
        {
            Debuger.LogError        ("本地阵容Pos列表文件读取失败");
            return null;
        }
    }

    public static UIAtlas           LoadAtlas   ( string path)                                                              // 加载图集                     
    {
        return Resources.Load(path, typeof(UIAtlas)) as UIAtlas;
    }
    public static GameObject        LoadRes     ( Configs_ActionAndEffectData actEffctData,                                 // 加载英雄模型
                                                  float scale = 1, bool isHeroSysShow = false )                                                            
    {
        string                      ResPath                             = "Prefabs/RoleModel/" + actEffctData.HeroModel;                        /// 模型资源加载路径
        UnityEngine.Object          TempObj                             = Util.Load ( ResPath );                                                /// 加载模型对象资源
        GameObject                  TempHeroObj                         = MonoBehaviour.Instantiate(TempObj) as GameObject;                     /// 模型对象实例
        
        if ( TempHeroObj != null)   TempHeroObj.name                    = actEffctData.HeroModel;                                               /// 设置模型名称
        TempObj                     = null;

        float                       UIScale                             = GameObject.Find ("UI Root").transform.localScale.x;                   /// UI Root 缩放比例
        if ( !isHeroSysShow )       TempHeroObj.transform.localScale    = new Vector3 ( scale * actEffctData.ModelAdjust * UIScale,             /// 非英雄界面   模型调整比例
                                                                                        scale * actEffctData.ModelAdjust * UIScale,
                                                                                        scale * actEffctData.ModelAdjust * UIScale      );
        else                        TempHeroObj.transform.localScale    = new Vector3 ( scale * actEffctData.HeroModelAdjust * UIScale,         /// 英雄系统界面  模型调整比例
                                                                                        scale * actEffctData.HeroModelAdjust * UIScale,
                                                                                        scale * actEffctData.HeroModelAdjust * UIScale  );
        return                      TempHeroObj;
    }
    #endregion

    #region================================================||  数据类型转换 + Json数据 + 数据加密  ||=====================================================
                                                                                                                            /// <| 数据类型转换 DataType |>
    public static T                 PhaseTo<T>      ( object obj)                                                                                              
    {
        T result = default(T);
        try
        {
            result = (T)obj;
        }
        catch (Exception e)
        {
            Debuger.Log("Phase Failed! obj = " + obj.ToString());
        }
        return result;
    }
    public static int               ParseToInt      ( string str)                               // 将字符串<string>强行转换成< int>,  转换失败则返回0.            
    {
        int returnInt = 0;
        if(str.Trim().Length < 1)
        {
            return returnInt;
        }
        if(int.TryParse(str,out returnInt))
        {
            return returnInt;
        }
        else
        {
            Debuger.LogWarning("string数据" + str + "转换int失败,返回默认值0");
            return 0;
        }
    }
    public static long              ParseToLong     ( string str)                               // 将字符串<string>强行转换成< long>, 转换失败则返回0.            
    {
        long returnLong = 0;
        if(str.Trim().Length < 1)
        {
            return returnLong;
        }
        if(long.TryParse(str,out returnLong))
        {
            return returnLong;
        }
        else
        {
            Debuger.LogWarning("string数据" + str + "转换long失败,返回默认值0");
            return 0;
        }
    }
    public static float             ParseToFloat    ( string str)                               // 将字符串<string>强行转换成< float>,转换失败则返回0.            
    {
        float returnFloat = 0;
        if(str.Trim().Length < 1)
        {
            return returnFloat;
        }
        if(float.TryParse(str,out returnFloat))
        {
            return returnFloat;
        }
        else
        {
            Debuger.LogWarning("string数据" + str + "转换float失败,返回默认值0");
            return 0;
        }
    }

    public static bool              ParseToBool     ( string str)                               // 将字符串<string>强行转换成< bool>,转换失败则返回false.         
    {
        bool returnBool = false;
        if(str.Trim().Length < 1)
        {
            return returnBool;
        }
        if(bool.TryParse(str,out returnBool))
        {
            return returnBool;
        }
        else
        {
            Debuger.LogWarning("string数据" + str + "转换DataTime失败,返回默认值false");
            return false;
        }
    }
    public static DateTime          ParseToDateTime ( string str)                               // 将字符串<string>强行转换成< DateTime>,转换失败则返回DateTime.MinValue  
    {
        DateTime returnDataTime = DateTime.MinValue;
        if (str.Trim().Length < 1)
        {
            return returnDataTime;
        }
        if (DateTime.TryParse(str, out returnDataTime))
        {
            return returnDataTime;
        }
        else
        {
            Debuger.LogWarning("string数据" + str + "转换DataTime失败,返回默认值" + returnDataTime);
            return returnDataTime;
        }
    }
    public static object            BytesToStruct   ( byte[] bytes,int length,Type type)        // 字节数组转换成结构体 ( bytes:字节内容 length:字节长度 type:对象类型 )   
    {
        object reValue;
        IntPtr buffer = Marshal.AllocHGlobal(length);                       // 创建对象指针 ( 从进程中非托管内存中分配内存 )
        Marshal.Copy(bytes, 0, buffer, length);                             // 将对象数据复制到对象指针 ((byte[])source,(int)startIndex,(IntPtr)dest,(int)length)
        try
        {
            reValue = Marshal.PtrToStructure(buffer, type);                 // 将指针数据 封送到 对象类型
            Marshal.FreeHGlobal(buffer);                                    // 释放指针内存
        }
        catch(Exception e)
        {
            return null;
        }
        return reValue;
    }
    public static object            BytesToBase     ( object T,byte[] bytes,ref int startIndex,
                                                      int stringCount = 32)                     // 字节数组转化成对应类型的值                                    
    {
        /// (T:转换类型, bytes: 字节数组 startIndex:字节数组下标, stringCount: 可以选参数,字符串长度)
        if (T.GetType().ToString() == "System.Int32")                       // int
        {
            T = BitConverter.ToInt32(bytes, startIndex);
            startIndex += 4;
            return T;
        }
        else if (T.GetType().ToString() == "System.Int16")                  // short
        {
            T = BitConverter.ToInt16(bytes, startIndex);
            startIndex += 2;
            return T;
        }
        else if (T.GetType().ToString() == "System.Byte")                   // byte
        {
            T = bytes[startIndex];
            startIndex += 1;
            return T;
        }
        else if (T.GetType().ToString() == "System.Long")                   // long
        {
            T = BitConverter.ToInt64(bytes, startIndex);
            startIndex += 4;
            return T;
        }
        else if (T.GetType().ToString() == "System.String")                 // string
        {
            byte[] Tempbytes = new byte[stringCount];
            Array.Copy(bytes, startIndex, Tempbytes, 0, stringCount);
            CUST_STRING_32 str = new CUST_STRING_32();
            str = (CUST_STRING_32)Util.BytesToStruct(Tempbytes, Tempbytes.Length, str.GetType());
            startIndex += stringCount;
            return str.custString32;
        }
        else if (T.GetType().ToString() == "Head")                          // Head 头文件
        {
            byte[] TempBytes = new byte[6];
            Array.Copy(bytes, 0, TempBytes, 0, 6);
            Head head = new Head();
            head.size = BitConverter.ToInt16(TempBytes, 0);
            head.type1 = BitConverter.ToInt16(TempBytes, 2);
            head.type2 = BitConverter.ToInt16(TempBytes, 2 + 2);
            T = head;
            startIndex += 6;
            return T;
        }
        else
        {
            Debug.Log("BytesToBase: 未知类型?");
            return null;
        }
    }
    public static byte[]            StructToBytes   ( object obj)                               // 结构体转换成字节数组                                          
    {
        int rawSize = Marshal.SizeOf(obj);              // 接收对象大小
        IntPtr buffer = Marshal.AllocHGlobal(rawSize);  // 从进程的非托管内存分配对象大小内存 并指定指针
        Marshal.StructureToPtr(obj, buffer, false);     // 将数据从托管对象封送到非托管内存块
        byte[] rawData = new byte[rawSize];
        Marshal.Copy(buffer, rawData, 0, rawSize);      // 将数据从非托管指针复制到托管 数组
        Marshal.FreeHGlobal(buffer);                    // 释放非托管内存
        return rawData;
    }


                                                                                                                            /// <| Json数据转换 JsonObject |>
    public static int               GetIntKeyValue          (JsonObject obj,string key)         // 根据Key读取对象中的< int>值,失败返回0                          
    {
        if (obj.ContainsKey(key) && obj[key] != null)
        {
            return ParseToInt(obj[key].ToString());
        }
        else
        {
            Debuger.LogWarning("JsonObject中" + key + "不存在或等于null,json:" + obj);
            return 0;
        }
    }
    public static long              GetLongKeyValue         (JsonObject obj,string key)         // 根据Key读取对象中的< long>值,失败返回0                         
    {
        if(obj.ContainsKey(key) && obj[key]!= null)
        {
            return ParseToLong(obj[key].ToString());
        }
        else
        {
            Debuger.LogWarning("JsonObject中" + key + "不存在或等于null,json:" + obj);
            return 0;
        }
    }
    public static float             GetFloatKeyValue        (JsonObject obj,string key)         // 根据Key读取对象中的< float>值,失败返回0                        
    {
        if (obj.ContainsKey(key) && obj[key]!=null)
        {
            return ParseToFloat(obj[key].ToString());
        }
        else
        {
            Debuger.LogWarning("JsonObject中" + key + "不存在或等于null,json:" + obj);
            return 0;
        }
    }
    public static string            GetStringKeyValue       (JsonObject obj,string key)         // 根据Key读取对象中的< string>值,失败返回String.Empty            
    {
        if(obj.ContainsKey(key) && obj[key] != null)
        {
            return obj[key].ToString();
        }
        else
        {
            Debuger.LogWarning      ("JsonObject中" + key + "不存在或等于null,json:" + obj);
            return string.Empty;
        }
    }
    public static bool              GetBoolKeyValue         (JsonObject obj,string key)         // 根据Key读取对象中的< bool>值,失败返回flase                     
    {
        if(obj.ContainsKey(key)&&obj[key] != null)
        {
            return ParseToBool(obj[key].ToString());
        }
        else
        {
            Debuger.LogWarning("JsonObject中" + key + "不存在或等于null,json:" + obj);
            return false;
        }
    }
    public static DateTime          GetDataTimeKeyToValue   (JsonObject obj,string key)         // 根据Key读取对象中的< DateTime>值,失败返回DateTime.MinValue     
    {
        if(obj.ContainsKey(key) && obj[key] != null)
        {
            return ParseToDateTime(obj[key].ToString());
        }
        else
        {
            Debuger.LogWarning("JsonObject中" + key + "不存在或等于null,json:" + obj);
            return DateTime.MinValue;
        }
    }


                                                                                                                            /// <| 数据加密t_MD5 |>
    public static string            EncryptAccount          (string account)                    // 加密账户信息                                                  
    {
        string p = "435kfa3fjaf03fakf3fa3220fasjf3we322f2fjaf92f2fsf92fl1h1535rb7nw4\0";

        char[] output = new char[64];

        Array.Copy(account.ToCharArray(), output, account.Length);

        char len = (char)account.Length;
        char key = '0';
        int a = 1;
        byte b = (byte)a;

        for (int i = 0; i < len; ++i)
        {
            if (output[i] >= 'A' && output[i] <= 'Z')
            {
                key = (char)((p[len] % 26 + 26) % 26);
                output[i] = (char)((output[i] - 'A' + key) % 26 + 'A');
            }
            else if (output[i] >= 'a' && output[i] <= 'z')
            {
                key = (char)((p[len] % 26 + 26) % 26);
                output[i] = (char)((output[i] - 'a' + key) % 26 + 'a');
            }
            else if (account.ToCharArray()[i] >= '0' && account.ToCharArray()[i] <= '9')
            {
                key = (char)((p[len] % 10 + 10) % 10);
                output[i] = (char)((output[i] - '0' + key) % 10 + '0');
            }
        }
        return new string(output);
    }
    public static string            MD5file                 (string filePath)                   // 计算文件的哈希值                                              
    {
        try
        {
            FileStream FileStm = new FileStream(filePath, FileMode.Open);                                           // 创建并打开文件
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();     // 加密服务,计算数据输入的哈希值
            byte[] retVal = md5.ComputeHash(FileStm);                                                               // 计算指定对象的哈希值
            FileStm.Close();                                                                                        // 关闭文件

            StringBuilder StrBuil = new StringBuilder();
            for (int i = 0; i < retVal.Length; i ++) 
            {
                StrBuil.Append(retVal[i].ToString("x2"));
            }
            return StrBuil.ToString();
        }
        catch (Exception e)
        {
            throw new Exception("md5file() fail, error:" + e.Message);
        }
    }
    public static string            GetMD5                  (string msg)                        // 获取字符串MD5值                                               
    {
        StringBuilder StrBuil = new StringBuilder();                  /// 表示可变字符字符串

        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            byte[] newBuffer = md5.ComputeHash(buffer);

            foreach (byte item in newBuffer)
            {
                StrBuil.Append(item.ToString("x2"));
            }
        }
        return StrBuil.ToString();
    }

    #endregion

    #region================================================||  英雄系统                      ||=====================================================

    private static Dictionary<int, int>         CurrentMergeItemDic     = new Dictionary<int, int>();                       // 单一合成的装备所消耗的资源
    private static Dictionary<string, UIAtlas>  Atlas                   = new Dictionary<string, UIAtlas>();
    public static  UIAtlas          GetGameAtlas(string key)                                                                // 获取图集                        
    {
        if (!Atlas.ContainsKey(key))
        {
            return Atlas[key];
        }
        else
        {
            Util.ErrLog("Not found Atlas !" + key);
        }
        return null;
    }
    private static bool             CheckBag                ( IPlayer player,int equipID,IHeroData heroData)                // 检查背包装备                    
    {
        Configs_EquipData CFEquipData = Configs_Equip.sInstance.GetEquipDataByEquipID(equipID);

        foreach(Equip equip in player.EquipList)
        {
            if(equip.ID == equipID)
            {
                if(CFEquipData.DressLev  <= heroData.HeroLevel)
                {   return true;    }

                else
                {    return false;  }
            }
        }
        return false;
    }                                                                                           
    private static bool             GetMergeItem            ( int ID,ref int needCoins,Dictionary<int,int> CurrentMergeItemDic,
                                                              IPlayer player)                                               // 是否可合成   
    {
        bool noHas = false;
        switch ((ItemType)Util.FindItemTypeFromID(ID, 0))
        {
            case ItemType.equip:
            case ItemType.scroll:
                {
                    foreach(Equip equip in player.EquipList)        
                    {
                        if(equip.ID == ID)
                        {
                            if(CurrentMergeItemDic.ContainsKey(ID))
                            {
                                if((1 + CurrentMergeItemDic[ID]) <= equip.count)
                                {
                                    CurrentMergeItemDic[ID] += 1;
                                    noHas = true;
                                    return true;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if( 1 <= equip.count)
                                {
                                    CurrentMergeItemDic.Add(ID, 1);
                                    noHas = true;
                                    return true;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }

                    if(!noHas)                                              // 背包没有这件装备或碎片
                    {
                        List<int> MaterialList = new List<int>();

                        if(Configs_Equip.sInstance.GetEquipDataByEquipID(ID).CompoundType == 1)
                        {
                            return false;
                        }
                        if(Configs_Equip.sInstance.GetEquipDataByEquipID(ID).CompoundType == 2)                     // 装备合成
                        {
                            MaterialList = Configs_Equip.sInstance.GetEquipDataByEquipID(ID).Material;
                        }
                        else if(Configs_Equip .sInstance.GetEquipDataByEquipID(ID).CompoundType == 3)               // 碎片合成
                        {
                            MaterialList = new List<int>();
                            MaterialList.Add(Configs_Equip.sInstance.GetEquipDataByEquipID(ID).FragmentID);
                        }
                        if (MaterialList.Count == 0)
                        {
                            return false;
                        }

                        foreach (int myID in MaterialList)
                        {
                            if(myID == 0 )
                            {
                                return false;
                            }
                            if(!GetMergeItem(myID, ref needCoins,CurrentMergeItemDic,player))
                            {
                                return false;
                            }
                        }
                        needCoins += Configs_Equip.sInstance.GetEquipDataByEquipID(ID).Cost;
                    }
                }
                break;
            case ItemType.equipFragment:
            case ItemType.scrollFragment:
                {
                    int needCount = Configs_Fragment.sInstance.GetFragmentDataByFragmentID(ID).Num;
                    foreach(Fragment frag in player.FragmentList)
                    {
                        if(frag.ID == ID)
                        {
                            if(CurrentMergeItemDic.ContainsKey(ID))
                            {
                                if(frag.count >= (needCount + CurrentMergeItemDic[ID]))
                                {
                                    CurrentMergeItemDic[ID] += needCount;
                                    needCoins += Configs_Fragment.sInstance.GetFragmentDataByFragmentID(ID).Cost;
                                    noHas = true;
                                    return true;
                                }
                                else
                                {   break;  }
                            }
                            else
                            {
                                if(frag.count >= needCount)
                                {
                                    CurrentMergeItemDic.Add(ID, needCount);
                                    noHas = true;
                                    return true;
                                }
                                else
                                {   break;  }
                            }
                        }
                    }
                    if(!noHas)
                    {
                        return false;
                    }
                }
                break;

        }
        return true;
    }


    public static int               OccuAdd                 ( int num, int proLevel, int type)                              // 计算职业等级增加的属性值         
    {
        Configs_OccupationalAdditionData data = Configs_OccupationalAddition.sInstance.GetOccupationalAdditionDataByID(num + proLevel);
        if (proLevel == 0)
        {
            return 0;
        }
        if (data == null)
        {
            return 0;
        }
        if (type == data.Strength)
        {
            return data.Agility;
        }
        else
        {
            return 0;
        }
    }
    public static int               FindItemTypeFromID      ( int ID, int type)                                             // 根据物品ID返回物品类型           
    {
        if (ID >= 60001 && ID <= 69999)
        {
            return (int)ItemType.equip;
        }
        else if (ID >= 70001 && ID <= 79999)
        {
            return (int)ItemType.equipFragment;
        }
        else if (ID >= 90001 && ID <= 99999)
        {
            return (int)ItemType.soul;
        }
        else if (ID >= 10001 && ID <= 19999)
        {
            return (int)ItemType.coinsprop;
        }
        else if (ID >= 1 && ID <= 9999)
        {
            return (int)ItemType.wing;
        }
        else if (ID >= 100000 && ID <= 200000)
        {
            return (int)ItemType.hero;
        }
        return type;
    }
    public static int               GetPrivateHeroID        ( int RoleHeroID)                                               // 主角英雄的私有英雄              
    {
        return Configs_Hero.sInstance.GetHeroDataByHeroID(RoleHeroID).InitializeHeroID;
    }

    public static string            GetQualityFrameName     ( HeroQuality inHeroQuality)                                    // 获取英雄框 图集子名             
    {
       switch(inHeroQuality)
        {
            case HeroQuality.White:                         return "touxiang-bai-84";
            case HeroQuality.Green:                         return "touxiang-lv-84";
            case HeroQuality.Green1:                        return "touxiang-lv+1-84";
            case HeroQuality.Blue:                          return "touxiang-lan-84";
            case HeroQuality.Blue1:                         return "touxiang-lan+1-84";
            case HeroQuality.Blue2:                         return "touxiang-lan+2-84";
            case HeroQuality.Purple:                        return "touxiang-zi-84";
            case HeroQuality.Purple1:                       return "touxiang-zi+1-84";
            case HeroQuality.Purple2:                       return "touxiang-zi+2-84";
            case HeroQuality.Purple3:                       return "touxiang-zi+3-84";
            case HeroQuality.Gold:                          return "touxiang-jin-84";
            default:
                Debug.LogWarning("边框数据错误!");           return "touxiang-bai-84";
        }      
    }
    public static string            GetHeroAttribTypeName   ( HeroAttribType_Main heroAttribType)                           // 获取英雄属性名称(主属性)        
    {
        switch(heroAttribType)
        {
            case HeroAttribType_Main.Blood:             return "生命";
            case HeroAttribType_Main.PhyAttack:         return "物理攻击";
            case HeroAttribType_Main.MagicAttack:       return "魔法攻击";
            case HeroAttribType_Main.PhyArmor:          return "物理护甲";
            case HeroAttribType_Main.MagicArmor:        return "魔法抗性";
            case HeroAttribType_Main.PhyCrit:           return "物理暴击";
            case HeroAttribType_Main.MagicCrit:         return "魔法暴击";
            case HeroAttribType_Main.ThroughPhyArmor:   return "物理穿透";

            case HeroAttribType_Main.EnergyRegen:       return "能力回复";
            case HeroAttribType_Main.BloodRegen:        return "生命回复";
            case HeroAttribType_Main.SuckBlood:         return "吸血";
            case HeroAttribType_Main.Hit:               return "命中";
            case HeroAttribType_Main.Dodge:             return "闪避";
        }
        return "Error! 未知属性: (GetHeroAttribTypeName())";
    }
    public static string            GetHeroAttribTypeName   ( HeroAttribType_Lv1 heroAttribType)                            // 获取英雄属性名称(1级属性)       
    {
        switch(heroAttribType)
        {
            case HeroAttribType_Lv1.Power:      return "力量";
            case HeroAttribType_Lv1.Agile:      return "敏捷";
            case HeroAttribType_Lv1.Intellect:  return "智力";

        }
        return "Error! 未知属性: (GetHeroAttribTypeName())";
    }
  

    public static bool              IsCanWearEquip          ( IHeroData heroData,IPlayer player)                            // 是否有可穿戴装备                 
    {
        int heroQualityID = 0;
        Configs_HeroQualityData CFHQualityData = Configs_HeroQuality.sInstance.GetHeroQualityDataByQualityID(heroQualityID);

        bool leftTopWear     = false;
        bool rightTopWear    = false;
        bool leftMidWear     = false;
        bool rightMidWear    = false;
        bool leftBottomWear  = false;
        bool rightBottomWear = false;

        switch(heroData.Quality)                                
        {
            case HeroQuality.White:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).White;
                break;
            case HeroQuality.Green:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Green;
                break;
            case HeroQuality.Green1:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Green1;
                break;
            case HeroQuality.Blue:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Blue;
                break;
            case HeroQuality.Blue1:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Blue1;
                break;
            case HeroQuality.Blue2:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Blue2;
                break;
            case HeroQuality.Purple:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple;
                break;
            case HeroQuality.Purple1:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple1;
                break;
            case HeroQuality.Purple2:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple2;
                break;
            case HeroQuality.Purple3:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Purple3;
                break;
            case HeroQuality.Gold:
                heroQualityID = Configs_Hero.sInstance.GetHeroDataByHeroID(heroData.ID).Gold;
                break;

        }

        foreach(WearPosition position in heroData.EquipList.Keys)
        {
            if(position == WearPosition.LeftTop)
            {   leftTopWear = true;  }

            if(position == WearPosition.RightTop)
            {   rightTopWear = true; }

            if(position == WearPosition.LeftMid)
            {   leftMidWear = true;  }

            if(position == WearPosition.RightMid)
            {   rightMidWear = true; }

            if(position == WearPosition.LeftBottom)
            {   leftBottomWear = true; }

            if(position == WearPosition.RightBottom)
            {   rightBottomWear = true; }
        }

        if (!leftTopWear)
        {   return CheckBag(player, CFHQualityData.Equip1, heroData);   }

        if (!rightTopWear)
        {   return CheckBag(player, CFHQualityData.Equip2, heroData);   }

        if (!leftMidWear)
        {   return CheckBag(player, CFHQualityData.Equip3, heroData);   }

        if (!rightMidWear)
        {   return CheckBag(player, CFHQualityData.Equip4, heroData);   }

        if (!leftBottomWear)
        {   return CheckBag(player, CFHQualityData.Equip5, heroData);   }

        if (!rightBottomWear)
        {   return CheckBag(player, CFHQualityData.Equip6, heroData);   }

        return false;
    }
    public static bool              IsFullToMerge           ( int ID,IHeroData  hero,IPlayer player)                        // 合成材料是否足够(参考HeroInfoPanelView.IsFullToMerg方法改写)
    {
        int currentCoins = 0;
        CurrentMergeItemDic.Clear();
        bool isNotFull = GetMergeItem(ID, ref currentCoins, CurrentMergeItemDic, player);

        if(!(isNotFull && Configs_Equip.sInstance.GetEquipDataByEquipID(ID).DressLev  <= hero.HeroLevel))
        {
            return false;
        }
        if(player.PlayerCoins < currentCoins)
        {
            return false;
        }
        return true;
    }
    public static bool              IsCanStarUp             ( IHeroData heroData,IPlayer player)                            // 是否可以升星                     
    {
        int tagID = 0;
        int myStarCount = 0;
        int mySoulCount = 0;
        bool isUpStar = true;

        if (heroData.HeroStar == 1)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar2SoulNum");
        }
        else if (heroData.HeroStar == 2)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar3SoulNum");
        }
        else if (heroData.HeroStar == 3)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar4SoulNum");
        }
        else if (heroData.HeroStar == 4)
        {
            myStarCount = CustomJsonUtil.GetValueToInt("ToStar5SoulNum");
        }
        else if(heroData.HeroStar == 5)
        {
            isUpStar = false;
            return isUpStar;
        }

        foreach (int soulID in Configs_Soul.sInstance.mSoulDatas.Keys)
        {
            Configs_SoulData CFSoulData = Configs_Soul.sInstance.mSoulDatas[soulID];
            if(CFSoulData.Target == heroData.ID)
            {
                tagID = soulID;
                break;
            }
        }
            
        foreach(Soul soul in player.GetHeroSoulList)
        {
            if(tagID == soul.ID)
            {
                mySoulCount = soul.count;
                break;
            }
        }
        if(mySoulCount >= myStarCount)
        {
            isUpStar = true;
        }
        else
        {
            isUpStar = false;
        }
        return isUpStar;
    }

    public static void              SetHeroNameDefine       ( UILabel lable,HeroQuality heroQ,Configs_HeroData heroData)    // 显示英雄名字 - 不改变字体颜色     
    {
        switch(heroQ)
        {
            case HeroQuality.White:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Green:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Green1:
                lable.text = Language.GetValue(heroData.HeroName) + "+1";
                break;
            case HeroQuality.Blue:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Blue1:
                lable.text = Language.GetValue(heroData.HeroName) + "+1";
                break;
            case HeroQuality.Blue2:
                lable.text = Language.GetValue(heroData.HeroName) + "+2";
                break;
            case HeroQuality.Purple:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
            case HeroQuality.Purple1:
                lable.text = Language.GetValue(heroData.HeroName) + "+1";
                break;
            case HeroQuality.Purple2:
                lable.text = Language.GetValue(heroData.HeroName) + "+2";
                break;
            case HeroQuality.Purple3:
                lable.text = Language.GetValue(heroData.HeroName) + "+3";
                break;
            case HeroQuality.Gold:
                lable.text = Language.GetValue(heroData.HeroName);
                break;
        }
    }
    public static void              HeroListSort            ( List<IHeroData> inHeroD_List)                                 // 英雄数据列表 排序(星级>品质>等级) 
    {
        IHeroData                   TheHeroD;                                                   // 英雄数据变量

        for (int i = 0; i < inHeroD_List.Count; i++ )
        {

            for ( int j = i + 1; j < inHeroD_List.Count ; j++ )
            {
                if      ( inHeroD_List[i].HeroStar < inHeroD_List[j].HeroStar )                 // 比较 英雄星级.1
                {
                    TheHeroD            = inHeroD_List[i];
                    inHeroD_List[i]     = inHeroD_List[j];
                    inHeroD_List[j]     = TheHeroD;
                }
                else if ( inHeroD_List[i].HeroStar == inHeroD_List[j].HeroStar ) 
                {
                    if ( inHeroD_List[i].Quality < inHeroD_List[j].Quality)                     // 比较 英雄品质.2
                    {
                        TheHeroD            = inHeroD_List[i];
                        inHeroD_List[i]     = inHeroD_List[j];
                        inHeroD_List[j]     = TheHeroD;
                    }
                    else if ( inHeroD_List[i].Quality == inHeroD_List[j].Quality ) 
                    {
                        if (inHeroD_List[i].HeroLevel < inHeroD_List[j].HeroLevel)              // 比较 英雄等级.3
                        {
                            TheHeroD            = inHeroD_List[i];
                            inHeroD_List[i]     = inHeroD_List[j];
                            inHeroD_List[j]     = TheHeroD;
                        }
                    }
                }
            }
        }
        TheHeroD                    = null;
    }
    #endregion

    #region================================================||  GameSystem -- 系统功能设置    ||=====================================================
    //----------------------------------------------|   CheckPointSys -- 关卡系统    |------------------------------------------------------
    public static void              MoveToCurrentCP         (GameObject obj, int chapterNum = 0 )                   // 移动到当前关卡章节    
    {
        UIPanel                 ThePanel                    = NGUITools.FindInParents<UIPanel>(obj);
        if ( ThePanel != null && ThePanel.clipping != UIDrawCall.Clipping.None )
        {
            int                 theChapterNum               = chapterNum - 1;
            UIScrollView        TheScorllView               = ThePanel.GetComponent<UIScrollView>();
            Vector3             OffSet                      = -ThePanel.cachedTransform.InverseTransformPoint(obj.transform.position);
            if (!TheScorllView.canMoveHorizontally)         OffSet.x = ThePanel.cachedTransform.localPosition.x;
            if (!TheScorllView.canMoveVertically)           OffSet.y = ThePanel.cachedTransform.localPosition.y;

            if ( theChapterNum != 0 )
            {
                for ( int i = 0; i < obj.transform.parent.childCount; i++ )
                {
                    if      ( i <= theChapterNum )          return;
                    else if ( i >= obj.transform.parent.childCount - theChapterNum )
                    {
                        if (TheScorllView.canMoveHorizontally)       TheScorllView.contentPivot = UIWidget.Pivot.Right;
                        if (TheScorllView.canMoveVertically)         TheScorllView.contentPivot = UIWidget.Pivot.Bottom;
                        TheScorllView.ResetPosition();
                        return;
                    }
                }
            }
            SpringPanel.Begin ( ThePanel.cachedGameObject, OffSet, 600f );
        }
    }
    #endregion

    #region================================================||  内存和对象的清理              ||=====================================================
    public static void              DestoryImmediate (UnityEngine.Object obj)                                       // 立即清理对象                      
    {
        GameObject.DestroyImmediate(obj);
        obj = null;
        Resources.UnloadUnusedAssets();
    }
    public static void              DestoryImmediate(GameObject obj)                                                // 立即清理对象                      
    {
        GameObject.DestroyImmediate(obj);
        obj = null;
        Resources.UnloadUnusedAssets();
    }
    #endregion

    #region================================================||  打印消息<Log>, MAC地址        ||=====================================================
    public static void              Log         (String info)                                                       // 消息打印                         
    {
        Debuger.Log(info);
    }
    public static void              ErrLog      (String errInfo)                                                    // 错误打印                         
    {
        Debuger.LogError(errInfo);
    }
    public static void              WarnLog     (String warnInfo)                                                   // 警告打印                         
    {
        Debuger.LogWarning(warnInfo);
    }

    public static string            GetMacAddress()                                                                 // 获取设备MAC     
    {
        string mac = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)            // 运行平台 IPhone
        {
#if UNITY_IPHONE
            mac = iphone.vendorIdentifier;
#endif
        }
        else if (Application.platform == RuntimePlatform.Android)           // 运行平台 android
        {
            mac = SystemInfo.deviceUniqueIdentifier;                        // 设备唯一标识
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            mac = SystemInfo.deviceUniqueIdentifier;
        }
        return mac;
    }
    #endregion

    #region================================================||  动作, 模型, 特效              ||=====================================================
    public static List<float>                   WingPos100001       = new List<float>() { -0.15f, 0, 0, -90, 90, 0 };
    public static List<float>                   WingPos100002       = new List<float>() { -0.18f, -0.12f, 0, -90, 90, 0 };
    public static List<float>                   WingPos100003       = new List<float>() { -0.32f, -0.28f, 0, -90, 90, 0 };
    public static Dictionary <string,string>    HeroObjDic          = new Dictionary<string, string>();                     /// 英雄模型对象 字典
    public static Dictionary <string,string>    HeroActDic          = new Dictionary<string, string>();                     /// 英雄动作     字典

    public static float             PlayAnima           ( GameObject role, string AnimaName)                                // 播放角色动画            
    {
        if ( role == null)                                  return 0;                                               /// 未找到该模型

        Animation                   theAnima                = role.GetComponent<Animation>();                       /// 动作实例
        AnimationState              theAnimaState           = theAnima [ AnimaName ];                               /// 动作状态实例
        theAnima.Stop();                                                                                            /// 暂停播放
        theAnima.wrapMode                                   = WrapMode.Loop;                                        /// 循环模式

        if ( theAnima == null )                             return 0;                                               /// 未找到该动作

        if ( !HeroObjDic.ContainsKey( role.name ))                                                                  /// 添加角色休闲动作和特效
        {
            Dictionary<int, Configs_ActionAndEffectData>    ActEffectD_Dic = Configs_ActionAndEffect.sInstance.mActionAndEffectDatas;
            foreach ( int key in ActEffectD_Dic.Keys )
            {
                if (!HeroObjDic.ContainsKey(ActEffectD_Dic[key].HeroModel))
                {
                    HeroObjDic.Add(ActEffectD_Dic[key].HeroModel, ActEffectD_Dic[key].ShowFreeActionEffect);         /// 添加休闲待机特效
                    HeroActDic.Add(ActEffectD_Dic[key].HeroModel, ActEffectD_Dic[key].ShowFreeAction);               /// 添加休闲待机动作
                }
            }
        }
        if ( HeroObjDic.ContainsKey ( role.name ))                                                                  /// 展示角色特效
        {
            if ( AnimaName.CompareTo( HeroActDic [role.name]) == 0 )
            {
                UnityEngine.Object  TempObj                 = Util.Load ( "Prefabs/RoleEffect/" + HeroActDic [ role.name ]);
                GameObject          theEffect = null;
                if ( TempObj != null )
                {
                    theEffect                               = MonoBehaviour.Instantiate(TempObj) as GameObject;
                    theEffect.transform.parent              = role.transform;
                    theEffect.transform.localEulerAngles    = Vector3.zero;
                    theEffect.transform.localPosition       = Vector3.zero;
                    theEffect.transform.localScale          = Vector3.one;
                }
                else                Debug.Log("展示特效未找到." + HeroObjDic [role.name]);
            }
        }
        else Debug.Log("模型未找到." + HeroObjDic[role.name]);

        theAnima.CrossFade          ( AnimaName );                                                                  /// 淡出动画
        if ( theAnimaState == null )                        return 0;                                               /// 动作状态为空 返回 0
        else                                                return theAnimaState.length;                            /// 返回  动作状态长度

    }
    public static float             PlayOnceAnima       ( GameObject role, string AnimaName )                               // 播放指定模型动画         
    {
        if ( role == null )                                 return 0;

        Animation                   TempAnima               = role.GetComponent <Animation>();                      /// 动画组件
        AnimationState              TempAnimaState          = TempAnima [AnimaName];                                /// 动画控制
        if ( TempAnimaState == null )                       return 0;

        if ( !HeroObjDic.ContainsKey ( role.name ))
        {
            Dictionary< int, Configs_ActionAndEffectData> ActEffectD = Configs_ActionAndEffect.sInstance.mActionAndEffectDatas;
            foreach ( int key in ActEffectD.Keys )
            {
                if ( !HeroObjDic.ContainsKey ( ActEffectD[key].HeroModel))
                {
                    HeroObjDic.Add ( ActEffectD[key].HeroModel, ActEffectD[key].ShowFreeActionEffect);
                    HeroActDic.Add ( ActEffectD[key].HeroModel, ActEffectD[key].ShowFreeAction);
                }
            }
        }
        if ( HeroObjDic.ContainsKey ( role.name ))
        {
            if ( AnimaName.CompareTo (HeroActDic[role.name]) == 0 )
            {
                UnityEngine.Object  obj                     = Util.Load("Prefabs/RoleEffect/" + HeroObjDic[role.name]);
                GameObject          TempEffect              = null;
                if ( obj != null)
                {
                    TempEffect                              = MonoBehaviour.Instantiate(obj) as GameObject;
                    TempEffect.transform.parent             = role.transform;
                    TempEffect.transform.localEulerAngles   = Vector3.zero;
                    TempEffect.transform.localScale         = Vector3.one;
                    TempEffect.transform.localPosition      = Vector3.zero;
                }
                else                Debug.Log("展示特效未找到: " + HeroObjDic[role.name]);
            }
        }
        else Debug.Log              (" 模型未找到 " + role.name );
        TempAnima.Play ( AnimaName );                                                                               /// 播放动画
        return TempAnimaState.length;                                                                               /// 动画长度
    }
    public static float             PlayAnim_Wing       ( GameObject inObj, string inAnimName, bool inIsLoop)               // 播放翅膀动画             
    {
        if (inObj == null)                                          return 0;
        Animation                   TheAnim                         = inObj.GetComponent<Animation>();
        AnimationState              TheAnimStat                     = TheAnim[inAnimName];

        TheAnim.Stop();
        if (inIsLoop)               TheAnim.wrapMode                = WrapMode.Loop;
        else                        TheAnim.wrapMode                = WrapMode.Once;
        if (TheAnimStat == null)
        {    Debuger.Log("动作没有找到！ Name = " + inAnimName);     return 0;                       }

        TheAnim.Play(inAnimName);
        return                      TheAnimStat.length;
    }

    public static void              AsyncLoadModelShadow( Transform modelTF, int modelID )                                  // 动态加载影子            
    {
        UnityEngine.Object          ShadowObj               = Util.Load( "Shadow/HeroShow/ShadowProjector" );       /// 加载影子路径
        GameObject                  Shadow                  = MonoBehaviour.Instantiate(ShadowObj) as GameObject;
        ShadowObj                   = null;
        Shadow.name                 = "shadow";
        Shadow.transform.parent     = modelTF;

        Transform Child             = modelTF.FindChild     ( Configs_ShadowBone.sInstance.                         /// 影子骨骼路径
                                                              GetShadowBoneDataByResourceID(modelID).ShadowBonePath );
        Shadow.GetComponent<shadowLogic>().trans            = Child;
        Shadow.transform.localRotation                      = Quaternion.Euler ( 90, 0, 0 );
        Shadow.transform.localScale                         = Vector3.zero;
        Shadow.transform.localPosition                      = new Vector3 ( Child.localPosition.x,                  
                                                                            Child.localPosition.y,
                                                                            Child.localPosition.z );
    }
    public static void              AddModelPartEffect  ( GameObject obj,int heroID )                                       // 添加模型粒子特效        
    {
        Configs_HeroData            TheHeroD                = Configs_Hero.sInstance.GetHeroDataByHeroID ( heroID );
        int                         theID                   = TheHeroD.Resource;
        string                      ModelName               = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID(TheHeroD.Resource).HeroModel;    /// 获取模型名称
        Dictionary <int, Configs_ActionAndEffectData>       TheActEffctDic          = Configs_ActionAndEffect.sInstance.mActionAndEffectDatas;

        foreach ( int  key in TheActEffctDic.Keys )
        {
            if ( TheActEffctDic[key].HeroModel.CompareTo ( obj.name) == 0)          theID = key;        break;
        }
        if ( theID != 0 )
        {
            Dictionary<int, Configs_ModelPermanentEffectData> TempModelEffectDic    = Configs_ModelPermanentEffect.sInstance.mModelPermanentEffectDatas;
            if (TempModelEffectDic.ContainsKey (theID ))
            {
                if ( obj != null )
                {
                    Configs_ModelPermanentEffectData        TempMoelEffctD          = TempModelEffectDic[theID];
                    if ( TempMoelEffctD.BindingBone1.CompareTo("") != 0 )
                    {    AddEffectToModel (obj, TempMoelEffctD.BindingBone1,        TempMoelEffctD.BindingBonePosition1, 
                                                TempMoelEffctD.BindingBoneRotate1,  TempMoelEffctD.AddBoneEffect1           ); }

                    if ( TempMoelEffctD.BindingBone2.CompareTo("") != 0 )
                    {    AddEffectToModel (obj, TempMoelEffctD.BindingBone2,        TempMoelEffctD.BindingBonePosition2, 
                                                TempMoelEffctD.BindingBoneRotate2,  TempMoelEffctD.AddBoneEffect2           ); }

                    if (TempMoelEffctD.BindingBone3.CompareTo("") != 0)
                    {    AddEffectToModel (obj, TempMoelEffctD.BindingBone3,         TempMoelEffctD.BindingBonePosition3,
                                                TempMoelEffctD.BindingBoneRotate3,   TempMoelEffctD.AddBoneEffect3          ); }

                    if (TempMoelEffctD.BindingBone4.CompareTo("") != 0)
                    {    AddEffectToModel (obj, TempMoelEffctD.BindingBone4,         TempMoelEffctD.BindingBonePosition4,
                                                TempMoelEffctD.BindingBoneRotate4,   TempMoelEffctD.AddBoneEffect4          ); }

                    if (TempMoelEffctD.BindingBone5.CompareTo("") != 0)
                    {    AddEffectToModel (obj, TempMoelEffctD.BindingBone5,         TempMoelEffctD.BindingBonePosition5,
                                                TempMoelEffctD.BindingBoneRotate5,   TempMoelEffctD.AddBoneEffect5          ); }

                    if (TempMoelEffctD.BindingBone6.CompareTo("") != 0)
                    {    AddEffectToModel (obj, TempMoelEffctD.BindingBone6,         TempMoelEffctD.BindingBonePosition6,
                                                TempMoelEffctD.BindingBoneRotate6,   TempMoelEffctD.AddBoneEffect6          ); }
                }
                return;
            }
            else                                            return; 
        }
        else                                                return;
    }
    public static void              AddModelPartEffect  ( GameObject inObj,int inResID,int inHeroID,int inWingID)           // 添加模型特效            
    {
        Configs_HeroData            TheHero_C                       = Configs_Hero.sInstance.GetHeroDataByHeroID(inHeroID); 

        if (TheHero_C.Resource.CompareTo(inResID) == 0)
        {
            Configs_WingData        TheWing_C                       = Configs_Wing.sInstance.GetWingDataByWingNum(inWingID);
            if (TheWing_C != null)
            {
                switch(TheHero_C.HeroID)
                {
                    case 100001:    AddEffectModel_Wing(inObj, WingPos100001, TheWing_C);   break;
                    case 100002:    AddEffectModel_Wing(inObj, WingPos100002, TheWing_C);   break;
                    case 100003:    AddEffectModel_Wing(inObj, WingPos100003, TheWing_C);   break;
                    default:                                                                            break;
                }
            }

            if (inResID != 0)
            {
                Dictionary<int, Configs_ModelPermanentEffectData>   TheModePerEffectDic     = Configs_ModelPermanentEffect.sInstance.mModelPermanentEffectDatas;
                if (TheModePerEffectDic.ContainsKey(inResID))
                {
                    if (inObj != null)
                    {
                        Configs_ModelPermanentEffectData            ThePerEffect_C          = TheModePerEffectDic[inResID];
                        if (ThePerEffect_C.BindingBone1.CompareTo("") != 0)
                        {   AddEffectToModel(inObj, ThePerEffect_C.BindingBone1, ThePerEffect_C.BindingBonePosition1, ThePerEffect_C.BindingBoneRotate1, ThePerEffect_C.AddBoneEffect1); }
                        if (ThePerEffect_C.BindingBone2.CompareTo("") != 0)
                        {   AddEffectToModel(inObj, ThePerEffect_C.BindingBone1, ThePerEffect_C.BindingBonePosition2, ThePerEffect_C.BindingBoneRotate2, ThePerEffect_C.AddBoneEffect2); }
                        if (ThePerEffect_C.BindingBone3.CompareTo("") != 0)
                        {   AddEffectToModel(inObj, ThePerEffect_C.BindingBone1, ThePerEffect_C.BindingBonePosition3, ThePerEffect_C.BindingBoneRotate3, ThePerEffect_C.AddBoneEffect3); }
                        if (ThePerEffect_C.BindingBone4.CompareTo("") != 0)
                        {   AddEffectToModel(inObj, ThePerEffect_C.BindingBone1, ThePerEffect_C.BindingBonePosition4, ThePerEffect_C.BindingBoneRotate4, ThePerEffect_C.AddBoneEffect4); }
                        if (ThePerEffect_C.BindingBone5.CompareTo("") != 0)
                        {   AddEffectToModel(inObj, ThePerEffect_C.BindingBone1, ThePerEffect_C.BindingBonePosition5, ThePerEffect_C.BindingBoneRotate5, ThePerEffect_C.AddBoneEffect5); }
                        if (ThePerEffect_C.BindingBone6.CompareTo("") != 0)
                        {   AddEffectToModel(inObj, ThePerEffect_C.BindingBone1, ThePerEffect_C.BindingBonePosition6, ThePerEffect_C.BindingBoneRotate6, ThePerEffect_C.AddBoneEffect6); }
                    }
                }
                return;
            }
            else                                                    return;
        }
        else                                                        return;
    }
    public static GameObject        LoadSceneEffect     ( string path,GameObject sceneSet )                                 // 加载场景特效             
    {
        GameObject                  TheEffectScene                  = null;                                                     /// 场景特效对象
        string                      TheName                         = System.IO.Path.GetFileNameWithoutExtension(path);         /// 对象名称
        Dictionary <string, Configs_SceneEffectInfoData > 
                                    TheSceneEffectDic               = Configs_SceneEffectInfo.sInstance.mSceneEffectInfoDatas;  /// 场景特效字典

        if ( TheSceneEffectDic.ContainsKey(TheName))
        {
            Configs_SceneEffectInfoData TempSceneEffectD            = Configs_SceneEffectInfo.sInstance.GetSceneEffectInfoDataBySceneName(TheName);
            UnityEngine.Object      TempEffect                      = Resources.Load ( "Prefabs/SceneEffect/" + TempSceneEffectD.SceneEffectName);
            if ( TempEffect != null )
            {
                TheEffectScene                                      = MonoBehaviour.Instantiate (TempEffect) as GameObject;
                TheEffectScene.name                                 = TempSceneEffectD.SceneEffectName;
                TheEffectScene.transform.localRotation              = Quaternion.identity;
                TheEffectScene.transform.localScale                 = Vector3.one;
                TheEffectScene.transform.localPosition              = Vector3.zero;
                TempEffect                                          = null;
            }
            else                                                    Debug.Log ("场景特效未找到" + TempSceneEffectD.SceneEffectName);

            if ( TempSceneEffectD.SceneCameraSetting != "" )
            {
                TempEffect                                          = Resources.Load ( "Prefabs/SceneEffect/" + TempSceneEffectD.SceneCameraEffect);
                if ( TempEffect != null )
                {
                    Camera          TempCa                          = sceneSet.GetComponentInChildren<Camera>();                    /// 摄像机指定对象
                    GameObject      TempEffectCa                    = MonoBehaviour.Instantiate(TempEffect) as GameObject;          /// 摄像机特效
                    TempEffectCa.transform.parent                   = TempCa.gameObject.transform;                              
                    TempEffectCa.name                               = TempSceneEffectD.SceneCameraEffect;
                    TempEffectCa.transform.localRotation            = Quaternion.identity;
                    TempEffectCa.transform.localPosition            = Vector3.zero;
                    TempEffectCa.transform.localScale               = Vector3.one;
                    TempEffect                                      = null; 
                }
                else                                                Debug.Log ( "摄像机特效未找到" + TempSceneEffectD.SceneCameraEffect);
            }
        }
        return                      TheEffectScene;
    }
    public static void              ChangeModelShader   ( GameObject obj )                                                  // 模型改变为展示材质       
    {
        Renderer[]                  TheRenders                      = obj.GetComponentsInChildren<Renderer>();
        foreach ( Renderer key in TheRenders )
        {
            Material[] Materials    = key.materials;
            for ( int i = 0;i < Materials.Length; i++ )
            {
                Materials[i].shader = Shader.Find ( "Custom/MyShader" );
            }
        }
    }

    private static void             AddEffectToModel    ( GameObject targObj,string path,                                   // 添加特效到模型
                                                           List<float> pos,  List<float> rot, string effectName )                                      
    {
        effectName                                              = effectName.Trim();
        string[]                    thePath                     = path.Split(new char[] { '/' });
        Vector3                     TheRot;
        Vector3                     ThePos;

        if ( pos.Count < 3 )
        {    ThePos                 = new Vector3 ( 0, 0, 0 );                  }
        else
        {    ThePos                 = new Vector3 ( pos[0], pos[1], pos[2]);    }

        if ( rot.Count < 3 )
        {    TheRot                 = new Vector3 ( 0, 0, 0 );                  }
        else
        {    TheRot                 = new Vector3 (rot[0], rot[1], rot[2]);     }

        UnityEngine.Object          TheObj                      = Util.Load( "Prefabs/RoleEffect/" + effectName );
        GameObject                  TheEffect                   = MonoBehaviour.Instantiate(TheObj) as GameObject;
        if ( TheEffect != null )
        {
            TheEffect.SetActive     ( false );
            TheEffect.AddComponent < ModelUIPartQueue >();  
            Transform               TempParent                  = targObj.transform;
            for ( int i = 0; i < thePath.Length; i++ )
            {
                TempParent                                      = TempParent.FindChild(thePath[i]);
                if ( TempParent == null )
                {
                    Debuger.Log     ( targObj.name + "模型骨骼点" + thePath[i] + "未找到");
                    MonoBehaviour.Destroy ( TheEffect );
                    return; 
                }
            }
            if (TempParent != null )
            {
                TheEffect.transform.parent                      = TempParent;
                TheEffect.transform.localEulerAngles            = TheRot;
                TheEffect.transform.localPosition               = ThePos;
                TheEffect.transform.localScale                  = Vector3.zero;
                TheEffect.SetActive(true);
                return;
            }
            else
                MonoBehaviour.Destroy ( TheEffect );
            MonoBehaviour.Destroy(TheEffect);
        }
        else
        {
#if UNITY_EDITOR
            Debuger.Log ("模型特效未找到" + "Prefabs/RoleEffect/" + effectName );
#endif
        }
    }
    private static void             AddEffectModel_Wing ( GameObject inObj, List<float> inPosList, Configs_WingData inWing_C )     // 添加模型翅膀特效  
    {
        Vector3                     TheRoteV3;
        Vector3                     ThePosV3;
        GameObject                  TheEffectObj                  = null;

        inWing_C.WingEffectNmae                                 = inWing_C.WingEffectNmae.Trim();
        string[]                    ThePath                     = inWing_C.WingBonePath.Split(new char[] { '/' });
        UnityEngine.Object          TheObj                      = Util.Load("Prefabs/RoleModel/" + inWing_C.WingEffectNmae);
        if (TheObj != null)         TheEffectObj                = MonoBehaviour.Instantiate(TheObj) as GameObject;
        ThePosV3                                                = new Vector3(inPosList[0], inPosList[1], inPosList[2]);
        TheRoteV3                                               = new Vector3(inPosList[3], inPosList[4], inPosList[5]);

        if (TheEffectObj != null)
        {
            Transform               TheTF                       = inObj.transform;
            for (int i = 0; i < ThePath.Length; i++)
            {
                TheTF               = TheTF.FindChild(ThePath[i]);

                if (TheTF == null)      
                {
                    Debug.Log("翅膀骨骼点未找到!_Path:" + ThePath[i] + "ObjName:" + inObj.name);
                    MonoBehaviour.Destroy(TheEffectObj);
                    return;
                }
            }
            if  (TheTF != null)
            {
                TheEffectObj.transform.parent                   = TheTF;
                TheEffectObj.transform.localPosition            = ThePosV3;
                TheEffectObj.transform.localEulerAngles         = TheRoteV3;
                TheEffectObj.transform.localScale               = Vector3.one;
                PlayAnim_Wing       (TheEffectObj, inWing_C.WingStandbyAction, true);
                return;
            }
            MonoBehaviour.Destroy(TheEffectObj);
        }
        else                        Debug.Log("模型特效未找到" + "Prefabs/RoleEffect/" + inWing_C.WingEffectNmae);
    }

    public static void              BackToNomarl        ( GameObject obj)                                                   // BlackToNormal_assist渐隐动画     
    {
        Debug.Log                   ("Util.BackToNomarl");
        UnityEngine.Object          black                   = Util.Load (UIPanelConfig.BlackPanel);
        GameObject                  blackIns                = (GameObject)MonoBehaviour.Instantiate(black);
        blackIns.name               = "blackPanel";
        blackIns.transform.         parent                  = obj.transform;
        blackIns.transform.         localPosition           = Vector3.zero;
        blackIns.transform.         localRotation           = Quaternion.identity;
        blackIns.transform.         localScale              = Vector3.one;
        black                                               = null;
        UISprite                    sprite                  = blackIns.GetComponentInChildren<UISprite>();
        TweenAlpha                  alpha                   = sprite.gameObject.AddComponent<TweenAlpha>();
        alpha.                      duration                = UIAnimationConfig.BlackToNomarl_duration * 2;
        alpha.                      from                    = 1;
        alpha.                      to                      = 0.1f;
        EventDelegate.Add           (alpha.onFinished, delegate ()
        {   sprite                  = null;
            MonoBehaviour.DestroyImmediate(blackIns);
        }, true);
    }

    #endregion
    public static bool              IsMaskString        ( string str )                                                     // 是否屏蔽字      
    {
        Dictionary<int, Configs_NamesAndMaskWordData>   TheNameMaskWrodDic = Configs_NamesAndMaskWord.sInstance.mNamesAndMaskWordDatas;
        foreach ( Configs_NamesAndMaskWordData key in TheNameMaskWrodDic.Values )
        {
            if ( key.MaskWord != "" && str != "" && str.Contains( key.MaskWord ))
            {
                str.Replace ( key.MaskWord, "****");
                return true;
            }
        }
        return false;
    }
}
public enum                         MainSysID                                                                               // 主界面系统ID    
{
    RechargePanel   = 1001,                         // 充值系统
    CheckPointPanel = 1002,                         // 关卡系统
    MallPanel       = 1003,                         // 商城系统
    BuyCoinsPanel   = 1004,                         // 购买金币
    HeroSysPanel    = 1005,                         // 英雄系统
    JJCPanel        = 1006,                         // 竞技场
    MonsterWar      = 1007,                         // 巨兽囚笼
    DragonTrial     = 1008,                         // 巨龙试炼
    ParadiseRoad    = 1009,                         // 天堂之路
    GuildPanel      = 1010,                         // 公会系统

    SecretTower     = 1011,                         // 秘境之塔
    MercantPanel    = 1012,                         // 商人系统
    FriendPanel     = 1013,                         // 好友系统
    WingPanel       = 1014,                         // 翅膀系统
    TaskPanel       = 1015,                         // 任务系统
    BagPanel        = 1016,                         // 背包系统
    MercPanel       = 1017,                         // 佣兵系统
    RankingPanel    = 1018,                         // 排行榜系统
    FirstRecharge   = 1019,                         // 首充
    AcitivityPanel  = 1021,                         // 活动系统
    SevenDayActivity= 1022,                         // 七日活动
    EmailPanel      = 1023,                         // 邮箱系统
    SingIn          = 1024,                         // 签到系统
    SkillUp         = 1025,                         // 技能提升
    Gemstone        = 1026                          // 宝石系统
}