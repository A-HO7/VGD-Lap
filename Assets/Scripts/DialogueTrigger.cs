using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    [TextArea(3, 5)]
    public string message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            dialogueText.text = message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(false);
        }
    }
}
