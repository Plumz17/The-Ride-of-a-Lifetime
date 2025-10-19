using UnityEngine;
using UnityEngine.SceneManagement;

public class VNManager : MonoBehaviour
{
    public static VNManager Instance { get; private set; }
    [SerializeField] Cutscene currentCutscene;
    private DialogueManager dialogueManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void FindManager()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
        if (dialogueManager)
        {
            Debug.Log("Dialogue Manager Found!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "VN")
        {
            FindManager();
            dialogueManager.LoadCutscene(currentCutscene);
        }
    } 
}
