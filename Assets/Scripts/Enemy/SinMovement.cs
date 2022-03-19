using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : MonoBehaviour
{

    public Vector2 currentVelocity;
    public float verticalPosition;
    public float amplitude;
    public float rotationScale;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = new Vector2();
        transform.localScale = new Vector3(1, -1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        verticalPosition = Mathf.Cos(Mathf.PI * Time.time + transform.localPosition.x) * amplitude;

        Vector2 currentlyFacing = new Vector2(Mathf.Abs(Mathf.Cos(Mathf.PI * Time.time + transform.localPosition.x)), Mathf.Sin(Mathf.PI * Time.time + transform.localPosition.x));
        transform.localPosition = new Vector3(transform.localPosition.x, verticalPosition, transform.localPosition.z);

        float rotation = Mathf.Rad2Deg * Mathf.Atan2(currentlyFacing.y, currentlyFacing.x);
        transform.rotation = Quaternion.Euler(0, 0, rotation * rotationScale + 180);
        
    }
}
