using UnityEngine;

public class EosController : MonoBehaviour
{
    public float pulseRadius = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Pulse();
    }

    public void Pulse()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pulseRadius);
        int count = 0;

        foreach (Collider hit in hits)
        {
            RevealByEos r = hit.GetComponent<RevealByEos>();
            if (r != null)
            {
                r.Reveal();
                count++;
            }
        }

        Debug.Log($"Eos pulse sent. Found: {count} revealable objects.");
    }
}
