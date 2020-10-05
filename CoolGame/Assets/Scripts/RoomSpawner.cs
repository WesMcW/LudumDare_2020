using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public bool isHall;
    public RoomSpawner nextRoom;

    public List<Spawner> myRoomSpawns;
    List<GameObject> goons;

    public DoorFunction myDoor;

    public GameObject myTimer;
    public GameObject[] myXs;

    public void SpawnAll()
    {
        goons = new List<GameObject>();
        
        for(int i = 0; i < myRoomSpawns.Count; i++)
        {
            StartCoroutine(SpawnThing(i, myRoomSpawns[i].spawnTime));
        }

        /*
        while(time <= maxTime)
        {
            //time += Time.deltaTime;
            foreach (Spawner a in myRoomSpawns)
            {
                if (a.spawnTime <= time && !a.hasSpawned)
                {
                    a.hasSpawned = true;
                    GameObject newSpawn = Instantiate(a.spawnObject, a.spawnpoint.position, Quaternion.identity);
                    active.Add(newSpawn);
                }
            }
            Debug.Log("before time: " + time);
            time += Time.deltaTime;
            Debug.Log("after time: " + time);
        }

        foreach (Spawner a in myRoomSpawns)
        {
            if (!a.hasSpawned)
            {
                a.hasSpawned = true;
                GameObject newSpawn = Instantiate(a.spawnObject, a.spawnpoint.position, Quaternion.identity);
                active.Add(newSpawn);
            }
        }

        Debug.Log("spawns completed");

        foreach (Spawner a in myRoomSpawns) a.hasSpawned = false;
        return active;*/
    }

    void CheckForSpawnsFinished(GameObject add)
    {
        goons.Add(add);

        bool ready = true;
        foreach (Spawner a in myRoomSpawns) if (!a.hasSpawned) ready = false;

        if (ready)
        {
            Debug.Log("spawns completed");
            foreach (Spawner a in myRoomSpawns) a.hasSpawned = false;
            GameManager.inst.GiveEnemies(goons);
        }
    }

    IEnumerator SpawnThing(int index, float time)
    {
        GameObject spawned;
        yield return new WaitForSeconds(time);

        myRoomSpawns[index].hasSpawned = true;
        spawned = Instantiate(myRoomSpawns[index].spawnObject, myRoomSpawns[index].spawnpoint.position, Quaternion.identity);
        CheckForSpawnsFinished(spawned);
    }

    float largestTime()
    {
        float value = -1;
        foreach(Spawner a in myRoomSpawns)
        {
            if (a.spawnTime > value) value = a.spawnTime;
        }
        return value;
    }
}

[System.Serializable]
public class Spawner
{
    public GameObject spawnObject;
    public Transform spawnpoint;
    public float spawnTime;
    public bool hasSpawned;
}
