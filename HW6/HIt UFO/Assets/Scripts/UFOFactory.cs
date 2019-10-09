using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : MonoBehaviour
{
    public int choose = 0;  //0 for Kinematics_actions   1 for Dynamic_actions
    public List<GameObject> used;
    public List<GameObject> not_used;
    public List<UFO_Kinematics_action> Kinematics_actions;
    public List<UFO_Dynamics_action> Dynamics_actions;
    public List<UFO_action> UFO_actions;
    public int round = 0;
    public int score = 0;

    private void Start()
    {
        used = new List<GameObject>();
        not_used = new List<GameObject>();
        Kinematics_actions = new List<UFO_Kinematics_action>();
        Dynamics_actions = new List<UFO_Dynamics_action>();
        UFO_actions = new List<UFO_action>();
        for (int i = 0; i < 10; i++)
        {
            not_used.Add(Object.Instantiate(Resources.Load("Prefabs/UFO", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject);
            Dynamics_actions.Add(ScriptableObject.CreateInstance<UFO_Dynamics_action>());
            Kinematics_actions.Add(ScriptableObject.CreateInstance<UFO_Kinematics_action>());
        }
        if (choose == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                UFO_actions.Add(Dynamics_actions[i]);
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                UFO_actions.Add(Kinematics_actions[i]);
            }

        }
        for(int i = 0; i < 10; i++)
        {
            UFO_actions[i].setPlayer(not_used[i]);
            UFO_actions[i].Start();
        }
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (round <= 10)
        {
            for (int i = 0; i < 10; i++)
                UFO_actions[i].Update();
            if (not_used.Count == 10)
            {
                round += 1;
                if (round <= 10)
                    get_ready(round);
            }
        }
    }
    public void hitted(GameObject g)
    {
        if (round <= 10)
        {
            if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.red)
                score += 3;
            else if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.yellow)
                score += 2;
            else if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.blue)
                score += 1;
        }
        this.used.Remove(g);
        g.transform.position = new Vector3(0, -20, 0);
        for(int i = 0; i < 10; i++)
        {
            if (UFO_actions[i].getPlayer() == g)
            {
                UFO_actions[i].SetRunning(false);
                Rigidbody rigit = UFO_actions[i].getPlayer().GetComponent<Rigidbody>();
                if (rigit != null)
                {
                    rigit.velocity = Vector3.zero;
                }
            }
        }
        this.not_used.Add(g);
    }
    public void not_hit(GameObject g)
    {
        this.used.Remove(g);
        g.transform.position = new Vector3(0, -20, 0);
        for (int i = 0; i < 10; i++)
        {
            if (UFO_actions[i].getPlayer() == g)
            {
                UFO_actions[i].SetRunning(false);
                Rigidbody rigit = UFO_actions[i].getPlayer().GetComponent<Rigidbody>();
                if (rigit != null)
                {
                    rigit.velocity = Vector3.zero;
                }
            }
        }
        this.not_used.Add(g);
    }

    public void get_ready(int round)
    {
        for(int i = 0; i < 10; i++)
        {
            used.Add(not_used[0]);
            not_used.Remove(not_used[0]);
            UFO_actions[i].SetSpeed(round + 2);
            UFO_actions[i].Start();
            UFO_actions[i].SetRunning(true);
        }
    }
}

