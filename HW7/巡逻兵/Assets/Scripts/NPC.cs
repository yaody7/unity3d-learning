using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public int sign;   //1,2,3,4
    Vector3 current_position;
    public delegate void AnimationHandler();
    Animation animation;
    public static NPC instance;
    public AnimationClip Attack;
    public AnimationClip Run;
    public AnimationClip Walk;
    public AnimationHandler animationHandler;
    void Start()
    {
        current_position = transform.position;
        instance = this;
        animationHandler = PlayWalk;
        animation = GetComponent<Animation>();

    }   
    public void Observe()
    {
        if (Director.getInstance().currentController._player.GetComponent<Player>().sign == this.sign)
        {
            animationHandler = PlayCatch;
        }
        else
        {
            animationHandler = PlayWalk;
        }
    }
    public void PlayCatch()
    {
        transform.forward = (Director.getInstance().currentController._player.transform.position - this.transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, Director.getInstance().currentController._player.transform.position, 3 * Time.deltaTime);
        animation.Play(Run.name);
    }
    public void PlayWalk()
    { 
        animation.Play(Walk.name);
        if ((transform.position - current_position).sqrMagnitude < 0.0001)
            this.transform.RotateAround(transform.position, Vector3.up, Random.Range(-180, 180));
        current_position = transform.position;
        transform.position += 1.5f * Time.deltaTime * transform.forward;
    }

    public void PlayAttack()
    {
        animation.Play(Attack.name);
    }
    void Update()
    {
        animationHandler();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (animationHandler != PlayAttack)
        {
            this.transform.RotateAround(transform.position, Vector3.up, Random.Range(-180, 180));
        }
        if (collision.collider.gameObject == Director.getInstance().currentController._player)
        {
            animationHandler = PlayAttack;
            transform.forward = (Director.getInstance().currentController._player.transform.position-this.transform.position).normalized;
            Director.getInstance().currentController._player.GetComponent<Player>().animationHandler = Director.getInstance().currentController._player.GetComponent<Player>().PlayDie;
        }
        animationHandler();
    }
}