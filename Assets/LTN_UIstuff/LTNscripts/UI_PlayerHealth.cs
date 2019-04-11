﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealth : MonoBehaviour
{

    public Image image;
    [SerializeField]
    Sprite[] healthBar;
    StatusManager status;


    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
        //healthBar = GetComponent<Image> ();
        healthBar = new Sprite[17];

    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = healthBar[playerHealth];


    }
}
