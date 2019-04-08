using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDetector : MonoBehaviour
{
    public GameObject player;

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Stair" && player.GetComponent<PlayerController>().isMoving)
        {
            if (player.GetComponent<PlayerController>().isFacingRight )
            {
                player.transform.position = new Vector3(transform.position.x + 0.099f, transform.position.y + 0.1f, transform.position.z);
            }
            else
            {
                player.transform.position = new Vector3(transform.position.x + -0.099f, transform.position.y + 0.1f, transform.position.z);
            }
        }
    }
}
