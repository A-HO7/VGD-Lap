using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;   // Player to follow
    public float smoothSpeed = 0.125f;  // Lerp smoothing
    public Vector3 offset = new Vector3(0f, 2f, -10f);  // Camera offset

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
