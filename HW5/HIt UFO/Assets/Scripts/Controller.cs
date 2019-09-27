using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public UFOFactory _UFOfactory;
    public Director _director;
    private GameObject my_UFOfactory;
    void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        my_UFOfactory = new GameObject("Disk_Factory");
        my_UFOfactory.AddComponent<UFOFactory>();
        _director = Director.getInstance();
        _UFOfactory = Singleton<UFOFactory>.Instance;
        _director.currentController = this;
    }
}
