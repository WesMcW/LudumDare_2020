using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

        if (currHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
