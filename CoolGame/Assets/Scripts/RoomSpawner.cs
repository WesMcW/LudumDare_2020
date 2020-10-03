using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public bool isHall;
    public RoomSpawner nextRoom;

    public List<Spawner> myRoomSpawns;

    public DoorFunction myDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> SpawnAll()
    {
        List<GameObject> active = new List<GameObject>();
        float maxTime = largestTime();
        float time = 0;
        
        while(time <= maxTime)
        {
            time += Time.deltaTime;
            foreach (Spawner a in myRoomSpawns)
            {
                if (a.spawnTime <= time && !a.hasSpawned)
                {
                    a.hasSpawned = true;
                    GameObject newSpawn = Instantiate(a.spawnObject, a.spawnpoint.position, Quaternion.identity);
                    active.Add(newSpawn);
                }
            }
        }

        foreach (Spawner a in myRoomSpawns) a.hasSpawned = false;
        return active;
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
