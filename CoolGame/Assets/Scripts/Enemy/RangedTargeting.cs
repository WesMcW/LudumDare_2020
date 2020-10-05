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
    public ParticleSystem flash;

    //Does nothing atm, but will be used to make shots not 100% hit  ¯\_(ツ)_/¯
    public float accuacyOffset;

    bool canShoot = true;
    private float currTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!canShoot)
        {
            currTime -= Time.deltaTime;
            if (currTime < 0)
            {
                canShoot = true;
                currTime = shootCooldown;
            }
        }

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
            if (canShoot)
            {
                canShoot = false;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        if (player.GetComponent<PlayerHealth>().currHealth > 0)
        {
            anim.SetBool("Shooting", true);
            AudioManager.inst.PlayShot1(.9f);
            flash.Play();

            float x = Random.Range(-accuacyOffset, accuacyOffset);
            float y = Random.Range(-accuacyOffset, accuacyOffset);
            //Debug.Log("(" + x + ", " + y + ")");

            Vector3 pewSpawn = new Vector3(shootPos.transform.forward.x + x, shootPos.transform.forward.y + y, shootPos.transform.forward.z);

            //Debug.DrawRay(shootPos.transform.position, pewSpawn * range, Color.red, 3F);

            RaycastHit hit;
            if (Physics.Raycast(shootPos.position, pewSpawn, out hit, range))
            {
                if (hit.transform.GetComponent<PlayerHealth>())
                {
                    hit.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
                    currTime = shootCooldown;
                }
            }
        }
    }
}
