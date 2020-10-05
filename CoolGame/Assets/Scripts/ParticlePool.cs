using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool inst;

    public GameObject particle;
    List<GameObject> pool;

    public GameObject otherParticle;
    List<GameObject> otherPool;

    public GameObject thirdParticle;
    List<GameObject> thirdPool;

    void Awake()
    {
        inst = this;
        pool = new List<GameObject>();
        otherPool = new List<GameObject>();
        thirdPool = new List<GameObject>();
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

    public void ReturnToOtherPool(GameObject thing)
    {
        otherPool.Add(thing);
        thing.SetActive(false);
    }

    public void UseFromOtherPool(Vector3 location)
    {
        GameObject newParticle;

        if (otherPool.Count > 0)
        {
            newParticle = otherPool[0];
            otherPool.RemoveAt(0);

            newParticle.transform.position = location;
            newParticle.SetActive(true);
        }
        else
        {
            Instantiate(otherParticle, location, Quaternion.identity);
        }
    }

    public void UseFromThirdPool(Vector3 location)
    {
        GameObject newParticle;

        if (thirdPool.Count > 0)
        {
            newParticle = thirdPool[0];
            thirdPool.RemoveAt(0);

            newParticle.transform.position = location;
            newParticle.SetActive(true);
        }
        else
        {
            Instantiate(thirdParticle, location, Quaternion.identity);
        }
    }

    public void ReturnToThirdPool(GameObject thing)
    {
        thirdPool.Add(thing);
        thing.SetActive(false);
    }
}
