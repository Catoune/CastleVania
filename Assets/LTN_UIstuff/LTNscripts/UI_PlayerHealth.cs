using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{

    private Image image;
    [SerializeField]
    Sprite[] healthBar;
    StatusManager status;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
        //status = playerHealth;
        healthBar = new Sprite[16];
    }

    // Update is called once per frame
    void Update()
    {
        int playerHealth = status.playerHealth;
        image.sprite = healthBar[playerHealth];        
    }
}
