using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

namespace StormBattle
{
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 战斗成员视图 </summary>
    public class                    BattleMemView : EventView
    {
            public BattlePosData        MemPos_D                        { get; set; }                                           // 成员位置数据
            public float                AttackRange                     { get; set; }                                           // 攻击范围
            public float                AttackInterval                  { get; set; }                                           // 攻击CD   ( 间隔 )
            public float                MoveSpeed                       { get; set; }                                           // 移动速度
            public float                BodySize                        { get; set; }                                           // 模型比例
            public MemAnimEffectData    Battle_AE                                                                               // 动画特效实例       
            {
                get { return _Battle_AE; }
                set
                {
                    _Battle_AE                                          = value;
                    _Battle_AE.ModelState                               = BattleModelState.Busy;                                // 设置模型状态

                    _Battle_AE.Model.transform.parent                   = ModelObj.transform;                                   // 父级指定
                    _Battle_AE.Model.transform.localPosition            = Vector3.zero;                                         // 位置设置
                    _Battle_AE.Model.transform.localRotation            = Quaternion.identity;                                  // 旋转设置
                    if (value.AnimAndEffec_C != null)                                                                           // 缩放设置 
                    {
                        _Battle_AE.Model.transform.localScale = new Vector3(value.AnimAndEffec_C.ModelAdjust,
                                                                                      value.AnimAndEffec_C.ModelAdjust,
                                                                                      value.AnimAndEffec_C.ModelAdjust);
                    }
                    else        _Battle_AE.Model.transform.localScale   = Vector3.one;
                }
            }

            public IBattleMemUI         IMemUI;                                                                                 // IMemUI   数据 接口
            public IBattleMemberData    IMem_D;                                                                                 // IMemData 数据 接口
            [HideInInspector]
            public Battle_Camp          Camp                            = Battle_Camp.Our;                                      // 阵营

            public GameObject           ModelObj                        { get; set; }                                           // 模型对象

            public void                 ShowSet     (bool inIsShow)                                                             // 显示设置           
            {
                if ( ModelObj != null )                                  ModelObj.SetActive(inIsShow);
                if ( IMemUI   != null)                                     IMemUI.SetMoveMemUIShow(inIsShow);
            }
            public void                 DestroyObj  ()                                                                          // 摧毁目标           
            {
                if ( Battle_AE != null && Battle_AE.Model != null )
                {
                     Battle_AE.Model.transform.parent                   = BattleControll.sInstance.RootModelListObj.transform;  /// 设置模型对象父级
                     Battle_AE.ModelState                               = BattleModelState.Ready;                               /// 设置模型状态
                }
                if ( Camp == Battle_Camp.Enemy &&
                     IMem_D.MemberType == Battle_MemberType.Npc_WorldBoss &&
                     IMemUI != null &&
                     IMemUI.AttackerObj)                         Destroy(IMemUI.AttackerObj);
                Destroy(ModelObj);
            }
            public void                 CloseEffect ( bool inIsDead = true )                                                    // 关闭特效           
            {
                foreach (var Item in ModelObj.GetComponentsInChildren<TimeEndHandler>())
                {
                    if (inIsDead || (!inIsDead && Item.gameObject.tag.ToLower() !=  BattleResStrName.BindEffectTag.ToLower()) )
                    {
                        Item.gameObject.SetActive (false);
                        Item.gameObject.transform.parent    = BattleControll.sInstance.RootEffectListObj.transform;
                    }
                }
                foreach (NoScaleEffect Item in ModelObj.GetComponentsInChildren<NoScaleEffect>())
                {
                    if ( inIsDead || (!inIsDead && Item.gameObject.tag.ToLower() != BattleResStrName.BindEffectTag.ToLower()))
                    {
                        Item.gameObject.SetActive(false);
                        Item.gameObject.transform.parent    = BattleControll.sInstance.RootEffectListObj.transform;
                    }
                }
            }
            public void                 MemberUI_Onclick(IBattleMemUI inMemUI)                                                  // 成员UI 点击事件    
            {
                dispatcher.Dispatch     (BattleEvent.MemUltOnClick_Event);
            }

            private MemAnimEffectData   _Battle_AE                     = null;                                                  // 动画特效

    }
    ///---------------------------------------------------------------------------------------------------------------------/// <summary>  成员行动数据 </summary>
    public class                    BattleMemMediator : EventMediator, IBattleMemMediator
        {
            [Inject]
            public  BattleMemView       InMemView           { set; get; }                                                       ///  成员视图
            [Inject]
            public IBattleStartData     InBattleStart_D     { set; get;}                                                        ///  战斗初始化数据
            [Inject]
            public IPlayer              InPlayer_D          { set; get; }                                                       ///  玩家数据  

            #region================================================||   PublicStatement-- < 公共_声明 >          ||<FourNode>================================================

            public static bool          IsShowed            = false;                                                            ///  已展示
            public bool                 isAttacking         { set { _IsProcessOver = !value; } get { return !_IsProcessOver; } }///  攻击中

            public int                  Hp                  { get; set; }                                                       ///  当前血量
            public int                  Energy              { get; set; }                                                       ///  能量
            public int                  MaxHp               { get { return  IMemData.Hp; } }                                    ///  血量上限

            public int                  AttackCount         { get; set; }                                                       ///  攻击累计
            public int                  UltSkillCount       { get; set; }                                                       ///  大招技能累计

            public float                MoveSpeed           { get { return InMemView.MoveSpeed; } }                             ///  移动速度
            public float                BodySize            { get { return InMemView.BodySize; } }                              ///  模型尺寸
            public float                AttackRange         { get { return InMemView.AttackRange; } }                           ///  攻击范围
            public float                AttackInterval      { get { return InMemView.AttackInterval; } }                        ///  攻击间隔

            public NoScaleTimeState     ScaleState          { get; set; }                                                       ///  没有缩放状态
            public Battle_Camp          Camp                { get { return InMemView.Camp; } }                                  ///  阵营
            public BattleMemState       MemState            { get; set; }                                                       ///  成员状态

            public ITrialMove           TrialMove           { get; set; }                                                       ///  轨迹移动
            public IBattleMemberData    IMemData            { get { return InMemView.IMem_D; } }                                ///  成员数据
            public IBattleMemUI         IMemUI              { get { return InMemView.IMemUI; } }                                ///  成员UI

            public MoveToAttackItem     AttackMove          { get; set; }                                                       ///  攻击移动
            public NormalAttackItem     AttackNormal        { set; get; }                                                       ///  普通攻击
            public ActiveSkillItem      AttackSkill_1       { set; get; }                                                       ///  主动技能_1
            public ActiveSkillItem      AttackSkill_2       { set; get; }                                                       ///  主动技能_2
            public UltSkillItem         UltSkill            { get; set; }                                                       ///  大招技能
            public BufferManagerItem    MemBuff             { set; get; }                                                       ///  成员buff
            public ColorMangerItem      ColorManger         { set; get; }                                                       ///  颜色管理

            public GameObject           ModelObj            { get { return InMemView.ModelObj; } }                              ///  模型对象
            public MemAnimEffectData    MemAnimEffect       { get { return InMemView.Battle_AE; } }                             ///  成员动画特效集合
            public BattleTeam           OurTeam             { get { return Camp == Battle_Camp.Our ?                            ///  我方队伍
                                                            BattleControll.sInstance.OurTeam : BattleControll.sInstance.EnemyTeam; } }
            public BattleTeam           EnemyTeam           { get { return Camp == Battle_Camp.Enemy ?                          ///  敌方队伍
                                                            BattleControll.sInstance.OurTeam : BattleControll.sInstance.EnemyTeam; } }
            public BattlePosData        MemPos_D            { get { return  InMemView.MemPos_D; } }                             ///  初始战斗位置
            public DefenderDamageSolve  DefendSolve         { get; set; }                                                       ///  守方结算
            public Configs_HeroData     Hero_C              { get; private set; }                                               ///  英雄配置数据
            public BossSkillData        BossSkill_D         { get; set; }                                                       ///  boss技能数据
            #endregion

            #region================================================||   PublicFunc--      < 公共调用方法 >       ||<FourNode>================================================
            public override void        OnRegister()                                                                            //  注册<Root> 
            {
                InMemView.dispatcher.AddListener            ( BattleEvent.MemUltOnClick_Event, MemUltOnClickHandler );          /// 监听 大招技能点击 事件

                BaseInit();                                                                                                     /// 基础数据初始化
                SkillDataInit();                                                                                                /// 技能数据初始化
                LoadMemDataComplete();                                                                                          /// 加载成员数据完成处理
            }
            public override void        OnRemove()                                                                              //  移除<End>  
            {
                InMemView.dispatcher.RemoveListener(BattleEvent.MemUltOnClick_Event, MemUltOnClickHandler);                     /// 移除 大招技能点击 事件
            }

        
            public void                 BaseInit()                                                                              //  基础数据 初始化<Root>   
            {
                                                                                                                                /// <| 基础数据配置 >
                Hp                                          = MaxHp;                                                            /// 血量
                Energy                                      = 0;                                                                /// 能量
                AttackCount                                 = 0;                                                                /// 攻击次数累计
                UltSkillCount                               = 0;                                                                /// 大招次数累计
                if (IMemData.MemberType == Battle_MemberType.Npc_WorldBoss) Hp = IMemData.CurrHp;                               /// 世界Boss血量修正

                IMemUI.SetHpSliderValue(Hp);                                                                                    /// 血量条
                IMemUI.SetEnergySliderValue(Energy);                                                                            /// 能量条
                Hero_C                                      = Configs_Hero.sInstance.GetHeroDataByHeroID(IMemData.MemberID);    /// 成员配置数据 设置
                TrialMove                                   = StraightTrialMove.BuildMovable();                                 /// 轨迹移动 设置(默认 直线轨迹)


                                                                                                                                ///<| 组件结构数据 配置 >
                MemBuff                                     = this.gameObject.AddComponent<BufferManagerItem>();                /// 成员BUFF 组件配置
                MemBuff.Owner                               = this;                                                             /// 成员BUFF 数据设置

                ColorManger                                 = this.gameObject.AddComponent<ColorMangerItem>();                  /// 颜色管理 组件配置
                ColorManger.Owner                           = this;                                                             /// 颜色管理 数据设置

                DefendSolve                                 = new DefenderDamageSolve();                                        /// 守方结算 组件配置 
                DefendSolve.Owner                           = this;                                                             /// 守方结算 数据设置 

                AttackMove                                  = this.gameObject.AddComponent<MoveToAttackItem>();                 /// 移动攻击 组件配置
                AttackMove.Owner                            = this;                                                             /// 移动攻击 数据设置
            }
            public void                 SkillDataInit()                                                                         //  技能数据 初始化         
            {
                Debug.Log("[ __Sub Thread ]_ MemSkillDataInit()_MemID:"+ IMemData.MemberID + " (成员技能数据初始化...)");

                AttackNormal                                = this.gameObject.AddComponent<NormalAttackItem>();                 /// 普通攻击 组件配置
                AttackNormal.Owner                          = this;                                                             /// 普通攻击 数据设置

                AttackSkill_1                               = this.gameObject.AddComponent<ActiveSkillItem>();                  /// 主动技能_1 组件配置
                AttackSkill_1.Owner                         = this;                                                             /// 主动技能_1 数据设置
                if (IMemData.ActiveSkillLv_1 > 0 )          AttackSkill_1.ActSkill_D = SkillDataBuilder.BuildActiveSkill        /// 主动技能_1 数据设置
                                                            (IMemData.ActiveSkillID_1, IMemData.ActiveSkillLv_1);
    
                AttackSkill_2                               = this.gameObject.AddComponent<ActiveSkillItem>();                  /// 主动技能_2 组件配置 
                AttackSkill_2.Owner                         = this;                                                             /// 主动技能_2 数据设置
                if (IMemData.ActiveSkillLv_2 > 0)           AttackSkill_2.ActSkill_D = SkillDataBuilder.BuildActiveSkill        /// 主动技能_2 数据设置
                                                            (IMemData.ActiveSkillID_2, IMemData.ActiveSkillLv_2);

                UltSkill                                    = this.gameObject.AddComponent<UltSkillItem>();                     /// 大招技能 组件配置
                UltSkill.Owner                              = this;                                                             /// 大招技能 数据设置
            if (IMemData.UltSkillLv > 0)
                {   UltSkill.UltSkill_D                     = SkillDataBuilder.BuildUltSkill(IMemData.UltSkillID, IMemData.UltSkillLv);     }

                BossSkill_D                                 = SkillDataBuilder.BuildBossSkill(IMemData.BossSkillID, IMemData.BossSkillLv);  /// Boss技能数据设置
            }
            public void                 LoadMemDataComplete()                                                                   //  加载成员数据完成处理     
            {
                if (Camp == Battle_Camp.Our)                                                                                    //  加载我方成员数据 
                {
                    BattleControll.sInstance.OurTeam.TeamList.Add(this);                                                        /// 加载成员数
                    dispatcher.Dispatch(BattleEvent.LoadBattleOurMemberComplete_Event,   MemPos_D.FixedPosNum);                 /// 加载成员数据完成 事件
                }
                else                                                                                                            //  加载敌方成员数据 
                {
                    BattleControll.sInstance.EnemyTeam.TeamList.Add(this);                                                      /// 加载成员数
                    dispatcher.Dispatch(BattleEvent.LoadBattleEnemyMemberComplete_Event, MemPos_D.FixedPosNum);                 /// 加载成员数据完成 事件
                }
            }


            public int                  HpAdd               ( int inAddValue, bool inIsCrit )                                   //  血量增加-<显示血条,显示加血数字> 
            {
                if ( Hp > 0)
                {
                    inAddValue          = Mathf.Abs(inAddValue);                                                                            /// 加血数值绝对值
                    inAddValue          = Mathf.Min(inAddValue, MaxHp - Hp);                                                                /// 修正加血上限值  
                    Hp                  += inAddValue;                                                                                      /// 增加血量
                    IMemUI.SetHpSliderValue(Hp);
                
                    if( inAddValue != 0 )
                    {
                        IMemUI.SetMoveMemUIShow(true);                                                                                      /// 模型上方血量条 显示
                        if (inIsCrit)                       IMemUI.TipAddHpBig(inAddValue);                                                 /// 显示加血数字 暴击加大
                        else                                IMemUI.TipAddHp(inAddValue);                                                    /// 显示加血数字 正常
                    }
                    return                                  inAddValue;                                                                     /// 返回
                }
                else                                        return 0;                                                                       //  返回
            }
            public int                  EnergyAdd           ( int inAddValue)                                                   //  能量增加    
            {
                if ( Hp  > 0)
                {
                    inAddValue                              = Mathf.Abs(inAddValue);                                                        //  
                    inAddValue                              = Mathf.Min(inAddValue, BattleParmConfig.MemberEnergyMax - Energy);             //  
                    Energy                                  += inAddValue;                                                                  //  
                    IMemUI.SetEnergySliderValue(Energy);                                                                                    //  

                    if ( Energy >= BattleParmConfig.MemberEnergyMax && UltSkill != null && UltSkill.UltSkill_D != null )                    //  能量满 大招释放激活
                    {    IMemUI.SetUltClick(true, false);            }
                    else
                    {    IMemUI.SetUltClick(false);                  }                                                                      //  大招释放关闭
                }
                return 0;
            }
            public int                  HpLost              ( int inLostValue, bool inIsCrit)                                   //  减少血量    
            {   
                bool                    IsBossSkillFired    = true;                                                                         //  Boss被动技能触发
                inLostValue                                 = Mathf.Abs(inLostValue);                                                       //  失血绝对值
                inLostValue                                 = Mathf.Min(Hp, inLostValue);                                                   //  失血上限
                Hp                                          -= inLostValue;                                                                 //  降低血线
                IMemUI.SetHpSliderValue(Hp);                                                                                                //  设置Hp
                if(IsBossSkillFired)                                                                                                        //  Boss技能触发 
                {
                    if(!_IsProcessOver && Hp > 0 && BossSkill_D != null && BossSkill_D.IsFired == false && Hp <= MaxHp * 0.3f )             // Boss血量小于30%_触发一次 
                    {
                        List<IBattleMemMediator> TheDefendList      = new List<IBattleMemMediator>();

                        BossSkill_D.IsFired                         = true;                                                                 // 大招是否已释放
                        TheDefendList                               = AttackTarget.GetAttackTargetList(this, BossSkill_D.AttackRange);      // 守方成员列表

                        foreach ( var Item in BossSkill_D.SkillEffectList )
                        {
                            for( int i = 0; i < TheDefendList.Count; i++ )
                            {    Item.Exec(this, TheDefendList[i],TheDefendList, 1, 1);         }                                           // 基类虚方法
                        }
                    }
                }

                if (inLostValue != 0)
                {
                    IMemUI.SetMoveMemUIShow(true);                                                                                          /// 模型上方血量条 显示
                    if (inIsCrit)                           IMemUI.TipDownHpBig(inLostValue);                                               /// 显示失血数字 暴击加大
                    else                                    IMemUI.TipDownHp(inLostValue);                                                  /// 显示失血数字 正常
                }
                if ( Hp <= 0 )                              OwnerDie();                                                                     //  成员死亡
                return                                      inLostValue;
            }
            public int                  EnergyLost          ( int inAddValue)                                                   //  减少能量    
            {
                inAddValue                                  = Mathf.Abs(inAddValue);                                                        /// 
                inAddValue                                  = Mathf.Min(Energy, inAddValue);                                                /// 
                Energy                                      -= inAddValue;                                                                  /// 

                if ( inAddValue != 0 )                      IMemUI.SetEnergySliderValue(Energy);                                            /// 
                return                                      inAddValue;                                                                     /// 
            }

            public void                 StartAcion          ( float inDelay)                                                    //  开始行动    
            {   StartCoroutine( startAction(inDelay));      }
            public void                 StartAttackCD       ()                                                                  //  开始攻击CD  
            {
                if ( Hp > 0 && MemState == BattleMemState.Normal )
                {
                    _IsProcessOver                          = false;                                                                        //  进度未结束
                    isAttacking                             = true; ;                                                                       //  攻击状态
                    StopCoroutine(AttackCD());                                                                                              //  停止迭代线程(AttackCD())
                    if (!_IsProcessOver && MemState == BattleMemState.Normal )
                    {
                        MemAnimEffect.Standby();                                                                                            //  成员待机状态
                        StartCoroutine(AttackCD());                                                                                         //  开始迭代线程(AttackCD())
                    }
                }
            }
            public void                 MemUltOnClickHandler()                                                                  //  释放大招点击
            {
                if( isAttacking     && UltSkill!= null            && BattleControll.sInstance.BattleState == BattleState.Fighting &&
                    !_IsProcessOver && MemBuff.IsAllowUltAttack() && MemState == BattleMemState.Normal                    )
                {    UltFire();                                                                                           }

                if (InBattleStart_D.BattleType == BattleType.GuideFirstBattle && IMemData.isRoleHero)                           // 开场战斗_释放大招 下一进度
                {   dispatcher.Dispatch(BattleEvent.FristBattleNext_Event, 2); }
            }
            public void                 UltFire()                                                                               //  大招释放    
            {
               if ( Camp == Battle_Camp.Our )              BattleReport.sInstance.AddUltEvent(this);

                IMemUI.SetUltClick(false);
                IsShowed                                    = false;
                StopAction();
                MemAnimEffect.MemAnimState                  = BattleMemAnimState.Standby;
                UltSkillCount++;
                UltSkill.AttackStart();
            }
            public void                 Hit                 ( bool inIsHard = false, Action inCallback = null )                 //  受击        
            {
                if ( Hp > 0 && (   MemAnimEffect.MemAnimState == BattleMemAnimState.Standby                                                 // 成员 待机, 普通受击, 重击状态
                                || MemAnimEffect.MemAnimState == BattleMemAnimState.NormalHit
                                || MemAnimEffect.MemAnimState == BattleMemAnimState.HardHit))
                {
                    float               TheTime             = 0;
                    TheTime             = MemAnimEffect.HardHit();
                    if ( TheTime > 0 )
                    {
                        if ( _IsBeingHited )                                                                                                // 替换上一个受击动作时,刷新更新状态时间,有延迟攻击事件,一并延迟
                        {    BattleControll.sInstance.TheMono.StopCoroutine ( EndHit(TheTime));       }
                        BattleControll.sInstance.TheMono.StartCoroutine     ( EndHit(TheTime));
                        _IsBeingHited                       = true;
                    }
                    else
                    {   MemAnimEffect.Standby();            }    
                }

                inCallback();
            }

            public void                 Graying             ()                                                                  //  灰屏        
            {
                ScaleState              = NoScaleTimeState.NoScale;                                                                         /// 时间进度停止 
                dispatcher.Dispatch     (BattleEvent.SceneGrey_Event, this);                                                                /// 灰屏事件 
            }
            public void                 GrayReleasing       ()                                                                  //  释放灰屏    
            {
                ScaleState              = NoScaleTimeState.Over;
                foreach ( var Item in BattleControll.sInstance.OurTeam.TeamList)
                {       if ( Item.ScaleState == NoScaleTimeState.NoScale)           return;    }

                foreach ( var Item in BattleControll.sInstance.EnemyTeam.TeamList)
                {       if ( Item.ScaleState == NoScaleTimeState.NoScale)           return;    }

                dispatcher.Dispatch     (BattleEvent.SceneGreyRelease_Event);
            }
            public void                 EnergyRecoverWave   ()                                                                  //  战斗波次,能量,血量恢复  
            {
                if( Hp > 0)
                {
                    HpAdd(IMemData.BloodRegen, false);                                                                                      /// 恢复血量
                    int                 TheEnergy           = EnergyAdd(IMemData.EnergyRegen);                                              /// 恢复能量
                    if (TheEnergy > 0)  IMemUI.TipAddEnergy(TheEnergy);                                                                     /// Tips显示
                }
            }
            public void                 IsShow              ( bool inIsShow )                                                   //  设置显示    
            {
                InMemView.ShowSet(inIsShow);
            }
            public void                 Delay               ( float inDurat, Action inTask)                                     //  延迟,直接调用controller对应方法 
            {
                StartCoroutine(RunDelayTask(inDurat, inTask));
            }
            public void                 OwnerDie            ()                                                                  //  成员死亡    
            {
                int                     MemCount            = OurTeam.GetAliveMemCount();                                                   //  敌方可攻击成员数量
                float                   DieTime             = MemAnimEffect.Death();

                isAttacking             = false;
                MemState                = BattleMemState.Dead;
                StopAction();

                MemBuff.ClearBuffer();                                                                                                      /// 清理成员buff
                IMemUI.SetUltClick(false);                                                                                                  /// 大招Btn (false)
                IMemUI.MemberDie();                                                                                                         /// 成员UI 设置死亡

                ShowDropCoin();                                                                                                             /// 死亡金币掉落
                ShowDropBossBox();                                                                                                          /// 死亡Boss宝箱掉落
                                                                                                                                            /// <@敌方最后一波最后一个成员 播放镜头拉近>
                if ( MemCount < 1)
                {
                    BattleControll.sInstance.BattleState = BattleState.PreprogressOver;
                    if ( IMemData.BattleCamp == Battle_Camp.Enemy && BattleControll.sInstance.EnemyProgress == BattleControll.sInstance.EnemyProgressMax - 1)
                    {    BattleControll.sInstance.CameraZoomInMember(this, DieTime);        }
                }

                BattleControll.sInstance.TheMono.StartCoroutine( HideModel(DieTime + 0.2f) );                                               /// 隐藏模型(隐藏尸体)

            }
            public void                 AttackOver          ()                                                                  //  攻击结束    
            {
                if( !_IsProcessOver && MemState == BattleMemState.Normal )
                {
                    MemAnimEffect.Standby();                                                                                                //  成员返回待机状态
                    StartCoroutine(AttackCD());                                                                                             //  关闭迭代线程 ( 攻击CD ) 
                }
            }
            public void                 ProgerssOver        ()                                                                  //  进度结束    
            {
                isAttacking                                 = false;                                                                        // 攻击状态: flase
                _IsProcessOver                              = true;                                                                         // 进度完成: true
                MemBuff.ClearBuffer();                                                                                                      // 清理成员buff
                AttackCount                                 = 0;                                                                            // 攻击累计清零
                StopAction();                                                                                                               // 停止行动
                InMemView.CloseEffect(false);                                                                                               // 关闭成员特效

                if ( MemState == BattleMemState.Normal && BattleControll.sInstance.EnemyProgress >= BattleControll.sInstance.EnemyProgressMax )   
                {
                    if ( Camp == Battle_Camp.Our )          MemAnimEffect.Victory();
                    else                                    MemAnimEffect.Standby();
                }
            }
            public void                 StopAction          ()                                                                  //  停止行动    
            {
                StopCoroutine(AttackCD());                                                                                                  //  关闭 攻击CD线程
                StopAllCoroutines();                                                                                                        //  关闭所有迭代线程

                TrialMove.Stop();                                                                                                           //  移动轨迹   停止
                AttackNormal.StopAction();                                                                                                  //  普通攻击   停止
                AttackSkill_1.StopAction();                                                                                                 //  主动技能_1 停止
                AttackSkill_2.StopAction();                                                                                                 //  主动技能_2 停止
                UltSkill.StopAction();                                                                                                      //  大招技能   停止
            }

            public void                 ProgressOver        ()                                                                  //  当前波次结束 
            {
                isAttacking             = false;
                _IsProcessOver          = true;
                MemBuff.ClearBuffer();
                AttackCount             = 0;
                StopAction();
                InMemView.CloseEffect   (false);

                if ( MemState == BattleMemState.Normal && BattleControll.sInstance.EnemyProgress >= BattleControll.sInstance.EnemyProgressMax )
                {
                    if ( Camp == Battle_Camp.Our )          MemAnimEffect.Victory();
                    else                                    MemAnimEffect.Standby();
                }
            }
            public void                 MoveToNextWavePos   (Vector3 inPos, float MoveTime, float inDelay)                      //  移动到下一波 站位   
            {
                Debug.Log("MoveNextWave!");
                MemAnimEffect.Runing();                                                                                         //  跑步动作
                MemAnimEffect.PlayRunEffect(MoveTime);                                                                          //  跑步特效
                IMemUI.SetMoveMemUIShow(false);                                                                                 //  模型上面血条 隐藏
                TrialMove.MoveTo(ModelObj,inPos, MoveTime, () =>                                                                //  回调 ( 封装方法 )
                {
                    MemAnimEffect.Standby();
                    StartAcion(inDelay);
                });                 
            }

            #endregion

            #region================================================||   PrivateModel--    < 私有模块 函数_声明 > ||<FourNode>================================================

            private bool                _IsBeingHited       = false;                                                            //  已受击
            private bool                _IsProcessOver      = true;                                                             //  当前波次结束
            private Action              _ExcAttack          = null;                                                             //  执行攻击

            IEnumerator                 startAction         ( float inDelay )                                                   //  开始行动          
            {
                yield return new WaitForSeconds(inDelay);
                _IsProcessOver                              = false;
                isAttacking                                 = true;
                AttackStart();
            }
            IEnumerator                 AttackCD            ()                                                                  //  开始攻击CD(间隔)   
            {
                if (BattleControll.sInstance.BattleState == BattleState.Fighting )
                {
                    IMemUI.SetEnergySliderValue(Energy);                                                                                    //  大招自动释放检测

                    if (Hp > 0)                             AttackNormal.SetDefender();
                    yield return new WaitForSeconds         (AttackInterval);                                                               //  延迟 (攻击间隔)                
                    if (!_IsBeingHited)                     BeginAttack();
                    else                _ExcAttack          = BeginAttack;                        
                }
            }
            IEnumerator                 EndHit              (float inTime)                                                      //  刷新受击状态，执行被延迟的攻击事件
            {
                yield return new WaitForSeconds(inTime);
                _IsBeingHited = false;
                if (_ExcAttack != null )
                {
                    _ExcAttack();       _ExcAttack          = null;                                                                         //  释放攻击
                }
                if (Hp > 0 && (MemAnimEffect.MemAnimState == BattleMemAnimState.NormalHit || MemAnimEffect.MemAnimState == BattleMemAnimState.HardHit))
                {   MemAnimEffect.Standby();                }                                                                               //  返回待机状态
            }
            IEnumerator                 HideModel           (float inTime)                                                      //  隐藏模型(隐藏尸体) 
            {
                yield return new WaitForSeconds(inTime);
                MemAnimEffect.PlayTimer_E(BattleResStrName.DeathEffect, 2);                                                     //  死亡特效
                yield return new WaitForSeconds(MemAnimEffect.Sink());                                                          //  死亡后沉默特效

                IsShow                  (false);                                                                                //  显示设置
                InMemView.CloseEffect   ();                                                                                     //  关闭特效
                OurTeam.TeamList.Remove (this);                                                                                 //  移除出队伍列表
                dispatcher.Dispatch     (BattleEvent.OverBattleWave_Event);                                                     //  本波战斗结束 事件

                MemState                                    = BattleMemState.Destroy;                                           //  成员状态:销毁
                InMemView.DestroyObj    ();                                                                                     //  销毁对象
            }   
            IEnumerator                 RunDelayTask        (float inDurat, Action inTask)                                      //  运行延迟任务       
            {
                yield return new WaitForSeconds(inDurat);
                inTask();
            }
            private void                AttackStart         ()                                                                  //  攻击开始          
            {
                if ( Hp <= 0 || isAttacking != true )       return;

                if (!_IsProcessOver && MemState == BattleMemState.Normal )
                {
                    int                 Order               = 0;

                    if ( AttackCount == 0)                  ActiveSkillAttack();                                                            // 主动技能攻击
                    IMemUI.SetEnergySliderValue(Energy);                                                                                    // 检测大招是否自动释放
                    Order                                   = AttackCount % 4 + 1;
                    AttackCount++;

                    bool                AllowSkill          = MemBuff.IsAllowSkillAttack();                                                 // 是否允许技能攻击

                    if      ( AllowSkill && AttackSkill_1 != null && AttackSkill_1.ActSkill_D != null && Order == AttackSkill_1.ActSkill_D.ReleaseTime)
                    {         AttackSkill_1.AttackStart();                  }                                                               // 主动技能_1 攻击初始化
                    else if ( AllowSkill && AttackSkill_2 != null && AttackSkill_2.ActSkill_D != null && Order == AttackSkill_2.ActSkill_D.ReleaseTime)
                    {         AttackSkill_2.AttackStart();                  }                                                               // 主动技能_2 攻击初始化
                    else if ( MemBuff.IsAllowNormalAttack() )                                                                               // 普通攻击               
                    {
                        bool            IsVibrat            = Configs_Hero.sInstance.GetHeroDataByHeroID(IMemData.MemberID).ShockScreen == Order;
                        AttackNormal.AttackStart();
                    }
                }
            }
            private void                ActiveSkillAttack   ()                                                                  //  主动技能攻击次序   
            {
                if (AttackSkill_1 != null && AttackSkill_1.ActSkill_D != null && AttackSkill_1.ActSkill_D.ReleaseTime == 0 )
                {
                    List<IBattleMemMediator> TheTargetList  = AttackTarget.GetAttackTargetList(this, AttackSkill_1.ActSkill_D.AttackRange);

                    foreach(var Item in AttackSkill_1.ActSkill_D.SkillEffectList )
                    {
                        Item.SkillCD                        = 300;
                        for ( int i = 0; i < TheTargetList.Count; i++ )
                        {     Item.Exec(this, TheTargetList[i], TheTargetList, 1, 1);        }
                    }
                }
                if (AttackSkill_2 != null && AttackSkill_2.ActSkill_D != null && AttackSkill_2.ActSkill_D.ReleaseTime == 0)
                {
                    List<IBattleMemMediator> TheTargetList  = AttackTarget.GetAttackTargetList(this, AttackSkill_2.ActSkill_D.AttackRange);

                    foreach (var Item in AttackSkill_2.ActSkill_D.SkillEffectList)
                    {
                        Item.SkillCD                        = 300;
                        for ( int i = 0; i < TheTargetList.Count; i++)
                        {     Item.Exec(this, TheTargetList[i], TheTargetList, 1, 1);       }
                    }
                }
            }
            private void                BeginAttack         ()                                                                  //  开始攻击          
            {
                if (!_IsProcessOver && MemState == BattleMemState.Normal )
                {
                    if (AttackMove.MoveTarget != null ) AttackMove.MoveTarget.RemoveTarget();
                    AttackStart();                                                                                                  
                }
            }

            private void                ShowDropCoin        ()                                                                  //  显示掉落金币      
            {
                if (InBattleStart_D.BattleType == BattleType.CheckPoint && IMemData.MemberType == Battle_MemberType.Npc_CheckPointBoss )
                {
                    int                     TheCoinCount            = 0;                                                                    //  金币数量
                    int                     GoldIcon                = 0;                                                                    //  金币图标 数量
                    if (BattleControll.sInstance.EnemyProgress < 2)                                                                         //  前两波( 30% 金币数 2个金币图标)
                    {
                        TheCoinCount                        = (int)(InBattleStart_D.rewardCoin * 0.3f);
                        GoldIcon                            = 2;
                    }
                    else                                                                                                                    //  第三波( 40% 金币数 3个金币图标)
                    {
                        TheCoinCount                        = (int)(InBattleStart_D.rewardCoin * 0.4f);
                        GoldIcon                            = 3;
                    }
                    for (int i = 1; i < GoldIcon; i++ )
                    {
                        int                 OnceCoin                = TheCoinCount / GoldIcon;
                        UnityEngine.Object  TheObj                  = Util.Load(BattleResStrName.PanelName_CoinBox);                        /// 加载资源
                        GameObject          TheCoinBox              = Instantiate(TheObj) as GameObject;                                    /// 实例化资源对象
                        if ( TheCoinBox != null)                                                                                            //  设置TheCoinBox参数  
                        {
                            TheCoinBox.name                         = OnceCoin.ToString();                                                  /// 对象名称设置
                            TheCoinBox.transform.parent             = ModelObj.transform.parent;                                            /// 设定父级对象

                            TheCoinBox.transform.localPosition      = ModelObj.transform.localPosition;                                     /// 位移设置
                            TheCoinBox.transform.localScale         = ModelObj.transform.localScale;                                        /// 缩放设置
                            TheCoinBox.transform.localRotation      = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);             /// 旋转(Y轴随机角度)
                        }

                        TimeEndHandler      TheEndHandler           = TheCoinBox.GetComponent<TimeEndHandler>();                            //  金币模型存在时长 面板配置
                        TheEndHandler.TimeEndCallBack               = delegate ()
                        {     dispatcher.Dispatch(BattleEvent.ShowCoinEffect_Event, OnceCoin);                    };                        /// 显示金币特效 事件

                        TheEndHandler.Run();
                        TheEndHandler.TimeEndToDo                   = TimeEndOperation.Destroy;

                        TheObj                                      = null;         
                    }
                }
            }
            private void                ShowDropBossBox     ()                                                                  //  显示掉落Boss宝箱  
            {
                if ( IMemData.MemberType == Battle_MemberType.Npc_WorldBoss && BattleParmConfig.IsBigBossType(InBattleStart_D.BattleType))
                {
                    int                     PassID              = (int)InBattleStart_D.TempObjData;                                         /// Boss关卡 ID
                    Configs_DragonTrialData TheDragon_C         = Configs_DragonTrial.sInstance.GetDragonTrialDataBydungeonID(PassID);      /// 配置数据
                    int                     TheCount            = TheDragon_C.FragmentID1.Count;                                            /// 掉落碎片数量

                    for ( int i = 0; i < TheCount; i++ )
                    {
                        UnityEngine.Object  TheObj              = BattleUtil.Load(BattleResStrName.PanelName_PropBox);                      /// 加载资源
                        GameObject          ThePorpBox          = Instantiate(TheObj) as GameObject;                                        /// 实例化资源对象

                        if (ThePorpBox != null)                                                                                             //  设置TheCBox参数 
                        {
                            ThePorpBox.name                     = "1";                                                                      /// 对象名称设置
                            ThePorpBox.transform.parent         = ModelObj.transform.parent;                                                /// 设定父级对象

                            ThePorpBox.transform.localPosition  = ModelObj.transform.localPosition;                                         /// 位移设置
                            ThePorpBox.transform.localScale     = ModelObj.transform.localScale;                                            /// 缩放设置
                            ThePorpBox.transform.localRotation  = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);                 /// 旋转(Y轴随机角度)
                        }

                        TimeEndHandler      TheEndHanler        = ThePorpBox.GetComponent<TimeEndHandler>();                                //  展示结束后处理
                        TheEndHanler.TimeEndCallBack            = delegate()
                        {     dispatcher.Dispatch(BattleEvent.ShowBoxEffect_Event,ThePorpBox);      };

                        TheEndHanler.Run();
                        TheEndHanler.TimeEndToDo                = TimeEndOperation.Destroy;

                        TheObj                                  = null;                                                                     //  释放内存
                    }
                }
            }
            #endregion

        }
    public interface                IBattleMemMediator
    {
            
        bool                        isAttacking                     { set; get; }                                   /// 攻击中

        int                         Hp                              { get; set; }                                   /// 血量
        int                         Energy                          { get; }                                        /// 能量
        int                         MaxHp                           { get; }                                        /// 最大血量

        int                         AttackCount                     { set; get; }                                   /// 攻击累计次数
        int                         UltSkillCount                   { set; get; }                                   /// 大招累计次数
            
        float                       MoveSpeed                       { get; }                                        /// 移动速度
        float                       BodySize                        { get; }                                        /// 模型缩放比例
        float                       AttackRange                     { get; }                                        /// 攻击范围
        float                       AttackInterval                  { get; }                                        /// 攻击间隔
        

        NoScaleTimeState            ScaleState                      { set; get; }                                   /// 非拉伸 状态
        Battle_Camp                 Camp                            { get; }                                        /// 阵营
        BattleMemState              MemState                        { get; set; }                                   /// 战斗成员状态
        ITrialMove                  TrialMove                       { get; }                                        ///  轨迹移动
        IBattleMemberData           IMemData                        { get; }                                        /// 成员数据
        IBattleMemUI                IMemUI                          { get; }                                        /// 成员UI

        MemAnimEffectData           MemAnimEffect                   { get; }                                        /// 模型动作特效资源
        Configs_HeroData            Hero_C                          { get; }                                        /// 英雄配置数据
        GameObject                  ModelObj                        { get; }                                        /// 模型对象
        BattleTeam                  OurTeam                         { get; }                                        /// 我方队伍
        BattleTeam                  EnemyTeam                       { get; }                                        /// 敌方队伍
        BattlePosData               MemPos_D                        { get; }                                        /// 固定位置号码
        DefenderDamageSolve         DefendSolve                     { get; set; }                                   /// 守方伤害结算
        BossSkillData               BossSkill_D                     { get; set; }                                   /// Boss技能数据

        MoveToAttackItem            AttackMove                      { get; set; }                                   /// 移动攻击
        NormalAttackItem            AttackNormal                    { get; set; }                                   /// 正常攻击
        ActiveSkillItem             AttackSkill_1                   { set; get; }                                   /// 主动技能_1
        ActiveSkillItem             AttackSkill_2                   { set; get; }                                   /// 主动技能_2
        UltSkillItem                UltSkill                        { get; set; }                                   /// 大招技能
        BufferManagerItem           MemBuff                         { get; set; }                                   /// 成员BUff
        ColorMangerItem             ColorManger                     { get; set; }                                   /// 颜色管理

        int                         HpAdd(int inAddValue, bool inIsCrit);                                           /// 血量增加
        int                         EnergyAdd(int inAlfa);                                                          /// 能量增加
        int                         HpLost(int inAlfa, bool inIsCrit);                                              /// 减少血量
        int                         EnergyLost(int inAlfa);                                                         /// 减少能量

        void                        StartAcion(float inDelay);                                                      /// 开始行动
        void                        StartAttackCD();                                                                /// 开始攻击CD(间隔)
        void                        UltFire();                                                                      /// 大招发动 
        void                        Hit ( bool inIsHard = false, System.Action inCallBack = null);                  /// 受击

        void                        Graying             ();                                                         /// 灰屏
        void                        GrayReleasing       ();                                                         /// 释放灰屏
        void                        EnergyRecoverWave   ();                                                         /// 战斗波次后 能量恢复
        void                        MoveToNextWavePos   (Vector3 inPos, float MoveTime, float StartDelay);          /// 移动到下一波 站位
        void                        IsShow              (bool inIsShow);                                            /// 设置显示
        void                        Delay               (float inDurat, System.Action inTask);                      /// 延迟,直接调用controller对应方法
        void                        OwnerDie            ();                                                         ///  成员死亡
        void                        StopAction          ();                                                         ///  停止行动
        void                        AttackOver          ();                                                         ///  攻击结束
        void                        ProgerssOver        ();                                                         ///  推进结束

    }
}