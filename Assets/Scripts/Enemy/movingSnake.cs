using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingSnake : MonoBehaviour
{
    public float speed;
    public GameObject deathAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
    }

    public void Die()
    {
        GameObject obj = Instantiate(deathAnimation);

        obj.transform.position = transform.position;

        Destroy(gameObject);

        
        
    }


}
