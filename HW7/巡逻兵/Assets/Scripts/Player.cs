using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public delegate void AnimationHandler();
    Animation animation;
//    public static Player instance;
    public AnimationClip Die;
    public AnimationClip Run;
    public AnimationClip Idle;
    public AnimationHandler animationHandler;


    public delegate void ChangeHandler();
    public ChangeHandler changeHandler;

    public int sign;    // 1, 2, 3, 4
    public void judge_sign()
    {
        int tempsign;
        if (transform.position.x < 0)
        {
            if (transform.position.z > 0)
                tempsign = 1;
            else
                tempsign = 3;
        }
        else
        {
            if (transform.position.z > 0)
                tempsign = 2;
            else
                tempsign = 4;
        }
        if (tempsign != sign)
        {
            sign = tempsign;
            changeHandler();
        }
        Debug.Log(sign);
    }
    public void Start()
    {
        animationHandler = PlayRun;
        sign = 3;
        changeHandler = Director.getInstance().currentController._NPCfactory.NPC1.GetComponent<NPC>().Observe;
        changeHandler += Director.getInstance().currentController._NPCfactory.NPC2.GetComponent<NPC>().Observe;
        changeHandler += Director.getInstance().currentController._NPCfactory.NPC3.GetComponent<NPC>().Observe;
   //     instance = this;
        animation = GetComponent<Animation>();

    }
    public void PlayIdle()
    {
        animation.Play(Idle.name);
    }
    public void PlayDie()
    {
        animation.Play(Die.name);
    }

    public void PlayRun()
    {
        animation.Play(Run.name);
    }
    void Update()
    {
        if (animationHandler != PlayDie)
        {
            float KeyVertical = Input.GetAxis("Vertical");
            float KeyHorizontal = Input.GetAxis("Horizontal");
            Vector3 newDir = new Vector3(KeyHorizontal, 0, KeyVertical).normalized;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                transform.forward = Vector3.Lerp(transform.forward, newDir, 1000000);
            }
            transform.position += newDir * Time.deltaTime * 3;
        }
        animationHandler();
        judge_sign();
        if (animationHandler == PlayDie)
            this.enabled = false;
    }

}