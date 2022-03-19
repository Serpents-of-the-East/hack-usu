using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public float speed = 0.5f;
    public int damage = 2;
    public Animator animator;
    public float hitDistance = 2f;

    public float speedMod = 1f;

    private float originalSpeedMod;
    public float slowTimer;

    private void Start()
    {
        originalSpeedMod = speedMod;
    }

    public void Die()
    {
        speed = 0f;
        animator.SetBool("IsDead", true);
        
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
        
    

    private void Update()
    {
        transform.Translate(-speed * Time.deltaTime * speedMod, 0, 0);
        if (speedMod != 1f)
        {
            StartCoroutine(SpeedReductionTimer());
        }
    }

    IEnumerator SpeedReductionTimer() 
    {
        yield return new WaitForSeconds(slowTimer);
        speedMod = originalSpeedMod;
            
        
    }

    public void OnCustomCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Attack", true);
        }
    }

    public void StopAnimation(string animation)
    {
        animator.SetBool(animation, false);
    }

    public void HurtPlayer()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, new Vector2(-1, 0), hitDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Health health = hit.collider.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                else
                {
                    Debug.LogError($"{hit.collider.gameObject.name} was a player, but it had no health component");
                }
            }
        }
        
    }


}
