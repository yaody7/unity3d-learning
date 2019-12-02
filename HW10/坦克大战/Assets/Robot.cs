using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    int count = 10000;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = Director.getInstance().currentController.player.transform;
        Debug.Log(player);
        Vector3 start = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z - 3);
        count++;
        Ray ray = new Ray(start, new Vector3(0,0,-1));
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            if (hit.collider.name.Equals("Wall"))
            {
                Debug.Log("HIT WALL");
                return;
            }
            else
            {
                if (count >= 10)
                {
                    count = 0;
                    GameObject temp = Instantiate(Resources.Load("Prefabs/Bullet", typeof(GameObject)), start, Quaternion.identity, null) as GameObject;
                    temp.GetComponent<Bullet>().speed = -3;
                }
                
            }
        }
        Vector3 target = new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, 5 * Time.deltaTime);
    }
}
