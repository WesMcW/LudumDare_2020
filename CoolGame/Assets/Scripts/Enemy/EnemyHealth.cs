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
            Destroy(gameObject);
        }
    }
}
