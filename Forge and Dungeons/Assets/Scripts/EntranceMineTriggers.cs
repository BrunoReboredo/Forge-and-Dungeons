using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EntranceMineTriggers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] KeyCode interactionKey = KeyCode.Return;

    private bool playerInside = false;
    private string sceneToLoad = "";

    void Start()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("Door"))
            {
                sceneToLoad = "TitleScreen";
            }
            else if (CompareTag("Ladder"))
            {
                sceneToLoad = "DemoLevel";
            }

            playerInside = true;

            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(interactionKey) && !string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}