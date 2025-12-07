using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    public float gravityMultiplier = 1.5f; // NEW

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        Vector3 v = rb.linearVelocity;
        rb.linearVelocity = new Vector3(h * moveSpeed, v.y, 0f);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        // EXTRA GRAVITY
        rb.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
    }
}
