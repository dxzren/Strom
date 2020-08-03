using UnityEngine;
using System.Collections;
using System;
using strange.extensions.mediation.impl;

/// <summary> 体力面板 </summary>
public class StaminaTipsPanelView : EventView
{
    [Inject]
    public IPlayer              InPlayer                { set; get; }

    public int                  staminaRecoverTime      = 360;                                  // 体力回复时间
    public UILabel              TipsDes;                                                        // 提升说明

    private int                 theStamina              = -1;                                   // 体力值
    private int                 nextStaminaTime         = -1;                                   // 下点体力时间
    private int                 staminaLimit            = 0;                                    // 体力上限

    private string              nextStaminaStr          = "";                                   // 下点体力恢复
    private string              fullStaminaStr          = "";                                   // 恢复所有体力
    private string              nowTimeStr              = "";                                   // 当前时间
    private string              buyedTimesStr           = "";                                   // 已购买体力次数
    private string              recoverTimeStr          = "";                                   // 回复体力时间

    private DateTime            NowDateTime;                                                    // 当前时间
    private Configs_VIPData     TheVipData;
    
    new private void Start()
    {
        TipsDes.width           = 200;
        if ( !IsInvoking("NowTime"))
        {    InvokeRepeating    ("NowTime",0.02f,1f);       }

        Init();
        staminaRecoverTime      = CustomJsonUtil.GetValueToInt("PhysicalRecouverTime");         // 读取配置中的回复体力CD值
        staminaLimit            = Configs_LeadingUpgrade.sInstance.GetLeadingUpgradeDataByCurrentLevel
                                  (InPlayer.PlayerLevel).PhysicalLimit;
        if ( !IsInvoking("NextStamina"))
        {    InvokeRepeating    ("NextStamina", 0.02f, 1f); }
    }
    private void                Init()                                                          // 体力数据初始化          
    {
        TheVipData              = Configs_VIP.sInstance.GetVIPDataByVIP(InPlayer.PlayerVIPLevel);
        buyedTimesStr           = "已买体力次数:" + InPlayer.BuyedStaminaTimes +"/" + TheVipData.PhysicalPower;
        recoverTimeStr          = "恢复体力时间间隔:" + staminaRecoverTime / 60 + "分钟";
    }
    private void                NowTime()                                                       // 当前时间_文本化         
    {
        string                  hour                    = "";
        string                  minute                  = "";
        string                  second                  = "";
        NowDateTime                                     = DateTime.Now;

        if ( NowDateTime.Hour < 10   )
        {    hour               = "0" + NowDateTime.Hour;     }
        else
        {    hour               = NowDateTime.Hour + "";      }

        if ( NowDateTime.Minute < 10 )
        {    minute             = "0" + NowDateTime.Minute;  }
        else
        {    minute             = NowDateTime.Minute + "";   }

        if ( NowDateTime.Second < 10 )
        {    second             = "0" + NowDateTime.Second;  }
        else
        {    second             = NowDateTime.Second + "";   }

        nowTimeStr              = "当前时间:" + hour + ":" + minute + ":" + second;
    }
    
    private void                NextStamina()                                                   // 下一次体力
    {
        if ( InPlayer.PlayerCurrentStamina < staminaLimit )
        {
            int                     hour, minute, second, hourMax, minuteMax, secondMax, MaxTime;
            string                  hourStr, minuteStr, secondStr, hourMaxStr, minuteMaxStr, secondMaxStr;

            if ( nextStaminaTime == -1 )
            {    nextStaminaTime                            = EditorClose.staminaRaply + 1;      }

            nextStaminaTime--;
            if ( nextStaminaTime <= 0 )
            {
                if ( theStamina != InPlayer.PlayerCurrentStamina )
                {
                    nextStaminaTime                         = staminaRecoverTime;
                    theStamina                              = EditorClose.staminaRaply + 1;
                }
                if ( nextStaminaTime <= 0)
                {    nextStaminaTime                        = 0;                                } 
            }
            hour                    = nextStaminaTime / (60 * 60);
            minute                  = (nextStaminaTime - (hour*60*60))/60;
            second                  = (nextStaminaTime - (hour*60*60)) - (minute*60);
            if ( hour   < 10 )
            {    hourStr            = "0" + hour;       }
            else
            {    hourStr            = hour + "";        }

            if ( minute < 10 )
            {    minuteStr          = "0" + minute;     }
            else
            {    minuteStr          = minute + "";      }

            if ( second < 10 )
            {    secondStr          = "0" + second;     }
            else
            {    secondStr          = second + "";      }
            nextStaminaStr          = "下点体力恢复:" + hourStr + ":" + minuteStr + ":" + secondStr;

            MaxTime                 = ( staminaLimit - InPlayer.PlayerCurrentStamina - 1) * staminaRecoverTime + nextStaminaTime;
            hourMax                 = MaxTime / (60*60);
            minuteMax               = (MaxTime - (hourMax*60*60)) /60;
            secondMax               = (MaxTime - (hourMax*60*60)) - (minuteMax*60);
            if ( hourMax   < 10 )
            {    hourMaxStr         = "0" + hourMax;       }
            else
            {    hourMaxStr         = hourMax + "";        }

            if ( minuteMax < 10 )
            {    minuteMaxStr       = "0" + minuteMax;     }
            else
            {    minuteMaxStr       = minuteMax + "";      }

            if ( secondMax < 10 )
            {    secondMaxStr       = "0" + secondMax;     }
            else
            {    secondMaxStr       = secondMax + "";      }
            fullStaminaStr          = "恢复全部体力:" + hourMaxStr +":" + minuteMaxStr +":" + secondMaxStr;
        }
        else
        {
            nextStaminaStr          = "体力已满";
            fullStaminaStr          = "";
        }
        if ( fullStaminaStr.CompareTo("") == 0 )
        {    TipsDes.text           = nowTimeStr + "\n" + buyedTimesStr + "\n" + nextStaminaStr + "\n" + recoverTimeStr; }
        else
        {   TipsDes.text            = nowTimeStr + "\n" + buyedTimesStr + "\n" + nextStaminaStr + "\n" + fullStaminaStr +"\n" + recoverTimeStr; }
    }
}
