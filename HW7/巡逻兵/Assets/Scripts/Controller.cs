using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public NPCFactory _NPCfactory;
    public Director _director;
    private GameObject my_NPCfactory;
    public GameObject _player;
    public GameObject _playground;
    public GameObject _bonus;
    private void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        _director = Director.getInstance();
        _director.currentController = this;
        _player = Object.Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject)), new Vector3(-2, 0, -2), Quaternion.identity, null) as GameObject;
        my_NPCfactory = new GameObject("NPC_Factory");
        my_NPCfactory.AddComponent<NPCFactory>();
        _NPCfactory = Singleton<NPCFactory>.Instance;
        _playground = Object.Instantiate(Resources.Load("Prefabs/Playground", typeof(GameObject)), new Vector3(5.286964f, -1.301903f, 0.5366425f), Quaternion.identity, null) as GameObject;
        _bonus = Object.Instantiate(Resources.Load("Prefabs/Bonus", typeof(GameObject)), new Vector3(-4, 0.5f, -4), Quaternion.identity, null) as GameObject;
    }

}
