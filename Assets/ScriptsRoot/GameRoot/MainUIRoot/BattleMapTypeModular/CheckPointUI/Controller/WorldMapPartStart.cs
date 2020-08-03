using UnityEngine;
using System.Collections;
/// <summary> 世界地图粒子特效启动 </summary>

public class WorldMapPartStart : MonoBehaviour
{
    public float                min_x                        = 0;                                                   // 最小x坐标值
    public float                max_x                        = 0;                                                   // 最大x坐标值

    public GameObject           PartObj;                                                                            // 粒子对象
    public Transform            Target_TF;                                                                          // 目标 (_Transform)
    public Transform            Source_TF;                                                                          // 原   (_Transform)

    private float               currentPos_x                = 0;                                                    // 当前x坐标值
    private bool                isStart                     = false;                                                // 是否启动

    private void Start()
    {
        Invoke                  ("StartRun", Random.Range(0.002f, 0.1f));
    }
    private void Update()
    {
        if ( isStart )                                                                          /// 特效刷新跟随目标X轴位置
        {
            if (currentPos_x != Target_TF.localPosition.x )
            {
                currentPos_x = Target_TF.localPosition.x;
                StartPart();
            }
        }
    }
    private void                StartRun()                                                                          // 启动运行         
    {
        currentPos_x            = Target_TF.localPosition.x;
        isStart                 = true;
        StartPart();
    }
    private void                StartPart()                                                                         // 激活特效         
    {
        if ( currentPos_x <= max_x && currentPos_x >= min_x )
        {
            if ( PartObj != null )
            {
                PartObj.SetActive(true);
                isStart         = false;
                Invoke          ("SetTrue",0.1f);
            }
        }
    }
    private void                SetTrue()                                                                           // 激活_特效队列    
    {
        if ( Source_TF.childCount > 0 )
        {
            for (int i = 0; i < Source_TF.childCount;i++ )
            {
                Transform       TheChild_TF                 = Source_TF.GetChild(i);                                /// 子集_TF
                UIEffectRenderQueue TheEffectRQ             = TheChild_TF.GetComponent<UIEffectRenderQueue>();      /// 特效队列
                if ( TheEffectRQ != null)                                                                           /// 特效队列_激活
                {
                    if ( TheEffectRQ.PartList.Count > 0 )
                    {
                        for ( int j = 0; j < TheEffectRQ.PartList.Count; j++ )
                        {       TheEffectRQ.PartList[j].gameObject.SetActive(true);     }
                    }
                }
            }
        }
    }
}
