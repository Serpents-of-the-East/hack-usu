using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoTargetting : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float delay;
    private bool hasCalculatedTrajectory = false;
    public bool hasReachedTarget = false;
    private Vector2 direction;
    private Vector2 slope;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (delay <= 0f && !hasCalculatedTrajectory)
        {
            slope = (target.transform.position - transform.position).normalized;
            direction = slope;
            hasCalculatedTrajectory = true;


        }
        else if (delay > 0f && !hasCalculatedTrajectory)
        {
            delay -= Time.deltaTime;
        }
        else if (!hasReachedTarget)
        {
            transform.position = new Vector3(transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime, 0);
            float rotation = Mathf.Rad2Deg * Mathf.Atan2(slope.y, slope.x);
            transform.rotation = Quaternion.Euler(0, 180, 180 - rotation);

        }
   



    }
}
