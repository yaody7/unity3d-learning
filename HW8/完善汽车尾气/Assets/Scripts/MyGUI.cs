using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGUI : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject mycar;
    GameObject boom;
    bool start = false;
    void Start()
    {
        mycar = Object.Instantiate(Resources.Load("Prefabs/SkyCar", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity, null) as GameObject;
        boom = Object.Instantiate(Resources.Load("Prefabs/Boom", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity, null) as GameObject;
        mycar.transform.forward = new Vector3(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0.7f * Screen.width, 250, 150, 35), "启动"))
        {
            mycar.transform.Find("normal").gameObject.GetComponent<ParticleSystem>().Play();
            start = true;
        }
        if (GUI.Button(new Rect(0.7f * Screen.width, 290, 150, 35), "故障")&&start)
        {
            mycar.transform.Find("normal").gameObject.GetComponent<ParticleSystem>().Stop();
            mycar.transform.Find("broken").gameObject.GetComponent<ParticleSystem>().Play();
        }
        if (GUI.Button(new Rect(0.7f * Screen.width, 330, 150, 35), "正常行驶")&&start)
        {
            mycar.transform.Find("normal").gameObject.GetComponent<ParticleSystem>().Play();
            mycar.transform.Find("broken").gameObject.GetComponent<ParticleSystem>().Stop();
        }
        if (GUI.Button(new Rect(0.7f * Screen.width, 210, 150, 35), "爆炸"))
        {
            boom.transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>().Play();
            Destroy(mycar);
        }
    }
}
