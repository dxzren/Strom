using UnityEngine;
using System.Collections;

public class WarningPanel : MonoBehaviour
{
    // public Camera cam;
    // use this for initalization
    public UILabel label;
    void Awake()
    {
        transform.localPosition = new Vector3(0f, -4000f, -200f);
    }
    void Start()
    {
        Invoke("Hide", 3f);
    }
    void Hide()
    {
        GameObject.DestroyImmediate(this.gameObject);
    }

}
