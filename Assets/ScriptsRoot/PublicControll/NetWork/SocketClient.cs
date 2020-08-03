using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;
/// <summary>   客户端连接   </summary>
public class SocketClient : ISocket
{
    private bool            Go_on               = true;                                         // 是否继续接收消息
    private Socket          msocket             = null;                                         // 通讯套接
    private Thread          RecieveMethod       = null;                                         // 接受消息的线程

    public static bool      isServerList        = false;                                        // 接收服务器列表回调后，服务器在一段时间后，会主动断开连接，会默认启动断线重连.
                                                                                                // 这个作用是用来区分，此种情况不会启动自动重连逻辑
    public bool             HeartBeat           = false;                                        // 心跳连接
    public bool             StartHeartBeat                                                      // 开始心跳                    
    {
        get
        {
            return HeartBeat;
        }
    }
    public Socket           GetSocket()                                                         // 获取Socket底层实例          
    {
        return msocket;
    }
    public void             SendRequest(byte[] data)                                            // 发送请求                    
    {
        if(Application.internetReachability  == NetworkReachability.NotReachable)
        {
            PanelManager.sInstance.ShowNoticePanel(Define.NetBreakNotice);
        }
        else
        {
            try
            {
                ACC_LOGIN_GS obj = new ACC_LOGIN_GS();
                obj = (ACC_LOGIN_GS)Util.BytesToStruct(data, data.Length, obj.GetType());
                msocket.Send(data, 0, data.Length, SocketFlags.None);
            }
            catch(Exception e)
            {
                Debuger.Log(e.ToString());
            }
        }
    }
    public void             SocketConnection(string IP, int port, bool isGameServer)            // 与服务器建立socket连接      
    {
        Debug.Log("与服务器建立连接!");
        try
        {
            msocket                             = new Socket ( AddressFamily. InterNetwork,     /// 建立套接实例 < ,地址簇,套接类型,协议类型 >
                                                               SocketType.    Stream, 
                                                               ProtocolType.  Tcp        );
            IPAddress           ipAddress       = IPAddress.Parse(IP);                          /// strindg IP 转为 IPAddress 格式
            IPEndPoint          ipEP            = new IPEndPoint(ipAddress, port);
            msocket.Connect     (ipEP);                                                         /// 建立套接连接
        }
        catch (Exception e)
        {
            Debuger.Log         (e.ToString());
        }

        HeartBeat               = isGameServer;                                                 /// 是否保持游戏服连接

        if( RecieveMethod == null)                                                              /// 接收回调线程 
        {
            Go_on               = true;                                                         /// 接收消息
            RecieveMethod       = new Thread( new ThreadStart(this.Receive));                   /// 接收回调
            RecieveMethod.Start();                                                              /// 开启线程
        }
 
    }
    public void             SocketThreadQuit()                                                  // 关闭Socket线程              
    {
        try
        {
            if(msocket != null)
            {
                Go_on = false;
                closeSocket();
                RecieveMethod = null;
            }
        }
        catch(Exception e)
        {
            Debuger.Log(e.ToString());
        }
        Debuger.Log("主动断开SocketThread连接");
    }
    public void             Receive()                                                           // 接收服务器返回的消息         
    {
        while(Go_on)
        {
            try
            {
                if(msocket != null && msocket.Connected)
                {
                    #region 接受消息头
                    byte[]          length                      = new byte[2];                                      // 字节长度数组
                    int             size                        = msocket.Receive(length);                          // 接受消息头
                    if ( size == 0 )                                                                                // 消息头为 0 断开 
                    {
                        if(Go_on)                                                                               // 服务器主动断开连接        
                        {
                            Debuger.Log("Socket 断开连接 -- 消息长度为0");
                            ReConnect();
                        }
                        break;
                    }
                    #endregion
//                    Debug.Log       ("消息头:length: " + length.Length.ToString() + "|| size: " + size.ToString());
                    short           lengthPackage               = BitConverter.ToInt16(length, 0);                  // 根据消息头长度确定包的长度
                    byte[]          data                        = new byte[lengthPackage - 2];                      // 数据 字节数组
                    size                                        = msocket.Receive(data);                            // 接收数据包
                    if (size == 0)                                                                       
                    {
                        if(Go_on)
                        {
                            Debuger.Log                     ("Socket 断开连接 -- 消息长度为0");
                            ReConnect();
                        }
                        break;
                    }
                    byte[]          dataPackage                 = new byte[lengthPackage];
                    Array.Copy                                  (length, 0, dataPackage, 0, 2);
                    Array.Copy                                  (data, 0, dataPackage, 2, data.Length);
//                    Debug.Log       ("消息包:dataPackage: " + dataPackage.Length.ToString());
                    Resolution                                  (dataPackage);
                }
                else
                {
                    Debuger.Log("Socket is null ro not Connected");
                    Go_on = false;
                    RecieveMethod = null;
                    break;
                }
            }
            catch (ThreadAbortException exp)
            {
                Debuger.Log("Receive Thread is Abort ThreadAbortException = " + exp.ToString());
                Go_on = false;
                RecieveMethod = null;
                break;
            }
            catch(Exception error)
            {
                Debuger.Log("Receive Thread is Exception = " + error.ToString());
                Go_on = false;
                RecieveMethod = null;
                break;
            }
            Thread.Sleep(10);
        }
    }
    private void            ReConnect()                                                         // 重新连接                    
    {
        if(!isServerList)                                                   /// 不是获取服务器列表信息,就启动断线重连                      
        {
            HeartBeat = false;
            BreakToContinue();
        }
        else                                                                /// 接受服务器列表回调后,服务器会主动断开连接,防止启动断线重连,所以主动断开连接         
        {
            SocketThreadQuit();
        }
    }
    public void             BreakToContinue()                                                   // 断线重连                    
    {
        Debug.Log("断线重连");
        NetEventDispatcher.Instance().DispathcEvent( (int) eMsgType._MSG_LOGIN_CLIENT_LS, 
                                                     (int) LOGIN_CLIENT_GS_CMD.CUSTOM_RELOGIN, null);
    }
    public void             Resolution (byte[] data)                                            // 解析Socket包                
    {
        byte[]              data_2                      = new byte[2];
        Array.Copy          ( data, 2, data_2, 0, 2);
        short               type_1                      = BitConverter.ToInt16(data_2, 0);

        byte[]              data_3                      = new byte[2];
        Array.Copy          ( data, 4, data_3, 0, 2);
        short               type_2                      = BitConverter.ToInt16(data_3, 0);
        Debuger.LogWarning  ("收到服务器返回--：" + type_1 + "/" + type_2);
        foreach (IResponseHandler handler in NetHandlerManager.Instance().GetHandlerList())     // 回调处理
        {
            if( handler.ProtocolHandler(data, type_1, type_2))      break;
        }
    }
    private void            closeSocket()                                                       // 关闭Socket                 
    {
        ///Android关掉socket，触发msocket.Receive返回0，跳出循环.ios 关掉sokcet 会触发Exception 异常，在这里跳出循环               
        HeartBeat = false;
        if( IsSocketConnected( msocket ))
        {
            msocket.Shutdown    ( SocketShutdown.Both );
            msocket.Disconnect  ( true );
        }
        msocket.Close();
        msocket                                         = null;
    }
    private bool            IsSocketConnected(Socket socket)                                    // 是否建立套接连接            
    {
        #region remarks
        /********************************************************************************************
     * 当Socket.Conneted为false时， 如果您需要确定连接的当前状态，请进行非阻塞、零字节的 Send 调用。
     * 如果该调用成功返回或引发 WAEWOULDBLOCK 错误代码 (10035)，则该套接字仍然处于连接状态； 
     * 否则，该套接字不再处于连接状态。
     * Depending on http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.connected.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-2
    ********************************************************************************************/
        #endregion
        #region 过程
        // This is how you can determine whether a socket is still connected.
        bool connectState = true;
        bool blockingState = socket.Blocking;
        try
        {
            byte[] tmp = new byte[1];

            socket.Blocking = false;
            socket.Send(tmp, 0, 0);
            //Console.WriteLine("Connected!");
            connectState = true;
        }
        catch (SocketException e)
        {
            // 10035 == WSAEWOULDBLOCK
            if (e.NativeErrorCode.Equals(10035))
            {
                //Console.WriteLine("Still Connected, but the Send would block");
                connectState = true;
            }

            else
            {
                //Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
                connectState = false;
            }
        }
        finally
        {
            socket.Blocking = blockingState;
        }

        //Console.WriteLine("Connected: {0}", client.Connected);
        return connectState;
        #endregion
    }
}
public class Socketclient //: ISocket                                               
{
    /// <summary>
    /// 接收服务器列表回调后，服务器在一段时间后，会主动断开连接，会默认启动断线重连.这个
    /// 作用是用来区分，此种情况不会启动自动重连逻辑
    /// </summary>
    public static bool      isServerList = false;
    private CoreSocket      CSocket = null;

    private bool            heartbeat = false;
    public bool             StartHeartBeat { get { return heartbeat; } }

    public void             SocketConnection(string IP, int Port, bool isGameServer)
    {
        CSocket = new CoreSocket(Resolution);

        CSocket.SocketConnection(IP, Port, null);
        heartbeat = isGameServer;
    }

    public void             SocketThreadQuit()
    {
        if (CSocket != null)
        {
            CSocket.RecloseSocket();
            CSocket = null;
        }
    }

    public void             SendRequest     (byte[] data)
    {
        CSocket.SendRequest(data, null);
    }

    public Socket           GetSocket()
    {
        return null;
    }

    /// <summary>
    /// 解析包
    /// </summary>
    /// <param name="data"></param>
    public void             Resolution      (byte[] data)
    {
        byte[]              d1                      = new byte[2];
        Array.Copy          (data, 2, d1, 0, 2);
        short               type1                   = BitConverter.ToInt16(d1, 0);

        byte[]              d2                      = new byte[2];
        Array.Copy          (data, 4, d2, 0, 2);
        short               type2                   = BitConverter.ToInt16(d2, 0);

        foreach (IResponseHandler handler in NetHandlerManager.Instance().GetHandlerList())
        {
            if (handler.ProtocolHandler(data, type1, type2))
            {
                break;
            }
        }
    }
}

public class CoreSocket                                                             
{
    private Action<byte[]>  Resolution                  = null;

    public Action           ConnectCallback, SendCallback;
    public Socket           socket                      = null;
    public                  CoreSocket  ( Action<byte[]> ReceiveExecute )            
    {
        Resolution          = ReceiveExecute;
    }

    public void             SocketConnection( string ip,int port,Action callback )              // 与服务器建立连接             
    {
        try
        {
            ConnectCallback                     = callback;
            socket                              = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress       ipAddr              = IPAddress.Parse(ip);
            IPEndPoint      iep                 = new IPEndPoint(ipAddr, port);
            Debuger.Log     ("Socket Connect begin with IP = " + ip + ",port = " + port);
            socket.Connect  (iep);
        }
        catch(Exception e)
        {
            Debuger.Log     (e.ToString());
        }
    }
    public void             SendRequest(byte[] data,Action callback)                            // 发送请求                    
    {
        try
        {
            SendCallback    = callback;
            socket.Send     (data, 0, data.Length, SocketFlags.None);
        }
        catch(Exception e)
        {
            Debuger.Log     (e.ToString());
        }
    }
    public void             RecloseSocket()                                                     // 重新关闭连接                
    {
        socket.Shutdown     (SocketShutdown.Both);
        socket.Disconnect   (true);
        socket.Close();
        socket              = null;
    }

}