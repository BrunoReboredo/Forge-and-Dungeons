using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GeneralAudioManager : MonoBehaviour
{
    public static GeneralAudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Cargar volumen guardado o usar valor por defecto
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        // Slider va de 0 a 1. El volumen en decibelios va de -80 a 0.
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1)) * 20;
        audioMixer.SetFloat("Master", dB);

        // Guardar el valor para la próxima vez
        PlayerPrefs.SetFloat("Volume", volume);
    }

    [SerializeField] private AudioMixerGroup masterMixerGroup;

    public AudioMixerGroup GetMixerGroup()
    {
        return masterMixerGroup;
    }

}