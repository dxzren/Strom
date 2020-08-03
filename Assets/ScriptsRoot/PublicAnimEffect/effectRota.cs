using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
//using XLua; 

//[Hotfix]
public class effectRota : MonoBehaviour
{
    public float ro = -2;
    public void Update() 
    {
        gameObject.transform.Rotate(new Vector3(0, 0, ro));
    }

}
