using UnityEngine;

public class DriftRubSFX : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private PrometeoCarController carController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];

        carController = FindFirstObjectByType<PrometeoCarController>();
        carController.OnDrifting += PlayRandomSqueeekSFX;
        carController.OnDriftEnd += StopSFX;

    }

    public void PlayRandomSqueeekSFX()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }
    }

    private void StopSFX()
    {
        audioSource.Stop();
    }   
}
