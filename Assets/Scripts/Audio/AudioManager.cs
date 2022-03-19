using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip defaultMusic;
    public AudioClip bossFightMusic;

    private bool isPlayingBossMusic;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(defaultMusic);
        audioSource.loop = true;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(isPlayingBossMusic ? bossFightMusic : defaultMusic);
        }
    }

    public void OnCustomCollision(GameObject other)
    {
        if (other.CompareTag("Boss Chamber") && !isPlayingBossMusic)
        {
            audioSource.Stop();
            isPlayingBossMusic = true;
            audioSource.PlayOneShot(bossFightMusic);
        }
    }
}
