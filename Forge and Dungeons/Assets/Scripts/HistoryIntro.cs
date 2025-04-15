using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class HistoryIntro : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoUI;
    [SerializeField] string[] textos;
    [SerializeField] private float velocidadEscritura = 0.05f;
    [SerializeField] private AudioClip sonidoLetra; // sonido para cada letra
    [SerializeField] private AudioClip sonidoEnter; // Sonido al pulsar Enter
    private int indiceActual = 0;
    private bool escribiendo = false;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound
        audioSource.volume = 0.5f;

        StartCoroutine(EscribirTexto(textos[indiceActual]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ReproducirSonidoEnter();

            if (escribiendo)
            {
                StopAllCoroutines();
                textoUI.text = textos[indiceActual];
                escribiendo = false;
            }
            else
            {
                SiguienteTexto();
            }
        }
    }

    void ReproducirSonidoEnter()
    {
        if (sonidoEnter != null)
        {
            audioSource.PlayOneShot(sonidoEnter);
        }
    }

    void SiguienteTexto()
    {
        indiceActual++;

        if (indiceActual < textos.Length)
        {
            StartCoroutine(EscribirTexto(textos[indiceActual]));
        }
        else
        {
            SceneManager.LoadScene("TestScene");
        }
    }

    IEnumerator EscribirTexto(string texto)
    {
        escribiendo = true;
        textoUI.text = "";

        foreach (char letra in texto)
        {
            textoUI.text += letra;

            if (sonidoLetra != null)
            {
                audioSource.PlayOneShot(sonidoLetra);
            }

            yield return new WaitForSeconds(velocidadEscritura);
        }

        escribiendo = false;
    }
}