using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_manager : MonoBehaviour
{
    string show;
    bool click = true;    //true 就可以点击对象  false 就不行  要等我的动作做完了再点
    person_action current_person_action;
    ship_action current_ship_action;
    Controller _controller = Controller.getInstance();
    // Start is called before the first frame update

    void Start()
    {
        ;
    }
        // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && click == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                Person temp = find_person(hit.collider.gameObject);
                if (temp != null)
                {
                    person_action charu = ScriptableObject.CreateInstance<person_action>();
                    charu.set_Person(temp);
                    current_person_action = charu;
                    click = false;
                }
                
            }
        }
        if (click == false)
        {
            if (current_ship_action!=null)
            {
                if (current_ship_action.update())
                {
                    this.current_ship_action = null;
                    this.click = true;
                }
            }
            else if(current_person_action!=null)
            {
           
                if (current_person_action.update())
                {
                    this.current_person_action = null;
                    this.click = true;
                }
            }

        }
        show = _controller.judge();

    }
    Person find_person(GameObject g)
    {
        if (this._controller.Priest1._person == g)
            return this._controller.Priest1;
        if (this._controller.Priest2._person == g)
            return this._controller.Priest2;
        if (this._controller.Priest3._person == g)
            return this._controller.Priest3;
        if (this._controller.Devil1._person == g)
            return this._controller.Devil1;
        if (this._controller.Devil2._person == g)
            return this._controller.Devil2;
        if (this._controller.Devil3._person == g)
            return this._controller.Devil3;
        else
            return null;
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0.4f * Screen.width, 70, 150, 35), "开船"))
        {
            if (click == true&&this._controller.Ship.passengers.Count!=0)
            {
                ship_action s = ScriptableObject.CreateInstance<ship_action>();
                this.current_ship_action = s;
                this.click = false;
            }
        }
        if (GUI.Button(new Rect(0.4f * Screen.width, 30, 150, 35), "重新开始"))
            Application.LoadLevel(0);
        GUIStyle bb = new GUIStyle();       //创建GUI的格式
        bb.normal.background = null;
        bb.normal.textColor = new Color(255, 255, 255);
        bb.fontSize = 25;
        GUI.Label(new Rect(0.43f * Screen.width, 110, 150, 35), show, bb);
        if (show == "You Fail!" || show == "You Win!")
            click = false;

    }
}

