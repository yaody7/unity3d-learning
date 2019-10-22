using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float KeyVertical = Input.GetAxis("Vertical");
        float KeyHorizontal = Input.GetAxis("Horizontal");
        Vector3 newDir = new Vector3(KeyHorizontal, 0, KeyVertical).normalized;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            transform.forward = Vector3.Lerp(transform.forward, newDir, 1);
        }
        transform.position += newDir * Time.deltaTime * 3;
    }
}
