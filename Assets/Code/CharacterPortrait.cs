using DG.Tweening;
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
        Color targetColor = new Color(originalColor.r * unfocusStrength,
                                originalColor.g * unfocusStrength,
                                originalColor.b * unfocusStrength);
        Vector3 targetScale = new Vector3(1, 1, 1);

        characterImage.DOColor(targetColor, 0.2f);
        characterImage.rectTransform.DOScale(targetScale, 0.2f);
        Debug.Log("UNFOCUSED!");
    }

    public void Focus(float focusStrength)
    {
        Color targetColor = originalColor;
        Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f);

        characterImage.DOColor(targetColor, 0.2f);
        characterImage.rectTransform.DOScale(targetScale, 0.2f);
    }
}
