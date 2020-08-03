using UnityEngine;
using System.Collections;
/// <summary>   角色动作播放    </summary>

public class RoleAnimPlay : MonoBehaviour
{
    GameObject                      Obj_3D                  = null;                             /// 3D 对象
    Configs_ActionAndEffectData     ActEffectD              = null;                             /// 攻击特效

    /// <summary>                   播放定制动画             </summary>
    public void                     Show3DModelAnima        ( GameObject obj,Configs_ActionAndEffectData actEffectD )
    {
        float                       one                     = 0;                                /// 单个动作长度 (秒)
        float                       two                     = 0;                                /// 两个动作长度 (秒)

        Obj_3D = obj;                              
        actEffectD                                          = ActEffectD;                       

        CancelInvoke                ( "PlayAction_2" );                                         /// 大招释放
        CancelInvoke                ( "PlayAction_3" );                                         /// 休闲待机动作
        CancelInvoke                ( "PlayAction_4" );                                         /// 
        CancelInvoke                ( "PlayNormalStandby");                                     /// 普通待机动作
        Animation                   animation               = obj.GetComponent<Animation>();

        if  ( animation[ActEffectD.ShowFreeAction]               != null )                      /// 休闲待机动作      
        {
            Util.PlayOnceAnima      ( obj,actEffectD.ShowFreeAction );                          /// 播放指定模型动画 ( 非循环 )
            one                     = animation[actEffectD.ShowFreeAction].length;              /// 休闲待机动画长度 (  秒)
            Invoke                  ( "PlayAction_3",one );                                     /// 播放休闲待机
            return;
        }

        if  ( animation[actEffectD.StorageUltimateAttackAction ] != null )                      /// 大招(Ult)蓄力动作 
        {
            Util.PlayOnceAnima      ( Obj_3D, ActEffectD.ShowAction );
            one                     = animation[ActEffectD.StorageUltimateAttackAction].length;  
        }
        else    Debug.Log           ( ActEffectD.StorageUltimateAttackAction + "not found !" );

        if  ( animation[ActEffectD.AggressUltimateAttackAction1] != null )                      /// 大招(Ult)攻击动作 
        {
            two                     = one + animation [ActEffectD.AggressUltimateAttackAction1].length;
            Invoke                  ( "Playaction_2",one );
        } 
        else    Debug.Log           ( ActEffectD.AggressUltimateAttackAction1 + "not found !");

        if  ( animation[ActEffectD.StandbyAction] != null )                                     /// 普通待机动作
        {   Invoke                  ( "PlayNormalStandby",two );         }
        else    Debug.Log           ( ActEffectD.StandbyAction + "not found !" );
    }

    public void                     PlayAction_2()                                              // 大招(Ult)攻击动作  
    {        Util.PlayOnceAnima     ( Obj_3D , ActEffectD.AggressUltimateAttackAction1 );    }
    public void                     PlayAction_3()                                              // 休闲待机动作       
    {        Util.PlayOnceAnima     ( Obj_3D , ActEffectD.ShowFreeAction );    }
    public void                     PlayAction_4()                                              // 休闲待机动作       
    {        Util.PlayOnceAnima     ( Obj_3D , ActEffectD.ShowFreeAction );    }
    public void                     PlayNormalStandby()                                         // 普通待机动作       
    {        Util.PlayOnceAnima     ( Obj_3D , ActEffectD.ShowFreeAction );    }

    public void                     Stop()                                                      // 停止播放         
    {
        CancelInvoke ( "PlayAction_2" );
        CancelInvoke ( "PlayAction_3" );
        CancelInvoke ( "PlayAction_4" );
        CancelInvoke ( "PlayNormalStandby");
    }
}
