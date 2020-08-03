using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet = CharSet.Ansi)]
public struct CUST_STRING_32                                               // 自定义字符串解析
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string custString32;
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct SYNC_PLAYER_ATTR                                              // 玩家属性同步
{
    public Head head;
    public PLAYER_ATTR_TYPE playerAttrType;
    public int nValue;
}

