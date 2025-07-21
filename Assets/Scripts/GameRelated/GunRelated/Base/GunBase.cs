using System.Collections;
//using System.Numerics;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public  GunData GunData;
    private int maxAmmo;
    private int currentAmmo;
    private GameObject muzzlePoint;
    protected Animator textAnimator;
    protected bool canShoot = true;
    protected bool isReloading = false;
    private bool triggerHeld = false; 
    private bool triggerPressed = false;
    private Animator[] allChildAnimators;
    protected virtual  void  Start(){
        GetComponentInChildren<SpriteRenderer>().sprite = GunData.gunSprite;
        allChildAnimators = GetComponentsInChildren<Animator>();
        textAnimator = GetTextAnimator();
        // Debug.Log("textAnimator: " + textAnimator);
        muzzlePoint = transform.Find("MuzzlePoint").gameObject;
        SetGunDataValues();
    }
    public virtual void FireOneBullet(){
        if(isReloading)
            return;

        if (GunData.projectilePrefab != null){
            Debug.Log("Bullet instantiated.");
            Instantiate(GunData.projectilePrefab, muzzlePoint.transform.position, GetSpreadAngle());
            if(currentAmmo > 0)
                currentAmmo--;
            if(currentAmmo <= 0)
                Reload();
        }

        if (GunData.fireSound != null){
            AudioSource.PlayClipAtPoint(GunData.fireSound, transform.position);
        }
    }
    public abstract void Shoot(bool triggerHeld, bool triggerPressed);
    public virtual void Reload(){
        if(!isReloading)
            StartCoroutine(ReloadGun());
    }
    private IEnumerator ReloadGun(){
        PlayReloadAnimation();
        isReloading = true;
        canShoot = false;
        yield return new WaitForSeconds(GunData.reloadTime);
        StopReloadAnimation();
        canShoot = true;
        isReloading = false;
        currentAmmo = maxAmmo;
    }
    private void PlayReloadAnimation(){
        if(textAnimator != null){
            textAnimator.SetTrigger("TriggerReload");
        }
    }
    private void StopReloadAnimation(){
        if(textAnimator != null){
            textAnimator.SetTrigger("TriggerIdle");
        }
    }
    private Animator GetTextAnimator(){
        foreach(Animator textAnim in allChildAnimators){
            if(textAnim.gameObject.name == "GunText")
                return textAnim;
        }
        return null;
    }
    private void SetGunDataValues(){
        maxAmmo = GunData.maxAmmo;
        currentAmmo = maxAmmo;
    }
    private Quaternion GetSpreadAngle(){
        float spreadAngle = Random.Range(-GunData.spreadAngle, GunData.spreadAngle);
        float baseZRotation = muzzlePoint.transform.eulerAngles.z;
        float finalZRotation = baseZRotation + spreadAngle;
        Quaternion spreadRotation = Quaternion.Euler(0f, 0f, finalZRotation);
        return spreadRotation;
    }
}
