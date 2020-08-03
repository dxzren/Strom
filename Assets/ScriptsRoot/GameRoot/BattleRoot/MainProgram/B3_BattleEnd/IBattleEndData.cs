using UnityEngine;
using System.Collections;
using SimpleJson;
using CodeStage.AntiCheat.ObscuredTypes;
using StormBattle;

///-------------------------------------------------------------------------------------------------------------------------/// <summary>  战斗结束数据接口 </summary>
public interface IBattleEndData
{ 
    bool                            IsCancel                        { set; get; }                                           // 取消战斗
    bool                            IsTimeOut                       { set; get; }                                           // 时间结束
    bool                            IsVictor                        { set; get; }                                           // 战斗胜利

    ObscuredByte                    AliveCount                      { get; set; }                                           // 可攻击数量(存活数量)
    ObscuredByte                    DeathCount                      { get; set; }                                           // 死亡数量 
    ObscuredByte                    StarScore                       { get; set; }                                           // 星级评分

    Battle_PreMemData               PreMem_D                        { get; set; }                                           // 战斗前数据(玩家等级,玩家体力)
    JsonObject                      BattleReport                    { get; set; }                                           // 战斗记录
    void                            ClearData();    
}

///-------------------------------------------------------------------------------------------------------------------------/// <summary>  战斗结束数据 </summary>
public class BattleEndData : IBattleEndData
{
    public bool                     IsCancel                        { set; get; }                                           // 取消战斗
    public bool                     IsTimeOut                       { set; get; }                                           // 时间结束
    public bool                     IsVictor                        { set; get; }                                           // 战斗胜利

    public ObscuredByte             AliveCount                      { get; set; }                                           // 可攻击数量(存活数量)
    public ObscuredByte             DeathCount                      { get; set; }                                           // 死亡数量 
    public ObscuredByte             StarScore                       { get; set; }                                           // 星级评分

    public Battle_PreMemData        PreMem_D                        { get; set; }                                           // 战斗前数据(玩家等级,玩家体力)
    public JsonObject               BattleReport                    { get; set; }                                           // 战斗记录

    public void                     ClearData()                                                                             // 清理数据 
    {
        IsCancel                    = true;
        IsTimeOut                   = false;
        IsVictor                    = false;

        AliveCount                  = 0;
        DeathCount                  = 0;
        StarScore                   = 0;
        BattleReport                = new JsonObject();
    }
}
///-------------------------------------------------------------------------------------------------------------------------/// <summary>  战斗前数据(玩家等级,玩家体力) </summary>
public class Battle_PreMemData                                                                                           
{
    public ObscuredInt              PlayerLevel                     = 0;                                                    /// 玩家等级
    public ObscuredInt              Stamina                         = 0;                                                    /// 体力
}

