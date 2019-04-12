using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Score : MonoBehaviour
{

    private Image image;
    private StatusManager status;
    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
        text = GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
