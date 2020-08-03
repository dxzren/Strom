using UnityEngine;
using System.Collections;
///-------------------------------------------------------------------------------------------------------------------------/// <summary>   UI 特效管理     </summary>
public class                        UIEffectManager
{
    private static UIEffectManager  _sInstance;
    public  static UIEffectManager  sInstance 
    {   get
        {
            if (_sInstance == null )        _sInstance                  = new UIEffectManager();
            return                          _sInstance;
        }
    }

    public static string            theEffectPath_Part                  = "Prefabs/UIEffect/";                              /// 粒子特效路径
    public static string            theEffectPath_Part_Role             = "Prefabs/RoleEffct/";                             /// 角色粒子特效 路径
    public static string            theEffectPath_Sequence              = "Texture/UISequence/";                            /// 动画序列帧 路径
    
    public GameObject               ShowUIEffect    ( GameObject target, string effectName, Vector3 pos, float destroyTime )                // Target:特效层级位于target之上 DestroyTime: 0：不删除 
    {
        GameObject                  TheObj                          = Util.Load ( theEffectPath_Part + effectName )      as GameObject;
        if ( TheObj == null )       TheObj                          = Util.Load ( theEffectPath_Part_Role + effectName ) as GameObject;

        if ( TheObj != null )
        {
            GameObject              TempObj                         = MonoBehaviour.Instantiate(TheObj) as GameObject;
            Vector3                 TempScale                       = TheObj.transform.localScale;
            TempObj.SetActive       (false);
            if ( target != null )
            {
                UIEffectRenderQueue TempEffeRenderQ                 = TempObj.AddComponent<UIEffectRenderQueue>();
                TempEffeRenderQ.Target                              = target;
                TempObj.transform.parent                            = target.transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            else
            {
                TempObj.transform.parent                            = GameObject.Find ( "UI Root").transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            if ( destroyTime > 0 )                                  MonoBehaviour.Destroy( TempObj, destroyTime );
            return TempObj;
        }
        else
        {
            Debug.Log               ( "未找到粒子UI特效" + effectName );
            return                  null;
        }
    }
    public void                     ShowUIEffect    ( GameObject target, string effectName, Vector3 pos, float destroyTime, bool front )    // Target:特效层级位于target之上 DestroyTime: 0：不删除 
    {
        GameObject                  TheObj                          = Util.Load ( theEffectPath_Part + effectName )      as GameObject;
        if ( TheObj == null )       TheObj                          = Util.Load ( theEffectPath_Part_Role + effectName ) as GameObject;

        if ( TheObj != null )
        {
            GameObject              TempObj                         = MonoBehaviour.Instantiate(TheObj) as GameObject;
            Vector3                 TempScale                       = TheObj.transform.localScale;
            TempObj.SetActive       (false);
            if ( target != null )
            {
                UIEffectRenderQueue TempEffeRenderQ                 = TempObj.AddComponent<UIEffectRenderQueue>();
                TempEffeRenderQ.Target                              = target;
                TempEffeRenderQ.atFront                             = front;
                TempObj.transform.parent                            = target.transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            else
            {
                UIEffectRenderQueue TempEffeRenderQ                 = TempObj.AddComponent<UIEffectRenderQueue>();
                TempEffeRenderQ.atFront                             = front;
                TempObj.transform.parent                            = GameObject.Find("UI Root").transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            if ( destroyTime > 0 )                                  MonoBehaviour.Destroy ( TempObj,destroyTime );
        }
        else
        {
            Debug.Log ("未找到 UI粒子特效" + effectName );
            return ;
        }
    }
    public void                     ShowUIEffect_2  ( GameObject target, string effectName, Vector3 pos, float destoryTime )                // Target:特效层级位于target之上 DestroyTime: 0：不删除 
    {
        GameObject                  TheObj                          = Util.Load( theEffectPath_Part + effectName )     as GameObject;
        if ( TheObj == null )       TheObj                          = Util.Load( theEffectPath_Part_Role + effectName) as GameObject;
        
        if ( TheObj != null )
        {
            GameObject              TempObj                         = MonoBehaviour.Instantiate(TheObj) as GameObject;
            Vector3                 TempScale                       = TheObj.transform.localScale;
            if ( target != null )
            {
                UIEffectRenderQueue TempEffectRenderQ               = TempObj.AddComponent<UIEffectRenderQueue>();
                TempEffectRenderQ.Target                            = target;
                TempObj.transform.parent                            = target.transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            else
            {
                TempObj.transform.parent                            = GameObject.Find ("UI Root").transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            if ( destoryTime > 0 )                                  MonoBehaviour.Destroy ( TempObj,destoryTime );
        }
        else
        {
            Debug.Log ("未找到UI粒子特效" + effectName );
            return;
        }
    }
    public void                     ShowUIEffect_2  ( GameObject target, string effectName, Vector3 pos, float destoryTime, bool front )    // Target:特效层级位于target之上 DestroyTime: 0：不删除 
    {
        GameObject                  TheObj                          = Util.Load( theEffectPath_Part + effectName )      as GameObject;
        if ( TheObj == null )       TheObj                          = Util.Load( theEffectPath_Part_Role + effectName ) as GameObject;        

        if ( TheObj != null )
        {
            GameObject              TempObj                         = MonoBehaviour.Instantiate(TheObj) as GameObject;
            Vector3                 TempScale                       = TheObj.transform.localScale;
            TempObj.SetActive       ( false );
            if ( target != null )
            {
                UIEffectRenderQueue TheEffeRenderQ                  = TempObj.AddComponent<UIEffectRenderQueue>();
                TheEffeRenderQ.Target                               = target;
                TheEffeRenderQ.atFront                              = front;
                TempObj.transform.parent                            = target.transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true ); 
            }
            else
            {
                UIEffectRenderQueue TheEffeRenderQ                  = TempObj.AddComponent<UIEffectRenderQueue>();
                TheEffeRenderQ.atFront                              = front;
                TempObj.transform.parent                            = GameObject.Find("UI Root").transform;
                TempObj.transform.localPosition                     = pos;
                TempObj.transform.localScale                        = TempScale;
                TempObj.SetActive                                   ( true );
            }
            if ( destoryTime > 0 )                                  MonoBehaviour.Destroy ( TempObj, destoryTime );
        }
        else
        {
            Debug.Log("未找到UI粒子特效" + effectName );
            return;
        }
    }

}
