using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_GUI : MonoBehaviour
{
    public Director _director;
    bool welcome = true;
    // Start is called before the first frame update
    void Start()
    {
        _director = Director.getInstance();
    }

    private void OnGUI()
    {
        if (welcome)
        {
            if (GUI.Button(new Rect(0.4f * Screen.width, 70, 150, 35), "运动学模式"))
            {
                welcome = false;
                _director.currentController._UFOfactory.choose = 0;
                _director.currentController._UFOfactory.enabled = true;
            }
            if (GUI.Button(new Rect(0.4f * Screen.width, 110, 150, 35), "动力学模式"))
            {
                welcome = false;
                _director.currentController._UFOfactory.choose = 1;
                _director.currentController._UFOfactory.enabled = true;
            }
        }
        int my_round = _director.currentController._UFOfactory.round;
        if (my_round == 11)
        {
            int final_score = _director.currentController._UFOfactory.score;
            GUIStyle ending = new GUIStyle();
            ending.normal.background = null;
            ending.normal.background = null;
            ending.normal.textColor = new Color(255, 255, 255);
            ending.fontSize = 80;
            string ending_score = "Final Score: " + final_score.ToString();
            GUI.Label(new Rect(0.13f * Screen.width, 0.4f * Screen.height, 300, 300), ending_score, ending);
            if (GUI.Button(new Rect(0.7f * Screen.width, 0.7f*Screen.height, 150, 35), "重新开始"))
            {
                Application.LoadLevel(0);
            }
        }
        else
        {
            string round = my_round.ToString();
            round = "Round: " + round;
            GUIStyle bb = new GUIStyle();       //创建GUI的格式
            bb.normal.background = null;
            bb.normal.textColor = new Color(255, 255, 255);
            bb.fontSize = 25;
            GUI.Label(new Rect(0.8f * Screen.width, 240, 150, 35), round, bb);
            string score = _director.currentController._UFOfactory.score.ToString();
            score = "Score:" + score;
            GUI.Label(new Rect(0.8f * Screen.width, 270, 150, 35), score, bb);
        }
    }
}
