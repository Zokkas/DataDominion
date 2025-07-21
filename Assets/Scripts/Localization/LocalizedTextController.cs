using TMPro;
using UnityEngine;
public class LocalizedTextController : MonoBehaviour
{
    [SerializeField] private string localizationKey;

    private TMP_Text textComponent;

    void Awake(){
        textComponent = GetComponent<TMP_Text>();
    }

    void OnEnable(){
        LocalizationLoader.OnLanguageChanged += UpdateText;
        UpdateText();
    }

    void OnDisable(){
        LocalizationLoader.OnLanguageChanged -= UpdateText;
    }

    private void UpdateText(){
        if (LocalizationLoader.Instance != null){
            textComponent.text = LocalizationLoader.Instance.GetLocalizedValue(localizationKey);
        }
    }
    // Used for dynamic text changes, like switching to a different character and displaying the new info.
    public void SetLocalizationKeyAndUpdate(string newKey){
        localizationKey = newKey;
        UpdateText();
    }
}
