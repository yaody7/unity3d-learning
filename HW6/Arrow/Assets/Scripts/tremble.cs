using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tremble : MonoBehaviour
{
    float radian = 0;
    float per_radian = 3f;
    Vector3 old_pos;                              
    public float left_time = 0;              


    public void OnEnable()
    {
        old_pos = transform.position;
        left_time = 0.3f;
    }

    public void Update()
    {
        left_time -= Time.deltaTime;
        if (left_time <= 0)
        {
            transform.position = old_pos;
            Destroy(this.transform.GetComponent<Rigidbody>());
           this.enabled = false;
        }
        if (left_time > 0)
        {
            float dy = Random.Range(-0.200f, 0.200f);
            transform.position = old_pos + new Vector3(0, dy, 0);
        }
    }
}
