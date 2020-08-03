using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LinqTools;
/// <summary>   模型UI粒子队列    </summary>

public class ModelUIPartQueue : MonoBehaviour
{
    public List<Renderer>       ThePartList                 = new List<Renderer>();             /// 粒子列表
    public bool                 atFront                     = true;                             /// true:特效RenderQueue在Targ之上  false: Targ之下
    private int                 theNum                      = 1;

    private void Start()                    
    {
        Init();
        Invoke("ggg", 0.1f);
    }
    private void                Init()      
    {
        if ( atFront )
        {    theNum             =  1;   }
        else
        {    theNum             = -1;   }
        ThePartList.AddRange    (gameObject.GetComponentsInChildren<Renderer>().ToList());
        if ( ThePartList.Count > 0 )
        {
            for ( int i = 0;    i < ThePartList.Count;i++ )
            {     ThePartList[i].gameObject.SetActive ( false );    }

            Invoke              ( "Next",0.05f );
        }
    }
    private void                Next()      
    {
        if ( ThePartList.Count > 0 )
        {
            for ( int i = 0; i < ThePartList.Count; i++ )
            {     ThePartList[i].gameObject.SetActive ( true );     }    
        }
    }
}
