using UnityEngine;
using System.Collections;

public class BatWake : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == Globals.playerTag) 
		{
			batWake();
			GetComponent<Collider2D>().enabled = false;
		}
	}
	
	void batWake()
	{
		SmallBatMotion sbScript = GetComponentInParent<SmallBatMotion>();
		sbScript.wakeUp ();
	}
}
