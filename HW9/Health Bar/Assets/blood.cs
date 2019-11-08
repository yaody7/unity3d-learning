using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{
    float padding = 0;
    public Transform t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(t.position.x-padding, t.position.y + 2, t.position.z);
    }
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.localScale.x > 0)
            {
                padding += 0.05f;
                transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y, transform.localScale.z);
            }

        }
    }
}
