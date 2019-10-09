using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_UFO : MonoBehaviour
{

    public GameObject cam;
    public Director director;
    private void Start()
    {
        director = Director.getInstance(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Vector3 mp = Input.mousePosition; //get Screen Position

            //create ray, origin is camera, and direction to mousepoint
            Camera ca;
            if (cam != null) ca = cam.GetComponent<Camera>();
            else ca = Camera.main;

            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            //Return the ray's hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                director.currentController._UFOfactory.hitted(hit.transform.gameObject);
            }
        }
    }
}