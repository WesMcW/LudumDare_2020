using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public AudioSource shot1;
    public AudioSource doorOpen;
    public AudioSource doorClose;

    public void PlayShot1()
    {
        shot1.Play();
    }

    public void Open()
    {
        doorOpen.Play();
    }

    public void Close()
    {
        doorClose.Play();
    }
}
