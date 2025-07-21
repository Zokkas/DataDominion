using System.Collections;
using UnityEngine;

public class GunBurst : GunBase
{
    public override void Shoot(bool triggerHeld, bool triggerPressed){
        if(triggerPressed)
            StartCoroutine(ShootBurst());
    }
    private IEnumerator ShootBurst(){
        if(!canShoot)
            yield break;
        canShoot = false;
        float burstInterval = 1f / GunData.fireRate;
        for(int i = 0; i < GunData.burstCount; i++){
            FireOneBullet();
            yield return new WaitForSeconds(burstInterval);
        }
        yield return new WaitForSeconds(GunData.timeBetweenShots);
        canShoot = true;
    }
}
