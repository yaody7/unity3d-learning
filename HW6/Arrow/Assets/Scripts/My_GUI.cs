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
        GUIStyle bb = new GUIStyle();       //创建GUI的格式
        bb.normal.background = null;
        bb.normal.textColor = new Color(255, 255, 255);
        bb.fontSize = 25;


        string wind = _director.currentController.af.wind.ToString();
        wind = "风向: " + wind;
        GUI.Label(new Rect(0.3f * Screen.width, 40, 150, 35), wind, bb);

        string score = _director.currentController.af.score.ToString();
        score = "Score: " + score;
        GUI.Label(new Rect(0.8f * Screen.width, 170, 150, 35), score, bb);
        string trial = _director.currentController.af.trial.ToString();
        trial = "Trial: " + trial;
        GUI.Label(new Rect(0.8f * Screen.width, 200, 150, 35), trial, bb);
        if (GUI.Button(new Rect(0.75f * Screen.width, 0.7f * Screen.height, 150, 35), "重新开始"))
        {
            Application.LoadLevel(0);
        }
    }
}
