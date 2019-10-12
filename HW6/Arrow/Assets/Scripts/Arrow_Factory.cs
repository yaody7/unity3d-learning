using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Factory : MonoBehaviour
{
    Director director;
    public List<GameObject> used;
    public List<GameObject> not_used;
    public int trial = 0;
    public int score;
    public bool once = false;
    public Vector3 wind;

    private void Start()
    {
        wind = new Vector3(Random.Range(100, 300), Random.Range(100, 300), Random.Range(1, 300));
        director = Director.getInstance();
        used = new List<GameObject>();
        not_used = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(Resources.Load("Prefabs/arrow", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject;
            temp.AddComponent<Rigidbody>();
            temp.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 100);
            temp.GetComponent<Rigidbody>().useGravity = false;
            temp.GetComponent<tremble>().enabled = false;
            not_used.Add(temp);
        }
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if (director.currentController.bow != null)
        {
            for (int i = 0; i < not_used.Count-1; i++)
            {
                not_used[i].transform.position = director.currentController.bow.transform.position + new Vector3(5, 0.1f, 0);
            }
            not_used[not_used.Count - 1].transform.position = director.currentController.bow.transform.position + new Vector3(0, 0.1f, 0);
        }
        if (Input.GetMouseButtonDown(0)&&not_used.Count>0)
        {
            trial++;
            once = true;
            used.Add(not_used[not_used.Count - 1]);
            used[used.Count - 1].GetComponent<Rigidbody>().AddForce(wind);
            wind = new Vector3(Random.Range(100, 300), Random.Range(100, 300), Random.Range(1, 300));
            not_used.Remove(not_used[not_used.Count - 1]);
        }
        ResetArrow();
    }
    public void ResetArrow()
    {
        if (not_used.Count == 1)
        {
            GameObject temp = Instantiate(Resources.Load("Prefabs/arrow", typeof(GameObject)), new Vector3(0, -20, 0), Quaternion.identity, null) as GameObject;
            temp.AddComponent<Rigidbody>();
            temp.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 100);
            temp.GetComponent<Rigidbody>().useGravity = false;
            temp.GetComponent<tremble>().enabled = false;
            not_used.Add(temp);
        }
    }
}

