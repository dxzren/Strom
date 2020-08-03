using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace StormBattle
{    
    ///---------------------------------------------------------------------------------------------------------------------/// <summary> 战斗面板管理 </summary>
    public class BattleControll : MonoBehaviour
    {
        public static BattleControll sInstance                      = null;                                                 /// 实例化
        public void Awake()
        {                           sInstance                       = this;     }                 
                                  
        public int                  OurProgress                     { get; set; }                                           /// 我方进度    
        public int                  OurProgressMax                  { get; set; }                                           /// 我方最大进度 
        public int                  EnemyProgress                   { get; set; }                                           /// 敌方进度     
        public int                  EnemyProgressMax                { get; set; }                                           /// 敌方最大进度  
        
        public bool                 IsGradeChallenge                = false;                                                /// 评级挑战 
        public bool                 IsBattlePause                   = false;                                                /// 战斗暂停 

        public MonoBehaviour        TheMono                         = null;                                                 /// unity基类
        public BattleTipsUI         TipsItem                        = null;                                                 /// 提示面板
        public ComboPointUI         ComboPoint_UI                   = null;                                                 /// 连击对象界面
        public BattleMainPanelView  BattleMianPanel                 = null;                                                 /// 战斗主界面

        public GameObject           PanelLoading                    = null;                                                 /// 加载面板
        public GameObject           Root3DMainObj                   = null;                                                 /// 3D对象 根
        public GameObject           RootMemberListObj               = null;                                                 /// 成员列表 根
        public GameObject           RootModelListObj                = null;                                                 /// 模型列表 根
        public GameObject           RootEffectListObj               = null;                                                 /// 特效列表 根
        public GameObject           SceneRootObj                    = null;                                                 /// 场景对象 根
        public GameObject           SceneSetObj                     = null;                                                 /// 场景设置对象
        public GameObject           Blind                           = null;                                                 /// 百叶窗界面显示动画(对象)

        public TweenPosition        ZoomInTween;                                                                            /// 镜头拉近
        public TweenPosition        VibrationTween;                                                                         /// 震屏
        public TimeSpan             BattleDuration                  = TimeSpan.Zero;                                        /// 战斗时长
        public SceneNodesCollect    SceneNodesCollect               = new SceneNodesCollect();                              /// 场景节点集合

        public RealTimer            RealTimerRunning;
        public BattleTeam           OurTeam                         = new BattleTeam(Battle_Camp.Our);                      /// 我方 Team
        public BattleTeam           EnemyTeam                       = new BattleTeam(Battle_Camp.Enemy);                    /// 敌方 Team

        public BattleType           BattleType                      = BattleType.CheckPoint;                                /// 战斗类型
        public BattleState          BattleState                     = BattleState.Ready;                                    /// 战斗状态

        public Camera               UICamera                        = null;                                                 /// UI摄像机
        public Camera               GreyCamera                      { get; private set; }                                   /// 灰色摄像机
        public Camera               EffectCamera                    { get; private set; }                                   /// 特效摄像机

        public Camera               MainCamera                                                                              // 主摄像机         
        {
            get { return _MainCamera; }
            set
            {
                _MainCamera                                         = value;                                        
                _MainCamera.cullingMask = (1 << BattleParmConfig.LayerDefault) | (1 << BattleUtil.LightMapLayer);                           /// 添加场景层，场景中使用灯光贴图，靠场景成过滤

                GameObject TheGrayCamera                            = MonoBehaviour.Instantiate(_MainCamera.gameObject) as GameObject;      /// 实例化对象
                TheGrayCamera.name                                  = "GrayCamera";                                                         /// 对象名称
                TheGrayCamera.transform.parent                      = BattleControll.sInstance.MainCamera.transform;                        /// 指定父级
                TheGrayCamera.transform.localPosition               = Vector3.zero;                                                         /// 设置位置坐标
                TheGrayCamera.transform.localRotation               = Quaternion.identity;                                                  /// 设置旋转角度
                TheGrayCamera.transform.localScale                  = Vector3.one;                                                          /// 设置缩放比例
                foreach ( var Item in TheGrayCamera.GetComponentsInChildren<Transform>(true))                                               /// 检索子级对象_摧毁其他Object   
                {
                    if ( Item != TheGrayCamera.transform )              GameObject.Destroy(Item.gameObject);
                }

                GameObject TheEffectCamera                          = MonoBehaviour.Instantiate(_MainCamera.gameObject) as GameObject;      /// 实例化对象
                TheEffectCamera.name                                = "EffectCamera";                                                       /// 对象名称
                TheEffectCamera.transform.parent                    = BattleControll.sInstance.MainCamera.transform;                        /// 指定父级
                TheEffectCamera.transform.localPosition             = Vector3.zero;                                                         /// 设置位置坐标
                TheEffectCamera.transform.localRotation             = Quaternion.identity;                                                  /// 设置旋转角度
                TheEffectCamera.transform.localScale                = Vector3.one;                                                          /// 设置缩放比例

                GreyCamera                                          = TheGrayCamera.GetComponent<Camera>();                                 /// 获取摄像机组件
                GreyCamera.clearFlags                               = CameraClearFlags.Depth;                                               /// 清理标志
                GreyCamera.cullingMask                              = BattleParmConfig.LayerPublicHitEffect;                                /// 消失遮罩<灯光贴图渲染>层级
                GreyCamera.depth                                    = BattleParmConfig.CameraGreyDept;                                      /// 设置摄像机深度

                EffectCamera                                        = TheEffectCamera.GetComponent<Camera>();                               /// 获取摄像机组件
                EffectCamera.clearFlags                             = CameraClearFlags.Depth;                                               /// 清理标志
                EffectCamera.cullingMask                            = (1 << BattleParmConfig.LayerGrey + 1);                                /// 消失遮罩<灯光贴图渲染>

                _MainCamera.SetCameraAspect();                                                                                              /// 调整摄像器画面比例到16：9 
                GreyCamera.SetCameraAspect();                                                                                               /// 调整摄像器画面比例到16：9 
                EffectCamera.SetCameraAspect();                                                                                             /// 调整摄像器画面比例到16：9  
            }
        }
        public float                CurrentGameSpeed                                                                        // 当前游戏速度      
        {
            set
            {
                if ( value != BattleParmConfig.TimeScaleZero)       _CurrentGameSpeed = value;
                Time.timeScale                                      = value;
            }
            get                                                     { return _CurrentGameSpeed; }
        }
        public void                 CombineTiemsAdd     ()                                                                  // 连击数增加( 显示) 
        {
            ComboPoint_UI.CombineNum++;
        }
        public void                 CameraZoomInMember  (IBattleMemMediator inMemMed,float inTimer)                         // 镜头拉近,移动     
        {
            if (_IsZoomIn)          return;
            GameObject              TheZoomInCamera                 = BattleControll.sInstance.MainCamera.gameObject;
            TweenPosition           TheZoomInFunc                   = BattleControll.sInstance.ZoomInTween;

            float                   FormViewField                   = BattleControll.sInstance.MainCamera.fieldOfView;
            float                   ToViewField                     = FormViewField * 0.45f;

            Time.timeScale                                          = BattleParmConfig.TimeScaleZoom;
            //BattleEffect TheEffect = ResourceKit
    }
        public void                 CameraVibration     ()                                                                  // 震屏动画          
        {
            if (_IsVibration || _IsZoomIn)                          return;
            _IsVibration                                            = true;

            GameObject              VibrateCamera                   = sInstance.MainCamera.gameObject;                      /// 震屏摄像机对象
            TweenPosition           VibrateTweenPos                 = sInstance.VibrationTween;                             /// 震屏位移
            Vector3                 CameraLocalPos                  = VibrateCamera.transform.localPosition;                /// 摄像机局部位置坐标

            TweenPosition           TheTweenPos                     = VibrateCamera.GetComponent<TweenPosition>();          /// 位移动画实例
            BattleParmConfig.OverTween(TheTweenPos);                                                                        /// 动画结束处理

            TheTweenPos.animationCurve                              = VibrateTweenPos.animationCurve;                       /// 动画曲线
            TheTweenPos.ignoreTimeScale                             = false;                                                /// 忽略缩放
            TheTweenPos.style                                       = UITweener.Style.Once;                                 /// 单次播放
            TheTweenPos.from                                        = VibrateCamera.transform.localPosition;                /// 起始坐标
            TheTweenPos.to                                          = new Vector3(CameraLocalPos.x, CameraLocalPos.y - 0.3f, CameraLocalPos.z);     /// 目的坐标
            TheTweenPos.duration                                    = VibrateTweenPos.duration;                             /// 持续时间

            TheTweenPos.onFinished.Clear();                                                                                 /// 完成后清理
            TheTweenPos.onFinished.Add                              ( new EventDelegate( delegate()
            {
                VibrateCamera.transform.localPosition               = CameraLocalPos;                                       /// 设置摄像机位置
                _IsVibration                                        = false;                                                /// 震屏设置 false
                TheTweenPos.enabled                                 = false;                                                /// 位移动画关闭
                Destroy(TheTweenPos);                                                                                       /// 摧毁 Tween
            }));
        }

        public static void          ShowBattleDialog(List<int> inDialogList,Action inCallback,IPlayer inPlayer,int inDex = 0)// 显示战斗对话      
        {
            if(inDialogList.Count > inDex)
            {
                int                 DialogID                        = inDialogList[inDex];
                if (DialogID > 0)
                {
                    Configs_DialogueData    TheDialog_C             = Configs_Dialogue.sInstance.GetDialogueDataByDialogueID(DialogID);
                    if (TheDialog_C != null)
                    {
                        BattleDialogUI      TheDialogUI             = null;                                                                                                 //
                        Configs_HeroData    TheHero_C               = null;                                                                                                 //

                        if (TheDialog_C.Speaker == 0) TheHero_C     = Configs_Hero.sInstance.GetHeroDataByHeroID(inPlayer.PlayerRoleHero.ID);                               //
                        else                TheHero_C               = TheDialog_C.Speaker == 888 ?                                                                          //
                                                                    Configs_Hero.sInstance.GetHeroDataByHeroID(Util.GetPrivateHeroID(inPlayer.PlayerRoleHero.ID)):
                                                                    Configs_Hero.sInstance.GetHeroDataByHeroID(TheDialog_C.Speaker);                                    
                        if      (TheDialog_C.DialogueType == 1)                                                                                                              // 人物在左
                        {   TheDialogUI     = PanelManager.sInstance.ShowPanel(SceneType.Battle, BattleResStrName.PanelName_DialogLeft).GetComponent<BattleDialogUI>(); }

                        else if (TheDialog_C.DialogueType == 2)                                                                                                              // 人物在右
                        {   TheDialogUI     = PanelManager.sInstance.ShowPanel(SceneType.Battle, BattleResStrName.PanelName_DialogRiget).GetComponent<BattleDialogUI>(); }
                        else                Debug.LogError("DialogType Error ! Tpye:" + TheDialog_C.DialogueType);

                        if (TheDialogUI != null && TheHero_C != null)
                        {
                            TheDialogUI.ShowDialog(TheHero_C, TheDialog_C.Dialogue, inPlayer.PlayerName, () => ShowBattleDialog(inDialogList, inCallback, inPlayer, ++inDex));
                            return;
                        }
                    }
                }
                inCallback();
            }
        }
        

        #region================================================||   Private -- 私有模块< 函数_声明 >      ||<FourNode>================================================
        private const float         ZoomInRate                      = 0.5f;                                                 /// 镜头伸缩率
        private const float         ZoomInMemHeight                 = 1;                                                    /// 镜头拉近 高度

        private float               _CurrentGameSpeed               = BattleParmConfig.TimeScaleNormal;                     /// 当前游戏速度
        private bool                _IsZoomIn                       = false;                                                /// 镜头拉近
        private bool                _IsVibration                    = false;                                                /// 摄像机 震屏
        private Camera              _MainCamera                     = null;                                                 /// 主摄像机

        #endregion
    }


}

