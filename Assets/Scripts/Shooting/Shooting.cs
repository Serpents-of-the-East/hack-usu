using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class Shooting : MonoBehaviour
{
    [Header("Shooting Options")]    
    public GameObject fireBall;
    public GameObject iceBolt;
    public float roundsPerMinute;
    private float shotCoolDown;



    private bool isShooting;
    private float currentShotCooldown = 0;

    private Vector2 currentAimDirection;
    private Vector3 mousePos;

    private Vector2 lookDelta;
    private Vector2 currLook;

    private Vector2 mouseAimLastDirection;

    public GameObject aimCursor;
    public float aimCursorDistance;
    public float deadzone = 0.1f;

    private GameObject currentSpell;

    public AudioClip fireSound;
    private AudioSource audioSource;

    private void Awake()
    {
        shotCoolDown = 1 / (roundsPerMinute / 60f);
        currLook = new Vector2(0, 0);
        aimCursor.transform.parent = transform;

        Cursor.lockState = CursorLockMode.Confined;

        // TODO make it so this is changed by UI rather then right here
        currentSpell = fireBall;
        audioSource = GetComponent<AudioSource>();
    }

    

    // Update is called once per frame
    void Update()
    {
        HandleAim();
        HandleShot();
    }

    public void SetSpell(GameObject spell)
    {
        currentSpell = spell;
    }

    void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();

        if (lookDelta.magnitude < deadzone)
        {
            return;
        }

        lookDelta.Normalize();


        currLook = lookDelta;
        
    }
    void HandleAim()
    {


        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 currentMouseAimDirection = (new Vector2(mousePos.x, mousePos.y) - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (mouseAimLastDirection != currentMouseAimDirection)
        {
            currLook = currentMouseAimDirection;
        }

        

        mouseAimLastDirection = currentMouseAimDirection;

        aimCursor.transform.localPosition = currLook * aimCursorDistance;
    }

    void HandleShot()
    {
        if (currentShotCooldown > 0)
        {
            currentShotCooldown -= Time.deltaTime;
        }
        if (isShooting && currentShotCooldown <= 0)
        {
            audioSource.PlayOneShot(fireSound);
            GameObject spell = Instantiate(currentSpell);
            spell.transform.position = transform.position;

            Vector2 lookDir = currLook;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            spell.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            currentShotCooldown = shotCoolDown;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, currentAimDirection);
    }

    // This calls on both press down, as well as press up
    void OnFire(InputValue value)
    {
        // Sets to true if called for button being pressed down, sets false for opposite

        isShooting = value.isPressed;

    }
}
