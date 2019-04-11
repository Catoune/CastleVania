using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	[HideInInspector]
	public bool isFacingRight = false;
    public bool grounded             ;
    public bool isMoving             ;

    public float VerticalSpeed;
   
	protected Animator          animator        ;
    protected CollisionManager  collManager     ;
    private   WhipAttackManager whipAttManager  ;
	private   HurtManager       hurtManager     ;
	private int horizontalVelocity = 0; // should only have values -1, 0, 1

	public int HorizontalVelocity
	{
		get{return horizontalVelocity ;}
		set{horizontalVelocity = value;}
	}

	void Start ()
    {
		animator         = GetComponent<Animator>         ();
		whipAttManager   = GetComponent<WhipAttackManager>();
		collManager      = GetComponent<CollisionManager> ();
		hurtManager      = GetComponent<HurtManager>      ();

		collManager.ExitGround += handleOnExitGround;
		Flip ();
	}

    void Update()
    {
        if (!animator.GetBool("Hurt")){horizontalVelocity = animator.GetInteger("Speed");}

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
            ButtonUpH();
            isMoving = false;
        }
        if (Input.GetAxis("Vertical") > 0.85){ CrounchOn();}
        if (Input.GetAxis("Vertical") ==   0){CrounchOff();}
        if (Input.GetButtonDown("Jump"  )   ){JumpButton();}
        if (Input.GetButtonDown("Attack")   ){    Attack();}
        //if (Input.GetButtonDown("Attack") && (Input.GetAxis("Vertical") < 0)){ThrowWeapon();}
    }

    void FixedUpdate()
    {
        if (animator.GetInteger("Speed") > 0 && !isFacingRight) { Flip() ;}
        if (animator.GetInteger("Speed") < 0 &&  isFacingRight) { Flip() ;}

        normalFixedUpdate();
    }

    void normalFixedUpdate()
    {
        /*if (collManager.isWallOn(Globals.Direction.Right))
        {
            curHorizontalVelocity = curHorizontalVelocity > 0 ? 0 : curHorizontalVelocity;
        }
        if (collManager.isWallOn(Globals.Direction.Left))
        {
            curHorizontalVelocity = curHorizontalVelocity < 0 ? 0 : curHorizontalVelocity;
        }*/

        // Horizontal Update
        if (collManager.isWallOn(Globals.Direction.Bottom))
        {
            if (VerticalSpeed < 0)
            {
                VerticalSpeed = 0;

                grounded = true;
                animator.SetInteger("Speed", 0);

                //	float reviseHeight = collManager.curBoxTop - collider2D.bounds.min.y;

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
        transform.position = new Vector2(transform.position.x + horizontalVelocity * 0.7f * Time.fixedDeltaTime, transform.position.y);
        transform.position = new Vector2(transform.position.x,transform.position.y + VerticalSpeed * Time.fixedDeltaTime             );
        if (!grounded) { VerticalSpeed += -5.1f * Time.fixedDeltaTime;}
    }

    void ButtonUpH ()
	{
		if (!grounded)               { return                          ;}
		if (horizontalVelocity != 0) { animator.SetInteger("Speed", 0) ;}
	}

	void RightDirection ()
    {
        if (!grounded || whipAttManager.attacking || animator.GetBool("Squat")) {return;}
		if (horizontalVelocity == 0)       {animator.SetInteger("Speed", 1);}
		else if (horizontalVelocity == 1)  {                               ;}
		else if (horizontalVelocity == -1) {animator.SetInteger("Speed", 0);}
	}

	void LeftDirection ()
    {
        if      (!grounded || whipAttManager.attacking || hurtManager.onFlyHurting() || animator.GetBool("Squat")) {return;}
		if      (horizontalVelocity == 0) {animator.SetInteger("Speed", -1);}
		else if (horizontalVelocity == 1) {animator.SetInteger("Speed",  0);}
		else if (horizontalVelocity == -1){                                ;}
	}

    //Jump
	void JumpButton ()
    {
        if (grounded  && !animator.GetBool("Squat")
                      && !animator.GetBool("Jump" )
                      && !whipAttManager.attacking)
        {StartCoroutine(Jump());}
	}

    IEnumerator Jump()
    {
        animator.SetBool("Jump", true);
        grounded = false;
        VerticalSpeed = 2f;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Jump", false);
    }

    //Attaque du Personnage
    void Attack ()
    {
        if (!whipAttManager.attacking) { StartCoroutine(whipAttManager.WhipAttack());}
	}

	
    //S'accroupir
	void CrounchOn ()
    {
        animator.SetInteger("Speed", 0);
        if (!grounded || whipAttManager.attacking)   {return                         ;}
        else                                         {animator.SetBool("Squat", true);}		
	}

    //Se relever
	void CrounchOff ()
    {
		animator.SetBool ("Squat", false);
	}

	public virtual void Hurt()
    {
        if (!hurtManager.Hurting && !animator.GetBool("Dead")) { StartCoroutine(hurtManager.Hurt());}
	}

	void handleOnExitGround()
    {
		if (animator.GetBool("Jump") ||hurtManager.Hurting) {return;}
		VerticalSpeed = -5f;
	}

	public void Flip()
    {
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
