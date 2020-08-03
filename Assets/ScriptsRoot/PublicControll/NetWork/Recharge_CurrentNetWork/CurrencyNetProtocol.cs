using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
public struct SYNC_CURRENCY_ALLDATA                                         // 同步所有货币
{
    public Head head;
    public long nDiamond;                               // 钻石数量
    public long nCoins;                                 // 金币数量
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct SYNC_CURRENCY_DATA                                                   // 同步货币
{
    public Head head;
    public int nCurrencyType;                           // 货币类型 (1: 钻石<Diamond> 2:金币<Coins>)
    public long nValue;                                 // 货币数量
}
[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
struct SYNC_CURRENCY_MONEY_DATA                                             // 同步充值金额
{
    public Head head;
    public int nMoney;                                  // 充值金额
}