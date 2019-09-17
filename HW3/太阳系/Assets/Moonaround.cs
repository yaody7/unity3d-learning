using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moonaround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    public Transform earth;
    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(earth.position, Vector3.up, 10000 * Time.deltaTime);
        this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }
}
