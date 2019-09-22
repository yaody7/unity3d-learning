using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public string direction = "Stay";
    private Controller controller = Controller.getInstance();
    GameObject _ship;
    public List<Person> passengers = new List<Person>();
    public Ship()
    {
        this._ship = Object.Instantiate(Resources.Load("Prefabs/ship", typeof(GameObject)), new Vector3(-2.5f, -5.5f, 0), Quaternion.identity, null) as GameObject;
    }
    public Vector3 get_pos()
    {
        return this._ship.transform.position;
    }
    public bool ship_full()
    {
        return this.passengers.Count == 2;
    }
    public void ship_add(Person add)
    {
        this.passengers.Add(add);
    }
    public void ship_remove(Person remove)
    {
        this.passengers.Remove(remove);
    }
    public void move(Vector3 target)
    {
        if (target.x > 0)
        {
            foreach (Person i in passengers)
                i._person.transform.position += new Vector3(1, 0, 0);
            this._ship.transform.position += new Vector3(1, 0, 0);
        }
        else
        {
            foreach (Person i in passengers)
                i._person.transform.position += new Vector3(-1, 0, 0);
            this._ship.transform.position += new Vector3(-1, 0, 0);
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
    }
    public void move(Vector3 target)
    {
        this._person.transform.position = Vector3.MoveTowards(this._person.transform.position, target, 3);
    }
}

public class Shore
{
    GameObject _shore;
    string position;
    public List<Person> passengers { get; set; } = new List<Person>();

    public void shore_add(Person p)
    {
        this.passengers.Add(p);
    }
    public void shore_remove(Person p)
    {
        this.passengers.Remove(p);
    }
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

