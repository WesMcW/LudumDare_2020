using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;
    public bool dead;
    public Material black;

    void Start()
    {
        currHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(float amt)
    {
        currHealth -= amt;

        if(currHealth <= 0)
        {
            dead = true;
            gameObject.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = black;
            GameManager.inst.activeEnemies.Remove(gameObject);
            gameObject.GetComponent<Animator>().SetTrigger("Dead");

            int rand = Random.Range(0, 4);
            AudioManager.inst.DeathSound(rand);

            if (gameObject.GetComponent<RangedTargeting>())
            {
                Destroy(gameObject.GetComponent<RangedTargeting>());
            }
            else if (gameObject.GetComponent<MeleeTargeting>())
            {
                Destroy(gameObject.GetComponent<MeleeTargeting>());
            }
            //gameObject.GetComponent<Animator>().SetTrigger("Dead");
            Destroy(gameObject.GetComponent<NavMeshAgent>());
            Destroy(gameObject.GetComponent<Collider>());
            Destroy(gameObject.GetComponent<EnemyHealth>());
        }
    }
}
