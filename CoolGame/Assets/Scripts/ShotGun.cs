using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public int shotCount;

    public override void PewPew()
    {
        // between 5 and 15
        //average spread: .05F
        //largest spread can be: .15F

        for (int i = 0; i < shotCount; i++)
        {
            float fromRet = ret.currentSize / 100F;

            float x = Random.Range(-fromRet, fromRet);
            float y = Random.Range(-fromRet, fromRet);
            //Debug.Log("(" + x + ", " + y + ")");

            Vector3 pewSpawn = new Vector3(fpsCam.transform.forward.x + x, fpsCam.transform.forward.y + y, fpsCam.transform.forward.z);

            //Debug.DrawRay(fpsCam.transform.position, pewSpawn * range, Color.green, 3F);

            currAmmo--;
            AM.PlayShot1(audioPitch);

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, pewSpawn, out hit, range))
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

        setText();
    }

    protected override void setText()
    {
        text.text = currAmmo / shotCount + " / " + maxAmmo / shotCount;
    }
}
