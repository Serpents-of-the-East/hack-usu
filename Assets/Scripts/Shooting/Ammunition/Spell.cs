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
                
                if (gameObject.name == "FireBall(Clone)")
                {
                    health.TakeDamage(damage);
                }
                else
                {
                    WalkingEnemy enemy = other.GetComponent<WalkingEnemy>();
                    enemy.speedMod = speedReductionPercentage;
                    health.TakeDamage(damage);
                }
                
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
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
