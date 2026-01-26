using Mono.Cecil;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Vector3 moveDirection;

    [Header("Lifetime Settings")]
    [SerializeField] float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        { 
            Destroy(gameObject);
        }
    }
}
