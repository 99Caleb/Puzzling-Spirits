using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] jumpSounds;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomJumpSound()
    {
        if (jumpSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, jumpSounds.Length);
            audioSource.PlayOneShot(jumpSounds[randomIndex]);
        }
    }
}