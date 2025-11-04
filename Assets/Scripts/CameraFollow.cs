using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Player or camera target
    public Vector3 offset = new Vector3(0f, 2f, -8f);
    public float followSpeed = 5f;
    public float panAmount = 2f;      // How far the camera pans
    public float panSmooth = 3f;      // How smooth the panning is
    private float smoothHorizontalInput = 0f;
    private float smoothVerticalInput = 0f;
    public float inputSmoothSpeed = 5f;
    private Vector3 currentPanOffset = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return;

        float targetHorizontal = 0f;
        float targetVertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) targetHorizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) targetHorizontal = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) targetVertical = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) targetVertical = -1f;

        // Smoothly interpolate the input over time
        smoothHorizontalInput = Mathf.Lerp(smoothHorizontalInput, targetHorizontal, Time.deltaTime * inputSmoothSpeed);
        smoothVerticalInput = Mathf.Lerp(smoothVerticalInput, targetVertical, Time.deltaTime * inputSmoothSpeed);

        Vector3 desiredPan = new Vector3(smoothHorizontalInput * panAmount, smoothVerticalInput * (panAmount * 0.5f), 0f);
        currentPanOffset = Vector3.Lerp(currentPanOffset, desiredPan, Time.deltaTime * panSmooth);

        // --- SMOOTH FOLLOW ---
        Vector3 desiredPosition = target.position + offset + currentPanOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        // --- FIXED CAMERA ANGLE FOR 2.5D ---
        transform.rotation = Quaternion.Euler(15f, 0f, 0f);
        float tiltAngle = smoothHorizontalInput * 5f; // how much the camera tilts
        transform.rotation = Quaternion.Euler(15f, tiltAngle, 0f);

    }
}
