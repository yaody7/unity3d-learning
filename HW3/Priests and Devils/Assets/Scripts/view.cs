using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class view : MonoBehaviour
{
    int my_time = 60;
    string show;
    bool play = true;
    string ship_direcion = "Stay";
    Controller controller = Controller.getInstance();
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0.4f * Screen.width, 70, 150, 35), "开船"))
        {
            if (play == true&&ship_direcion=="Stay")
            {
                ship_direcion = controller.ship_move();
            }
        }
        if(GUI.Button(new Rect(0.4f*Screen.width,30,150,35),"重新开始"))
            Application.LoadLevel(0);
        GUIStyle bb = new GUIStyle();       //创建GUI的格式
        bb.normal.background = null;
        bb.normal.textColor = new Color(255, 255, 255);
        bb.fontSize = 25;
        GUI.Label(new Rect(0.43f*Screen.width, 110, 150, 35), show, bb);
        if (show == "You Fail!" || show == "You Win!")
            play = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (ship_direcion == "Right")
        {
            foreach (Person i in this.controller.Ship.passengers)
                i._person.transform.position += Vector3.right *3* Time.deltaTime;
            this.controller.Ship._ship.transform.position += Vector3.right *3* Time.deltaTime;
            if (this.controller.Ship._ship.transform.position.x >= 2.5f)
            {
                ship_direcion = "Stay";
                this.controller.Ship.direction = "Stay";
                show = this.controller.judge();
            }
        }
        else if(ship_direcion=="Left")
        {
            foreach (Person i in this.controller.Ship.passengers)
                i._person.transform.position += Vector3.left *3* Time.deltaTime;
            this.controller.Ship._ship.transform.position += Vector3.left *3* Time.deltaTime;
            if (this.controller.Ship._ship.transform.position.x <= -2.5f)
            {
                ship_direcion = "Stay";
                this.controller.Ship.direction = "Stay";
                show = this.controller.judge();
            }
        }
    }


}
public class Person_script : MonoBehaviour
{
    Controller _controller = Controller.getInstance();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if (this._controller.Ship.direction == "Stay")
            _controller.person_move(this.transform);
    }

}