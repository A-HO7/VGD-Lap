using UnityEngine;

public class RevealByEos : MonoBehaviour
{
    public float revealDuration = 2f;
    public bool isRevealed = false;

    private Renderer rend;
    private Collider col;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        // start hidden
        SetVisible(false);
    }

    public void Reveal()
    {
        if (isRevealed) return;
        StartCoroutine(DoReveal());
    }

    private System.Collections.IEnumerator DoReveal()
    {
        isRevealed = true;
        SetVisible(true);

        yield return new WaitForSeconds(revealDuration);

        SetVisible(false);
        isRevealed = false;
    }

    private void SetVisible(bool v)
    {
        // 1. Control the visual mesh (what you see)
        if (rend) rend.enabled = v;

        // 2. Control the physical collision (what you hit)
        // Assuming 'col' is your primary, non-trigger Collider.
        if (col && !col.isTrigger)
        {
            col.enabled = v; // Disable the physical collider when hidden (v=false)
        }
    }
}
