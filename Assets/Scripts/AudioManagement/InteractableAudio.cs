using UnityEngine;

public class InteractableAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundToPlay;

    [Header("Opciones")]
    public bool playOnStart = false;
    public bool loop = false;

    //USO
    //GetComponent<InteractableAudio>()?.PlayInteractionSound();
    void Start()
    {
        if (playOnStart)
        {
            if (loop)
            {
                audioSource.loop = true;
                audioSource.clip = soundToPlay;
                audioSource.Play();
            }
            else
            {
                PlayInteractionSound();
            }
        }
    }

    public void PlayInteractionSound()
    {
        if (audioSource && soundToPlay)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }
}
