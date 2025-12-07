using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneFader : MonoBehaviour
{
    public Image img;
    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void FadeAndLoad(string sceneName, float duration)
    {
        StartCoroutine(Fader(sceneName, duration));
    }
    IEnumerator Fader(string sceneName, float duration)
    {
        float elapsedTime = 0f;
        Color c = img.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            c.a = (elapsedTime / duration);
            img.color = c;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color c = img.color;
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            c.a = 1f - (elapsedTime/1f) ;
            img.color = c;
            yield return null;
        }
    }
    private void Update()
    {
        
    }
}

