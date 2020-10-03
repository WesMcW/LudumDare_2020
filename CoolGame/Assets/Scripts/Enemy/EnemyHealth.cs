using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (gameObject.GetComponent<RangedTargeting>())
            {
                Destroy(gameObject.GetComponent<RangedTargeting>());
            }
            else if (gameObject.GetComponent<MeleeTargeting>())
            {
                Destroy(gameObject.GetComponent<MeleeTargeting>());
            }
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
            Destroy(gameObject.GetComponent<EnemyHealth>());
        }
    }
}
