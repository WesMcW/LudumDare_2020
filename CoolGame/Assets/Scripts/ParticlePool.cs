using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool inst;

    public GameObject particle;
    public List<GameObject> pool;

    void Awake()
    {
        inst = this;
        pool = new List<GameObject>();
    }

    public void ReturnToPool(GameObject thing)
    {
        pool.Add(thing);
        thing.SetActive(false);
    }

    public void UseFromPool(Vector3 location)
    {
        GameObject newParticle;

        if(pool.Count > 0)
        {
            newParticle = pool[0];
            pool.RemoveAt(0);

            newParticle.transform.position = location;
            newParticle.SetActive(true);
        }
        else
        {
            Instantiate(particle, location, Quaternion.identity);
        }
    }
}
