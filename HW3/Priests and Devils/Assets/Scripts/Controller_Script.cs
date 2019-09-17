using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Controller_Script : MonoBehaviour
{
    public Controller controller = Controller.getInstance();
    private void Start()
    {
        controller.loadResource();
    }
}
public class Controller
{
    public Person Priest1;
    public Person Priest2;
    public Person Priest3;
    public Person Devil1;
    public Person Devil2;
    public Person Devil3;
    public Shore leftShore;
    public Shore rightShore;
    public Ship Ship;
    public GameObject mylight;
    private static Controller _instance = new Controller();
    public static Controller getInstance()
    {
        if (_instance == null)
            _instance = new Controller();
        return _instance;
    }
    public void loadResource()
    {
        mylight = Object.Instantiate(Resources.Load("Prefabs/Directional Light", typeof(GameObject)), new Vector3(0, 3, 0), Quaternion.identity, null) as GameObject;
        leftShore = new Shore("left");
        rightShore = new Shore("right");
        Priest1 = new Person(new Vector3(-6, -1.5f, 0), "Priest");
        Priest2 = new Person(new Vector3(-8, -1.5f, 0), "Priest");
        Priest3 = new Person(new Vector3(-10, -1.5f, 0), "Priest");
        Devil1 = new Person(new Vector3(-7, -1.5f, 0), "Devil");
        Devil2 = new Person(new Vector3(-9, -1.5f, 0), "Devil");
        Devil3 = new Person(new Vector3(-11, -1.5f, 0), "Devil");
        Ship = new Ship();
        leftShore.passengers.Add(Devil1);
        leftShore.passengers.Add(Devil2);
        leftShore.passengers.Add(Devil3);
        leftShore.passengers.Add(Priest1);
        leftShore.passengers.Add(Priest2);
        leftShore.passengers.Add(Priest3);
    }
    public string ship_move()
    {
        if (Ship.passengers.Count == 0)
        {
            this.Ship.direction = "Stay";
            return "Stay";
        }
        if (Ship._ship.transform.position.x < 0)
        {
            this.Ship.direction = "Right";
            return "Right";
        }
        else
        {
            this.Ship.direction = "Left";
            return "Left";
        }
    }
    public string judge()
    {
        
        int leftDevil=0, leftPriest=0, rightDevil=0, rightPriest=0;
        if (Ship._ship.transform.position.x < 0)
        {
            for(int i = 0; i < Ship.passengers.Count; i++)
            {
                if (Ship.passengers[i]._name == "Devil")
                    leftDevil++;
                else
                    leftPriest++;
            }
        }
        else
        {
            for (int i = 0; i < Ship.passengers.Count; i++)
            {
                if (Ship.passengers[i]._name == "Devil")
                    rightDevil++;
                else
                    rightPriest++;
            }
        }
        for(int i = 0; i < leftShore.passengers.Count; i++)
        {
            if (leftShore.passengers[i]._name == "Devil")
                leftDevil++;
            else
                leftPriest++;
        }
        for (int i = 0; i < rightShore.passengers.Count; i++)
        {
            if (rightShore.passengers[i]._name == "Devil")
                rightDevil++;
            else
                rightPriest++;
        }
        if (rightDevil + rightPriest == 6)
            return "You Win!";
        if ((leftDevil > leftPriest&&leftPriest!=0) || (rightDevil > rightPriest&&rightPriest!=0))
            return "You Fail!";
        else
            return "";
        
    }
    public void person_move(Transform _ob)
    {
        
        if (_ob.transform.position.y == -1.5)
        {
            if (Ship.passengers.Count == 2)      //船还有位置
                return;
            if (_ob.transform.position.x < 0)
            {
                Person temp;
                for (int i = 0; i < leftShore.passengers.Count; i++)
                {
                    if (leftShore.passengers[i]._person.transform == _ob)
                    {
                        temp = leftShore.passengers[i];
                        if (Ship._ship.transform.position.x < 0)
                        {
                            temp.leftdown();
                            Ship.passengers.Add(temp);
                            leftShore.passengers.Remove(temp);
                        }
                    }
                }
            }
            else
            {
                Person temp;
                for (int i = 0; i < rightShore.passengers.Count; i++)
                {
                    if (rightShore.passengers[i]._person.transform == _ob)
                    {
                        temp = rightShore.passengers[i];
                        if (Ship._ship.transform.position.x > 0)
                        {
                            temp.rightdown();
                            Ship.passengers.Add(temp);
                            rightShore.passengers.Remove(temp);
                        }
                    }

                }
            }
        }
        else
        {
            Person temp;
            for(int i = 0; i < Ship.passengers.Count; i++)
            {
                if (Ship.passengers[i]._person.transform == _ob)
                {
                    temp = Ship.passengers[i];
                    if (this.Ship._ship.transform.position.x < 0)
                    {
                        temp.leftup();
                        leftShore.passengers.Add(temp);
                        Ship.passengers.Remove(temp);
                    }
                    else
                    {
                        temp.rightup();
                        rightShore.passengers.Add(temp);
                        Ship.passengers.Remove(temp);
                    }
                }
            }           
        }
    }
}
