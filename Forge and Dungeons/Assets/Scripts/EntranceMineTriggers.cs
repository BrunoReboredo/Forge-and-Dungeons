using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EntranceMineTriggers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI promptText; // Texto que se muestra al jugador
    [SerializeField] KeyCode interactionKey = KeyCode.Return;

    private bool playerInside = false;
    private string sceneToLoad = "";

    void Start()
    {
        // Asegúrate de ocultar el texto al inicio
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
                promptText.text = "Pulsa ENTER para volver al menú";
            }
            else if (CompareTag("Ladder"))
            {
                sceneToLoad = "DemoLevel";
                promptText.text = "Pulsa ENTER para entrar a la mina";
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