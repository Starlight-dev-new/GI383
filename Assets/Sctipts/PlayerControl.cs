using NUnit.Framework;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float autoUpSpeed = 2.5f; 
    [SerializeField] float sideForce = 4f;


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
        Jump();
    }

    void Update()
    {
        if (!GameManager.instance.isdead) rb.linearVelocity = new Vector2(rb.linearVelocity.x, autoUpSpeed);
        if (Input.GetMouseButtonDown(0)&& canJump && !GameManager.instance.isdead || Input.GetKeyDown(KeyCode.Space) && canJump && !GameManager.instance.isdead)
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            GameManager.instance.isdead = true;
        }
    }
}
