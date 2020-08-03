using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using strange.extensions.dispatcher.eventdispatcher.api;

public interface IHeroSysNetWorkCallback
{
    void                        OnUpHeroQualityResponse     (EventBase obj);            // 升级品质回调 (32/55)
    void                        OnUpHeroStarResponse        (EventBase obj);            // 升级星级回调
    void                        OnUpHeroSkillLvResponse     (EventBase obj);            // 升级技能等级回调
    void                        OnWearEquipResponse         (EventBase obj);            // 穿戴装备回调
    void                        OnMergeEquipResponse        (EventBase obj);            // 合成装备回调
}