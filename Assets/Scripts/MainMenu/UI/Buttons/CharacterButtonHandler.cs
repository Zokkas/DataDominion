using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonHandler : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    private Image buttonSprite;
    private GameObject characterSelectedGO;
    private TMP_Text characterNameText;
    private TMP_Text characterIntoText;
    private Image characterImageComponnent;

    void Awake(){
        characterSelectedGO = GameObject.FindGameObjectsWithTag("CharacterSelectedGO").First();
        // Gets the (first) Childs Image, not "this" one
        buttonSprite = GetComponentsInChildren<Image>()
                        .FirstOrDefault(img => img.gameObject != this.gameObject);
        buttonSprite.sprite = characterData.CharacterSprite;
        GetAllComponents();
    }
    void GetAllComponents(){
        foreach(TMP_Text text in characterSelectedGO.GetComponentsInChildren<TMP_Text>()){
            if(text.gameObject.name == "CharacterNameText"){
                characterNameText = text;
            }
            if(text.gameObject.name == "CharacterInfoText"){
                characterIntoText = text;
            }
        }
        foreach(Image image in characterSelectedGO.GetComponentsInChildren<Image>()){
            if(image.gameObject.name == "CharacterSprite"){
                characterImageComponnent = image;
            }
        }        
    }
    public void ChangeCharacter(){
        characterNameText.GetComponent<LocalizedTextController>().SetLocalizationKeyAndUpdate(characterData.NameTextKey);
        characterIntoText.GetComponent<LocalizedTextController>().SetLocalizationKeyAndUpdate(characterData.InfoTextKey);
        characterImageComponnent.sprite = characterData.CharacterSprite;
    }
}
