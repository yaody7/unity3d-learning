using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public string direction = "Stay";
    private Controller controller = Controller.getInstance();
    public GameObject _ship;
    public List<Person> passengers = new List<Person>();
    public Ship()
    {
        this._ship= Object.Instantiate(Resources.Load("Prefabs/ship", typeof(GameObject)), new Vector3(-2.5f, -5.5f, 0), Quaternion.identity, null) as GameObject;
    }

    public void moveLeft()
    {
        while (this._ship.transform.position.x > -2.5f)
        {
            foreach (Person i in passengers)
                i._person.transform.position += Vector3.left * Time.deltaTime;
            this._ship.transform.position += Vector3.left * Time.deltaTime;
        }
    }
    public void moveRight()
    {
        while (this._ship.transform.position.x < 2.5f)
        {
            foreach (Person i in passengers)
                i._person.transform.position += Vector3.right * Time.deltaTime;
            Debug.Log("1");
            this._ship.transform.position += Vector3.right * Time.deltaTime;
        }
    }
}
public class Person
{
    public GameObject _person;
    public string _name;
    public Person(Vector3 pos,string name)
    {
        if (name == "Priest")
            this._person = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), new Vector3(-6, -1.5f, 0), Quaternion.identity, null) as GameObject;
        else
            this._person= Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), new Vector3(-6, -1.5f, 0), Quaternion.identity, null) as GameObject;
        this._person.transform.position = pos;
        this._name = name;
        this._person.AddComponent<Person_script>();
    }

    public void leftdown()
    {
        Vector3 move = new Vector3(6, 0, 0);
        _person.transform.position += move;
        move = new Vector3(0, -3, 0);
        _person.transform.position += move;
    }
    public void leftup()
    {
        Vector3 move = new Vector3(0, 3, 0);
        _person.transform.position += move;
        move = new Vector3(-6, 0, 0);
        _person.transform.position += move;
    }
    public void rightdown()
    {
        Vector3 move = new Vector3(-6, 0, 0);
        _person.transform.position += move;
        move = new Vector3(0, -3, 0);
        _person.transform.position += move;
    }
    public void rightup()
    {
        Vector3 move = new Vector3(0, 3, 0);
        _person.transform.position += move;
        move = new Vector3(6, 0, 0);
        _person.transform.position += move;
    }
}

public class Shore
{
    GameObject _shore;
    string position;
    public List<Person> passengers = new List<Person>();
    public Shore(string pos)
    {
        if (pos == "left")
        {
            this._shore = Object.Instantiate(Resources.Load("Prefabs/leftShore", typeof(GameObject)), new Vector3(-10,-6,0), Quaternion.identity, null) as GameObject;
        }
        else
        {
            this._shore = Object.Instantiate(Resources.Load("Prefabs/rightShore", typeof(GameObject)), new Vector3(10, -6, 0), Quaternion.identity, null) as GameObject;
        }
        this.position = pos;
    }

}

