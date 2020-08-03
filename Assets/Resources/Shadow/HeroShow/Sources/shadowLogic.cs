using UnityEngine;
using System.Collections;

public class shadowLogic : MonoBehaviour
{
    public Transform trans;
	void Update ()
    {
        transform.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y+5, trans.localPosition.z);
	}
}
