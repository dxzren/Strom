using UnityEngine;
using System.Collections;
using System.Net.Sockets;
/// <summary>   客户端连接  </summary>
 
public interface ISocket
{
    Socket GetSocket();                                                     // 获取Socket底层实例
    bool StartHeartBeat { get; }                                            // 开始心跳
    void SocketConnection(string IP, int port, bool isGameServer);          // 与服务器建立连接
    void SocketThreadQuit();                                                // 关闭Socket线程
    void SendRequest(byte[] data);                                          // 发送请求
}
