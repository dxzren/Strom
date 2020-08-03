using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_MAIN_CHANGE_ICON                                          // 更换玩家头像请求
{
    public Head head;
    public int nIconType;                               // 头像类型
    public int nIconID;                                 // 不同类型ID解析不同
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_MAIN_CHANGE_ICON                                          // 更换玩家头像回调
{
    public Head head;
    public int nNewIconID;                              // 新头像ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct MAIN_PMD_SINGLE                                               // 跑马灯单条                
{
    public Head Head;
    public MAIN_PMD_DATA PMDData;
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct MAIN_PMD_DATA                                                 // 跑马灯数据                
{
    public int nDurtime;                                // 持续时间
    public int nTimes;                                  // 次数
    public byte nColor;                                 // 颜色 (0:Black 1:Red 2:Green 3:Yellow 4:White)
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 300)]
    public string sPMDText;                             // 文本内容
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct GM                                                                   // GM后台指令                
{
    public Head head;
    public short nSize;                                 // Msg长度,小于64
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
    public string sMsg;                                 // 消息
}
