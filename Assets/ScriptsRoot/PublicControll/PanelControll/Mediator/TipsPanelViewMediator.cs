using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class TipsPanelViewMediator : EventMediator
{
    [Inject]
    public TipsPanelView view { set; get; }
    [Inject]
    public IGameData gameData { set; get; }
    [Inject]
    public IPlayer player { set; get; }

    private float edge = 50f;                                                                   // 边缘
    private float distance = 30f;                                                               // 距离

    private bool isShow = false;

    private System.DateTime resumeT = System.DateTime.Now;

    public override void OnRegister()
    {
        view.ItemObj.SetActive(false);                                                          // 物品对象
        view.CurrencyObj.SetActive(false);                                                      // 货币对象
        view.HeroAtlas = gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);                       // 加载英雄图集
        view.PropAtlas = gameData.GetGameAtlas(AtlasConfig.PropIcon70);                         // 加载道具图集
        view.EquipAtlas = gameData.GetGameAtlas(AtlasConfig.EquipIcon84);                       // 加载装备图集
        view.FragAtlas = gameData.GetGameAtlas(AtlasConfig.FragmentIcon);                       // 加载碎片图集

        view.player = player;
        SetPosition();                                                                          // 设置位移坐标

    }
    public override void OnRemove()         
    {

    }
    public void Update()                    
    {
        if(!isShow)                                     // 延迟500毫秒
        {
            isShow = true;
            view.ShowTips();
        }
    }
    public void SetPosition()               
    {
        if(view.boxTips &&(view.type == (int)ItemType.diamonds || view.type == (int)ItemType.coins))                            // 等级宝箱特殊处理
        {
            if((640/2 - edge) >= this.transform.localPosition.y + (view.targHeight/2) + distance + view.BackGround.height)      // 偏上显示     
            {
                float y = this.transform.localPosition.y + (view.targHeight / 2) + distance + view.BackGround.height / 2;
                if(this.transform.localPosition.x >= 0)                                                                     // 偏左显示
                {
                    float x = this.transform.localPosition.x - view.BackGround.width / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
                else                                                                                                        // 偏右显示
                {
                    float x = this.transform.localPosition.x + view.BackGround.width / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
            }
                                                                                                                                // 偏左显示
            else if((1136 / 2 - edge) >= Mathf.Abs(this.transform.localPosition.x - (view.targWidth / 2) - distance - view.BackGround.width))  
            {
                float x = this.transform.localPosition.x - distance - (view.targWidth / 2) - view.BackGround.width / 2;

                if(this.transform.localPosition.y >= 0)                                                                     // 偏下显示
                {
                    float y = this.transform.localPosition.y - view.BackGround.height / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
                else                                                                                                        // 偏上显示
                {
                    float y = this.transform.localPosition.y + view.BackGround.height / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
            }
            else                                                                                                                // 偏右显示     
            {
                float x = this.transform.localPosition.x + distance + (view.targWidth / 2) + view.BackGround.width / 2;
                float y = this.transform.localPosition.y + view.BackGround.height / 1.5f;
                this.transform.localPosition = new Vector3(x, y, 0);
            }
        }
        else
        {
            if ((640 / 2 - edge) >= this.transform.localPosition.y + (view.targHeight / 2) + distance + view.BackGround.height) // 偏上显示
            {
                float y = this.transform.localPosition.y + (view.targHeight / 2) + distance + view.BackGround.height / 2;
                if(this.transform.localPosition.x >= 0)                                                                         // 偏左显示     
                {
                    float x = this.transform.localPosition.x - view.BackGround.width / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
                else                                                                                                            // 偏右显示     
                {
                    float x = this.transform.localPosition.x + view.BackGround.width / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
            }
                                                                                                                                // 偏左显示
            else if ((1136 / 2 - edge) >= Mathf.Abs(this.transform.localPosition.x - (view.targWidth / 2) - distance - view.BackGround.width))  
            {
                float x = this.transform.localPosition.x - distance - (view.targWidth / 2) - distance - view.BackGround.width / 2;
                if(this.transform.localPosition.y >= 0)
                {
                    float y = this.transform.localPosition.y - view.BackGround.height / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
                else
                {
                    float y = this.transform.localPosition.y + view.BackGround.height / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }

            }
            else                                                                                                                // 偏右显示
            {
                float x = this.transform.localPosition.x + distance + (view.targWidth / 2) - distance - view.BackGround.width / 2;
                if(this.transform.localPosition.y >= 0)                                                                         // 偏下显示     
                {
                    float y = this.transform.localPosition.y - view.BackGround.height / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }
                else                                                                                                            // 偏上显示     
                {
                    float y = this.transform.localPosition.y + view.BackGround.height / 2;
                    this.transform.localPosition = new Vector3(x, y, 0);
                }

            }

        }
    }

}