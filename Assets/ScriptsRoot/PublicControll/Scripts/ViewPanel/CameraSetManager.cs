using UnityEngine;
using System.Collections;

public class CameraSetManager
{
    static CameraSetManager _sInstance;
    public static CameraSetManager sInstance                                                                        
    {
        get
        {
            if (_sInstance == null) _sInstance = new CameraSetManager();
            return _sInstance;
        }
    }

    public void AdaptiveUI()                                                            // UI界面自适应              
    {
        int ManualWidth  = 1136;
        int ManualHeight = 640;
        UIRoot uiRoot = GameObject.FindObjectOfType<UIRoot>();

        if (uiRoot != null)
        {
            if(System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
            {
                uiRoot.manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
            }
            else
            {
                uiRoot.manualHeight = ManualHeight;
            }
        }
    }

    public void CamInit(Camera cam)                                                     // 3D摄像机自适应            
    {
        int ManualWidth  = 1136;
        int ManualHeight = 640;
        int theUiRootH = 0;
        Camera theCA = cam;

        if(System.Convert.ToSingle(Screen.height)/Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
        {
            theUiRootH = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
        }
        else
        {
            theUiRootH = ManualHeight;
        }

        if(theCA != null)
        {
            float rectHeight;
            if(theUiRootH >= ManualHeight)
            {
                rectHeight = theUiRootH - ManualHeight;
            }
            else
            {
                rectHeight = -theUiRootH + ManualHeight;
            }
            rectHeight = rectHeight / (float)ManualHeight;
            theCA.rect = new Rect(0f, rectHeight / 2f, 1f, (1f - rectHeight));
        }
    }

}