using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serpentSegment : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject snakeEntire;
    void Start()
    {
        
    }

    public void AlertSnakeOfDamage()
    {
        snakeEntire.GetComponent<Health>().TakeDamage(1);
    }


    // Update is called once per frame
    void Update()
    {
        


    }
}
