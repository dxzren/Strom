using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using strange.extensions.command.impl;

/// <summary>  客户端准备完毕请求  </summary>
public class ClientReady_Command : EventCommand
{
    [Inject]
    public IStartData           startData               { set; get; }
    [Inject]
    public IGameData            gameData                { set; get; }
    [Inject]
    public IPlayer              player                  { set; get; }
    [Inject]
    public IHeroSysData         heroSys                 { set; get; }
    [Inject]
    public ICheckPointSys       checkPointSys           { set; get; }
    [Inject]
    public ISocket              socket                  { set; get; }
    [Inject]
    public IRequest             netRequset              { set; get; }


    public override void Execute()
    {
        Debuger.Log("ClientReady_Command");
        ClearOldGameData();                                                                     /// 清理旧数据
        Send_CLGC_CLIENT_READY_MSG();                                                           /// 客户端准备完毕 发送  < 31/3 >
        Send_REQ_RECHARGE_PLAYERID_MSG();                                                       /// 玩家ID请求 发送     < 51/3 >

    }
    
    private void                ClearOldGameData()                                              // 清理旧数据                  
    {
        Debuger.Log("====================|| 清理旧数据 ||======================");
        gameData.HeroSysRed = false;                                        // 英雄系统红点
        gameData.subServerT = 0;                                            // 服务器同步时间差
            
        player.HeroList.Clear();                                            // 玩家英雄列表
        player.EquipList.Clear();                                           // 装备列表
        player.FragmentList.Clear();                                        // 碎片列表
        player.PropList.Clear();                                            // 道具列表

        player.WingList.Clear();                                            // 翅膀列表
        player.GetNormalCheckPointStars.Clear();                            // 获得普通关卡的星数
        player.GetEliteCheckPointStars.Clear();                             // 获得精英关卡的星数
        heroSys.skillPointTime = 0;                                         // 技能点时间

        checkPointSys.EliteChallangeTimesDic.Clear();                       // 精英关卡挑战次数
        checkPointSys.EliteCheckPointList.Clear();                          // 精英关卡列表
    }
    private void                Send_CLGC_CLIENT_READY_MSG()                                    // 客户端准备完毕 发送  < 31/3 >
    {
        Debuger.Log             ("客户端数据准备完毕!--: < 31/3 > ");
        CLGC_CLIENT_READY       ClinetReadyMsg              = new CLGC_CLIENT_READY();
        ClinetReadyMsg.Head.size                            = (short)Marshal.SizeOf(ClinetReadyMsg);
        ClinetReadyMsg.Head.type1                           = (short)eMsgType._MSG_PLAYER_MODULE;
        ClinetReadyMsg.Head.type2                           = (short)PLAYER_CMD.PLAYER_REQ_CLIENT_READY;
        ClinetReadyMsg.Extra                                = 1;                                                    /// 错误码 1:成功 ...

        byte[] sendClientReady                              = Util.StructToBytes(ClinetReadyMsg);
        socket.SendRequest                                  (sendClientReady);                                      /// 客户端准备完毕请求
        Debuger.Log             ("同步信息请求(玩家信息,已有英雄,背包,佣兵,商城,服务器时间,任务,好友列表,邮箱列表)");
    }
    private void                Send_REQ_RECHARGE_PLAYERID_MSG()                                // 玩家ID请求 发送     < 51/3 > 
    {
        Debuger.Log("玩家帐号ID请求发送--: < 51/3 >");
        REQ_RECHARGE_PLAYERID   PlayerID                    = new REQ_RECHARGE_PLAYERID();
        PlayerID.Head.size                                  = (short)Marshal.SizeOf(PlayerID);
        PlayerID.Head.type1                                 = (short)eMsgType._MSG_RECHARGE_MODULE;
        PlayerID.Head.type2                                 = (short)eReCharge_CMD.RECHARGE_REQ_PlayerID;
        PlayerID.a                                          = '0';
        byte[]                  sendPlayerID                = Util.StructToBytes(PlayerID);
        socket.SendRequest      (sendPlayerID);                                                                     /// 玩家ID请求
    }
}

