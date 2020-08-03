using UnityEngine;
using System.Collections;
/// <summary> 系统喇叭 -- 主界面滚动广播 </summary>
public class SystemHorn : MonoBehaviour
{

}
public class PMDMessage
{
    public int durtime;                                 // 持续时间
    public int times;                                   // 时间
    public byte color;                                  // 颜色
    public string content;                              // 内容
    public PMDMessage(MAIN_PMD_DATA pmd)
    {
        durtime = pmd.nDurtime;
        times = pmd.nTimes;
        color = pmd.nColor;
        content = pmd.sPMDText;
    }
    public PMDMessage(MAIN_PMD_SINGLE pmd)
    {
        durtime = pmd.PMDData.nDurtime;
        times = pmd.PMDData.nTimes;
        color = pmd.PMDData.nColor;
        content = pmd.PMDData.sPMDText;
    }
}
