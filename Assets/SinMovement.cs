using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{

    public Vector2 currentVelocity;

    public float amplitude;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalPosition = Mathf.Cos(Mathf.PI * Time.time + transform.localPosition.x) * amplitude;

        Vector2 currentlyFacing = new Vector2(Mathf.Abs(Mathf.Cos(Mathf.PI * Time.time + transform.localPosition.x)), Mathf.Sin(Mathf.PI * Time.time + transform.localPosition.x));
       /* float rotation = new Vector2();*/

        transform.localPosition = new Vector3(transform.localPosition.x, verticalPosition, transform.localPosition.z);
        transform.rotation = Quaternion.Euler(currentlyFacing);
    }
}
