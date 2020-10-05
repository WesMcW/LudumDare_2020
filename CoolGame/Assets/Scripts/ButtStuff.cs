using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtStuff : MonoBehaviour
{
    public GameObject mute;
    public bool muted;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MuteSong()
    {
        if (muted)
        {
            AudioManager.inst.song.volume = .1f;
            mute.SetActive(false);
            muted = false;
        }
        else if(!muted)
        {
            AudioManager.inst.song.volume = 0f;
            mute.SetActive(true);
            muted = true;
        }
    }
}
