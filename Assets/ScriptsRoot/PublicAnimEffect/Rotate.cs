using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public int                      steps                           = 0;                        // 旋转360 需要拖动步数,0:代表自由旋转
    public float                    speed                           = 0.03f;                    // 旋转速度
    public float                    Resistance                      = 5;                        // 阻力

    public GameObject               ModelParent;                                                // 旋转对象
        
    private float                   pos_a;                                                      //
    private float                   pos_b;                                                      //
    private bool                    onceDrag                        = false;                    // 单次拖动
    private Vector3                 SourcePos                       = new Vector3();

    private void Start()
    {
        InvokeRepeating             ( "UpdatePos",0,0.2f );
    }
    private void Update()           
    {
        if ( steps == 0 )
        {
            if      ( pos_b > 0 )
            {
                for ( int i = 0; i < ModelParent.transform.childCount; i++ )
                {    ModelParent.transform.GetChild(i).Rotate ( new Vector3 ( 0, pos_a*speed ));    }
            }
            else if ( pos_b < 0 )
            {
                for ( int i = 0; i < ModelParent.transform.childCount; i++ )
                {    ModelParent.transform.GetChild(i).Rotate ( new Vector3 ( 0, -pos_a*speed));     }
            }

            if ( pos_a > 0 )
            {    pos_a              -= Resistance;   } 
            else
            {
                pos_a               = 0;
                pos_b               = 0;
            }
        }
        else
        {
            if ( onceDrag )
            {
                if ( pos_a > 30 )   
                {
                    onceDrag        = false;
                    if      ( pos_b > 0 )
                    {    ModelParent.transform.GetChild(0).Rotate ( new Vector3 ( 0, (float)360 / steps ));     }
                    else if ( pos_b < 0 )
                    {    ModelParent.transform.GetChild(0).Rotate ( new Vector3 ( 0, (float)360 / steps ));     }    
                }
            }
        }
    }
    private void                    UpdatePos()                                                 // 更新坐标
    {   SourcePos                   = this.gameObject.transform.localPosition;   }
    private void                    OnDrag()                                                    // 拖动
    {
        pos_b                       = SourcePos.x - this.gameObject.transform.localPosition.x;
        pos_a                       = Vector3.Distance ( SourcePos, this.gameObject.transform.localPosition );
    }
    private void                    OnDragEnd()                                                 // 拖动结束
    {
        onceDrag                                                    = false;
        this.gameObject.transform.localPosition                     = Vector3.zero;
    }
}
