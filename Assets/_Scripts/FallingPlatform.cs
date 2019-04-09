using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;

    public GameObject fallingPlatform;
    public Transform generationPoint;
    public static float breakTimer;
    public static bool breakTimerStart = false;
    public static float spawnTimer;
    public static bool spawnTimerStart = false;
    public static bool isPlatformDestroyed = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        generationPoint = fallingPlatform.GetComponent<Transform>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            breakTimerStart = true;
            animator.SetBool("TimerStart", true);

            if (breakTimer == 3)
            {
                Destroy(this);
                isPlatformDestroyed = true;
            }
        }
    }

    void Update()
    {
        if (isPlatformDestroyed == true)
        {
            spawnTimerStart = true;
        }

        if (breakTimerStart == true)
        {
            breakTimer += Time.deltaTime;
        }

        if (spawnTimerStart == true)
        {
            spawnTimer += Time.deltaTime;
        }

        if (spawnTimer == 10)
        {
            Instantiate(fallingPlatform, generationPoint.position, generationPoint.rotation);
        }
    }
}
