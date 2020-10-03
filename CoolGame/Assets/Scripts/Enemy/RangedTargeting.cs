using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedTargeting : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator anim;

    public bool targetReached;
    public float damage;
    public float range;
    public float shootCooldown;
    public float stoppingDist;
    public float lookSpeed;
    public Transform shootPos;

    //Does nothing atm, but will be used to make shots not 100% hit  ¯\_(ツ)_/¯
    public float accuacyOffset;

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

        if (Vector3.Distance(transform.position, player.transform.position) > stoppingDist)
        {
            targetReached = false;
        }
        else
        {
            targetReached = true;
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * lookSpeed);
        }

        Move();
    }

    public void Move()
    {
        if (!targetReached)
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Moving", true);
            anim.SetBool("Shooting", false);
        }
        else
        {
            agent.SetDestination(this.transform.position);
            anim.SetBool("Moving", false);
            Shoot();
        }
    }

    public void Shoot()
    {
        anim.SetBool("Shooting", true);

        RaycastHit hit;
        if (Physics.Raycast(shootPos.position, shootPos.forward, out hit, range))
        {
            if (hit.transform.GetComponent<PlayerHealth>() && currTime <= 0)
            {
                hit.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
                currTime = shootCooldown;
            }
        }
    }
}
