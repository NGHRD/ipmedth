using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    // Reference to the script containing the sequence coroutine
    public TextSequenceController sequenceController;
    public string TagPlayer = "";

    // Delay before starting the sequence coroutine
    public float delayBeforeStart = 0.5f;

    private bool sequenceStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!sequenceStarted && other.CompareTag(TagPlayer))
        {
            Debug.Log("Trigger entered!");
            // Start the coroutine after a delay
            StartSequenceCoroutine();
        }
    }


    void StartSequenceCoroutine()
    {
        Debug.Log("Starting sequence coroutine!");
        // Trigger the sequence coroutine
        sequenceController.StartCoroutine(sequenceController.SequenceCoroutine());

        // Set the flag to prevent starting the sequence multiple times
        sequenceStarted = true;
        gameObject.SetActive(false);
    }
}
