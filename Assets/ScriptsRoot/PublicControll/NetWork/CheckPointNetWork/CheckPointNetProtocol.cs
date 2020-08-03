using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_CHECKPOINT_Main                                           // 关卡入口 关卡信息请求              
{
    public Head Head;
    public char temp;
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_CHECKPOINT_ResetChallengeTimes                            // 重置关卡挑战次数请求               
{
    public Head Head;
    public int nCheckPointID;                           // 关卡ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_ResetChallengeTimes                            // 重置关卡挑战次数回调               
{
    public Head Head;
    public byte isSuccess;                           // 成功:0 失败:1
}



[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_CHECKPOINT_Challenge                                      // 挑战关卡请求                       
{
    public Head Head;
    public int nCheckPointID;                           // 关卡ID
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_Challenge                                      // 挑战关卡回调                       
{
    public Head Head;
    public byte isSuccess;                                // 0:成功 1:失败
}

/// <summary>
/// 6.扫荡(多次扫荡会出现客户端与服务器可扫荡次数不一致问题，服务器可能会多扫荡，这样可能体力不足，所以如果可挑战次数不一致的时候回复精英关卡的已挑战次数，客户端重新判断；正常情况就返回扫荡结果)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_CHECKPOINT_Sweep                                          // 扫荡关卡请求                       
{
    public Head Head;
    public int nCheckPointID;                           // 关卡ID
    public short cleanUpTimes;                          // 扫荡次数
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_Sweep                                          // 扫荡关卡回调                       
{
    public Head Head;       
    public short thisSize;                              // 本次数据数量
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public ItemData[] ItemDataList;                     // 扫荡物品列表
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_CHECKPOINT_ChallengeSuccess                               // 关卡挑战成功请求                   
{
    public Head Head;
    public int nCheckPointID;                           // 关卡ID
    public short PassStarLv;                            // 通关星级
    public short HerosNum;                              // 英雄数量(本条消息数据长度:6) 
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public int[] HeroIDList;                            // 英雄ID列表
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_ChallengeSuccess                               // 关卡挑战成功回调                   
{
    public Head Head;
    public byte isSuccess;                              // 0:成功 1:失败
    public byte thisSize;                               // 本条数据长度(MAX:10)
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public ItemData[] ItemDataList;                     // 物品列表
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct REQ_CHECKPOINT_StarBoxAward                                   // 星级宝箱奖励请求                   
{
    public Head Head;
    public short starBoxLv;                             // 星级宝箱等级
    public byte checkPointType;                         // 关卡类型 (1:普通 2:精英)
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_StarBoxAward                                   // 星级宝箱奖励回调                   
{
    public Head Head;
    public short StarBoxAwardLv;                        // 星级宝箱奖励等级
}



[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_PassedNorMaxCP_ID                              // 通过的普通最大关卡ID回调               
{
    public Head Head;
    public int nCheckPointID;
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_Normal_Info                                    // 普通大关卡信息回调                 
{
    public Head Head;
    public short total;                                 // 总数据数量
    public short thisSize;                              // 本条消息数据数量(Max:10)
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public CHECKPOINT_DATA_Normal[] CheckPointNormalList;// 普通关卡星级列表
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_Elite_Info                                     // 精英关卡信息回调                   
{
    public Head Head;
    public short total;                                 // 总数据数量
    public short thisSize;                              // 本条消息数据数量(Max:10)
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public CHECKPOINT_DATA_Elite[] CheckPointEliteList; // 精英关卡星级列表
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_Nor_Chaper                                      // 普通章节数据               
{
    public Head Head;
    public short gotNormalStarBoxLv;                      // 已领取普通关卡星级宝箱
    public short gotEliteStarBoxLv;                       // 已领取精英关卡星级宝箱
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_Elite_Chaper                                   // 精英章节数据     
{
    public Head Head;
    public short thisSize;                              // 本条数据长度(MAX:20)
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] Elite_GotStarBoxList;                 // 已领取精英星级宝箱列表
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_CHECKPOINT_ChallengeTimes                                 // 挑战次数回调                       
{
    public Head Head;
    public byte isSuccess;                              // 1:表示其他验证失败；0: 则为代表扫荡次数不一致，读取下面的已挑战次数，重新刷新
    public short haveChallangeTimes;                    // 可以挑战次数
}



[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct CHECKPOINT_DATA_Normal                                        // 普通大关卡数据                     
{
    public int nCheckPointID;                           // 关卡ID
    public short starLv;                                // 星级等级
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct CHECKPOINT_DATA_Elite                                         // 精英关卡数据                       
{
    public int nCheckPointID;                           // 关卡ID
    public short starLv;                                // 星级等级
    public short challengeTimes;                        // 挑战次数
    public short resetTimes;                            // 重置次数
}
