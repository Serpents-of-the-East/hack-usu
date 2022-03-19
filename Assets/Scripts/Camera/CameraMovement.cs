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
    private bool isMovingToCenter;

    [Header("Boss Chamber")]
    public Transform bossChamberCameraPosition;
    public float speedToBossChamber = 5;
    public float bossChamberFOV;
    public float fovChangeSpeed;
    private bool inBossChamber = false;

    [Header("Camera Shake")]
    public float firstShakeAmplitude;
    public float maxShakeAmplitude = 2;
    public float shakeDuration;
    private bool canShake = true;


    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    public void ShakeCamera(GameObject other)
    {
        if (other.tag == "Environment" && canShake)
        {
            StartCoroutine(ShakethThyCamera());
        }
    }

    private IEnumerator ShakethThyCamera()
    {
        canShake = false;
        Vector3 originalPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * firstShakeAmplitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * firstShakeAmplitude;

            transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;

        firstShakeAmplitude = maxShakeAmplitude;

        canShake = true;
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
            if (isMovingToCenter)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(bossChamberCameraPosition.position.x, bossChamberCameraPosition.position.y, transform.position.z), speedToBossChamber * Time.deltaTime);
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(bossChamberCameraPosition.position.x, bossChamberCameraPosition.position.y)) < 0.05)
                {
                    Debug.Log("Camera Has finished");
                    isMovingToCenter = false;
                }
            }
            m_camera.orthographicSize = Mathf.MoveTowards(m_camera.orthographicSize, bossChamberFOV, fovChangeSpeed * Time.deltaTime);

        }
    }

    public void SetInBossChamber(GameObject other)
    {
        if (other.CompareTag("Boss Chamber"))
        {
            if (!inBossChamber)
            {
                isMovingToCenter = true;
            }
            inBossChamber = true;
        }
    }
}
