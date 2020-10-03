using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OpenDoor();
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) CloseDoor();
    }

    public void OpenDoor()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().SetTrigger("open");
    }

    public void CloseDoor()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Animator>().SetTrigger("close");
    }
}
