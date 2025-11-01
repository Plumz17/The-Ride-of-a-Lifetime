using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    public void OnClick(int buttonIndex)
    {
        dialogueManager.OnChoiceSelected(buttonIndex);
    }
}
