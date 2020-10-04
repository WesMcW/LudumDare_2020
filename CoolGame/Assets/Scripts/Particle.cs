using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public bool isOther;

    private void Awake()
    {
        Invoke("ReturnToPool", 1F);
    }

    void ReturnToPool()
    {
        if(isOther) ParticlePool.inst.ReturnToOtherPool(gameObject);
        else ParticlePool.inst.ReturnToPool(gameObject);
    }
}
