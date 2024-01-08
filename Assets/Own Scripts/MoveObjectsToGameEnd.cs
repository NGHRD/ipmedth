using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MoveObjectsToGameEnd : MonoBehaviour
{
    public GameObject position;

    void Start()
    {
        MovePlayersToNewPosition(position);
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
