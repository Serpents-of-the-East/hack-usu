using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public UnityEvent onDeath;
    private bool deathHandled = false;

    public float immunityTime = 1f;
    private bool canTakeDamage = true;

    private IEnumerator ImmunityPhase()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(immunityTime);
        canTakeDamage = true;
    }


    public void ModifyHealth(int healthAmount)
    {
        this.health = Mathf.Max(this.health + healthAmount, this.maxHealth);
    }

    public void TakeDamage(int healthAmount)
    {
        if (canTakeDamage)
        {
            StartCoroutine(ImmunityPhase());
            this.health -= healthAmount;

        }
        
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
            deathHandled = true;
            onDeath.Invoke();
        }
    }

    
}
