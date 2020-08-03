using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using XLua; 

//[Hotfix]
//根据物品id  类型设置显示
public class ItemAndGui
{
    public int                  _type;                      // 类型
    public ItemData             _itemData;                  // 物品信息
    public UISprite             Kuang;                      // 物品品质框
    public UISprite             icon;                       // 物品图标
    public UILabel              nameLabel;                  // 名字
    public UILabel              numLabel;                   // 物品数量
    public GameObject           numObj;                     // 物品数量底图
    public IGameData            gameData;                   //
}
public enum TweenType
{
    alpha,
    position,
    color,
    volume,
    rotation,
    scale,
    height,
    width,
}
//[Hotfix]
public class FromToV3
{
    public Vector3 from;
    public Vector3 to;
}
//[Hotfix]
public class GlobalUiEffect : MonoBehaviour
{
    // static Transform  theSelf;
    float delayStart = 0.3f;
    //
    public bool AlphaOpen = false;//true：select_TweenAlpha起作用  为物体添加TweenAlpha 执行
    public List<string> select_TweenAlpha = new List<string>();
    public List<float> startDelay_TweenAlpha = new List<float>();
    //
    public bool PositionOpen = false;
    public List<string> select_TweenPosition = new List<string>();
    public List<float> startDelay_TweenPosition = new List<float>();
    List<FromToV3> FromToV3D = new List<FromToV3>();
    //
    public bool ColorOpen = false;
    public List<string> select_TweenColor = new List<string>();
    public List<float> startDelay_TweenColor = new List<float>();
    //
    public bool VolumeOpen = false;
    public List<string> select_TweenVolume = new List<string>();
    public List<float> startDelay_TweenVolume = new List<float>();
    List<AudioSource> AudioSourceList = new List<AudioSource>();
    //
    public bool RotationOpen = false;
    public List<string> select_TweenRotation = new List<string>();
    public List<float> startDelay_TweenRotation = new List<float>();
    //
    public bool ScaleOpen = false;
    public List<string> select_TweenScale = new List<string>();
    public List<float> startDelay_TweenScale = new List<float>();
    //
    public bool HeightOpen = false;
    public List<string> select_TweenHeight = new List<string>();
    public List<float> startDelay_TweenHeight = new List<float>();
    //
    public bool WidthOpen = false;
    public List<string> select_TweenWidth = new List<string>();
    public List<float> startDelay_TweenWidth = new List<float>();

    //
    public bool WriteEffectOpen = false;
    // public bool WriteOnce = true;//只执行一次打字机效果
    public bool WriteEffectRepet = false;//一旦内容更新，就会执行打字效果
    public string effectName = "";
    float writeEffect = 0.4f;//每隔一定时间检测重新打字
    float writeEffect_x;
    //
    public bool buttonPressUpEffect = false;//
    public Vector3 scale_press = new Vector3(0.9f, 0.9f, 0.9f);
    public Vector3 scale_up = new Vector3(1f, 1f, 1f);
    public float scale_duration = 0.08f;
    //
    public bool buttonPressUpEffect_color = false;
    public Color color_press = new Color(0.1f, 0.1f, 0.1f, 1f);
    public Color color_up = new Color(1f, 1f, 1f, 1f);
    public float color_duration = 0.08f;
    //
    public bool buttonPressUpEffect_alpha = false;
    public float alpha_press = 0.5f;
    public float alpha_up = 1f;
    public float alpha_duration = 0.08f;
    //
    public bool buttonPressUpEffect_rotation = false;
    public Vector3 rotation_press = new Vector3(0f, 0f, 90f);
    public Vector3 rotation_up = new Vector3(0f, 0f, 0f);
    public float rotation_duration = 0.08f;
    //
    public bool buttonPressUpEffect_position = false;
    public Vector3 position_press = new Vector3(0f, 20f, 0f);
    public Vector3 position_up = new Vector3(0f, 0f, 0f);
    public float position_duration = 0.08f;
    //
    public bool buttonPressUpChangeTexture = false;
    public string textureName_press = "按下时图片名字";
    public string textureName_up = "抬起时图片名字";
    public Texture uiTexture_press;
    public Texture uiTexture_up;

    void Awake()
    {
        if (GetComponent<UIPanel>() == null && GetComponent<UISprite>() != null && GetComponent<UITexture>() == null && GetComponent<UIWidget>() == null && GetComponent<UILabel>() != null == null)
        {
            gameObject.AddComponent<UIWidget>();
        }
        writeEffect_x = writeEffect;
    }
    void Start()
    {
        if (select_TweenAlpha.Count == 0)
        {
            string info = "此处填写效果名字 如" + "/n" + "“Resources/TweenPrefabs/Alpha/Alpha_OneToHide”" + "/n" + "填写Alpha_OneToHide" + "/n" + "startDelay_TweenAlpha时对应效果的延迟播放时间";
            select_TweenAlpha.Add(info);
        }
        //if (AlphaOpen)
        //{

        //}
        if (!buttonPressUpEffect && !buttonPressUpEffect_color && !buttonPressUpEffect_alpha && !buttonPressUpEffect_rotation && !buttonPressUpEffect_position && !WriteEffectOpen && !buttonPressUpChangeTexture)
        {
            if (GetComponent<UIPanel>() != null)
            {
                GetComponent<UIPanel>().alpha = 0;
            }
            if (GetComponent<UISprite>() != null)
            {
                GetComponent<UISprite>().alpha = 0;
            }
            if (GetComponent<UILabel>() != null)
            {
                GetComponent<UILabel>().alpha = 0;
            }
            if (GetComponent<UITexture>() != null)
            {
                GetComponent<UITexture>().alpha = 0;
            }
            if (GetComponent<UIWidget>() != null)
            {
                GetComponent<UIWidget>().alpha = 0;
            }
        }
        if (PositionOpen)
        {
            gggg = delayStart;
            if (startDelay_TweenPosition.Count > 0)
            {
                gggg += startDelay_TweenPosition[0] + 0.1f;
            }
        }
        scale_duration_static = scale_duration;
        color_duration_static = color_duration;
        alpha_duration_static = alpha_duration;
        rotation_duration_static = rotation_duration;
        position_duration_static = position_duration;
    }
    float gggg = 0f;
    void Update()
    {
        if (PositionOpen)
        {
            if (gggg > 0f)
            {
                gggg -= Time.deltaTime;
                if (gggg <= 0f)
                {
                    if (!AlphaOpen)
                    {
                        Debug.LogError("GlobalUIEffect gggg = " + gggg);
                        if (GetComponent<UIPanel>() != null)
                        {
                            GetComponent<UIPanel>().alpha = 1f;
                        }
                        if (GetComponent<UISprite>() != null)
                        {
                            GetComponent<UISprite>().alpha = 1f;
                        }
                        if (GetComponent<UILabel>() != null)
                        {
                            GetComponent<UILabel>().alpha = 1f;
                        }
                        if (GetComponent<UITexture>() != null)
                        {
                            GetComponent<UITexture>().alpha = 1f;
                        }
                        if (GetComponent<UIWidget>() != null)
                        {
                            GetComponent<UIWidget>().alpha = 1f;
                        }
                    }
                }
            }
        }
        if (delayStart > 0f)
        {
            delayStart -= Time.deltaTime;
            if (delayStart <= 0f)
            {
                Init();
            }
        }
        // writeEffect
        if (WriteEffectOpen && writeIsEnd)
        {
            if (writeEffect_x > 0f)
            {
                writeEffect_x -= Time.deltaTime;
            }
            else
            {
                writeEffect_x = writeEffect;
                if (GetComponent<UILabel>() != null)
                {
                    if (writeIsEnd && theTextLabel.CompareTo(GetComponent<UILabel>().text) != 0)
                    {
                        StartWrite();
                    }
                }
            }
        }
    }

    void Init()
    {
        if (buttonPressUpEffect || buttonPressUpEffect_color || buttonPressUpEffect_alpha || buttonPressUpEffect_rotation || buttonPressUpEffect_position || buttonPressUpChangeTexture)//
        {
            //Debug.Log("开启按钮按下效果，其他TweenScale禁用");
            if (buttonPressUpEffect)
            {
                ScaleOpen = false;
            }
            if (buttonPressUpEffect_color)
            {
                ColorOpen = false;
            }
            if (buttonPressUpEffect_alpha)
            {
                AlphaOpen = false;
            }
            if (buttonPressUpEffect_rotation)
            {
                RotationOpen = false;
            }
            if (buttonPressUpEffect_position)
            {
                PositionOpen = false;
            }
            if (GetComponent<BoxCollider>() == null)
            {
                gameObject.AddComponent<BoxCollider>();
                Vector2 size = Vector2.zero;
                if (GetComponent<UIWidget>() != null)
                {
                    size = GetComponent<UIWidget>().localSize;
                }
                if (GetComponent<UISprite>() != null)
                {
                    size = GetComponent<UISprite>().localSize;
                }
                if (GetComponent<UILabel>() != null)
                {
                    size = GetComponent<UILabel>().localSize;
                }
                if (GetComponent<UITexture>() != null)
                {
                    size = GetComponent<UITexture>().localSize;
                }
                if (size == Vector2.zero)
                {
                    GetComponent<BoxCollider>().size = new Vector3(86f, 86f, 0f);
                }
                else
                {
                    GetComponent<BoxCollider>().size = new Vector3(size.x, size.y, 0f);
                }

                GetComponent<BoxCollider>().isTrigger = true;
            }
            UIEventListener.Get(gameObject).onPress = PressButton;
        }
        if (AlphaOpen)//添加 TweenAlpha组件
        {
            string path_start = "TweenPrefabs/Alpha/";
            if (select_TweenAlpha.Count > 0)
            {
                for (int i = 0; i < select_TweenAlpha.Count; i++)
                {
                    string theText = select_TweenAlpha[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        //GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenAlpha>() != null)
                            {
                                TweenAlpha thePrefab = obj.GetComponent<TweenAlpha>();
                                TweenAlpha thisObj = gameObject.AddComponent<TweenAlpha>();
                                thisObj.enabled = false;
                                thisObj.from = thePrefab.from;
                                thisObj.to = thePrefab.to;
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenAlpha.Count > i)
                                {
                                    if (startDelay_TweenAlpha[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenAlpha[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                        }
                    }
                }
            }
        }
        //添加 TweenPosition组件
        if (PositionOpen)
        {
            string path_start = "TweenPrefabs/Position/";
            if (select_TweenPosition.Count > 0)
            {
                //FromToV3D
                FromToV3D.Clear();
                for (int i = 0; i < select_TweenPosition.Count; i++)
                {
                    string theText = select_TweenPosition[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            if (obj.GetComponent<TweenPosition>() != null)
                            {
                                TweenPosition thePrefab = obj.GetComponent<TweenPosition>();
                                TweenPosition thisObj = gameObject.AddComponent<TweenPosition>();
                                thisObj.enabled = false;
                                Vector3 fromD = thePrefab.from - thePrefab.transform.localPosition;
                                Vector3 toD = thePrefab.to - thePrefab.transform.localPosition;
                                FromToV3 FromToSave = new FromToV3();
                                FromToSave.from = fromD;
                                FromToSave.to = toD;
                                FromToV3D.Add(FromToSave);
                                thisObj.from = fromD + transform.localPosition;
                                thisObj.to = toD + transform.localPosition;
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenPosition.Count > i)
                                {
                                    if (startDelay_TweenPosition[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenPosition[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////添加 TweenColor组件
        if (ColorOpen)
        {
            string path_start = "TweenPrefabs/Color/";
            if (select_TweenColor.Count > 0)
            {
                for (int i = 0; i < select_TweenColor.Count; i++)
                {
                    string theText = select_TweenColor[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        //  GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenColor>() != null)
                            {
                                TweenColor thePrefab = obj.GetComponent<TweenColor>();
                                TweenColor thisObj = gameObject.AddComponent<TweenColor>();
                                thisObj.enabled = false;
                                thisObj.from = thePrefab.from;
                                thisObj.to = thePrefab.to;
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenColor.Count > i)
                                {
                                    if (startDelay_TweenColor[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenColor[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////添加 TweenVolume组件
        if (VolumeOpen)
        {
            string path_start = "TweenPrefabs/Volume/";
            if (select_TweenVolume.Count > 0)
            {
                AudioSourceList.Clear();
                for (int i = 0; i < select_TweenVolume.Count; i++)
                {
                    string theText = select_TweenVolume[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        // GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenVolume>() != null)
                            {
                                TweenVolume thePrefab = obj.GetComponent<TweenVolume>();
                                if (obj.GetComponent<AudioSource>() != null)
                                {
                                    AudioSource audioSource = obj.GetComponent<AudioSource>();
                                    AudioSourceList.Add(audioSource);
                                }
                                else
                                {
                                    AudioSource audioSource = new AudioSource();
                                    AudioSourceList.Add(audioSource);
                                }
                                TweenVolume thisObj = gameObject.AddComponent<TweenVolume>();
                                if (GetComponent<AudioSource>() != null)
                                {
                                    GetComponent<AudioSource>().enabled = false;
                                }
                                thisObj.enabled = false;
                                thisObj.from = thePrefab.from;
                                thisObj.to = thePrefab.to;
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenColor.Count > i)
                                {
                                    if (startDelay_TweenColor[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenColor[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////添加 TweenRotation组件
        if (RotationOpen)
        {
            string path_start = "TweenPrefabs/Rotation/";
            if (select_TweenRotation.Count > 0)
            {
                for (int i = 0; i < select_TweenRotation.Count; i++)
                {
                    string theText = select_TweenRotation[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        // GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenRotation>() != null)
                            {
                                TweenRotation thePrefab = obj.GetComponent<TweenRotation>();
                                TweenRotation thisObj = gameObject.AddComponent<TweenRotation>();
                                thisObj.enabled = false;
                                Vector3 fromD = thePrefab.from - thePrefab.transform.localEulerAngles;
                                Vector3 toD = thePrefab.to - thePrefab.transform.localEulerAngles;
                                thisObj.from = fromD + transform.localEulerAngles;
                                thisObj.to = toD + transform.localEulerAngles;
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenRotation.Count > i)
                                {
                                    if (startDelay_TweenRotation[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenRotation[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////添加 TweenScale组件
        if (ScaleOpen)
        {
            string path_start = "TweenPrefabs/Scale/";
            if (select_TweenScale.Count > 0)
            {
                for (int i = 0; i < select_TweenScale.Count; i++)
                {
                    string theText = select_TweenScale[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        //  GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenScale>() != null)
                            {
                                TweenScale thePrefab = obj.GetComponent<TweenScale>();
                                TweenScale thisObj = gameObject.AddComponent<TweenScale>();
                                thisObj.enabled = false;
                                Vector3 fromD = new Vector3(thePrefab.from.x / thePrefab.transform.localScale.x, thePrefab.from.y / thePrefab.transform.localScale.y, thePrefab.from.z / thePrefab.transform.localScale.z);
                                Vector3 toD = new Vector3(thePrefab.to.x / thePrefab.transform.localScale.x, thePrefab.to.y / thePrefab.transform.localScale.y, thePrefab.to.z / thePrefab.transform.localScale.z);
                                thisObj.from = new Vector3(fromD.x * transform.localScale.x, fromD.y * transform.localScale.y, fromD.z * transform.localScale.z);
                                thisObj.to = new Vector3(toD.x * transform.localScale.x, toD.y * transform.localScale.y, toD.z * transform.localScale.z);
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenScale.Count > i)
                                {
                                    if (startDelay_TweenScale[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenScale[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////添加 TweenHeight组件
        if (HeightOpen)
        {
            string path_start = "TweenPrefabs/Height/";
            if (select_TweenHeight.Count > 0)
            {
                for (int i = 0; i < select_TweenHeight.Count; i++)
                {
                    string theText = select_TweenHeight[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        // GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenHeight>() != null)
                            {
                                TweenHeight thePrefab = obj.GetComponent<TweenHeight>();
                                TweenHeight thisObj = gameObject.AddComponent<TweenHeight>();
                                thisObj.enabled = false;
                                float fromD = thePrefab.from;
                                float toD = thePrefab.to;
                                if (thePrefab.GetComponent<UISprite>() != null)
                                {
                                    fromD = (float)thePrefab.from / thePrefab.GetComponent<UISprite>().localSize.y;
                                    toD = (float)thePrefab.to / thePrefab.GetComponent<UISprite>().localSize.y;
                                }
                                else
                                {
                                    if (thePrefab.GetComponent<UILabel>() != null)
                                    {
                                        fromD = (float)thePrefab.from / thePrefab.GetComponent<UILabel>().localSize.y;
                                        toD = (float)thePrefab.to / thePrefab.GetComponent<UILabel>().localSize.y;
                                    }
                                    else
                                    {
                                        if (thePrefab.GetComponent<UITexture>() != null)
                                        {
                                            fromD = (float)thePrefab.from / thePrefab.GetComponent<UITexture>().localSize.y;
                                            toD = (float)thePrefab.to / thePrefab.GetComponent<UITexture>().localSize.y;
                                        }
                                    }
                                }
                                if (transform.GetComponent<UISprite>() != null)
                                {
                                    thisObj.from = (int)(fromD * transform.GetComponent<UISprite>().localSize.y);
                                    thisObj.to = (int)(toD * transform.GetComponent<UISprite>().localSize.y);
                                }
                                else
                                {
                                    if (transform.GetComponent<UILabel>() != null)
                                    {
                                        thisObj.from = (int)(fromD * transform.GetComponent<UILabel>().localSize.y);
                                        thisObj.to = (int)(toD * transform.GetComponent<UILabel>().localSize.y);
                                    }
                                    else
                                    {
                                        if (transform.GetComponent<UITexture>() != null)
                                        {
                                            thisObj.from = (int)(fromD * transform.GetComponent<UITexture>().localSize.y);
                                            thisObj.to = (int)(toD * transform.GetComponent<UITexture>().localSize.y);
                                        }
                                        else
                                        {
                                            thisObj.from = thePrefab.from;
                                            thisObj.to = thePrefab.to;
                                        }
                                    }
                                }
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenHeight.Count > i)
                                {
                                    if (startDelay_TweenHeight[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenHeight[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////添加 TweenWidth组件
        if (WidthOpen)
        {
            string path_start = "TweenPrefabs/Width/";
            if (select_TweenWidth.Count > 0)
            {
                for (int i = 0; i < select_TweenWidth.Count; i++)
                {
                    string theText = select_TweenWidth[i];
                    theText.Trim();
                    if (theText.CompareTo("") != 0)
                    {
                        string path = path_start + theText;
                        //  GameObject obj = GlobalResourceLoad.ResLoad(path);
                        GameObject obj = Util.Load(path) as GameObject;
                        if (obj != null)
                        {
                            ///////////////
                            if (obj.GetComponent<TweenWidth>() != null)
                            {
                                TweenWidth thePrefab = obj.GetComponent<TweenWidth>();
                                TweenWidth thisObj = gameObject.AddComponent<TweenWidth>();
                                thisObj.enabled = false;
                                float fromD = thePrefab.from;
                                float toD = thePrefab.to;
                                if (thePrefab.GetComponent<UISprite>() != null)
                                {
                                    fromD = (float)thePrefab.from / thePrefab.GetComponent<UISprite>().localSize.x;
                                    toD = (float)thePrefab.to / thePrefab.GetComponent<UISprite>().localSize.x;
                                }
                                else
                                {
                                    if (thePrefab.GetComponent<UILabel>() != null)
                                    {
                                        fromD = (float)thePrefab.from / thePrefab.GetComponent<UILabel>().localSize.x;
                                        toD = (float)thePrefab.to / thePrefab.GetComponent<UILabel>().localSize.x;
                                    }
                                    else
                                    {
                                        if (thePrefab.GetComponent<UITexture>() != null)
                                        {
                                            fromD = (float)thePrefab.from / thePrefab.GetComponent<UITexture>().localSize.x;
                                            toD = (float)thePrefab.to / thePrefab.GetComponent<UITexture>().localSize.x;
                                        }
                                    }
                                }
                                if (transform.GetComponent<UISprite>() != null)
                                {
                                    thisObj.from = (int)(fromD * transform.GetComponent<UISprite>().localSize.x);
                                    thisObj.to = (int)(toD * transform.GetComponent<UISprite>().localSize.x);
                                }
                                else
                                {
                                    if (transform.GetComponent<UILabel>() != null)
                                    {
                                        thisObj.from = (int)(fromD * transform.GetComponent<UILabel>().localSize.x);
                                        thisObj.to = (int)(toD * transform.GetComponent<UILabel>().localSize.x);
                                    }
                                    else
                                    {
                                        if (transform.GetComponent<UITexture>() != null)
                                        {
                                            thisObj.from = (int)(fromD * transform.GetComponent<UITexture>().localSize.x);
                                            thisObj.to = (int)(toD * transform.GetComponent<UITexture>().localSize.x);
                                        }
                                        else
                                        {
                                            thisObj.from = thePrefab.from;
                                            thisObj.to = thePrefab.to;
                                        }
                                    }
                                }
                                thisObj.animationCurve = thePrefab.animationCurve;//曲线
                                thisObj.style = thePrefab.style;
                                thisObj.duration = thePrefab.duration;
                                if (startDelay_TweenWidth.Count > i)
                                {
                                    if (startDelay_TweenWidth[i] == 0f)
                                    {
                                        thisObj.delay = thePrefab.delay;
                                    }
                                    else
                                    {
                                        thisObj.delay = startDelay_TweenWidth[i];
                                    }
                                }
                                else
                                {
                                    thisObj.delay = thePrefab.delay;
                                }
                                thisObj.tweenGroup = thePrefab.tweenGroup;
                                thisObj.ignoreTimeScale = thePrefab.ignoreTimeScale;
                            }
                            ///////////////
                        }
                    }
                }
            }
        }
        ///***************打字机效果***************///
        if (WriteEffectOpen)
        {
            if (GetComponent<UILabel>() != null)
            {
                if (GetComponent<TypewriterEffect>() != null)
                {
                    GetComponent<TypewriterEffect>().enabled = false;
                    DestroyImmediate(GetComponent<TypewriterEffect>());
                }
                TypewriterEffect typeWrite = gameObject.AddComponent<TypewriterEffect>();
                typeWrite.enabled = false;
                string path_start = "TweenPrefabs/WriteEffect/";
                effectName.Trim();
                if (effectName.CompareTo("") != 0)
                {
                    string path = path_start + effectName;
                    // GameObject obj = GlobalResourceLoad.ResLoad(path);
                    GameObject obj = Util.Load(path) as GameObject;
                    if (obj != null)
                    {
                        theEffect = obj.GetComponent<TypewriterEffect>();
                        if (theEffect != null)
                        {
                            typeWrite.charsPerSecond = theEffect.charsPerSecond;
                            typeWrite.fadeInTime = theEffect.fadeInTime;
                            typeWrite.delayOnPeriod = theEffect.delayOnPeriod;
                            typeWrite.delayOnNewLine = theEffect.delayOnNewLine;
                            typeWrite.scrollView = null;
                            typeWrite.keepFullDimensions = theEffect.keepFullDimensions;
                            typeWrite.onFinished.Clear();
                            if (WriteEffectRepet)
                            {
                                EventDelegate ev = new EventDelegate();
                                ev.Set(this, "WriteRepet");
                                typeWrite.onFinished.Add(ev);
                            }
                            else
                            {
                                EventDelegate ev = new EventDelegate();
                                ev.Set(this, "DestroyWriteEffect");
                                //ev.parameters[0].obj = gameObject;
                                typeWrite.onFinished.Add(ev);
                            }
                            //WriteEffectRepet
                            typeWrite.enabled = true;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("没有找到UILabel组件，打字机效果无效！");
            }
        }
        ///
       // ************************************************//
        StartTween();
    }
    /// <summary>
    /// 打字机效果
    /// </summary>
    bool writeIsEnd = false;
    string theTextLabel = "";
    void DestroyWriteEffect(GameObject obj)
    {

        if (GetComponent<TypewriterEffect>() != null)
        {
            GetComponent<TypewriterEffect>().enabled = false;
            DestroyImmediate(GetComponent<TypewriterEffect>());
        }
    }
    void WriteRepet()
    {
        writeIsEnd = true;
        theTextLabel = GetComponent<UILabel>().text;
        if (GetComponent<TypewriterEffect>() != null)
        {
            GetComponent<TypewriterEffect>().enabled = false;
            DestroyImmediate(GetComponent<TypewriterEffect>());
        }
    }
    TypewriterEffect theEffect;
    void StartWrite()
    {
        writeIsEnd = false;
        if (GetComponent<TypewriterEffect>() != null)
        {
            GetComponent<TypewriterEffect>().enabled = false;
            DestroyImmediate(GetComponent<TypewriterEffect>());
        }
        TypewriterEffect typeWrite = gameObject.AddComponent<TypewriterEffect>();
        typeWrite.enabled = false;
        if (theEffect != null)
        {
            typeWrite.charsPerSecond = theEffect.charsPerSecond;
            typeWrite.fadeInTime = theEffect.fadeInTime;
            typeWrite.delayOnPeriod = theEffect.delayOnPeriod;
            typeWrite.delayOnNewLine = theEffect.delayOnNewLine;
            typeWrite.scrollView = null;
            typeWrite.keepFullDimensions = theEffect.keepFullDimensions;
            typeWrite.onFinished.Clear();
            if (WriteEffectRepet)
            {
                EventDelegate ev = new EventDelegate();
                ev.Set(this, "WriteRepet");
                typeWrite.onFinished.Add(ev);
            }
            else
            {
                EventDelegate ev = new EventDelegate();
                ev.Set(this, "DestroyWriteEffect");
                typeWrite.onFinished.Add(ev);
            }
            //WriteEffectRepet
            typeWrite.enabled = true;
        }
    }

    /// <summary>
    /// /////////
    /// </summary>
    int TweenPoditionIndeex = 0;//用于记录执行的第几个TweenPosition
    int TweenVolumeIndex = 0;//用于记录执行的第几个TweenVolume
    void StartTween()
    {
        if (AlphaOpen)
        {
            if (GetComponent<TweenAlpha>() != null)
            {
                GetComponent<TweenAlpha>().enabled = true;
                GetComponent<TweenAlpha>().AddOnFinished(True_Alpha);
            }
        }
        //
        if (PositionOpen)
        {
            if (GetComponent<TweenPosition>() != null)
            {
                //GetComponent<TweenPosition>().from = FromToV3D[TweenPoditionIndeex].from + transform.localPosition;
                // GetComponent<TweenPosition>().to = FromToV3D[TweenPoditionIndeex].to + transform.localPosition;
                // TweenPoditionIndeex++;
                GetComponent<TweenPosition>().enabled = true;
                GetComponent<TweenPosition>().AddOnFinished(True_Position);
            }
        }
        //
        if (ColorOpen)
        {
            if (GetComponent<TweenColor>() != null)
            {
                GetComponent<TweenColor>().enabled = true;
                GetComponent<TweenColor>().AddOnFinished(True_Color);
            }
        }
        //
        if (VolumeOpen)
        {
            if (GetComponent<TweenVolume>() != null)
            {
                AudioSource theAudioSource = GetComponent<AudioSource>();
                if (theAudioSource != null)
                {
                    theAudioSource.enabled = false;
                    theAudioSource.clip = AudioSourceList[TweenVolumeIndex].clip;
                    theAudioSource.mute = AudioSourceList[TweenVolumeIndex].mute;
                    theAudioSource.bypassEffects = AudioSourceList[TweenVolumeIndex].bypassEffects;
                    theAudioSource.bypassListenerEffects = AudioSourceList[TweenVolumeIndex].bypassListenerEffects;
                    theAudioSource.bypassReverbZones = AudioSourceList[TweenVolumeIndex].bypassReverbZones;
                    theAudioSource.playOnAwake = AudioSourceList[TweenVolumeIndex].playOnAwake;
                    theAudioSource.loop = AudioSourceList[TweenVolumeIndex].loop;
                    theAudioSource.priority = AudioSourceList[TweenVolumeIndex].priority;
                    theAudioSource.volume = AudioSourceList[TweenVolumeIndex].volume;
                    theAudioSource.pitch = AudioSourceList[TweenVolumeIndex].pitch;
                    theAudioSource.dopplerLevel = AudioSourceList[TweenVolumeIndex].dopplerLevel;
                    theAudioSource.rolloffMode = AudioSourceList[TweenVolumeIndex].rolloffMode;
                    theAudioSource.minDistance = AudioSourceList[TweenVolumeIndex].minDistance;
                    theAudioSource.maxDistance = AudioSourceList[TweenVolumeIndex].maxDistance;
                    theAudioSource.spatialBlend = AudioSourceList[TweenVolumeIndex].spatialBlend;
                    theAudioSource.spread = AudioSourceList[TweenVolumeIndex].spread;
                    theAudioSource.panStereo = AudioSourceList[TweenVolumeIndex].panStereo;
                    theAudioSource.enabled = true;
                }
                TweenVolumeIndex++;
                GetComponent<TweenVolume>().enabled = true;
                GetComponent<TweenVolume>().AddOnFinished(True_Volume);
            }
        }
        //
        if (RotationOpen)
        {
            if (GetComponent<TweenRotation>() != null)
            {
                GetComponent<TweenRotation>().enabled = true;
                GetComponent<TweenRotation>().AddOnFinished(True_Rotation);
            }
        }
        //
        if (ScaleOpen)
        {
            if (GetComponent<TweenScale>() != null)
            {
                GetComponent<TweenScale>().enabled = true;
                GetComponent<TweenScale>().AddOnFinished(True_Scale);
            }
        }
        //
        if (HeightOpen)
        {
            if (GetComponent<TweenHeight>() != null)
            {
                GetComponent<TweenHeight>().enabled = true;
                GetComponent<TweenHeight>().AddOnFinished(True_Height);
            }
        }
        //
        if (WidthOpen)
        {
            if (GetComponent<TweenWidth>() != null)
            {
                GetComponent<TweenWidth>().enabled = true;
                GetComponent<TweenWidth>().AddOnFinished(True_Width);
            }
        }
    }
    void DestroyTween(TweenType tweenType)
    {
        switch (tweenType)
        {
            case TweenType.alpha:
                if (GetComponent<TweenAlpha>() != null)
                {
                    DestroyImmediate(GetComponent<TweenAlpha>());
                    if (GetComponent<TweenAlpha>() != null)
                    {
                        GetComponent<TweenAlpha>().enabled = true;
                        GetComponent<TweenAlpha>().AddOnFinished(True_Alpha);
                    }
                }
                break;
            case TweenType.position:
                if (GetComponent<TweenPosition>() != null)
                {
                    DestroyImmediate(GetComponent<TweenPosition>());
                    if (GetComponent<TweenPosition>() != null)
                    {
                        GetComponent<TweenPosition>().enabled = true;
                        GetComponent<TweenPosition>().AddOnFinished(True_Position);
                    }
                }
                break;
            case TweenType.color:
                if (GetComponent<TweenColor>() != null)
                {
                    DestroyImmediate(GetComponent<TweenColor>());
                    if (GetComponent<TweenColor>() != null)
                    {
                        GetComponent<TweenColor>().enabled = true;
                        GetComponent<TweenColor>().AddOnFinished(True_Color);
                    }
                }
                break;
            case TweenType.volume:
                if (GetComponent<TweenVolume>() != null)
                {
                    DestroyImmediate(GetComponent<TweenVolume>());
                    if (GetComponent<TweenVolume>() != null)
                    {
                        AudioSource theAudioSource = GetComponent<AudioSource>();
                        if (theAudioSource != null)
                        {
                            theAudioSource.enabled = false;
                            theAudioSource.clip = AudioSourceList[TweenVolumeIndex].clip;
                            theAudioSource.mute = AudioSourceList[TweenVolumeIndex].mute;
                            theAudioSource.bypassEffects = AudioSourceList[TweenVolumeIndex].bypassEffects;
                            theAudioSource.bypassListenerEffects = AudioSourceList[TweenVolumeIndex].bypassListenerEffects;
                            theAudioSource.bypassReverbZones = AudioSourceList[TweenVolumeIndex].bypassReverbZones;
                            theAudioSource.playOnAwake = AudioSourceList[TweenVolumeIndex].playOnAwake;
                            theAudioSource.loop = AudioSourceList[TweenVolumeIndex].loop;
                            theAudioSource.priority = AudioSourceList[TweenVolumeIndex].priority;
                            theAudioSource.volume = AudioSourceList[TweenVolumeIndex].volume;
                            theAudioSource.pitch = AudioSourceList[TweenVolumeIndex].pitch;
                            theAudioSource.dopplerLevel = AudioSourceList[TweenVolumeIndex].dopplerLevel;
                            theAudioSource.rolloffMode = AudioSourceList[TweenVolumeIndex].rolloffMode;
                            theAudioSource.minDistance = AudioSourceList[TweenVolumeIndex].minDistance;
                            theAudioSource.maxDistance = AudioSourceList[TweenVolumeIndex].maxDistance;
                            theAudioSource.spatialBlend = AudioSourceList[TweenVolumeIndex].spatialBlend;
                            theAudioSource.spread = AudioSourceList[TweenVolumeIndex].spread;
                            theAudioSource.panStereo = AudioSourceList[TweenVolumeIndex].panStereo;
                            theAudioSource.enabled = true;
                        }
                        TweenVolumeIndex++;
                        GetComponent<TweenVolume>().enabled = true;
                        GetComponent<TweenVolume>().AddOnFinished(True_Volume);
                    }
                    else
                    {
                        if (GetComponent<AudioSource>() != null)
                        {
                            DestroyImmediate(GetComponent<AudioSource>());
                        }
                    }
                }
                break;
            case TweenType.rotation:
                if (GetComponent<TweenRotation>() != null)
                {
                    DestroyImmediate(GetComponent<TweenRotation>());
                    if (GetComponent<TweenRotation>() != null)
                    {
                        GetComponent<TweenRotation>().enabled = true;
                        GetComponent<TweenRotation>().AddOnFinished(True_Rotation);
                    }
                }
                break;
            case TweenType.scale:
                if (GetComponent<TweenScale>() != null)
                {
                    DestroyImmediate(GetComponent<TweenScale>());
                    if (GetComponent<TweenScale>() != null)
                    {
                        GetComponent<TweenScale>().enabled = true;
                        GetComponent<TweenScale>().AddOnFinished(True_Scale);
                    }
                }
                break;
            case TweenType.height:
                if (GetComponent<TweenHeight>() != null)
                {
                    DestroyImmediate(GetComponent<TweenHeight>());
                    if (GetComponent<TweenHeight>() != null)
                    {
                        GetComponent<TweenHeight>().enabled = true;
                        GetComponent<TweenHeight>().AddOnFinished(True_Height);
                    }
                }
                break;
            case TweenType.width:
                if (GetComponent<TweenWidth>() != null)
                {
                    DestroyImmediate(GetComponent<TweenWidth>());
                    if (GetComponent<TweenWidth>() != null)
                    {
                        GetComponent<TweenWidth>().enabled = true;
                        GetComponent<TweenWidth>().AddOnFinished(True_Width);
                    }
                }
                break;
        }
    }
    ///
    void True_Alpha()
    {
        DestroyTween(TweenType.alpha);
    }
    void True_Position()
    {
        DestroyTween(TweenType.position);
    }
    void True_Color()
    {
        DestroyTween(TweenType.color);
    }
    void True_Volume()
    {
        DestroyTween(TweenType.volume);
    }
    void True_Rotation()
    {
        DestroyTween(TweenType.rotation);
    }

    void True_Scale()
    {
        DestroyTween(TweenType.scale);
    }
    void True_Height()
    {
        DestroyTween(TweenType.height);
    }
    void True_Width()
    {
        DestroyTween(TweenType.width);
    }


    public static bool IsEnd(GameObject obj)//三级界面点击 ui效果结束
    {
        bool bool_pos = isTruePos(obj);
        bool bool_scale = isTrueScale(obj);
        bool bool_alphe = isTrueAlphe(obj);
        bool bool_rotation = isTrueRotation(obj);
        if (!bool_pos && !bool_scale && !bool_alphe && !bool_rotation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool isTruePos(GameObject obj)
    {
        TweenPosition[] pos = obj.transform.GetComponentsInChildren<TweenPosition>();
        for (int j = 0; j < pos.Length; j++)
        {
            if (pos[j].enabled)
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    pos[i].transform.localPosition = pos[i].to;
                    pos[i].enabled = false;
                    DestroyImmediate(pos[i]);
                }
                return true;
            }
        }
        return false;
    }
    public static bool isTrueScale(GameObject obj)
    {
        TweenScale[] pos = obj.transform.GetComponentsInChildren<TweenScale>();
        for (int j = 0; j < pos.Length; j++)
        {
            if (pos[j].enabled)
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    pos[i].transform.localScale = pos[i].to;
                    pos[i].enabled = false;
                    DestroyImmediate(pos[i]);
                }
                return true;
            }
        }
        return false;
    }
    public static bool isTrueColor(GameObject obj)
    {
        TweenColor[] pos = obj.transform.GetComponentsInChildren<TweenColor>();
        for (int j = 0; j < pos.Length; j++)
        {
            if (pos[j].enabled)
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    if (pos[i].transform.GetComponent<UISprite>() != null)
                    {
                        pos[i].transform.GetComponent<UISprite>().color = pos[i].to;
                    }
                    if (pos[i].transform.GetComponent<UILabel>() != null)
                    {
                        pos[i].transform.GetComponent<UILabel>().color = pos[i].to;
                    }
                    if (pos[i].transform.GetComponent<UITexture>() != null)
                    {
                        pos[i].transform.GetComponent<UITexture>().color = pos[i].to;
                    }
                    pos[i].enabled = false;
                    DestroyImmediate(pos[i]);
                }
                return true;
            }
        }
        return false;
    }
    public static bool isTrueAlphe(GameObject obj)
    {
        TweenAlpha[] pos = obj.transform.GetComponentsInChildren<TweenAlpha>();
        for (int j = 0; j < pos.Length; j++)
        {
            if (pos[j].enabled)
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    if (pos[i].transform.GetComponent<UISprite>() != null)
                    {
                        pos[i].transform.GetComponent<UISprite>().alpha = pos[i].to;
                    }
                    if (pos[i].transform.GetComponent<UILabel>() != null)
                    {
                        pos[i].transform.GetComponent<UILabel>().alpha = pos[i].to;
                    }
                    if (pos[i].transform.GetComponent<UITexture>() != null)
                    {
                        pos[i].transform.GetComponent<UITexture>().alpha = pos[i].to;
                    }
                    if (pos[i].transform.GetComponent<UIPanel>() != null)
                    {
                        pos[i].transform.GetComponent<UIPanel>().alpha = pos[i].to;
                    }
                    pos[i].enabled = false;
                    DestroyImmediate(pos[i]);
                }
                return true;
            }
        }
        return false;
    }
    public static bool isTrueRotation(GameObject obj)
    {
        TweenRotation[] pos = obj.transform.GetComponentsInChildren<TweenRotation>();
        for (int j = 0; j < pos.Length; j++)
        {
            if (pos[j].enabled)
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    pos[i].transform.localEulerAngles = pos[i].to;
                    pos[i].enabled = false;
                    DestroyImmediate(pos[i]);
                }
                return true;
            }
        }
        return false;
    }
    
    static TweenScale press;
    static TweenScale up;
    void PressButton(GameObject obj, bool bl)
    {
        if (bl)//按下
        {
            if (buttonPressUpEffect)
            {
                PressButtonScale(gameObject, bl, scale_up, scale_press, scale_duration);
            }
            if (buttonPressUpEffect_color)
            {
                PressButtonColor(gameObject, bl, color_up, color_press, color_duration);
            }
            if (buttonPressUpEffect_alpha)
            {
                PressButtonAlpha(gameObject, bl, alpha_up, alpha_press, alpha_duration);
            }
            if (buttonPressUpEffect_rotation)
            {
                PressButtonRotation(gameObject, bl, rotation_up, rotation_press, rotation_duration);
            }
            if (buttonPressUpEffect_position)
            {
                PressButtonPosition(gameObject, bl, position_up, position_press, position_duration);
            }
            if (buttonPressUpChangeTexture)
            {
                PressButtonChangTexture(gameObject, bl, textureName_press, textureName_up, uiTexture_press, uiTexture_up);
            }
        }
        else//抬起
        {
            if (buttonPressUpEffect)
            {
                PressButtonScale(gameObject, bl, scale_up, scale_press, scale_duration);
            }
            if (buttonPressUpEffect_color)
            {
                PressButtonColor(gameObject, bl, color_up, color_press, color_duration);
            }
            if (buttonPressUpEffect_alpha)
            {
                PressButtonAlpha(gameObject, bl, alpha_up, alpha_press, alpha_duration);
            }
            if (buttonPressUpEffect_rotation)
            {
                PressButtonRotation(gameObject, bl, rotation_up, rotation_press, rotation_duration);
            }
            if (buttonPressUpEffect_position)
            {
                PressButtonPosition(gameObject, bl, position_up, position_press, position_duration);
            }
            if (buttonPressUpChangeTexture)
            {
                PressButtonChangTexture(gameObject, bl, textureName_press, textureName_up, uiTexture_press, uiTexture_up);
            }
        }
    }
    static float scale_duration_static = 0.08f;
    public static void PressButtonScale(GameObject obj, bool bl, Vector3 scale_up, Vector3 scale_press, float duration)
    {
        if (bl)//按下
        {
            while (obj.GetComponent<TweenScale>() != null)
            {
                obj.GetComponent<TweenScale>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenScale>());
            }
            if (obj.GetComponent<UIButtonScale>() != null)
            {
                obj.GetComponent<UIButtonScale>().enabled = false;
            }
            press = obj.AddComponent<TweenScale>();
            press.enabled = false;
            press.from = scale_up;
            press.to = scale_press;
            press.duration = duration;
            press.delay = 0f;
            press.enabled = true;
            press.AddOnFinished(DestroyPress);
        }
        else//抬起
        {
            while (obj.GetComponent<TweenScale>() != null)
            {
                obj.GetComponent<TweenScale>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenScale>());
            }
            if (obj.GetComponent<UIButtonScale>() != null)
            {
                obj.GetComponent<UIButtonScale>().enabled = false;
            }
            up = obj.AddComponent<TweenScale>();
            up.enabled = false;
            float theX = Math.Abs(scale_press.x - scale_up.x);
            float theY = Math.Abs(scale_press.y - scale_up.y);
            float theZ = Math.Abs(scale_press.z - scale_up.z);
            float bili = 1f;
            float theX_now = Math.Abs(obj.transform.localScale.x - scale_up.x);
            float theY_now = Math.Abs(obj.transform.localScale.y - scale_up.y);
            float theZ_now = Math.Abs(obj.transform.localScale.z - scale_up.z);
            if (theX > 0f)
            {
                bili = theX_now / theX;
            }
            if (theY > 0f)
            {
                bili = theY_now / theY;
            }
            if (theZ > 0f)
            {
                bili = theZ_now / theZ;
            }
            //up.from = scale_press;
            up.from = obj.transform.localScale;
            up.to = scale_up;
            up.duration = duration * bili;
            up.delay = 0f;
            up.enabled = true;
            up.AddOnFinished(DestroyUp);
        }
    }
    /// <summary>
    /// //
    /// </summary>
    static TweenColor press_color;
    static TweenColor up_color;
    static float color_duration_static = 0.08f;
    public static void PressButtonColor(GameObject obj, bool bl, Color up, Color press, float duration)
    {
        if (bl)//按下
        {
            while (obj.GetComponent<TweenColor>() != null)
            {
                obj.GetComponent<TweenColor>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenColor>());
            }
            if (obj.GetComponent<UIButtonColor>() != null)
            {
                obj.GetComponent<UIButtonColor>().enabled = false;
            }
            press_color = obj.AddComponent<TweenColor>();
            press_color.enabled = false;
            press_color.from = up;
            press_color.to = press;
            press_color.duration = duration;
            press_color.delay = 0f;
            press_color.enabled = true;
            press_color.AddOnFinished(DestroyPressColor);
        }
        else//抬起
        {
            while (obj.GetComponent<TweenColor>() != null)
            {
                obj.GetComponent<TweenColor>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenColor>());
            }
            if (obj.GetComponent<UIButtonColor>() != null)
            {
                obj.GetComponent<UIButtonColor>().enabled = false;
            }
            up_color = obj.AddComponent<TweenColor>();
            up_color.enabled = false;
            float theR = Math.Abs(press.r - up.r);
            float theG = Math.Abs(press.g - up.g);
            float theB = Math.Abs(press.b - up.b);
            float theA = Math.Abs(press.a - up.a);
            float bili = 1f;
            Color XXcolor = press;
            if (obj.GetComponent<UITexture>() != null)
            {
                XXcolor = obj.GetComponent<UITexture>().color;
                float theR_now = Math.Abs(obj.GetComponent<UITexture>().color.r - up.r);
                float theG_now = Math.Abs(obj.GetComponent<UITexture>().color.g - up.g);
                float theB_now = Math.Abs(obj.GetComponent<UITexture>().color.b - up.b);
                float theA_now = Math.Abs(obj.GetComponent<UITexture>().color.a - up.a);
                if (theR > 0f)
                {
                    bili = theR_now / theR;
                }
                if (theG > 0f)
                {
                    bili = theG_now / theG;
                }
                if (theB > 0f)
                {
                    bili = theB_now / theB;
                }
                if (theA > 0f)
                {
                    bili = theA_now / theA;
                }
            }
            if (obj.GetComponent<UILabel>() != null)
            {
                XXcolor = obj.GetComponent<UILabel>().color;
                float theR_now = Math.Abs(obj.GetComponent<UILabel>().color.r - up.r);
                float theG_now = Math.Abs(obj.GetComponent<UILabel>().color.g - up.g);
                float theB_now = Math.Abs(obj.GetComponent<UILabel>().color.b - up.b);
                float theA_now = Math.Abs(obj.GetComponent<UILabel>().color.a - up.a);
                if (theR > 0f)
                {
                    bili = theR_now / theR;
                }
                if (theG > 0f)
                {
                    bili = theG_now / theG;
                }
                if (theB > 0f)
                {
                    bili = theB_now / theB;
                }
                if (theA > 0f)
                {
                    bili = theA_now / theA;
                }
            }
            if (obj.GetComponent<UISprite>() != null)
            {
                XXcolor = obj.GetComponent<UISprite>().color;
                float theR_now = Math.Abs(obj.GetComponent<UISprite>().color.r - up.r);
                float theG_now = Math.Abs(obj.GetComponent<UISprite>().color.g - up.g);
                float theB_now = Math.Abs(obj.GetComponent<UISprite>().color.b - up.b);
                float theA_now = Math.Abs(obj.GetComponent<UISprite>().color.a - up.a);
                if (theR > 0f)
                {
                    bili = theR_now / theR;
                }
                if (theG > 0f)
                {
                    bili = theG_now / theG;
                }
                if (theB > 0f)
                {
                    bili = theB_now / theB;
                }
                if (theA > 0f)
                {
                    bili = theA_now / theA;
                }
            }
            if (obj.GetComponent<UIWidget>() != null)
            {
                XXcolor = obj.GetComponent<UIWidget>().color;
                float theR_now = Math.Abs(obj.GetComponent<UIWidget>().color.r - up.r);
                float theG_now = Math.Abs(obj.GetComponent<UIWidget>().color.g - up.g);
                float theB_now = Math.Abs(obj.GetComponent<UIWidget>().color.b - up.b);
                float theA_now = Math.Abs(obj.GetComponent<UIWidget>().color.a - up.a);
                if (theR > 0f)
                {
                    bili = theR_now / theR;
                }
                if (theG > 0f)
                {
                    bili = theG_now / theG;
                }
                if (theB > 0f)
                {
                    bili = theB_now / theB;
                }
                if (theA > 0f)
                {
                    bili = theA_now / theA;
                }
            }
            up_color.from = XXcolor;
            up_color.to = up;
            up_color.duration = duration * bili;
            up_color.delay = 0f;
            up_color.enabled = true;
            up_color.AddOnFinished(DestroyUpColor);
        }
    }

    static TweenAlpha press_alpha;
    static TweenAlpha up_alpha;
    static float alpha_duration_static;
    public static void PressButtonAlpha(GameObject obj, bool bl, float up, float press, float duration)
    {
        if (bl)//按下
        {
            while (obj.GetComponent<TweenAlpha>() != null)
            {
                obj.GetComponent<TweenAlpha>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenAlpha>());
            }
            press_alpha = obj.AddComponent<TweenAlpha>();
            press_alpha.enabled = false;
            press_alpha.from = up;
            press_alpha.to = press;
            press_alpha.duration = duration;
            press_alpha.delay = 0f;
            press_alpha.enabled = true;
            press_alpha.AddOnFinished(DestroyPressAlpha);
        }
        else//抬起
        {
            while (obj.GetComponent<TweenAlpha>() != null)
            {
                obj.GetComponent<TweenAlpha>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenAlpha>());
            }
            up_alpha = obj.AddComponent<TweenAlpha>();
            up_alpha.enabled = false;
            float theX = Math.Abs(press - up);
            float bili = 1f;
            float Al = press;
            if (obj.GetComponent<UITexture>() != null)
            {
                Al = obj.GetComponent<UITexture>().alpha;
                float theX_now = Math.Abs(Al - up);
                if (theX > 0f)
                {
                    bili = theX_now / theX;
                }
            }
            if (obj.GetComponent<UISprite>() != null)
            {
                Al = obj.GetComponent<UISprite>().alpha;
                float theX_now = Math.Abs(Al - up);
                if (theX > 0f)
                {
                    bili = theX_now / theX;
                }
            }
            if (obj.GetComponent<UILabel>() != null)
            {
                Al = obj.GetComponent<UILabel>().alpha;
                float theX_now = Math.Abs(Al - up);
                if (theX > 0f)
                {
                    bili = theX_now / theX;
                }
            }
            if (obj.GetComponent<UIPanel>() != null)
            {
                Al = obj.GetComponent<UIPanel>().alpha;
                float theX_now = Math.Abs(Al - up);
                if (theX > 0f)
                {
                    bili = theX_now / theX;
                }
            }
            if (obj.GetComponent<UIWidget>() != null)
            {
                Al = obj.GetComponent<UIWidget>().alpha;
                float theX_now = Math.Abs(Al - up);
                if (theX > 0f)
                {
                    bili = theX_now / theX;
                }
            }
            up_alpha.from = Al;
            up_alpha.to = up;
            up_alpha.duration = duration * bili;
            up_alpha.delay = 0f;
            up_alpha.enabled = true;
            up_alpha.AddOnFinished(DestroyUpAlpha);
        }
    }

    static TweenRotation press_rotation;
    static TweenRotation up_rotation;
    static float rotation_duration_static;

    public static void PressButtonRotation(GameObject obj, bool bl, Vector3 up, Vector3 press, float duration)
    {
        if (bl)//按下
        {
            while (obj.GetComponent<TweenRotation>() != null)
            {
                obj.GetComponent<TweenRotation>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenRotation>());
            }
            press_rotation = obj.AddComponent<TweenRotation>();
            press_rotation.enabled = false;
            press_rotation.from = up;
            press_rotation.to = press;
            press_rotation.duration = duration;
            press_rotation.delay = 0f;
            press_rotation.enabled = true;
            press_rotation.AddOnFinished(DestroyPressRotation);
        }
        else//抬起
        {
            while (obj.GetComponent<TweenRotation>() != null)
            {
                obj.GetComponent<TweenRotation>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenRotation>());
            }
            up_rotation = obj.AddComponent<TweenRotation>();
            up_rotation.enabled = false;
            float theX = Math.Abs(press.x - up.x);
            float theY = Math.Abs(press.y - up.y);
            float theZ = Math.Abs(press.z - up.z);
            float bili = 1f;
            float theX_now = 0f;
            if (press.x > 0f)
            {
                theX_now = Math.Abs(obj.transform.eulerAngles.x - up.x);
            }
            else
            {
                theX_now = Math.Abs((obj.transform.eulerAngles.x - 360f) - up.x);
            }
            float theY_now = 0f;
            if (press.y > 0f)
            {
                theY_now = Math.Abs(obj.transform.eulerAngles.y - up.y);
            }
            else
            {
                theY_now = Math.Abs((obj.transform.eulerAngles.y - 360f) - up.y);
            }
            float theZ_now = 0f;
            if (press.z > 0f)
            {
                theZ_now = Math.Abs(obj.transform.eulerAngles.z - up.z);
            }
            else
            {
                theZ_now = Math.Abs((obj.transform.eulerAngles.z - 360f) - up.z);
            }
            if (theX > 0f)
            {
                bili = theX_now / theX;
            }
            if (theY > 0f)
            {
                bili = theY_now / theY;
            }
            if (theZ > 0f)
            {
                bili = theZ_now / theZ;
            }
            //up_rotation.from = press;
            up_rotation.from = obj.transform.eulerAngles;
            if (press.x < 0f)
            {
                up_rotation.from = up_rotation.from - new Vector3(360f, 0f, 0f); ;
            }

            if (press.y < 0f)
            {
                up_rotation.from = up_rotation.from - new Vector3(0f, 360f, 0f); ;
            }

            if (press.z < 0f)
            {
                up_rotation.from = up_rotation.from - new Vector3(0f, 0f, 360f); ;
            }
            
            up_rotation.to = up;
            up_rotation.duration = duration * bili;
            up_rotation.delay = 0f;
            up_rotation.enabled = true;
            up_rotation.AddOnFinished(DestroyUpRotation);
        }
    }
    
    static TweenPosition press_position;
    static TweenPosition up_position;
    static float position_duration_static;

    public static void PressButtonPosition(GameObject obj, bool bl, Vector3 up, Vector3 press, float duration)
    {
        if (bl)//按下
        {
            while (obj.GetComponent<TweenPosition>() != null)
            {
                obj.GetComponent<TweenPosition>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenPosition>());
            }
            press_position = obj.AddComponent<TweenPosition>();
            press_position.enabled = false;
            press_position.from = up;
            press_position.to = press;
            press_position.duration = duration;
            press_position.delay = 0f;
            press_position.enabled = true;
            press_position.AddOnFinished(DestroyPressPosition);
        }
        else//抬起
        {
            while (obj.GetComponent<TweenPosition>() != null)
            {
                obj.GetComponent<TweenPosition>().enabled = false;
                DestroyImmediate(obj.GetComponent<TweenPosition>());
            }
            up_position = obj.AddComponent<TweenPosition>();
            up_position.enabled = false;
            float theX = Math.Abs(press.x - up.x);
            float theY = Math.Abs(press.y - up.y);
            float theZ = Math.Abs(press.z - up.z);
            float bili = 1f;
            float theX_now = Math.Abs(obj.transform.localPosition.x - up.x);
            float theY_now = Math.Abs(obj.transform.localPosition.y - up.y);
            float theZ_now = Math.Abs(obj.transform.localPosition.z - up.z);
            if (theX > 0f)
            {
                bili = theX_now / theX;
            }
            if (theY > 0f)
            {
                bili = theY_now / theY;
            }
            if (theZ > 0f)
            {
                bili = theZ_now / theZ;
            }
            //up_position.from = press;
            up_position.from = obj.transform.localPosition;
            up_position.to = up;
            up_position.duration = duration * bili;
            up_position.delay = 0f;
            up_position.enabled = true;
            up_position.AddOnFinished(DestroyUpPosition);
        }
    }
    ///


    public static void PressButtonChangTexture(GameObject obj, bool bl, string textureName_press_x, string textureName_up_x, Texture uiTexture_press_x, Texture uiTexture_up_x)
    {
        if (bl)//按下
        {
            if (obj.GetComponent<UISprite>() != null || obj.GetComponent<UITexture>() != null)
            {
                if (obj.GetComponent<UISprite>() != null)
                {
                    textureName_press_x.Trim();
                    if (textureName_press_x.CompareTo("") != 0)
                    {
                        obj.GetComponent<UISprite>().spriteName = textureName_press_x;
                    }
                }
                if (obj.GetComponent<UITexture>() != null)
                {
                    if (uiTexture_press_x != null)
                    {
                        obj.GetComponent<UITexture>().mainTexture = uiTexture_press_x;
                    }
                }
            }
            else
            {
                Debug.Log("未检测到UISprite或者UITexture组件！");
            }
        }
        else//抬起
        {
            if (obj.GetComponent<UISprite>() != null || obj.GetComponent<UITexture>() != null)
            {
                if (obj.GetComponent<UISprite>() != null)
                {
                    textureName_up_x.Trim();
                    if (textureName_up_x.CompareTo("") != 0)
                    {
                        obj.GetComponent<UISprite>().spriteName = textureName_up_x;
                    }
                }
                if (obj.GetComponent<UITexture>() != null)
                {
                    if (uiTexture_up_x != null)
                    {
                        obj.GetComponent<UITexture>().mainTexture = uiTexture_up_x;
                    }
                }
            }
            else
            {
                Debug.Log("未检测到UISprite或者UITexture组件！");
            }
        }
    }
    /// 
    ///  ///
    static void DestroyPressPosition()
    {
        if (press_position != null)
        {
            DestroyImmediate(press_position);
            press_position = null;
        }
    }
    static void DestroyUpPosition()
    {
        if (up_position != null)
        {
            DestroyImmediate(up_position);
            up_position = null;
        }
    }
    //
    ///
    static void DestroyPressRotation()
    {
        if (press_rotation != null)
        {
            DestroyImmediate(press_rotation);
            press_rotation = null;
        }
    }
    static void DestroyUpRotation()
    {
        if (up_rotation != null)
        {
            DestroyImmediate(up_rotation);
            up_rotation = null;
        }
    }
    //
    ///
    static void DestroyPressAlpha()
    {
        if (press_alpha != null)
        {
            DestroyImmediate(press_alpha);
            press_alpha = null;
        }
    }
    static void DestroyUpAlpha()
    {
        if (up_alpha != null)
        {
            DestroyImmediate(up_alpha);
            up_alpha = null;
        }
    }
    //
    static void DestroyPressColor()
    {
        if (press_color != null)
        {
            DestroyImmediate(press_color);
            press_color = null;
        }
    }
    static void DestroyUpColor()
    {
        if (up_color != null)
        {
            DestroyImmediate(up_color);
            up_color = null;
        }
    }
    static void DestroyPress()
    {
        if (press != null)
        {
            DestroyImmediate(press);
            press = null;
        }
    }
    static void DestroyUp()
    {
        if (up != null)
        {
            DestroyImmediate(up);
            up = null;
        }
    }
}

