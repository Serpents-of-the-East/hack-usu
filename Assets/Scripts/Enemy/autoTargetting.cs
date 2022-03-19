using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoTargetting : MonoBehaviour
{
    private GameObject target;
    private Vector2 acceleration;
    public float accelerationConstant;
    private Vector2 velocity;
    public bool hasReachedTarget = false;
    public float sightRange;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        velocity = new Vector2();
    }

    void Update()
    {
        if ((target.transform.position - transform.position).magnitude <= sightRange && !hasReachedTarget)
        {
            acceleration = (target.transform.position - transform.position).normalized;
            velocity = velocity + acceleration * accelerationConstant * Time.deltaTime;
            transform.position = new Vector2(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime);

            if (transform.position.x > target.transform.position.x)
            {
                float rotation = Mathf.Rad2Deg * Mathf.Atan2(acceleration.y, acceleration.x);
                transform.rotation = Quaternion.Euler(0, 180, 180 - rotation);

            }
        }
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
