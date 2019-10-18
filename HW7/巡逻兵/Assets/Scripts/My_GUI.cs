using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (_director.currentController._player.GetComponent<Player>().animationHandler == _director.currentController._player.GetComponent<Player>().PlayDie)
        {
            GUIStyle bb = new GUIStyle();   //创建GUI的格式
            bb.normal.background = null;
            bb.normal.textColor = new Color(0, 0, 0);
            bb.fontSize = 50;
            string score = _director.currentController._bonus.GetComponent<bonus>().score.ToString();
            score = "FinalScore: " + score;
            GUI.Label(new Rect(0.3f * Screen.width, 0.4f*Screen.height, 150, 35), score, bb);
        }
        else
        {

            GUIStyle bb = new GUIStyle();   //创建GUI的格式
            bb.normal.background = null;
            bb.normal.textColor = new Color(0, 0, 0);
            bb.fontSize = 25;
            string score = _director.currentController._bonus.GetComponent<bonus>().score.ToString();
            score = "Score: " + score;
            GUI.Label(new Rect(0.7f * Screen.width, 270, 150, 35), score, bb);
            
        }
    }
}
