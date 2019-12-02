using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    int position = 0;   //0 for left, 1 for right
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (position == 1)
        {
            this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
            if (this.transform.position.x > 10)
                position = 0;
        }
        if (position == 0)
        {
            this.transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
            if (this.transform.position.x < -10)
                position = 1;
        }

    }

}
