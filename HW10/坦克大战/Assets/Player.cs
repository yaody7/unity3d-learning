using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int count = 10000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (Input.GetMouseButton(0))
        {
            if (count > 10)
            {
                count = 0;
                Vector3 start = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z + 3);
                GameObject temp = Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject)), start, Quaternion.identity, null) as GameObject;
                temp.GetComponent<Bullet>().speed = 3;
            }
        }
    }
    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.position += new Vector3(-10 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.position += new Vector3(10 * Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("我中弹了！");
    }
}
