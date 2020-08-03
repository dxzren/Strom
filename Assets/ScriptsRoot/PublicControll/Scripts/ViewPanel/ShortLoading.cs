using UnityEngine;
using System.Collections;
/// <summary>   短暂加载界面    </summary>

public class ShortLoading : MonoBehaviour
{

    public float            theEulerAngle           = 5f;                                       // 旋转角度
    public float            pointTime               = 0.3f;                                     // 顶点时间
    public GameObject       LoadSnow;                                                           // 加载Icon
    public GameObject       Background;                                                         // 背景


    private int             connetNo                = 0;                                        // 连接
    private int             index                   = 0;                                        // 索引号
    private UISprite        ThePoint_1              = null;                                     // 顶点_1
    private UISprite        ThePoint_2              = null;                                     // 顶点_2
    private UISprite        ThePoint_3              = null;                                     // 顶点_3
    private UISprite        TheSP                   = null;                                     // 工会
    private Transform       TFObj                   = null;                                     // 变换对象


	void                    Start ()            
    {
        SetHide();
        TFObj               = LoadSnow.transform.FindChild("SnowIcon_Sprite");
        ThePoint_1          = LoadSnow.transform.FindChild("LoadingLabel").Find("1").GetComponent<UISprite>();
        ThePoint_2          = LoadSnow.transform.FindChild("LoadingLabel").Find("2").GetComponent<UISprite>();
        ThePoint_3          = LoadSnow.transform.FindChild("LoadingLabel").Find("3").GetComponent<UISprite>();

        if(!IsInvoking      ( "PointPlay" ))
        {
            InvokeRepeating ( "PointPlay", 0.02f, pointTime );
        }
	}	
	void                    Update ()           
    {
        Play();	
	}

    public void             SetHide()                                                           // 设置隐藏     
    {   
        CancelInvoke            ( "ShowDialog" );                                               /// 线程取消howDialog方法
        LoadSnow.SetActive      ( false );                                                      /// Fish隐藏
        Background.SetActive    ( false );                                                      /// 背景隐藏
    }
    public void             SetShow( float WaitSeconds = 10f )                                  // 设置显示     
    {
        LoadSnow.SetActive      ( true );                                                       /// Fish显示
        Background.SetActive    ( true );                                                       /// 背景显示
        if ( WaitSeconds != 0)
        {
            Invoke("ShowDialog", WaitSeconds);
        }
    }
    public void             ShowDialog()                                                        // 显示对话框    
    {
        if ( Application.internetReachability == NetworkReachability.NotReachable)              /// 设备无法访问网络
        {
            PanelManager.sInstance.ShowDialogPanel( "无法连接游戏服务器,请检查网络", "确定",
            () =>
            {
                Invoke("ShowDialog", 4.0f);
                connetNo = 1;
            },
            () =>
            {
                PanelManager.sInstance.HideLoadingPanel();
            });
        }
        else if( connetNo < 1)
        {
            PanelManager.sInstance.ShowDialogPanel( "请求超时,重新请求", "确定", 
            () =>
            {
                 connetNo++;
                 Invoke("ShowDialog", 3.0f);
            },
            () =>
            {
                PanelManager.sInstance.HideLoadingPanel();
            });
        }
        else 
        {
            PanelManager.sInstance.ShowDialogPanel( "请求超时,重新请求", "确定",
            () =>
            {
                if (Define.SDKSwitch)
                {
                    //SDKManager.Instance.LogOut();
                }
                StartNetWorkCallback.isFirst = true;
                Util.ReLogIn();
                Debug.LogError("请求超时,重新登录......");
            },
            () =>
            {
                PanelManager.sInstance.HideLoadingPanel();
            });
        }
    }
    private void            Play()                         
    {
        TFObj.localEulerAngles = new Vector3(0f, 0f, TFObj.localEulerAngles.z - theEulerAngle);
    }
    private void            PointPlay()                    
    {
        if      ( index == 0 )          
        {
            ThePoint_1.width            = 7;
            ThePoint_1.height           = 7;
            ThePoint_1.spriteName       = "dadian";
            ThePoint_2.width            = 4;
            ThePoint_2.height           = 4;
            ThePoint_2.spriteName       = "xiaodian";
            ThePoint_3.width            = 4;
            ThePoint_3.height           = 4;
            ThePoint_3.spriteName       = "xiaodian";
        }
        else if ( index == 1 )          
        {
            ThePoint_1.width            = 4;
            ThePoint_1.height           = 4;
            ThePoint_1.spriteName       = "xiaodian";

            ThePoint_2.width            = 7;
            ThePoint_2.height           = 7;
            ThePoint_2.spriteName       = "dadian";

            ThePoint_3.width            = 4;
            ThePoint_3.height           = 4;
            ThePoint_3.spriteName       = "xiaodian";
        }
        else if ( index == 2 )          
        {
            ThePoint_1.width            = 4;
            ThePoint_1.height           = 4;
            ThePoint_1.spriteName       = "xiaodian";

            ThePoint_2.width            = 4;
            ThePoint_2.height           = 4;
            ThePoint_2.spriteName       = "xiaodian";

            ThePoint_3.width            = 7;
            ThePoint_3.height           = 7;
            ThePoint_3.spriteName       = "dadian";
        }
        else
        {
            index = 0;
            ThePoint_1.width            = 7;
            ThePoint_1.height           = 7;
            ThePoint_1.spriteName       = "dadian";

            ThePoint_2.width            = 4;
            ThePoint_2.height           = 4;
            ThePoint_2.spriteName       = "xiaodian";

            ThePoint_3.width            = 4;
            ThePoint_3.height           = 4;
            ThePoint_3.spriteName       = "xiaodian";
        }
        index++;
    }

}
