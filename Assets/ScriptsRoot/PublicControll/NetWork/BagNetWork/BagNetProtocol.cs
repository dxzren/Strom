using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet =CharSet.Ansi)]        // 背包列表请求，强化相应
public struct REQ_BAG_LIST               
{
    public Head Head;                   //头文件
    public byte none;                   //任意值均可，无意义
}

[StructLayout (LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]          // 背包列表相应，该协议分多条回复,每条包含10组数据
public struct RET_BAG_LIST
{
    public Head Head;                   
    public short PackTotalCount;                        //发包的总数量
    public short Size;                                  //本条数据数量
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public BagData[] bagList;                           //物品列表
}

[StructLayout (LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]          // 物品数据（暂时不用）
public struct BagData
{                  
    public int nItemID;                                 //物品ID
    public int nNum;                                    //物品数量
    public int nGetTime;                                //获得时间
    public byte Type;                                   //物品类型
}

[StructLayout (LayoutKind.Sequential,Pack=1,CharSet=CharSet.Ansi)]          
struct REQ_BAG_SellItem                                                     // 出售物品请求                
{
    public Head Head;
    public SellItem Data;
}
[StructLayout (LayoutKind.Sequential,Pack = 1,CharSet = CharSet.Ansi)]
public struct RET_BAG_SellItem                                              // 出售物品回调                
{
    public Head Head;
    public byte isSuccess;                 // 0：成功，1：失败
}

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet = CharSet.Ansi)]        
struct RET_USE_SOULBAG                                                      // 魂石包使用返回              
{
    public Head Head;                   
    public int SoulID1;                 // 魂石包小ID
    public int Count1;                  // 魂石包小数量
    public int SoulID2;                 // 魂石包中ID
    public int Count2;                  // 魂石包中数量
    public int SoulID3;                 // 魂石包大ID
    public int Count3;                  // 魂石包大数量
}

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet = CharSet.Ansi)]       // 合成
struct MergeItem
{
    public Head Head;                   
    public int TargetItemID;            // 合成目标的ID
    public short TargetNum;             // 合成目标的数量
}

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet =CharSet.Ansi)]        // 使用物品
struct REQ_USE_ITEM
{
    public Head Head;                   

    public int HeroID;                  // 对英雄使用时需附上英雄ID，否则为0即可
    public int ItemID;                  // 物品ID
    public short Num;                   // 物品使用数量
}

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet =CharSet.Ansi)]        // 背包内卖出的物品，非公用物品结构体
struct SellItem
{ 
    public int ItemID;                  // 物品ID
    public short Num;                   // 物品数量
}

[StructLayout(LayoutKind.Sequential,Pack = 1,CharSet =CharSet.Ansi)]        // 物品数据
public  struct ItemData
{
    public int ItemID;                  // 物品ID
    public int Num;                     // 数量数量
    public byte Sailed;                 // 0：已卖出，1：未卖出
}

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct RET_BAG_ChangeItem                                            // 物品变化
{
    public Head head;
    public short total;                                 // 物品总数
    public short size;                                  // 本条数据长度
    public int time;                                    // 添加物品时间戳(修改物品和删除物品,此时间无效)
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public ITEM_DATA[] itemDataList;                    // 物品列表
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct ITEM_DATA                                                     // 物品数据
{
    public int nItemID;             // 物品ID
    public byte itemType;           // 物品类型
    public int nItemNum;            // 修改后的物品数量
}
