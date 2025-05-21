using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip coinSound;
    void Start()
    {
        PlayBackgroundMusic();
    }
    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpSound);
    }
    public void PlayCoinSound()
    {
        effectAudioSource.PlayOneShot(coinSound);
    }
    public void PlayBackgroundMusic()
    {
        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.Play();
    }
}
