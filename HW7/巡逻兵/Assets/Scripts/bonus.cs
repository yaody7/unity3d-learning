using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonus : MonoBehaviour
{
    public int score;
    int sign;    //1,2,3,4
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        sign = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject == Director.getInstance().currentController._player) {
            score++;
            if (sign == 1)
            {
                this.transform.position = new Vector3(Random.Range(0.5f, 6.5f), 0.5f, Random.Range(1.5f, 6.5f));
                sign = 2;
            }
            else if (sign == 2)
            {
                this.transform.position = new Vector3(Random.Range(-6.5f, -0.5f), 0.5f, Random.Range(-6.5f, 0.5f));
                sign = 3;
            }
            else if (sign == 3)
            {
                this.transform.position = new Vector3(Random.Range(0.5f, 6.5f), 0.5f, Random.Range(-6.5f, -0.5f));
                sign = 4;
            }
            else if (sign == 4)
            {
                this.transform.position = new Vector3(Random.Range(-6.5f, -0.5f), 0.5f, Random.Range(1.5f, 6.5f));
                sign = 1;
            } 
        }
    }
}
