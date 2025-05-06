using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TitleMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip titleMusicClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = titleMusicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Asegúrate de que el AudioSource esté en el grupo correcto del AudioMixer
        audioSource.outputAudioMixerGroup = GeneralAudioManager.Instance.GetMixerGroup();

        audioSource.Play();
    }
}
