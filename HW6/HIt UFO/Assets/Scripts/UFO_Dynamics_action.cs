using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Dynamics_action : ScriptableObject, UFO_action
{
    public Director _director;
    public GameObject player;
    Vector3 start;
    Vector3 end;
    public int speed = 3;
    public bool running = true;
    int framecount = 0;
    // Start is called before the first frame update
    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }
    public void SetRunning(bool b)
    {
        this.running = b;
    }
    public GameObject getPlayer()
    {
        return this.player;
    }
    public void setPlayer(GameObject g)
    {
        this.player = g;
    }
    public void Start()
    {
        _director = Director.getInstance();
        if (player.GetComponent<Rigidbody>() == null)
            player.AddComponent<Rigidbody>();
        start = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0);
        if (start.x < 10 && start.x > -10)
            start.x *= 4;
        if (start.y < 10 && start.y > -10)
            start.y *= 4;
        end = new Vector3(0, 0, 20);
        player.transform.position = start;
        setColor();
        Rigidbody rigit = player.GetComponent<Rigidbody>();
        rigit.velocity = (end - start) * speed * Random.Range(0.0f, 0.10f);
        rigit.useGravity = false;
    }


    // Update is called once per frame
    public void Update()
    {
        framecount++;
        if(framecount>300)
            this._director.currentController._UFOfactory.not_hit(this.player);

        Rigidbody rigit = player.GetComponent<Rigidbody>();
        if (running == false)
        {
            rigit.velocity = Vector3.zero;
            framecount = 0;
        }
        if (player.transform.position.x < -100 || player.transform.position.x > 100 || player.transform.position.x < -100 || player.transform.position.x > 100 || player.transform.position.x < -100 || player.transform.position.x > 100)
        {
            rigit.velocity = Vector3.zero;
            this._director.currentController._UFOfactory.not_hit(this.player);
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
