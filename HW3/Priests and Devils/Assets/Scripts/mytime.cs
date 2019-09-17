using UnityEngine;

using System.Collections;

using UnityEngine;

using System.Collections;

using UnityEngine.UI;

public class mytime : MonoBehaviour
{

    public int TotalTime = 60;
    void Start()
    {
        StartCoroutine(startTime());   
    }

    public IEnumerator startTime()
    {

        while (TotalTime >= 0)
        {
            yield return new WaitForSeconds(1);
            TotalTime--;

        }
    }
    private void OnGUI()
    {
        if (TotalTime < 1)
            Application.LoadLevel(0);
        GUIStyle bb = new GUIStyle();      
        bb.normal.background = null;
        bb.normal.textColor = new Color(255, 255, 255);
        bb.fontSize = 15;
        GUI.Label(new Rect(0.7f * Screen.width, 20, 150, 35), "倒计时："+TotalTime.ToString(), bb);
    }

}

