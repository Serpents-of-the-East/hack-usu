using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;


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
            Explode();
        }
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }

}
