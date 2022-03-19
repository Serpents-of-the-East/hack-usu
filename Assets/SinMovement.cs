using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{

    public Vector2 speed;

    public Vector2 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector2();
        currentVelocity.x = speed.x;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalVelocity = Mathf.Cos(Mathf.PI * Time.time);
       /* float rotation = new Vector2();*/

        currentVelocity.y = verticalVelocity;

        currentVelocity.y *= speed.y;

        transform.Translate(currentVelocity * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0, 10f * Time.time));
    }
}
