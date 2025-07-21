using UnityEngine;

public class OptionsSwitcher : MonoBehaviour
{
    [SerializeField] GameObject AudioTab;
    [SerializeField] GameObject VideoTab;
    [SerializeField] GameObject ControlsTab;

    private void CloseAllTabs(){
        AudioTab.SetActive(false);
        VideoTab.SetActive(false);
        ControlsTab.SetActive(false);
    }
    public void AudioButtonClick(){
        CloseAllTabs();
        AudioTab.SetActive(true);
    }
     public void VideoButtonClick(){
        CloseAllTabs();
        VideoTab.SetActive(true);
    }
     public void ControlsButtonClick(){
        CloseAllTabs();
        ControlsTab.SetActive(true);
    }
}
