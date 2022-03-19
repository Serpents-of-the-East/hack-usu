using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickerTorch : MonoBehaviour
{

    public Light2D torch;
    public float maxIntensity = 3;
    public int flickerRepeatLength = 5;
    public float flickerProbability = 0.4f;
    private int flickerIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (flickerIndex % flickerRepeatLength == 0)
        {
            float randomNumber = Random.Range(0.0f, 1.0f);

            if (randomNumber < flickerProbability)
            {
                torch.intensity = Random.Range(1.0f, maxIntensity);
            }
        }

        flickerIndex = (flickerIndex + 1) % flickerRepeatLength;



    }
}
