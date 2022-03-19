using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    public void DestroyTheParent()
    {
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
