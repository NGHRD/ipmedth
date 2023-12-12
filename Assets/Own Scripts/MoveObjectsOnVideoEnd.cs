using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MoveObjectsOnVideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject cameraLight;
    public Vector3 newPosition; // Set the new position for the players

    private bool videoEnded = false;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        StartCoroutine(CheckVideoEnd());
    }

    IEnumerator CheckVideoEnd()
    {
        while (true)
        {
            if (videoPlayer.isPlaying)
            {

                yield return null;
            }
            else
            {
                // Video has ended
                Debug.Log("naar andere positie");
                MovePlayersToNewPosition();
                cameraLight.SetActive(false);
                
                yield break;
            }
        }
    }

    void MovePlayersToNewPosition()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            player.transform.position = newPosition;
        }
    }
}
