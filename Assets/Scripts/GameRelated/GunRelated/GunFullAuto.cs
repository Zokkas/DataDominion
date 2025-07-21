using UnityEngine;

public class GunFullAuto : GunBase
{
    private float nextFireTime;
    public override void Shoot(bool triggerHeld, bool triggerPressed){
        if (triggerHeld && Time.time >= nextFireTime){
            nextFireTime = Time.time + 1f / GunData.fireRate;
            FireOneBullet();
        }
    }

}
