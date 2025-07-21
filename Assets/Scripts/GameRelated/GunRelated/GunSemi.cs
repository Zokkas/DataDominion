using System.Collections;
using UnityEngine;

public class GunSemi : GunBase
{
    public override void Shoot(bool triggerHeld, bool triggerPressed){
        if(triggerPressed){
            StartCoroutine(ShootSingleAndWait());
        }
    }
    private IEnumerator ShootSingleAndWait(){
        if(!canShoot)
            yield break;
        canShoot = false;
        FireOneBullet();
        yield return new WaitForSeconds(GunData.timeBetweenShots);
        canShoot = true;

    }
}
