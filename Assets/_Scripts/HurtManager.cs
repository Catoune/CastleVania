using UnityEngine;
using System.Collections;

public class HurtManager : MonoBehaviour
{
	private bool hurting;

    //Juste un Get, pas besoin de Set.
	public bool Hurting
    {
		get {return hurting || disableControl;}
	}

	private bool disableControl;

	private Animator animator;
	private PlayerController pc;
	private SpriteRenderer sprite;
	private StatusManager status;

	void Start ()
    {
		animator = GetComponent<Animator        >();
		pc       = GetComponent<PlayerController>();
		sprite   = GetComponent<SpriteRenderer  >();
		status   = GetComponent<StatusManager   >();

		hurting        = false;
		disableControl = false;
	}

	public bool onFlyHurting (){return disableControl;} //Permet d'avoir la condition

    public IEnumerator Hurt()
    {
        status.playerHealth -= 2; //Vie que le joueur perd
        hurting        = true;
        disableControl = true;

        animator.SetBool("Hurt", true);
        pc.VerticalSpeed      = 1.7f                     ;
        pc.HorizontalVelocity = pc.isFacingRight ? -1 : 1;
        StartCoroutine(Invisible());

        yield return new WaitForSeconds(0.33f);
        animator.SetBool   ("Hurt" , false);
        animator.SetInteger("Speed",     0);

        yield return new WaitForSeconds(   0f);
        pc.HorizontalVelocity = 0;
        disableControl        = false;
    }
	
    //Effet d'invisibilité lorsque touché
	public IEnumerator Invisible ()
    {
		hurting        = true;
		Color curColor = sprite.color;
		curColor.a     = curColor.a/2; //Alpha coupé en 2
		sprite.color   = curColor    ;

		yield return new WaitForSeconds (1.0f);
		curColor.a   = 1.0f          ; //Alpha de base
		sprite.color = curColor      ;
		hurting = false              ;
	}
}
