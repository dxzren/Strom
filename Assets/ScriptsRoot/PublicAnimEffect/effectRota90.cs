using UnityEngine;
using System.Collections;
//using XLua; 

//[Hotfix]
public class effectRota90 : MonoBehaviour
{
    private float dur = 3;
    

    public void Start() 
    {
        InvokeRepeating("Rota",1,dur);
    }

    public void Rota() 
    {
           TweenRotation  rota = this.gameObject.AddComponent<TweenRotation>();
            rota.duration = 0.5f;
            rota.from = new Vector3(this.gameObject.transform.localRotation.x, this.gameObject.transform.localRotation.y, this.gameObject.transform.localRotation.z);
            rota.to = new Vector3(this.gameObject.transform.localRotation.x, this.gameObject.transform.localRotation.y, this.gameObject.transform.localRotation.z - 90);
            EventDelegate.Add(rota.onFinished, delegate()
            {
                MonoBehaviour.DestroyImmediate(rota);
            }, true);
    }
}
