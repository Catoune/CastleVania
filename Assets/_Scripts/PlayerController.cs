using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float jumpHeight = 2.0f;
	public float HorizonalSpeedScale; // define in editor
	public float initVerticalSpeed;
	public float DropVerticalSpeed;
	public float StairStepLength; // absolute value.
	public float VerticalAccerlation;


	// [HideInInspector]
	public bool isFacingRight = false;
	[HideInInspector]
	public float VerticalSpeed;
	// not on stairs curHorizontalVelocity * HorizonalSpeedScale
	// on stairs, a constant value, involving +-
	[HideInInspector]
	public float HorizontalSpeed; 
	public bool grounded;

    public float moveX;
    public bool isMoving;

	protected Animator animator;
	private WhipAttackManager whipAttManager;
	protected CollisionManager collManager;
	private SubWeaponManager subWeaponManager;
	private HurtManager hurtManager;
	private int curHorizontalVelocity = 0; // should only have values -1, 0, 1

	public int CurHorizontalVelocity
	{
		get{return curHorizontalVelocity ;}
		set{curHorizontalVelocity = value;}
	}

	void Start ()
    {
		animator = GetComponent<Animator> ();
		whipAttManager = GetComponent<WhipAttackManager> ();
		collManager = GetComponent<CollisionManager> ();
		subWeaponManager = GetComponent<SubWeaponManager> ();
		hurtManager = GetComponent<HurtManager> ();

		collManager.ExitGround += handleOnExitGround;
		Flip ();
	}

    void Update()
    {
        if (!animator.GetBool("Hurt")){curHorizontalVelocity = animator.GetInteger("Speed");}

        if(Input.GetAxis("Horizontal")>0)
        {
            RightDirection();
            isMoving = true;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            LeftDirection();
            isMoving = true;
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            KeyUpH();
            isMoving = false;
        }
        if(Input.GetAxis("Vertical") > 0.85)
        {
            CrounchOn();
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            CrounchOff();
        }
        if (Input.GetButtonDown("Jump"))
        {
            JumpButton();
        }
        if(Input.GetButtonDown("Attack"))
        {
            Attack();
        }
        if(Input.GetButtonDown("Attack") && (Input.GetAxis("Vertical") < 0))
        {
            ThrowWeapon();
        }
    }

    void FixedUpdate()
    {
        if (animator.GetInteger("Speed") > 0 && !isFacingRight) { Flip() ;}
        if (animator.GetInteger("Speed") < 0 &&  isFacingRight) { Flip() ;}

        normalFixedUpdate();
    }


    /*void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Stair")
        {
            
        }
    }*/

    void normalFixedUpdate()
    {
        if (collManager.isWallOn(Globals.Direction.Right))
        {
            curHorizontalVelocity = curHorizontalVelocity > 0 ? 0 : curHorizontalVelocity;
        }
        if (collManager.isWallOn(Globals.Direction.Left))
        {
            curHorizontalVelocity = curHorizontalVelocity < 0 ? 0 : curHorizontalVelocity;
        }

        // Horizontal Update
        if (collManager.isWallOn(Globals.Direction.Bottom))
        {
            if (VerticalSpeed < 0)
            {
                VerticalSpeed = 0;

                grounded = true;
                animator.SetInteger("Speed", 0);

                //				float reviseHeight = collManager.curBoxTop - collider2D.bounds.min.y;

                // Vertical overwrite update
                /*transform.position = new Vector2(
                    transform.position.x,
                    collManager.curBoxTop + GetComponent<Collider2D>().bounds.size.y / 2.0f
                    );*/
            }
        }
        else
        {
            grounded = false;
        }
        transform.position = new Vector2(transform.position.x + curHorizontalVelocity * HorizonalSpeedScale * Time.fixedDeltaTime, transform.position.y);
        // Vertical update
       transform.position = new Vector2(
            transform.position.x,
            transform.position.y + VerticalSpeed * Time.fixedDeltaTime
            );
        if (!grounded)
            VerticalSpeed += VerticalAccerlation * Time.fixedDeltaTime;
    }

    void KeyUpH ()
	{
		if (!grounded)                  { return                          ;}
		if (curHorizontalVelocity != 0) { animator.SetInteger("Speed", 0) ;}
	}

	void RightDirection ()
    {
        if (!grounded || whipAttManager.attacking || animator.GetBool("Squat")) {return;}
		if (curHorizontalVelocity == 0)       {animator.SetInteger("Speed", 1);}
		else if (curHorizontalVelocity == 1)  {                               ;}
		else if (curHorizontalVelocity == -1) {animator.SetInteger("Speed", 0);}
	}

	void LeftDirection ()
    {
        if      (!grounded || whipAttManager.attacking || hurtManager.onFlyHurting() || animator.GetBool("Squat")) {return;}
		if      (curHorizontalVelocity == 0) {animator.SetInteger("Speed", -1);}
		else if (curHorizontalVelocity == 1) {animator.SetInteger("Speed",  0);}
		else if (curHorizontalVelocity == -1) {;}
	}

	void KeyDownUp ()
	{
        if (!grounded || whipAttManager.attacking || subWeaponManager.throwing){ return;}
	}

    //Jump
	void JumpButton ()
    {
		if (grounded 
		    && !animator.GetBool("Squat")
		    && !animator.GetBool("Jump")
		    && !whipAttManager.attacking)
			StartCoroutine(Jump());
	}

    IEnumerator Jump()
    {
        animator.SetBool("Jump", true);
        grounded = false;
        VerticalSpeed = initVerticalSpeed;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Jump", false);
    }

    //Attaque du Personnage
    void Attack ()
    {
        if (!whipAttManager.attacking) { StartCoroutine(whipAttManager.WhipAttack()); }
	}

	
    //S'accroupir
	void CrounchOn ()
    {
        animator.SetInteger("Speed", 0);
        if (!grounded || whipAttManager.attacking)   {return;}
        else
        {
            animator.SetBool("Squat", true);
        }		
	}

    //Se relever
	void CrounchOff ()
    {
		animator.SetBool ("Squat", false);
	}

	void ThrowWeapon ()
    {
        if      (!subWeaponManager.throwing && subWeaponManager.isCarrying && !whipAttManager.attacking) { StartCoroutine(subWeaponManager.Throw())  ;}
        else if (!whipAttManager.attacking)                                                              { StartCoroutine(whipAttManager.WhipAttack());}
	}

	public virtual void Hurt()
    {
        if (!hurtManager.Hurting && !animator.GetBool("Dead")) { StartCoroutine(hurtManager.Hurt());}
	}

	void handleOnExitGround()
    {
		if (animator.GetBool("Jump") ||hurtManager.Hurting) {return;}
		VerticalSpeed = DropVerticalSpeed;
	}

	public void Flip()
    {
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
