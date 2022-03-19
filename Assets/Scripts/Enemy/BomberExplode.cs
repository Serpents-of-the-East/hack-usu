using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public int damage;

    public void Explode()
    {
        autoTargetting targeting = gameObject.GetComponent<autoTargetting>();
        targeting.hasReachedTarget = true;
        animator.SetBool("isExploded", true);
        
    }

    public void Explode(GameObject other)
    {
        if (!other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Slow") || other.CompareTag("Spell"))
            {
                Destroy(other.gameObject);
            }
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Explode();
            
        }
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }

}
