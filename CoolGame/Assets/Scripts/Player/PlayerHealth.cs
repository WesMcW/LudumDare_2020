using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

    public Image deathImg;
    float dying = 0F;

    void Start()
    {
        currHealth = maxHealth;
    }

    void Update()
    {
        if(currHealth < maxHealth)
        {
            dying -= (Time.deltaTime * 2F);
            deathImg.color = new Color32(77, 5, 5, (byte)dying);

            if((maxHealth - currHealth - 1) * 40 == Mathf.Floor(dying))
            {
                dying = (float)(maxHealth - currHealth - 1) * 40F;
                currHealth++;
                if (currHealth == maxHealth) dying = 0;
            }
        }
    }

    public void TakeDamage(float amt)
    {
        currHealth -= amt;

        dying = (maxHealth - currHealth) * 40F;

        if (currHealth <= 0)
        {
            Debug.Log("Game Over");
            dying = 200F;

            // death stuff
        }
    }
}
