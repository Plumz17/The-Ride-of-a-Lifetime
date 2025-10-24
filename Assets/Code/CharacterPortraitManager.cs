using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using Ink.Parsed;
using UnityEditor.SceneManagement;
using System.Linq;
using System.Collections;


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
        if (activePortraits.ContainsKey(id)) //Temporary fix
            return;
        noOfCharacters++;
        CharacterPortrait currrentPortrait = characterPortraits[noOfCharacters - 1].GetComponent<CharacterPortrait>();
        currrentPortrait.gameObject.SetActive(true);
        currrentPortrait.LoadSprite(characterSprite);
        currrentPortrait.EaseIn(easeDistance, easeDuration, easeDelay, unfocusStrength);
        activePortraits.Add(id, currrentPortrait);
    }

    public void UnloadPortrait(string id)
    {
        StartCoroutine(UnloadPortraitCoroutine(id));
    }

    private IEnumerator UnloadPortraitCoroutine(string id)
    {   
        noOfCharacters--;
        CharacterPortrait currentPortrait = activePortraits[id].GetComponent<CharacterPortrait>();
        activePortraits.Remove(id);
        currentPortrait.EaseOut(easeDistance, easeDuration, easeDelay);
        yield return new WaitForSeconds(easeDuration + easeDelay);
        currentPortrait.gameObject.SetActive(false);
        currentPortrait.UnloadSprite();
    }

    // public void UnloadAllPortrait()
    // {
    //     foreach (GameObject cp in characterPortraits)
    //     {
    //         CharacterPortrait currentCP = cp.GetComponent<CharacterPortrait>();
    //         currentCP.EaseOut(easeDistance, easeDuration, easeDelay);
    //     }
    //     noOfCharacters = 0;
    //     activePortraits.Clear();
    // }

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
