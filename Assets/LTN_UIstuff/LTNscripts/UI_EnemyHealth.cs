using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHealth : MonoBehaviour
{
    private Image image;
    //BossMotion eStatus;

    [SerializeField]
    Sprite[] healthBar;



    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();        
        /*eStatus = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossMotion>();
        eStatus.bossHealth = Globals.maxBossHealth;*/
        healthBar = new Sprite[17];

    }

    // Update is called once per frame
    void Update()
    {
        /*int bossHealth = eStatus.bossHealth;
        image.sprite = healthBar[bossHealth];*/
    }
}
