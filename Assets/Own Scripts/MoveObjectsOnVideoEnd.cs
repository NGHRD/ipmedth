using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MoveObjectsOnVideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject cameraLight;
    public GameObject roomPosition; // Set the new position for the players

    private bool videoPlaying = false;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
            cameraLight.SetActive(false);
        }
    }

    public void PlayVideoAndMovePlayers()
    {
        StartCoroutine(CheckVideoEnd());
    }

    public IEnumerator CheckVideoEnd()
    {
        videoPlaying = true;

        videoPlayer.Play();
        cameraLight.SetActive(true);

        // Wait until the video has ended
        yield return new WaitUntil(() => !videoPlayer.isPlaying);

        // Video has ended
        Debug.Log("Video has ended");

        MovePlayersToNewPosition(roomPosition);

        videoPlaying = false;

        cameraLight.SetActive(false);
    }

    public bool IsVideoPlaying()
    {
        return videoPlaying;
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
        videoPlaying = false;
        cameraLight.SetActive(false);
    }

    public void MovePlayersToNewPosition(GameObject newPosition)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            player.transform.position = newPosition.transform.position;
        }
    }
}
