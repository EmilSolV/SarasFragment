using System.Collections.Generic;
using UnityEngine;

public class AudioRoomZone : MonoBehaviour
{
    public List<AudioSource> roomAudioSources = new List<AudioSource>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var src in roomAudioSources)
                src.mute = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var src in roomAudioSources)
                src.mute = true;
        }
    }
}
