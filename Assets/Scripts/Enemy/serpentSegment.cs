using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serpentSegment : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snakeEntire;

    public int damageDealt = 1;

    void Start()
    {
        
    }

    public void AlertSnakeOfDamage()
    {
        snakeEntire.GetComponent<Health>().TakeDamage(1);
    }

    public void DealPain(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageDealt);
            }
            else
            {
                Debug.LogError($"Jeff is a bad boy. Snakey snake did a bad thing to {other.name}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
