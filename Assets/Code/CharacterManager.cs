using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Character[] availableCharacters; //Just for adding
    private Dictionary<string, Character> availableCharactersDict; //Actually used in the Editor

    void Awake()
    {
        SetupCharacters();
    }

    private void SetupCharacters() {
        availableCharactersDict = new Dictionary<string, Character>();
        foreach (Character c in availableCharacters)
        {
            availableCharactersDict.Add(c.characterName.ToLower(), c);
            Debug.Log(c.characterName.ToLower());
        }
    }

    public Character GetCharacterData(string id)
    {
        return availableCharactersDict[id];
    }
}
