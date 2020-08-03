using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;

///<summary> 全局服务器回调处理 </summary>
public class GlobalResponseHandler : IResponseHandler
{
    private IEventDispatcher dispatcher = null;
    private static GlobalResponseHandler _Instance = null;

    public static GlobalResponseHandler Instance()                  
    {
        if(_Instance == null)
        {
            _Instance = new GlobalResponseHandler();
        }
        return _Instance;
    }

    ///<summary> 处理服务器返回消息后_分发事件 </summary>  
    public bool ProtocolHandler(byte[] msg,short type1,short type2)                             // msg: 消息 type1:通信大类 type2:通信的具体内容类别 
    {
        if      ((eMsgType)type1 == eMsgType._MSG_PLAYER_MODULE)                                // (31)玩家模块        < Player >             
        {
            switch((PLAYER_CMD)type2)
            {
                case PLAYER_CMD.PLAYER_SYNC_CURRENCY:                       // (52)_玩家同步货币            
                    {
                        Debug.Log("ProtocolHandler: PLAYER_CMD.PLAYER_SYNC_CURRENCY!");
                        SYNC_CURRENCY_DATA CurrencyDataObj = new SYNC_CURRENCY_DATA();
                        CurrencyDataObj = (SYNC_CURRENCY_DATA)Util.BytesToStruct(msg, msg.Length, CurrencyDataObj.GetType());
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_SYNC_CURRENCY, CurrencyDataObj);    // 分发事件
                        return true;
                    }
                case PLAYER_CMD.PLAYER_SYNC_ATTRIBUTE:                      // (53)_玩家同步属性            
                    {
                        Debug.Log("ProtocolHandler: PLAYER_CMD.PLAYER_SYNC_ATTRIBUTE!:玩家同步属性");
                        SYNC_PLAYER_ATTR PlayerAttObj = new SYNC_PLAYER_ATTR();
                        PlayerAttObj = (SYNC_PLAYER_ATTR)Util.BytesToStruct(msg, msg.Length, PlayerAttObj.GetType());
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_SYNC_ATTRIBUTE, PlayerAttObj);
                        return true;
                    }
                case PLAYER_CMD.PLAYER_SYNC_ERROR_CODE:                     // (54)_玩家同步错误码          
                    {
                        Debug.Log("ProtocolHandler: PLAYER_CMD.PLAYER_SYNC_ERROR_CODE");
                        ErrorCode ErrorCodeObj = new ErrorCode();
                        ErrorCodeObj = (ErrorCode)Util.BytesToStruct(msg, msg.Length, ErrorCodeObj.GetType());
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_PLAYER_MODULE, (int)PLAYER_CMD.PLAYER_SYNC_ERROR_CODE, ErrorCodeObj);
                        return true;
                    }
                default:
                    Debug.Log("(PLAYER_CMD)type2 Case: 未适配类型?");
                    return false;
            }
        }
        else if ((eMsgType)type1 == eMsgType._MSG_HERO_MODULE)                                  // (32)英雄模块        < Hero >               
        {
            switch((HERO_CMD)type2)
            {
                case HERO_CMD.RET_HERO_AddHero:                             // (51)添加英雄             
                    {
                        Debug.Log("ProtocolHandler: HERO_CMD.PLAYER_SYNC_CURRENCY!");
                        RET_HERO_DATA HeroDataObj = new RET_HERO_DATA();
                        HeroDataObj = Global_CustomDecData.DecAddHeroData(msg);
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_HERO_MODULE, (int)HERO_CMD.RET_HERO_AddHero, HeroDataObj);
                        return true;
                    }
                case HERO_CMD.RET_SYNC_HERO_HeroLvExp:                      // (53)更改英雄经验等级     
                    {
                        Debug.Log("ProtocolHandler: RET_SYNC_HERO_HeroLvExp!");
                        SYNC_HeroLvExp ReHeroLvExp = new SYNC_HeroLvExp();
                        ReHeroLvExp = (SYNC_HeroLvExp)Util.BytesToStruct(msg, msg.Length, ReHeroLvExp.GetType());
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_HERO_MODULE, (int)HERO_CMD.RET_SYNC_HERO_HeroLvExp, ReHeroLvExp);
                        return true;
                    }
            }
        }
        else if ((eMsgType)type1 == eMsgType._MSG_BAG_MODULE)                                   // (40)背包物品模块    < Item >               
        {
            switch((BAG_CMD)type2)
            {
                case BAG_CMD.RET_Push_BAG_ChangeItem:                       // 物品变化         
                    {
                        Debug.Log("ProtocolHandler: BAG_CMD.RET_Push_BAG_ChangeItem");
                        RET_BAG_ChangeItem ReChangeItem = new RET_BAG_ChangeItem();
                        ReChangeItem = Global_CustomDecData.DecChangeItem(msg);
                        NetEventDispatcher.Instance().DispathcEvent((int)eMsgType._MSG_BAG_MODULE, (int)BAG_CMD.RET_Push_BAG_ChangeItem, ReChangeItem);
                        return true;
                    }
            }
        }

        /*  未启用功能
        else if ((eMsgType)Type1 == eMsgType._MSG_TASK_MODULE)                                  // 4.任务模块        < Task >               
        { }
        else if ((eMsgType)Type1 == eMsgType._MSG_EMAIL_MODULE)                                 // 5.邮件模块        < Email >              
        { }
        else if ((eMsgType)Type1 == eMsgType._MSG_FRIENDS_MODULE)                               // 6.好友模块        < Friend >             
        { }
        */

        return false;
    }
}
/// <summary>  自定义(手动) 解压网络数据 </summary>
public class Global_CustomDecData                                                  
{
    public static RET_HERO_DATA DecAddHeroData(byte[] msg)                  // 添加新英雄数据手动解析 (支持IOS)              
    {
        RET_HERO_DATA ReHeroData = new RET_HERO_DATA();
        int startIndex = 0;
        ReHeroData.head = (Head)Util.BytesToBase(ReHeroData.head,msg,ref startIndex);                                           // 0.头文件
        ReHeroData.nErrId = (int)Util.BytesToBase(ReHeroData.nErrId, msg, ref startIndex);                                      // 1.英雄ID
        ReHeroData.HeroData.nHeroID = (int)Util.BytesToBase(ReHeroData.HeroData.nHeroID, msg, ref startIndex);                  // 2.英雄等级
        ReHeroData.HeroData.nHeroLv = (int)Util.BytesToBase(ReHeroData.HeroData.nHeroLv, msg, ref startIndex);                  // 3.英雄经验
        ReHeroData.HeroData.nHeroExp = (int)Util.BytesToBase(ReHeroData.HeroData.nHeroExp, msg, ref startIndex);                // 4.英雄星级
        ReHeroData.HeroData.nHeroStarLv = (int)Util.BytesToBase(ReHeroData.HeroData.nHeroStarLv, msg, ref startIndex);          // 5.英雄品质
        ReHeroData.HeroData.nMedalLv = (int)Util.BytesToBase(ReHeroData.HeroData.nMedalLv, msg, ref startIndex);                // 6.英雄勋章等级
        ReHeroData.HeroData.nMedalExp = (int)Util.BytesToBase(ReHeroData.HeroData.nMedalExp, msg, ref startIndex);              // 7.英雄勋章经验
        ReHeroData.HeroData.nWingID = (int)Util.BytesToBase(ReHeroData.HeroData.nWingID, msg, ref startIndex);                  // 8.英雄翅膀ID
        ReHeroData.HeroData.nHeroEquips = new int[6];                                                                           // 9.装备列表
        for(int i= 0;i<6;i++)           
        {
            ReHeroData.HeroData.nHeroEquips[i] = (int)Util.BytesToBase(ReHeroData.HeroData.nHeroEquips[i], msg, ref startIndex);
        }
        ReHeroData.HeroData.nHeroSkillLv = new int[4];                                                                          // 10.技能等级列表
        for(int i = 0; i < 4;i++)       
        {
            ReHeroData.HeroData.nHeroSkillLv[i] = (int)Util.BytesToBase(ReHeroData.HeroData.nHeroSkillLv[i], msg, ref startIndex);
        }
        return ReHeroData;
    }

    public static RET_BAG_ChangeItem DecChangeItem(byte[] msg)              // 添加物品变化手动解析 (支持IOS)               
    {
        RET_BAG_ChangeItem ReChangeItem = new RET_BAG_ChangeItem();
        int startIndex = 0;
        ReChangeItem.head = (Head)Util.BytesToBase(ReChangeItem.head, msg, ref startIndex);     // 头文件
        ReChangeItem.total = (short)Util.BytesToBase(ReChangeItem.total, msg, ref startIndex);  // 物品总数
        ReChangeItem.size = (short)Util.BytesToBase(ReChangeItem.size, msg, ref startIndex);    // 本条数据长度
        ReChangeItem.time = (int)Util.BytesToBase(ReChangeItem.time, msg, ref startIndex);      // 添加物品时间戳(修改物品和删除物品,此时间无效)
        ReChangeItem.itemDataList = new ITEM_DATA[ReChangeItem.size];                           // 物品列表
        for (int i = 0;i < ReChangeItem.size;i++)
        {
            ITEM_DATA TempItemData = new ITEM_DATA();
            TempItemData.nItemID = (int)Util.BytesToBase(TempItemData.nItemID, msg, ref startIndex);
            TempItemData.itemType = (byte)Util.BytesToBase(TempItemData.itemType, msg, ref startIndex);
            TempItemData.nItemNum = (int)Util.BytesToBase(TempItemData.nItemNum, msg, ref startIndex);
            ReChangeItem.itemDataList[i] = TempItemData;
        }
        return ReChangeItem;
    }
}