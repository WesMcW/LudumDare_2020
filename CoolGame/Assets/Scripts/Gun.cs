using System.Collections;
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
    public float audioPitch = 1;

    public bool fullAuto;
    public bool shooting;

    public Reticle ret;

    protected Camera fpsCam;
    protected ParticleSystem flash;
    protected float currTime;
    protected int currAmmo;
    protected bool reloading;
    protected AudioManager AM;

    protected void Start()
    {
        //SetReticle();
        currAmmo = maxAmmo;
        reloading = false;
        fpsCam = GetComponentInParent<Camera>();
        flash = GetComponentInChildren<ParticleSystem>();
        AM = AudioManager.inst;
    }

    protected void Update()
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
                currTime = shootCooldown;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                shooting = false;
            }
        }

        if (Input.GetButtonDown("Fire2") && !reloading)
        {
            gameObject.GetComponent<Animator>().SetTrigger("reload");
            reloading = true;
        }
    }

    public void SetReticle()
    {
        ret.maxSize = maxAcc;
        ret.restingSize = defaultAcc;
    }

    public virtual void PewPew()
    {
        // between 5 and 15
        //average spread: .05F
        //largest spread can be: .15F

        float fromRet = ret.currentSize / 100F;

        float x = Random.Range(-fromRet, fromRet);
        float y = Random.Range(-fromRet, fromRet);
        //Debug.Log("(" + x + ", " + y + ")");

        Vector3 pewSpawn = new Vector3(fpsCam.transform.forward.x + x, fpsCam.transform.forward.y + y, fpsCam.transform.forward.z);

        Debug.DrawRay(fpsCam.transform.position, pewSpawn * range, Color.green, 3F);

        currAmmo--;
        AM.PlayShot1(audioPitch);

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, pewSpawn, out hit, range))
        {
            if (hit.transform.GetComponent<EnemyHealth>())
            {
                hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
                ParticlePool.inst.UseFromPool(hit.point);
            }
            else
            {
                ParticlePool.inst.UseFromOtherPool(hit.point);
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
        reloading = false;
        currAmmo = maxAmmo;
    }
}
