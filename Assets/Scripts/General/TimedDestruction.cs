using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float TimeToDestroy;

    private void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }
}
