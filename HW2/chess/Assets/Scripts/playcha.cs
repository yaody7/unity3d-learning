using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playcha : MonoBehaviour
{
    bool over = false;
    bool x11 = false, x12 = false, x13 = false, x21 = false, x22 = false, x23 = false, x31 = false, x32 = false, x33 = false;
    Vector3 mousePosition, targetPosition;
    public Transform targetObject;
    public int judge = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        float x, z;
        if (mousePosition.x < 314)
            x = -3.8f;
        else if (mousePosition.x < 440)
            x = 0;
        else
            x = 3.8f;
        if (mousePosition.y > 228)
            z = 3.8f;
        else if (mousePosition.y > 123)
            z = 0;
        else
            z = -3.8f;




        targetPosition = new Vector3(x, 0, z);
        
            //Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, distance));
        targetObject.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (judge == 1)
            {
                Instantiate(targetObject, targetObject.transform.position, targetObject.transform.rotation);
                if (x < 0 && z < 0)
                    x31 = true;
                else if (x < 0 && z == 0)
                    x21 = true;
                else if (x < 0 && z > 0)
                    x11 = true;
                else if (x == 0 && z < 0)
                    x32 = true;
                else if (x == 0 && z == 0)
                    x22 = true;
                else if (x == 0 && z > 0)
                    x12 = true;
                else if (x > 0 && z < 0)
                    x33 = true;
                else if (x > 0 && z == 0)
                    x23 = true;
                else
                    x13 = true;
                if ((x11 && x12 && x13) || (x21 && x22 && x23) || (x31 && x32 && x33) || (x11 && x21 && x31) || (x12 && x22 && x32) || (x13 && x23 && x33) || (x11 && x22 && x33) || (x13 && x22 && x31))
                {
                    over = true;
                }
                judge = 0;
            }
            else
                judge = 1;
        }
    }
    private void OnGUI()
    {
        if (over == true)
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 50;

            GUI.Label(new Rect(Screen.width * 0.32f, Screen.height * 0.33f, 300, 300), "Game Over", bb);
            GUI.Label(new Rect(Screen.width * 0.35f, Screen.height * 0.50f, 300, 300), "Black Win", bb);

            //   GameObject.Find("Main Camera").GetComponent<playball>().enabled = false;
            //   GameObject.Find("Main Camera").GetComponent<playcha>().enabled = false;
            if (Input.GetMouseButtonDown(0))
                Application.LoadLevel(0);
        }
    }
}
