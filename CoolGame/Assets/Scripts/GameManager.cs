using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameTime = 0f;

    [Header("Pause Stuff")]
    public bool paused = false;
    public GameObject pauseMenu;

    void Start()
    {
        
    }


    void Update()
    {
        if(!paused) gameTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pauseMenu.SetActive(paused);
        }
    }
}
