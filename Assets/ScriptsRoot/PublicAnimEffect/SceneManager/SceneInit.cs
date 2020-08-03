using UnityEngine;
using System.Collections;

public class SceneInit : MonoBehaviour
{
    public GameObject               SceneSetObj;
    public Camera                   Camera;

    private void Start()
    {
        SceneController.Scene                                       = gameObject;
        SceneController.SceneCamera                                 = Camera;
        SceneController.SceneSetting                                = SceneSetObj;

        Util.LoadSceneEffect(gameObject.name, SceneSetObj);

        if (SceneController.LoadSceneCallback != null )
        {
            SceneController.LoadSceneCallback();
            SceneController.LoadSceneCallback                       = null;
        }
    }
}
