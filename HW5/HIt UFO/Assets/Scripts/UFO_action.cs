using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_action : ScriptableObject
{
    public Director _director;
    public GameObject player;
    Vector3 start;
    Vector3 end;
    public int speed=3;
    public bool running = true;
    // Start is called before the first frame update
    public void Start()
    {
        _director = Director.getInstance();
        start = new Vector3(Random.Range(-20f,20f), Random.Range(-20f, 20f), 0);
        if (start.x < 10 && start.x > -10)
            start.x *= 4;
        if (start.y < 10 && start.y > -10)
            start.y *= 4;
        end = new Vector3(-start.x, -start.y, 0);
        player.transform.position = start;
        setColor();
    }

    // Update is called once per frame
    public void Update()
    {
        if (running)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, end, speed * Time.deltaTime);
            if (player.transform.position == end)
            {
                this._director.currentController._UFOfactory.not_hit(this.player);   
            }
        }
    }

    public void setColor()
    {
        int color = Random.Range(1, 4);
        switch (color)
        {
            case 1:
                player.GetComponent<MeshRenderer>().material.color = Color.red;
                foreach (Transform child in player.transform)
                {
                    child.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                break;
            case 2:
                player.GetComponent<MeshRenderer>().material.color = Color.yellow;
                foreach (Transform child in player.transform)
                {
                    child.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                break;
            case 3:
                player.GetComponent<MeshRenderer>().material.color = Color.blue;
                foreach (Transform child in player.transform)
                {
                    child.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                break;
            default:
                break;
        }
    }
}
