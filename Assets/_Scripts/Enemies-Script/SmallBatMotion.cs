using UnityEngine;
using System.Collections;

public class SmallBatMotion : MonoBehaviour
{
	public float HorizontalSpeed; // adjust in inspector 
	public float VerticalSpeed; // should be private 
	public float omega;
	public float amplitude;

	private const float perishInSec = 1.0f;
	private bool isAwake;
	private Vector2 speed;
	
	private bool onGround;
	public bool facingRight; // true for right 
	private Animator animator;
	private float inittime;

	void Start ()
    {
		inittime = Time.time;
		isAwake = false;
		VerticalSpeed = 0.0f;

		facingRight = true;

		GameObject playerObj = GameObject.FindGameObjectWithTag (Globals.playerTag);
		if(playerObj.transform.position.x < transform.position.x)
		{
			facingRight = false;
		}

		if (!facingRight)
			Flip();

		animator = GetComponent<Animator> ();
		wakeUp ();
	}
	public void wakeUp()
	{
		isAwake = true;
		animator.SetBool ("Fly", true);
	}
	
	void FixedUpdate ()
    {
		if(isAwake)
			move ();
	}

	void move()
	{
		int direction = facingRight ? 1 : -1;
		VerticalSpeed = amplitude * Mathf.Cos (omega * (Time.time - inittime));
		transform.position = new Vector2(transform.position.x + HorizontalSpeed * Time.fixedDeltaTime * direction,
		                                 transform.position.y + VerticalSpeed * Time.fixedDeltaTime);
	}
	
	
	void OnTriggerEnter2D( Collider2D coll )
    {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			onPlayerEnter(coll.gameObject);		              
		}
	}
	
	void onPlayerEnter(GameObject gb)
	{
		PlayerController pcScript = gb.GetComponent<PlayerController> ();
		pcScript.Hurt ();

		OnWhipHitDestroy owhScript = GetComponent<OnWhipHitDestroy>();
		owhScript.onWhipEnter();
	}

	public void Flip()
    {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
