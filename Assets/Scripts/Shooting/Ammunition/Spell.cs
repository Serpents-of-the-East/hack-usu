using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;
    public float speed;
    public bool isPlayerSpell;
    public float lifeTime;

    public float speedReductionPercentage = 0.5f;

    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;


    }

    public void OnCustomCollision(GameObject other)
    {
        if (other.CompareTag("Enemy") && isPlayerSpell)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log("Got here");
                if (gameObject.tag == "Slow")
                {
                    WalkingEnemy enemy = other.GetComponent<WalkingEnemy>();
                    if (enemy != null)
                    {
                        enemy.speedMod = speedReductionPercentage;
                    }
                }
                health.TakeDamage(damage);
                
                
                Animator animator = other.GetComponent<Animator>();
                
                if (animator != null && health.health > 0)
                {
                    animator.SetBool("WasHurt", true);
                }
            }
            else
            {
                Debug.LogError($"Found enemy {other.name} which was marked as enemy, but missing a health component");
            }
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && !isPlayerSpell)
        {
            
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
