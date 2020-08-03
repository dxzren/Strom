using UnityEngine;
using System.Collections;
/// <summary>   模型显示与隐藏    </summary>

public class ModelShowHide : MonoBehaviour
{
    public int                      heroID                      = 0;
    public Renderer[]               changRenderList;

    private float                   ShowValue                   = 0f;
    private float                   HideValue                   = 1f;
    private Renderer[]              RendererList;
    private Vector3                 StartPos                    = Vector3.zero;
    private Vector3                 OutPos                      = Vector3.zero;
    
    void Start()                                                                                   
    {
        StartPos                    = transform.position;
        OutPos                      = new Vector3 (StartPos.x - 6000f, StartPos.y,StartPos.z );
        GetRender ();
        Invoke                      ("Show",0.3f);
    }
    
    void                            GetRender ()                                                   
    {   RendererList                = gameObject.GetComponentsInChildren<Renderer> ();     }

    public void                     Stop()                                                      // 停止 
    {
        CancelInvoke                ("HideRun");
        CancelInvoke                ("ShowRun");
        transform.position          = StartPos;
    }
    public void                     Show ()                                                     // 展示
    {
        CancelInvoke                ("HideRun");
        CancelInvoke                ("ShowRun");
        ShowValue                   = 0f;
        transform.position          = StartPos;
        InvokeRepeating             ("ShowRun",0f,0.1f );
    }
    private void                    Hide ()                                                     // 隐藏
    {
        ChangeModelShader           ( gameObject );
        CancelInvoke                ( "ShowRun" );
        CancelInvoke                ( "HideRun" );
        HideValue                   = 1f;
        InvokeRepeating             ( "Hiderun",0f,0.1f );
    } 
    public void                     ShowRun()                                                   // 显示运行
    {
        foreach ( Renderer key in RendererList )
        {
            Material[]              ms                              = key.materials;
            for ( int i = 0; i < ms.Length;i++)
            {
                ms[i].SetColor      ("_Color",new Color (1f,1f,1f,ShowValue));
            }
        }
        ShowValue                   = ShowValue + 0.07f;
        if ( ShowValue > 1f )
        {
            ChangeModelShader_Ref   ( gameObject );
            CancelInvoke            ( "ShowRun" );
        }
    }
    private void                    HideRun ()                                                  // 隐藏运行
    {
        foreach ( Renderer key in RendererList )
        {
            Material[]              ms                              = key.materials;
            for ( int i = 0; i < ms.Length; i++ )
            {
                ms[i].SetColor      ("_Color",new Color (1f,1f,1f,HideValue ));
            }
        }
        HideValue = HideValue - 0.03f;
        if ( HideValue < 0f )
        {
            ChangeModelShader_Ref   ( gameObject );
            transform.position      = OutPos;
            CancelInvoke            ("HideRun");
        }
    }
    public void                     ChangeModelShader     ( GameObject model )                  // 更改模型Shader
    {
        foreach ( Renderer key in changRenderList )
        {
            Material[]              ms                              = key.materials;
            for ( int i = 0; i < ms.Length; i++ )
            {
                ms[i].shader        = Shader.Find("Custom/DoubleFace");
                ms[i].SetColor      ("_Color",new Color (1f,1f,1f,1f));
            }
        }
    }
    public void                     ChangeModelShader_Ref ( GameObject model )                  // 更改模型Shader
    {
        foreach ( Renderer key in changRenderList)
        {
            Material[]              ms                              = key.materials;
            for ( int i = 0; i < ms.Length; i++)
            {
                ms[i].shader        = Shader.Find ("Custom/MyShaderSelectChar");
                ms[i].SetFloat      ("_Glossiness",30f);
                ms[i].SetColor      ("_AmbientColor", new Color (0f, 0f, 0f, 1f));
                ms[i].SetColor      ("_SpecularColor",new Color (80f/255f,91f/255f,101f/255f,1f ));
                ms[i].SetColor      ("RimColor",new Color (56f/255f,105f/255f,201f/255f/1f));
            }
        }
    }
}
