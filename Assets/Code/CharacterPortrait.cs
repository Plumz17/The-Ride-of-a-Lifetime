using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    private Image characterImage;
    private Color originalColor;
    private RectTransform rectTransform;
    private Vector2 originalPos;

    void Awake()
    {
        characterImage = GetComponentInChildren<Image>();
        rectTransform = characterImage.GetComponent<RectTransform>();
        originalColor = characterImage.color;
    }

    void Start()
    {
        StartCoroutine(GrabOriginalPos());
    }

    IEnumerator GrabOriginalPos()
    {
        yield return null; //Wait for one frame
        originalPos = rectTransform.anchoredPosition;
        Debug.Log(originalPos);
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
    }

    public void Focus(float focusStrength)
    {
        Color targetColor = originalColor;
        Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f);

        characterImage.DOColor(targetColor, 0.2f);
        characterImage.rectTransform.DOScale(targetScale, 0.2f);
    }

    public void Ease(float easeDistance, float easeDuration,  float easeDelay, float unfocusStrength)
    {
        Vector2 startPos = originalPos + new Vector2(easeDistance, 0);
        rectTransform.anchoredPosition = startPos;

        Color startColor = characterImage.color;
        startColor.a = 0;
        characterImage.color = startColor;
        Debug.Log(characterImage.color.a);
        Color targetColor = new Color(originalColor.r * unfocusStrength,
                                originalColor.g * unfocusStrength,
                                originalColor.b * unfocusStrength);

        Sequence seq = DOTween.Sequence();
        seq.Append(rectTransform.DOAnchorPos(originalPos, easeDuration));
        //seq.Join(characterImage.DOColor(targetColor, easeDuration)).SetDelay(easeDelay);
    }
}
