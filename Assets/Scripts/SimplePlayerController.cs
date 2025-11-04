using UnityEngine;
public class SimplePlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Animator anim;
    private Rigidbody rb;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(h, 0f, v).normalized;
        if (movement.magnitude > 0)
        {
            anim.SetBool("PlayerAnimator", true);
            rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
            transform.forward = movement;
        }
        else
        {
            anim.SetBool("PlayerAnimator", false);
        }
    }
}