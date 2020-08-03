using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;

public class MainNetWorkCallback : IMainNetWorkCallback
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { set; get; }

    [Inject]
    public IPlayer player { set; get; }
    [Inject]
    public IGameData gameData { set; get; }
    [Inject]
    public IMainData mainData { set; get; }

    public void OnBuyResponse(EventBase obj)                                                    // 购买回调            
    {
        PanelManager.sInstance.HideLoadingPanel();
        RET_PLAYER_BUY_SOMETHING BuyData = (RET_PLAYER_BUY_SOMETHING)obj.eventValue;

        if(BuyData.nErrorID == 0)
        {
            if (BuyData.nType == (int)eBuyType.BuyCoins)                    // 金币购买返回
            {
                mainData.coinsCrit = CoinsCrit((int)BuyData.lCoins);        // 本次暴击倍数
                mainData.currentbuyCoinsNum = (int)BuyData.lCoins;          // 本次购买金币数量
                player.BuyedCoinsTimes += 1;                                // 购买金币次数+1
                dispatcher.Dispatch(MainEvent.BuyCoins_Event);              // 购买金币
               //UMeng.Instance.UMengOnEvent("Exoense", "BuyGold", Data.Coins.ToString());
            }
            else if (BuyData.nType == (int)eBuyType.BuyStamina)             // 体力购买返回
            {
                //int num = Configs_NumberConsumeDiamond.sInstance.GetNumberConsumeDiamondDataByBuyNumber(player.BuyedStaminaTimes).PhysicalPowerConsume;
                //UMeng.Instance.UMengOnEvent("Exoense", "BuyStanima", num.ToString());
                Debug.Log("BuyStamina");
            }
            else if (BuyData.nType == (int)eBuyType.BuySkill)               // 购买技能返回
            {
                //int money = Configs_NumberConsumeDiamond.sInstance.GetNumberConsumeDiamondDataByBuyNumber(player.BuyedSkillTimes + 1).SkillPointConsume;
                //UMeng.Instance.UMengOnEvent("Exoense", "BuySkillPts", money.ToString());
                dispatcher.Dispatch(HeroSysEvent.RETBuySkill_Event);        // 购买技能回调 
            }
            dispatcher.Dispatch(EventSignal. UpdateInfo_Event);                    // 事件信号 更新
        }
        else
        {
            Debug.Log(BuyData.nType.ToString());
            PanelManager.sInstance.ShowNoticePanel("购买失败");
        }
    }

    public void OnChangeIconResponse(EventBase obj)                                             // 更换头像回调        
    {
        Debug.Log("更换头像回调");
        PanelManager.sInstance.HideLoadingPanel();
        RET_MAIN_CHANGE_ICON ChangeData = (RET_MAIN_CHANGE_ICON)obj.eventValue;

        player.PlayerHeadIconName = ChangeData.nNewIconID.ToString();       // 更换头像
        dispatcher.Dispatch(EventSignal. UpdateInfo_Event);               // 更新主界面
    }

    public void OnChangeNameResponse(EventBase obj)                                             // 更换名称回调        
    {
        Debug.Log("更换名称回调");
        PanelManager.sInstance.HideLoadingPanel();
        RET_PLAYER_RENAME PlayerRename = (RET_PLAYER_RENAME)obj.eventValue;

        if(PlayerRename.nErrorID == 0)
        {
            PanelManager.sInstance.HidePanel(SceneType.Main, UIPanelConfig.ChangePalyerNamePanel);
            dispatcher.Dispatch(StartEvent.GetNickNameEventCallBack_Event);                     // 玩家名称回调
            dispatcher.Dispatch(EventSignal. UpdateInfo_Event);                                 // 更新主界面
        }
        else
        {
            Debug.Log("更换名称失败,服务器错误码:" + PlayerRename.nErrorID);
        }
    }

    public void OnUpLvAwardResponse(EventBase obj)                                              // 领取升级奖励回调     
    {
        Debug.Log("升级奖励回调");
        PanelManager.sInstance.HideLoadingPanel();

        RET_PLAYER_UpLvAward UpLvAward = (RET_PLAYER_UpLvAward)obj.eventValue;
                                                                            // 弹出奖励界面
        if(UpLvAward.nErrorID == 0)
        {
            Debug.Log("升级奖励领取成功!");
            int awardID = 0;
            foreach(Configs_LeadingAwardData item in Configs_LeadingAward.sInstance.mLeadingAwardDatas.Values)
            {
                if(item.NeedLevel == player.LvRewardGot)
                {
                    awardID = Configs_LeadingAward.sInstance.GetLeadingAwardDataByBoxID(item.BoxID + 1) == null ? item.BoxID : item.BoxID + 1;
                    break;
                }
            }
            if(awardID == 0)
            {
                awardID = 1;
            }
            Configs_LeadingAwardData AwardData = Configs_LeadingAward.sInstance.GetLeadingAwardDataByBoxID(awardID);
            List<int> AwardList = new List<int>();
            AwardList.Add(AwardData.GiftID);
            PanelManager.sInstance.ShowAwardView(AwardList);

            int newID = 0;
            foreach(Configs_LeadingAwardData item in Configs_LeadingAward.sInstance.mLeadingAwardDatas.Values)
            {
                if(item.NeedLevel == player.LvRewardGot)
                {
                    newID = item.BoxID + 1;
                    if(Configs_LeadingAward.sInstance.GetLeadingAwardDataByBoxID(newID)!=null)
                    {
                        player.LvRewardGot = Configs_LeadingAward.sInstance.GetLeadingAwardDataByBoxID(newID).NeedLevel;
                    }
                    break;
                }
            }
            if(newID == 0)
            {
                player.LvRewardGot = Configs_LeadingAward.sInstance.GetLeadingAwardDataByBoxID(newID + 1).NeedLevel;
            }
            dispatcher.Dispatch(EventSignal. UpdateInfo_Event);
        }
        else
        {
            Debug.Log("领取升级奖励失败,错误码:" + UpLvAward.nErrorID);
        }
    }

    public void TimeSyncResponse(EventBase obj)                                                 // 同步时间            
    {
        SYNC_CLOCK Time = (SYNC_CLOCK)obj.eventValue;

        System.DateTime newTime = Util.StampToDateTime(Time.time);

        if(Util.StampToDateTime(Time.time).Hour == 0)
        {
            dispatcher.Dispatch(EventSignal. NewDayRefresh_Event);           // 每天更新服务器发送的推送通知    
        }
    }

    public int CoinsCrit(int data)                                                              // 金币暴击倍数       
    {
        return (data / (20000 + player.PlayerLevel * 100 + (player.BuyedCoinsTimes - 1) * (800 + player.PlayerLevel * 4)));
    }
}

