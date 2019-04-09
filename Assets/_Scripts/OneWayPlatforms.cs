using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatforms : MonoBehaviour
{
    private PlatformEffector2D effector;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetButton("Effector"))
        {
            effector.rotationalOffset = 180f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
