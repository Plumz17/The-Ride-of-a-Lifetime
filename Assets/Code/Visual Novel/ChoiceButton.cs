using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private float highlightScale = 1.15f;
    [SerializeField] private float highlightSpeed = 0.15f;
    [SerializeField] private Ease highlightEase;
    private Vector2 originalScale;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();   
    }

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnClick(int buttonIndex)
    {
        dialogueManager.OnChoiceSelected(buttonIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * highlightScale, highlightSpeed).SetEase(highlightEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, highlightSpeed).SetEase(highlightEase);
    }
}
