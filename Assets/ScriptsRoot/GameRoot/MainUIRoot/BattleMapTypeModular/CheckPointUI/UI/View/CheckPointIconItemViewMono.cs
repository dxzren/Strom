using UnityEngine;
using System.Collections;
/// <summary> 关卡_NPC_掉落物品_图标 </summary>

public class CheckPointIconItemViewMono : MonoBehaviour
{
    public int      itemID;
    public ItemType itemTpye;
    public UISprite BG_BossFrame,Boss_Label, BG_Frame, ItemIcon;
    public UISprite Star_1, Star_2, Star_3, Star4, Star_5;

    public Configs_CheckPointData TheCheckP_D = new Configs_CheckPointData();
    public void Awake()
    {
        UIEventListener.Get(this.gameObject).onPress = GetItemInfo;
    }
    public void GetItemInfo(GameObject obj,bool pressd)
    {

        PanelManager.sInstance.ShowTipsPanel(obj, pressd, itemID, itemTpye, TheCheckP_D, true);
    }
}
