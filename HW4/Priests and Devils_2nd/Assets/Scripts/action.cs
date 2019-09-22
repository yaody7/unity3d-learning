using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action : ScriptableObject
{
    public GameObject player;
    public virtual bool update()
    {
        return true;
    }
}
public class ship_action : action
{
    Controller _controller = Controller.getInstance();
    Vector3 target;
    private void Awake()
    {
        judge_direction();
    }
    void judge_direction()
    {
        target = _controller.Ship.get_pos();
        if (target.x < 0)
        {
            target.x = 2.5f;
        }
        else
        {
            target.x = -2.5f;
        }
    }
    public override bool update()
    {
        if (Vector3.Distance(_controller.Ship.get_pos(), target) > 0.001)
        {
            _controller.Ship.move(target);
            return false;
        }
        else
            return true;
    }
}
public class person_action : action
{
    Controller _controller = Controller.getInstance();
    Person p;
    Vector3 target;
    bool yes = false;
    public void set_Person(Person _p)
    {
        this.p = _p;
    }
    public override bool update()
    {
        Debug.Log(this._controller.Ship.passengers.Count);
        if (yes == false)
        {
            judge_direction();
            yes = true;
        }
        if (target==p._person.transform.position)
        {
            return true;
        }
        p.move(target);
        return false;

    }
    void judge_direction()
    {
        target = p._person.transform.position;
        if (_controller.Ship.ship_full() && p._person.transform.position.y == -1.5)
        {
            return;
        }
        if (p._person.transform.position.y == -1.5)
        {
            if (p._person.transform.position.x < 0)
                target += new Vector3(6, -3, 0);
            else
                target += new Vector3(-6, -3, 0);
        }
        else
        {
            if (this._controller.Ship.get_pos().x < 0)
                target += new Vector3(-6, 3, 0);
            else
                target += new Vector3(6, 3, 0);
        }
        this.change_passengers();
    }
    void change_passengers()
    { 
        if (target.y < -1.5f)
        {
            this._controller.Ship.ship_add(p);
            if (this._controller.Ship.get_pos().x < 0)
                this._controller.leftShore.shore_remove(p);
            else
                this._controller.rightShore.shore_remove(p);
        }
        else
        {
            this._controller.Ship.ship_remove(p);
            if (this._controller.Ship.get_pos().x < 0)
                this._controller.leftShore.shore_add(p);
            else
                this._controller.rightShore.shore_add(p);
        }
    }
}

