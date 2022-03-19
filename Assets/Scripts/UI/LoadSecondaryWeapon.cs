using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSecondaryWeapon : MonoBehaviour
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
        spell = inventory.secondaryWeapon;
        GetComponent<Image>().sprite = spell.GetComponent<SpriteRenderer>().sprite;
        GetComponent<Image>().color = Color.white;
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
