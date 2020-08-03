using UnityEngine;
using System.Collections;

public class EquipMergeObj                                                  // 装备合成对象
{
    public int ID = 0;                                  // 合成对象ID
    public int type = 1;                                // 类型 1:装备 2:碎片
    public int quality = 0;                             // 品质
    public string iconName = "";                        // 图标名称

    public UIAtlas equipBg = null;                      // 装备框
    public UIAtlas fragmentBg = null;                   // 碎片框

    /// <summary> 装备合成对象构造函数 </summary>
    public EquipMergeObj(int id,int type,string iconName,int quality,UIAtlas equipBg,UIAtlas fragmentBg)
    {
        this.ID = id;
        this.type = type;
        this.quality = quality;
        this.iconName = iconName;
        this.equipBg = equipBg;
        this.fragmentBg = fragmentBg;
    }
}