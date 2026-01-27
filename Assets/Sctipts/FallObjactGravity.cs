using UnityEngine;

public class FallObjactGravity : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Vector2 moveDirec = Vector2.down;
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   void Update()
    {
        rb.linearVelocity = moveDirec * speed;
    }
    
    void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("UpperCamera")&& GameManager.instance.gravity)
        {
            int rng = Random.Range(0,2);
            if(rng == 0 )
            {
                moveDirec = Vector2.left;
            }
            if( rng == 1)
            {
                moveDirec = Vector2.right;
            }
        }
        Destroy(this.gameObject,4);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

    }
}
