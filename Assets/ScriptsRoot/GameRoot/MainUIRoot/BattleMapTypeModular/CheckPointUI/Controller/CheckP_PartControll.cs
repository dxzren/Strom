using UnityEngine;
using System.Collections;
/// <summary> 关卡粒子特小控制 </summary>
public class CheckP_PartControll : MonoBehaviour
{
    public float                MinPosX                     = 0;
    public float                MaxPosX                     = -2281f;
    public float                SelfMinPosX                 = -554f;
    public float                SelfMaxPosX                 = -506f;
    
    public Transform            TarGetPos;
    public Transform            SelfPos;

    private float               currentTargetX              = -1f;
    private bool                start                       = false;

    private void Start()
    {
        Invoke("StartRun",0.1f);
    }
    private void Update()
    {
        if (start)
        {
            if (currentTargetX != TarGetPos.localPosition.x)
            {
                currentTargetX              = TarGetPos.localPosition.x;
                float percentage            = Mathf.Abs(currentTargetX - MinPosX) / Mathf.Abs(MaxPosX - MinPosX);
                float thePosX               = SelfMaxPosX - percentage * Mathf.Abs(SelfMaxPosX - SelfMinPosX);
                SelfPos.localPosition       = new Vector3(thePosX, SelfPos.localPosition.y, SelfPos.localPosition.z);
            }
        }
    }
    void StartRun ()
    {   start                   = true ;}
   
}
