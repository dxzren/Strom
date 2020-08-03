using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace StormBattle
{
    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 战斗系统配置参数 </summary>
    public class BattleParmConfig                                                                                           
    {
        public const int        LayerDefault                    = 0;                                                        /// 3D对象      层级
        public const int        LayerGrey                       = 1;                                                        /// 黑屏摄像机的 层级
        public const int        LayerPublicHitEffect            = 2;                                                        /// 通用被击特效 层级
                                                        
        public const int        CameraNormalDept                = -1;                                                       /// 3D摄影机深度 
        public const int        CameraGreyDept                  = 2;                                                        /// 摄影机深度

        public const int        MaxCDTime                       = 600;                                                      /// 单位表
        public const int        MemberEnergyMax                 = 1000;                                                     /// 大招能力值上限  
        public const int        MemberUltExEnergy               = 1000;                                                     /// 大招释放消耗

        public static float     StartBattleDelay                = CustomJsonUtil.GetValueToFloat("AttackTimeInterVal");     /// 战斗开始序列的攻击延迟  (0.3f)
        public static float     startBattleDelayPvp             = CustomJsonUtil.GetValueToFloat("AttackTimeIntervalPvp");  /// 战斗开始序列的攻击延迟 -(PVP) (0.3f)

        public const float      CameraToFov                     = 26.5f;                                                    /// 切换波次过程中摄影机需要调整到的视角范围

        public const float      ThunderHoldTime                 = 0.3f;                                                     /// 闪电链接保持时长
        public const float      ImpactProtectTime               = 0.2f;                                                     /// 背击动作保护时长

        public const float      ShootSpeed                      = 15;                                                       /// 直接轨迹   速度
        public const float      ParabolaSpeed                   = 15;                                                       /// 抛物线轨迹 速度
        public const float      ThunderSpeed                    = 20;                                                       /// 闪电轨迹   速度

        public const float      MemberSprintSpeed               = 20;                                                       /// 成员 冲刺  速度<刺客突击后排>
        public const float      MemberMoveSpeed                 = 4;                                                        /// 成员 移动  速度
        public const float      MemberRepelSpeed                = 12;                                                       /// 成员 击退  速度
        public const float      MemberRepelDistance             = 0.5f;                                                     /// 成员 击退  距离
        public static float     NeighbourRange                  = 2f;                                                       /// 相邻距离

        public static float     MemberBody_Boss                 = 12f;                                                      /// Boss成员模型大小比例
        public static float     MemberBody_Normal               = 1.3f;                                                     /// 普通成员模型大小比例

        public static float     TipsIntervalTime                = 0.2f;                                                     /// Buff提示 间隔时长
        public static float     TimeScaleNormal                 = 1.2f;                                                     /// 正常 时间速度
        public static float     TimeScaleZero                   = 0;                                                        /// 静止 时间速度
        public static float     SpeedPara_X2                    = 2.0f;                                                     /// 2倍加速
        public static float     SpeedPara_X3                    = 3.0f;                                                     /// 3倍加速
        public static float     TimeScaleZoom                   = 0.4f;                                                     /// 镜头拉近时间
        public static float     TimeScaleUltFired               = TimeScaleNormal;                                          /// 大招释放时间

        public static bool      IsAutoBattle                    = false;                                                    /// 自动战斗开关

        public static Vector3   MemberModelSize_Normal          = new Vector3( 1,1,1);                                      /// 正常比例 
        public static Vector3   MemberModelSize_Normal_2        = new Vector3( 1.1f, 1.1f, 1.1f );                          /// 竞技场          英雄1.1
        public static Vector3   MemberModelSize_Normal_3        = new Vector3( 1.2f, 1.2f, 1.2f );                          /// 巨龙第冰龙      英雄1.2

        public static Vector3   MemberModelSize_Boss_1          = new Vector3( 1.3f, 1.3f, 1.3f );                          /// 关卡 小boss
        public static Vector3   MemberModelSize_Boss_2          = new Vector3( 1.4f, 1.4f, 1.4f );                          /// 巨龙 巨兽中的大boss
        public static Vector3   MemberModelSize_Boss_3          = new Vector3( 1.5f, 1.5f, 1.5f );                          /// 巨龙 巨兽中的大boss

        private static UnityEngine.Object _ShadowObj            = null;                                                     /// 战斗成员阴影 对象


        public static bool      IsBigBossType           (BattleType inBattleType )                                          // 是否世界Boss类型       
        {
            switch (inBattleType)
            {
                case BattleType.MonsterWarPhy:
                case BattleType.MonsterWarMagic:
                case BattleType.DragonTrialIce:
                case BattleType.DragonTrialThunder:                 return true;
                default:                                            return false;
            }
        }
        public static bool      IsChangeScene                                                                               // 是否更换场景(第三波_巨龙,巨兽更换)
        {
            get
            {
                if (BattleControll.sInstance.EnemyProgress != 2)        return false;
                switch ( BattleControll.sInstance.BattleType)
                {
                    case BattleType.DragonTrialIce:
                    case BattleType.DragonTrialFire:
                    case BattleType.DragonTrialThunder:
                    case BattleType.MonsterWarPhy:
                    case BattleType.MonsterWarMagic:                    return true;
                    default:                                            return false;
                }
            }
        }
        public static float     GetMemberModelSize      (Battle_MemberType inMemType)                                       // 获取成员尺寸         
        {
            switch(inMemType)
            {
                case Battle_MemberType.Npc_WorldBoss:           return MemberBody_Boss;
                default:                                        return MemberBody_Normal;
            }
        }
        public static float     GetMemberBodyHeight     ( int inResID )                                                     // 获取成员血条高度        
        {
            return Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID(inResID).HeroBloodHeight;
        }

        public static float     GetTimeByIndexNumPVE    ( int inIndex,Battle_Camp inCamp )                                  // 获取时间速度            
        {
            float               TimeSpeed                       = CustomJsonUtil.GetValueToFloat("FirstHeroArrivalTime");   // 关卡中首位英雄到达时间（PVE）
            if (inCamp == Battle_Camp.Enemy)                    TimeSpeed += CustomJsonUtil.GetValueToFloat("NpcTimeDelay");// 敌方整体时间延迟( PVE )
            TimeSpeed                                           += CustomJsonUtil.GetValueToFloat("ArrivalTimeInterval");   // 到达时间间隔（适用npc、英雄）
            return              TimeSpeed;
        }
        public static float     GetCompareNumByMem      ( IBattleMemMediator inMemMediator )                                // 获取成员速度排序比较参数 
        {
            int                 X_AxisNum                           = 3 * (int)inMemMediator.MemPos_D.XPos;                 /// 竖排位置号码
            int                 FixedPosNum                         = (int)inMemMediator.MemPos_D.FixedPosNum;              /// 固定位置号码

            float               CompareNum                          = (X_AxisNum * 5) - Mathf.Pow(-2f, X_AxisNum - FixedPosNum);
            return              CompareNum;                                                                                 /// 比较参数
        }
        public static float     GetAttackRange          ( BattlePosData inPos_D)                                          // 获取攻击范围   
        {
            switch (inPos_D.XPos )
            {
                case X_Axis.X_04:
                case X_Axis.X_03:                               return 1;   
                case X_Axis.X_05:
                case X_Axis.X_02:                               return 2;
                case X_Axis.X_01:
                case X_Axis.X_06:                               return 3;
                default: Debug.LogError("X_Axis Out of range"); return 0;
            }
        }
        public static float     GetEnergyRate           ( BattlePosData inPos_D )                                        // 获取能量系数   
        {
            switch(inPos_D.XPos)
            {
                case X_Axis.X_03:
                case X_Axis.X_04:                               return 0.5f;
                case X_Axis.X_05:
                case X_Axis.X_02:                               return 0.7f;
                default:                                        return 1f;
            }
        }

        public static Vector3   GetMemberModelScale     ( IBattleMemberData inMem_D )                                       // 获取成员模型 缩放比例 
        {
            Vector3             TheMemScale                     = new Vector3(1, 1, 1);                                     /// 缩放实例

            switch (inMem_D.MemberType)                                                                                     /// 成员类型
            {
                case Battle_MemberType.Hero:                                                                                /// 英雄       
                    {
                        if( BattleControll.sInstance.BattleType == BattleType.JJC ||
                            BattleControll.sInstance.BattleType == BattleType.JJCLevel||
                            BattleControll.sInstance.BattleType == BattleType.ParadiseRoad)
                        {
                            return MemberModelSize_Normal_2;
                        }
                        return MemberModelSize_Normal;
                    }
                case Battle_MemberType.Npc_Normal:              return TheMemScale;                                         /// 普通NPC
                case Battle_MemberType.Npc_CheckPointBoss:      return MemberModelSize_Boss_1;                              /// 关卡小Boss
                case Battle_MemberType.Npc_WorldBoss:                                                                       /// 世界大Boss  
                    {
                        switch(BattleControll.sInstance.BattleType)
                        {
                            case BattleType.MonsterWarPhy:
                            case BattleType.MonsterWarMagic:    return MemberModelSize_Boss_1;
                            case BattleType.DragonTrialIce:     return MemberModelSize_Boss_2;
                            case BattleType.DragonTrialFire:
                            case BattleType.DragonTrialThunder: return MemberModelSize_Boss_3;
                            case BattleType.GuideFirstBattle:   return MemberModelSize_Normal;
                            default:                            return MemberModelSize_Boss_1;
                        }
                    }
                default:                                        return TheMemScale;
            }
        }
        public static Vector3   ListToVector3           (List<float> inPosList)                                             // 轴向坐标列表 转化三维坐标    
        {
            if ( inPosList.Count >= 3)                          return new Vector3(inPosList[0],inPosList[1],inPosList[2]);
            else                                                return Vector3.zero;
        }
        public static Vector3   WorldToUiPoint          (Vector3 inWorldPoint)                                              // 世界顶点坐标 转化UI屏幕坐标  
        {
            Vector3             ThePoint                        = BattleControll.sInstance.MainCamera.WorldToScreenPoint(inWorldPoint);
            ThePoint.z                                          = 0;
            return              BattleControll.sInstance.UICamera.ScreenToWorldPoint(ThePoint);
        }
        public static Vector3   CameraPosSet            (BattleType inBattleMap,int inBattleWave)                           // 战斗场景摄影机位置    
        {
            if ( inBattleMap == BattleType.CheckPoint)                                                                      // 关卡节点位置
            {
                if      (inBattleWave == 0)                         return new Vector3(9,   7, 24);                         /// 出生点
                else if (inBattleWave == 1)                         return new Vector3(-1,  7, 24);                         /// 第一波节点
                else if (inBattleWave == 2)                         return new Vector3(-18, 7, 24);                         /// 第二波节点
                else if (inBattleWave == 3)                         return new Vector3(-37, 7, 24);                         /// 第三波节点
            }

            Debug.LogError          ("GetAcmeraPos_Error!");
            return                  Vector3.zero;
        }

        public static Color     GetMemberModelColor     (IBattleMemberData inMem_D)                                         // 获取成员模型 颜色亮度 
        {
            if (inMem_D.MemberType == Battle_MemberType.Npc_WorldBoss )
            {
                switch ( BattleControll.sInstance.BattleType)
                {
                    case BattleType.MonsterWarPhy:
                    case BattleType.MonsterWarMagic:            return new Color(190f / 255, 190f / 255, 190f / 255);
                    case BattleType.DragonTrialIce:             return new Color(180f / 255, 180f / 255, 180f / 255);
                    case BattleType.DragonTrialFire:            return new Color(150f / 255, 150f / 255, 150f / 255);
                    case BattleType.DragonTrialThunder:         return new Color(220f / 255, 220f / 255, 220f / 255);
                }
            }
            return              new Color(150f / 255, 150f / 255, 150f / 255, 1);
        }
        public static TimeSpan  BattleMaxTime                                                                               // 战斗最大时长*_(间隔 H,M,S)
        {
            get
            {
                if      ( BattleControll.sInstance.BattleType == BattleType.SecretTower)        return new TimeSpan(0,0,0);
                else if ( BattleControll.sInstance.BattleType == BattleType.Guild )             return new TimeSpan(0,3,0);
                else if ( BattleControll.sInstance.BattleType == BattleType.GuideFirstBattle)   return new TimeSpan(0,3000,0);
                else                                                                            return new TimeSpan(0,2,0);
            }
        }
        public static void      OverTween (UITweener inTween)                                                               // UI动画结束处理    
        {
            if ( inTween != null )
            {
                if (inTween.tweenFactor < 1)
                {
                    inTween.Sample(1, true);
                    inTween.onFinished.ForEach(P => P.Execute());
                }
                UnityEngine.GameObject.Destroy(inTween);
            }
        }
        public static void      StopTween (UITweener inTween)                                                               // 停止并摧毁UI动画  
        {
            if ( inTween != null )              GameObject.Destroy(inTween);
        }


        public static void      ReSetting()                                                                                 // 重新配置         
        {
            TimeScaleNormal                                     = 1.2f;                                                     // 正常速度
            TimeScaleZero                                       = 0;                                                        // 停止速度
            TimeScaleZoom                                       = 0.4f;                                                     // 镜头拉近
            TimeScaleUltFired                                   = TimeScaleNormal;                                          // 大招时间
            SpeedPara_X2                                        = 2.0f;                                                     // 2倍加速
            SpeedPara_X3                                        = 3.0f;                                                     // 3倍加速        
        }
        public static           UnityEngine.Object ShadowObj                                                                // 加载成员阴影对象   
        {
            get
            {
                if(_ShadowObj == null)                          _ShadowObj = Util.Load(BattleResStrName.PanelName_Shadow);
                return                                          _ShadowObj;
            }
        }

        public static List<string> PublicHitStrList             = new List<string>()                                        // 通用受击特效列表  
        {
            BattleResStrName.PublicHit_E_Melee1,                                                                            /// 重击_1 <战士>
            BattleResStrName.PublicHit_E_Melee2,                                                                            /// 重击_2 <战士>
            BattleResStrName.PublicHit_E_Cure1,                                                                             /// 治疗_1 <牧师>
            BattleResStrName.PublicHit_E_Cure2,                                                                             /// 治疗_2 <牧师>
            BattleResStrName.PublicHit_E_Assassin1,                                                                         /// 刺杀_1 <刺客>
            BattleResStrName.PublicHit_E_Assassin2,                                                                         /// 刺杀_2 <刺客>
            BattleResStrName.PublicHit_E_Shooter1,                                                                          /// 枪击,弓击 <射手>_1
            BattleResStrName.PublicHit_E_Shooter2,                                                                          /// 枪击,弓击 <射手>_2
            BattleResStrName.PublicHit_E_Suppout1,                                                                          /// 辅助 <辅助>
            BattleResStrName.PublicHit_E_Support2,                                                                          /// 辅助 <辅助>
            BattleResStrName.PublicHit_E_Ice,                                                                               /// 冰击 
            BattleResStrName.PublicHit_E_Fire,                                                                              /// 火击
            BattleResStrName.PublicHit_E_Thunder,                                                                           /// 雷击
        };
    }

    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 战斗资源名称 </summary>
    public class BattleResStrName                                                                                           
    {
        public const string     DeathEffect                     = "publiceffect_death";                                     /// 死亡特效
        public const string     BornEffectName                  = "publiceffect_born";                                      /// 天赋特效
        public const string     UltFired2Deffect                = "publiceffect_release";                                   /// 大招技能2D 特效
        public const string     RunningEffect                   = "publiceffect_running";                                   /// 跑动特效
        public const string     BindEffectTag                   = "bindeffect";                                             /// 绑定特效标签
        public const string     CameraZoomInEffect              = "publiceffect_deathcamera";                               /// 摄像机缩放特效(死亡)

        #region================================================||   通用击中特效 -- PublicHit         ||=====================================================

        public const string     PublicHit_E_Melee1              = "strikeeffect_melee1";                                    /// 重击_1 <战士>
        public const string     PublicHit_E_Melee2              = "strikeeffect_melee2";                                    /// 重击_2 <战士>
        public const string     PublicHit_E_Cure1               = "strikeeffect_cure1";                                     /// 治疗_1 <牧师>
        public const string     PublicHit_E_Cure2               = "strikeeffect_cure2";                                     /// 治疗_2 <牧师>
        public const string     PublicHit_E_Assassin1           = "strikeeffect_assassin1";                                 /// 刺杀_1 <刺客>
        public const string     PublicHit_E_Assassin2           = "strikeeffect_assassin2";                                 /// 刺杀_2 <刺客>
        public const string     PublicHit_E_Shooter1            = "strikeeffect_shooter1";                                  /// 枪击,弓击 <射手>_1
        public const string     PublicHit_E_Shooter2            = "strikeeffect_shooter2";                                  /// 枪击,弓击 <射手>_2
        public const string     PublicHit_E_Suppout1            = "strikeeffect_support1";                                  /// 辅助 <辅助>
        public const string     PublicHit_E_Support2            = "strikeeffect_support2";                                  /// 辅助 <辅助>
        public const string     PublicHit_E_Ice                 = "strikeeffect_ice";                                       /// 冰击 
        public const string     PublicHit_E_Fire                = "strikeeffect_fire";                                      /// 火击
        public const string     PublicHit_E_Thunder             = "strikeeffect_thunder";                                   /// 雷击
        #endregion
        #region================================================||   战斗面板名称 -- BattlePanelName   ||=====================================================

        public const string PanelName_NormalVictory             = "UIs/Battle/VictoryPanel";                                /// 普通胜利面板
        public const string PanelName_NormalFailure             = "UIs/Battle/FailurePanel";                                /// 普通失败面板
        public const string PanelName_PVPVictory                = "UIs/Battle/VictoryPvpPanel";                             /// PVP  胜利面板
        public const string PanleName_PVPFailure                = "UIs/Battle/FailurePvpPanel";                             /// PVP  失败面板
        public const string PanelName_PVP2Victory               = "UIs/Battle/VictoryPvp2Panel";                            /// PVP2 胜利面板

        public const string PanelName_AccountPVPFriend          = "UIs/Battle/AccountPvpFriendPanel";                       /// 好友对战面板
        public const string PanelName_DragonWarEnd              = "UIs/Battle/DragonWarEndPanel";                           /// 巨龙试炼 结算面板
        public const string PanelName_Pause                     = "UIs/Battle/PausePanel";                                  /// 暂停界面
        public const string PanelName_WingMet                   = "UIs/Battle/PanelWingMet";                                /// 翅膀面板

        public const string PanelName_LevelUp                   = "UIs/Battle/LevelUpPanel";                                /// 英雄升级面板
        public const string PanelName_HeroExp                   = "UIs/Battle/HeroExpAdd";                                  /// 英雄经验面板
        public const string PanelName_Award                     = "UIs/Battle/Award";                                       /// 奖励面板
        public const string PanelName_Award84                   = "UIs/Battle/Award84";                                     /// 奖励面板 84

        public const string PanelName_MemberUI                  = "UIs/Battle/NewBattleUIs/MemberUI/MemberUI";              /// 成员View

        public const string PanelName_Tip                       = "UIs/Battle/Tip";                                         /// 战斗Tips面板
        public const string PanelName_CoinBox                   = "UIs/Battle/CoinBox";                                     /// 金币箱子

        public const string PanelName_PropBox                   = "UIs/Battle/PropBox";                                     /// 道具箱子
        public const string PanelName_Shadow                    = "UIs/Battle/BattleShadow";                                /// 阴影

        public const string PanelName_DialogLeft                = "UIs/Battle/DialogLeftPanel";                             /// 对话左
        public const string PanelName_DialogRiget               = "UIs/Battle/DialogRightPanel";                            /// 对话右
        public const string PanelName_FireUltraTipText          = "UIs/Battle/FireUltraTipText";                            /// 发动大招提示面板

        public const string PanelName_DamageItemLeft            = "UIs/Battle/DamageLeftItem";                              /// 伤害左
        public const string PanelName_DamageItemRight           = "UIs/Battle/DamageRightItem";                             /// 伤害右
        public const string PanelName_DamageDate                = "UIs/Battle/DamageDataPanel";                             /// 伤害数据

        public const string PanelName_BattleDataSet             = "UIs/Battle/BattleDataPanel";                             /// 战斗数据设置面板
        public const string PanelName_BattleDataItem            = "UIs/Battle/BattleDataItem";                              /// 战斗数据项
        public const string PanelName_RoleUltraFired            = "UIs/Battle/RoleUltraFired";                              /// 主角大招发动

        public const string PanelName_RoleShowUI                = "UIs/Battle/RoleShowUI";                                  /// 首次战斗 主角Show

        public const string PanelName_ScreenZoomIn              = "UIs/Anim_Effect/CameraAnim/ScreenZoomIn";                /// 镜头拉近
        public const string PanelName_ScreenVibration           = "UIs/Anim_Effect/CameraAnim/ScreenVibration";             /// 震屏效果
        public const string PanelName_StripAnim_E               = "UIs/Anim_Effect/ScreenAnim_AE/StripAnim_AE";             /// 百叶窗效果


        #endregion
        #region================================================||   攻击范围类型 -- AttackRangeName   ||=====================================================
        public const string DefendRandomThree                   = "DefenceRandomThree";                                     /// 分裂攻击(随机攻击守方三个目标) 
        public const string DefendBackRowOne                    = "defencebackrowone";
        #endregion
    }


    ///-------------------------------------------------------------------------------------------------------------------- /// <summary> 颜色亮度管理 </summary>
    public class ColorMangerItem : MonoBehaviour                                                                            
    {
        public static readonly Color    _DefaultColor                   = new Color(150f / 255, 150f / 255, 150f / 255, 1); /// 默认色彩
        public static readonly Color    _IceColor                       = new Color(1, 70f / 255, 70f / 255, 1);            /// 冰系色彩
        public static readonly Color    _FireColor                      = new Color(1, 70f / 255, 70f / 255, 1);            /// 火系色彩
        public static readonly Color    _ThunderColor                   = new Color(1, 70f / 255, 70f / 255, 1);            /// 雷系色彩

        public IBattleMemMediator       Owner;                                                                              //  成员数据
        public Color                    DefaultColor                    = _DefaultColor;                                    /// 默认色彩
        public Color                    GoToColor                       = _DefaultColor;                                    /// 目标色彩
    
        private float                   max                             = 0.3f;    
        private float                   alfa                            = 0;
        private bool                    IsComplete                      = true;
        Color                           FromColor                       = _DefaultColor;    
        public void                     ToBeHitColor (IBattleMemMediator inAttcker)                                         // 击中颜色       
        {
            if ( Owner.Camp == Battle_Camp.Enemy )
            {
                switch (inAttcker.IMemData.MemberPolarity)
                {
                    case 1:             ToBeIce();                      break;                                              // 冰
                    case 2:             ToBeFire();                     break;                                              // 火
                    case 3:             ToBeThunder();                  break;                                              // 雷
                }
            }
        }

        public void                     ToBeIce()                                                                           // 冰             
        {
            if ( GoToColor != _IceColor )
            {
                IsComplete              = true;
                StopAllCoroutines();
                StopCoroutine(ChangeToColor(_IceColor));
            }
        }
        public void                     ToBeFire()                                                                          // 火             
        {
            if ( GoToColor != _FireColor )
            {
                IsComplete              = true;
                StopAllCoroutines();
                StopCoroutine(ChangeToColor(_FireColor));
            }
        }
        public void                     ToBeThunder()                                                                       // 雷             
        {
            if ( GoToColor != _ThunderColor )
            {
                IsComplete              = true;
                StopAllCoroutines();
                StopCoroutine(ChangeToColor(_ThunderColor));
            }
        }

        private IEnumerator             ChangeToColor(Color c)                                                              // 更换颜色        
        {
            IsComplete                  = true;
            alfa                        = 0;
            GoToColor                   = c;

            FromColor                   = GoToColor;
            SetMemModelColor             (Owner.MemAnimEffect.Model, GoToColor);
            yield return new WaitForSeconds(0.4f);

            IsComplete                  = true;
            alfa                        = 0;
            GoToColor                   = DefaultColor;
            IsComplete                  = false;
        }
        private void Update()
        {
            if (!IsComplete)
            {
                alfa                    += Time.deltaTime;
                alfa                    = Mathf.Min(alfa, max);
                Color CurrColor         = (GoToColor - FromColor) * alfa / max + FromColor;
                FromColor               = CurrColor;
                SetMemModelColor        (Owner.MemAnimEffect.Model, FromColor);
                if (alfa >= max)        IsComplete = true;
            }
        }
        public static void              SetMemModelColor( GameObject inModel, Color inColor)                                // 设置成员模型颜色 
        {
            foreach ( var Item in inModel.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                for (int i = 0; i < Item.materials.Length;i++)
                {
                    Item.materials[i].color = inColor;
                }
            }
        }
    }

    public enum BattleState                                                                                                 // 战斗状态          
    {
        Ready                       = 0,                                                                                    /// 准备完毕
        Fighting                    = 1,                                                                                    /// 战斗中
        PreprogressOver             = 2,                                                                                    /// 本波战斗结束,等待死亡动作完成
        ProgressOver                = 3,                                                                                    /// 进度结束
        Battle                      = 4,                                                                                    /// 战斗结束
    }
    public enum BattleMemState                                                                                              // 战斗成员状态       
    {
        Normal                      = 0,                                                                                    /// 正常
        Dead                        = 1,                                                                                    /// 阵亡
        Destroy                     = 2,                                                                                    /// 摧毁
        Over                        = 3,                                                                                    /// 结束
    }
    public enum BattleMemAnimState                                                                                          // 战斗成员动作状态   
    {
        Standby                     = 0,                                                                                    /// 站立 等待
        Running                     = 1,                                                                                    /// 奔跑
        Sprint                      = 2,                                                                                    /// 冲刺
        UltStorage                  = 3,                                                                                    /// 大招蓄力
        UltAttack                   = 4,                                                                                    /// 大招释放
        NormalAttack                = 5,                                                                                    /// 普通攻击
        SkillAttack_1               = 6,                                                                                    /// 技能_1 攻击
        SkillAttack_2               = 7,                                                                                    /// 技能_2 攻击
        NormalHit                   = 8,                                                                                    /// 普通击中
        HardHit                     = 9,                                                                                    /// 重击
        Death                       = 10,                                                                                   /// 死亡
        Repel                       = 11,                                                                                   /// 击退
        Aerial                      = 12,                                                                                   /// 空中
        Win                         = 13,                                                                                   /// 胜利
        Waiting                     = 14,                                                                                   /// 待机,播放待机执行其他任务
        Entrance                    = 15,                                                                                   /// 入场动作
        Talent                      = 16,                                                                                   /// 天赋Buff

    }

    public enum NoScaleTimeState                                                                                            // 未拉伸时间 状态    
    {
        Normal  = 0,                                    // 正常
        NoScale = 1,                                    // 时间   静止中
        Over    = 2                                     // 已完成 静止状态
    }
    public enum AttackTarRanageType                                                                                         // 攻击目标范围类型   
    {

        DefendSingle                = 1,                            /// 守方单人
        DefendFrontRow              = 2,                            /// 守方前排
        DefendBackRow               = 3,                            /// 守方后排
        DefendFrontRow_MidRow       = 4,                            /// 守方前两排
        DefnedBackRow_MidRow        = 5,                            /// 守方后两排

        DefendRandomSingle          = 6,                            /// 守方随机一人
        DefendRandomDouble          = 7,                            /// 守方随机二人
        DefendRandomThird           = 8,                            /// 守方随机三人

        DefendTeam                  = 9,                            /// 守方全体
        DefendHpMin                 = 10,                           /// 守方血量最低
        DefendHpMax                 = 11,                           /// 守方血量最高
        DefendJumpThree             = 12,                           /// 守方弹射三次
        DefendJumpFour              = 13,                           /// 守方弹射四次
        DefendAround                = 14,                           /// 守方单体周围

        Owner                       = 15,                           /// 本体    
        AttackHpMin                 = 16,                           /// 攻方血量最低
        AttackTeam                  = 17,                           /// 攻方全体   
    }
}