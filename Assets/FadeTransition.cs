using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    private float opacity;
    private int maxOpacity = 255;
    private bool increasing = true;
    public TMPro.TextMeshProUGUI enterText;
    public TMPro.TextMeshProUGUI exitText;

    // Start is called before the first frame update
    void Start()
    {
        opacity = 0;
    }

    // Update is called once per frame
    void Update()
    {

        opacity += .01f;
        enterText.faceColor = new Color(173f, 51f, 49f, 255 * Mathf.Abs(Mathf.Sin(opacity * Time.deltaTime)));
        exitText.faceColor = new Color(255f, 131f, 0f, 255 * Mathf.Abs(Mathf.Sin(opacity * Time.deltaTime)));

    }
}
