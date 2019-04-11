using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour
{

	public int        score = 0;
	public int        time  = 300;
	public int        heart = 0;
	public static int lives = 3;
	public int playerHealth = Globals.maxPlayerHealth;

	private static float prevPos = 0.0f;
    public bool savePos = false;

    public bool bossDefeated = false;
	private bool isDying = false;
	private Animator animator;

	void Awake ()
    {
		positionControl ();
	}

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (playerHealth <= 0 && !isDying) 
		{
			isDying = true;
			playerDie();	
		}
	}

	public void changeBGM()
	{
		GameObject BGM = Resources.Load ("Prefab/ AudioObject/BGMcus") as GameObject;	
		Instantiate (BGM, transform.position, Quaternion.identity);
	}

	void positionControl()
	{
		// If the PreviousPlayerPosition already exists, read it
		if (PlayerPrefs.HasKey("PreviousPlayerPosition")) {prevPos = PlayerPrefs.GetFloat("PreviousPlayerPosition");}
		// Assign the prevPos to PreviousPlayerPosition
		PlayerPrefs.SetFloat("PreviousPlayerPosition", prevPos); 

		if(savePos)
			transform.position = new Vector3 (prevPos, transform.position.y, transform.position.z);
	}

	public void playerDie()
	{
		StartCoroutine (playerDying());

	}

	IEnumerator playerDying()
	{
		lives--;
		animator.SetBool ("Dead", true);

		yield return new WaitForSeconds (2.5f);

		Destroy (GameObject.Find("InputManager"));

		GameObject transEvent = Resources.Load ("Prefab/transEvent") as GameObject;
		Instantiate (transEvent, transform.position, Quaternion.identity);
	}


	void OnDestroy ()
    {
		PlayerPrefs.SetFloat("PreviousPlayerPosition", transform.position.x);
	}
}
