using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>   挂在粒子特效上,根据atFront 自动设置粒子特效显示在target ( 上 or 下 )  </summary>

public class UIEffectRenderQueue : MonoBehaviour
{
    public GameObject               Target;                                                                         /// 目标对象
    public List<GameObject>         PartList                        = new List<GameObject>();                       /// 粒子列表
    public bool                     atFront                         = true;                                         /// 是否在目标对象之上
    int                             theNum                          = 1;                                            /// 标签

    private void Start()    
    {
        Init();                                                                                                     /// 初始化特效
        Invoke("Run",0.1f);                                                                                         /// 运行 添加所有类型特效 并激活
    }
    public void ReSet()     
    {
        UpSet();                                                                                                    /// 所有特效添加组件 并设置参数
    }
    void                            UpSet()                                                                         // 所有特效添加组件 并设置参数   
    {
        if      ( Target.GetComponent< UISprite  >() != null && Target.GetComponent< UISprite  >().drawCall != null )     
        {
            int                     index                           = Target.GetComponent<UISprite>().drawCall.renderQueue;
            if ( PartList.Count > 0)
            {
                for ( int i = 0; i < PartList.Count; i++ )
                {
                    if ( PartList[i].GetComponent<Renderer>().material != null )
                    {    PartList[i].GetComponent<Renderer>().material.renderQueue = index + theNum;}
                }
            }
        }
        else if ( Target.GetComponent< UITexture >() != null && Target.GetComponent< UITexture >().drawCall != null )     
        {
            int                     index                           = Target.GetComponent<UITexture>().drawCall.renderQueue;
            if ( PartList.Count > 0 )
            {
                for ( int  i = 0; i < PartList.Count; i++ ) 
                {
                    if ( PartList[i].GetComponent<Renderer>().material != null )
                    { PartList[i].GetComponent<Renderer>().material.renderQueue = index + theNum; }
                }
            }
        }
        else if ( Target.GetComponent< Renderer  >() != null )     
        {
            if  ( Target.GetComponent<Renderer>().material != null )        
            {
                int                 index                           = Target.GetComponent<Renderer>().material.renderQueue;
                for ( int i = 0; i < PartList.Count; i++ )          
                {
                    if ( PartList[i].GetComponent<Renderer>().material != null )
                    {    PartList[i].GetComponent<Renderer>().material.renderQueue = index + theNum;    }
                }
            }
        }
    }
    void                            Init ()                                                                         // 初始化特效                   
    {
        if  ( atFront )             theNum                          = 1;
        else                        theNum                          = -1;

        if ( gameObject.transform.GetComponent< Renderer >()                != null )   
        {
            if ( gameObject.transform.GetComponent< Renderer >().material   != null )
            {
                PartList.Add        ( gameObject );
                gameObject.layer                                    = 5;
            }
        }
        for ( int i = 0; i < transform.childCount; i++ )                        
        {
            Transform               chid_01                         = transform.GetChild(i);
            if ( chid_01.GetComponent< Renderer >()                                     != null &&
                 chid_01.GetComponent< Renderer >().material.mainTexture                != null     )
            {    PartList.Add   ( chid_01.gameObject );             }

            for ( int j = 0; j < chid_01.childCount; j++ )
            {
                Transform           chid_02                         = chid_01.GetChild(j);
                if ( chid_02.GetComponent< Renderer >()                                 != null &&
                     chid_02.GetComponent< Renderer >().material.mainTexture            != null     )
                {    PartList.Add   ( chid_02.gameObject );         }

                for ( int k = 0; k < chid_02.childCount; k++ )
                {
                    Transform       chid_03                         = chid_02.GetChild(k);
                    if ( chid_03.GetComponent< Renderer >()                             != null &&
                         chid_03.GetComponent< Renderer >().material.mainTexture        != null     )
                    {    PartList.Add(chid_03.gameObject );         } 

                    for  ( int x = 0; x < chid_03.childCount; x++   )
                    {
                        Transform   chid_04                         = chid_03.GetChild(x);
                        if ( chid_04.GetComponent< Renderer >()                         != null &&
                             chid_04.GetComponent< Renderer >().material.mainTexture    != null     )
                        {    PartList.Add ( chid_04.gameObject );   }
                    }
                }
            }
        }
        if ( PartList.Count > 0 )                                               
        {
            for ( int i = 0; i < PartList.Count; i++ )
            {    PartList[i].SetActive ( false );                   }
        }
    }
    void                            Run  ()                                                                         // 运行 添加所有类型特效 并激活  
    {
        if ( Target != null )
        {
            if      ( Target.GetComponent< UISprite >() != null )        
            {
                if ( Target.GetComponent <UISprite>().enabled && Target.activeSelf && Target.GetComponent<UISprite>().drawCall != null )
                {
                    try
                    {
                        int theIndex = Target.GetComponent<UISprite>().drawCall.renderQueue;
                        if ( PartList.Count > 0 )
                        {
                            for ( int i = 0; i < PartList.Count; i++ )
                            {
                                if ( PartList[i].GetComponent<Renderer>().material != null )
                                {    PartList[i].GetComponent<Renderer>().material.renderQueue = theIndex + theNum; }
                            }
                            Invoke ( "Next", 0.03f );
                        }
                    }
                    catch ( System.Exception e )
                    {
                        Invoke ( "Next", 0.03f );
                        Debug.Log (e);
                    }
                }
            }
            else if ( Target.GetComponent< UITexture>() != null )        
            {
                if  ( Target.GetComponent<UITexture>().enabled && Target.GetComponent<UITexture>().drawCall != null )
                {
                    int             theIndex                        = Target.GetComponent<UITexture>().drawCall.renderQueue;
                    if ( PartList.Count > 0 )
                    {
                        for ( int i = 0; i < PartList.Count; i++ )  
                        {
                            if ( PartList[i].GetComponent<Renderer>().material != null )
                            {    PartList[i].GetComponent<Renderer>().material.renderQueue = theIndex + theNum; }
                        }
                        Invoke ("Next", 0.03f );
                    }
                }
            }
            else if ( Target.GetComponent< Renderer >() != null )        
            {
                if ( Target.GetComponent< Renderer >().material != null )
                {
                    if ( Target.activeSelf )
                    {
                        int theIndex = Target.GetComponent< Renderer >().material.renderQueue;
                        if ( PartList.Count > 0 )
                        {
                            for ( int i = 0; i < PartList.Count; i++ )
                            {
                                if ( PartList[i].GetComponent<Renderer>().material != null )
                                {    PartList[i].GetComponent<Renderer>().material.renderQueue = theIndex + theNum; }
                            }
                            Invoke ( "Next",0.03f );
                        }
                    }
                }
            }
        }
        Invoke ( "Next", 0.03f );
    }
    void                            Next ()                                                                         // 设置特效列表所有项 激活       
    {
        if ( PartList.Count > 0 )
        {
            for ( int i = 0; i < PartList.Count; i++ )
            {    PartList[i].SetActive (true);       }
        }
    }
}
