using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_GUI : MonoBehaviour
{
    public Director _director;
    // Start is called before the first frame update
    void Start()
    {
        _director = Director.getInstance();
    }

    private void OnGUI()
    {
        int my_round = _director.currentController._UFOfactory.round;
        if (my_round == 11)
        {
            GUIStyle ending = new GUIStyle();
            ending.normal.background = null;
            ending.normal.background = null;
            ending.normal.textColor = new Color(255, 255, 255);
            ending.fontSize = 80;
            string ending_score = "Final Score: " + _director.currentController._UFOfactory.score.ToString();
            GUI.Label(new Rect(0.13f * Screen.width, 0.4f * Screen.height, 300, 300), ending_score, ending);
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
