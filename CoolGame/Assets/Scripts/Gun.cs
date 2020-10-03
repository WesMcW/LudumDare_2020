﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float defaultAcc;
    public float maxAcc;
    public float range;
    public float shootCooldown;
    public int maxAmmo;

    public bool fullAuto;
    public bool shooting;

    public Reticle ret;

    private Camera fpsCam;
    private ParticleSystem flash;
    private float currTime;
    private int currAmmo;
    private bool reloading;
    AudioManager AM;

    private void Start()
    {
        SetReticle();
        currAmmo = maxAmmo;
        reloading = false;
        fpsCam = GetComponentInParent<Camera>();
        flash = GetComponentInChildren<ParticleSystem>();
        AM = AudioManager.inst;
    }

    private void Update()
    {
        currTime -= Time.deltaTime;

        if (!fullAuto)
        {
            if (Input.GetButtonDown("Fire1") && currTime <= 0 && currAmmo > 0)
            {
                //PewPew();
                gameObject.GetComponent<Animator>().SetTrigger("BOOP");
                shooting = true;
                currTime = shootCooldown;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                shooting = false;
            }
        }
        else if (fullAuto)
        {
            if (Input.GetButton("Fire1") && currTime <= 0 && currAmmo > 0)
            {
                //PewPew();
                gameObject.GetComponent<Animator>().SetTrigger("BOOP");
                shooting = true;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                shooting = false;
            }
        }

        if (Input.GetButtonDown("Fire2") && !reloading)
        {
            reloading = true;
            gameObject.GetComponent<Animator>().SetTrigger("reload");
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

        currAmmo--;
        AM.PlayShot1();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.GetComponent<EnemyHealth>())
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }

        //currTime = shootCooldown;
    }

    public void MuzzleFlash()
    {
        flash.Play();
    }

    public void ReloadAmmo()
    {
        currAmmo = maxAmmo;
        reloading = false;
    }
}
