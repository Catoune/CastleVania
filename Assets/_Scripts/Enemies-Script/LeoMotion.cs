using UnityEngine;
using System.Collections;

public class LeoMotion : MonoBehaviour
{
	public float initVerticalSpeed;
	public float HorizontalSpeed; 
	public float VerticalSpeed;
	public float VerticalAccerlation;

	private bool isAwake;

	private bool onGround;
	public bool facingRight = false; 
	private Animator animator;
    private GameObject player;

	void Start ()
    {
		isAwake = false;
		VerticalSpeed = 0.0f;
		CollisionManager cmScript = GetComponent<CollisionManager>();
		animator = GetComponent<Animator> ();
        player = GameObject.Find("Player");

		cmScript.ExitGround  += onExitGround ;
		cmScript.EnterGround += onEnterGround;
	}

	public void wakeUp()
	{
		isAwake = true;
		animator.SetBool ("Jump", true);
		VerticalSpeed = initVerticalSpeed;
	}
	
	void FixedUpdate ()
    {
        if (isAwake) { move(); }
	}

	void onEnterGround()
	{
		if (isAwake && player.transform.position.x > transform.position.x)
        {
            HorizontalSpeed *= -1;
			VerticalSpeed = 0.0f;
			animator.SetBool ("Jump", false);
			onGround = true;
		}
        else if (isAwake && player.transform.position.x < transform.position.x)
        {
            HorizontalSpeed *= 1;
            VerticalSpeed = 0.0f;
            animator.SetBool("Jump", false);
            onGround = true;
        }
    }

	void onExitGround()
	{
		VerticalSpeed = initVerticalSpeed;
		onGround = false;
	}

	void move()
	{
		transform.position = new Vector2(transform.position.x + HorizontalSpeed * Time.fixedDeltaTime ,
		                                 transform.position.y + VerticalSpeed   * Time.fixedDeltaTime);
        if (!onGround) { VerticalSpeed += VerticalAccerlation * Time.fixedDeltaTime; }
	}
	
	
	void OnTriggerEnter2D( Collider2D coll )
    {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			onPlayerEnter(coll.gameObject);		              
		}
		else if(collidedObj.tag == "Ground")
		{
            onEnterGround();
		}
	}
	
	void onPlayerEnter(GameObject gb)
	{
		PlayerController pcScript = gb.GetComponent<PlayerController> ();
		pcScript.Hurt ();
	}
}
