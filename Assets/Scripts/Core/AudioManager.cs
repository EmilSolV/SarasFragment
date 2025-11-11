using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("UI SFX")]
    public AudioClip clickUI;

    [Header("Door SFX")]
    public AudioClip doorOpenSound;
    public AudioClip doorLockedSound;
    public AudioClip doorUnlockedSound;

    [Header("Grabbable SFX")]
    public AudioClip grabSound;
    public AudioClip hitFloorSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.pitch = Random.Range(0.95f, 1.05f); // Variación natural opcional
        sfxSource.PlayOneShot(clip, volume);
    }
}