using UnityEngine;
using System.Collections;

public class FaceToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        //foreach (Camera cam in gameObject.transform.root.GetComponentsInChildren<Camera>())
        //{
        //    if (cam.gameObject.tag == "MainCamera")
        //    {
        //        m_Camera = cam;
        //        break;
        //    }
        //}
		m_Camera = GameObject.FindGameObjectWithTag("MainCamera");
        //m_Camera = FTSceneManager.GetManager().getMainScene().getSceneCamera().gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_Camera != null)
        {
            gameObject.transform.forward = -m_Camera.transform.forward;
        }
	}
    GameObject m_Camera;
}
