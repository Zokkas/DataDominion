using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public Sprite CharacterSprite;
    public string InfoTextKey;
    public string NameTextKey;
}
