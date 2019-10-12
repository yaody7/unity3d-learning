using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    Director director;
    // Start is called before the first frame update
    void Start()
    {
        director = Director.getInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (director != null)
        {
            director.currentController.af.used[director.currentController.af.used.Count - 1].GetComponent<Rigidbody>().velocity = Vector3.zero;
            director.currentController.af.score += 1;
            if (director.currentController.af.once == true)
            {
                director.currentController.af.used[director.currentController.af.used.Count - 1].GetComponent<tremble>().enabled = true;
                director.currentController.af.once = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
