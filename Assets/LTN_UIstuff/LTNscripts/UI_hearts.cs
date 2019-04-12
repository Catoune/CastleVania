using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_hearts : MonoBehaviour
{

    private Text text;
    StatusManager heartCount;
    
    void Start()
    {
        text = GetComponent<Text>();
        heartCount = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string heartNumber = heartCount.heart.ToString("D8");
        text.text = heartNumber;
    }
}
