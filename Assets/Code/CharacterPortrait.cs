using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    private Image characterImage;
    private Color originalColor;
    
    void Awake()
    {
        characterImage = GetComponent<Image>();
        originalColor = characterImage.color; 
    }

    public void SetPortrait(Sprite characterSprite)
    {
        characterImage.sprite = characterSprite;
    }

    public void Unfocus(float unfocusStrength)
    {
        Color c = originalColor;
        c.r *= unfocusStrength;
        c.g *= unfocusStrength;
        c.b *= unfocusStrength;

        characterImage.color = new Color(c.r, c.g, c.b, 1f);
        characterImage.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public void Focus(float focusStrength)
    {
        characterImage.rectTransform.localScale *= focusStrength;
        Color c = characterImage.color;
        c.r *= 255;
        c.g *= 255;
        c.b *= 255;

        characterImage.color = new Color(c.r, c.g, c.b, 1f);
    }
}
