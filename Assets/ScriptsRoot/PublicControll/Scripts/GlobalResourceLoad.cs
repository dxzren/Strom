using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using XLua; 

//[Hotfix]
public class GlobalResourceLoad : MonoBehaviour
{
    public static GameObject ResLoad(string str)
    {
        object obj = Resources.Load(str);
        if (obj!=null)
        {
            return (GameObject)Resources.Load(str);
        }
        return null;
    }

    public static Material ResLoad_Mat(string str)
    {
        object obj = Resources.Load(str);
        if (obj != null)
        {
            return (Material)Resources.Load(str);
        }
        return null;
    }
    //
    public static List<Texture> ResLoadAll_ToTex(string str)
    {
        List<Texture> objs = new List<Texture>();
        Object[] theObjs = Resources.LoadAll(str);
        if (theObjs != null)
        {
            for (int i = 0; i < theObjs.Length; i++)
            {
                objs.Add((Texture)theObjs[i]);
            }
        }
        return objs;
    }

    //
    public static List<GameObject> ResLoadAll(string str)
    {
        List<GameObject> objs = new List<GameObject>();
        Object[] theObjs = Resources.LoadAll(str);
        if (theObjs != null)
        {
            for (int i = 0; i < theObjs.Length; i++)
            {
                objs.Add((GameObject)theObjs[i]);
            }
        }
        return objs;
    }

    public static GameObject[] ResLoadAll_Arry(string str)
    {
        Object[] theObjs = Resources.LoadAll(str);
        GameObject[] theArry = null;
        if (theObjs != null)
        {
            theArry = new GameObject[theObjs.Length];
            for (int i = 0; i < theObjs.Length; i++)
            {
                theArry[i] = (GameObject)theObjs[i];
            }
        }
        return theArry;
    }


    public static AudioClip[] ResLoadAudioClipAll(string str)
    {
        AudioClip[] aucli = null;
        Object[] theObjs = Resources.LoadAll(str);
        if (theObjs.Length > 0)
        {
            aucli = new AudioClip[theObjs.Length];
            for (int i = 0; i < theObjs.Length; i++)
            {
                aucli[i] = theObjs[i] as AudioClip;
            }
        }
        return aucli;
    }

    public static AudioClip ResLoadAudioClip(string str)
    {
        AudioClip aucli = null;
        Object theObjs = Resources.Load(str) as Object;
        if (theObjs != null)
        {
            aucli = theObjs as AudioClip;
        }
        return aucli;
    }
    public static void SetUiItemSprite(ItemAndGui itemAndGui)
    {
        switch (itemAndGui._type)
        {
            case 1://装备
                Dictionary<int, Configs_EquipData> equipData = Configs_Equip.sInstance.mEquipDatas;
                Configs_EquipData equipDat = equipData[itemAndGui._itemData.ItemID];
                //品质框确定
                itemAndGui.Kuang.atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.PropIcon70);
                switch (equipDat.EquipQuality)
                {
                    case 1:
                        itemAndGui.Kuang.spriteName = "daoju-bai-84";
                        break;

                    case 2:
                        itemAndGui.Kuang.spriteName = "daoju-lv-83";
                        break;

                    case 3:
                        itemAndGui.Kuang.spriteName = "daoju-lan-84";
                        break;

                    case 4:
                        itemAndGui.Kuang.spriteName = "daoju-zi-83";
                        break;

                    case 5:
                        itemAndGui.Kuang.spriteName = "daoju-jin-84";
                        break;
                }
                //里框
                itemAndGui.icon.GetComponent<UISprite>().atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.EquipIcon84);
                itemAndGui.icon.GetComponent<UISprite>().spriteName = equipDat.EquipIcon_84;
                itemAndGui.nameLabel.text = Language.GetValue(equipDat.EquipName);//道具名字
                if (itemAndGui._itemData.Num == 1)//数目显示
                {
                    itemAndGui.numLabel.text = "";
                    itemAndGui.numObj.gameObject.SetActive(false);
                }
                else
                {
                    itemAndGui.numLabel.text = itemAndGui._itemData.Num.ToString();
                    itemAndGui.numObj.gameObject.SetActive(true);
                }
                break;
            case 2://装备碎片
                Dictionary<int, Configs_FragmentData> fragmentData = Configs_Fragment.sInstance.mFragmentDatas;
                Configs_FragmentData fragmentDat = fragmentData[itemAndGui._itemData.ItemID];
                //品质框确定
                itemAndGui.Kuang.atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.FragmentIcon);
                switch (fragmentDat.FragmentQuality)
                {
                    case 1:
                        itemAndGui.Kuang.spriteName = "suipian-bai-84";
                        break;

                    case 2:
                        itemAndGui.Kuang.spriteName = "suipian-lv-84";
                        break;

                    case 3:
                        itemAndGui.Kuang.spriteName = "suipian-lan-84";
                        break;

                    case 4:
                        itemAndGui.Kuang.spriteName = "suipian-zi-84";
                        break;

                    case 5:
                        itemAndGui.Kuang.spriteName = "suipian-jin-84";
                        break;
                }
                //里框
                itemAndGui.icon.GetComponent<UISprite>().atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.EquipIcon84);
                itemAndGui.icon.GetComponent<UISprite>().spriteName = fragmentDat.FragmentIcon_84;
                 itemAndGui.nameLabel.text = Language.GetValue(fragmentDat.FragmentName);//道具名字
                 if (itemAndGui._itemData.Num == 1)//数目显示
                {
                    itemAndGui.numLabel.text = "";
                    itemAndGui.numObj.gameObject.SetActive(false);
                }
                else
                {
                    itemAndGui.numLabel.text = itemAndGui._itemData.Num.ToString();
                    itemAndGui.numObj.gameObject.SetActive(true);
                }
                break;
            case 5://魂石=====单独处理，魂石的数目没有用，星级是1的是20000金币，星级非1的是40钻石
                {
                    //品质框固定
                    itemAndGui.Kuang.atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);
                    itemAndGui.Kuang.spriteName = "hunshikuang-84";
                    //图标框
                    itemAndGui.icon.GetComponent<UISprite>().atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.HeroHeadIcon);
                    Dictionary<int, Configs_SoulData> soulData = Configs_Soul.sInstance.mSoulDatas;
                    Configs_SoulData soulDat = soulData[itemAndGui._itemData.ItemID];
                    itemAndGui.icon.GetComponent<UISprite>().spriteName = soulDat.head84;
                    itemAndGui.nameLabel.text = Language.GetValue(soulDat.SoulName);//道具名字
                    itemAndGui.numLabel.text = "";
                    itemAndGui.numObj.gameObject.SetActive(false);
                }
                break;
            case 6:
            case 7://英雄经验道具
            case 8://勋章
            case 9:
            case 10://扫荡
            case 12://体力
                {
                    Dictionary<int, Configs_PropData> propData = Configs_Prop.sInstance.mPropDatas;
                    Configs_PropData propDat = propData[itemAndGui._itemData.ItemID];
                    itemAndGui.Kuang.atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.PropIcon70);
                    switch (propDat.PropQuality)
                    {
                        case 1:
                            itemAndGui.Kuang.spriteName = "daoju-bai-84";
                            break;

                        case 2:
                            itemAndGui.Kuang.spriteName = "daoju-lv-83";
                            break;

                        case 3:
                            itemAndGui.Kuang.spriteName = "daoju-lan-84";
                            break;

                        case 4:
                            itemAndGui.Kuang.spriteName = "daoju-zi-83";
                            break;

                        case 5:
                            itemAndGui.Kuang.spriteName = "daoju-jin-84";
                            break;
                    }
                    itemAndGui.icon.GetComponent<UISprite>().atlas = itemAndGui.gameData.GetGameAtlas(AtlasConfig.PropIcon70);
                    itemAndGui.icon.GetComponent<UISprite>().spriteName = propDat.PropIcon84;

                    itemAndGui.nameLabel.text = Language.GetValue(propDat.PropName);//道具名字
                    if (itemAndGui._itemData.Num == 1)//数目显示
                    {
                        itemAndGui.numLabel.text = "";
                        itemAndGui.numObj.gameObject.SetActive(false);
                    }
                    else
                    {
                        itemAndGui.numLabel.text = itemAndGui._itemData.Num.ToString();
                        itemAndGui.numObj.gameObject.SetActive(true);
                    }
                }
                break;
        }
    }
}
