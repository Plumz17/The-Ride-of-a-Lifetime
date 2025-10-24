using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using Ink.Parsed;
using UnityEditor.SceneManagement;


public class CharacterPortraitManager : MonoBehaviour
{
    [SerializeField] GameObject[] characterPortraits;
    

    [Header("Focus Settings")]
    [Tooltip("How Dark the Unfocus Sprite is, between 0 - 1")]
    [SerializeField] float unfocusStrength = 0.7f;
    [SerializeField] float focusStrength = 1.1f;

    [Header("Easing Settings")]
    [SerializeField] private float easeDistance = 100;
    [SerializeField] private float easeDuration = 1.5f;
    [SerializeField] private float easeDelay = 1f;

    private int noOfCharacters; 
    private Dictionary<string, CharacterPortrait> activePortraits;

    void Awake()
    {
        Debug.Log(noOfCharacters);
        activePortraits = new Dictionary<string, CharacterPortrait>();
        noOfCharacters = 0;
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
        currrentPortrait.SetPortrait(characterSprite);
        currrentPortrait.Ease(easeDistance, easeDuration, easeDelay, unfocusStrength);
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
