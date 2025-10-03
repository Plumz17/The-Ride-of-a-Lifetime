using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterPortraitManager : MonoBehaviour
{
    [SerializeField] GameObject[] characterPortraits;
    

    [Header("Focus Settings")]
    [Tooltip("How Dark the Unfocus Sprite is, between 0 - 1")]
    [SerializeField] float unfocusStrength = 0.7f;
    [SerializeField] float focusStrength = 1.1f;

    private int noOfCharacters; 
    private Dictionary<string, CharacterPortrait> activePortraits;

    void Awake()
    {
        activePortraits = new Dictionary<string, CharacterPortrait>();
    }

    void Start()
    {
        foreach (GameObject i in characterPortraits)
        {
            i.SetActive(false);
        }
    }

    public void LoadPortrait(Sprite characterSprite, string id)
    {
        noOfCharacters++;
        CharacterPortrait currrentPortrait = characterPortraits[noOfCharacters - 1].GetComponent<CharacterPortrait>();
        currrentPortrait.gameObject.SetActive(true);
        currrentPortrait.Unfocus(unfocusStrength);
        currrentPortrait.SetPortrait(characterSprite);
        activePortraits.Add(id, currrentPortrait);
    }

    public void FocusPortrait(Sprite characterSpirte, string id)
    {
        CharacterPortrait currrentPortrait = activePortraits[id].GetComponent<CharacterPortrait>();
        currrentPortrait.Focus(focusStrength);
    }

    public void UnfocusAll()
    {
        foreach (var portrait in activePortraits.Values)
        {
            if (portrait != null && portrait.gameObject.activeSelf)
            {
                portrait.Unfocus(unfocusStrength);
            }
        }
    }
}
