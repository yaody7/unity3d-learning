using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    string hint = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        if (this.transform.position.z < -10)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("RobotCube"))
        {
            if (Director.getInstance().currentController.hint.Equals(""))
                Director.getInstance().currentController.hint = "You Win!";
            //win
        }
        else if (other.name.Equals("PlayerCube"))
        {
            if (Director.getInstance().currentController.hint.Equals(""))
                Director.getInstance().currentController.hint = "You Lose!";
            //lose
        }
        Destroy(this.gameObject);
    }
}
