using UnityEngine;
using System.Collections;

public class ThunderChains : MonoBehaviour
{

    public float speed = 8;
    public int SplitCount=4;
    public GameObject BeginPoint;
    public GameObject EndPoint;
    
    Renderer[] renders;
    void Start()
    {
        renders = gameObject.GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        int i = 0;
        foreach (Renderer render in renders)
        {
            foreach (Material m in render.materials)
            {
                int t = (int)(speed * Time.time + i) % SplitCount; ;
                m.mainTextureOffset = new Vector2(0, 1.0f / SplitCount * t);
                m.mainTextureScale = new Vector2(1, 1.0f / SplitCount);
            }
        }
    }
}
