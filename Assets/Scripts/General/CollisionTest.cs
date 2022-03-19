using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    Color color = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCustomCollision(GameObject other)
    {
        Debug.Log($"Collided with {other.name}");
        color = Color.red;
    }

    private void FixedUpdate()
    {
        color = Color.green;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 2);
    }
}
