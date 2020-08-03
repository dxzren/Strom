using UnityEngine;
using System.Collections;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;
///-------------------------------------------------------------------------------------------------------------------------/// <summary> 英雄系统网络回调</summary>
public class HeroSysNetWorkCallback : IHeroSysNetWorkCallback
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]                                                    // 环境派发器的注入
    public IEventDispatcher         dispatcher                      { set; get; }
    [Inject]
    public IPlayer                  player                          { set; get; }
    [Inject]
    public IHeroSysData             heroSys                         { set; get; }
    [Inject]
    public IGameData                gameData                        { set; get; }



    public void                     OnUpHeroQualityResponse         (EventBase obj)                                         // 英雄升阶品质回调         
    {
        RET_HeroUpQuality ReHeroUpQuality = (RET_HeroUpQuality)obj.eventValue;
        Debug.Log("英雄品质升阶回调:" + ReHeroUpQuality.nHeroID);

        PanelManager.sInstance.HideLoadingPanel();
        heroSys.TempHero.Quality = (HeroQuality)ReHeroUpQuality.nQuality;                       // 当前选中英雄的品质
        heroSys.TempHero.EquipList.Clear();                                                     // 清理当前选中英雄的装备列表

        if(player.IsGuide)
        {
            dispatcher.Dispatch(GuideEvent.PanelChanged_Event, "UIs/HeroSys/UpStarOrLevelSuccessPanel");            // 打开升级,升阶成功界面
        }
        dispatcher.Dispatch(HeroSysEvent.RETUpQuality_Event);                                   // 升级品质回调 事件

        FightForce newFightForct = new FightForce();                                            // 更新战斗力
        newFightForct.heroID = ReHeroUpQuality.nHeroID;
        newFightForct.item = FightForceType.HeroUpQuality;
    }

    public void                     OnUpHeroStarResponse            (EventBase obj)                                         // 英雄升星回调             
    {
        RET_HeroUpStar ReHeroUpStar = (RET_HeroUpStar)obj.eventValue;
        Configs_HeroData CHeroData = Configs_Hero.sInstance.GetHeroDataByHeroID(ReHeroUpStar.nHeroID);
        Debug.Log("英雄提升星级回调:" + ReHeroUpStar.nHeroID);

        PanelManager.sInstance.HideLoadingPanel();
        heroSys.TempHero.HeroStar = ReHeroUpStar.nHeroStarLv;

        gameData.HeroSysRed = false;


        if(CHeroData.HeroType != 4)                                         // 已有英雄 (非佣兵)
        {
            foreach(IHeroData T in player.HeroList)
            {
                if (T.ID == ReHeroUpStar.nHeroID)
                {
                    if(Util.IsCanWearEquip(T,player))                       // 是否有可穿戴装备
                    {
                        gameData.HeroSysRed = Util.IsCanWearEquip(T, player);
                        break;
                    }
                    if(Util.IsCanStarUp(T,player))                          // 是否可升星
                    {
                        gameData.HeroSysRed = Util.IsCanStarUp(T, player);
                        break;
                    }
                }
            }
        }
        dispatcher.Dispatch(GuideEvent.PanelChanged_Event, UIPanelConfig.UpSartOrLevelSuccessPanel);   // 打开升级,升阶成功界面
        dispatcher.Dispatch(GlobalEvent.RET_Red_Point_Event);               // 刷新主界面红点
        dispatcher.Dispatch(HeroSysEvent.RETUpStar_Event);                  // 升星回调 事件(回调界面)

        FightForce NewFightForce = new FightForce();                        // 战斗力更新
        NewFightForce.heroID = ReHeroUpStar.nHeroID;
        NewFightForce.item = FightForceType.HeroUpStar;

    }

    public void                     OnUpHeroSkillLvResponse         (EventBase obj)                                         // 英雄升级技能点回调       
    {
        RET_HeroUpSkill ReHeroSkill = (RET_HeroUpSkill)obj.eventValue;
        Debug.Log("英雄升级技能回调" + ReHeroSkill.nHeroID);
        int[] skillType = new int[2];

        foreach (IHeroData T in player.HeroList)
        {
            if (T.ID == ReHeroSkill.nHeroID)
            {
                if (ReHeroSkill.nSkillID == T.ActiveSkillLevel1)
                {
                    heroSys.TempHero.ActiveSkillLevel1 = ReHeroSkill.nSkillLv;
                    skillType[0] = (int)SkillType.ActiveSkill1;
                }
                else if (ReHeroSkill.nSkillID == T.ActiveSkillLevel2)
                {
                    heroSys.TempHero.ActiveSkillLevel2 = ReHeroSkill.nSkillLv;
                    skillType[0] = (int)SkillType.ActiveSkill2;
                }
                else if (ReHeroSkill.nSkillID == T.PassiveSkillLevel)
                {
                    heroSys.TempHero.PassiveSkillLevel = ReHeroSkill.nSkillLv;
                    skillType[0] = (int)SkillType.PassiveSkill;
                }
                else if (ReHeroSkill.nSkillID == T.UltSkillLevel)
                {
                    heroSys.TempHero.UltSkillLevel = ReHeroSkill.nSkillLv;
                    skillType[0] = (int)SkillType.UltSkill;

                }
            }
        }
        skillType[1] = ReHeroSkill.nSkillID;

        dispatcher.Dispatch(HeroSysEvent.RETUpSkillLv_Event);                                   // 升级技能回调 事件
        dispatcher.Dispatch(PropUpInfoEvent.Event_PropUp_HeroSkillUp,skillType);                // 英雄技能提升事件
        
        if(ReHeroSkill.nSkillLv > 1)
        {
            FightForce NewFightForce = new FightForce();                                        // 战斗力更新
            NewFightForce.heroID = ReHeroSkill.nHeroID;
            NewFightForce.item = FightForceType.HeroUpSkill;
        } 
    }

    public void                     OnWearEquipResponse             (EventBase obj)                                         // 英雄穿戴装备回调         
    {
        Debug.Log("英雄穿戴装备回调:");
        RET_HeroWearEquip ReHeroWearEquip = (RET_HeroWearEquip)obj.eventValue;                  
        PanelManager.sInstance.HideLoadingPanel();                                              // 隐藏加载面板
        heroSys.TempHero.EquipList.Clear();                                                     // 清空装备列表
        
        for(int i = 0;i < 6;i++)                                                                // 更新英雄装备
        {
            heroSys.TempHero.EquipList.Add((WearPosition)i, ReHeroWearEquip.HeroEquipList[i]);
        }

        if(heroSys.isAllEquip)                                                                  // 一键穿戴的逻辑块         
        {
            foreach( WearPosition key in heroSys.GetOneKeyAllEquipDic.Keys)
            {
                if(heroSys.GetOneKeyAllEquipDic[key] == heroSys.TempHero.EquipList[key])
                {
                    heroSys.GetOneKeyAllEquipDic.Remove(key);
                    break;
                }

                if(heroSys.GetOneKeyAllEquipDic.Count != 0 || heroSys.MergeOneKeyAllEquipDic.Count != 0)
                {
                    return;
                }
                                                                                                // 一键穿戴的逻辑都执行完成
                heroSys.isAllEquip = false;                             
            }
        }

        gameData.HeroSysRed = false;
        foreach(IHeroData key in player.HeroList)                                             // 更新所有英雄可穿戴红点   
        {
            Configs_HeroData CHeroData = Configs_Hero.sInstance.GetHeroDataByHeroID(key.ID);
            if(CHeroData.HeroType != 4)
            {
                if(Util.IsCanWearEquip(key,player))
                {
                    gameData.HeroSysRed = Util.IsCanWearEquip(key, player);
                    break;
                }
            }
        }

        dispatcher.Dispatch(PropUpInfoEvent.Event_PropUp_Hero);                                 // 道具提升英雄属性
        dispatcher.Dispatch(GlobalEvent.RET_Red_Point_Event);                                   // 主界面红点刷新
        dispatcher.Dispatch(HeroSysEvent.RefreshUI_Event);                                      // 英雄系统界面UI刷新

        FightForce newFightForce = new FightForce();                                            // 战斗力更新    
        newFightForce.heroID = heroSys.TempHero.ID;
        newFightForce.item = FightForceType.HeroUpEquip;          
    }

    public void                     OnMergeEquipResponse            (EventBase obj)                                         // 英雄合成装备回调         
    {
        Debug.Log("装备合成回调:");
        MergeItem ReMerge = (MergeItem)obj.eventValue;

        PanelManager.sInstance.HideLoadingPanel();
        if(heroSys.isAllEquip)
        {
            foreach (WearPosition key in heroSys.MergeOneKeyAllEquipDic.Keys)                   // 一键合成组件(装备字典)
            {
                if (heroSys.MergeOneKeyAllEquipDic[key] == ReMerge.TargetItemID)
                {
                    heroSys.tempEquipID = ReMerge.TargetItemID;
                    heroSys.equipGrid = key;
                    dispatcher.Dispatch(HeroSysEvent.WearEquip_Event, new int[] { heroSys.tempEquipID, heroSys.TempHero.ID });
                    heroSys.MergeOneKeyAllEquipDic.Remove(key);
                    break;
                }
            }
        }
        else
        {
            dispatcher.Dispatch(HeroSysEvent.RETMergeEquip_Event);                              // 合成装备回调事件
            dispatcher.Dispatch(BagEvent.EventMergeItemCallback);                               // 合成物品回调事件
        }
    }



}
