using UnityEngine;

public class GuitarController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip song;

    private bool isStrumming = false;
    private float currentSongTime = 0f;

    private void Start()
    {
        audioSource.clip = song;
    }

    private void Update()
    {
        if (isStrumming)
        {
            currentSongTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartStrumming();
    }

    private void OnTriggerExit(Collider other)
    {
        StopStrumming();
    }

    private void StartStrumming()
    {
        if (!isStrumming)
        {
            isStrumming = true;
            audioSource.Play();
        }
    }

    private void StopStrumming()
    {
        if (isStrumming)
        {
            isStrumming = false;
            audioSource.Pause();
            currentSongTime = audioSource.time;
        }
    }
}
