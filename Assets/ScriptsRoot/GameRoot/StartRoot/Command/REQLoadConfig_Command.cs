using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.api;


public class REQLoadConfig_Command : EventCommand
{
    [Inject]
    public IGameData gameData { set; get; }
    [Inject]
    public IPlayer playerData { set; get; }
    [Inject]
    public IRequest request { set; get; }
    [Inject]
    public IRequest configsRequest { set; get; }
    [Inject]
    public IStartData startData { set; get; }
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    MonoBehaviour mRoot;                                

    public override void Execute()                                                              // 执行
    {
        Debug.Log("10>    REQLoadConfig_Command.Execute()_开始读取配置ReadConfigs()");
        mRoot = contextView.GetComponent<MonoBehaviour>();                  // 环境获取组件
        ReadConfigs();                                                      // 读取配置
    }

    //          |````````````````````````````   Public Function  `````````````````````````````|
    //          |_____________________________________________________________________________|

    Dictionary<string, ProcessConfigData> ConfigNameDic = new Dictionary<string, ProcessConfigData>();
    int loadCount = 0;                                                                          // 加载数量
    public void OnComplete(string name,bool isError)                                            // 读取配置文件的回调
    {
        if (isError)
        {
            Debug.Log(name + "is Load Erro");
        }

        loadCount += 1;
        dispatcher.Dispatch(StartEvent.Process_Event, (float)loadCount / (float)ConfigNameDic.Count);               // 显示加载进度
        if (loadCount == ConfigNameDic.Count)                                                                       // 加载完毕
        {
            loadCount = 0;
            Debug.Log("12>    OnComplete().SceneManager.LoadScene(LogIn) 加载LogIn_Scene");
            if (Util.GetAccFile("SavedAccountInfos").Password != null)                                              // 读取本地账户文件
            {
                Debug.Log("获取本地帐号文件成功,加载 <LogIn> Scene");
                SceneManager.LoadScene("LogIn");                                                                    // 加载登录场景
            }
            else
            {
                Debug.Log("LoadConfig_Fail_返回 <Start> Scene!");
                PanelManager.sInstance.ShowPanel(SceneType.Start, UIPanelConfig.StartMoviePanel);                  // 显示Start场景 文本面板
            }
        }

    }

    //          |````````````````````````````   Private Function  ````````````````````````````|
    //          |_____________________________________________________________________________|

    private void ReadConfigs ()                                                                 // 读取配置
    {
        #region 配置数据读入
        ConfigNameDic.Add("Consts", CustomJsonUtil.LoadConfig);
        ConfigNameDic.Add("LanguageChinese", Language.LoadConfig);
        Debug.Log("Consts! LanguageChinese! over!");
        //说明：
        //加载Asset目录下的文件不需要后缀名 比如：Hero
        //加载本地系统目录下文件需要加后缀名 比如：Hero.json

        //-----------------------<< 全局设置(GameGlobal) >>--------------------------------------------------------------------------------
        ConfigNameDic.Add("NamesAndMaskWord",       Configs_NamesAndMaskWord.sInstance.InitConfiguration);          // 随机名和屏蔽字库
        ConfigNameDic.Add("Notice",                 Configs_Notice.sInstance.InitConfiguration);                    // 公告
        ConfigNameDic.Add("Dialogue",               Configs_Dialogue.sInstance.InitConfiguration);                  // 对话表
        ConfigNameDic.Add("PanelInformation",       Configs_PanelInformation.sInstance.InitConfiguration);          // 面板信息

        //-----------------------<< 玩家系统(Player) >>------------------------------------------------------------------------------------
        ConfigNameDic.Add("TroopsHeadS",            Configs_TroopsHeadS.sInstance.InitConfiguration);               // 玩家头像
        ConfigNameDic.Add("LeadingUpgrade",         Configs_LeadingUpgrade.sInstance.InitConfiguration);            // 玩家升级表与体力上限
        ConfigNameDic.Add("VIP",                    Configs_VIP.sInstance.InitConfiguration);                       // 玩家VIP
        ConfigNameDic.Add("NumberConsumeDiamond",   Configs_NumberConsumeDiamond.sInstance.InitConfiguration);      // 玩家各系统购买消耗表
        //-----------------------<< 英雄系统(Hero) >>---------------------------------------------------------------------------------------

        ConfigNameDic.Add("Hero",                   Configs_Hero.sInstance.InitConfiguration);                      // 英雄模版
        ConfigNameDic.Add("AttributeRelation",      Configs_AttributeRelation.sInstance.InitConfiguration);         // 英雄属性关系
        ConfigNameDic.Add("HeroStar",               Configs_HeroStar.sInstance.InitConfiguration);                  // 英雄星级
        ConfigNameDic.Add("HeroQuality",            Configs_HeroQuality.sInstance.InitConfiguration);               // 英雄品质
        ConfigNameDic.Add("HeroUpgrade",            Configs_HeroUpgrade.sInstance.InitConfiguration);               // 英雄升级经验

        ConfigNameDic.Add("NPCAbilityModifier",     Configs_NPCAbilityModifier.sInstance.InitConfiguration);        // NPC属性调整
        ConfigNameDic.Add("LibraryCard",            Configs_LibraryCard.sInstance.InitConfiguration);               // 英雄卡池
        ConfigNameDic.Add("LibrarySoul",            Configs_LibrarySoul.sInstance.InitConfiguration);               // 英雄魂石卡池
        ConfigNameDic.Add("NPCQuality",             Configs_NPCQuality.sInstance.InitConfiguration);                // NPC品质
        ConfigNameDic.Add("OccupationalAddition",   Configs_OccupationalAddition.sInstance.InitConfiguration);      // 英雄职业加成

        ConfigNameDic.Add("UltSkill",               Configs_UltSkill.sInstance.InitConfiguration);                  // 英雄大招技能
        ConfigNameDic.Add("ActiveSkill",            Configs_ActiveSkill.sInstance.InitConfiguration);               // 英雄主动技能
        ConfigNameDic.Add("PassiveSkill",           Configs_PassiveSkill.sInstance.InitConfiguration);              // 英雄被动技能
        ConfigNameDic.Add("SkillConsume",           Configs_SkillConsume.sInstance.InitConfiguration);              // 英雄技能消耗

        //-----------------------<< 背包物品(Item) >>---------------------------------------------------------------------------------------

        ConfigNameDic.Add("Soul",                   Configs_Soul.sInstance.InitConfiguration);                      // 魂石
        ConfigNameDic.Add("Equip",                  Configs_Equip.sInstance.InitConfiguration);                     // 装备
        ConfigNameDic.Add("Fragment",               Configs_Fragment.sInstance.InitConfiguration);                  // 碎片
        ConfigNameDic.Add("Prop",                   Configs_Prop.sInstance.InitConfiguration);                      // 道具
        ConfigNameDic.Add("LibraryEquip",           Configs_LibraryEquip.sInstance.InitConfiguration);              // 装备卡池

        //-----------------------<< 主界面功能系统(MainSys) >>------------------------------------------------------------------------------

        ConfigNameDic.Add("SystemNavigation",       Configs_SystemNavigation.sInstance.InitConfiguration);          // 系统导航表

        ConfigNameDic.Add("Gift",                   Configs_Gift.sInstance.InitConfiguration);                      // 礼包表
        ConfigNameDic.Add("Recharge",               Configs_Recharge.sInstance.InitConfiguration);                  // 充值表

        //-----------------------<< 公会系统(GuildSys) >>------------------------------------------------------------------------------

        //-----------------------<< 关卡与副本(CheckPoint_BattleMap) >>---------------------------------------------------------------------

        ConfigNameDic.Add("CheckPoint",             Configs_CheckPoint.sInstance.InitConfiguration);                // 关卡数据配置
        ConfigNameDic.Add("Chapter",                Configs_Chapter.sInstance.InitConfiguration);                   // 关卡章节表
        ConfigNameDic.Add("CheckPointDrop",         Configs_CheckPointDrop.sInstance.InitConfiguration);            // 关卡掉落
        ConfigNameDic.Add("NPCArray",               Configs_NPCArray.sInstance.InitConfiguration);                  // NPC阵容表
        ConfigNameDic.Add("StarBoxAward",           Configs_StarBoxAward.sInstance.InitConfiguration);              // 星级宝箱奖励表

        //-----------------------<< 动作特效_战斗系统(BattleSys) >>----------------------------------------------------------------------------------

        ConfigNameDic.Add("BattleScenePos",         Configs_BattleScenePos.sInstance.InitConfiguration);            // 战斗场景位置
        ConfigNameDic.Add("SceneBakerInfo",         Configs_SceneBakerInfo.sInstance.InitConfiguration);            // 场景烘焙相关
        ConfigNameDic.Add("ActionAndEffect",        Configs_ActionAndEffect.sInstance.InitConfiguration);           // 动作特效
        ConfigNameDic.Add("SkillBonesMatch",        Configs_SkillBonesMatch.sInstance.InitConfiguration);           // 技能特效骨骼点绑定
        ConfigNameDic.Add("AttackTrailBonePos",     Configs_AttackTrailBonePos.sInstance.InitConfiguration);        // 攻击轨迹骨骼

        ConfigNameDic.Add("ModelPermanentEffect",   Configs_ModelPermanentEffect.sInstance.InitConfiguration);      // 模型常驻特效
        ConfigNameDic.Add("BuffBindBone",           Configs_BuffBindBone.sInstance.InitConfiguration);              // BUFF骨骼绑定位置
        ConfigNameDic.Add("ShadowBone",             Configs_ShadowBone.sInstance.InitConfiguration);                // 影子骨骼路径
        ConfigNameDic.Add("SceneEffectInfo",        Configs_SceneEffectInfo.sInstance.InitConfiguration);           // 场景特效配置

#if (!UNITY_EDITOR || UPDATERES) && !Force_Local
        ResourceManger.Instance.InitAssetBundles();
#endif
        dispatcher.Dispatch(StartEvent.SetLoadTitle_Event, "加载本地配置文件");                                      /// 设置加载界面标题
#if (UPDATERES || !UNITY_EDITOR) && !Force_Local
        mRoot.StartCoroutine(Util.AsyncLoadLoaclText(configsNameList,OnComplete));
#else
        foreach(string name in ConfigNameDic.Keys)
        {
//            Debug.Log("11>    ReadConfigs() .StartCoroutine.协调程序!");
            mRoot.StartCoroutine(Util.AsyncLoadLocalText(name, ConfigNameDic[name], OnComplete));                   // 开始协同程序 ( 异步加载文本配置, 读取配置文件的回调) 
        }
#endif
        #endregion
    }

}


