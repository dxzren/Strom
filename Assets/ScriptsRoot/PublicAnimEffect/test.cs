using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
    public UILabel label;
    float[] from = new float[] { 500,0,0};
    float[] to = new float[] { 0, 0, 0 };
    string hello = "lokhglwikje;ljhweopirnlhwerijh";

	void Start () {
        Debug.Log((int)(0.1f * 100));
        // UIAnimation.Instance().LineMove(this.gameObject, to,from);
        //Invoke("setLabel",10);
      
	}
	
	
	void Update () {
	
	}

    public void setLabel() 
    {
        label.text = hello;
    }

}
