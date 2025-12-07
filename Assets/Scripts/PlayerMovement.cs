using UnityEngine;
using System; // Required for [Obsolete]

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed and Force")]
    public float moveSpeed = 5f;        // Base movement speed for walking/running
    public float turnSpeed = 20f;
    public float jumpForce = 9f;
    public float pushForce = 5f;

    [Header("Jump Tuning")]
    public float gravityScale = 2.5f;     // Gravity multiplier when moving up
    public float fallMultiplier = 3f;     // Stronger pull when falling
    // jumpForwardBoost is now optional/minimal since animation handles major distance
    public float jumpForwardBoost = 0.5f;

    [Header("Internal State")]
    public bool isGrounded;
    private float gravityMultiplier = 1f; // Used for external triggers

    // Components
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    // Movement Calculation Variables
    private Vector3 m_Movement;
    private Quaternion m_Rotation = Quaternion.identity;
    private Rigidbody pushableObject = null;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        // Crucial: Freeze rotation so external forces don't tip the character over.
        m_Rigidbody.freezeRotation = true;
    }

    [Obsolete]
    void FixedUpdate()
    {
        // 1. INPUT HANDLING
        float horizontal = 0f;
        float vertical = 0f;

        // --- Player input using only WASD ---
        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.W)) vertical = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        // 2. ROTATION CALCULATION
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // 3. MOVEMENT APPLICATION (Only horizontal velocity when grounded)
        if (isGrounded)
        {
            if (isWalking)
            {
                // Set horizontal velocity directly for immediate response
                Vector3 worldMovement = m_Movement * moveSpeed;
                m_Rigidbody.linearVelocity = new Vector3(worldMovement.x, m_Rigidbody.linearVelocity.y, worldMovement.z);
            }
            else
            {
                // Stop horizontal movement when input stops
                m_Rigidbody.linearVelocity = new Vector3(0, m_Rigidbody.linearVelocity.y, 0);
            }
        }

        // 4. CUSTOM GRAVITY
        if (!isGrounded)
        {
            float gravityForce = Physics.gravity.y * gravityMultiplier;

            if (m_Rigidbody.linearVelocity.y > 0)
            {
                // Going up: Apply customized ascending gravity
                m_Rigidbody.AddForce(Vector3.up * gravityForce * (gravityScale - 1f) * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                // Falling: Apply stronger descending gravity
                m_Rigidbody.AddForce(Vector3.up * gravityForce * (fallMultiplier - 1f) * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    [Obsolete]
    void Update()
    {
        // JUMP INPUT
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // FIX: Enable Root Motion to capture the animation's forward movement data
            m_Animator.applyRootMotion = true;

            // Clear vertical velocity before jump impulse
            m_Rigidbody.linearVelocity = new Vector3(m_Rigidbody.linearVelocity.x, 0, m_Rigidbody.linearVelocity.z);

            // Apply upward force and a small forward boost (if needed)
            Vector3 jumpDirection = transform.forward;
            Vector3 jumpVector = (Vector3.up * jumpForce) + (jumpDirection * jumpForwardBoost);

            // Apply force immediately
            m_Rigidbody.AddForce(jumpVector, ForceMode.Impulse);

            isGrounded = false;
            m_Animator.SetTrigger("Jump");
        }
    }

    // --- ROOT MOTION HANDLER FIX ---

    void OnAnimatorMove()
    {
        // Always apply rotation smoothing as calculated in FixedUpdate.
        m_Rigidbody.MoveRotation(m_Rotation);

        // If the jump animation is running, force the Rigidbody to adopt the animation's forward displacement.
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {

            m_Rigidbody.position += m_Animator.deltaPosition;
        }
        // Note: You must set the "Jump" tag on the jump state in your Animator Controller!
    }

    // --- UTILITY FUNCTIONS ---

    public void SetGravityMultiplier(float value, float upwardBoost)
    {
        gravityMultiplier = value;
        m_Rigidbody.linearVelocity += Vector3.up * upwardBoost;
    }

    public void ResetGravity()
    {
        gravityMultiplier = 1f;
    }

    // --- COLLISION HANDLERS ---

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // Always enable Root Motion when grounded to allow OnAnimatorMove to work correctly
            m_Animator.applyRootMotion = true;
        }

        if (collision.gameObject.CompareTag("Pushable"))
        {
            pushableObject = collision.rigidbody;
            m_Animator.SetBool("isPushing", true);

            Vector3 pushDir = collision.transform.position - transform.position;
            pushDir.y = 0f;
            pushableObject.AddForce(pushDir.normalized * pushForce, ForceMode.Force);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pushable"))
        {
            pushableObject = null;
            m_Animator.SetBool("isPushing", false);
        }
    }

    // --- TRIGGER HANDLERS (Gravity Zone) ---

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.SetGravityMultiplier(gravityScale, 0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.ResetGravity();
        }
    }
}