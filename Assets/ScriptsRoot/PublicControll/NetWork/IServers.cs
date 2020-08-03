using UnityEngine;
using System.Collections;
using LinqTools;
using System.Text;

public interface IServers
{
    ServerAddress               GameServerAddress       { set; get; }
    ServerAddress               ServoAddress            { set; get; }
}

public class ServerAddress                                                                      // 服务器地址
{
    private ServerProtocolType  _ProtocolType           = ServerProtocolType.http;
    public  ServerProtocolType  ProtocolType
    {
        get { return this._ProtocolType; }
        set { this._ProtocolType = value; }
    }
    public string               IP                      { set; get; }
    public int                  port                    { set; get; }

    public override string      ToString()
    {
        return ProtocolType + "://" + IP + ":" + port.ToString() + "/";
    }
}
public enum                     ServerProtocolType                                              // 协议类型
{
    http,
    https
}