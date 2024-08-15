using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayTime = 0.1f;

    public void PlayAudioWithDelay(float customDelay)
    {
        customDelay = delayTime;
        if (audioSource != null)
        {
            audioSource.PlayDelayed(customDelay);
        }
    }
}