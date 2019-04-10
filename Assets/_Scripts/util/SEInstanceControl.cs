using UnityEngine;
using System.Collections;

public class SEInstanceControl : MonoBehaviour
{
	private static SEInstanceControl instance = null;

	void Awake()
    {
		if (instance != null && instance != this)
		{
			Destroy(instance.gameObject);
		}
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
}
