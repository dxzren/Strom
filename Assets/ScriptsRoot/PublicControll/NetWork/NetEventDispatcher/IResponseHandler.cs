using UnityEngine;
using System.Collections;

public interface IResponseHandler
{
    ///<summary> 处理服务器返回消息 </summary>  
    bool ProtocolHandler(byte[] msg, short type1, short type2);        // msg: 消息 type1:通信大类 type2:通信的具体内容类别
}