using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDamage : MonoBehaviour
{
    // Start is called before the first frame update

    public float fallSpeed;

    public int damage;

    private bool canDamage = true;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    public void OnCustomCollision(GameObject other)
    {
        if (other.CompareTag("Player") && canDamage)
        {
            Health otherHealth = other.GetComponent<Health>();

            if (otherHealth != null)
            {
                otherHealth.TakeDamage(damage);
                canDamage = false;
            }
            else
            {
                Debug.LogError($"{other.name} was marked as a player but is missing a health component!");
            }
        }
        else if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
