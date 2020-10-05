using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFade : MonoBehaviour
{
    Animator anim;
    public GameObject winScreen;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(GameManager.inst.win == true)
        {
            anim.SetTrigger("Won");
        }
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
