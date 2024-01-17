using NUnit.Framework;
using UnityEngine;

public class NewTestScript
{  
    [Test]
    public void NewTestScriptSimplePasses()
    {

        GameObject gameobjectPlaying = new GameObject();
        MoveObjectsToGameEnd moveObjectsToGameEnd = gameobjectPlaying.AddComponent<MoveObjectsToGameEnd>();
        moveObjectsToGameEnd.audioSource = gameobjectPlaying.AddComponent<AudioSource>();

        moveObjectsToGameEnd.MovePlayersToNewPosition(gameobjectPlaying);

        Assert.AreEqual(false, moveObjectsToGameEnd.audioSource.isPlaying);
    }
}
