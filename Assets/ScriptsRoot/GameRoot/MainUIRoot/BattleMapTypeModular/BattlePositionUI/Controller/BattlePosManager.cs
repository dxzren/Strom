using UnityEngine;
using System.Collections;
                                                            /// <summary>  英雄阵容设置参数 </summary>
public class BattlePosSet
{
    public IHeroData            BP_HeroData;                // 英雄数据
    public GameObject           UpEffect;                   // 上阵特效
    public GameObject           TableEffect;                // 常驻特效
    public GameObject           HeroContainer;              // 英雄集合

    GameObject                  _HeroModel                  = null;

    public GameObject           HeroModel                   // 英雄模型设置 
    {
        set
        {
            this._HeroModel = value;
            if (_HeroModel == null )
            {
                UpEffect.SetActive      (false);
                TableEffect.SetActive   (false);
            }
            else
            {
                UpEffect.SetActive      (true);
                TableEffect.SetActive   (true);
            }
        }
        get
        { return _HeroModel; }
    }
    
}
public class BattleLinupPOS_Item : MonoBehaviour
{
    public  BattlePosData      LineupPos;
}
public enum BP_HeroItemShowType                                                                 // 英雄预置阵容 英雄图标展示类型
{
    AllHeroList                 = 0,                        // 所有英雄
    FrontHeroList               = 1,                        // 前排英雄
    MiddleHeroList              = 2,                        // 中排英雄
    BackHeroList                = 3                         // 后排英雄
}


