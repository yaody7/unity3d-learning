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
    public int leftDevil = 0, leftPriest = 0, rightDevil = 0, rightPriest = 0;
    public Person Priest1;
    public Person Priest2;
    public Person Priest3;
    public Person Devil1;
    public Person Devil2;
    public Person Devil3;
    public Shore leftShore { get; set; }
    public Shore rightShore { get; set; }
    public Ship Ship { get; set; }
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
        leftShore.shore_add(Devil1);
        leftShore.shore_add(Devil2);
        leftShore.shore_add(Devil3);
        leftShore.shore_add(Priest1);
        leftShore.shore_add(Priest2);
        leftShore.shore_add(Priest3);
    }
    public string judge()
    {
        leftDevil = 0;
        leftPriest = 0;
        rightDevil = 0;
        rightPriest = 0;
        if (Ship.get_pos().x < 0)
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
}
