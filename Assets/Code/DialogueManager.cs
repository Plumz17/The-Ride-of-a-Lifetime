using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEditor.IMGUI.Controls;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Current Dialogue")]
    [SerializeField] private TextAsset currentInkFile;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text characterNameText;

    [Header("Dialogue Settings")]
    [SerializeField] private float dialogueSpeed = 0.05f;

    private InputActions inputActions;

    private Story story;
    private List<string> tags;

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

    void Start()
    {
        story = new Story(currentInkFile.text);
        tags = new List<string>();
    }

    private void OnEPressed(InputAction.CallbackContext context)
    {
        HandleDialogue();
    }

    private void HandleDialogue()
    {
        if (story.canContinue)
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
        Debug.Log("Dialogue Ended!");
    }

    private void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        HandleTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    private void HandleTags()
    {
        tags = story.currentTags;
        foreach (string s in tags)
        {
            Debug.Log(s);
            string tagName = s.Split(":")[0];
            string tagParam = s.Split(":")[1];

            switch (tagName.ToLower())
            {
                case "name":
                    characterNameText.text = tagParam;
                    break;
                default:
                    Debug.Log("Tag Not Found 404");
                    break;
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
}

