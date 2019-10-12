using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Arrow_Factory af;
    public Director _director;
    private GameObject my_af;
    public GameObject bow;
    public GameObject target;
    void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        my_af = new GameObject("Arrow_Factory");
        my_af.AddComponent<Arrow_Factory>();
        _director = Director.getInstance();
        af = Singleton<Arrow_Factory>.Instance;
        _director.currentController = this;
        LoadResource();
    }
    void LoadResource()
    {
        bow = Instantiate(Resources.Load("Prefabs/bow", typeof(GameObject)), new Vector3(0, 0, -4), Quaternion.identity, null) as GameObject;
        target = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject)), new Vector3(-1, 0, 20), Quaternion.identity, null) as GameObject;
        
    }

    private void Update()
    {
        bow.transform.position = Input.mousePosition - new Vector3(374, 138, 0);
    }
}
