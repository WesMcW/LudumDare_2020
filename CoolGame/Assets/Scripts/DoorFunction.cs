using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    public bool waitingForPlayer = false;

    public void OpenDoor()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().SetTrigger("open");
        waitingForPlayer = true;
    }

    public void CloseDoor()
    {
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
