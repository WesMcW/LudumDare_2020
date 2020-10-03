using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public float damage;
    public float defaultAcc;
    public float maxAcc;
    public float range;

    public Reticle ret;
    public Camera fpsCam;

    private void Start()
    {
        SetReticle();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PewPew();
        }
    }

    public void SetReticle()
    {
        ret.maxSize = maxAcc;
        ret.restingSize = defaultAcc;
    }

    public void PewPew()
    {
        //wtf am I doing?
        Vector3 pewSpawn = fpsCam.ViewportToWorldPoint(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.GetComponent<EnemyHealth>())
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
