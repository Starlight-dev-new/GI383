using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float autoUpSpeed = 2.5f; 
    public float sideForce = 4f;

    Rigidbody rb;
    SpriteRenderer srPlayer;
    private bool jumpToRight = true;
    private bool canJump = true;
    private bool flip = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        srPlayer = GetComponent<SpriteRenderer>();
        if (transform.position.x < 0)
        {
            jumpToRight = false;
        }
        else
        {
            jumpToRight = true;
        }
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, autoUpSpeed);
        if (Input.GetMouseButtonDown(0)&& canJump|| Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            Jump();
        }
        if (transform.position.x < 0 && !flip)
        {
            Flip();
        }
        else if (transform.position.x > 0 && flip)
        {
            Flip();
        }
    }

    void Jump()
    {
        rb.linearVelocity = Vector2.zero;

        float dir = jumpToRight ? 1f : -1f;
        rb.AddForce(new Vector2(dir * sideForce,0), ForceMode.Impulse);

        jumpToRight = !jumpToRight;
    }
    void Flip()
    {
        flip = !flip;
        srPlayer.flipY = flip;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canJump = true;
        }
        
    }
}
