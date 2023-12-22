using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextSequenceController : MonoBehaviour
{
    [System.Serializable]
    public class MediaSequence
    {
        public string text;
        public Material[] imageMaterials;
        public AudioClip audioClip;
    }

    public TextMeshPro textMeshPro;
    public MediaSequence[] mediaSequences;
    public GameObject[] imageCubes;
    public GameObject teleportLocation;
    public MoveObjectsOnVideoEnd moveObjectsOnVideoEnd;
    public AudioSource audioSource;

    private int currentIndex = 0;
    private bool isSequenceCompleted = false;

    private Renderer[] cubeRenderers;

    void Start()
    {
        cubeRenderers = new Renderer[imageCubes.Length];
        for (int i = 0; i < imageCubes.Length; i++)
        {
            cubeRenderers[i] = imageCubes[i].GetComponent<Renderer>();
        }
    }

    public async Task SequenceCoroutineAsync()
    {
        while (currentIndex < mediaSequences.Length)
        {
            MediaSequence currentSequence = mediaSequences[currentIndex];
            textMeshPro.text = currentSequence.text;

            for (int i = 0; i < Mathf.Min(imageCubes.Length, currentSequence.imageMaterials.Length); i++)
            {
                cubeRenderers[i].material = currentSequence.imageMaterials[i];
            }

            if (!audioSource.isPlaying)
            {
                audioSource.clip = currentSequence.audioClip;
                audioSource.Play();
                Debug.Log("Playing audio: " + currentSequence.audioClip.name);
            }

            await WaitWhile(() => audioSource.isPlaying);

            currentIndex++;
            Debug.Log("Advancing to the next index: " + currentIndex);
        }

        isSequenceCompleted = true;
        Debug.Log("All sequences completed.");
        TeleportToLocation();
    }

    async Task WaitWhile(Func<bool> condition)
    {
        while (condition.Invoke())
        {
            await Task.Yield();
        }
    }

    void TeleportToLocation()
    {
        if (isSequenceCompleted && teleportLocation != null)
        {
            moveObjectsOnVideoEnd.MovePlayersToNewPosition(teleportLocation);
            StartCoroutine(moveObjectsOnVideoEnd.CheckVideoEnd());
            Debug.Log("Teleporting to location: " + teleportLocation);
        }
        else if (!isSequenceCompleted)
        {
            Debug.LogError("Teleport attempted before completing all sequences!");
        }
        else
        {
            Debug.LogError("Teleport location not assigned!");
        }
    }
}
