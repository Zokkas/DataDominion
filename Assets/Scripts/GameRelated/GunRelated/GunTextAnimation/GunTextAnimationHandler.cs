using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
public class GunTextAnimationHandler : MonoBehaviour
{
    private TMP_Text text;
    private int reloadStep = 0;
    private bool isChamberTextRight = false; 
    void Start(){
        text = GetComponent<TMP_Text>();
    }
    public void ResetText(){
        text.text = "";
    }
    public void ChangeReloadingText(){
        //Debug.Log("ChangeReloadingText called");
        if(reloadStep >= 3)
            reloadStep = -1;
        reloadStep++;
        //Debug.Log("ReloadStep: " + reloadStep);
        text.text = "Reloading" + new string('.', reloadStep);
    }
    // Used on sniper weapons
    public void ChamberText(){
        if(isChamberTextRight){
            text.text = "clack";
            isChamberTextRight = false;
            return;
        }
        if(!isChamberTextRight){
            text.text = "click";
            isChamberTextRight = true;
            return;
        }
    }
}
