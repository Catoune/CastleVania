using UnityEngine;
using System.Collections;

public class DestoryByTime : MonoBehaviour
{
	public float lingerTime = 0.5f;

	void Start ()
    {
		Destroy (gameObject, lingerTime);
	}
}
