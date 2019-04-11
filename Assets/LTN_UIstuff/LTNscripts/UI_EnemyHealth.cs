using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHealth : MonoBehaviour
{
    private Image image;

    [SerializeField]
    Sprite[] healthBar;
    BossMotion eStatus;



    // Start is called before the first frame update
    void Start()
    {
        eStatus = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossMotion>();
        healthBar = new Sprite[17];

    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = healthBar[bossHealth];


    }
}
