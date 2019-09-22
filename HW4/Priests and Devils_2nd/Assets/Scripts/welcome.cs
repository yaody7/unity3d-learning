using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welcome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnGUI()
    {
        GUIStyle bb = new GUIStyle();       //创建GUI的格式
        bb.normal.background = null;
        bb.normal.textColor = new Color(255, 255, 255);
        bb.fontSize = 25;
        GUI.Label(new Rect(0.2f * Screen.width, 30, 150, 35), "游戏规则:", bb);
        GUI.Label(new Rect(0.2f * Screen.width, 60, 150, 35), "白球是牧师，黑球是魔鬼", bb);
        GUI.Label(new Rect(0.2f * Screen.width, 90, 150, 35), "只要有一侧的魔鬼多于牧师，则游戏失败", bb);
        GUI.Label(new Rect(0.2f * Screen.width, 120, 150, 35), "牧师和魔鬼都过河即为胜利", bb);
        if (GUI.Button(new Rect(0.7f * Screen.width, 250, 150, 35), "开始游戏"))
        {
            Application.LoadLevel("play");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
