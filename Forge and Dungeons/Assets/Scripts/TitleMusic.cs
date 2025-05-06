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

        audioSource.outputAudioMixerGroup = GeneralAudioManager.Instance.GetMixerGroup();

        audioSource.Play();
    }
}
