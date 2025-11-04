using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float jumpForce = 9f;
    public float pushForce = 5f;
    [Header("Jump Tuning")]
    public float gravityScale = 2.5f;       // gravity multiplier
    public float fallMultiplier = 3f;       // stronger pull when falling
    public float jumpForwardBoost = 2f;     // horizontal push when jumping
    Rigidbody pushableObject = null;
    bool isGrounded;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    [System.Obsolete]
    void FixedUpdate()
    {
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

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // --- Custom gravity for smoother jumps ---
        if (!isGrounded)
        {
            if (m_Rigidbody.velocity.y > 0)
            {
                // Going up — apply lighter gravity
                m_Rigidbody.AddForce(Vector3.up * Physics.gravity.y * (gravityScale - 1f) * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                // Falling — apply stronger gravity
                m_Rigidbody.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1f) * Time.deltaTime, ForceMode.VelocityChange);
            }

        }
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            m_Animator.applyRootMotion = false;

            // Keep movement direction when jumping
            Vector3 jumpDirection = new Vector3(m_Movement.x, 0f, m_Movement.z).normalized;
            m_Rigidbody.velocity = Vector3.zero;

            // Add both upward and forward force
            Vector3 jumpVector = (Vector3.up * jumpForce) + (jumpDirection * jumpForwardBoost);
            m_Rigidbody.AddForce(jumpVector, ForceMode.Impulse);

            isGrounded = false;
            m_Animator.SetTrigger("Jump");
        }

    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            m_Animator.applyRootMotion = true;
        }

        if (collision.gameObject.CompareTag("Pushable"))
        {
            pushableObject = collision.rigidbody;
            m_Animator.SetBool("isPushing", true);

            // Apply pushing force
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


    void OnAnimatorMove()
    {
        float moveSpeed = 1.5f; // Adjust for your desired walking speed
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * moveSpeed * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

}