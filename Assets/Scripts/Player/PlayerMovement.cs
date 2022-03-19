using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public class CollisionEvent : UnityEvent<GameObject> { }

    [Header("Horizontal Movement")]
    public float baseSpeed = 10f;
    public float maxSpeedUpMultiplier = 2f;
    public float minSlowDownMultiplier = 0.5f;
    public float walkAcceleration = 5f;

    private bool hasFreeMovement = false;

    private Vector2 currentInput;
    private Vector2 currentVelocity;

    [Header("Jumping Values")]
    public float timePerJump = 2;
    private float currentJumpTime;
    public float maxJumpSpeed = 20f;
    public float jumpAcceleration = 5f;
    public float initialJumpPercentage;

    private bool isJumping = false;
    private bool shouldJump = false;

    public int maxJumps = 2;
    private int currentJumps;
    public ParticleSystem smokeEffect;

    [Header("Gravity Values")]
    public float gravity = 9.81f;
    public float maxFallSpeed = 10f;

    private bool gravityIsDown = true;
    private bool canFlipGravity = true;
    private bool onGround = false;

    private CapsuleCollider2D capsuleCollider;

    [Header("Delegate Functions")]
    public CollisionEvent onCollisionFunctions;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        currentJumps = maxJumps;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleCollisions();

        if (currentVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (currentVelocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        
    }

    void HandleMovement()
    {
        Vector2 horizontalVelocity = new Vector2(currentVelocity.x, 0);

        Vector2 otherSpeed = baseSpeed * currentInput;

        if (!hasFreeMovement)
        {
            horizontalVelocity = Vector2.MoveTowards(horizontalVelocity, new Vector2(otherSpeed.x + baseSpeed, 0), walkAcceleration * Time.deltaTime);
            if (horizontalVelocity.x < baseSpeed * minSlowDownMultiplier)
            {
                horizontalVelocity.x = baseSpeed * minSlowDownMultiplier;
            }

            if (horizontalVelocity.x > baseSpeed * maxSpeedUpMultiplier)
            {
                horizontalVelocity.x = baseSpeed * maxSpeedUpMultiplier;
            }
        }
        else
        {
            horizontalVelocity = Vector2.MoveTowards(horizontalVelocity, new Vector2(otherSpeed.x * maxSpeedUpMultiplier, 0), walkAcceleration * Time.deltaTime);
        }

        currentVelocity.x = horizontalVelocity.x;

        if (isJumping && shouldJump && currentJumps > 0)
        {
            currentVelocity.y = maxJumpSpeed * (!gravityIsDown ? -1 : 1) * initialJumpPercentage;
            shouldJump = false;
        }
        
        if (isJumping && currentJumpTime > 0)
        {
            currentJumpTime -= Time.deltaTime;
            currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, maxJumpSpeed * (gravityIsDown ? 1 : -1), jumpAcceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity.y -= Time.deltaTime * gravity * (gravityIsDown ? 1 : -1);
            if (Mathf.Abs(currentVelocity.y) > maxFallSpeed)
            {
                currentVelocity.y = maxFallSpeed * (gravityIsDown ? -1 : 1);
            }
        }

        animator.SetFloat("Horizontal", Mathf.Abs(currentVelocity.x));

        transform.Translate(currentVelocity * Time.deltaTime);
    }

    void HandleCollisions()
    {
        Collider2D[] hits = Physics2D.OverlapCapsuleAll(transform.position, capsuleCollider.size, CapsuleDirection2D.Vertical, 0);
        foreach (Collider2D hit in hits)
        {
            if (hit == capsuleCollider)
            {
                continue;
            }

            ColliderDistance2D colliderDistance = hit.Distance(capsuleCollider);

            if (colliderDistance.isOverlapped && hit.CompareTag("Environment"))
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }

            if (hit.CompareTag("Environment"))
            {
                
                if (colliderDistance.pointA.y > colliderDistance.pointB.y)
                {
                    if (!onGround)
                    {
                        CreateSmokeField(hit);
                    }
                    currentJumps = maxJumps;
                    currentVelocity.y = 0;
                    canFlipGravity = true;
                    onGround = true;
                }
            }

            if (hit.CompareTag("Boss Chamber"))
            {
                hasFreeMovement = true;
            }

            onCollisionFunctions.Invoke(hit.gameObject);
        }
    }

    private void CreateSmokeField(Collider2D other)
    {
        ParticleSystem obj = Instantiate(smokeEffect);
        obj.transform.position = other.ClosestPoint(transform.position);
    }

    private void OnMove(InputValue value)
    {
        currentInput = value.Get<Vector2>();
        currentInput.y = 0;
    }

    private void OnGravityFlip(InputValue value)
    {
        float direction = value.Get<float>();

        if (canFlipGravity)
        {
            onGround = false;
            if (direction < 0)
            {
                gravityIsDown = true;
                canFlipGravity = false;
                currentVelocity.y = -maxFallSpeed;
                spriteRenderer.flipY = false;
            }
            else if (direction > 0)
            {
                gravityIsDown = false;
                canFlipGravity = false;
                currentVelocity.y = maxFallSpeed;
                spriteRenderer.flipY = true;

            }
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && currentJumps > 0)
        {
            isJumping = true;
            currentJumpTime = timePerJump;
            currentJumps--;
            shouldJump = true;
            onGround = false;
        }
        else
        {
            isJumping = false;
            shouldJump = false;
            currentJumpTime = 0;
        }
    }

}
