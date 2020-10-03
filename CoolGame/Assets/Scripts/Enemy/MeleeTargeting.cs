using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeTargeting : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator anim;

    public bool targetReached;
    public float stoppingDist;
    public float range;
    public float damage;
    public float atkCooldown;
    public Transform atkPos;

    private float currTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        currTime -= Time.deltaTime;


        if(Vector3.Distance(transform.position, player.transform.position) > stoppingDist)
        {
            targetReached = false;
        }
        else
        {
            targetReached = true;
        }

        Move();
    }

    public void Move()
    {
        if (!targetReached)
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Moving", true);
        }
        else
        {
            agent.SetDestination(this.transform.position);
            anim.SetBool("Moving", false);
            Punch();
        }
    }

    //Add cooldown
    public void Punch()
    {
        RaycastHit hit;
        if (Physics.Raycast(atkPos.position, transform.forward, out hit, range))
        {
            if (hit.transform.GetComponent<PlayerHealth>() && currTime <= 0)
            {
                anim.SetTrigger("Punch");
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
                currTime = atkCooldown;
            }
        }
    }
}
