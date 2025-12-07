using UnityEngine;
using TMPro;
using System.Collections;

public class SceneIntroDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 5)]
    public string introMessage = "Lumen... This place feels unstable. Proceed carefully and collect the orb.";

    public float delay = 0.5f;

    private void Start()
    {
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        yield return new WaitForSeconds(delay);
        dialoguePanel.SetActive(true);
        dialogueText.text = introMessage;
        yield return new WaitForSeconds(3); // stays 3 seconds
        dialoguePanel.SetActive(false);
    }
}
