using UnityEngine;
using TMPro;

public class InteractiveMineral : MonoBehaviour
{
    public string actionMessage = "Pulsa F para minar";
    public TextMeshProUGUI messageText; // Asigna esto desde el inspector

    private bool isPlayerInRange = false;
    private bool isMining = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!isMining)
            {
                StartMining();
            }
            else
            {
                StopMining();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!isMining)
                ShowMessage(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ShowMessage(false);
            if (isMining)
                StopMining(); // Detener si se aleja mientras mina
        }
    }

    void ShowMessage(bool show)
    {
        if (messageText != null)
        {
            if (show)
            {
                messageText.text = actionMessage;
                messageText.gameObject.SetActive(true);
                Debug.Log(actionMessage);
            }
            else
            {
                messageText.text = "";
                messageText.gameObject.SetActive(false);
                Debug.Log("Mensaje oculto");
            }
        }
    }

    void StartMining()
    {
        isMining = true;
        Debug.Log("Interacción ejecutada: minando mineral");
        if (messageText != null)
            messageText.text = "Minando... (pulsa F para cancelar)";
    }

    void StopMining()
    {
        isMining = false;
        Debug.Log("Minería cancelada");
        if (isPlayerInRange && messageText != null)
        {
            messageText.text = actionMessage;
        }
        else if (messageText != null)
        {
            messageText.text = "";
            messageText.gameObject.SetActive(false);
        }
    }
}
