using UnityEngine;
using TMPro;
using System.Collections;

public class InteractiveMineral : MonoBehaviour
{
    public string actionMessage = "Pulsa F para minar";
    public TextMeshProUGUI messageText;
    [SerializeField] private AudioClip miningClip;
    [SerializeField] private AudioSource miningAudioSource;
    private bool isPlayerInRange = false;
    private bool isMining = false;
    private Coroutine miningCoroutine;

    private int maxExtractions = 5;
    private int currentExtractions = 0;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!isMining)
                StartMining();
            else
                StopMining();
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
            StopMining(); // Detener minería si se aleja del trigger
            ShowMessage(false);
        }
    }

    void ShowMessage(bool show)
    {
        if (messageText != null)
        {
            messageText.text = show ? actionMessage : "";
            messageText.gameObject.SetActive(show);
            Debug.Log(show ? actionMessage : "Mensaje oculto");
        }
    }

    void StartMining()
    {
        isMining = true;
        if (miningAudioSource != null && miningClip != null)
        {
            miningAudioSource.clip = miningClip;
            miningAudioSource.loop = true;
            miningAudioSource.Play();
        }
        Debug.Log("Minando: " + gameObject.tag);
        messageText.text = "Minando... (pulsa F para cancelar)";
        miningCoroutine = StartCoroutine(MiningRoutine());
    }

    void StopMining()
    {

        isMining = false;
        if (miningAudioSource != null && miningAudioSource.isPlaying)
            miningAudioSource.Stop();
        if (miningCoroutine != null)
            StopCoroutine(miningCoroutine);

        Debug.Log("Minería cancelada");
        messageText.text = isPlayerInRange ? actionMessage : "";
        messageText.gameObject.SetActive(isPlayerInRange);
    }

    IEnumerator MiningRoutine()
    {
        while (currentExtractions < maxExtractions)
        {
            yield return new WaitForSeconds(3f);

            int amount = GetRandomAmount(gameObject.tag);
            ItemType type;

            if (System.Enum.TryParse(gameObject.tag, out type))
            {
                for (int i = 0; i < amount; i++)
                    Inventory.Instance.AddItem(new Item(type));

                currentExtractions++;
                Debug.Log($"Extracción {currentExtractions}/{maxExtractions}: +{amount} {type}");

                if (currentExtractions >= maxExtractions)
                {
                    Debug.Log("Veta agotada");
                    messageText.text = "Veta agotada";
                    messageText.gameObject.SetActive(true); // Por si estaba oculto

                    if (miningAudioSource != null && miningAudioSource.isPlaying)
                        miningAudioSource.Stop();

                    isMining = false;
                    isPlayerInRange = false;
                    GetComponent<Collider>().enabled = false;

                    // Esperar 2 segundos antes de destruir el objeto
                    yield return new WaitForSeconds(2f);

                    Destroy(gameObject);
                    yield break;
                }
            }
            else
            {
                Debug.LogWarning("Tag del mineral no reconocido: " + gameObject.tag);
                StopMining();
                yield break;
            }
        }
    }

    int GetRandomAmount(string tag)
    {
        switch (tag)
        {
            case "Coal": return Random.Range(3, 6);      // 3-5
            case "Copper": return Random.Range(2, 5);    // 2-4
            case "Iron": return Random.Range(1, 4);      // 1-3
            case "Titanium": return Random.Range(1, 3);  // 1-2
            default: return 1;
        }
    }
}

