using UnityEngine;
using TMPro;

public class UIOrbCounter : MonoBehaviour
{
    public static UIOrbCounter Instance;

    public TextMeshProUGUI orbText;
    int count = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // KEEP UI across all scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddOrb(int amount = 1)
    {
        count += amount;
        UpdateText();
    }

    void UpdateText()
    {
        orbText.text = "Orbs: " + count;
    }
}
