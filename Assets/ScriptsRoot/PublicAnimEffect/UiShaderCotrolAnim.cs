using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
public class UiShaderCotrolAnim : MonoBehaviour
{
    public string                   materialName                = "";
    public float                    limit                       = 0.5f;
    public float                    waitTime                    = 0.5f;
    public float                    speedY                      = -0.5f;
    public float                    speedX                      = 0;
    public float                    speedY2                     = 0;
    public float                    speedX2                     = 0;
    public UITexture                texture;

    private float                   timeWentX                   = 0;
    private float                   timeWentY                   = 0;
    private float                   timeWentX2                  = 0;
    private float                   timeWentY2                  = 0;
    private float                   timeWentX_Wait              = 0;
    private float                   timeWentY_Wait              = 0;
    private float                   timeWentX2_Wait             = 0;
    private float                   timeWentY2_Wait             = 0;
    private Material                theMat;
    private string                  theEffectPath               = UIEffectPath.theEffectPath_Se;    //粒子特效预制体路径
	void Start () {
        if (materialName.CompareTo("")!=0)
        {
            Material themateri = GlobalResourceLoad.ResLoad_Mat(theEffectPath + materialName + "/" + materialName);
            if (themateri != null)
            {
                texture.material = Instantiate(themateri) as Material;
            }
            else
            {
                Debug.Log("未找到对应材质" + theEffectPath);
                this.enabled = false;
            }
        }
       texture.gameObject.SetActive(false);
       theMat = texture.material;
       float timeWentX_in = 0f;
       float timeWentY_in = 0f;
       float timeWentX2_in = 0f;
       float timeWentY2_in = 0f;
       if (speedX!=0f)
       {
           timeWentX_in = 0.5f;
        }
       if (speedY!=0f)
       {
           timeWentY_in = 0.5f;
       }
       if (speedX2 != 0f)
       {
           timeWentX2_in = 0.5f;
       }
       if (speedY2 != 0f)
       {
           timeWentY2_in = 0.5f;
       }
       theMat.SetTextureOffset("_MainTex", new Vector2(timeWentX_in, timeWentY_in));
       theMat.SetTextureOffset("_Cutout", new Vector2(timeWentX2_in, timeWentY2_in));
       theMat.SetColor("_Color", new Color(theMat.color.r, theMat.color.g, theMat.color.b, 0f));
       texture.gameObject.SetActive(true);
	}



	void Update ()
    {
            UpdateView();
	}
    void UpdateView()
    {
        if (timeWentX_Wait > 0f)
        {
            timeWentX_Wait -= Time.deltaTime;
        }
        if (timeWentY_Wait > 0f)
        {
            timeWentY_Wait -= Time.deltaTime;
        }
        if (timeWentX2_Wait > 0f)
        {
            timeWentX2_Wait -= Time.deltaTime;
        }
        if (timeWentY2_Wait > 0f)
        {
            timeWentY2_Wait -= Time.deltaTime;
        }
        if (timeWentX_Wait <= 0f && timeWentY_Wait <= 0f && timeWentX2_Wait <= 0f && timeWentY2_Wait <= 0f)
        {
            // texture.material.SetColor("_Color", new Color(1, 1, 1, 1));
            texture.gameObject.SetActive(false);
            timeWentY += Time.deltaTime * speedY;
            timeWentX += Time.deltaTime * speedX;
            timeWentY2 += Time.deltaTime * speedY2;
            timeWentX2 += Time.deltaTime * speedX2;
            float Thex = timeWentY;
            float speed = speedY;
            if (speedX != 0f)
            {
                if (Mathf.Abs(speedX) > Mathf.Abs(speed))
                {
                    Thex = timeWentX;
                    speed = speedX;
                }
            }
            if (speedY2 != 0)
            {
                if (Mathf.Abs(speedY2) > Mathf.Abs(speed))
                {
                    Thex = timeWentY2;
                    speed = speedY2;
                }
            }
            if (timeWentX2 != 0)
            {
                if (Mathf.Abs(speedX2) > Mathf.Abs(speed))
                {
                    Thex = timeWentX2;
                    speed = speedX2;
                }
            }
            if (Thex > 0f || Thex < 0)
            {
                if (Thex > 0f)
                {
                    theMat.SetColor("_Color", new Color(theMat.color.r, theMat.color.g, theMat.color.b, (1f - (1f / limit) * Thex) / 3f));
                }
                if (Thex < 0f)
                {
                    theMat.SetColor("_Color", new Color(theMat.color.r, theMat.color.g, theMat.color.b, (1f + (1f / limit) * Thex) / 3f));
                }
            }
            if (timeWentY >= 1f || timeWentY <= -1f)
            {
                timeWentY = 0;
            }
            if (timeWentX >= 1f || timeWentX <= -1f)
            {
                timeWentX = 0f;
            }
            if (timeWentY2 >= 1f || timeWentY2 <= -1f)
            {
                timeWentY2 = 0;
            }
            if (timeWentX2 >= 1f || timeWentX2 <= -1f)
            {
                timeWentX2 = 0f;
            }
            //
            if (timeWentY >= 0.5f || timeWentY <= -0.5f)
            {
                if (timeWentY <= -0.5f)
                {
                    timeWentY = 0.5f;
                    // texture.material.SetColor("_Color", new Color(1, 1, 1, 0));
                }
                else
                {
                    if (timeWentY >= 0.5f)
                    {
                        timeWentY = -0.5f;
                        // texture.material.SetColor("_Color", new Color(1, 1, 1, 0));
                    }

                }
                timeWentY_Wait = waitTime;
            }
            if (timeWentX >= 0.5f || timeWentX <= -0.5f)
            {
                if (timeWentX <= -0.5f)
                {
                    timeWentX = 0.5f;
                }
                else
                {
                    if (timeWentX >= 0.5f)
                    {
                        timeWentX = -0.5f;
                    }

                }
                timeWentX_Wait = waitTime;
            }
            if (timeWentY2 >= 0.5f || timeWentY2 <= -0.5f)
            {
                if (timeWentY2 <= -0.5f)
                {
                    timeWentY2 = 0.5f;
                }
                else
                {
                    if (timeWentY2 >= 0.5f)
                    {
                        timeWentY2 = -0.5f;
                    }

                }
                timeWentY2_Wait = waitTime;
            }
            if (timeWentX2 >= 0.5f || timeWentX2 <= -0.5f)
            {
                if (timeWentX2 <= -0.5f)
                {
                    timeWentX2 = 0.5f;
                }
                else
                {
                    if (timeWentX2 >= 0.5f)
                    {
                        timeWentX2 = -0.5f;
                    }

                }
                timeWentX2_Wait = waitTime;
            }
            texture.material.SetTextureOffset("_MainTex", new Vector2(timeWentX, timeWentY));
            texture.material.SetTextureOffset("_Cutout", new Vector2(timeWentX2, timeWentY2));
            texture.gameObject.SetActive(true);
        }
    }
}
