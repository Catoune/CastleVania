using UnityEngine;
using System.Collections;

public class LeoWake : MonoBehaviour
{	
	void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag == "Player")
        {
			leoWake();
			GetComponent<Collider2D>().enabled = false;
		}
	}
	
	void leoWake()
	{
		LeoMotion lmScript = GetComponentInParent<LeoMotion>();
		lmScript.wakeUp ();
	}
}
