using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitManager : MonoBehaviour
{
    [SerializeField] GameObject[] characterPortraits;

    [Header("Focus Settings")]
    [Tooltip("How Dark the Unfocus Sprite is, between 0 - 1")]
    [SerializeField] float unfocusStrength = 0.7f;

    private int noOfCharacters = 0;

    void Start()
    {
        foreach (GameObject i in characterPortraits)
        {
            i.SetActive(false);
        }
    }

    public void LoadPortrait(Sprite characterSprite)
    {
        CharacterPortrait currrentPortrait = characterPortraits[noOfCharacters].GetComponent<CharacterPortrait>();
        currrentPortrait.gameObject.SetActive(true);
        currrentPortrait.Unfocus(unfocusStrength);
        currrentPortrait.SetPortrait(characterSprite);
        noOfCharacters++;
    }
}
