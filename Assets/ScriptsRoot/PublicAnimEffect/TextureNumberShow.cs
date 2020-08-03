using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>   图片数字显示  </summary>

public class TextureNumberShow : MonoBehaviour
{
    public int                  currentNum              = 0;                                    /// 当前显示的图片
    public float                distance                = 1f;                                   /// 图片间隔
    public string               frontName               = "";                                   /// 图片数字名字前缀
    public bool                 zeroShow                = true;                                 /// 为0是否显示
    public UISprite             NumSprite;                                                      /// 数字图片初始位置

    List <GameObject>           NumObjList              = new List<GameObject>();               /// 对象列表
    public void Show ( int num )
    {
        float                   distance_x              = distance + NumSprite.width;           /// X轴坐标
        Clear();
        if ( num == 0 )
        {
            if ( zeroShow )
            {    NumSprite.     spriteName              = frontName + ""+0;   } 
            else
            {    NumSprite.     spriteName              = "";                 }
            return;
        }
        else
        {
            string              numStr                  = "" + num;
            GameObject          TempObj                 = Instantiate( NumSprite.gameObject ) as GameObject;
            if ( TempObj.GetComponent <TextureNumberShow> ())                                   /// 如果已经有数字组件 销毁组件  
            {
                 Destroy        ( TempObj.GetComponent  <TextureNumberShow> ());
            }

            NumSprite.spriteName                        = frontName + numStr[0];
            for ( int i = 1; i < numStr.Length; i++ )   
            {
                GameObject      TempObj_2               = Instantiate (TempObj) as GameObject;

                TempObj_2.GetComponent<UISprite>().spriteName       = frontName + numStr[i];
                TempObj_2.transform.parent              = NumSprite.transform;
                TempObj_2.transform.localPosition       = new Vector3(i * distance_x, 0, 0);
                TempObj_2.transform.localEulerAngles    = Vector3.zero;
                TempObj_2.transform.localScale          = Vector3.one;
                NumObjList.Add  ( TempObj_2);     
            }
            Destroy             ( TempObj );
        }
    }
    private void                Clear()                                                         // 清理图片列表
    {
        if ( NumObjList.Count > 0 )
        {
            for (int i = 0; i < NumObjList.Count; i++ )
            {
                if ( NumObjList[i] != null)             Destroy (NumObjList[i]);
            }
            NumObjList.Clear();
        }
    }
}
