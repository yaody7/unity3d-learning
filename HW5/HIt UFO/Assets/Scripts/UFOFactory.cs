using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : MonoBehaviour
{
    public List<GameObject> used;
    public List<GameObject> not_used;
    public List<UFO_action> actions;
    public int round = 0;
    public int score = 0;
    private void Start()
    {
        used = new List<GameObject>();
        not_used = new List<GameObject>();
        actions = new List<UFO_action>();
        for(int i = 0; i < 10; i++)
        {
            not_used.Add(Object.Instantiate(Resources.Load("Prefabs/UFO", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject);
            actions.Add(ScriptableObject.CreateInstance<UFO_action>());
        }
        for(int i = 0; i < 10; i++)
        {
            actions[i].player = not_used[i];
        }
    }
    private void Update()
    {
        if (round <= 10)
        {
            for (int i = 0; i < 10; i++)
            {
                actions[i].Update();
            }
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
        if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.red)
            score += 3;
        else if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.yellow)
            score += 2;
        else if (g.gameObject.GetComponent<MeshRenderer>().material.color == Color.blue)
            score += 1;
        this.used.Remove(g);
        g.transform.position = new Vector3(0, -20, 0);
        for(int i = 0; i < 10; i++)
        {
            if (actions[i].player == g)
                actions[i].running = false;
        }
        this.not_used.Add(g);
    }
    public void not_hit(GameObject g)
    {
        this.used.Remove(g);
        g.transform.position = new Vector3(0, -20, 0);
        for (int i = 0; i < 10; i++)
        {
            if (actions[i].player == g)
                actions[i].running = false;
        }
        this.not_used.Add(g);
    }

    public void get_ready(int round)
    {
        for(int i = 0; i < 10; i++)
        {
            used.Add(not_used[0]);
            not_used.Remove(not_used[0]);
            actions[i].speed = round + 2;
            actions[i].Start();
            actions[i].running = true;
        }
    }
}
