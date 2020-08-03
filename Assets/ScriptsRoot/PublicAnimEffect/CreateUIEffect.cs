using UnityEngine;
using System.Collections;

public class CreateUIEffect : MonoBehaviour
{
    public float                    destroyTime                     = 0f;                       // 摧毁时间
    public float                    destoryTime_2                   = 3f;                       // 摧毁时间_2
    public float                    delay                           = 0.6f;                     // 延迟
    public float                    effectShowTimeDelay             = 0.02f;                    // 特效展示延迟

    public string                   effectName                      = "";

    public bool                     reaptCreate                     = false;
    public bool                     front                           = true;

    public GameObject               Target;
    public GameObject               PosObj;
    public GameObject               TheObj;
    private Vector3                 Pos;

    private void Start()
    {
        if ( !reaptCreate )
        {
            if ( PosObj != null )
            {
                Pos                 = PosObj.transform.position;
                PosObj.SetActive    ( false );
            }
            TheObj.SetActive        ( false );
            Invoke                  ("CreatePos",effectShowTimeDelay ); 
        }
        else
        {
            destroyTime             = destoryTime_2;
            if ( !IsInvoking        ("CreatePos"))
            {    InvokeRepeating    ( "CreatePos",0.05f,delay);      }
        }
    }
    private void                    CreatePos()
    {
        if ( PosObj != null )
        {
            Pos                     = PosObj.transform.position;
            PosObj.SetActive        (false);
        }
        TheObj.SetActive            (false);
        if ( effectName.CompareTo("") != 0 )
        {
            if ( front )
            {    UIEffectManager.sInstance.ShowUIEffect_2(Target, effectName, Pos, destroyTime ); }
            else
            {    UIEffectManager.sInstance.ShowUIEffect_2(Target, effectName, Pos, destroyTime,front);  }
        }
    }

}
