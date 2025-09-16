using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Current Dialogue")]
    [SerializeField] private TextAsset currentInkFile;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text characterNameText;

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
        Debug.Log("E");
    }
}

