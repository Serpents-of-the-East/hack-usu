using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : MonoBehaviour
{

    [System.Serializable]
    public class CollisionEvent : UnityEvent<GameObject> { }

    [Header("Delegate Functions")]
    public CollisionEvent onCollisionFunctions;

    private enum ColliderType
    {
        Capsule,
        Box,
        Circle,
        Missing
    }

    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;

    Collider2D ownCollider;


    ColliderType colliderType;

    // Start is called before the first frame update
    void Start()
    {
        ownCollider = GetComponent<Collider2D>();
        if (ownCollider.GetType() == typeof(CapsuleCollider2D))
        {
            capsuleCollider = (CapsuleCollider2D)ownCollider;
            colliderType = ColliderType.Capsule;
        }
        else if (ownCollider.GetType() == typeof(BoxCollider2D))
        {
            boxCollider = (BoxCollider2D)ownCollider;
            colliderType = ColliderType.Box;
        }
        else if (ownCollider.GetType() == typeof(CircleCollider2D))
        {
            circleCollider = (CircleCollider2D)ownCollider;
            colliderType = ColliderType.Circle;
        }
        else
        {
            colliderType = ColliderType.Missing;
        }
    }

    private void FixedUpdate()
    {
        HandleCollisions();
    }

    void HandleCollisions()
    {

        Collider2D[] hits;

        switch (colliderType)
        {
            case (ColliderType.Box):
                hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
                break;
            case (ColliderType.Capsule):
                hits = Physics2D.OverlapCapsuleAll(transform.position, capsuleCollider.size, CapsuleDirection2D.Vertical, 0);
                break;
            case (ColliderType.Circle):
                hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);
                break;
            default:
                Debug.LogError($"No collider was assigned to {gameObject.name}'s collision handler... You should do that.");
                return;
        }

        foreach (Collider2D hit in hits)
        {
            
            if (hit == ownCollider)
            {
                continue;
            }

            onCollisionFunctions.Invoke(hit.gameObject);
        }
    }
}
