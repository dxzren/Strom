using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StormBattle
{
    public class                    MemberUI : MonoBehaviour, IBattleMemUI                                      
    {
            public UISprite             MoveProLv_Sp;                                                                           /// 移动职业等级图标
            public UISlider             MoveHp_Sl;                                                                              /// 移动血条
            public GameObject           OurHeadGroup, BossHeadGroup, MoveMemUIObj;                                              /// 我方头像组, Boss头像组, 移动UI组; 
            public GameObject           WarningObj;                                                                             /// Boss提醒Obj

                                                                                                                                /// <@ ViewFixedUI: 视图固定UI >
            public UILabel              BossName_La;                                                                            /// Boss名称
            public UILabel              BossLv_La;                                                                              /// Boss等级
            public UILabel              BossMaxHpNum_La;                                                                        /// Boss 最大血量数字化
            public UILabel              BossCurrHpNum_La;                                                                       /// Boss 当前血量数字化
            public UISprite             BossHeadIcon_Sp;                                                                        /// Boss 头像

            public UISprite             QualityFrame_Sp,HeadIcon_Sp;                                                            /// 品质框,头像图标
            public UISprite             ProLv_Sp, Polarity_Sp;                                                                  /// 职业等级图示,元素属性 
            public UISlider             Hp_Sl, Energy_Sl;                                                                       /// 固定血条,固定能量条
            public UIGrid               Star_Grid;                                                                              /// 星级 组 

                                                                                                                                /// <@ MemberData: 接口数据 >
            public GameObject           AttackerObj             { get { return this.gameObject; }  }                            /// 攻击对象 Obj   
            public GameObject           MemModelObj             { get { return TheMoveMemUI.ModelUpObj; } set { TheMoveMemUI.ModelUpObj = value; } }  /// 成员模型 Obj
            public MoveMemUI            TheMoveMemUI;                                                                           /// 移动UI实例
            public MoveMemUI            MyModelUpItem           { get { return TheMoveMemUI; } }                                /// 成员头部UI(血条)
            public IBattleMemberData    IMemData                { set; get; }                                                   /// 成员数据
            public Action<IBattleMemUI> OnClicked               { set; get; }                                                   /// 点击
                                         
            public GameObject           MemDieObj;                                                                              /// 成员尸体对象
            public GameObject           UltOnEffectObj;                                                                         /// 大招激活特效
            public GameObject           UltlickObj;                                                                             /// 大招点击对象
            public GameObject           OnWarningObj;                                                                           /// 警告对象     

            private void Start          ()
            {
                UIEventListener.Get     (UltlickObj).onClick            = OnClick;
                Debug.Log("MemberUI:Start():" +  IMemData.MemberID);
                BaseInit();
            }
            private void Update         ()
            {
                if (MoveMemUIObj != null && BattleControll.sInstance.MainCamera != null )                                       // 刷新成员移动UI 位置
                {
                    Vector3             TheMoveUIV3                     = MemModelObj.transform.position;
                    TheMoveUIV3.y                                       += _MoveMemUIHeight;
                    TheMoveUIV3                                         = BattleControll.sInstance.MainCamera.WorldToScreenPoint(TheMoveUIV3);
                    TheMoveUIV3.z                                       = 0;
                    MoveMemUIObj.transform.position                     = BattleControll.sInstance.UICamera.ScreenToWorldPoint(TheMoveUIV3);
                }
            }
                                                                                                                                /// <@ PublicFun -调用方法 >
            public void                 OnClick                 (GameObject inObj)                                              // 大招点击       
            {   OnClicked(this);                                        }
            public void                 SetUltClick             (bool inEnable, bool GuideUlt = false )                         // 设置大招激活   
            {
                UltOnEffectObj.SetActive(inEnable);
                if ( inEnable == true ) _IsUltCanClick                  = true;
            
                if ( inEnable && _EffectOnce == false)
                {
                    if(Energy_Sl.value >= 1f )
                    {
                        Vector3         ThePos                          = Vector3.zero;
                        UIEffectManager.sInstance.ShowUIEffect(HeadIcon_Sp.gameObject, EffectStrConst.UIEffect_EnergyUp, ThePos, 1.5f);
                        _EffectOnce                                     = true;
                    }
                }
                if(!inEnable)
                {
                    _EffectOnce                                         = false;       
                }    
            }
            public void                 SetHpSliderValue        (int inHp)                                                      // 设置血量条值   
            {
                Hp_Sl.value             = Mathf.Min( inHp * 1.0f / IMemData.Hp, 1);
                MoveHp_Sl.value         = Hp_Sl.value;
            }
            public void                 SetEnergySliderValue    (int inEnergy)                                                  // 设置能量条值   
            {
                Energy_Sl.value         = Mathf.Min( inEnergy*1.0f/BattleParmConfig.MemberEnergyMax, 1);
            }
            public void                 SetWarning              (bool inShow)                                                   // 设置警告提示   
            {   OnWarningObj.SetActive(inShow);                 }           
            public void                 MemberDie()                                                                             // 成员死亡      
            {
                MemDieObj.SetActive (true);                                                                                     // 显示尸体
                SetMoveMemUIShow    (false);                                                                                    // 隐藏移动血量条
                Hp_Sl.gameObject.SetActive(false);                                                                              // 隐藏固定血量条
                Energy_Sl.gameObject.SetActive(false);                                                                          // 隐藏固定能量条
            }

                                                                                                                                /// <@ MoveUI   -移动UI >
            public void                 SetMoveMemUIShow        (bool inShow)                                                   // 设置移动血条   
            {
                if (inShow)             MoveMemUIObj.SetActive  (false);
                MoveHp_Sl.gameObject.SetActive                  (inShow);
                MoveProLv_Sp.gameObject.SetActive               (inShow);
            }
            public void                 TheMoveUI_Show          ()                                                              // 移动血条显示   
            {
                MoveMemUIObj.SetActive                          (true);
                MoveHp_Sl.gameObject.SetActive                  (true);
                MoveProLv_Sp.gameObject.SetActive               (true);
            }
            public void                 TheMoveUI_Hide          ()                                                              // 移动血条隐藏   
            {
                TweenAlpha              TheTA                           = MoveMemUIObj.GetComponent<TweenAlpha>();
                if ( TheTA != null)
                {
                    TheTA.from                                          = 1;
                    TheTA.to                                            = 0;
                    TheTA.duration                                      = 0.6f;
                }
                DestroyImmediate(TheTA);
                MoveMemUIObj.SetActive(false);
            }

                                                                                                                                /// <@ MoveTips -移动提示 >
            public void                 TipAddHp                (int inValue, bool inShow = false)                              // 增加血量     提示  
            {
                if (inShow)             BattleControll.sInstance.TipsItem.TipsHpAdd("+"+ inValue, MoveMemUIObj).SetActive(true);/// 展示Numtips
                else
                {
                    _NumTipsList.Add    (new KeyValuePair<Action<int, bool>, int>(TipAddHp, inValue));                          /// 添加入Num列表
                    StartCoroutine      (ShowNumTips());                                                                        /// 启动线程(展示NumTips)
                }

            }
            public void                 TipAddHpBig             (int inValue, bool inShow = false)                              // 增加血量(大) 提示  
            {
                if (inShow)         BattleControll.sInstance.TipsItem.TipsHpAddBig("+" + inValue, MoveMemUIObj).SetActive(true);/// 展示NumTips
                else
                {
                    _NumTipsList.Add    (new KeyValuePair<Action<int, bool>, int>(TipAddHpBig, inValue));                       /// 添加入Num列表
                    StartCoroutine      (ShowNumTips());                                                                        /// 启动线程(展示NumTips)
                }

            }
            public void                 TipDownHp               (int inValue, bool inShow = false)                              // 减少血量     提示  
            {
                if (inShow)             BattleControll.sInstance.TipsItem.TipsHpSub("-" + inValue, MoveMemUIObj).SetActive(true);   /// 展示NumTips
                else
                {
                    _NumTipsList.Add    (new KeyValuePair<Action<int, bool>, int>(TipDownHp, inValue));                         /// 添加入Num列表
                    StartCoroutine      (ShowNumTips());                                                                        /// 启动线程(展示NumTips)
                }
            }
            public void                 TipDownHpBig            (int inValue, bool inShow = false)                              // 减少血量(大) 提示  
            {
                if (inShow)             BattleControll.sInstance.TipsItem.TipsHpSubBig("-" + inValue, MoveMemUIObj).SetActive(true);/// 展示NumTips
                else
                {
                    _NumTipsList.Add    (new KeyValuePair<Action<int, bool>, int>(TipDownHpBig, inValue));                      /// 添加入Num列表
                    StartCoroutine      (ShowNumTips());                                                                        /// 启动线程(展示NumTips)
                }
            }
            public void                 TipAddEnergy            (int inValue, bool inShow = false)                              // 增加能量     提示  
            {
                if (inShow)             BattleControll.sInstance.TipsItem.TipsEnergy("+" + inValue,MoveMemUIObj).SetActive(true);   /// 展示NumTips
                else
                {
                    _NumTipsList.Add    (new KeyValuePair<Action<int, bool>, int>(TipAddEnergy, inValue));                      /// 添加入Num列表
                    StartCoroutine      (ShowNumTips());                                                                        /// 启动线程(展示NumTips)
                }
            }
            public void                 TipDownEnergy           (int inValue, bool inShow = false)                              // 消耗能量     提示  
            {
                if(inShow)              BattleControll.sInstance.TipsItem.TipsEnergy("-" + BattleParmConfig.MemberEnergyMax, MoveMemUIObj).SetActive(true); ///  展示NumTips
                else
                {
                    _NumTipsList.Add    (new KeyValuePair<Action<int, bool>, int>(TipDownEnergy, inValue));                     /// 添加入Num列表
                    StartCoroutine      (ShowNumTips());                                                                        /// 启动线程(展示NumTips)
                }
            }
            public void                 TipBuffHit              (string inValue, bool inShow = false)                           // Buff效果_命中 提示 
            {
                if (inShow)
                {
                    if (IMemData.BattleCamp == Battle_Camp.Our)
                    {   BattleControll.sInstance.TipsItem.TipsHitTarget(inValue, MoveMemUIObj).SetActive(true);     }           /// 展示命中效果
                    else
                    {   BattleControll.sInstance.TipsItem.TipsMissTarget(inValue, MoveMemUIObj).SetActive(true);    }           /// 展示未命中效果
                }
                else
                {
                    _WordTipsList.Add   (new KeyValuePair<Action<string, bool>, string>( TipBuffHit, inValue));                 /// 文本展示列表 添加
                    StartCoroutine      (ShowWordTips());                                                                       /// 文本展示列表 添加
                }
            }
            public void                 TipBuffMiss             (string inValue, bool inShow = false)                           // Buff效果_未命中 提示 
            {
                if (inShow)
                {
                    if (IMemData.BattleCamp == Battle_Camp.Our)
                    {   BattleControll.sInstance.TipsItem.TipsMissTarget(inValue, MoveMemUIObj).SetActive(true);    }           /// 展示未命中效果
                    else
                    {   BattleControll.sInstance.TipsItem.TipsHitTarget(inValue, MoveMemUIObj).SetActive(true);     }           /// 展示命中效果
                }
                else
                {
                    _WordTipsList.Add   (new KeyValuePair<Action<string, bool>, string>(TipBuffMiss,inValue));                  /// 文本展示列表 添加
                    StartCoroutine      (ShowWordTips());                                                                       /// 文本展示列表 添加
                }
            }


            #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

            private bool                _IsShowMoveMemUI                    = true;                                             /// 显示成员移动UI
            private bool                _IsUltCanClick                      = false;                                            /// 大招点击激活
            private bool                _EffectOnce                         = false;                                            /// 特效已展示
            private bool                _IsRuningNumTips                    = false;                                            /// 数字展示中
            private bool                _IsRuningWordTips                   = false;                                            /// 文本展示中

            List<KeyValuePair<Action<int, bool>, int>>       _NumTipsList   = new List<KeyValuePair<Action<int, bool>, int>>();         /// 数字展示 列表
            List<KeyValuePair<Action<string, bool>, string>> _WordTipsList  = new List<KeyValuePair<Action<string, bool>, string>>();   /// 文本展示 列表
            private byte                _StarCount                          = 0;                                                /// 星级数量
            private float               _MoveMemUIHeight                    = 2;                                                /// 移动UI高度
            private MemberUIType        _TheMemUIType                       = MemberUIType.OurMemberUI;                         /// 成员UI类型


                                                                                                                                ///<@ Init -初始化设置>
            private void                BaseInit()                                                                              // 初始化<Root>    
            {
                TheMemUIType_Init();                                                                                            // 成员UI类型设置
                MemUI_Init();                                                                                                   // 成员UI初始化
                UtlBtnInit();                                                                                                   // 按钮设置初始化
            }
            private void                TheMemUIType_Init()                                                                     // 成员类型UI设置   
            {
                Debug.Log(IMemData.BattleCamp);
                if      ( IMemData.BattleCamp == Battle_Camp.Our)                       _TheMemUIType = MemberUIType.OurMemberUI;   /// 我方UI
                else if ( IMemData.MemberType == Battle_MemberType.Npc_WorldBoss)       _TheMemUIType = MemberUIType.BossMemberUI;  /// 敌方UI
                else                                                                    _TheMemUIType = MemberUIType.EnemyMeberUI;  /// 世界BossUI
            }
            private void                MemUI_Init()                                                                            // 成员UI初始化     
            {
                if      ( _TheMemUIType == MemberUIType.OurMemberUI)                                                            // 我方成员UI初始化 
                {
                                                                                                                                    ///<@ 固定UI>
                    BossLv_La.text                      = IMemData.MemberLv.ToString();                                             /// 成员等级
                    QualityFrame_Sp.spriteName          = HeroControll.GetHeroFrameName((HeroQuality)IMemData.memberQuality);       /// 英雄配置框
                    HeadIcon_Sp.spriteName              = IMemData.HeadIconSpriteName;                                              /// 英雄头像
                    ProLv_Sp.spriteName                 = "-" + IMemData.MemberProLv;                                               /// 职业等级图标
                    Polarity_Sp.spriteName              = HeroControll.GetPolarityName((Polarity)((int)IMemData.MemberPolarity));   /// 元素相性
                    Hp_Sl.value                         = 1;                                                                        /// 血量条
                    Energy_Sl.value                     = 0;                                                                        /// 能量条

                    _StarCount                          = (byte)IMemData.memberStarLv;                                              /// 星级数
                    if (_StarCount > 0)                                                                                             ///<@ 星级图标展示>
                    {
                        for (int i = 0; i < Star_Grid.GetChildList().Count; i++)
                        {   Destroy(Star_Grid.GetChild(i).gameObject);      }
                        for(int i = 0; i < _StarCount; i++)
                        {
                            UIAtlas             TheAtlas    = Util.LoadAtlas(AtlasConfig.HeroSysAtlas);                             // 星级图集获取
                            UISprite            TheStar     = NGUITools.AddSprite(Star_Grid.gameObject, TheAtlas,"xing");           // 星级Sp设置
                            TheStar.height                  = 20;                                                                   // 星级高度
                            TheStar.width                   = 20;                                                                   // 星级宽度
                            TheStar.depth                   = 30;                                                                   // 星级深度
                        }
                    }

                                                                                                                                    ///<@ 移动UI >
                    MoveProLv_Sp.spriteName             = "-" + IMemData.MemberProLv;                                               /// 移动UI 职业等级图标
                    MoveHp_Sl.value                     = 1;                                                                        /// 移动UI 血量条
                }
                else if ( _TheMemUIType == MemberUIType.EnemyMeberUI)                                                           // 敌方成员UI初始化 
                {
                    ProLv_Sp.spriteName                 = "-" + IMemData.MemberProLv;                                           /// 移动UI 职业等级图标
                    ProLv_Sp.gameObject.SetActive(false);
                    OurHeadGroup.SetActive(false);
                    BossHeadGroup.SetActive(false);
                    MoveMemUIObj.SetActive(true);
                    
                }
                else                                                                                                            // Boss UI初始化   
                {
                    BossName_La.text                    = Language.GetValue(IMemData.memberName);                               // Boss名称 数字显示
                    BossLv_La.text                      = IMemData.MemberLv.ToString();                                         // Boss等级 数字显示
                    BossMaxHpNum_La.text                = IMemData.Hp.ToString();                                               // 最大血量 数字显示
                    BossCurrHpNum_La.text               = IMemData.CurrHp.ToString();                                           // 当前血量 数字显示
                    MoveHp_Sl.value                     = 1;                                                                    // 血量条 值

                    Configs_HeroData TheHero_C          = Configs_Hero.sInstance.GetHeroDataByHeroID(IMemData.MemberID);
                    Configs_TroopsHeadSData TroopHead_C = Configs_TroopsHeadS.sInstance.GetTroopsHeadSDataByTroopsID(TheHero_C.TroopsID);

                    if (TroopHead_C != null)            BossHeadIcon_Sp.spriteName = TroopHead_C.head70;                        // Boss头像
                    else                                Debug.LogError("HeadIcon Not Find ID:"+ TheHero_C.TroopsID);            
                }

            }
            private void                UtlBtnInit()                                                                            // 大招按钮初始化    
            {
                UIEventListener.Get(UltlickObj).onClick                 = UltClick;
            }
            private void                UltClick            ( GameObject inObj)                                                 // 大招点击         
            {    OnClicked(this);                                       }

            private IEnumerator         ShowNumTips         ( bool inIsContent = false )                                        // 展示数字Tips     
            {
                if (!_IsRuningNumTips && inIsContent)
                {
                    if(_NumTipsList.Count > 0)
                    {
                        _IsRuningNumTips                                    = true;                                             /// NumTips播放状态
                        KeyValuePair<Action<int, bool>,int> TheKV           = _NumTipsList[0];                                  /// 展示列表 第一条
                        TheKV.Key(TheKV.Value, true);                                                                           /// 展示封装 Tips
                        yield return new WaitForSeconds(BattleParmConfig.TipsIntervalTime);                                     /// 延迟(Tips持续时间)
                        _NumTipsList.RemoveAt(0);                                                                               /// 移除出列表
                        StartCoroutine(ShowNumTips(true));                                                                      /// 回调该线程
                    }
                    else                _IsRuningNumTips                    = false;                                            /// 没有展示内容,
                }
            }
            private IEnumerator         ShowWordTips        ( bool inIsContent = false)                                         // 展示文本Tips     
            {
                if ( !_IsRuningWordTips && inIsContent )
                {
                    _IsRuningWordTips                                       = true;                                             /// NumTips播放状态
                    KeyValuePair<Action<string, bool>,string > TheKV        = _WordTipsList[0];                                 /// 展示列表 第一条
                    TheKV.Key(TheKV.Value, true);                                                                               /// 展示封装 Tips
                    yield return new WaitForSeconds(BattleParmConfig.TipsIntervalTime);                                         /// 延迟(Tips持续时间)
                    _WordTipsList.RemoveAt(0);                                                                                  /// 移除出列表
                    StartCoroutine(ShowWordTips(true));                                                                         /// 回调该线程
                }
                else                    _IsRuningWordTips                   = false;                                            /// 没有展示内容,
            }

            public enum                MemberUIType                                                                            // 成员UI类型       
            {
                OurMemberUI             = 1,                            /// 我方成员UI
                EnemyMeberUI            = 2,                            /// 敌方成员UI
                BossMemberUI            = 3,                            /// Boos成员UI
            }
            #endregion
        }

    public interface                IBattleMemUI                                                               
    {
        GameObject                  AttackerObj                     { get; }                                                // 攻击对象
        GameObject                  MemModelObj                     { set; get; }                                           // 模型对象
        IBattleMemberData           IMemData                        { set; get; }                                           // 成员数据
        MoveMemUI                   MyModelUpItem                   { get; }                                                // 成员模型上方显示集合<血量条_能量条_职业图标>
        Action<IBattleMemUI>        OnClicked                       { set; get; }                                           // 已点击成员UI

        void                        SetMoveMemUIShow                ( bool inIsShow);                                       // 血条_职业等级图标 显示
        void                        SetWarning                      ( bool inIsShow );                                      // 感叹号_职业等级图标 显示
        void                        SetHpSliderValue                ( int inHp);                                            // 设置血量
        void                        SetEnergySliderValue            ( int inEnergy);                                        // 设置能量

        void                        SetUltClick                     (bool inEnable, bool GuideUltra = false);               // 设置点击激活状态(大招释放激活)
        void                        MemberDie                       ();                                                     // 设置成员死亡

        void                        TipAddHp                        (int inAlfa, bool isShow = false);                      // 提示:增加血量
        void                        TipDownHp                       (int inAlfa, bool isShow = false);                      // 提示:减少血量
        void                        TipAddHpBig                     (int inAlfa, bool isShow = false);                      // 提示:增加血量大
        void                        TipDownHpBig                    (int inAlfa, bool isShow = false);                      // 提示:减少血量大
        void                        TipAddEnergy                    (int inAlfa, bool isShow = false);                      // 提示:增加能量
        void                        TipDownEnergy                   (int inAlfa, bool isShow = false);                      // 提示:减少能量
        void                        TipBuffHit                      (string inWord, bool isShow = false);                   // 提示:击中
        void                        TipBuffMiss                     (string inWord, bool isShow = false);                   // 提示:未击中
    }

/*    public class                    MemUI_Enemy : MonoBehaviour                                                             // 弃用
        {

            public MoveMemUI            MemModelUpItem { get{ return _HeadUpItem; } }                                           /// UpItem
            public UISlider             Hp_Sl;                                                                                  /// 血量条
            public UISprite             ProLevel_Sp;                                                                            /// 职业等级 Sprite
            public Action<IBattleMemUI> OnClicked { get; set; }                                                                 /// 点击(处理)
            public IBattleMemberData    IMemData { set; get; }                                                                  /// 英雄数据

            public GameObject           WarningObj;                                                                             /// 警告对象
            public GameObject           MemModelObj { get { return _HeadUpItem.ModelUpObj; } set { _HeadUpItem.ModelUpObj = value; } } /// UpItem对象
            public GameObject           AttackedGameObject { get { return this.gameObject; } }                                  /// 攻击对象
            private void Start()
            {
                if ( IMemData != null )
                {
                    if ( IMemData.MemberProLv >= 1 && IMemData.MemberProLv <= 9 )
                    {    ProLevel_Sp.spriteName = "-" + IMemData.MemberProLv; }
                    else
                    {    ProLevel_Sp.gameObject.SetActive(false);               }
                }
            }

                                                                                                                                /// <@ PublicFun -调用方法 >     
            public void                 IsShowHp_ProLv      ( bool inIsShow)                                                    // 血条_职业等级图标   显示 
            {
                if (Hp_Sl != null)                          Hp_Sl.gameObject.SetActive(inIsShow);
                if (ProLevel_Sp != null)                    ProLevel_Sp.gameObject.SetActive(inIsShow);
            }
            public void                 ShowWarning_ProLv   ( bool inIsShow)                                                    // 感叹号_职业等级图标 显示 
            {
                WarningObj.SetActive(inIsShow);
                ProLevel_Sp.gameObject.SetActive(inIsShow);
            }
            public void                 SetClickEnable      ( bool inEnable, bool inGuideMerc = false, bool GuideUltra = false )// 设置大招点击激活        
            { }                                                                        
            public void                 SetHp               ( int inHp )                                                        // 设置血量条              
            {
                if ( IMemData != null)
                {    Hp_Sl.value = Mathf.Min(inHp * 1.0f / IMemData.Hp, 1);     }
            }
            public void                 SetEnergy           ( int inEnergy )                                                    // 设置能量条              
            {
    //            if ( inEnergy >= BattleParmConfig.MemberEnergyMax)
    //            {    OnClicked();                                           }
            }
            public void                 MemberDie           ()                                                                  // 成员死亡关闭 血条显示    
            {
                IsShowHp_ProLv(false);
            }

                                                                                                                                /// <@ TipsInfo  -提示显示 > 
            public void                 TipAddHp        (int inValue, bool inIsShow = false)                                    // 加血    提示(不结算) 
            {
                if (inIsShow)
                {   BattleControll.sInstance.TipsItem.TipsHpAdd("+" + inValue, _HeadUpItem.gameObject).SetActive(true); }
                else
                {
                    _NumTipsList.Add(new KeyValuePair<Action<int, bool>, int>(TipAddHp, inValue));
                    StartCoroutine(ShowNumTips());
                }
            }
            public void                 TipDownHp       (int inValue, bool inIsShow = false)                                    // 减血    提示(不结算) 
            {
                if (inIsShow)
                {   BattleControll.sInstance.TipsItem.TipsHpSub("-" + Mathf.Abs(inValue), _HeadUpItem.gameObject).SetActive(true); }
                else
                {
                    _NumTipsList.Add(new KeyValuePair<Action<int, bool>, int>(TipDownHp, inValue));
                    StartCoroutine(ShowNumTips());
                }
            }
            public void                 TipAddHpBig     (int inValue, bool inIsShow = false)                                    // 加血 大 提示(不结算) 
            {
                if (inIsShow)
                {   BattleControll.sInstance.TipsItem.TipsHpAddBig("+" + inValue, _HeadUpItem.gameObject).SetActive(true); }
                else
                {
                    _NumTipsList.Add(new KeyValuePair<Action<int, bool>, int>(TipAddHpBig, inValue));
                    StartCoroutine(ShowNumTips());
                }
            }
            public void                 TipDownHpBig    (int inValue, bool inIsShow = false)                                    // 减血 大 提示(不结算) 
            {
                if (inIsShow)
                {   BattleControll.sInstance.TipsItem.TipsHpSubBig("-" + Mathf.Abs(inValue), _HeadUpItem.gameObject).SetActive(true); }
                else
                {
                    _NumTipsList.Add(new KeyValuePair<Action<int, bool>, int>(TipDownHpBig, inValue));
                    StartCoroutine(ShowNumTips());
                }
            }
            public void                 TipAddEnergy    (int inValue, bool inIsShow = false)                                    // 能量增加 提示(不结算)
            {
                if (inIsShow)
                {   BattleControll.sInstance.TipsItem.TipsEnergy("+" + Mathf.Abs(inValue), _HeadUpItem.gameObject).SetActive(true); }
                else
                {
                    _NumTipsList.Add(new KeyValuePair<Action<int, bool>, int>(TipAddEnergy, inValue));
                    StartCoroutine(ShowNumTips());
                }
            }
            public void                 TipDownEnergy   (int inValue, bool InIsShow = false)                                    // 能量减少 提示(不结算)
            {
                if (InIsShow)
                {   BattleControll.sInstance.TipsItem.TipsEnergy("-" + Mathf.Abs(inValue), _HeadUpItem.gameObject).SetActive(true); }
                else
                {
                    _NumTipsList.Add(new KeyValuePair<Action<int, bool>, int>(TipDownEnergy, inValue));
                    StartCoroutine(ShowNumTips());
                }
            }
            public void                 TipHit          (string inWord, bool isShow = false)                                    // 提示:击中           
            {
                if(isShow)
                {
                    if (IMemData.BattleCamp == Battle_Camp.Our)
                    {   BattleControll.sInstance.TipsItem.TipsHitTarget(inWord, _HeadUpItem.gameObject).SetActive(true); }
                    else
                    {   BattleControll.sInstance.TipsItem.TipsMissTarget(inWord, _HeadUpItem.gameObject).SetActive(true); }
                }
                else
                {
                    _WordsTipsList.Add(new KeyValuePair<Action<string, bool>, string>(TipHit, inWord));
                    StartCoroutine(ShowWordTips());
                }
            }
            public void                 TipMiss         (string inWord, bool isShow = false)                                    // 提示:未击中         
            {
                if (isShow)
                {
                    if (IMemData.BattleCamp == Battle_Camp.Our)
                    { BattleControll.sInstance.TipsItem.TipsMissTarget(inWord, _HeadUpItem.gameObject).SetActive(true); }
                    else
                    { BattleControll.sInstance.TipsItem.TipsHitTarget(inWord, _HeadUpItem.gameObject).SetActive(true); }
                }
                else
                {
                    _WordsTipsList.Add(new KeyValuePair<Action<string, bool>, string>(TipMiss, inWord));
                    StartCoroutine(ShowWordTips());
                }
            }

            #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================

            private bool                _IsRunNumTips                                   = false;                                                    /// 数字Tips显示
            private bool                _IsRunWordTips                                  = false;                                                    /// 文本Tips显示
            private MoveMemUI           _HeadUpItem                                     = new MoveMemUI();                                          /// 实例
            private List<KeyValuePair<Action<int, bool>, int>>       _NumTipsList       = new List<KeyValuePair<Action<int, bool>, int>>();         /// 数字Tips封装 列表
            private List<KeyValuePair<Action<string, bool>, string>> _WordsTipsList     = new List<KeyValuePair<Action<string, bool>, string>>();   /// 数字Tips封装 列表

            IEnumerator                 ShowNumTips     ( bool isInFor = false)                                                 // 显示Tips数字 
            {
                if (!_IsRunNumTips || isInFor )
                {
                    if ( _NumTipsList.Count > 0)
                    {
                        _IsRunNumTips                                           = true;
                        KeyValuePair<Action<int, bool>, int>    TheNumTips      = _NumTipsList[0];
                        TheNumTips.Key(TheNumTips.Value, true);
                        yield return new WaitForSeconds(BattleParmConfig.TipsIntervalTime);
                        _NumTipsList.RemoveAt(0);
                        StartCoroutine(ShowNumTips(true));
                    }
                    else
                    {   _IsRunNumTips = false;              }             
                }
            }
            IEnumerator                 ShowWordTips    ( bool isInFor = false)                                                 // 显示Tips文本 
            {
                if (!_IsRunWordTips || isInFor )
                {
                    if (_WordsTipsList.Count > 0)
                    {
                        _IsRunWordTips                                          = true;
                        KeyValuePair<Action<string, bool>, string> TheWordTips  = _WordsTipsList[0];
                        TheWordTips.Key(TheWordTips.Value, true);
                        yield return new WaitForSeconds(BattleParmConfig.TipsIntervalTime);
                        _WordsTipsList.RemoveAt(0);
                        StartCoroutine(ShowWordTips(false));
                    }
                    else
                    {   _IsRunWordTips = false; }
                }
            }
            #endregion
        }
*/
}
