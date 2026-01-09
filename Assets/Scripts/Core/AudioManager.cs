using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPop()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
