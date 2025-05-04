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

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CompareTag("door"))
            {
                sceneToLoad = "MainMenu";
                promptText.text = "Pulsa ENTER para volver al menú";
            }
            else if (CompareTag("ladder"))
            {
                sceneToLoad = "Nivel1";
                promptText.text = "Pulsa ENTER para entrar a la mina";
            }

            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
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