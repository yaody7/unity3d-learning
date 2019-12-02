using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Controller : MonoBehaviour

{
    public Director _director;

    private GameObject my_UFOfactory;
    public GameObject robot;
    public GameObject player;
    public string hint;

    void Awake()

    {
        robot = Instantiate(Resources.Load("Prefabs/Robot", typeof(GameObject)), new Vector3(0,0.5f,20), Quaternion.identity, null) as GameObject;
        player = Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject)), new Vector3(0, 0.5f, 0), Quaternion.identity, null) as GameObject;
        _director = Director.getInstance();
        _director.currentController = this;

    }
    private void OnGUI()
    {
        Debug.Log(hint);
        GUIStyle bb = new GUIStyle();       //创建GUI的格式
        bb.normal.background = null;
        bb.normal.textColor = new Color(255, 255, 255);
        bb.fontSize = 25;
        GUI.Label(new Rect(0.3f * Screen.width, 0.4f * Screen.height, 150, 35), hint, bb);
    }
}