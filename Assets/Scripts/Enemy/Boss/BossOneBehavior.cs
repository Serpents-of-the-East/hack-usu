using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BoxCollider2D))]
public class BossOneBehavior : MonoBehaviour
{
    [Header("Movement")]
    public float gravity;
    public float launchMagnitude;
    private Vector2 velocity;
    private bool isGrounded = false;

    [Header("AI")]
    public float maxTimeBeforeJump;
    public float minTimeBeforeJump;
    private bool canJump = true;
    public uint minRocksPerStomp;
    public uint maxRocksPerStomp;
    private bool isActive = false;

    private Health health;

    [Header("Spawned GameObjects")]
    public GameObject boulder;
    public GameObject spawnedRockArea;

    [Header("Collider")]
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (isGrounded && canJump)
        {
            Jump();
        }

        velocity.y -= gravity * Time.deltaTime;

        if (health.health < health.maxHealth / 2)
        {
            maxTimeBeforeJump /= 2;
            minTimeBeforeJump /= 2;
        }

        transform.Translate(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        StartCoroutine(JumpCooldown());
        isGrounded = false;
        Vector2 direction = new Vector2(Random.value * 2 - 1, Random.value).normalized;

        velocity = direction * launchMagnitude;

    }

    private void Stomp()
    {
        int rocksToSpawn = (int)Mathf.Clamp(Random.value * maxRocksPerStomp, minRocksPerStomp, maxRocksPerStomp);

        BoxCollider2D areaCollider = spawnedRockArea.GetComponent<BoxCollider2D>();
        Vector2 size = areaCollider.size / 2;
        Vector2 minValues = new Vector2(spawnedRockArea.transform.position.x, spawnedRockArea.transform.position.y) - size;
        Vector2 maxValues = new Vector2(spawnedRockArea.transform.position.x, spawnedRockArea.transform.position.y) + size;

        for (int i = 0; i < rocksToSpawn; i++)
        {
            GameObject spawnedBoulder = Instantiate(boulder);

            Vector2 boulderPos = new Vector2(Random.Range(minValues.x, maxValues.x), Random.Range(minValues.y, maxValues.y));

            spawnedBoulder.transform.position = boulderPos;
            
        }
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;

        float jumpCooldownTime = Random.value * maxTimeBeforeJump;
        if (jumpCooldownTime < minTimeBeforeJump)
        {
            jumpCooldownTime = minTimeBeforeJump;
        }

        Debug.Log($"Waiting {jumpCooldownTime} seconds to jump again");
        yield return new WaitForSeconds(jumpCooldownTime);
        canJump = true;
    }

    public void AdvancedOnCollisionCustom(Collider2D collider)
    {
        ColliderDistance2D colliderDistance = collider.Distance(boxCollider);

        if (colliderDistance.isOverlapped && !collider.CompareTag("Boss Chamber") && collider.CompareTag("Environment"))
        {
            transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

            if (colliderDistance.pointA.y > colliderDistance.pointB.y)
            {
                if (!isGrounded)
                {
                    Stomp();
                }
                isGrounded = true;
                velocity = Vector2.zero;
            }
            else
            {
                velocity.x *= -1;
            }
        }
    }

    public void OnCustomCollision(GameObject other)
    {
        if (other.CompareTag("Boss Chamber"))
        {
            isActive = true;
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
