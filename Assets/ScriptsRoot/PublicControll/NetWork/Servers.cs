using UnityEngine;
using System.Collections;

public class Servers : IServers
{
    private ServerAddress _ServoAddress = new ServerAddress() { ProtocolType = ServerProtocolType.http, IP = Define.LOCAL_SERVO_IP, port = Define.LOCAL_SERVO_PORT };
    private ServerAddress       _GameServerAddress      = new ServerAddress();

    public ServerAddress        ServoAddress                   // 登录服地址
    {
        set { this._ServoAddress = value; }
        get { return this._ServoAddress; }
    }
    public ServerAddress        GameServerAddress              // 游戏服务地址
    {
        set { this._GameServerAddress = value; }
        get { return this._GameServerAddress; }
    }
}


