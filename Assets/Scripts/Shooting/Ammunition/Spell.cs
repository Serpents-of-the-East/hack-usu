using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;
    public float speed;
    public bool isPlayerBullet;
    public float lifeTime;

    private CircleCollider2D circleCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        if (!isPlayerBullet)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, LayerMask.GetMask("Player"));
            foreach (Collider2D hit in hits)
            {
                if (hit == circleCollider)
                    continue;

                Debug.Log("Bullet hit player");
                // Do something when player gets hit
                Destroy(gameObject);

            }
        }
        else // this is enemy bullet
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, LayerMask.GetMask("Enemy"));
            foreach (Collider2D hit in hits)
            {
                if (hit == circleCollider)
                    continue;

                // Do something when enemy gets hit
                Destroy(gameObject);



            }
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
