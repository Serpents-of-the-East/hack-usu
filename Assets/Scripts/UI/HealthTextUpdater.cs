using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class HealthTextUpdater : MonoBehaviour
{
    private TextMeshProUGUI healthText;
    private GameObject player;
    private Health playerHealth;


    private void Awake()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        healthText.SetText($"{playerHealth.health}/{playerHealth.maxHealth} HP");
    }
}
