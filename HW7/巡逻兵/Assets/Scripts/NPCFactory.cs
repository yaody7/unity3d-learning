using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactory : MonoBehaviour
{
    public int choose = 0;  //0 for Kinematics_actions   1 for Dynamic_actions
    public List<GameObject> used;
    public List<GameObject> not_used;
    public int score = 0;
    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;
    static int once = 1;
    private void Awake()
    {
        if (once == 1)
        {
            NPC1 = Object.Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject)), new Vector3(-2, 0, 2), Quaternion.identity, null) as GameObject;
            NPC1.GetComponent<NPC>().sign = 1;
            NPC2 = Object.Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject)), new Vector3(2, 0, 2), Quaternion.identity, null) as GameObject;
            NPC2.GetComponent<NPC>().sign = 2;
            NPC3 = Object.Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject)), new Vector3(2, 0, -2), Quaternion.identity, null) as GameObject;
            NPC3.GetComponent<NPC>().sign = 4;
            once++;
        }
    }
  
}

