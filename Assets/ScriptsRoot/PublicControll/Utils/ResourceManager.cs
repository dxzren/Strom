using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ResourceManager
{
    long time = 0;
    private ResourceManager() { }
    private static ResourceManager _Instance = null;
    public  static ResourceManager  Instance    
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = new ResourceManager();
            }
            return _Instance;
        }
    }

    private Dictionary<AssetBundleName, AssetBundle> _AssetBundleMap = new Dictionary<AssetBundleName, AssetBundle>();
    public  Dictionary<AssetBundleName, AssetBundle>  AssetBundleMap { get { return _AssetBundleMap; } }


    public AssetBundleRequest LoadAssetAsync(AssetBundleName bundleName,string assetName)                                   // 根据资源名和包名异步加载资源文件     
    {
        AssetBundleRequest assetReq = null;
        AssetBundle ab = GetAssetBundle(bundleName);

        if(ab != null)
        {
            time = Util.GetNowTime();
            assetReq = ab.LoadAssetAsync(assetName);

            if(assetReq == null)
            {
                foreach(AssetBundle item in _AssetBundleMap.Values)
                {
                    assetReq = item.LoadAssetAsync(assetName);
                    if(assetReq != null)
                    {
                        break;
                    }
                }
            }
            if(assetReq == null)
            {
                Debug.LogError("资源:" + assetName + "未找到!");
            }
        }
        return assetReq;
    }
    public T LoadAsset<T>(AssetBundleName bundleName,string assetName) where T : Object
    {
        T Asset = null;
        AssetBundle ab = GetAssetBundle(bundleName);

        if(ab != null)
        {
            time = Util.GetNowTime();
            Asset = ab.LoadAsset<T>(assetName);

            if(Asset == null)
            {
                foreach(AssetBundle item in _AssetBundleMap.Values)
                {
                    Asset = item.LoadAsset<T>(assetName);
                    if(Asset != null)
                    {
                        break;
                    }
                }
            }
            if(Asset == null)
            {
                Debug.LogError("资源:" + assetName + "未找到!");
            }
        }
        return Asset;
    }
    public AssetBundle GetAssetBundle(AssetBundleName bundleName)                                                           // 获取资源集合包                     
    {
        AssetBundle ab = null;

        if(_AssetBundleMap.ContainsKey(bundleName))
        {
            ab = _AssetBundleMap[bundleName];
        }
        else
        {
            string url = Application.persistentDataPath + "/res/" + bundleName;
            time = Util.GetNowTime();
            ab = AssetBundle.LoadFromFile(url);
            if(ab != null)
            {
                _AssetBundleMap.Add(bundleName, ab);
            }
        }
        return ab;
    }  

    public bool IsBundleContainsAsset(string assetName,AssetBundleName bundleName)                                          // 确认资源包是否存在                 
    {
        AssetBundle ab = GetAssetBundle(bundleName);
        return ab.Contains(assetName);
    }
    public void ReleaseAllAB()                                                                                              // 释放所有资源包                     
    {
        foreach(AssetBundle item in _AssetBundleMap.Values)
        {
            item.Unload(true);
        }
        _AssetBundleMap.Clear();
        System.GC.Collect();
    }
    public void UnLoadAB(AssetBundleName bundleName)                                                                        // 卸载资源包                         
    {
        if(_AssetBundleMap.ContainsKey(bundleName))
        {
            _AssetBundleMap[bundleName].Unload(false);
        }
        _AssetBundleMap.Remove(bundleName);
        System.GC.Collect();
    }
    public void InitAssetBundles()                                                                                          // 初始化资源包                       
    {
        GetAssetBundle(AssetBundleName.commonhero);
        GetAssetBundle(AssetBundleName.config);
        GetAssetBundle(AssetBundleName.effect);
        GetAssetBundle(AssetBundleName.merc);
        GetAssetBundle(AssetBundleName.npc);
        GetAssetBundle(AssetBundleName.wing);
    }
    
                                                              
}