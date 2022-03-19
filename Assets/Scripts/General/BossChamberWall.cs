using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChamberWall : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnCustomCollision(GameObject other)
    {
        
        if (other.CompareTag("Boss Chamber"))
        {
            gameObject.SetActive(true);
        }
    }
}
