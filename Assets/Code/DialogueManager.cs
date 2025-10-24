using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEditor.IMGUI.Controls;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Current Dialogue")]
    private TextAsset currentInkFile;

    [Header("Dialogue UI")]
    [SerializeField] private CanvasGroup dialoguePanelGroup;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text characterNameText;
    [SerializeField] private Image backgroundImage;
    private Vector2 originalPanelPos;

    [Header("Easing Settings")]
    [SerializeField] private float easeDistance = 1200;
    [SerializeField] private float easeDuration = 1.5f;
    [SerializeField] private Ease easeType = Ease.InQuint;

    [Header("Dialogue Settings")]
    [SerializeField] private float dialogueSpeed = 0.05f;

    [Header("Character Manager")]
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private CharacterPortraitManager characterPortraitManager;

    private InputActions inputActions;

    private Story story;
    private List<string> tags;
    private bool isTyping = false;
    private Coroutine TypingCoroutine;
    private string currentSpeaker = "";

    void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Interact.started += OnEPressed;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.started -= OnEPressed;
        inputActions.Player.Disable();
    }

    public void LoadCutscene(Cutscene currentCutscene) // Plays in VNManager OnSceneLoaded
    {
        currentInkFile = currentCutscene.inkFile;
        backgroundImage.sprite = currentCutscene.backgroundImage;
        StartCoroutine(StartAfterDelay(currentCutscene.introDelay));
    }

    IEnumerator StartAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SetupDialogue();
        HandleDialogue();
    }

    private void SetupDialogue()
    {
        story = new Story(currentInkFile.text);
        tags = new List<string>();
        EaseInDialoguePanel();
    }

    private void EaseInDialoguePanel()
    {
        RectTransform panelTransform = dialoguePanelGroup.GetComponent<RectTransform>();
        if (originalPanelPos == Vector2.zero) //From ChatGPT lol
            originalPanelPos = panelTransform.anchoredPosition;
        Vector2 hiddenPos = new Vector2(originalPanelPos.x, originalPanelPos.y - easeDistance);
        panelTransform.anchoredPosition = hiddenPos;
        dialoguePanelGroup.alpha = 1;
        panelTransform.DOAnchorPos(originalPanelPos, easeDuration).SetEase(easeType);
    }

    private void OnEPressed(InputAction.CallbackContext context)
    {
        HandleDialogue();
    }

    private void HandleDialogue()
    {
        if (story == null)
        {
            Debug.LogWarning("HandleDialogue called before story was initialized!");
            return;
        }
        
        if (isTyping)
        {
            CompleteSentence();
            return;
        }

        if (story.canContinue && !isTyping)
        {
            AdvanceDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        //characterPortraitManager.UnloadAllPortrait();
        SceneManager.LoadScene("Game");
    }

    private void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        HandleTags();
        TypingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    private void HandleTags()
    {
        tags = story.currentTags;
        foreach (string s in tags)
        {
            string tagName = s.Split(":")[0];
            string tagParam = s.Split(":")[1];
            Character currentCharacter = characterManager.GetCharacterData(tagParam);
            bool isMC = tagParam == "mc";

            switch (tagName.ToLower())
            {
                case "show":
                    Debug.Log(tagParam + " showed!");
                    if (!isMC)
                    {
                        characterPortraitManager.LoadPortrait(currentCharacter.characterImage, tagParam);
                    }
                    break;
                case "speak":
                    bool isSameSpeaker = tagParam == currentSpeaker;
                    if (isSameSpeaker) //To prevent focus from being triggered at the same char
                        break;
                    if (!string.IsNullOrEmpty(currentSpeaker)) //To Prevent Easing to be disturbed by unfocusing
                        characterPortraitManager.UnfocusAll();
                    if (!isMC) //MC doesn't have a portrait
                        characterPortraitManager.FocusPortrait(currentCharacter.characterImage, tagParam);
                    currentSpeaker = tagParam;
                    characterNameText.text = currentCharacter.characterName;
                    break;
                case "leave":
                    characterPortraitManager.UnloadPortrait(tagParam);
                    break;

                default:
                    Debug.Log("Tag Not Found 404");
                    break;
            }
        }
    }

    private void CompleteSentence() //Instantly Complete Sentence
    {
        StopCoroutine(TypingCoroutine);
        isTyping = false;
        dialogueText.maxVisibleCharacters = int.MaxValue;
    }

    IEnumerator TypeSentence(string sentence) //Type each character
    {
        isTyping = true;
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.text = sentence;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(dialogueSpeed);
        }
        CompleteSentence();
    }
}

