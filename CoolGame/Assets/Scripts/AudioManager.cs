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
    public AudioSource deth1;
    public AudioSource deth2;
    public AudioSource deth3;
    public AudioSource song;

    public void PlayShot1(float pitch)
    {
        shot1.pitch = pitch;
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

    public void DeathSound(int num)
    {
        if(num == 1)
        {
            float rand = Random.Range(.7f, .9f);
            deth1.pitch = rand;
            deth1.Play();
        }
        else if(num == 2)
        {
            float rand = Random.Range(.7f, .9f);
            deth2.pitch = rand;
            deth2.Play();
        }
        else
        {
            float rand = Random.Range(.7f, .9f);
            deth3.pitch = rand;
            deth3.Play();
        }
    }

    public void PlaySong()
    {
        song.Play();
    }

    private void Start()
    {
        Invoke("PlaySong", 3F);
    }
}
