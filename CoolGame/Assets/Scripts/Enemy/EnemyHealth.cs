using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

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
            GameManager.inst.activeEnemies.Remove(gameObject);
            gameObject.GetComponent<Animator>().SetTrigger("Dead");

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
