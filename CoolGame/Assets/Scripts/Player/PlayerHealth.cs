using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;

    public Image deathImg;
    public Image gameOver;
    float dying = 0F;
    Animator anim;

    void Start()
    {
        currHealth = maxHealth;
        anim = GetComponent<Animator>();
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
            GameManager.inst.currentGun.GetComponent<Gun>().text.gameObject.SetActive(false);
            GameManager.inst.currentGun.SetActive(false);
            GameManager.inst.ret.gameObject.SetActive(false);
            GetComponentInChildren<CameraLook>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            anim.SetTrigger("dead");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("Game Over");
            dying = 200F;
        }
    }

    public void TurnOnGameOver()
    {
        gameOver.gameObject.SetActive(true);
    }
}
