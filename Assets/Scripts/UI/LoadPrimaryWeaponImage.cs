using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPrimaryWeaponImage : MonoBehaviour
{
    private GameObject spell;
    private GameObject player;
    private Inventory inventory;
    public Image imageContainer;
    public Image childImage;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        spell = inventory.primaryWeapon;
        childImage.sprite = spell.GetComponent<SpriteRenderer>().sprite;
        childImage.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        if (spell == inventory.getCurrentWeapon())
        {
            imageContainer.color = new Color32(118, 255, 46, 123);
        }
        else
        {
            imageContainer.color = Color.white;
        }




    }
}
