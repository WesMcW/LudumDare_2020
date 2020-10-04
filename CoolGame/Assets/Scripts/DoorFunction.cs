using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    public bool waitingForPlayer = false;

    public void OpenDoor()
    {
        AudioManager.inst.Open();
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().SetTrigger("open");
        waitingForPlayer = true;
    }

    public void CloseDoor()
    {
        AudioManager.inst.Close();
        GetComponent<Collider>().enabled = true;
        GetComponent<Animator>().SetTrigger("close");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (waitingForPlayer && other.CompareTag("Player"))
        {
            waitingForPlayer = false;
            CloseDoor();
            GameManager.inst.StartRoom();
        }
    }
}
