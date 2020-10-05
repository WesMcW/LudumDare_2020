using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public float gameTime = -5f;
    public bool win = false;

    [Header("Pause Stuff")]
    public bool paused = false;
    public GameObject pauseMenu;

    [Header("Room Stuff")]
    public bool inBattle;
    public RoomSpawner activeRoom;
    public List<GameObject> activeEnemies;
    int lastGun = -1;

    [Header("Gun Stuff")]
    bool newWeapon = true;
    public GameObject currentGun;
    public List<GameObject> guns;
    public Reticle ret;

    void Awake()
    {
        if (inst) Destroy(gameObject);
        else inst = this;

        Invoke("StartRoom", 3F);
    }

    void Update()
    {
        if(!paused && !win) gameTime += Time.deltaTime;
        activeRoom.myTimer.GetComponent<TextMeshProUGUI>().text = SecondsToTime(gameTime);
        if (activeRoom.isHall) activeRoom.nextRoom.myTimer.GetComponent<TextMeshProUGUI>().text = SecondsToTime(gameTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pauseMenu.SetActive(paused);
            if (paused)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }

        if (inBattle)
        {
            if(activeEnemies.Count == 0)
            {
                //check for win
                if (guns.Count == 0 && activeRoom.isHall)
                {
                    //win things
                    Networking.inst.SendScore(PlayerPrefs.GetString("username"), gameTime);
                    win = true;
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
            if(lastGun > -1)
            {
                foreach(RoomSpawner a in FindObjectsOfType<RoomSpawner>())
                {
                    if (!a.isHall) a.myXs[lastGun].SetActive(true);
                }
            }

            //get a new weapon
            int rand = Random.Range(0, guns.Count);
            //lastGun = rand;

            if (currentGun) currentGun.SetActive(false);
            currentGun = guns[rand];
            currentGun.SetActive(true);
            ret.activeGun = currentGun.GetComponent<Gun>();
            currentGun.GetComponent<Gun>().SetReticle();

            lastGun = currentGun.transform.GetSiblingIndex();

            guns.RemoveAt(rand);
            newWeapon = false;
        }

        activeRoom.SpawnAll();
    }

    public void GiveEnemies(List<GameObject> enemies)
    {
        activeEnemies = enemies;
        inBattle = true;
    }

    public string SecondsToTime(float seconds)
    {
        if (seconds < 0) return "0:00";

        int minutes = Mathf.FloorToInt(seconds / 60F);
        seconds -= (float)(minutes * 60);
        seconds *= 100F;
        seconds = (float)Mathf.Round(seconds);
        seconds /= 100F;

        string time;
        if (seconds < 10) time = minutes.ToString() + ":0" + seconds.ToString();
        else time = minutes.ToString() + ":" + seconds.ToString();
        return time;
    }
}
