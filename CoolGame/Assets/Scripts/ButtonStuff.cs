using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonStuff : MonoBehaviour
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
            AudioManager.inst.deth1.volume = .3f;
            AudioManager.inst.deth2.volume = .3f;
            AudioManager.inst.deth3.volume = .3f;
            AudioManager.inst.doorClose.volume = .3f;
            AudioManager.inst.doorOpen.volume = .3f;
            AudioManager.inst.shot1.volume = .3f;
            mute.SetActive(false);
            muted = false;
        }
        else if(!muted)
        {
            AudioManager.inst.song.volume = 0f;
            AudioManager.inst.deth1.volume = 0f;
            AudioManager.inst.deth2.volume = 0f;
            AudioManager.inst.deth3.volume = 0f;
            AudioManager.inst.doorClose.volume = 0f;
            AudioManager.inst.doorOpen.volume = 0f;
            AudioManager.inst.shot1.volume = 0f;
            mute.SetActive(true);
            muted = true;
        }
    }
}
