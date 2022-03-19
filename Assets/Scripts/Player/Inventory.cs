using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    private GameObject currentWeapon;

    public GameObject meleeWeapon;
    public GameObject super;
    public int currency;
    private Shooting shooting;


    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = primaryWeapon;
        shooting = GetComponent<Shooting>();
    }


    public GameObject getCurrentWeapon()
    {
        return currentWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPrimaryWeapon(InputValue inputValue)
    {
        if (currentWeapon == primaryWeapon && inputValue.isPressed)
        {
            currentWeapon = secondaryWeapon;

        }
        else
        {
            currentWeapon = primaryWeapon;
        }
        shooting.SetSpell(currentWeapon);
    }

    void OnSecondaryWeapon(InputValue inputValue)
    {
        if (currentWeapon == primaryWeapon && inputValue.isPressed)
        {
            currentWeapon = secondaryWeapon;

        }
        else
        {
            currentWeapon = primaryWeapon;
        }
        shooting.SetSpell(currentWeapon);
    }
}
