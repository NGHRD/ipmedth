using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AudioTest
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AudioTestWithEnumeratorPasses()
    {
        GameObject gameobjectAudio = new GameObject();
        AudioController audioController = gameobjectAudio.AddComponent<AudioController>();

        audioController.PlayAudioAtTargetIndex(0);

        yield return null;

        Assert.AreEqual(true, audioController.audioSource.isPlaying);
    }
}
