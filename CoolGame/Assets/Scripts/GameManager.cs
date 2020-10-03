using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public float gameTime = 0f;

    [Header("Pause Stuff")]
    public bool paused = false;
    public GameObject pauseMenu;

    [Header("Room Stuff")]
    public bool inBattle;
    public RoomSpawner activeRoom;
    public List<GameObject> activeEnemies;

    [Header("Gun Stuff")]
    bool newWeapon = true;
    int currentGun = -1;
    public List<GameObject> guns;
    public Reticle ret;

    void Awake()
    {
        if (inst) Destroy(gameObject);
        else inst = this;

        Invoke("StartRoom", 5F);
    }


    void Update()
    {
        if(!paused) gameTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pauseMenu.SetActive(paused);
            if (paused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }

        if (inBattle)
        {
            if(activeEnemies.Count == 0)
            {
                //check for win
                if (guns.Count == 0)
                {
                    //win things
                }
                else
                {
                    inBattle = false;
                    activeRoom.myDoor.OpenDoor();

                    if (activeRoom.isHall) newWeapon = true;
                    activeRoom = activeRoom.nextRoom;
                }
            }
        }
    }

    public void StartRoom()
    {
        if (newWeapon)
        {
            //get a new weapon
            int rand = Random.Range(0, guns.Count);

            if (currentGun > -1) guns[currentGun].SetActive(false);
            guns[rand].SetActive(true);
            ret.activeGun = guns[rand].GetComponent<Gun>();
            guns[rand].GetComponent<Gun>().SetReticle();

            guns.RemoveAt(rand);
            currentGun = rand;

            newWeapon = false;
        }

        activeEnemies = activeRoom.SpawnAll();
        inBattle = true;
    }
}
