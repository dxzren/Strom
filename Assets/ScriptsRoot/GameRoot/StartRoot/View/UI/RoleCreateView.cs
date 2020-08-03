using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
/// <summary>   角色创建视图   </summary>

public class RoleCreateView : EventView
{
    public string                       IceMasterClick_Event        = "IceMasterClick_Event";
    public string                       DwarfKingClick_Event        = "DwarfKingClick_Event";
    public string                       ArrowQueenClick_Event       = "ArrowQueenClick_Event";
    public string                       GameEnterClick_Event        = "GameEnterClick_Event";
    public string                       RandomNameClick_Event       = "RandomNameClick_Event";
    public string                       RoleHide_Event              = "RoleHide_Event";
    public string                       CheckNickName_Event         = "CheckNickName_Event";

    public TextureNumberShow            IceMasterNum, DwarfKingNum, ArrowQueenNum;                                  /// 冰法师,矮人国王,射手女王 
    public Camera                       TheCamera;                                                                  /// 摄像器
    public UILabel                      HeroInfoShow;                                                               /// 英雄介绍文本信息
    public UISprite                     TalentIcon;                                                                 /// 天赋图标
    public UIInput                      PlayerName;                                                                 /// 玩家名称

    public List<int>                    ShowHeroIDList              = new List<int>();                              /// 模型ID列表
    public List<Transform>              ModelPosList;                                                               /// 模型位置列表
    public List<Transform>              ModelEffectPosList;                                                         /// 模型特效位置列表
    public List<Vector3>                ModelStartPosList           = new List<Vector3>();                          /// 模型初始位置列表
    public List<Vector3>                ModelEffectSatrtPosList     = new List<Vector3>();                          /// 模型特效初始坐标

    public List<RoleAnimPlay>           ModelAnimPlay               = new List<RoleAnimPlay>();                     /// 模型动画列表
    public List<TweenPosition>          ViewShowList                = new List<TweenPosition>();                    /// 界面初始化动画显示

    public List<GameObject>             ParticalPosList             = new List<GameObject>();                       /// 特效粒子位置列表

    public GameObject                   IceMasterObjBtn,    DwarfKingObjBtn,    ArrowQueenObjBtn;                   /// 冰女法师,矮人国王,射手女王
    public GameObject                   IceMasterShow,      DwarfKingShow,      ArrowQueenShow;
    public GameObject                   IceMasterHide,      DwarfKingHide,      ArrowQueenHide;
    public GameObject                   IceMasterName,      DwarfKingName,      ArrowQueenName;
    public GameObject                   FirstSkill,         SecondSkill,        ThirdSkill,         FourthSkill;
    public GameObject                   GameEnterBtn,       RandomNameBtn,      ModelPos,Effect;

    new private void Awake()
    {
        for ( int i = 0; i < ViewShowList.Count; i++ )                                                              /// 初始化动画
        {   ViewShowList[i].enabled                                 = false;                        }

        for ( int i = 0; i < ModelPosList.Count; i++ )                                                              /// 初始化模型位置
        {   ModelPosList[i].localPosition                           = ModelStartPosList[i];         }

        for ( int i = 0; i < ModelEffectPosList.Count; i++)                                                         /// 初始化模型特效位置
        {   ModelEffectPosList[i].localPosition                     = ModelEffectSatrtPosList[i];   }
    }

    public void Init()
    {
        UIEventListener.Get ( IceMasterObjBtn ).onClick             = IceMasterClick;           /// 冰女法师点击
        UIEventListener.Get ( DwarfKingObjBtn ).onClick             = DwarfKingClick;           /// 矮人国王点击
        UIEventListener.Get ( ArrowQueenObjBtn ).onClick            = ArrowQueenClick;          /// 射手女王点击
        UIEventListener.Get ( GameEnterBtn ).onClick                = GameEnterClick;           /// 进入游戏点击
        UIEventListener.Get ( RandomNameBtn ).onClick               = RandomNameClick;          /// 随机名称点击
    }

    public void                         IceMasterClick   ( GameObject obj )                     // 冰女法师点击
    {   dispatcher.Dispatch             ( IceMasterClick_Event );   }
    public void                         DwarfKingClick  ( GameObject obj )                      // 矮人国王点击
    {   dispatcher.Dispatch             ( DwarfKingClick_Event );   }
    public void                         ArrowQueenClick ( GameObject obj )                      // 射手女王点击
    {   dispatcher.Dispatch             ( ArrowQueenClick_Event );  }
    public void                         GameEnterClick  ( GameObject obj )                      // 进入游戏点击
    {   dispatcher.Dispatch             ( GameEnterClick_Event );   }
    public void                         RandomNameClick ( GameObject obj )                      // 随机名称点击
    {   dispatcher.Dispatch             ( RandomNameClick_Event );  }
    public void                         CheckNickName()                                         // 验证昵称
    {   dispatcher.Dispatch             ( CheckNickName_Event );    }
}

