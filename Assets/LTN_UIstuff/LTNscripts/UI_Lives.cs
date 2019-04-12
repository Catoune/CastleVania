using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lives : MonoBehaviour
{
    private Text text;
    StatusManager lifeCount;

    void Start()
    {
        text = GetComponent<Text>();
        lifeCount = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string lifeNumber = lifeCount.heart.ToString("D9");
        text.text = lifeNumber;
    }
}
