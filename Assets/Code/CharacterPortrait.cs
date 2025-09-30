using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    private Image characterImage;
    void Awake()
    {
        characterImage = GetComponent<Image>();
    }

    public void SetPortrait(Sprite characterSprite)
    {
        characterImage.sprite = characterSprite;
    }

    public void Unfocus(float unfocusStrength)
    {
        Color c = characterImage.color;
        c.r *= unfocusStrength;
        c.g *= unfocusStrength;
        c.b *= unfocusStrength;

        characterImage.color = new Color(c.r, c.g, c.b, 1f); 
    }
}
