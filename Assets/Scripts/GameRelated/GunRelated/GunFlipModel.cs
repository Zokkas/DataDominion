using UnityEngine;

public class GunFlipModel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gunModel;
    private SpriteRenderer[] allChildSpriteRenderers;
    void Start(){
        allChildSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        gunModel = GetGunModelSR();
    }

    // Update is called once per frame
    void Update(){
        if(gunModel != GetGunModelSR()){
            gunModel = GetGunModelSR();
        }
        //Debug.Log("gunrotation: " + transform.eulerAngles.z);
        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270 && gunModel != null){
            gunModel.flipY = true;
        } else if (transform.eulerAngles.z < 90 || transform.eulerAngles.z > 270 && gunModel != null){
            gunModel.flipY = false;
        }
    }
    SpriteRenderer GetGunModelSR(){
        foreach(SpriteRenderer gunModelSpriteRenderer in allChildSpriteRenderers){
            if(gunModelSpriteRenderer.gameObject.name == "GunSprite"){
                return gunModelSpriteRenderer;
            }
        }
        return null;
    }
}
