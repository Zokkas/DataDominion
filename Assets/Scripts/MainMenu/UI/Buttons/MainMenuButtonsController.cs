using UnityEngine;

public class MainMenuButtonsController : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject CreditsMenu;
    [SerializeField] GameObject XButton;

    private void CloseAllTabs(){
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }
    public void OptionsButtonClick(){
        CloseAllTabs();
        XButton.SetActive(true);
        OptionsMenu.SetActive(true);
    }
    public void CreditsButtonClick(){
        CloseAllTabs();
        XButton.SetActive(true);
        CreditsMenu.SetActive(true);
    }
    public void XButtonClick(){
        CloseAllTabs();
        XButton.SetActive(false);
        MainMenu.SetActive(true);
    }

}
