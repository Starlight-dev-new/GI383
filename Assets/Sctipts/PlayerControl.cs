using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float autoUpSpeed = 2.5f; 
    public float sideForce = 4f;

    Rigidbody rb;
    bool jumpToRight = true;
    bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, autoUpSpeed);
        if (Input.GetMouseButtonDown(0)&& canJump|| Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;

        float dir = jumpToRight ? 1f : -1f;
        rb.AddForce(new Vector2(dir * sideForce,0), ForceMode.Impulse);

        jumpToRight = !jumpToRight;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canJump = true;
        }
        
    }
}
