using UnityEngine;

public class LowGravityZone : MonoBehaviour
{
    public float gravityScale = 0.3f;
    private float normalGravity;

    private void Start()
    {
        // Store Unity’s default gravity
        normalGravity = Physics.gravity.y;
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply lower gravity inside the zone
            Physics.gravity = new Vector3(0, normalGravity * gravityScale, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Restore normal gravity when leaving
            Physics.gravity = new Vector3(0, normalGravity, 0);
        }
    }
}
