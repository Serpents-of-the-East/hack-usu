using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("General")]
    public Transform playerTransform;

    public float cameraHorizontalOffset;
    private Camera m_camera;

    [Header("Boss Chamber")]
    public Transform bossChamberCameraPosition;
    public float speedToBossChamber = 5;
    public float bossChamberFOV;
    public float fovChangeSpeed;
    private bool inBossChamber = false;
    


    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inBossChamber)
        {
            transform.position = new Vector3(playerTransform.position.x + cameraHorizontalOffset, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(bossChamberCameraPosition.position.x, bossChamberCameraPosition.position.y, transform.position.z), speedToBossChamber * Time.deltaTime);
            m_camera.orthographicSize = Mathf.MoveTowards(m_camera.orthographicSize, bossChamberFOV, fovChangeSpeed * Time.deltaTime);
        }
    }

    public void SetInBossChamber(GameObject other)
    {
        if (other.CompareTag("Boss Chamber"))
        {
            inBossChamber = true;
        }
    }
}
