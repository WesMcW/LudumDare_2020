using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private void Awake()
    {
        Invoke("ReturnToPool", 1F);
    }

    void ReturnToPool()
    {
        ParticlePool.inst.ReturnToPool(gameObject);
    }
}
