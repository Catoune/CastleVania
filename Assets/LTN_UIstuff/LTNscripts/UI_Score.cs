﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Score : MonoBehaviour
{
    private StatusManager status;
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        string score = status.score.ToString("D6");
        text.text = score;
    }
}
