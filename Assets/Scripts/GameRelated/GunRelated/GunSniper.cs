using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GunSniper : GunBase
{

    private bool isChamberTextRight = false;
    private TMP_Text gunText;
    private TMP_Text[] allGunTexts;
    private float chamberAnimTime;
    protected override void Start(){
        base.Start();
        allGunTexts = GetComponentsInChildren<TMP_Text>();
        gunText = GetGunText();
    }
    public override void Shoot(bool triggerHeld, bool triggerPressed){
        if(triggerPressed){
            //StartCoroutine(ShootPlayAnimAndWait());
            StartCoroutine(ShootAndWait());
        }
    }
    private IEnumerator ShootAndWait(){
        if(!canShoot)
            yield break;
        FireOneBullet();
        canShoot = false;
        // if (GunData.chamberSound != null){            
        //     AudioSource.PlayClipAtPoint(GunData.chamberSound, transform.position);
        // }
        yield return new WaitForSeconds(GunData.timeBetweenShots);
        canShoot = true;
    }
    private IEnumerator ShootPlayAnimAndWait(){
        if(!canShoot)
            yield break;
        FireOneBullet();
        canShoot = false;
        SetTextAnimSpeed();
        textAnimator.SetTrigger("TriggerChamber");
        ClickClackText();
        yield return new WaitForSeconds(GunData.timeBetweenShots / 2);
        ClickClackText();
        yield return new WaitForSeconds(GunData.timeBetweenShots / 2);
        ResetTextAnimatorSpeed();
        gunText.text = "";
        textAnimator.SetTrigger("TriggerIdle");
        canShoot = true;
    }
    // Disabled for testing
    private void ClickClackText(){
        return;
        if(isChamberTextRight){
            gunText.text = "clack";
            isChamberTextRight = false;
            return;
        }
        if(!isChamberTextRight){
            gunText.text = "click";
            isChamberTextRight = true;
            return;
        }
    }
    private void SetTextAnimSpeed(){
        AnimationClip clip = textAnimator.runtimeAnimatorController.animationClips
            .First(c => c.name == "ChamberRound");

        textAnimator.speed = clip.length / GunData.timeBetweenShots;
        chamberAnimTime = clip.length * textAnimator.speed;
    }
    private void ResetTextAnimatorSpeed(){
        textAnimator.speed = 1;
    }
    private TMP_Text GetGunText(){
        foreach(TMP_Text text in allGunTexts){
            if(text.gameObject.name == "GunText")
                return text;
        }
        return null;
    }
}
