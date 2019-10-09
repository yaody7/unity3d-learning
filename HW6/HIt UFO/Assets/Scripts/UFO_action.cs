using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UFO_action
{
    void SetSpeed(int speed);
    void Start();
    void SetRunning(bool b);
    GameObject getPlayer();
    void setPlayer(GameObject g);
    void Update();
}
