using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
/// <summary>   角色创建视图  </summary>
public class RoleCreateViewMediator : EventMediator
{
    [Inject]
    public RoleCreateView           View                            { set; get; }
    [Inject]
    public IStartData               startData                       { set; get; }
    [Inject]
    public IPlayer                  playerData                      { set; get; }
    [Inject]
    public IHeroData                heroData                        { set; get;}

    public  HeroData                CurrentHero;                                                                    /// 当前英雄

    private bool                    canCreateRole                   = false;                                        /// 限制创建一次角色
    private GameObject              CreateModelObj                  = null;                                         /// 创建模型对象
    private GameObject              Scene                           = null;                                         /// 场景
    private GameObject              SceneSetting                    = null;                                         /// 场景设置

    public override void            OnRegister()        
    {
        StartCoroutine              ( PlayStartAnimation());                                                        /// 协调程序   ( 播放3D角色动画 )
        RenderSet();                                                                                                /// 渲染设置
        LoadSceneType();                                                                                            /// 加载场景类型
        CurrentHero                 = new HeroData();
        View.Init();
        RandomNameClick_Handler();                                                                                  /// 随机角色名称

        View.dispatcher.AddListener ( View.IceMasterClick_Event,    IceMasterClick_Handler );                       /// 监听 冰女法师    点击
        View.dispatcher.AddListener ( View.DwarfKingClick_Event,    DwarfKingClick_Handler );                       /// 监听 矮人国王    点击
        View.dispatcher.AddListener ( View.ArrowQueenClick_Event,   ArrowQueenClick_Handler );                      /// 监听 射手女王    点击
        View.dispatcher.AddListener ( View.RandomNameClick_Event,   RandomNameClick_Handler );                      /// 监听 随机名称    点击
        View.dispatcher.AddListener ( View.GameEnterClick_Event,    GameEnterClick_Handler );                       /// 监听 进入游戏    点击
        View.dispatcher.AddListener ( View.CheckNickName_Event,     CheckNickName_Handler );                        /// 监听 验证随机名  点击

        Invoke                      ( "CameraInit",0.3f );                                                          /// 初始化摄影机
        NewCreateModelList();                                                                                       /// 新创建模型列表
        PanelManager.sInstance.LoadOverHandler_10Planel ( gameObject.name);                                         /// lv10.面板动画
    }
    public override void            OnRemove()          
    {
        View.dispatcher.RemoveListener ( View.IceMasterClick_Event,    IceMasterClick_Handler );                    /// 监听 冰女法师    点击
        View.dispatcher.RemoveListener ( View.DwarfKingClick_Event,    DwarfKingClick_Handler );                    /// 监听 矮人国王    点击
        View.dispatcher.RemoveListener ( View.ArrowQueenClick_Event,   ArrowQueenClick_Handler );                   /// 监听 射手女王    点击
        View.dispatcher.RemoveListener ( View.RandomNameClick_Event,   RandomNameClick_Handler );                   /// 监听 随机名称    点击
        View.dispatcher.RemoveListener ( View.GameEnterClick_Event,    GameEnterClick_Handler );                    /// 监听 进入游戏    点击
        View.dispatcher.RemoveListener ( View.CheckNickName_Event,     CheckNickName_Handler );                     /// 监听 验证随机名  点击
        Destroy                     ( Scene );                                                                      /// 销毁场景
        Destroy                     ( SceneSetting );                                                               /// 销毁场景设置
    }

    IEnumerator                     PlayStartAnimation()                                                            // 播放角色开场动画     
    {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        Handheld.PlayFullScreenMovie("RoleShow.mp4", Color.black, FullScreenMovieControlMode.Hidden);
#endif
        yield return 0;
    }
    private void                    RenderSet()                                                                     // 渲染环境设置         
    {
        RenderSettings.ambientLight = new Color(70f / 255f, 73f / 255f, 84f / 255f, 1f);                            // 环境光颜色
    }
    public void                     LoadSceneType()                                                                 // 加载场景             
    {
        UnityEngine.Object TheSceneObj                                      = Util.Load ("Prefabs/BattleScene/normalscene_choose" );
        UnityEngine.Object TheScnenSetObj                                   = Util.Load ("Prefabs/BattleScene/normalscene_choose_setting");

        Scene                                                               = Instantiate( TheSceneObj ) as GameObject;
        Scene.name                                                          = "normalscene_choose";
        Scene.transform.localPosition                                       = new Vector3 ( 0f, 50f, 0f );

        SceneSetting                                                        = Instantiate( TheScnenSetObj ) as GameObject;
        if ( View.TheCamera == null )
        {
            View.TheCamera                                                  = SceneSetting.GetComponentInChildren<Camera>();
            View.TheCamera.gameObject.isStatic                              = false;
            View.TheCamera.SetCameraAspect();
        }
        SceneSetting.name                                                   = "normalscene_choose_setting";
        SceneSetting.transform.localPosition                                = new Vector3 (0f, 50f, 0f);
        Util.LoadSceneEffect ("Prefabs/BattleScene/normalscene_choose",SceneSetting ).transform.localPosition = new Vector3 (0f,50f,0f);


    }
    public void                     GameEnterClick_Handler()                                                        // 进入游戏点击         
    {
        if ( canCreateRole == false )                               return;
        if ( startData.createTimes == 0 )
        {
            startData.createTimes                                   = 1;
            string                  TheName                         = View.PlayerName.value;
            playerData.PlayerName                                   = TheName;

            if ( View.PlayerName.value.Length < 2 || View.PlayerName.value.Length > 7 )
            {
                PanelManager.sInstance.ShowNoticePanel              ("昵称2-7个字符");
                startData.createTimes                               = 0;
                return;
            }
            if ( View.PlayerName.value.Length < 1 || View.PlayerName.value == "请输入昵称" )
            {
                PanelManager.sInstance.ShowNoticePanel              ( Language.GetValue ( "FeedbackMsg004" ));
                startData.createTimes                               = 0;
                return;
            }
            if ( Util.IsMaskString( View.PlayerName.value ))                                                        /// 是否屏蔽字
            {
                PanelManager.sInstance.ShowNoticePanel              ( Language.GetValue ( "FeedbackMsg006"));
                startData.createTimes                               = 0;
                return;
            }
            startData.tempHeroID                                    = CurrentHero.ID;                               /// 当前角色ID
            dispatcher.Dispatch                                     ( StartEvent.REQCheckCreateRole_Event );        /// 验证创建角色请求
        }
    }
    public void                     IceMasterClick_Handler()                                                        // 冰女法师 点击执行    
    {
        if ( isPlayBack )                                           return;                                         /// 后跳展示是否激活
        if ( CurrentHero.ID == 100001 )                             return;                                         /// 验证ID
        bool isStop                                                 = false;                                        /// 是否停止
        CurrentHero.ID                                              = 100001;                                       /// 冰女法师ID
        if ( !CanSelectHeroBtn )                                                                                    /// 英雄按钮是否激活    
        {
            if ( frontShowHeroID != CurrentHero.ID )
            {
                Stop();
                isStop = true;
            }
        }
        Configs_ActionAndEffectData TheActEffeD                     = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID
                                                                      ( Configs_Hero.sInstance.GetHeroDataByHeroID(CurrentHero.ID).Resource);
        if ( isStop == false )                                      ShowHero();                                     /// 展示英雄
        ButtonSet (3);                                                                                              /// 英雄点击 设置
        SetName   (3);                                                                                              /// 英雄名称 设置
        View.HeroInfoShow.text                                      = Language.GetValue ( Configs_Hero.sInstance.GetHeroDataByHeroID(100001).HeroDes );
        ////-----------|  添加冰女法师 四个技能图标  |---------------------------------------------------------------------------||
        //View.FirstSkill .transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_huihuangguanghuan_84";
        //View.SecondSkill.transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_bingfengjinzhi_84";
        //View.ThirdSkill .transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_bingshuangxinxing_84";
        //View.FourthSkill.transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_jihanzhinu_84";

        ////-----------|  添加技能动作点击 监听 < SecondSkill:(技能2) >,  < ThirdSkill:(技能3) >, < FourthSkill:(大招技能)  |-----||
        //UIEventListener.Get ( View.SecondSkill ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima ( CreateModelObj,TheActEffeD.ActiveSkillAction1);                                    /// 单次播发 技能动作 
        //    StartCoroutine ( PlayNorSkillorFreeAct( CreateModelObj, TheActEffeD, Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction1 ),
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2,  TheActEffeD.StorageUltimateAttackAction));
        //};
        //UIEventListener.Get ( View.ThirdSkill  ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction2);
        //    StartCoroutine ( PlayNorSkillorFreeAct ( CreateModelObj, TheActEffeD, Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction1 ),
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2,  TheActEffeD.StorageUltimateAttackAction));
        //};
        //UIEventListener.Get ( View.FourthSkill ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima( CreateModelObj, TheActEffeD.StorageUltimateAttackAction );
        //    StartCoroutine ( PlayUltSkillorFreeAct ( CreateModelObj, TheActEffeD, new List<string>
        //                     { TheActEffeD.StorageUltimateAttackAction,TheActEffeD.AggressUltimateAttackAction1 },
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2, TheActEffeD.StorageUltimateAttackAction));
        //};

        canCreateRole = true;
    }
    public void                     ArrowQueenClick_Handler()                                                       // 射手女王 点击执行    
    {
        if ( isPlayBack )                                           return;                                         /// 后跳展示是否激活
        if ( CurrentHero.ID == 100002 )                             return;                                         /// 验证ID
        bool isStop                                                 = false;                                        /// 是否停止
        CurrentHero.ID                                              = 100002;                                       /// 射手女王ID
        if ( !CanSelectHeroBtn )                                                                                    /// 英雄按钮是否激活 
        {
            if ( frontShowHeroID != CurrentHero.ID )
            {
                Stop();
                isStop = true;
            }
        }
        Configs_ActionAndEffectData TheActEffeD                     = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID
                                                                      ( Configs_Hero.sInstance.GetHeroDataByHeroID(CurrentHero.ID).Resource);
        if ( isStop == false )                                      ShowHero();                                     /// 展示英雄
        ButtonSet (2);                                                                                              /// 英雄点击 设置
        SetName   (2);                                                                                              /// 英雄名称 设置
        View.HeroInfoShow.text                                      = Language.GetValue ( Configs_Hero.sInstance.GetHeroDataByHeroID(100002).HeroDes );
        ////-----------|  添加射手女王 四个技能图标  |---------------------------------------------------------------------------||
        //View.FirstSkill .transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_sheshoutianfu_84";
        //View.SecondSkill.transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_hanbingjian_84";
        //View.ThirdSkill .transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_jinmolingyu_84";
        //View.FourthSkill.transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_jinglingzhinu_84";

        ////-----------|  添加技能动作点击 监听 < SecondSkill:(技能2) >,  < ThirdSkill:(技能3) >, < FourthSkill:(大招技能)  |-----||
        //UIEventListener.Get ( View.SecondSkill ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima ( CreateModelObj,TheActEffeD.ActiveSkillAction1);                                    /// 单次播发 技能动作 
        //    StartCoroutine ( PlayNorSkillorFreeAct( CreateModelObj, TheActEffeD, Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction1 ),
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2,  TheActEffeD.StorageUltimateAttackAction));
        //};
        //UIEventListener.Get ( View.ThirdSkill  ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction2);
        //    StartCoroutine ( PlayNorSkillorFreeAct ( CreateModelObj, TheActEffeD, Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction1 ),
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2,  TheActEffeD.StorageUltimateAttackAction));
        //};
        //UIEventListener.Get ( View.FourthSkill ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima( CreateModelObj, TheActEffeD.StorageUltimateAttackAction );
        //    StartCoroutine ( PlayUltSkillorFreeAct ( CreateModelObj, TheActEffeD, new List<string>
        //                     { TheActEffeD.StorageUltimateAttackAction,TheActEffeD.AggressUltimateAttackAction1 },
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2, TheActEffeD.StorageUltimateAttackAction));
        //};

        canCreateRole = true;
    }
    public void                     DwarfKingClick_Handler()                                                        // 矮人国王 点击执行    
    {
        Debug.Log("DwarfKingShow!");
        if ( isPlayBack )                                           return;                                         /// 后跳展示是否激活
        if ( CurrentHero.ID == 100003 )                             return;                                         /// 验证ID
        bool isStop                                                 = false;                                        /// 是否停止
        ButtonSet(1);                                                                                               /// 英雄点击 设置
        CurrentHero.ID                                              = 100003;                                       /// 矮人国王ID

        if ( !CanSelectHeroBtn)                                                                                     /// 英雄按钮是否激活    
        {
            if ( CurrentHero.ID != frontShowHeroID )
            {
                Stop();
                isStop = true;
            }
        }
        SetName(1);                                                                                                 /// 英雄名称 设置
        View.HeroInfoShow.text                                      = Language.GetValue(Configs_Hero.sInstance.GetHeroDataByHeroID(100003).HeroDes);
        Configs_ActionAndEffectData TheActEffeD                     = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID
                                                                      ( Configs_Hero.sInstance.GetHeroDataByHeroID(CurrentHero.ID).Resource );
        if ( isStop == false )                                      ShowHero();                                     /// 展示英雄

        //-----------|  添加矮人国王 四个技能图标  |---------------------------------------------------------------------------||
        //View.FirstSkill .transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_jingdianchang_84";
        //View.SecondSkill.transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_honglei_84";
        //View.ThirdSkill .transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_leishen_84";
        //View.FourthSkill.transform.GetChild(0).GetComponent<UISprite>().spriteName      = "skill_airenzhinu_84";

        //-----------|  添加技能动作点击 监听 < SecondSkill:(技能2) >,  < ThirdSkill:(技能3) >, < FourthSkill:(大招技能)  |-----||
        //UIEventListener.Get ( View.SecondSkill ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima ( CreateModelObj,TheActEffeD.ActiveSkillAction1);                                    /// 单次播发 技能动作 
        //    StartCoroutine ( PlayNorSkillorFreeAct( CreateModelObj, TheActEffeD, Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction1 ),
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2,  TheActEffeD.StorageUltimateAttackAction));
        //};
        //UIEventListener.Get ( View.ThirdSkill  ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction2);
        //    StartCoroutine ( PlayNorSkillorFreeAct ( CreateModelObj, TheActEffeD, Util.PlayOnceAnima( CreateModelObj, TheActEffeD.ActiveSkillAction1 ),
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2,  TheActEffeD.StorageUltimateAttackAction));
        //};
        //UIEventListener.Get ( View.FourthSkill ).onClick = ( GameObject obj ) =>        
        //{
        //    Util.PlayOnceAnima( CreateModelObj, TheActEffeD.StorageUltimateAttackAction );
        //    StartCoroutine ( PlayUltSkillorFreeAct ( CreateModelObj, TheActEffeD, new List<string>
        //                     { TheActEffeD.StorageUltimateAttackAction,TheActEffeD.AggressUltimateAttackAction1 },
        //                     TheActEffeD.ActiveSkillAction1, TheActEffeD.ActiveSkillAction2, TheActEffeD.StorageUltimateAttackAction));
        //};

        canCreateRole = true;
    }
    public void                     RandomNameClick_Handler()                                                       // 随机名称按钮         
    {
        dispatcher.AddListener      (StartEvent.GetNickNameEventCallBack_Event, ChangeName);
        dispatcher.Dispatch         (StartEvent.GetNickName_Event);
    }
    public void                     ChangeName()                                                                    // 更换名称             
    {
        dispatcher.RemoveListener   (StartEvent.GetNickNameEventCallBack_Event, ChangeName);
        View.PlayerName.value         = startData.randomName;
    }
    public void                     CheckNickName_Handler()                                                         // 验证昵称             
    {
        List<char>                  NameCharList                    = new List<char>();
        char[]                      NameChars                       = new char[7];

        foreach ( char key in View.PlayerName.value )
        {
            System.Text.RegularExpressions.Regex rex                = new System.Text.RegularExpressions.
                                                                          Regex (@"^[a-zA-Z0-9\u4e00-\u9fa5]+$");
            if ( rex.IsMatch ( key.ToString()))
            {
                NameCharList.Add(key);
                NameCharList.CopyTo(NameChars);
            }
        }
        View.PlayerName.value = new string ( NameChars );
    }

    #region================================================||  Camera(摄影机)设置   ||=============================================================

    string                          CameraBegainPath                = "Prefabs/SelectAnimation/roleCameraBegain";   /// 摄影机 角色开始路径
    string                          CameraJumpPath                  = "Prefabs/SelectAnimation/roleCameraJump";     /// 摄影机 角色跳跃路径
    string                          CameraNormalScene               = "Prefabs/SelectAnimation/normalscene_choose_Camera";  /// 普通场景选择路径
                                                                      
    Animator                        animator                        = null;                                         /// 动画控制组件
    public AnimationClip            CameraBegainClip                = null;                                         /// 开始关键帧动画
    public AnimationClip            CameraJumpClip                  = null;                                         /// 跳跃关键帧动画
    public RuntimeAnimatorController    TheController               = null;                                         /// 动画控制器

    private float                   PlayOnceAnima_Camera ( GameObject cameraObj,int theIn )                         // 播放动画 (非循环)-自动切换 
    {
        if ( TheController == null )
        {
            Object                  TempObj                         = Util.Load ( CameraNormalScene );
            TheController                                           = TempObj as RuntimeAnimatorController;
        }
        GetClip_CameraBegain();                                                                                     /// 获取开始动作关键帧
        GetClip_CameraJump();                                                                                       /// 获取跳跃动作关键帧
        if ( animator == null )     
        {
            animator                                                = cameraObj.GetComponent<Animator>();
            if ( animator == null)
            {
                animator                                            = cameraObj.AddComponent<Animator>();
                animator.runtimeAnimatorController                  = TheController;
            }
        }
        AnimationClip               SwitchCameraClip                = null;                                         /// 开始动作和跳跃动作切换数据
        if      ( theIn == 1 )      
        {
            SwitchCameraClip        = CameraBegainClip;
            animator.SetTrigger     ("CB");
        }
        else if ( theIn == 2 )
        {
            SwitchCameraClip        = CameraJumpClip;
            animator.SetTrigger     ("RC");
        }

        return SwitchCameraClip.length;                                                                             /// 返回动作长度
}
    private float                   PlayCameraBegain     ( GameObject cameraObj )                                   // 播放开始动作         
    {
        return PlayOnceAnima_Camera ( cameraObj , 1 );
    }
    private float                   PlayCameraJump       ( GameObject cameraObj )                                   // 播放跳跃动作         
    {
        return PlayOnceAnima_Camera ( cameraObj , 2 );
    }
    private void                    CameraInit()                                                                    // 摄像机初始化         
    {
        if ( View.TheCamera == null )
        {
            ShowView();
            return;
        }
        float                       TempTime                        = PlayCameraBegain ( View.TheCamera.gameObject);
        Invoke                      ( "ShowView",TempTime );
    }
    private void                    ShowView()                                                                      // 显示界面             
    {
        for (int i = 0; i < View.ViewShowList.Count; i++)
        {
            View.ViewShowList[i].enabled = true;
        }
        Invoke("DwarfKingClick_Handler", 0.5f);
    }
    private void                    GetClip_CameraBegain()                                                          // 获取开始关键帧动画    
    {
        if ( CameraBegainClip != null )                             return ;

        Object                      TempObj                         = Util.Load ( CameraBegainPath );
        CameraBegainClip            = TempObj as AnimationClip; 
    }
    private void                    GetClip_CameraJump()                                                            // 获取跳跃关键帧动画    
    {
        if ( CameraJumpClip != null )                               return  ;

        Object                      TempObj                         = Util.Load ( CameraJumpPath );
        CameraJumpClip              = TempObj as AnimationClip;
    }
    private void                    PlayCamera_H()                                                                  // 摄影机拉近           
    { }
    private void                    PlayCamera_F()                                                                  // 摄影机拉远           
    { }

    #endregion

    #region================================================||  英雄动作特效行为设置  ||=============================================================
    int                             frontShowHeroID                 = 0;                                            // 之前选择的英雄ID
    int                             index                           = 0;                                            // ModelPos 计数
    int                             once                            = 0;                                            // 
    int                             SetButton                       = 0;                                            // 1.矮人 2:射手 3:法师
    float                           tweenTime                       = 0.4f;                                         // NameObj_Alpha变化时间
    bool                            CanSelectHeroBtn                = false;                                        // 选择英雄按钮是否激活  
    bool                            isPlayBack                      = false;                                        // 后跳展示激活

    RoleAnimPlay                    theFrontAnim                    = null;                                         // 之前的动画
    RoleAnimPlay                    theNowAnim                      = null;                                         // 现在的动画

    GameObject                      theFrontHero                    = null;                                         // 之前的英雄对象
    GameObject                      theNowHero                      = null;                                         // 现在的英雄对象
    GameObject                      SetBigObj                       = null;                                         // 设置大的模型对象
    GameObject                      CurrentPart                     = null;                                         // 当前粒子
    GameObject                      CurrentNameObj                  = null;                                         // 当前对象名称

    List <GameObject>               LoadedHeroModelList             = new List<GameObject>();                       // 加载完的角色模型列表
    List <GameObject>               CurrentEffectList               = new List<GameObject>();                       // 当前特效列表
    List <int >                     ShowHeroIDList                  = new List<int>();                              // 展示英雄ID列表

    Dictionary <GameObject, ModelShowHide> ModelShowHideDic         = new Dictionary<GameObject, ModelShowHide>();  // 模型显示隐藏字典 

    public void                     ShowHero()                                                                      // 展示英雄             
    {
        if ( frontShowHeroID == CurrentHero.ID )                    return ;

        CanSelectHeroBtn            = false;
        theFrontHero                = FindHero      ( frontShowHeroID );
        theNowHero                  = FindHero      ( CurrentHero.ID );
        theFrontAnim                = FindHeroAnim  ( frontShowHeroID );
        theNowAnim                  = FindHeroAnim  ( CurrentHero.ID );
        if ( theFrontHero   != null )
        {    PlayBack();            }               
        else
        {    PlayFront();         }                      
    }
    public void                     Stop()                                                                          // 停止播放             
    {
        CancelInvoke ( "PlayFront_2" );
        CancelInvoke ( "PlayFront_3" );
        CancelInvoke ( "PlayBack_2" );

        Configs_HeroData            HeroD                           = Configs_Hero.sInstance.GetHeroDataByHeroID(frontShowHeroID);              /// 英雄数据实例
        Configs_ActionAndEffectData ActEffectD                      = Configs_ActionAndEffect.sInstance.                                        /// 动作特效实例
                                                                      GetActionAndEffectDataByResourceID(HeroD.Resource);               
        float                       TempTime                        = Util.PlayAnima ( theFrontHero,ActEffectD.CreateRoleStandbyAction );       /// 创建角色平台待机
        frontShowHeroID             = 0;
        ClearEffect();                                                                                                                          /// 清空特效         
        ShowHero();                                                                                                                             /// 角色展示
    }
    private void                    PlayFront ()                                                                    // 播放前跳动作         
    {
        PlayCameraJump              ( View.TheCamera.gameObject );
        frontShowHeroID             = CurrentHero.ID;
        theFrontAnim                = theNowAnim;
        theNowAnim                  = null;
        theFrontHero                = theNowHero;
        theNowHero                  = null;

        Configs_HeroData            theHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID(frontShowHeroID);              /// 英雄数据实例
        Configs_ActionAndEffectData theActEffectD                   = Configs_ActionAndEffect.sInstance.                                        /// 动作特效实例
                                                                      GetActionAndEffectDataByResourceID(theHeroD.Resource);
        theFrontAnim.Stop();                                                                                                                    /// 停止动作
        float                       theTime                         = Util.PlayOnceAnima ( theFrontHero,theActEffectD.CreateRoleJumpAction );   /// 前跳动作
        Invoke                      ( "PlayFront_2",theTime );                                                                       
    }
    private void                    PlayFront_2 ()                                                                  // 播放角色霸气特效      
    {
        Configs_HeroData            theHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID(frontShowHeroID);              /// 英雄数据实例
        Configs_ActionAndEffectData theActEffectD                   = Configs_ActionAndEffect.sInstance.                                        /// 动作特效实例
                                                                      GetActionAndEffectDataByResourceID(theHeroD.Resource);
        theFrontAnim.Stop();                                                                                                                    /// 停止动作   
        float                       theTime                         = Util.PlayOnceAnima ( theFrontHero,theActEffectD.CreateArrogantAction);    /// 角色霸气
        CreateEffect ( frontShowHeroID );
        Invoke ("PlayFront_3", theTime );
    }
    private void                    PlayFront_3 ()                                                                  // 展示动作_3段         
    {
        Configs_HeroData            theHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID(frontShowHeroID);              /// 英雄数据实例
        Configs_ActionAndEffectData theActEffectD                   = Configs_ActionAndEffect.sInstance.                                        /// 动作特效实例
                                                                      GetActionAndEffectDataByResourceID(theHeroD.Resource);
        theFrontAnim.Stop();                                                                                                                    /// 停止动作
        float                       theTime                         = Util.PlayAnima ( theFrontHero,theActEffectD.LeisureAction );              /// 创建休闲动作
        CanSelectHeroBtn            = true;                                                                                                     /// 角色选择点击激活
    }
    private void                    PlayBack()                                                                      // 回跳动作             
    {
        isPlayBack                  = true;
        Configs_HeroData            theHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID(frontShowHeroID);              /// 英雄数据实例
        Configs_ActionAndEffectData theActEffectD                   = Configs_ActionAndEffect.sInstance.                                        /// 动作特效实例
                                                                      GetActionAndEffectDataByResourceID(theHeroD.Resource);
        theFrontAnim.Stop();                                                                                                                    /// 停止动作
        float                       theTime                         = Util.PlayOnceAnima ( theFrontHero,theActEffectD.CreateRoleJumpBackAction);/// 创建后跳动作
        Invoke                      ( "PlayBack_2",theTime );
    }
    private void                    PlayBack_2()                                                                    // 后跳动作_2           
    {
        Configs_HeroData            theHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID(frontShowHeroID);              /// 英雄数据实例
        Configs_ActionAndEffectData theActEffectD                   = Configs_ActionAndEffect.sInstance.                                        /// 动作特效实例
                                                                      GetActionAndEffectDataByResourceID(theHeroD.Resource);
        theFrontAnim.Stop();                                                                                                                    /// 停止动作   
        float                       theTime                         = Util.PlayOnceAnima ( theFrontHero,theActEffectD.CreateRoleStandbyAction); /// 创建角色待机动作
        PlayFront();
        isPlayBack                  = false;
    }

    GameObject                      FindHero     ( int heroID )                                                     // 加载完模型列表,查找英雄   
    {
        for ( int i = 0;i < View.ShowHeroIDList.Count; i++ )
        {
            if (View.ShowHeroIDList[i] == heroID)
            {
                Debug.Log("return Model:" + LoadedHeroModelList[i].name);
                return LoadedHeroModelList[i];
            }
        }
        return null;
    }
    RoleAnimPlay                    FindHeroAnim ( int heroID )                                                     // 模型动作列表,查找英雄动作 
    {
        for ( int i = 0; i < View.ShowHeroIDList.Count; i++ )
        {
            if ( View.ShowHeroIDList[i] == heroID )                 return View.ModelAnimPlay[i];
        }
        return null;
    }
    public void                     CreateModel()                                                                   // 创建模型                
    {
        Configs_ActionAndEffectData TheActEffectD                   = Configs_ActionAndEffect.sInstance.GetActionAndEffectDataByResourceID
                                                                      ( Configs_Hero.sInstance.GetHeroDataByHeroID(CurrentHero.ID).Resource);
        if ( CreateModelObj != null )
        {
            GameObject.DestroyImmediate ( CreateModelObj );
            CreateModelObj                                          = null;
        }
        CreateModelObj                                              = Util.LoadRes ( TheActEffectD,280 );
        Util.AsyncLoadModelShadow ( CreateModelObj.transform,TheActEffectD.ResourceID );

        View.ModelPos.transform.localEulerAngles                    = Vector3.zero;
        CreateModelObj.transform.parent                             = View.ModelPos.transform;
        CreateModelObj.transform.SetAsFirstSibling();
        CreateModelObj.transform.localEulerAngles                   = Vector3.zero;
        CreateModelObj.transform.localPosition                      = Vector3.zero;
    }
    public void                     NewCreateModelList()                                                            // 创建角色模型列表         
    {
        if ( LoadedHeroModelList.Count > 0 )
        {
            for ( int i = 0; i < LoadedHeroModelList.Count; i++ )
            {
                if ( LoadedHeroModelList[i] != null )               DestroyImmediate ( LoadedHeroModelList [i]);
            }
            LoadedHeroModelList.Clear();
            Debug.Log("Clean ModelList!");
        }
        for ( int i = 0; i < View.ShowHeroIDList.Count; i++ )
        {
            ShowHeroIDList.Add ( View.ShowHeroIDList[i] );
        }
        if ( !IsInvoking ("NewCreateModelListRun"))
        {
            InvokeRepeating ( "NewCreateModelListRun",0.1f,0.1f );
        }
    }
    public void                     NewCreateModelListRun ()                                                        // 创建模型运行             
    {
        if ( ShowHeroIDList.Count > 0 )
        {
            Debug.Log("NewCreateModelListRun > 0 ");
            int                     TempHeroID                      = ShowHeroIDList[0];
            if ( index < View.ModelPosList.Count )
            {
                Configs_HeroData    TheHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID ( TempHeroID );            /// 英雄数据实例
                Configs_ActionAndEffectData TheActEffectD           = Configs_ActionAndEffect.sInstance.                                    /// 模型特效实例
                                                                      GetActionAndEffectDataByResourceID ( TheHeroD.Resource);              

                GameObject          TempHeroObj                     = Util.LoadRes ( TheActEffectD,300 );                                   /// 加载模型特效资源
                LoadedHeroModelList.Add                             ( TempHeroObj );
                ChangeModelShader                                   ( TempHeroID,TempHeroObj );                                             /// 更改模型Shader
                Util.AsyncLoadModelShadow                           ( TempHeroObj.transform, TheActEffectD.ResourceID );                    /// 异步加载模型阴影
                
                TempHeroObj.transform.parent                        = View.ModelPosList [index];
                TempHeroObj.transform.SetAsFirstSibling();                                                                                  /// 将transform转到开头
                TempHeroObj.transform.localEulerAngles              = Vector3.zero;                                                         /// 
                TempHeroObj.transform.localPosition                 = Vector3.zero;                                                         /// 
                float               theTime                         = Util.PlayAnima ( TempHeroObj, TheActEffectD.CreateRoleStandbyAction );/// 角色平台待机动作
            }
            ShowHeroIDList.Remove   ( TempHeroID );
            index++;
        }
        else
        {
            Debug.Log               ("NewCreateModelListRun < 0 -- NewCreateModelRun()");
            CanSelectHeroBtn        = true;
            CancelInvoke            ("NewCreateModelListRun");
        }
    }
    public void                     ChangeShader ( GameObject modelObj )                                            // 更换Shader( 明暗器 )     
    {
        Renderer[]                  RendererList                    = modelObj.GetComponentsInChildren<Renderer>(); /// 渲染
        foreach ( Renderer key in RendererList )
        {
            Material[]              MateList                        = key.materials;                                /// 材质列表
            for ( int i = 0; i < MateList.Length; i++ )
            {
                MateList[i].shader                                  = Shader.Find ( "Custom/MyShaderSelectChar");   /// shader路径
                MateList[i].SetFloat ( "_Glossiness",30f );                                                         /// 光泽度
                MateList[i].SetColor ( "_AmbientColor", new Color   ( 0f, 0, 0, 1f));                               /// 环境颜色
                MateList[i].SetColor ( "_SpecularColor",new Color   ( 80f/255f,  91f/255f, 101f/255f, 1f));         /// 镜面反射颜色
                MateList[i].SetColor ( "_RimColor",     new Color   ( 56f/255f, 105f/255f, 201f/255f, 1f));         /// 边缘颜色
            }
        }
         
    }
    public void                     ChangeModelShader ( int heroID,GameObject modelObj )                            // 更改模型Shader           
    {
        Renderer[]                  RendererList                    = modelObj.GetComponentsInChildren< Renderer >();
        ModelShowHide               ModelShowHide                   = modelObj.AddComponent < ModelShowHide >();
        ModelShowHide.changRenderList                               = RendererList;
        ModelShowHide.heroID                                        = heroID; 
        if ( !ModelShowHideDic.ContainsKey ( modelObj ) )
        {    ModelShowHideDic.Add ( modelObj,ModelShowHide );   }
    }
    public void                     LoadScene()                                                                     // 加载场景                 
    {
        UnityEngine.Object          TheScene                        = Util.Load ( "Prefabs/BattleScene/normalscene_choose");
        UnityEngine.Object          TheSetting                      = Util.Load ( "Prefabs/BattlesScene/normalscene_choose_setting");

        Scene                                                       = Instantiate(TheScene) as GameObject;          /// 场景实例
        Scene.name                                                  = "normalscene_choose";                         
        Scene.transform.localPosition                               = new Vector3(0, 50f, 0);

        SceneSetting                                                = Instantiate( TheSetting ) as GameObject;      /// 场景设置实例
        SceneSetting.name                                           = "normalscene_choose_setting";
        SceneSetting.transform.localPosition                        = new Vector3 (0, 50f, 0);
        Util.LoadSceneEffect                                        ( "Prefabs/BattleScene/normalscene_choose",SceneSetting ).
                                                                    transform.localPosition = new Vector3 (0,50f,0);
        if (View.TheCamera == null)                                                                                 /// 摄像机设置   
        {
            View.TheCamera                                          = SceneSetting.GetComponent<Camera>();          /// 摄影机
            View.TheCamera.gameObject.isStatic                      = false;                                        /// 给定的对象是否静态
            View.TheCamera.SetCameraAspect();                                                                       /// 设置摄像机侧面
        }

    }

    private IEnumerable             AddEffect    ( GameObject obj,int ID )                                          // 添加特效                 
    {
        Util.ChangeModelShader  ( obj );
        Util.AddModelPartEffect ( obj, ID );
        yield return null;
    }
    private IEnumerable             AddEffectNew ( GameObject obj,int ID,int wingID )                               // 添加新特效               
    {
        ChangeShader ( obj );
        Util.AddModelPartEffect ( obj, ID );
        yield return null;
    }
    private IEnumerator             PlayNorSkillorFreeAct( GameObject obj, Configs_ActionAndEffectData actEffeD,    // 释放普通技能动作后回    播放休闲待机
                                                           float time, string skill1, string skill2, string skill3 )                            
    {
        yield return new WaitForSeconds ( time + 0.03f );
        if ( obj != null )
        {
            Animation               TheAnim                         = obj.GetComponent<Animation>();
            if ( !TheAnim.IsPlaying ( actEffeD.LeisureAction ) && !TheAnim.IsPlaying( skill1 ) && 
                 !TheAnim.IsPlaying ( skill2 ) && !TheAnim.IsPlaying ( skill3 )                    )
            {    Util.PlayAnima ( obj, actEffeD.LeisureAction );                                   }
        }
    }
    private IEnumerator             PlayUltSkillorFreeAct ( GameObject obj, Configs_ActionAndEffectData actEffeD,   // 释放被动技能序列动作后回 播放休闲待机
                                                            List<string> ultList, string skill1, string skill2, string skill3 )                 
    {
        for ( int i = 0; i < ultList.Count; i++ )
        {
            if ( ultList[i] == "" )                                 continue;
            yield return new WaitForSeconds ( Util.PlayOnceAnima ( obj, ultList[i]) + 0.03f );
        }
        if ( obj != null )
        {
            Animation TheAnim = obj.GetComponent<Animation>();
            if ( !TheAnim.IsPlaying ( actEffeD.LeisureAction) && !TheAnim.IsPlaying ( skill1 ) &&
                 !TheAnim.IsPlaying ( skill2 ) && !TheAnim.IsPlaying ( skill3 ))
            {    Util.PlayAnima ( obj, actEffeD.LeisureAction );                                    }
        }
    }
    private void                    AddTweenScale( GameObject obj,Vector3 from,Vector3 to,float dur )               // 添加缩放范围              
    {
        TweenScale                  TheTweenScale                   = obj.GetComponent<TweenScale>();
        if ( TheTweenScale != null )                                Destroy ( TheTweenScale );

        TweenScale                  AddTweenScale                   = obj.AddComponent<TweenScale>();
        AddTweenScale.from                                          = from;                                         /// 原始参数
        AddTweenScale.to                                            = to;                                           /// 目的参数
        AddTweenScale.duration                                      = dur;                                          /// 持续时间
    }   
    private void                    SetSmallModel( GameObject obj )                                                 // 设置小的模型比例          
    {
        AddTweenScale               (obj, obj.transform.localScale, Vector3.one, 0.25f);
    }
    private void                    SetBigModel  ( GameObject obj )                                                 // 设置大的模型比例          
    {
        if ( SetBigObj != null )
        {
            SetSmallModel           ( SetBigObj );
            SetBigObj               = null;
        }
        SetBigObj                   = obj;
        AddTweenScale               ( obj, Vector3.one, new Vector3(1.05f, 1.05f, 1.05f), 0.25f );
    }
    void                            ButtonSet    ( int num )                                                        // 英雄点击设置             
    {
        if      ( num == 1 )                                                                                        /// 展示矮人国王
        {
            Configs_HeroData        TheHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID(100003);
            Configs_PassiveSkillData ThePassSkillD                  = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID( TheHeroD.Talent[0] );

            if ( View.DwarfKingNum != null )
            {
                View.DwarfKingNum.Show((int)ThePassSkillD.BaseValue);
            }

            View.DwarfKingShow.SetActive                            (true);                                         /// 显示矮人
            View.DwarfKingHide.SetActive                            (false);
            View.ArrowQueenShow.SetActive                           (false);                                        /// 隐藏射手
            View.ArrowQueenHide.SetActive                           (true);
            View.IceMasterShow.SetActive                            (false);                                        /// 隐藏冰女
            View.IceMasterHide.SetActive                            (true);

            if ( CurrentPart != null )                              Destroy ( CurrentPart );
            CurrentPart             = UIEffectManager.sInstance.ShowUIEffect (View.ParticalPosList[0],"uieffect_chooserole_thunder",Vector3.zero,0);
            SetBigModel             ( View.DwarfKingObjBtn );
        }
        else if ( num == 2 )                                                                                        /// 展示射手女王
        {
            Configs_HeroData        TheHeroData                     = Configs_Hero.sInstance.GetHeroDataByHeroID (100002);
            Configs_PassiveSkillData ThePassSkillD                  = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID( TheHeroData.Talent[0] );
            if ( View.ArrowQueenNum != null )                       View.ArrowQueenNum.Show((int)ThePassSkillD.BaseValue);

            View.DwarfKingShow.SetActive                            ( false );
            View.DwarfKingHide.SetActive                            ( true  );
            View.ArrowQueenShow.SetActive                           ( true  );
            View.ArrowQueenHide.SetActive                           ( false );
            View.IceMasterShow.SetActive                            ( false );
            View.IceMasterHide.SetActive                            ( true  );

            if ( CurrentPart != null )                              Destroy ( CurrentPart );
            CurrentPart             = UIEffectManager.sInstance.ShowUIEffect ( View.ParticalPosList[1],"uieffect_chooserole_fire",Vector3.zero,0);
            SetBigModel             ( View.ArrowQueenObjBtn );                     
        }
        else if ( num == 3 )                                                                                        /// 展示冰女法师
        {
            Configs_HeroData        TheHeroData                     = Configs_Hero.sInstance.GetHeroDataByHeroID ( 100001 );
            Configs_PassiveSkillData ThePassSkillD                  = Configs_PassiveSkill.sInstance.GetPassiveSkillDataByPassiveSkillID ( TheHeroData.Talent[0] );
            if ( View.IceMasterNum != null )                        View.IceMasterNum.Show ((int)ThePassSkillD.BaseValue);

            View.DwarfKingShow.SetActive                            ( false);
            View.DwarfKingHide.SetActive                            ( true );
            View.ArrowQueenShow.SetActive                           ( false);
            View.ArrowQueenHide.SetActive                           ( true );
            View.IceMasterShow.SetActive                            ( true );
            View.IceMasterHide.SetActive                            ( false);

            if ( CurrentPart != null )                              Destroy ( CurrentPart );
            CurrentPart             = UIEffectManager.sInstance.ShowUIEffect ( View.ParticalPosList[2],"uieffect_chooserole_ice",Vector3.zero,0 );
            SetBigModel             ( View.IceMasterObjBtn );
        }
        ShowNameObj ( num );      
    }
    private void                    ShowNameObj  ( int num)                                                         // 展示对象名称             
    {
        if ( CurrentNameObj != null )
        {
            CurrentNameObj.GetComponent<UIWidget>().alpha           = 0;
            CurrentNameObj.SetActive                                ( false );
            CurrentNameObj                                          = null;
        }
        switch ( num )
        {
            case 1: CurrentNameObj = View.DwarfKingName;            break;
            case 2: CurrentNameObj = View.ArrowQueenName;           break;
            case 3: CurrentNameObj = View.IceMasterName;            break;
        }
        if ( !IsInvoking ("NameObjCheck"))                          InvokeRepeating("NameObjCheck", 0.3f, 0.1f );
        else
        {
            CancelInvoke            ("NameObjCheck");
            InvokeRepeating         ("NameObjCheck", 0.35f,0.1f);
        }
    }
    private void                    NameObjCheck()                                                                  // 校验 展示名称对象        
    {
        if ( animator != null )
        {
            AnimatorStateInfo       AnimaState                      = animator.GetCurrentAnimatorStateInfo(0);
            if ( AnimaState.normalizedTime > 1f && AnimaState.IsName("roleCameraJump"))
            {
                ShowNameObjRun();
                CancelInvoke        ("NameObjCheck");
            }
        }
        else                        CancelInvoke("NameObjCheck");
    }
    private void                    ShowNameObjRun()                                                                // 英雄名称展示运行         
    {
        if (CurrentNameObj != null )
        {
            CurrentNameObj.GetComponent<UIWidget>().alpha           = 0;
            TweenAlpha              TheTweenAlpha                   = CurrentNameObj.GetComponent<TweenAlpha>();
            if ( TheTweenAlpha != null )                            Destroy( TheTweenAlpha );

            TheTweenAlpha                                           = CurrentNameObj.AddComponent<TweenAlpha>();
            TheTweenAlpha.from                                      = 0f;
            TheTweenAlpha.to                                        = 1f;
            TheTweenAlpha.duration                                  = tweenTime;
            CurrentNameObj.SetActive                                (true);
        }
    }
    private void                    SetName      ( int num )                                                        // 英雄名称_展示设置        
    {
        if      ( num == 1 )                                        /// 展示 矮人国王_Name    
        {
            View.DwarfKingName.SetActive        ( true  );
            View.IceMasterName.SetActive        ( false );
            View.ArrowQueenName.SetActive       ( false );
        }
        else if ( num == 2 )                                        /// 展示 射手女王_Name    
        {
            View.DwarfKingName.SetActive        ( false );
            View.IceMasterName.SetActive        ( false );
            View.ArrowQueenName.SetActive       ( true  );
        }
        else if ( num == 3 )                                        /// 展示 冰女法师_Name    
        {
            View.DwarfKingName.SetActive        ( false );
            View.IceMasterName.SetActive        ( true  );
            View.ArrowQueenName.SetActive       ( false );
        }
    }
    public void                     CreateEffect ( Configs_ActionAndEffectData actEffeD )                           // 创建角色特效             
    {
        GameObject                  TheEffect                       = Instantiate ( Util.Load( "Prefabs/RoleEffect/" +
                                                                                    actEffeD.CreateRoleEffect)) as GameObject;
        if ( View.Effect.transform.childCount > 0 )                 DestroyImmediate( View.Effect.transform.GetChild(0).gameObject );
        TheEffect.transform.SetParent                               ( View.Effect.transform );
        TheEffect.transform.localPosition                           = Vector3.zero;
    }
    private void                    CreateEffect ( int heroID )                                                     // 创建出场特效             
    {
        Configs_HeroData            TheHeroD                        = Configs_Hero.sInstance.GetHeroDataByHeroID (heroID);
        Configs_ActionAndEffectData TheActEffectD                   = Configs_ActionAndEffect.sInstance.
                                                                      GetActionAndEffectDataByResourceID(TheHeroD.Resource);
        GameObject                  Effect                          = Instantiate(Util.Load ("Prefabs/RoleEffect/" + 
                                                                      TheActEffectD.CreateRoleEffect)) as GameObject;
        int                         theIndex                        = 0;
        foreach ( GameObject key in ModelShowHideDic.Keys )
        {
            if ( ModelShowHideDic[key].heroID == heroID )           break;
            theIndex++;
        }
        if  ( View.ModelEffectPosList[theIndex].childCount > 0 )
        {     DestroyImmediate      ( View.ModelEffectPosList[theIndex].GetChild(0).gameObject );   }

        Effect.transform.SetParent  ( View.ModelEffectPosList[theIndex] );
        Effect.transform.localPosition                              = Vector3.zero;
        CurrentEffectList.Add       ( Effect );
    }
    private void                    ClearEffect()                                                                   // 清除特效( CurEffectList) 
    {
        if ( CurrentEffectList.Count > 0 )                          
        {
            for ( int i = 0; i < CurrentEffectList.Count; i++ )
            {
                if ( CurrentEffectList[i] != null )
                {    Destroy ( CurrentEffectList[i] );      }
            }
            CurrentEffectList.Clear();
        }
    }
    #endregion
}