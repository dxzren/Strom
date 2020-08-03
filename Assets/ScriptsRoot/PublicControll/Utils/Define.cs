using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Define                                                                                                 // 登录访问设置（IP,端口,SDK...）                   
{
    public const int        LOCAL_SERVO_PORT            = 80;                                                       /// 本地伺服端口（WEB服 用来持无SDK版本的帐号创建等操作）

    public static GameVersion SDKVersion                = GameVersion.Yijie;                                        /// 游戏版本 0.版署 1.易接 2.U8 3.内网 4.外网

    public static bool      Quota                       = false;                                                    /// 支付定额开关（已知定额：百度）
    public static bool      UsingUmeng                  = false;                                                    /// 友盟开关
    public static bool      SDKSwitch                   = false;                                                    /// SDK开关

    public const string     downloadpath                = "http://.........";                                       /// 网络下载地址
    public const string     RESOURCES_FTP_URL           = downloadpath + "Res/";                                    /// 资源下载地址+路径
    public const string     RESCONFIG_FTP_URL           = downloadpath + "ResConfig.txt";                           /// 配置下载地址+路径

    public static string    RESOURCES_LOCAL_PATH        = Application.persistentDataPath + "/Res/";                 /// 本地资源路径
    public static string    RESCONFIG_LOCAL_PATH        = Application.persistentDataPath + "ResConfig.txt";         /// 本地配置文件路径

    public const string     NOTICE_NETWORK              = "您目前使用的是数据流量，将要下载｛0｝M大小的更新文件，是否继续？";
    public const string     NOTICE_UPDATE               = "更新游戏资源文件";
    public const string     NOTICE_CONNECTFAILED        = "无法连接到更新服务器，请检查网络连接！";
    public const string     NetBreakNotice              = "无法连接到游戏服务器，请检查网络连接！";

    public const string     UPDATE_MESSAGE              = "UpdateMessage";                                          /// 更新消息
    public const string     UPDATE_EXTRACT              = "UpdateExtract";                                          /// 更新解包
    public const string     UPDATE_DOWNLOAD             = "UpdateDownload";                                         /// 更新下载
    public const string     UPDATE_PROGRESS             = "UpdateProgress";                                         /// 更新进度

    public const string     KeystorePassword            = "111111";                                                 /// 商店密码
    public const string     KeyaliasPassword            = "111111";                                                 /// 别名密码

    public static bool      GMEnbled                    = false;                                                    /// GM开关
    public const string     ENABLEGMCOMMAND             = "ENABLEGMCOMMAND";                                        /// GM开启指令

    //-----------------------------------------------------------------------------------------------------------------

    public static string    LOCAL_SERVO_IP                                                      // 伺服IP                
    {
        get
        {
            switch (SDKVersion)
            {
                case GameVersion.Yijie:
                    return "47.96.96.95";                                   // 王晓赣阿里云TEST
                case GameVersion.Lan:
                    return "......";
                case GameVersion.TestServer:
                    return "......";
                default:
                    return "......";

            }
        }
    }
    public static string    LOGIN_IP                                                            // 登录服IP              
    {
        get
        {
            if (Application.bundleIdentifier == "com.tencent.tmgp.ldblal01")        // 腾讯应用宝打包_IP
            {
                return "......";
            }
            switch (SDKVersion)
            {
                case GameVersion.Yijie:
                    return "47.96.96.95";                                           // 王晓赣阿里云TEST
                case GameVersion.Lan:
                    return "......";
                case GameVersion.TestServer:
                    return "......";
                default:
                    return "......";

            }
        }
    }

    public static int       LOGIN_PORT                                                          // 登录服端口            
    {
        get
        {
            switch (SDKVersion)
            {

                case GameVersion.Yijie:
                    return 10005;                                           // 王晓赣阿里云TEST
                case GameVersion.Lan:
                    return 0;
                case GameVersion.TestServer:
                    return 0;
                default:
                    return 0;
            }
        }
    }
    public static int       AccountLength                                                       // 用户名长度            
    {
        get
        {
            switch (SDKVersion)
            {
                case GameVersion.Yijie:
                    return 64;                                              // 王晓赣阿里云TEST
                case GameVersion.Lan:
                    return 32;
                case GameVersion.TestServer:
                    return 32;
                default:
                    return 32;
            }
        }
    }

    public static string    LocalResPath                                                        // 获取本地下载资源的路径                         
    { get { return Application.persistentDataPath + RESCONFIG_FTP_URL.Replace(downloadpath, "/"); } }
    public static string    GetLocalSavePath()                                                  // 获取本地文件保存的路径                         
    { return Application.persistentDataPath + "/GameData/"; }                                   // Windows_C:\Users\username\AppData\LocalLow\company name\product name

}

public static class ExternsionMethods                                                           // 扩展方法：调整摄像器画面比例到16：9               
{
    public static void SetCameraAspect(this Camera Cam)
    {
        Camera camera3D;                                                                            // 摄影机
        float device_width = 0f;                                                                    // 设备宽度      
        float device_height = 0f;                                                                   // 设备高度
        float set_width = 0f;                                                                       // 设置宽度
        float set_rectH = 0f;                                                                       // 设置矩形高度

        camera3D = Cam.GetComponent<Camera>();
        device_width = Screen.width;
        device_height = Screen.height;
        set_width = (9f / 16f) / (device_height / device_width);
        camera3D.rect = new Rect(0, 0, 1, set_width);
        set_rectH = ((float)Screen.height - (float)camera3D.pixelHeight) / (float)Screen.height / 2;
        camera3D.rect = new Rect(0, set_rectH, 1, set_width);
    }
}

public enum GameVersion                                                                         // 游戏SDK版本类型                                  
{
    Censor = 0,                                                             /// 0.版署
    Yijie = 1,                                                              /// 1.易接
    U8 = 2,                                                                 /// 2.U8
    Lan = 3,                                                                /// 3.内网
    TestServer = 4                                                          /// 4.外网测试
}