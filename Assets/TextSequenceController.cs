// TextSequenceController.cs

using System.Collections;
using TMPro;
using UnityEngine;

public class TextSequenceController : MonoBehaviour
{
    [System.Serializable]
    public class MediaSequence
    {
        public string text;
        public Material imageMaterial;
        public float transitionDuration = 2.0f;
    }

    public TextMeshPro textMeshPro;
    public MediaSequence[] mediaSequences;
    public GameObject imageCube; // Assign the cube that will display the images
    public Vector3 teleportLocation; // Assign the teleport location in the inspector
    public MoveObjectsOnVideoEnd moveObjectsOnVideoEnd;

    private int currentIndex = 0;
    private bool isSequenceCompleted = false;

    void Start()
    {
        StartCoroutine(SequenceCoroutine());
    }

    IEnumerator SequenceCoroutine()
    {
        while (currentIndex < mediaSequences.Length)
        {
            // Display the current text, set the material to the cube
            MediaSequence currentSequence = mediaSequences[currentIndex];
            textMeshPro.text = currentSequence.text;
            imageCube.GetComponent<Renderer>().material = currentSequence.imageMaterial;

            // Wait for the specified transition duration
            yield return new WaitForSeconds(currentSequence.transitionDuration);

            // Move to the next media sequence
            currentIndex++;
        }

        // All sequences are completed
        isSequenceCompleted = true;

        // Teleport to the specified location
        TeleportToLocation();
    }

    void TeleportToLocation()
    {
        if (isSequenceCompleted && teleportLocation != null)
        {
            // Teleport to the specified location
            moveObjectsOnVideoEnd.MovePlayersToNewPosition(teleportLocation);

            // Start the video in MoveObjectsOnVideoEnd
            StartCoroutine(moveObjectsOnVideoEnd.CheckVideoEnd());
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
