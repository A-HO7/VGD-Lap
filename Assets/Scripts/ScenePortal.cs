using UnityEngine;

public class ScenePortal : MonoBehaviour
{
    public string sceneToLoad = "VoidCorridor";  
    public float fadeDuration = 1f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            SceneFader fader = FindObjectOfType<SceneFader>();
            fader.FadeAndLoad(sceneToLoad, fadeDuration);
        }
    }
}
