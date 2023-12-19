using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Radio_oranje : MonoBehaviour
{
    private AudioSource audioSource;
    private bool alreadyPlayed = false;
    public AudioClip clip;
    private bool hasSwitchedToNextTarget = false;
    public SpotlightController spotlightController;
    public GameObject radioPlek;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public void playRadioOranje()
    {
        Debug.Log("radio wordt afgespeeld");
        if (!alreadyPlayed)
        {
            audioSource.Play();
            StartCoroutine(WaitForClipEnd());
            alreadyPlayed = true;
        }
    }

    IEnumerator WaitForClipEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        if (!hasSwitchedToNextTarget)
        {
            spotlightController.SwitchToNextTarget();
            hasSwitchedToNextTarget = true;
        }
    }
}
