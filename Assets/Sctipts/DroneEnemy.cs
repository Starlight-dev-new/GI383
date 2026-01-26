using UnityEngine;
using System.Collections;

public class DroneEnemy : MonoBehaviour
{
    [Header("Flight Settings")]
    public float hoverHeight = 2f;
    public float extraHoverOffset = 3f; 
    public float moveSpeed = 3f;
    public float smoothTime = 0.3f;
    public float offsetRange = 3f;
    public float driftSpeed = 1f;

    [Header("Manual Bounds")]
    public Transform boundsTransform; 

    [Header("Retreat Settings")]
    public float retreatSpeed = 6f;
    public float retreatDistance = 20f;
    public int shotsBeforeRetreat = 3;

    [Header("Attack Settings")]
    public GameObject laserPrefab;
    public Transform firePoint;
    public float warningDuration = 1.5f;
    public float fireCooldown = 3f;

    [Header("Audio (Optional)")]
    public AudioClip warningSound;
    public AudioClip attackSound;

    [Header("Detection Settings")]
    public float detectionRange = 10f;       
    public float detectionBuffer = 2f;       

    private float attackTimer;
    private bool isWarning;
    private Transform player;

    private LineRenderer lineRenderer;
    private bool playerInRange = false;
    private bool playerDetected = false;     

    private Vector3 velocity = Vector3.zero;
    private float baseXOffset;
    private float baseYOffset;
    private float driftPhase;

    private int shotsFired = 0;
    private bool isRetreating = false;
    private Vector3 retreatDirection;

    void Start()
    {
        baseXOffset = Random.Range(-offsetRange, offsetRange);
        baseYOffset = Random.Range(-offsetRange, offsetRange);
        driftPhase = Random.Range(0f, Mathf.PI * 2f);

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (isRetreating)
        {
            transform.position += retreatDirection * retreatSpeed * Time.deltaTime;

            if (player == null || Vector3.Distance(transform.position, player.position) > retreatDistance)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (player == null)
        {
            BeginRetreat();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!playerDetected && distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else if (playerDetected && distanceToPlayer > detectionRange + detectionBuffer)
        {
            playerDetected = false;
        }
        playerInRange = playerDetected;

        if (playerInRange)
        {
            float driftX = Mathf.Sin(Time.time * driftSpeed + driftPhase);
            float driftY = Mathf.Cos(Time.time * driftSpeed + driftPhase) * 0.5f;

            Vector3 targetPos = new Vector3(
    player.position.x + baseXOffset + driftX,
    player.position.y + hoverHeight + extraHoverOffset + baseYOffset + driftY,
    0f
);

            Vector3 pos = transform.position;
            pos.x = Mathf.MoveTowards(pos.x, targetPos.x, moveSpeed * Time.deltaTime);
            pos.y = targetPos.y;
            transform.position = pos;



            attackTimer += Time.deltaTime;
            if (!isWarning && attackTimer >= fireCooldown)
            {
                StartCoroutine(AttackPattern());
            }
        }
        else
        {
            Vector3 chasePos = new Vector3(player.position.x, player.position.y, 0f);
            transform.position = Vector3.MoveTowards(transform.position, chasePos, moveSpeed * Time.deltaTime);
        }

        ClampHorizontalBounds();
    }

    private void ClampHorizontalBounds()
    {
        if (isRetreating || boundsTransform == null) return;

        Vector3 boundsCenter = boundsTransform.position;
        Vector3 boundsSize = boundsTransform.localScale;

        float minX = boundsCenter.x - boundsSize.x / 2f;
        float maxX = boundsCenter.x + boundsSize.x / 2f;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    private IEnumerator AttackPattern()
    {
        isWarning = true;
        lineRenderer.enabled = true;

        if (warningSound != null)
            AudioSource.PlayClipAtPoint(warningSound, transform.position);

        float elapsed = 0f;
        Vector3 lockedTarget = player != null ? player.position : firePoint.position;

        
        while (elapsed < warningDuration)
        {
            elapsed += Time.deltaTime;

            if (player != null)
                lockedTarget = player.position; 

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, lockedTarget);

            
            Vector3 aimDir = (lockedTarget - firePoint.position).normalized;
            float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        lineRenderer.enabled = false;

        
        if (laserPrefab != null)
        {
            Vector3 direction = (lockedTarget - firePoint.position).normalized;
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            laser.GetComponent<LaserProjectile>().SetDirection(direction);

            if (attackSound != null)
                AudioSource.PlayClipAtPoint(attackSound, transform.position);

            shotsFired++;
            if (shotsFired >= shotsBeforeRetreat)
            {
                BeginRetreat();
            }
        }

        attackTimer = 0f;
        isWarning = false;
    }

    private void BeginRetreat()
    {
        isRetreating = true;
        retreatDirection = (transform.position - (player != null ? player.position : Vector3.zero)).normalized;
        if (retreatDirection == Vector3.zero)
        {
            retreatDirection = Vector3.up;
        }
    }
}
