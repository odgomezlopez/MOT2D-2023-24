using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(EnemyController))]
public class EnemyPatrol : MonoBehaviour
{
    // Cached references
    private EnemyStats stats;
    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private int currentDestination = 0;
    [SerializeField] private List<Vector3> patrolDestinations;
    [SerializeField] private bool inverseFlip = false;

    private float waitTime = 1f;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Start()
    {
        InitializeComponents();
        InitializePatrolRoute();
        if (patrolDestinations.Count > 0)
            MoveToNextDestination();
    }

    void Update()
    {
        if (isWaiting)
        {
            if (waitTimer < waitTime)
            {
                waitTimer += Time.deltaTime;
            }
            else
            {
                isWaiting = false;
                waitTimer = 0f;
                MoveToNextDestination();
            }
        }
        else
        {
            Vector3 targetPosition = patrolDestinations[currentDestination];
            if (Vector3.Distance(transform.position, targetPosition) < 1f)
            {
                rb.linearVelocity = Vector2.zero;
                isWaiting = true;
            }
            else
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                rb.linearVelocity = direction * stats.movementSpeed;
            }
        }

        Flip();
        UpdateAnimatorParameters();
    }

    private void InitializeComponents()
    {
        stats = (EnemyStats)GetComponent<EnemyController>().GetStats();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void InitializePatrolRoute()
    {
        patrolDestinations = new List<Vector3> { transform.position };
        Transform patrolRoute = transform.Find("Patrol");
        foreach (Transform child in patrolRoute)
        {
            patrolDestinations.Add(child.position);
        }
    }

    private void MoveToNextDestination()
    {
        currentDestination = (currentDestination + 1) % patrolDestinations.Count;
    }

    private void Flip()
    {
        if (rb.linearVelocity.x != 0)
        {
            float xScale = Mathf.Abs(sprite.transform.localScale.x) * (rb.linearVelocity.x > 0 ? 1 : -1);
            if (inverseFlip) xScale = -xScale;
            sprite.transform.localScale = new Vector3(xScale, sprite.transform.localScale.y, sprite.transform.localScale.z);
        }
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("velocityY", rb.linearVelocity.y);
        // Assume Utils.IsGrounded2D is efficient, otherwise consider caching its result if it involves heavy calculations
        animator.SetBool("isGrounded", Utils.Utils.IsGrounded2D(gameObject, 0.5f));
    }
}
