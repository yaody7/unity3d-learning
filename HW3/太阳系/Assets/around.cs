using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class around : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float speed;
    public float yangle, zangle;
    void Update()
    {
        Vector3 axis = new Vector3(0, yangle, zangle);
        this.transform.RotateAround(new Vector3(0, 0, 0), axis, speed * Time.deltaTime);
        this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }
}
