using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]       
public struct REQ_RECHARGE_MONTHCARD_TIME                                  // 月卡时间戳请求 
{
    public Head Head;
    public char none;
}


[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]       
public struct RET_RECHARGE_MONTHCARD_TIME                                  // 月卡时间戳回调 
{
    public Head Head;
    public int nMonthCreateTime;                           // 月卡时间戳
}

#region 请求充值次数                                                    //请求充值次数
[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct REQ_RECHARGE_TIMES                                            //充值次数请求
{
    public Head Head;
    public char a;
}

[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct REQ_RECHARGE_NONE
{
    public Head Head;
    public int a;
}
[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct RET_RECHARGE_TIMES                                            //充值次数回调
{
    public Head Head;
    public int nTotal;                                  // 总充值次数
    public int nPackNum;                                // 本包携带次数
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
    public RECHARGE_DATA[] RechargeDataList;            // 充值数据列表
}
public struct RECHARGE_DATA
{
    public int nCardID;                                 // 卡ID
    public int nTimes;                                  // 次数
}
#endregion

                                                                                  
[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct REQ_RECHARGE_PLAYERID                                     // 玩家ID请求
{
    public Head Head;
    public char a;                                      // '0'
}
[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct RET_RECHARGE_PLAYERID                                     // 玩家ID回调
{
    public Head Head;
    //删除
    //public int playerID;
    public byte havearged;                                              // 是否已充值
    //删除
    //public PaoMaDeng PMDData;
}

[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct PMDSingle                                                 // 跑马灯单挑
{
    public Head Head;
    public PaoMaDeng PMDData;
}
[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]
public struct PaoMaDeng                                                 // 跑马灯数据
{
    public int durtime;
    public int times;
    public byte color;
    [MarshalAs(UnmanagedType.ByValTStr,SizeConst=300)]
    public string content;
}

[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]       // 请求充值地址   （Client->Server）
struct REQ_RECHARGE_SERVER_URL
{
    public Head Head;
    public short SDKID;             //1:易接 2:U8 3:海外
}

[StructLayout(LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]       // 请求充值地址 返回 (Server->Client )
struct RET_RECHARGE_SERVER_URL
{
    public Head Head;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string sURL;                                 // 充值地址
}