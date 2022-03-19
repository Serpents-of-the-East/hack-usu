using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health { get ; private set; }
    public int maxHealth { get ; private set; }
    public UnityEvent onDeath { get; private set; }
    private bool deathHandled = false;


    public void ModifyHealth(int healthAmount)
    {
        this.health = Mathf.Max(this.health + healthAmount, this.maxHealth);
    }

    public void TakeDamage(int healthAmount)
    {
        this.health -= healthAmount;
    }

    public void Heal(int healthAmount)
    {
        this.health += healthAmount;
    }


    // Start is called before the first frame update
    void Start()
    {
        this.health = this.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0 && !deathHandled)
        {
            onDeath.Invoke();
            deathHandled = true;
        }
    }
}
