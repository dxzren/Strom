using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using LinqTools;
namespace StormBattle
{
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>   战斗数据初始化接口   </summary>
    public interface    IBattleStartData    
    { 
        bool                        IsJJCLevel                      { set; get; }                                           /// 竞技场段位挑战
        int                         rewardCoin                      { set; get; }                                           /// 奖励金币
        int                         CurrBattleWave                  { set; get; }                                           /// 当前进度

        string                      battleScene                     { set; get; }                                           /// 战斗场景
        string                      battleScene2                    { set; get; }                                           /// 战斗场景2

        object                      TempObjData                     { set; get; }                                           /// 事件参数(evt) 临时数据
        BattleType                  BattleType                      { set; get; }                                           /// 战斗地图

        List<int>                   DialogPanelIDList               { set; get; }                                           /// 对话框ID 列表
        List<IBattleMemberData>     OurMemberList                   { set; get; }                                           /// 我方成员列表 [Base]
        Dictionary<int, List<IBattleMemberData>>    OurMemListAtWaveDic     { set; get; }                                   /// 战斗波次_我方成员数据列表 
        Dictionary<int, List<IBattleMemberData>>    EnemyMemListAtWaveDic   { set; get; }                                   /// 战斗波次_敌方方成员数据列表


        string                      GetProLvText            ();                                                             /// 获取职业等级_文本
        JsonObject                  BattleDataToJsonObj     ();                                                             /// 战斗数据ToJSonObject

        void                        JsonObjToBattleData     (JsonObject inJsonObj);                                         /// JsonToBattleData
        void                        ExecPassSkOrTalentSk    ();                                                             /// 执行被动技能 or 天赋技能
        void                        Clear();                                                                                /// 清空数据

    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>   战斗数据初始化数据  </summary>
    public class        BattleStartData     : IBattleStartData
    {
        public bool                     IsJJCLevel              { set; get; }                                                                   /// 竞技场段位挑战
        public int                      rewardCoin              { set { _rewardCoin         = value; }   get { return _rewardCoin; } }          /// 奖励金币
        public int                      CurrBattleWave          { set { _startAtProcess     = value; }   get { return _startAtProcess; } }      /// 当前进度
        public string                   battleScene             { set { _battleScene        = value; }   get { return _battleScene; } }         /// 战斗场景

        public string                   battleScene2            { set { _battleScene2       = value; }   get { return _battleScene2; } }         /// 战斗场景2
        public object                   TempObjData             { set { _ObjData            = value; }   get { return _ObjData; } }              /// 其他数据--关卡ID,PVP对方数据
        public BattleType               BattleType              { set { _BattleType         = value; }   get { return _BattleType; } }           /// 战斗地图
        public List<int>                DialogPanelIDList       { set { _DialogPanelIDList  = value; }   get { return _DialogPanelIDList; } }    /// 对话框ID 列表   

        public List<IBattleMemberData>  OurMemberList           { set { _OurMemberList      = value; }   get { return _OurMemberList; } }        /// 我方成员列表
        public Dictionary<int, List<IBattleMemberData>> OurMemListAtWaveDic     { set { _OurMemListAtWaveDic    = value; }   get { return _OurMemListAtWaveDic; } }      /// 战斗波次_我方成员列表 
        public Dictionary<int, List<IBattleMemberData>> EnemyMemListAtWaveDic   { set { _EnemyMemListAtWaveDic  = value; }   get { return _EnemyMemListAtWaveDic; } }    /// 战斗波次_敌方成员列表 

        public string                   GetProLvText()                                                                                          // 获取职业等级_文本      
        {
            string                      TheProLvText                    = "";

            foreach ( var Item in OurMemberList)        
            {
                TheProLvText            += Item.MemberProLv + ",";
            }
            TheProLvText                = TheProLvText.TrimEnd(',') + ";";

            foreach ( var Item in EnemyMemListAtWaveDic[1])       
            {
                TheProLvText            += Item.MemberProLv + ",";
            }
            TheProLvText                = TheProLvText.TrimEnd(',');
            return                      TheProLvText;
        }
        public JsonObject               BattleDataToJsonObj()                                                                                   // BattleDataToJSon      
        {
            JsonObject                  JsonObj                         = new JsonObject();                                 // Json返回类型

            JsonObj.Add                 ("BattleType", (int)BattleType);                                                    /// 战斗地图(类型)
            JsonObj.Add                 ("battleScene", battleScene);                                                       /// 战斗场景
            JsonObj.Add                 ("battleScene2", battleScene2);                                                     /// 战斗场景2

            JsonArray                   DialogJsonObj                   = new JsonArray();                                  /// 添加 对话框 List
            DialogPanelIDList.ForEach   (T => DialogJsonObj.Add(T));
            JsonObj.Add                 ("DialogPanelIDList", DialogJsonObj);

            JsonArray                   OurJsonArray                    = new JsonArray();                                  /// 添加 我方成员字典 List

            JsonArray                   TempJsonArray                   = new JsonArray();
            _OurMemberList.ForEach      (T =>   TempJsonArray.Add (T.MemberDataToJsonObj()));
            OurJsonArray.Add            (TempJsonArray);
            JsonObj.Add                 ("OurMemberList", OurJsonArray);

            JsonArray                   EnemyJsonArrList                = new JsonArray();                                   /// 添加 敌方成员字典 List
            for (int i = 0; i < _EnemyMemListAtWaveDic.Count; i++)
            {
                JsonArray               EnemyJsonArray                  = new JsonArray();
                foreach (var Item in _EnemyMemListAtWaveDic[i+1])
                {
                    EnemyJsonArray.Add  (Item.MemberDataToJsonObj());
                }
                EnemyJsonArrList.Add    (EnemyJsonArray);
            }
            JsonObj.Add                 ("EnemyMemberListDic", EnemyJsonArrList);                                           

            return                      JsonObj;                                                                            /// 返回

        }
        public void                     JsonObjToBattleData(JsonObject inJsonObj)                                                               // JsonToBattleData      
        {
            this.Clear();
            List<IBattleMemberData>     TheMemList                      = new List<IBattleMemberData>();
            BattleType                  = (BattleType)Util.GetIntKeyValue   (inJsonObj, "BattleType");                      /// 战斗地图
            battleScene                 = Util.GetStringKeyValue            (inJsonObj, "battleScene");                     /// 战斗场景
            battleScene2                = Util.GetStringKeyValue            (inJsonObj, "battleScene2");                    /// 战斗场景2

            if (inJsonObj.ContainsKey("DialogPanelIDList"))                                                                 /// 对话框列表   
            {
                JsonArray               TempJsonArray                   = (JsonArray)inJsonObj["DialogPanelIDList"];;
                TempJsonArray.ForEach(T => DialogPanelIDList.Add        (Util.ParseToInt(T.ToString())));
            }

            _OurMemberList.Clear();                                                                                         // 添加我方成员到列表
            JsonArray                   OurJsonArray                    = inJsonObj["OurMemberList"] as JsonArray;          /// Our<字典>List
            for (int i = 0; i < OurJsonArray.Count; i++ )
            {
                JsonObject              TempObj                         = new JsonObject();
                TempObj                                                 = OurJsonArray[i] as JsonObject;
                OurMemberList.Add       ((IBattleMemberData)TempObj);
            }

            _EnemyMemListAtWaveDic.Clear();                                                                                 // 添加敌方成员到列表
            JsonArray                   EnemyJsonArrayList              = inJsonObj["EnemyMemberListDic"] as JsonArray;     /// Enemy<字典>List 
            for (int  i = 0; i < EnemyJsonArrayList.Count; i++)
            {
                JsonArray               EnemyJsonArray                  = new JsonArray();
                EnemyJsonArray                                          = EnemyJsonArrayList[i] as JsonArray;                
                for(int y = 0; y < EnemyJsonArray.Count; y++)
                {
                    JsonObject          TempObj                         = new JsonObject();
                    TempObj                                             = EnemyJsonArray[i] as JsonObject;
                    TheMemList.Add      ((IBattleMemberData)TempObj);
                }                
                _EnemyMemListAtWaveDic.Add     (i+1, TheMemList);
            } 

        }
        public void                     ExecPassSkOrTalentSk()                                                                                  // 执行被动技能和天赋技能  
        {
            ExecAllMemListPassSkill     ( OurMemberList, EnemyMemListAtWaveDic );                                           /// 执行所有成员列表 被动技能
        }

        public void                     Clear()                                                                                                 // 清空类数据             
        {
            CurrBattleWave              = 1;                                                                                    /// 进度清空
            _battleScene                = "";                                                                                   /// 战斗场景  清空
            _battleScene2               = "";                                                                                   /// 战斗场景2 清空
            _BattleType                 = BattleType.CheckPoint;                                                                /// 战斗类型 初始化

            _DialogPanelIDList.Clear();                                                                                         /// 对话框清空
            _OurMemberList.Clear();                                                                                             /// 我方成员列表 清空
            _EnemyMemListAtWaveDic.Clear();                                                                                     /// 敌方成员列表 清空
            GC.Collect();
        }



                                                                                                                                                ///< |英雄_NPC_成员数据列表 [MemberDataList] |> 
        public static List<IBattleMemberData> BuildHeroMemberList   ( Dictionary<PosNumType, IHeroData> inBattleHeroDic, Battle_Camp inCamp)    // 创建战斗我方英雄成员    
        {
            List<IBattleMemberData>     TheMemberD_List                 = new List<IBattleMemberData>();
            foreach(var Item in inBattleHeroDic)
            {
                BattleMemberData        TheBattleMemD                   = new BattleMemberData();       
                TheBattleMemD           = BattleMemberData.BuildFromHeroData( Item.Key,Item.Value);                                         /// 创建我方成员数据
                TheBattleMemD.BattleCamp                                =  Battle_Camp.Our;                                                 /// 成员阵营
                TheBattleMemD.MemberType                                =  Battle_MemberType.Hero;                                          /// 英雄类型
                TheMemberD_List.Add     (TheBattleMemD);
            }
            return                      TheMemberD_List;
        }
        public static Dictionary<int, List<IBattleMemberData>>      BuildNpcMemberDic_CP ( Configs_CheckPointData inCheckP_C)                   // 创建Npc成员列表( 关卡 ) 
        {
            Dictionary<int, List<IBattleMemberData>> TheNpcMemDic   = new Dictionary<int, List<IBattleMemberData>>();                       /// Npc成员数据字典

            for ( int i = 0; i < inCheckP_C.ArrayID.Count; i++)                                                                             /// Npc阵容数据 
            {
                int                     TempArrayID                 = inCheckP_C.ArrayID[i];                                                /// Npc阵容列表
                Debug.Log("ArrayID: " + inCheckP_C.ArrayID[i]);
                BattleMemberData        TempMemD                    = new BattleMemberData();                                               /// 成员数据实例
                Configs_NPCArrayData    NpcArray_C                  = Configs_NPCArray.sInstance.GetNPCArrayDataByArrayID(TempArrayID);     /// Npc阵容配置数据
                List<IBattleMemberData>     TheMemDataList          = new List<IBattleMemberData>();                                        /// Npc成员数据列表实例
            
                if (NpcArray_C.Number1 != 0)                                                                                                // 1号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 1, NpcArray_C.Number1, NpcArray_C.SkillNumber1, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number2 != 0)                                                                                                // 2号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 2, NpcArray_C.Number2, NpcArray_C.SkillNumber2, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number3 != 0)                                                                                                // 3号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 3, NpcArray_C.Number3, NpcArray_C.SkillNumber3, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number4 != 0)                                                                                                // 4号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 4, NpcArray_C.Number4, NpcArray_C.SkillNumber4, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number5 != 0)                                                                                                // 5号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 5, NpcArray_C.Number5, NpcArray_C.SkillNumber5, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number6 != 0)                                                                                                // 6号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 6, NpcArray_C.Number6, NpcArray_C.SkillNumber6, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number7 != 0)                                                                                                // 7号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 7, NpcArray_C.Number7, NpcArray_C.SkillNumber7, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number8 != 0)                                                                                                // 8号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 8, NpcArray_C.Number8, NpcArray_C.SkillNumber8, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                if (NpcArray_C.Number9 != 0)                                                                                                // 9号位  
                {
                    TempMemD            = BuildNpcMemberD(inCheckP_C, 9, NpcArray_C.Number9, NpcArray_C.SkillNumber9, inCheckP_C.BossID[i]);
                    TheMemDataList.Add  (TempMemD);
                }
                TheNpcMemDic.Add(i + 1, TheMemDataList);
            }
            foreach(var Item in TheNpcMemDic.Keys)
            {
                for(int i = 0; i < TheNpcMemDic[Item].Count; i++ )
                {
                    Debug.Log("TheNpcMemDic_Wave" + Item + "\t NPCID:"+"["+i +"]"+ TheNpcMemDic[Item][i].MemberID);
                }
            }
            return                      TheNpcMemDic;
        }


                                                                                                                                                ///<| 被动技能_天赋技能 [PassiveSkill_Talent] |>
        public void     ExecRoleTalentSkill     ( List<IBattleMemberData> inOurMemList, Dictionary<int, List<IBattleMemberData>> inEnemyMemDic) // 执行我方天赋技能         
        {
            IBattleMemberData           TheRoleHeroD                    = null;                                                                 /// 成员数据实例
            foreach (var Item in inOurMemList)                                                                                                  /// 确认阵容中的主角英雄      
            {
                if (Item.isRoleHero)
                {
                    TheRoleHeroD = Item;
                    break;
                }            
            }
            if (TheRoleHeroD == null)                                   return;

            int                         polarityCount                   = 0;                                                                    /// 元素属性累计 (冰,火,雷)
            foreach ( var Item in inOurMemList )                                                                                                /// 主角同元素英雄检索 (count)
            {
                if ( Item.MemberPolarity == TheRoleHeroD.MemberPolarity)
                {
                    polarityCount++;
                }
            }
            if (polarityCount < 2)                                      return;                                                                 /// 天赋技能 最少两人触发

            int                         skillIndex                      = polarityCount - 2;                                                    /// 天赋技能标签
            int                         talentLv                        = polarityCount - 1;                                                    /// 天赋技能等级 
            Configs_HeroData            TheHero_C       = Configs_Hero.sInstance.GetHeroDataByHeroID(TheRoleHeroD.MemberID);                    /// 主角英雄配置数据
            if ( TheHero_C == null || TheHero_C.Talent.Count <= skillIndex )                                                                    /// 技能验证                 
            {
                Debug.LogError      ("主角英雄天赋配置错误,英雄不存在 技能条目小于技能数数量");           return;
            }    

            int                         skillID                         = TheHero_C.Talent[skillIndex];                                         /// 技能数组获取技能ID
            Configs_PassiveSkillData    ThePassSkill_C  = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID(skillID);          /// 被动技能配置数据

            if (ThePassSkill_C == null )                                return;
            TheRoleHeroD.TalentSkillID                                  = skillID;                                                              /// 主角天赋技能ID
            ExecMemPassSkill            ( TheRoleHeroD, ThePassSkill_C, 0, inOurMemList, inEnemyMemDic);                                        /// 执行天赋技能 
        }
        public void     ExecAllMemListPassSkill ( List<IBattleMemberData> inOurMemDList,Dictionary<int, List<IBattleMemberData>> inEnemyMemDic) // 执行所有成员列表 被动技能 
        {
            foreach ( var Item in inOurMemDList)                                                                                            /// 我方执行被动技能
            {
                if ( Item.PassiveSkillID > 1 && Item.PassiveSkillLv > 1)
                {
                    Configs_PassiveSkillData    ThePassSkill_C  = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID(Item.PassiveSkillID);
                    if ( ThePassSkill_C != null)
                    {
                        ExecMemPassSkill(Item, ThePassSkill_C, Item.PassiveSkillLv, inOurMemDList, inEnemyMemDic);
                    }
                }
            }
            for (int i = 0; i < inEnemyMemDic.Count; i++)
            {
                foreach ( var Item in inEnemyMemDic[i+1])                                                                                          /// 敌方执行被动技能
                {
                    if ( Item.PassiveSkillID > 1 && Item.PassiveSkillLv > 1)
                    {
                        Configs_PassiveSkillData    ThePassSkill_C  = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID(Item.PassiveSkillID);
                        if ( ThePassSkill_C != null )
                        {
                            ExecMemPassSkill(Item, ThePassSkill_C, Item.PassiveSkillLv, inOurMemDList, inEnemyMemDic);
                        }
                    }
            }
            }

        }

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private int                     _rewardCoin                     { set; get; }                                                           /// 奖励金币
        private int                     _startAtProcess                 { set; get; }                                                           /// 当前进度
        private string                  _battleScene                    { set; get; }                                                           /// 战斗场景
        private string                  _battleScene2                   { set; get; }                                                           /// 战斗场景2

        private object                  _ObjData                        { set; get; }                                                           /// 其他数据--关卡ID,PVP对方数据
        private BattleType              _BattleType                     { set; get; }                                                           /// 战斗地图

        private List<int>               _DialogPanelIDList              = new List<int>();                                                      /// 对话框ID 列表
        private List<IBattleMemberData> _OurMemberList                  = new List<IBattleMemberData>();                                        /// 我方成员列表[Base]
        Dictionary<int, List<IBattleMemberData>> _OurMemListAtWaveDic   = new Dictionary<int, List<IBattleMemberData>>();                       /// 战斗波次_我方成员列表
        Dictionary<int, List<IBattleMemberData>> _EnemyMemListAtWaveDic = new Dictionary<int, List<IBattleMemberData>>();                       /// 战斗波次_敌方成员列表 

        private void                    ExecMemPassSkill ( IBattleMemberData inMemD, Configs_PassiveSkillData inPassSkill_C, int inPassSkillLv, // 执行成员被动技能
                                                          List<IBattleMemberData> inOurMemDataList, Dictionary<int, List<IBattleMemberData>> inEnemyMemDataDic )          
        {
            int                     campValue                       = 1;                                                                    /// 阵营(1:我方 2:敌方)
            float                   addPassValue                    = inPassSkill_C.BaseValue + inPassSkill_C.UpValue*inPassSkillLv +       /// 被动技能数值公式_(增加数值) 
                                                                      inPassSkill_C.BaseValue * inPassSkill_C.PerValue; 
            List<IBattleMemberData> TheMemberDataList               = new List<IBattleMemberData>();                                        /// 成员列表实例

            if      ( inPassSkill_C.RangeType == (int)Battle_Camp.Our )                                                                     /// 我方技能 目标范围  
            {
                campValue           = 1;                                                                                                    /// 阵营增减数值 <.本方正值: +  敌方负值: ->
                switch ( inPassSkill_C.EffectRange )
                {
                    case (int)SkillTargRange.AllMember:             EnemyMemListAtWaveDic = inEnemyMemDataDic;  break;                      /// 全体
                    case (int)SkillTargRange.Own:                   TheMemberDataList.Add(inMemD);              break;                      /// 本体
                    case (int)SkillTargRange.Ice:                                                                                           /// 冰系 
                        {
                            foreach(var Item in inOurMemDataList)
                            {
                                if (Item.MemberPolarity == (int)Polarity.Ice)
                                {
                                    TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int)SkillTargRange.Fire:                                                                                          /// 火系 
                        {
                            foreach (var Item in inOurMemDataList)
                            {
                                if (Item.MemberPolarity == (int)Polarity.Fire)
                                {
                                    TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int)SkillTargRange.Thunder:                                                                                       /// 雷系 
                        {
                            foreach (var Item in inOurMemDataList)
                            {
                                if (Item.MemberPolarity == (int)Polarity.Thunder)
                                {
                                    TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int)SkillTargRange.Phy:                                                                                           /// 物理 
                        {
                            foreach ( var Item in inOurMemDataList)
                            {
                                if (Item.MemberProAtt != (int)HeroPorAtt.Intellect)     TheMemberDataList.Add(Item);
                            }
                            break;
                        }
                    case (int)SkillTargRange.Magic:                                                                                         /// 魔法 
                        {
                            foreach( var Item in inOurMemDataList )
                            {
                                if (Item.MemberProAtt != (int)HeroPorAtt.Intellect)     TheMemberDataList.Add(Item);
                            }
                            break;
                        }
                    default:        Debug.LogError("配置出错,未识别技能范围!");                                   return;         
                }
            }
            else if ( inPassSkill_C.RangeType == (int)Battle_Camp.Enemy)                                                                    /// 敌方技能 目标范围  
            {
                campValue           = -1;                                                                                                   /// 阵营增减数值 <.本方正值: +  敌方负值: ->
                switch ( inPassSkill_C.EffectRange )  
                {
                    case (int) SkillTargRange.AllMember:             EnemyMemListAtWaveDic  = inEnemyMemDataDic;    break;                  /// 全体
                    case (int) SkillTargRange.Own:                                                                  break;                  /// 本体
                    case (int) SkillTargRange.Ice:                                                                                          /// 冰系 
                        {
                            for(int i = 0; i < inEnemyMemDataDic.Count; i++)
                            {
                                foreach (var Item in inEnemyMemDataDic[i+1])
                                {
                                    if (Item.MemberPolarity == (int)Polarity.Ice)           TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int) SkillTargRange.Fire:                                                                                          /// 火系 
                        {
                            for(int i = 0; i < inEnemyMemDataDic.Count; i++)
                            {
                                foreach (var Item in inEnemyMemDataDic[i+1])
                                {
                                    if (Item.MemberPolarity == (int)Polarity.Fire)          TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int) SkillTargRange.Thunder:                                                                                       /// 雷系 
                        {
                            for(int i = 0; i < inEnemyMemDataDic.Count; i++)
                            {
                                foreach (var Item in inEnemyMemDataDic[i+1])
                                {
                                    if (Item.MemberPolarity == (int)Polarity.Thunder)       TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int) SkillTargRange.Phy:                                                                                           /// 物理 
                        {
                            for (int i = 0; i < inEnemyMemDataDic.Count; i++)
                            {
                                foreach (var Item in inEnemyMemDataDic[i+1])
                                {
                                    if (Item.MemberProAtt != (int)HeroPorAtt.Intellect)     TheMemberDataList.Add(Item);
                                }
                            }
                            break;
                        }
                    case (int) SkillTargRange.Magic:                                                                                         /// 魔法 
                        {
                            for (int i = 0; i < inEnemyMemDataDic.Count; i++)
                            {
                                foreach (var Item in inEnemyMemDataDic[i+1])
                                {
                                    if (Item.MemberProAtt != (int)HeroPorAtt.Intellect)     TheMemberDataList.Add(Item);
                                }
                            }

                            break;
                        }
                    default:        Debug.LogError("配置出错,未识别技能范围!");                                    return;         
                }
            }
        
            switch  ( inPassSkill_C.EffectType )                                                                                            /// 技能特效类型
            {
                case (int) SkillEffecType.Power:                                                                                            /// 力量          
                    {
                        foreach( var Item in TheMemberDataList )
                        {
                            int     lastValue                       = (int)(campValue * addPassValue);                  // 技能数值效果(正:+ 负:-) 
                            Item.Power                              += lastValue;                                       // 添加力量属性
                            BattleMemberData.SecondAttToMember(Item, lastValue, 0, 0);                                  // 添加转换一级属性
                        }
                        break;
                    }
                case (int) SkillEffecType.Agile:                                                                                            /// 敏捷          
                    {
                        foreach( var Item in TheMemberDataList )
                        {
                            int     lastValue                       = (int)(campValue * addPassValue);                  // 技能数值效果(正:+ 负:-) 
                            Item.Agile                              += lastValue;                                       // 添加敏捷属性
                            BattleMemberData.SecondAttToMember(Item, lastValue, 0, 0);                                  // 添加转换一级属性
                        }
                        break;  
                    }
                case (int) SkillEffecType.Intellect:                                                                                        /// 智力          
                    {
                        foreach( var Item in TheMemberDataList )
                        {
                            int     lastValue                       = (int)(campValue * addPassValue);                  // 技能数值效果(正:+ 负:-) 
                            Item.Intellect                          += lastValue;                                       // 添加敏捷属性
                            BattleMemberData.SecondAttToMember(Item, lastValue, 0, 0);                                  // 添加转换一级属性
                        }
                        break;  
                    }
                case (int) SkillEffecType.Blood:                                                                                            /// 血量          
                    {
                        foreach(var Item in TheMemberDataList)
                        {
                            Item.Hp += (int)(campValue * addPassValue);
                        }
                        break;
                    }

                case (int)SkillEffecType.PhyAttact:                                                                                         /// 物理攻击        
                    {
                        foreach(var Item in TheMemberDataList)
                        {
                            Item.PhyAttack += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.MagicAttact:                                                                                       /// 魔法攻击        
                    {
                        foreach(var Item in TheMemberDataList)
                        {
                            Item.MagicAttack += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.PhyAromr:                                                                                          /// 物理护甲        
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.PhyArmor += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.MagicArmor:                                                                                        /// 魔法护甲        
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.MagicArmor += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.PhyCrit:                                                                                           /// 物理暴击        
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.PhyCrit += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.MagicCrit:                                                                                         /// 魔法暴击        
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.MagicCrit += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.ThroughPhyArmor:                                                                                   /// 物理护甲穿透     
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.ThroughPhyArmor += (int)(campValue * addPassValue);
                        }
                        break;
                    }

                case (int)SkillEffecType.SuckBlood:                                                                                         /// 吸血          
                    {
                        foreach( var Item in TheMemberDataList )
                        {
                            Item.SuckBlood += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.Dodge:                                                                                             /// 闪避          
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.Dodge += (int)(campValue * addPassValue);
                        }
                        break;
                    }
                case (int)SkillEffecType.Hit:                                                                                               /// 命中          
                    {
                        foreach (var Item in TheMemberDataList)
                        {
                            Item.Hit += (int)(campValue * addPassValue);
                        }
                        break;
                    }
            }
        }
        private static BattleMemberData BuildNpcMemberD  ( Configs_CheckPointData inCheckP_C, int inPos,                                        // 创建NPC成员数据( 关卡 )
                                                          int inNpcID, int inUltFlag, int inBossPos)    
        {
            Configs_HeroData            TheHero_C                   = Configs_Hero.sInstance.GetHeroDataByHeroID(inNpcID);
            BattleMemberData            TempMem_D                   = new BattleMemberData();

            if (TheHero_C == null)                                  return  null;
            TempMem_D                   = BattleMemberData.BuildNpcMemData_CP( inPos, inBossPos, inCheckP_C, TheHero_C);                    /// 创建NPC成员数据
            if ( inUltFlag == 0)                                                                                                            /// Npc技能释放设置 
            {
                TempMem_D.UltSkillID    = 0;                                                                    
                TempMem_D.UltSkillLv    = 0;
            }
            return                      TempMem_D;   
        }
        #endregion
    }
    public enum         SkillTargRange                                                                                      // 技能目标范围  
        {
            AllMember               = 1,                            /// 全体成员
            Own                     = 2,                            /// 自己
            Ice                     = 3,                            /// 冰系
            Fire                    = 4,                            /// 火系
            Thunder                 = 5,                            /// 雷系
            Magic                   = 6,                            /// 魔法
            Phy                     = 7,                            /// 物理
        }
    public enum         SkillEffecType                                                                                      // 技能效果类型  
        {
            Power                   = 1,                            /// 力量
            Agile                   = 2,                            /// 敏捷
            Intellect               = 3,                            /// 智力
            Blood                   = 4,                            /// 血量
            PhyAttact               = 5,                            /// 物理攻击
            MagicAttact             = 6,                            /// 魔法攻击
            PhyAromr                = 7,                            /// 物理护甲
            MagicArmor              = 8,                            /// 魔法护甲
            PhyCrit                 = 9,                            /// 物理暴击
            MagicCrit               = 10,                           /// 魔法暴击
            ThroughPhyArmor         = 11,                           /// 物理护甲穿透
            SuckBlood               = 12,                           /// 吸血
            Dodge                   = 13,                           /// 闪避
            Hit                     = 14,                           /// 命中
        }
}
public class Fix
{
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\BattleController\Command\BattleDataInit_Command.cs(30):           
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\BattleController\Command\LoadDataForCheckP_Command.cs(52):    

    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(24):       
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(69):       
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(86):          
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(115):     

    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(144):           
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(149):               
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(156):           
    //public string D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\UI\BattleDataUI.cs(26):

    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\BattleController\Command\BattleDataInit_Command.cs(28):           
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\BattleController\Command\BattleMemDataLoad_Command.cs(27):               
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\BattleController\Command\BattleResInitCheckP_Command.cs(53):        
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\BattleController\Command\LoadDataForCheckP_Command.cs(53):               
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(23):      
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(68):       
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(80):            
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(110):          
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(135):            
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(140):               
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\MainProgram\B1_BattleStart\IBattleStartData.cs(156):           
    //    D:\WORK_PROJECT\2019\Strom2019\Strom\Assets\ScriptsRoot\GameRoot\BattleRoot\UI\BattleDataUI.cs(22):         
    //
}